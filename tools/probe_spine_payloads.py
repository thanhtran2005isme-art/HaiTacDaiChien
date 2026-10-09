#!/usr/bin/env python3
"""Inventory Spine source payloads from a locally accessible, authorized XAPK.

Report only file kinds, sizes and counts. No skeleton, art or attachment bytes
are written to the repository, printed in CI or uploaded as artifacts.
"""
from __future__ import annotations

import argparse
import collections
import io
import json
import re
import shutil
import tempfile
import zipfile
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
MAX_TEXT_PREVIEW = 4096
MAX_BUNDLE = 1024 * 1024 * 1024
MAX_ARCHIVE = 1024 * 1024 * 1024


def classify_payload(name, raw):
    """Return evidence-based classification, never assume proprietary binary is Spine."""
    lower = name.lower()
    if isinstance(raw, str):
        raw = raw.encode("utf-8", errors="replace")
    if not isinstance(raw, bytes):
        return "unreadable"
    sniff = raw[:MAX_TEXT_PREVIEW].lstrip(b"\xef\xbb\xbf\x00 \t\r\n")
    if sniff[:1] == b"{" and len(raw) <= 16 * 1024 * 1024:
        try:
            parsed = json.loads(raw.decode("utf-8-sig"))
            if isinstance(parsed, dict) and isinstance(parsed.get("bones"), list) and (
                "animations" in parsed or "slots" in parsed
            ):
                return "spine_json_verified"
        except (ValueError, UnicodeDecodeError):
            pass
    sample = sniff.decode("utf-8", errors="ignore")
    if "filter:" in sample and "size:" in sample and (
        ".png" in sample.lower() or ".webp" in sample.lower()
    ):
        return "atlas_text_candidate"
    if lower.endswith((".skel", ".skel.bytes")):
        return "skeleton_binary_name_only"
    if lower.endswith((".atlas", ".atlas.txt", ".atlas.bytes")):
        return "atlas_name_only"
    if "spine" in lower or "skeleton" in lower:
        return "spine_name_only"
    return "other"


def payload_bytes(data):
    content = getattr(data, "m_Script", None)
    if isinstance(content, bytes):
        return content
    if isinstance(content, str):
        return content.encode("utf-8")
    if isinstance(content, bytearray):
        return bytes(content)
    return None


def inspect_bundle(bundle_bytes, unitypy, counts, examples):
    try:
        env = unitypy.load(bundle_bytes)
    except Exception as exc:
        counts["bundle_unreadable"] += 1
        return
    for obj in env.objects:
        kind_name = getattr(getattr(obj, "type", None), "name", "")
        if kind_name == "Texture2D":
            try:
                name = str(getattr(obj.read(), "m_Name", "") or "")
                counts["textures_total"] += 1
                if name:
                    examples["_texture_names"].append(name)
            except Exception:
                counts["texture_read_error"] += 1
            continue
        if kind_name != "TextAsset":
            continue
        counts["textasset_total"] += 1
        try:
            data = obj.read()
            name = str(getattr(data, "m_Name", "") or "")
            raw = payload_bytes(data)
            if raw is None:
                counts["textasset_payload_unreadable"] += 1
                continue
            kind = classify_payload(name, raw)
            counts[kind] += 1
            if kind == "spine_json_verified":
                try:
                    skeleton = json.loads(raw.decode("utf-8-sig"))
                    version = str(skeleton.get("skeleton", {}).get("spine", "unknown"))[:16]
                    counts["version_" + version] += 1
                except (ValueError, UnicodeDecodeError, AttributeError):
                    counts["version_unreadable"] += 1
                examples["_skeleton_names"].append(name)
            if kind == "atlas_text_candidate":
                examples["_atlas_names"].append(name.removesuffix(".atlas"))
                atlas = raw.decode("utf-8", errors="replace")
                pages = [line.strip() for line in atlas.splitlines()
                         if line.strip().lower().endswith((".png", ".webp"))]
                examples["_atlas_pages"].extend(pages[:8])
            if kind != "other" and len(examples[kind]) < 5:
                examples[kind].append({
                    "name": name[:90],
                    "bytes": len(raw),
                    "path_id": str(obj.path_id),
                    "serialized_file": str(getattr(getattr(obj, "assets_file", None), "name", ""))[-80:],
                })
        except Exception:
            counts["textasset_read_error"] += 1


def inspect_apk(apk, temp, unitypy, counts, examples):
    try:
        with zipfile.ZipFile(apk) as archive:
            for i, entry in enumerate(archive.infolist()):
                if entry.is_dir() or not entry.filename.lower().endswith(
                    (".unity3d", ".bundle", ".assetbundle")
                ):
                    continue
                if entry.file_size > MAX_BUNDLE:
                    counts["oversized_bundle_skipped"] += 1
                    continue
                counts["bundle_total"] += 1
                # Avoid Windows WinError 32: do not mmap temporary bundle paths.
                with archive.open(entry) as inp:
                    blob = inp.read()
                inspect_bundle(blob, unitypy, counts, examples)
                del blob
    except (OSError, zipfile.BadZipFile):
        counts["apk_unreadable"] += 1


def inspect(xapk, root=ROOT, unitypy=None):
    if unitypy is None:
        import UnityPy as unitypy
    if not xapk.is_file() or not zipfile.is_zipfile(xapk):
        raise ValueError("Full valid XAPK required (run git lfs pull)")
    counts = collections.Counter()
    examples = collections.defaultdict(list)
    with tempfile.TemporaryDirectory(prefix="hai_spine_audit_") as tmp:
        temp = Path(tmp)
        with zipfile.ZipFile(xapk) as archive:
            for i, entry in enumerate(archive.infolist()):
                if entry.is_dir() or not entry.filename.lower().endswith(".apk"):
                    continue
                if entry.file_size > MAX_ARCHIVE:
                    counts["oversized_apk_skipped"] += 1
                    continue
                apk = temp / ("apk-" + str(i) + ".apk")
                with archive.open(entry) as inp, apk.open("wb") as out:
                    shutil.copyfileobj(inp, out)
                try:
                    inspect_apk(apk, temp, unitypy, counts, examples)
                finally:
                    apk.unlink(missing_ok=True)
    skeleton_names = set(examples.pop("_skeleton_names", []))
    atlas_names = set(examples.pop("_atlas_names", []))
    tex_names = set(examples.pop("_texture_names", []))
    atlas_pages = set(examples.pop("_atlas_pages", []))
    counts["skeleton_atlas_same_name"] = len(skeleton_names & atlas_names)
    counts["atlas_pages_matching_texture_name"] = sum(
        1 for p in atlas_pages if p in tex_names or Path(p).stem in tex_names
    )
    counts["unique_atlas_page_names"] = len(atlas_pages)
    examples["pair_examples"] = sorted(skeleton_names & atlas_names)[:10]
    examples["atlas_page_examples"] = sorted(atlas_pages)[:10]
    return {"schema": 1, "counts": dict(sorted(counts.items())),
            "examples": dict(examples), "status": "inspected_no_animation_claim",
            "caveat": "TextAsset inventory alone does not prove Spine animation can be rendered."}


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--xapk", type=Path)
    parser.add_argument("--output", type=Path, default=ROOT / "output/local-spine-audit.json")
    args = parser.parse_args()
    xapk = args.xapk or next(iter(ROOT.glob("*.xapk")), None)
    if xapk is None:
        parser.exit(2, "No XAPK found: run git lfs pull\n")
    result = inspect(xapk)
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    print(json.dumps(result, ensure_ascii=False, indent=2))


if __name__ == "__main__":
    main()
