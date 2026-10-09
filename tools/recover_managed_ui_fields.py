#!/usr/bin/env python3
"""Opt-in, source-bound IL2CPP TypeTree recovery. Never modifies the XAPK.

IL2CPP metadata version 31 is NOT a Unity SerializedFile TypeTree version.
Class-name strings and runtime memory offsets are not serialized field offsets.
This module only accepts a TypeTree generated from the exact XAPK pair
(libil2cpp.so, global-metadata.dat), with strict full-object parsing and
independent source owner/MonoScript checks. No binary content is exported.
"""
from __future__ import annotations

import hashlib
import json
import math
import shutil
import tempfile
import zipfile
from pathlib import Path

import audit_local_ui_components as ui
import il2cpp_refs as refs

MAX_APK = 1024 * 1024 * 1024
MAX_LIBRARY = 384 * 1024 * 1024
MAX_OBJECT = 2 * 1024 * 1024


class RecoveryBlocked(ValueError):
    """Fail closed: do not apply or label any guessed source fields."""


def _nested_members(xapk):
    """Yield temporary seekable nested APKs, without persisting copyrighted data."""
    if not xapk.is_file() or not zipfile.is_zipfile(xapk):
        raise RecoveryBlocked("Canonical source XAPK not accessible")
    with zipfile.ZipFile(xapk) as outer, tempfile.TemporaryDirectory(
        prefix="haitac_binary_proof_"
    ) as tmp:
        for index, member in enumerate(outer.infolist()):
            if member.is_dir() or not member.filename.lower().endswith(".apk"):
                continue
            if not 0 < member.file_size <= MAX_APK:
                raise RecoveryBlocked("Nested APK size exceeds strict limit")
            path = Path(tmp) / (str(index) + ".apk")
            with outer.open(member) as source, path.open("wb") as target:
                shutil.copyfileobj(source, target, 1024 * 1024)
            try:
                with zipfile.ZipFile(path) as apk:
                    yield member.filename, apk
            finally:
                path.unlink(missing_ok=True)


def read_source_pair(xapk):
    """Return in-memory source bytes and SHA256 proofs; reject duplicate candidates."""
    found = {}
    for apk_name, apk in _nested_members(Path(xapk)):
        for member in apk.infolist():
            name = member.filename.replace("\\", "/").lower()
            kind = ("metadata" if name.endswith("/global-metadata.dat")
                    else "library" if name.endswith("/libil2cpp.so") else None)
            if kind is None:
                continue
            limit = refs.MAX_METADATA_BYTES if kind == "metadata" else MAX_LIBRARY
            if not 8 <= member.file_size <= limit:
                raise RecoveryBlocked("Unexpected or oversized IL2CPP " + kind)
            if kind in found:
                raise RecoveryBlocked("Ambiguous duplicate IL2CPP " + kind)
            data = apk.read(member)
            if kind == "metadata":
                header = refs.metadata_header(data[:264], len(data))
                if (header.get("state") != "standard_header" or
                        header.get("version") != 31):
                    raise RecoveryBlocked("Source IL2CPP metadata is not validated v31")
            elif not data.startswith(b"\x7fELF"):
                raise RecoveryBlocked("Source libil2cpp.so is not an ELF binary")
            found[kind] = (data, {
                "sourceApk": apk_name,
                "sha256": hashlib.sha256(data).hexdigest(),
                "byteLength": len(data),
            })
    if set(found) != {"metadata", "library"}:
        raise RecoveryBlocked("Source XAPK lacks the complete IL2CPP binary pair")
    return found


def exact_unity_version(scenes):
    """Only the serialized source's own version is acceptable, never editor defaults."""
    versions = {str(scene.get("unityVersion", "")).split("\n", 1)[0].strip()
                for scene in scenes}
    if len(versions) != 1:
        raise RecoveryBlocked("SerializedFiles do not agree on game Unity version")
    version = next(iter(versions))
    if (not version or version.startswith("0.0.0") or
            not any(c.isdigit() for c in version) or len(version) > 48):
        raise RecoveryBlocked("Game Unity version is stripped or unverified")
    return version


def source_generator(xapk, version, factory=None):
    """Create generated TypeTrees from this exact source build only."""
    if factory is None:
        try:
            from UnityPy.helpers.TypeTreeGenerator import TypeTreeGenerator
        except ImportError as exc:
            raise RecoveryBlocked(
                "Install optional TypeTreeGeneratorAPI for local binary recovery"
            ) from exc
        factory = TypeTreeGenerator
    found = read_source_pair(xapk)
    try:
        generator = factory(version)
        generator.load_il2cpp(found["library"][0], found["metadata"][0])
    except (Exception) as exc:
        raise RecoveryBlocked(
            "Source IL2CPP TypeTree generation unsupported: " + type(exc).__name__
        ) from exc
    return generator, {
        "generator": "EXACT_XAPK_IL2CPP_BINARY_PAIR",
        "gameUnityVersion": version,
        "library": found["library"][1],
        "metadata": found["metadata"][1],
    }


def _valid_field(name, value):
    """Conservative field validation; never coerce an invalid layout into UI."""
    if name in ("m_Type", "m_UiScaleMode", "m_ScreenMatchMode",
                "m_FillMethod", "m_StartCorner", "m_StartAxis",
                "m_ChildAlignment", "m_Constraint", "m_AspectMode"):
        return type(value) is int and 0 <= value <= 12
    if name in ("m_PreserveAspect", "m_FillClockwise", "m_ShowMaskGraphic",
                "m_ChildControlWidth", "m_ChildControlHeight",
                "m_ChildForceExpandWidth", "m_ChildForceExpandHeight"):
        return type(value) is bool
    if name in ("m_FillAmount", "m_MatchWidthOrHeight", "m_Alpha"):
        return type(value) in (float, int) and 0 <= value <= 1
    if name == "m_ReferenceResolution":
        return (isinstance(value, dict) and
                set(value) == {"x", "y"} and
                all(type(v) in (int, float) and math.isfinite(v) and
                    0 < v <= 32768 for v in value.values()))
    if name == "m_Color":
        return (isinstance(value, dict) and set(value) == {"r", "g", "b", "a"}
                and all(type(v) in (int, float) and math.isfinite(v)
                        and 0 <= v <= 4 for v in value.values()))
    if name == "m_Padding":
        return (isinstance(value, dict) and bool(value) and
                set(value).issubset({"m_Left", "m_Right", "m_Top", "m_Bottom"}))
    if name in ("m_CellSize", "m_Softness"):
        return (isinstance(value, dict) and set(value) == {"x", "y"} and
                all(type(v) in (int, float) and math.isfinite(v)
                        for v in value.values()))
    if name == "m_Spacing":
        return ((type(value) in (int, float) and math.isfinite(value)) or
                (isinstance(value, dict) and set(value) == {"x", "y"} and
                 all(type(v) in (int, float) and math.isfinite(v)
                     for v in value.values())))
    if name in ("m_ScaleFactor", "m_ReferencePixelsPerUnit",
                "m_AspectRatio"):
        return type(value) in (int, float) and math.isfinite(value) and value > 0
    if name == "m_ConstraintCount":
        return type(value) is int and 0 <= value <= 100000
    return value is not None


def verified_fields(reader, row, generator):
    """Decode one *already source-identified* MonoBehaviour, all-or-nothing."""
    if (row.get("kind") != "MonoBehaviour" or
            row.get("className") not in ui.FIELDS or
            not row.get("assembly") or
            row.get("scriptResolution") not in
            ("LOCAL_PATHID", "EXTERNAL_RESOLVED", "UNITYPY_DEREF",
             "GLOBAL_XAPK_EXACT_FILE_ALIAS_PATHID")):
        raise RecoveryBlocked("Missing exact MonoBehaviour/MonoScript provenance")
    raw = reader.get_raw_data()
    if not raw or len(raw) > MAX_OBJECT:
        raise RecoveryBlocked("Serialized object is absent or oversized")
    try:
        nodes = generator.get_nodes_up(row["assembly"], row["className"])
        if nodes is None:
            raise RecoveryBlocked("Generated TypeTree is unavailable")
        # UnityPy check_read=True requires the TypeTree to consume the full object.
        decoded = reader.read_typetree(nodes=nodes, check_read=True)
    except Exception as exc:
        raise RecoveryBlocked("Generated TypeTree failed strict object parsing: " +
                              type(exc).__name__) from exc
    if not isinstance(decoded, dict):
        raise RecoveryBlocked("Generated data is not an object")
    if refs.pptr(decoded.get("m_GameObject")) != (0, row["gameObjectId"]):
        raise RecoveryBlocked("Decoded GameObject owner does not match source")
    expected = row.get("scriptPointer", {})
    if refs.pptr(decoded.get("m_Script")) != (
        expected.get("fileId"), expected.get("pathId")
    ):
        raise RecoveryBlocked("Decoded MonoScript pointer does not match source")
    if ("nativeEnabled" in row and
            decoded.get("m_Enabled") != row["nativeEnabled"]):
        raise RecoveryBlocked("Decoded enabled flag contradicts source header")
    fields = {}
    for name in ui.FIELDS[row["className"]]:
        if name not in decoded:
            continue
        value = ui.plain(decoded[name])
        if value is None or not _valid_field(name, value):
            raise RecoveryBlocked("Invalid recovered UI field: " + name)
        fields[name] = value
    if not fields:
        raise RecoveryBlocked("No target managed fields in generated TypeTree")
    return fields, {
        "method": "SOURCE_IL2CPP_GENERATED_TYPETREE",
        "rawObjectSha256": hashlib.sha256(raw).hexdigest(),
        "rawObjectBytes": len(raw),
        "exactSourcePointerChecked": True,
        "strictObjectSizeChecked": True,
        "sourceFields": sorted(fields),
    }
