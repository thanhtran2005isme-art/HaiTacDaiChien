#!/usr/bin/env python3
"""Build a SMALL, metadata-only offline Unity viewer dataset.

Reads prior analysis CSVs, never copies textures, APKs, sprites, screenshots,
game code, private accounts or binaries. Scene names are candidates, not
verified original runtime scenes. Works with Python standard library.
"""
from __future__ import annotations

import argparse
import collections
import csv
import json
from pathlib import Path

REFERENCE_TITLES = {
    "REF01-ship-upgrade": "01 · Nâng cấp tàu",
    "REF02-hero-detail": "02 · Chi tiết nhân vật",
    "REF03-islands-map-A": "03A · Bản đồ đảo (ứng viên A)",
    "REF03-islands-map-B": "03B · Bản đồ đảo (ứng viên B)",
    "REF04-home-crew": "04 · Trang chủ đội hình",
}
MAX_NODES_PER_SCENE = 2500


def table(path):
    with path.open(encoding="utf-8", newline="") as f:
        return list(csv.DictReader(f))


def vec2(value, default=(0., 0.)):
    if not value:
        return list(default)
    try:
        x, y = [float(t) for t in value.split(",")]
        if abs(x) > 1000000 or abs(y) > 1000000:
            return list(default)
        return [x, y]
    except (ValueError, TypeError):
        return list(default)


def descendants(root_id, objects):
    children = collections.defaultdict(list)
    for node_id, node in objects.items():
        try:
            parent = int(node["parent_transform_id"])
        except (TypeError, ValueError):
            parent = 0
        if parent != node_id and parent in objects:
            children[parent].append(node_id)
    for siblings in children.values():
        siblings.sort(key=lambda nid: (int(objects[nid].get("sibling_index") or -1), nid))
    output, visited, stack = [], set(), [root_id]
    while stack:
        nid = stack.pop()
        if nid in visited:
            continue
        visited.add(nid)
        if nid not in objects:
            continue
        output.append(nid)
        if len(output) > MAX_NODES_PER_SCENE:
            raise ValueError("Too many UI nodes in one scene; inspect first")
        stack.extend(reversed(children.get(nid, [])))
    return output


def build(root):
    report = root / "reports/xapk"
    references = table(report / "ui-screenshot-reference-previews.csv")
    if len(references) != 5 or set(x["reference"] for x in references) != set(REFERENCE_TITLES):
        raise ValueError("Expected exactly five individually verified metadata reference rows")

    wanted = {x["bundle"] for x in references}
    hierarchy = collections.defaultdict(dict)
    for row in table(report / "ui-hierarchy.csv"):
        if row["bundle"] in wanted:
            hierarchy[row["bundle"]][int(row["transform_id"])] = row

    # MonoBehaviour.component ID -> owning GameObject ID. Do not join by name
    # or string path because sibling names can repeat in Unity scenes.
    component_go = {}
    for row in table(report / "ui-components.csv"):
        if row["bundle"] in wanted:
            component_go[(row["bundle"], row["component_id"])] = int(row["gameobject_id"])
    types = collections.defaultdict(set)
    for row in table(report / "resolved-ui-components.csv"):
        if row["file"] not in wanted:
            continue
        go = component_go.get((row["file"], row["path_id"]))
        if go is not None:
            types[(row["file"], go)].add(row["ui_type"])
    per_ref = collections.defaultdict(lambda: collections.defaultdict(
        lambda: {"sprites": [], "missing": 0, "spine": [], "types": set()}))
    bundles = {r["reference"]: r["bundle"] for r in references}

    for row in table(report / "scene-image-texture-links.csv"):
        ref = row["reference"]
        bundle = bundles[ref]
        go = component_go.get((bundle, row["component_id"]))
        if go is not None:
            entry = per_ref[ref][go]
            name = row["sprite_name"]
            if name and name not in entry["sprites"]:
                entry["sprites"].append(name)
    for row in table(report / "scene-unresolved-images.csv"):
        ref, bundle = row["reference"], bundles[row["reference"]]
        go = component_go.get((bundle, row["component_id"]))
        if go is not None:
            per_ref[ref][go]["missing"] += 1
    for row in table(report / "scene-spine-components.csv"):
        ref, bundle = row["reference"], bundles[row["reference"]]
        go = component_go.get((bundle, row["component_id"]))
        if go is not None and row["class"] not in per_ref[ref][go]["spine"]:
            per_ref[ref][go]["spine"].append(row["class"])

    scenes = []
    for ref in references:
        rid = ref["reference"]
        bundle = ref["bundle"]
        objects = hierarchy[bundle]
        root_id = int(ref["root_id"])
        if root_id not in objects:
            raise ValueError("Root transform unavailable: " + rid)
        order = descendants(root_id, objects)
        if len(order) < 15:
            raise ValueError("Scene too small for expected reference: " + rid)
        scene_go = per_ref[rid]
        nodes = []
        linked_count = missing_count = spine_count = 0
        for transform_id in order:
            row = objects[transform_id]
            go = int(row["gameobject_id"])
            entry = scene_go.get(go, {"sprites": [], "missing": 0, "spine": []})
            sprite = entry["sprites"]
            missing = entry["missing"]
            spine = entry["spine"]
            linked_count += len(sprite)
            missing_count += missing
            spine_count += len(spine)
            node = {
                "id": transform_id,
                "parent": int(row["parent_transform_id"] or 0),
                "name": row["name"][:110],
                "path": row["path"][:550],
                "active": row["active"].lower() == "true",
                "a0": vec2(row["anchor_min"], (.5, .5)),
                "a1": vec2(row["anchor_max"], (.5, .5)),
                "pivot": vec2(row["pivot"], (.5, .5)),
                "delta": vec2(row["size_delta"]),
                "position": vec2(row["anchored_position"]),
                "scale": vec2(row.get("scale"), (1., 1.)),
                "rotationZ": float(row.get("rotation_z") or 0),
                "order": int(row.get("sibling_index") or 0),
                "types": sorted(types.get((bundle, go), ())),
                "sprites": sprite[:6],
                "spine": spine[:6],
                "missingImages": missing,
            }
            nodes.append(node)
        scene = {
            "id": rid,
            "title": REFERENCE_TITLES[rid],
            "source": bundle,
            "rootTransform": root_id,
            "confidence": ref["confidence"],
            "expectedNodes": int(ref["tree_nodes"]),
            "linkedImageEntries": int(ref["linked_images"]),
            "unlinkedImageEntries": missing_count,
            "nodeCount": len(nodes),
            "nodes": nodes,
        }
        scenes.append(scene)

    return {
        "schemaVersion": 1,
        "source": "reports/xapk (metadata extracted from XAPK; no art)",
        "viewport": "Hypothetical 1600x900; CanvasScaler runtime unverified",
        "licenseNotice": "Reference-only geometry. No original copyrighted game art is included.",
        "scenes": scenes,
    }


def validate(data):
    if data.get("schemaVersion") != 1 or len(data["scenes"]) != 5:
        raise ValueError("Unexpected UI database")
    for scene in data["scenes"]:
        nodes = scene["nodes"]
        ids = [n["id"] for n in nodes]
        if len(set(ids)) != len(ids):
            raise ValueError("Duplicate serialized transform in " + scene["id"])
        known = set(ids)
        if scene["rootTransform"] not in known:
            raise ValueError("Missing root in " + scene["id"])
        for node in nodes:
            if node["id"] != scene["rootTransform"] and node["parent"] not in known:
                raise ValueError("Missing parent in " + scene["id"])
            if len(node["a0"]) != 2 or len(node["delta"]) != 2:
                raise ValueError("Invalid rect in " + scene["id"])
        if len(nodes) != scene["nodeCount"]:
            raise ValueError("Node count mismatch")
        if sum(n["missingImages"] for n in nodes) != scene["unlinkedImageEntries"]:
            raise ValueError("Missing Image count does not match node inventory")


def main():
    p = argparse.ArgumentParser()
    p.add_argument("--repo-root", type=Path, default=Path("."))
    p.add_argument("--output", type=Path, default=None)
    args = p.parse_args()
    root = args.repo_root.resolve()
    output = args.output or (
        root / "unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json")
    data = build(root)
    validate(data)
    output.parent.mkdir(parents=True, exist_ok=True)
    output.write_text(json.dumps(data, ensure_ascii=False, separators=(",", ":")) + "\n",
                      encoding="utf-8")
    print(json.dumps({
        "status": "PASS", "scenes": len(data["scenes"]),
        "nodes": sum(x["nodeCount"] for x in data["scenes"]),
        "output": str(output), "bytes": output.stat().st_size
    }, ensure_ascii=False))


if __name__ == "__main__":
    main()
