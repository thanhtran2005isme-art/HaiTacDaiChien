#!/usr/bin/env python3
"""Export locally owned Unity Sprite pixels for the offline UI viewer.

Requires real XAPK/APKs and UnityPy + Pillow. Output stays under output/ and
is never committed. No arbitrary assets or guessed character skins.
"""
from __future__ import annotations

import argparse
import collections
import csv
import hashlib
import io
import json
import re
import shutil
import tempfile
import zipfile
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
MAX_PNG = 20 * 1024 * 1024
MAX_TOTAL = 250 * 1024 * 1024
MAX_PIXELS = 25_000_000
FILE_RE = re.compile(r"^[0-9a-f]{32}\.png$")


def read_rows(path):
    with path.open(encoding="utf-8", newline="") as stream:
        return list(csv.DictReader(stream))


def key_of(serialized_file, sprite_id):
    return (str(serialized_file), str(sprite_id))


def image_name(key):
    return hashlib.sha256((key[0] + ":" + key[1]).encode("utf-8")).hexdigest()[:32] + ".png"


def make_manifest(links, scenes, exported):
    """Only unambiguous Image→Sprite paths are mapped; never guess by name."""
    by_scene = {scene["id"]: scene for scene in scenes["scenes"]}
    node_paths = {
        ref: collections.Counter(node["path"] for node in scene["nodes"])
        for ref, scene in by_scene.items()
    }
    candidates = collections.defaultdict(set)
    for row in links:
        ref, path = row["reference"], row["ui_path"]
        if ref not in by_scene or node_paths[ref][path] != 1:
            continue
        candidates[(ref, path)].add(key_of(row["sprite_file"], row["sprite_id"]))
    mapped = {ref: {} for ref in by_scene}
    ambiguous = 0
    for (ref, path), keys in candidates.items():
        if len(keys) != 1:
            ambiguous += 1
            continue
        key = next(iter(keys))
        if key in exported:
            mapped[ref][path] = exported[key]
    used = {item for nodes in mapped.values() for item in nodes.values()}
    return {
        "version": 1,
        "files": sorted(used),
        "scenes": mapped,
        "stats": {
            "sprite_images_exported": len(exported),
            "ui_nodes_mapped": sum(len(nodes) for nodes in mapped.values()),
            "ambiguous_paths": ambiguous,
            "unmatched_sprite_images": len(exported) - len(set(exported.values()) & used),
        },
    }


def export_bundle(path, bundle_label, expected, output, exported, issues, unitypy):
    try:
        env = unitypy.load(str(path))
    except Exception as exc:
        issues.append("Cannot read Unity bundle: " + str(exc)[:160])
        return
    for obj in env.objects:
        if getattr(getattr(obj, "type", None), "name", "") != "Sprite":
            continue
        asset_file = getattr(obj, "assets_file", None)
        source_name = str(getattr(asset_file, "name", "")).replace("\\", "/").split("/")[-1]
        if not source_name:
            continue
        key = key_of(bundle_label + "__" + source_name, obj.path_id)
        if key not in expected or key in exported:
            continue
        try:
            image = obj.read().image  # UnityPy handles Sprite rectangle / atlas extraction.
            if image is None or image.width * image.height > MAX_PIXELS:
                raise ValueError("Sprite image unavailable or exceeds pixel limit")
            stream = io.BytesIO()
            image.save(stream, format="PNG")
            contents = stream.getvalue()
            used = sum((output / name).stat().st_size for name in exported.values())
            if not contents or len(contents) > MAX_PNG or used + len(contents) > MAX_TOTAL:
                raise ValueError("PNG export exceeds file or total size limit")
            name = image_name(key)
            (output / name).write_bytes(contents)
            exported[key] = name
        except Exception as exc:
            issues.append("Cannot decode matched Sprite: " + str(exc)[:160])


def scan_apk(apk, apk_label, expected, output, exported, issues, unitypy, work):
    try:
        with zipfile.ZipFile(apk) as z:
            for index, item in enumerate(z.infolist()):
                if item.is_dir() or not item.filename.lower().endswith(
                    (".unity3d", ".bundle", ".assetbundle")
                ):
                    continue
                label = apk_label + "_" + Path(item.filename).stem
                if not any(key[0].startswith(label + "__") for key in expected):
                    continue
                if item.file_size > 1024 * 1024 * 1024:
                    issues.append("Oversized bundle skipped")
                    continue
                bundle = work / ("bundle_" + str(index) + ".unity3d")
                with z.open(item) as src, bundle.open("wb") as dst:
                    shutil.copyfileobj(src, dst)
                try:
                    export_bundle(bundle, label, expected, output, exported, issues, unitypy)
                finally:
                    bundle.unlink(missing_ok=True)
    except (OSError, zipfile.BadZipFile) as exc:
        issues.append("Cannot read APK: " + str(exc)[:160])


def extract(root, xapk=None, apk_dir=None, out=None, unitypy=None):
    report = root / "reports/xapk"
    links = read_rows(report / "scene-image-texture-links.csv")
    scenes = json.loads((root / "unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json").read_text(
        encoding="utf-8"
    ))
    expected = {key_of(row["sprite_file"], row["sprite_id"]) for row in links}
    if unitypy is None:
        try:
            import UnityPy as unitypy
        except ImportError as exc:
            raise RuntimeError("Install dependencies: py -m pip install UnityPy Pillow") from exc
    output = out or (root / "output/local-ui-art")
    output.mkdir(parents=True, exist_ok=True)
    exported, issues = {}, []
    apk_dir = apk_dir or (root / "output/apks")
    with tempfile.TemporaryDirectory(prefix="hai_ui_art_") as tmp:
        work = Path(tmp)
        if apk_dir.is_dir() and list(apk_dir.glob("*.apk")):
            for apk in sorted(apk_dir.glob("*.apk")):
                scan_apk(apk, apk.stem, expected, output, exported, issues, unitypy, work)
        else:
            if xapk is None:
                xapk = next(iter(root.glob("*.xapk")), None)
            if not xapk or not xapk.is_file():
                raise RuntimeError("No real XAPK/APKs found. Run git lfs pull first.")
            if xapk.read_bytes()[:48].startswith(b"version https://git-lfs.github.com/"):
                raise RuntimeError("XAPK is an LFS pointer. Run git lfs pull first.")
            if not zipfile.is_zipfile(xapk):
                raise RuntimeError("XAPK is not a valid ZIP archive.")
            with zipfile.ZipFile(xapk) as outer:
                apks = [i for i in outer.infolist() if i.filename.lower().endswith(".apk")]
                for i, member in enumerate(apks):
                    label = str(i).zfill(3) + "_" + Path(member.filename).stem
                    if not any(key[0].startswith(label + "_") for key in expected):
                        continue
                    apk = work / (label + ".apk")
                    with outer.open(member) as src, apk.open("wb") as dst:
                        shutil.copyfileobj(src, dst)
                    try:
                        scan_apk(apk, label, expected, output, exported, issues, unitypy, work)
                    finally:
                        apk.unlink(missing_ok=True)
    manifest = make_manifest(links, scenes, exported)
    # Remove orphaned exported images on rebuild; only manifest-allowed art is served.
    for old in output.glob("*.png"):
        if FILE_RE.fullmatch(old.name) and old.name not in set(manifest["files"]):
            old.unlink()
    (output / "manifest.json").write_text(
        json.dumps(manifest, ensure_ascii=False, indent=2) + "\n", encoding="utf-8"
    )
    result = {"status": "PASS" if manifest["files"] else "NO_DECODED_ART",
              **manifest["stats"], "issues": issues[:20],
              "manifest": str(output / "manifest.json")}
    return result


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--repo-root", type=Path, default=ROOT)
    parser.add_argument("--xapk", type=Path, default=None)
    parser.add_argument("--apk-dir", type=Path, default=None)
    args = parser.parse_args()
    try:
        result = extract(args.repo_root.resolve(), args.xapk, args.apk_dir)
    except (OSError, ValueError, RuntimeError, zipfile.BadZipFile) as exc:
        parser.exit(1, "BLOCKED: " + str(exc) + "\n")
    print(json.dumps(result, ensure_ascii=False, indent=2))


if __name__ == "__main__":
    main()
