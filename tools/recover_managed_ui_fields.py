#!/usr/bin/env python3
"""Opt-in, source-bound IL2CPP TypeTree recovery. Never modifies the XAPK.

IL2CPP metadata version 31 is NOT a Unity SerializedFile TypeTree version.
Class-name strings and runtime memory offsets are not serialized field offsets.
This module only accepts a TypeTree generated from the exact XAPK pair
(libil2cpp.so, global-metadata.dat), with strict full-object parsing and
independent source owner/MonoScript checks. No binary content is exported.
"""
from __future__ import annotations

import copy
import hashlib
import json
import math
import shutil
import traceback
import tempfile
import zipfile
from pathlib import Path

import audit_local_ui_components as ui
import il2cpp_refs as refs

MAX_APK = 1024 * 1024 * 1024
MAX_LIBRARY = 384 * 1024 * 1024
MAX_OBJECT = 2 * 1024 * 1024


class RecoveryBlocked(ValueError):
    """Fail closed with a stable stage/code, never with inferred source values."""

    def __init__(self, message, *, phase="preflight", code="SOURCE_BLOCKED",
                 frame=None, tree=None):
        super().__init__(message)
        self.phase = phase
        self.code = code
        self.frame = frame
        self.tree = tree


def _safe_exception_frame(exc):
    """Expose only the failing Python function, not local paths or source bytes."""
    stack = traceback.extract_tb(exc.__traceback__)
    if not stack:
        return "UNKNOWN_FRAME"
    frame = stack[-1]
    return f"{Path(frame.filename).name}:{frame.lineno}:{frame.name}"[:140]


def _tree_summary(node):
    """Inspect generated schema shape only, not serialized field values."""
    children = getattr(node, "m_Children", None)
    if not isinstance(children, (list, tuple)):
        return {"rootType": str(getattr(node, "m_Type", "ABSENT"))[:80],
                "rootLevel": getattr(node, "m_Level", None),
                "childCount": None, "headerNodes": []}
    expected = ("m_GameObject", "m_Script", "m_Enabled")
    names = {getattr(child, "m_Name", None) for child in children}
    return {"rootType": str(getattr(node, "m_Type", "ABSENT"))[:80],
            "rootLevel": getattr(node, "m_Level", None),
            "childCount": len(children),
            "headerNodes": sorted(set(expected).intersection(names))}



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


def source_generator(xapk, version, factory=None, backend="AssetsTools"):
    """Generate source-bound schema from an explicitly selected native backend."""
    if backend not in ("AssetsTools", "AssetStudio", "AssetRipper"):
        raise RecoveryBlocked("Unsupported IL2CPP TypeTree backend",
                              phase="preflight", code="UNTRUSTED_GENERATOR_BACKEND")
    if factory is None:
        try:
            from UnityPy.helpers.TypeTreeGenerator import TypeTreeGenerator
        except ImportError as exc:
            raise RecoveryBlocked(
                "Install optional TypeTreeGeneratorAPI for local binary recovery"
            ) from exc
        factory = lambda source_version: TypeTreeGenerator(
            source_version, generator=backend)
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
        "backend": backend,
        "gameUnityVersion": version,
        "library": found["library"][1],
        "metadata": found["metadata"][1],
    }


def verified_native_header_root(generated, native):
    """Recombine engine native header with IL2CPP-derived managed schema.

    This DOES NOT reconstruct unknown field offsets or accept any field value.
    UnityPy's exact-version native MonoBehaviour TypeTree is independently
    checked against source in parse_monobehaviour_head(); the final combined
    tree must still fully consume raw bytes and match all source PPtrs.
    """
    if (getattr(generated, "m_Level", None) != 0 or
            getattr(native, "m_Level", None) != 0 or
            getattr(native, "m_Type", None) != "MonoBehaviour"):
        raise RecoveryBlocked("Native or generated MonoBehaviour root invalid",
                              phase="native_header", code="INVALID_NATIVE_HEADER_ROOT")
    derived = getattr(generated, "m_Children", None)
    header = getattr(native, "m_Children", None)
    if not isinstance(derived, (list, tuple)) or not isinstance(header, (list, tuple)):
        raise RecoveryBlocked("Native or derived child nodes absent",
                              phase="native_header", code="MISSING_HEADER_CHILDREN")
    header_names = [getattr(child, "m_Name", None) for child in header]
    derived_names = [getattr(child, "m_Name", None) for child in derived]
    if (len(header_names) != len(set(header_names)) or
            len(derived_names) != len(set(derived_names)) or
            not {"m_GameObject", "m_Script", "m_Enabled"}.issubset(header_names) or
            not {"m_GameObject", "m_Script", "m_Enabled"}.issubset(derived_names)):
        raise RecoveryBlocked("Header fields missing or ambiguous",
                              phase="native_header", code="AMBIGUOUS_HEADER_FIELDS")
    header_set = set(header_names)
    managed_children = [child for child in derived if
                        getattr(child, "m_Name", None) not in header_set]
    if not managed_children:
        raise RecoveryBlocked("No managed fields after native header",
                              phase="native_header", code="NO_MANAGED_NODES")
    # UnityPyBoost.TypeTreeNode is a native extension and is not pickle/copy
    # compatible. Construct a fresh node with the documented UnityPy API
    # rather than mutating the generator's cached root.
    try:
        from UnityPy.helpers.TypeTreeNode import TypeTreeNode
    except ImportError:
        TypeTreeNode = None
    if TypeTreeNode is not None and isinstance(generated, TypeTreeNode):
        merged = TypeTreeNode(
            generated.m_Level, generated.m_Type, generated.m_Name,
            generated.m_ByteSize, generated.m_Version,
            m_MetaFlag=generated.m_MetaFlag)
        merged.m_Children.extend(list(header) + managed_children)
    else:
        # Lightweight pure-Python test doubles only.
        merged = copy.copy(generated)
        merged.m_Children = list(header) + managed_children
    return merged


def exact_source_unity_header(reader):
    """Use the SAME engine-version node used by parse_monobehaviour_head."""
    try:
        from UnityPy.enums import ClassIDType
        from UnityPy.helpers.Tpk import get_typetree_node
        return get_typetree_node(ClassIDType.MonoBehaviour, reader.version)
    except Exception as exc:
        raise RecoveryBlocked(
            "Exact source version native MonoBehaviour TypeTree unavailable: " +
            type(exc).__name__, phase="native_header",
            code="NATIVE_HEADER_NOT_AVAILABLE", frame=_safe_exception_frame(exc)
        ) from exc


def _valid_field(name, value):
    """Conservative field validation; never coerce an invalid layout into UI."""
    enums = {
        "m_Type": 3, "m_UiScaleMode": 2, "m_ScreenMatchMode": 2,
        "m_FillMethod": 4, "m_StartCorner": 3, "m_StartAxis": 1,
        "m_ChildAlignment": 8, "m_Constraint": 2, "m_AspectMode": 5,
    }
    if name in enums:
        return type(value) is int and 0 <= value <= enums[name]
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


def verified_fields(reader, row, generator, *,
                    use_unitypy_native_header=False, native_root=None):
    """Decode one *already source-identified* MonoBehaviour, all-or-nothing."""
    if (row.get("kind") != "MonoBehaviour" or
            row.get("className") not in ui.FIELDS or
            not row.get("assembly") or
            row.get("scriptResolution") not in
            ("LOCAL_PATHID", "EXTERNAL_RESOLVED", "UNITYPY_DEREF",
             "GLOBAL_XAPK_EXACT_FILE_ALIAS_PATHID")):
        raise RecoveryBlocked("Missing exact MonoBehaviour/MonoScript provenance",
                              phase="source_identity", code="SOURCE_IDENTITY_MISSING")
    raw = reader.get_raw_data()
    if not raw or len(raw) > MAX_OBJECT:
        raise RecoveryBlocked("Serialized object is absent or oversized",
                              phase="source_identity", code="OBJECT_BYTES_INVALID")
    try:
        nodes = generator.get_nodes_up(row["assembly"], row["className"])
    except Exception as exc:
        raise RecoveryBlocked(
            "Generated TypeTree node generation failed: " + type(exc).__name__,
            phase="generate_nodes", code="NODE_GENERATION_" + type(exc).__name__,
            frame=_safe_exception_frame(exc),
        ) from exc
    if nodes is None:
        raise RecoveryBlocked(
            "Generated TypeTree is unavailable",
            phase="generate_nodes", code="NO_GENERATED_NODES",
        )
    shape = _tree_summary(nodes)
    # The known Unity MonoBehaviour native header must be represented as part
    # of a complete generated root. An incomplete root is not a serialized layout.
    # TypeTreeGeneratorAPI may legitimately replace the root's m_Type with
    # the derived script type ("Image", "CanvasScaler", etc.) while retaining
    # the genuine native MonoBehaviour header. Never require the literal name.
    allowed_roots = {"MonoBehaviour", row["className"],
                     row["className"].rsplit(".", 1)[-1]}
    if (shape["rootType"] not in allowed_roots or
            shape["rootLevel"] != 0 or shape["childCount"] is None or
            set(shape["headerNodes"]) !=
            {"m_GameObject", "m_Script", "m_Enabled"}):
        raise RecoveryBlocked(
            "Generated TypeTree root lacks Unity MonoBehaviour source header",
            phase="validate_root", code="INCOMPLETE_GENERATED_ROOT",
            tree=shape,
        )
    if use_unitypy_native_header:
        native = native_root if native_root is not None else exact_source_unity_header(reader)
        nodes = verified_native_header_root(nodes, native)
    try:
        # CRITICAL: check_read=True; NEVER treat partial parse as verified.
        decoded = reader.read_typetree(nodes=nodes, check_read=True)
    except Exception as exc:
        raise RecoveryBlocked(
            "Generated TypeTree strict object parse failed: " + type(exc).__name__,
            phase="strict_parse", code="STRICT_PARSE_" + type(exc).__name__,
            frame=_safe_exception_frame(exc), tree=shape,
        ) from exc
    if not isinstance(decoded, dict):
        raise RecoveryBlocked("Generated data is not an object",
                              phase="source_compare", code="DECODED_NOT_OBJECT")
    if refs.pptr(decoded.get("m_GameObject")) != (0, row["gameObjectId"]):
        raise RecoveryBlocked("Decoded GameObject owner does not match source",
                              phase="source_compare", code="GAMEOBJECT_POINTER_MISMATCH")
    expected = row.get("scriptPointer", {})
    if refs.pptr(decoded.get("m_Script")) != (
        expected.get("fileId"), expected.get("pathId")
    ):
        raise RecoveryBlocked("Decoded MonoScript pointer does not match source",
                              phase="source_compare", code="MONOSCRIPT_POINTER_MISMATCH")
    if ("nativeEnabled" in row and
            decoded.get("m_Enabled") != row["nativeEnabled"]):
        raise RecoveryBlocked("Decoded enabled flag contradicts source header",
                              phase="source_compare", code="ENABLED_VALUE_MISMATCH")
    fields = {}
    for name in ui.FIELDS[row["className"]]:
        if name not in decoded:
            continue
        value = ui.plain(decoded[name])
        if value is None or not _valid_field(name, value):
            raise RecoveryBlocked("Invalid recovered UI field: " + name,
                                  phase="field_validation", code="FIELD_VALUE_INVALID")
        fields[name] = value
    if not fields:
        raise RecoveryBlocked("No target managed fields in generated TypeTree",
                              phase="field_validation", code="NO_TARGET_FIELDS")
    return fields, {
        "method": ("SOURCE_IL2CPP_TREE_WITH_EXACT_UNITY_NATIVE_HEADER"
                   if use_unitypy_native_header else
                   "SOURCE_IL2CPP_GENERATED_TYPETREE"),
        "rawObjectSha256": hashlib.sha256(raw).hexdigest(),
        "rawObjectBytes": len(raw),
        "exactSourcePointerChecked": True,
        "strictObjectSizeChecked": True,
        "nativeHeaderMethod": ("UNITYPY_EXACT_SOURCE_UNITY_VERSION"
                               if use_unitypy_native_header else
                               "GENERATOR_NATIVE_HEADER"),
        "sourceFields": sorted(fields),
    }
