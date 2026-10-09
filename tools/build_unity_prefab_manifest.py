#!/usr/bin/env python3
"""Prepare evidence-based Unity prefab reconstruction metadata from local Sprite art.

No Unity binaries, XAPK, original prefabs, textures or screenshots are committed.
The output is local-only under output/ and is consumed by Unity Editor.
"""
from __future__ import annotations

import argparse
import collections
import csv
import json
import re
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
ART_FILE = re.compile(r"[0-9a-f]{32}\.png\Z")
SCENE_ID = re.compile(r"REF[0-9A-Za-z-]+\Z")


def read_csv(path):
    with path.open(newline="", encoding="utf-8") as stream:
        return list(csv.DictReader(stream))


def prepare(scene_data, art, components, candidates):
    if scene_data.get("schemaVersion") != 1 or art.get("version") not in (1, 2):
        raise ValueError("Incompatible scene metadata or Sprite manifest")
    available = set(art.get("files") or [])
    if not available or len(available) > 2000 or any(
        not isinstance(file, str) or not ART_FILE.fullmatch(file) for file in available
    ):
        raise ValueError("Sprite manifest has no valid allowlisted images")
    scene_maps = art.get("scenes")
    if not isinstance(scene_maps, dict):
        raise ValueError("Sprite manifest scene mapping missing")
    scenes = scene_data["scenes"]
    result_art, result_spine, scene_counts = [], [], {}
    component_candidates = collections.defaultdict(list)
    for row in candidates:
        key = (row.get("reference"), row.get("ui_path"), row.get("source_component_id"))
        if (row.get("relation") == "direct_typed_pointer_candidate"
                and row.get("target_class") == "Spine.Unity.SkeletonDataAsset"):
            component_candidates[key].append(row)
    for scene in scenes:
        scene_id = scene["id"]
        if not SCENE_ID.fullmatch(scene_id):
            raise ValueError("Invalid scene identifier")
        nodes = scene["nodes"]
        if not nodes or nodes[0]["id"] != scene["rootTransform"]:
            raise ValueError("Root transform is not first: " + scene_id)
        by_id = {int(node["id"]): node for node in nodes}
        if len(by_id) != len(nodes):
            raise ValueError("Duplicate transform id: " + scene_id)
        ordered = set()
        paths = collections.Counter(n["path"] for n in nodes)
        for node in nodes:
            ident = int(node["id"])
            if ident != scene["rootTransform"] and int(node["parent"]) not in ordered:
                raise ValueError("Parent must precede child: " + scene_id)
            ordered.add(ident)
        if isinstance(art.get("nodeBindings"), list) and art["nodeBindings"]:
            if not isinstance(art.get("nodeBindings"), list):
                raise ValueError("Exact Image ID bindings missing from Sprite manifest")
            for entry in art["nodeBindings"]:
                if entry["sceneId"] != scene_id:
                    continue
                node_id = int(entry["nodeId"])
                component_id = int(entry["imageComponentId"])
                name = entry["spriteFile"]
                if node_id not in by_id or component_id <= 0 or name not in available:
                    raise ValueError("Invalid exact Image component binding: " + scene_id)
                result_art.append({
                    "sceneId": scene_id, "nodeId": node_id,
                    "spriteFile": name, "sourceImageComponentId": component_id,
                })
        else:
            # Legacy local manifests had only UI paths. Do not mistake them for
            # enough evidence when duplicate sibling names exist.
            for path, name in (scene_maps.get(scene_id) or {}).items():
                if paths[path] != 1 or not isinstance(name, str) or name not in available:
                    continue
                node = next(n for n in nodes if n["path"] == path)
                result_art.append({
                    "sceneId": scene_id, "nodeId": int(node["id"]),
                    "spriteFile": name,
                })
        scene_counts[scene_id] = sum(a["sceneId"] == scene_id for a in result_art)
        for row in components:
            if row.get("reference") != scene_id:
                continue
            path = row.get("ui_path", "")
            if paths[path] != 1:
                continue
            node = next(n for n in nodes if n["path"] == path)
            key = (scene_id, path, row.get("component_id"))
            choices = component_candidates.get(key, [])
            # Candidate addresses are NOT verified field names. Never auto-bind.
            result_spine.append({
                "sceneId": scene_id, "nodeId": int(node["id"]),
                "componentId": str(row["component_id"]),
                "componentClass": str(row.get("class", ""))[:160],
                "bindingStatus": "pointer_candidates_unverified" if choices else "unresolved",
                "candidateCount": len(choices),
            })
    result_art.sort(key=lambda x: (x["sceneId"], x["nodeId"]))
    if len({(x["sceneId"], x["nodeId"]) for x in result_art}) != len(result_art):
        raise ValueError("Multiple Image components for one GameObject; no source guess")
    result_spine.sort(key=lambda x: (x["sceneId"], x["nodeId"], x["componentId"]))
    return {
        "schemaVersion": 2 if art.get("nodeBindings") else 1,
        "spriteFileNames": sorted({x["spriteFile"] for x in result_art}),
        "sprites": result_art,
        "spine": result_spine,
        "scenes": [{
            "sceneId": scene["id"],
            "nodeCount": len(scene["nodes"]),
            "mappedSprites": scene_counts[scene["id"]],
        } for scene in scenes],
        "disclaimer": (
            "Reconstructed Canvas hierarchy from serialized metadata, not original "
            "Unity scenes or prefab source. Spine pointers, default skins, tracks, "
            "runtime CanvasScaler, LayoutGroups and serialized custom UI behaviors "
            "are NOT verified."
        ),
    }


def build(root, output=None):
    scenes = json.loads((root / "unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json").read_text(
        encoding="utf-8"))
    art = json.loads((root / "output/local-ui-art/manifest.json").read_text(encoding="utf-8"))
    folder = root / "reports/xapk"
    plan = prepare(
        scenes, art, read_csv(folder / "scene-spine-components.csv"),
        read_csv(folder / "scene-spine-asset-candidates.csv"))
    export = output or root / "output/unity-prefab-map.json"
    export.parent.mkdir(parents=True, exist_ok=True)
    tmp = export.with_suffix(".tmp")
    tmp.write_text(json.dumps(plan, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    tmp.replace(export)
    return {"status": "PASS", "scenes": len(plan["scenes"]),
            "sprites": len(plan["sprites"]), "spine_nodes": len(plan["spine"]),
            "output": str(export)}


def main():
    p = argparse.ArgumentParser(description=__doc__)
    p.add_argument("--repo-root", type=Path, default=ROOT)
    p.add_argument("--output", type=Path)
    args = p.parse_args()
    try:
        result = build(args.repo_root.resolve(), args.output)
    except (OSError, ValueError, KeyError, TypeError) as exc:
        p.exit(1, "BLOCKED: " + str(exc) + "\n")
    print(json.dumps(result, ensure_ascii=False))


if __name__ == "__main__":
    main()
