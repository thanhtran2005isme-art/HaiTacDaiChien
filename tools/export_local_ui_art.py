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


def component_node_lookup(root, scenes):
    """Resolve image component IDs to candidate node IDs through ORIGINAL GO IDs.

    This is a source metadata join; duplicated Unity names/paths are allowed,
    but ambiguous GameObject IDs or component owners are not.
    """
    hierarchy = read_rows(root / "reports/xapk/ui-hierarchy.csv")
    components = read_rows(root / "reports/xapk/ui-components.csv")
    file_to_refs = collections.defaultdict(list)
    by_scene = {}
    for scene in scenes["scenes"]:
        file_to_refs[scene["source"]].append(scene["id"])
        by_scene[scene["id"]] = {
            int(node["id"]): node for node in scene["nodes"]
        }
    owners = {}
    for row in components:
        for ref in file_to_refs.get(row["bundle"], []):
            key = (ref, str(row["component_id"]))
            owner = int(row["gameobject_id"])
            if key in owners and owners[key] != owner:
                raise ValueError("Conflicting original component owner " + str(key))
            owners[key] = owner
    by_go = {}
    for row in hierarchy:
        for ref in file_to_refs.get(row["bundle"], []):
            node_id = int(row["transform_id"])
            if node_id not in by_scene[ref]:
                continue
            key = (ref, int(row["gameobject_id"]))
            if key in by_go and by_go[key] != node_id:
                raise ValueError("Ambiguous GameObject transform " + str(key))
            by_go[key] = node_id
    result = {}
    for key, owner in owners.items():
        node_id = by_go.get((key[0], owner))
        if node_id is not None:
            result[key] = node_id
    return result


def make_manifest(links, scenes, exported, component_nodes=None):
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
    exact = collections.defaultdict(set)
    if component_nodes is not None:
        for row in links:
            ref = row["reference"]
            node_id = component_nodes.get((ref, str(row["component_id"])))
            if node_id is None or ref not in by_scene:
                continue
            node = next((n for n in by_scene[ref]["nodes"]
                         if int(n["id"]) == node_id), None)
            # Component owner must also have the original expected UI path.
            if node is None or node["path"] != row["ui_path"]:
                continue
            exact[(ref, node_id)].add((
                key_of(row["sprite_file"], row["sprite_id"]),
                str(row["component_id"])
            ))
    for (ref, path), keys in candidates.items():
        if len(keys) != 1:
            ambiguous += 1
            continue
        key = next(iter(keys))
        if key in exported:
            mapped[ref][path] = exported[key]
    bindings = []
    ambiguous_components = 0
    for (ref, node_id), possibilities in sorted(exact.items()):
        if len(possibilities) != 1:
            ambiguous_components += 1
            continue
        source_key, component_id = next(iter(possibilities))
        name = exported.get(source_key)
        if name is None:
            continue
        bindings.append({
            "sceneId": ref, "nodeId": node_id, "imageComponentId": int(component_id),
            "spriteFile": name,
            # Additional original, PPtr-linked source identity is local-only;
            # it permits native Sprite border/PPU decoding for duplicated
            # UI paths without using the ambiguous displayed GameObject name.
            "spriteSerializedFile": source_key[0],
            "spritePathId": int(source_key[1]),
        })
    used = {item for nodes in mapped.values() for item in nodes.values()}
    used.update(b["spriteFile"] for b in bindings)
    exact_counts = collections.Counter(b["sceneId"] for b in bindings)
    return {
        "version": 1,  # Backward-compatible Web manifest; new nodeBindings are additive.
        "files": sorted(used),
        "scenes": mapped,
        "nodeBindings": bindings,
        "stats": {
            "sprite_images_exported": len(exported),
            "ui_nodes_mapped": len(bindings) if component_nodes is not None
                else sum(len(nodes) for nodes in mapped.values()),
            "scene_nodes_mapped": {ref: exact_counts[ref] if component_nodes is not None
                                   else len(nodes) for ref, nodes in mapped.items()},
            "ambiguous_paths": ambiguous,
            "ambiguous_component_bindings": ambiguous_components,
            "exact_node_bindings": len(bindings),
            "unmatched_sprite_images": len(exported) - len(set(exported.values()) & used),
        },
    }


def resolve_bundle_label(apk_label, inner_path, expected):
    """Match existing report bundle IDs even when outer XAPK APK order changes."""
    direct = apk_label + "_" + Path(inner_path).stem
    prefixes = {key[0].split("__", 1)[0] for key in expected}
    if direct in prefixes:
        return direct
    plain_apk = re.sub(r"^\d+_", "", apk_label)
    suffix = "_" + plain_apk + "_" + Path(inner_path).stem
    matches = sorted(prefix for prefix in prefixes if prefix.endswith(suffix))
    return matches[0] if len(matches) == 1 else None


def export_bundle(bundle_bytes, bundle_label, expected, output, exported, issues, unitypy):
    """Load from memory: UnityPy cannot hold an open temporary file on Windows."""
    try:
        env = unitypy.load(bundle_bytes)
    except Exception as exc:
        issues.append("Cannot read Unity bundle: " + str(exc)[:160])
        return
    sprite_objects, matched, sample = 0, 0, []
    ids_in_bundle = collections.defaultdict(set)
    for sprite_key in expected:
        if sprite_key[0].startswith(bundle_label + "__"):
            ids_in_bundle[sprite_key[1]].add(sprite_key)
    for obj in env.objects:
        if getattr(getattr(obj, "type", None), "name", "") != "Sprite":
            continue
        sprite_objects += 1
        asset_file = getattr(obj, "assets_file", None)
        source_name = str(getattr(asset_file, "name", "")).replace("\\", "/").split("/")[-1]
        if not source_name:
            continue
        if len(sample) < 3:
            sample.append(source_name)
        key = key_of(bundle_label + "__" + source_name, obj.path_id)
        if key not in expected:
            # UnityPy may expose the actual serialized file as "resources.assets",
            # while the static inventory calls it "file110". Never trust a
            # filename-only or name-only match: require unique path ID and name.
            possible = ids_in_bundle.get(str(obj.path_id), set())
            if len(possible) != 1:
                continue
            key = next(iter(possible))
            alias = True
        else:
            alias = False
        if key in exported:
            continue
        try:
            sprite = obj.read()
            real_name = str(getattr(sprite, "m_Name", "") or "")
            names = expected[key]
            if alias and (not names or real_name not in names):
                continue
            matched += 1
            image = sprite.image  # UnityPy handles Sprite rectangle / atlas extraction.
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
    if not matched:
        issues.append("No Sprite ID match in " + bundle_label +
                      " (objects=" + str(sprite_objects) + ", serialized=" +
                      ",".join(sample) + ")")


def scan_apk(apk, apk_label, expected, output, exported, issues, unitypy, work):
    try:
        with zipfile.ZipFile(apk) as z:
            for index, item in enumerate(z.infolist()):
                if item.is_dir() or not item.filename.lower().endswith(
                    (".unity3d", ".bundle", ".assetbundle")
                ):
                    continue
                label = resolve_bundle_label(apk_label, item.filename, expected)
                if label is None:
                    issues.append("No matching metadata bundle for " + apk_label +
                                  "/" + Path(item.filename).name)
                    continue
                if item.file_size > 1024 * 1024 * 1024:
                    issues.append("Oversized bundle skipped")
                    continue
                # Reading an archive member as bytes avoids UnityPy retaining a
                # memory-mapped Windows handle when the temporary file is removed.
                # Files are bounded above and no bundle is written to disk.
                with z.open(item) as src:
                    bundle_bytes = src.read()
                export_bundle(bundle_bytes, label, expected, output, exported, issues, unitypy)
                del bundle_bytes
    except (OSError, zipfile.BadZipFile) as exc:
        issues.append("Cannot read APK: " + str(exc)[:160])


def extract(root, xapk=None, apk_dir=None, out=None, unitypy=None):
    report = root / "reports/xapk"
    links = read_rows(report / "scene-image-texture-links.csv")
    scenes = json.loads((root / "unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json").read_text(
        encoding="utf-8"
    ))
    expected = collections.defaultdict(set)
    for row in links:
        expected[key_of(row["sprite_file"], row["sprite_id"])].add(row["sprite_name"])
    if unitypy is None:
        try:
            import UnityPy as unitypy
        except ImportError as exc:
            raise RuntimeError("Install dependencies: py -m pip install UnityPy Pillow") from exc
    output = out or (root / "output/local-ui-art")
    output.mkdir(parents=True, exist_ok=True)
    exported, issues = {}, []
    apk_dir = apk_dir or (root / "output/apks")
    if xapk is None:
        xapk = next(iter(sorted(root.glob("*.xapk"))), None)
    source = "xapk"
    # Prefer the authoritative XAPK over previously unpacked, potentially stale APKs.
    # --apk-dir remains a supported explicit input when an XAPK is unavailable.
    has_xapk = xapk is not None and xapk.is_file()
    if has_xapk:
        with xapk.open("rb") as stream:
            is_pointer = stream.read(64).startswith(b"version https://git-lfs.github.com/")
        if is_pointer:
            raise RuntimeError("XAPK is only an LFS pointer. Run git lfs pull first.")
        if not zipfile.is_zipfile(xapk):
            raise RuntimeError("XAPK is invalid. Download a complete XAPK.")
    elif not apk_dir.is_dir() or not list(apk_dir.glob("*.apk")):
        raise RuntimeError("No XAPK or unpacked APKs found. Run git lfs pull first.")
    with tempfile.TemporaryDirectory(prefix="hai_ui_art_") as tmp:
        work = Path(tmp)
        if not has_xapk:
            source = "apks"
            for apk in sorted(apk_dir.glob("*.apk")):
                scan_apk(apk, apk.stem, expected, output, exported, issues, unitypy, work)
        else:
            with zipfile.ZipFile(xapk) as outer:
                apks = [i for i in outer.infolist() if i.filename.lower().endswith(".apk")]
                for i, member in enumerate(apks):
                    label = str(i).zfill(3) + "_" + Path(member.filename).stem
                    plain_stem = Path(member.filename).stem
                    if not any("_" + plain_stem + "_" in key[0] for key in expected):
                        continue
                    apk = work / (label + ".apk")
                    with outer.open(member) as src, apk.open("wb") as dst:
                        shutil.copyfileobj(src, dst)
                    try:
                        scan_apk(apk, label, expected, output, exported, issues, unitypy, work)
                    finally:
                        apk.unlink(missing_ok=True)
    # UI paths may be identical for siblings; component and GameObject IDs
    # distinguish them. The local layout exporter cross-checks the same owner.
    component_nodes = component_node_lookup(root, scenes)
    manifest = make_manifest(links, scenes, exported, component_nodes)
    # Remove orphaned exported images on rebuild; only manifest-allowed art is served.
    for old in output.glob("*.png"):
        if FILE_RE.fullmatch(old.name) and old.name not in set(manifest["files"]):
            old.unlink()
    manifest_path = output / "manifest.json"
    if manifest["files"]:
        manifest_path.write_text(
            json.dumps(manifest, ensure_ascii=False, indent=2) + "\n", encoding="utf-8"
        )
    else:
        manifest_path.unlink(missing_ok=True)
    result = {"status": "PASS" if manifest["files"] else "NO_DECODED_ART",
              **manifest["stats"], "source": source, "issues": issues[:20],
              "manifest": str(output / "manifest.json")}
    return result


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--repo-root", type=Path, default=ROOT)
    parser.add_argument("--xapk", type=Path, default=None)
    parser.add_argument("--apk-dir", type=Path, default=None)
    args = parser.parse_args()
    output = args.repo_root.resolve() / "output/local-ui-art"
    output.mkdir(parents=True, exist_ok=True)
    try:
        result = extract(args.repo_root.resolve(), args.xapk, args.apk_dir)
    except (OSError, ValueError, RuntimeError, zipfile.BadZipFile) as exc:
        result = {"status": "BLOCKED", "reason": str(exc)[:300]}
    status_path = output / "export-status.json"
    status_path.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n",
                           encoding="utf-8")
    print(json.dumps(result, ensure_ascii=False, indent=2))
    if result["status"] != "PASS":
        raise SystemExit(1)


if __name__ == "__main__":
    main()
