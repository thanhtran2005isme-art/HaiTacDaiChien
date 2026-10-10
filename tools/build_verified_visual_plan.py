#!/usr/bin/env python3
"""Derive private 3D visual preview bindings from exact 3C Image+source Sprite IDs.

No Unity assets, original binary, image, or source field values are published.
The resulting plan is stored exclusively in ignored output/. Its sprite records
are only admissible if original GameObject, RectTransform and MonoBehaviour
component identities agree with the fully verified 3C source graph.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
import re
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
SCENE_RE = re.compile(r"REF[0-9A-Za-z-]+\Z")
SPRITE_RE = re.compile(r"[0-9a-f]{32}\.png\Z")
UI_CLASS = "UnityEngine.UI.Image"
EXPECTED_BINDINGS = 963
CLASSIFICATION = "EXACT_SOURCE_SPRITES_ON_DUAL_VERIFIED_IMAGE_COMPONENTS"


def sha(blob):
    return hashlib.sha256(blob).hexdigest()


def build(verified, graph, prefab, art, blobs):
    if (verified.get("schemaVersion") != 1 or
        verified.get("classification") !=
            "TWO_BACKEND_STRICT_SOURCE_VERIFIED_UI_FIELDS" or
        verified.get("verifiedComponents") != 1108 or
        verified.get("verifiedFieldValues") != 7451 or
        verified.get("singleBackendExcludedFieldValues") != 651 or
        verified.get("sourceGraphSha256") != sha(blobs["graph"]) or
        graph.get("classification") !=
            "SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE" or
        prefab.get("schemaVersion") != 2 or art.get("version") != 1):
        raise ValueError("Verified source documents missing or changed")
    source = {s["sceneId"]: s for s in graph["scenes"]}
    ui = {s["sceneId"]: s for s in verified["scenes"]}
    if (set(source) != set(ui) or len(source) != 5 or
        len(prefab.get("scenes", [])) != 5 or
        art.get("stats", {}).get("exact_node_bindings") != EXPECTED_BINDINGS):
        raise ValueError("Five source scene identities or Sprite counts changed")
    source_art = {
        (s["sceneId"], s["nodeId"], s["imageComponentId"]): s["spriteFile"]
        for s in art.get("nodeBindings", [])
    }
    if len(source_art) != EXPECTED_BINDINGS:
        raise ValueError("Original Sprite binding identities duplicate/missing")
    available = set(art.get("files", []))
    matches, counts, used = [], collections.Counter(), set()
    for scene_id in sorted(source):
        if not SCENE_RE.fullmatch(scene_id):
            raise ValueError("Invalid source scene ID")
        nodes = {n["rectTransformId"]: n for n in source[scene_id]["nodes"]}
        images = {
            (c["rectTransformPathId"], c["componentPathId"]): c
            for c in ui[scene_id]["components"]
            if c["className"] == UI_CLASS
        }
        if len(images) != sum(c["className"] == UI_CLASS for c in ui[scene_id]["components"]):
            raise ValueError("Duplicate cross-verified Image component identity")
        entries = []
        for row in prefab["sprites"]:
            if row["sceneId"] != scene_id:
                continue
            tid = row["nodeId"]
            cid = row["sourceImageComponentId"]
            file = row["spriteFile"]
            key = (scene_id, tid, cid)
            if (key in used or
                not SPRITE_RE.fullmatch(file) or file not in available or
                source_art.get(key) != file):
                raise ValueError("No unique exact Sprite-to-Image source binding")
            used.add(key)
            node = nodes.get(tid)
            image = images.get((tid, cid))
            if (node is None or image is None or
                cid not in node["componentIds"] or
                image["gameObjectPathId"] != node["gameObjectId"] or
                not re.fullmatch(r"[0-9a-f]{64}", image["rawObjectSha256"])):
                raise ValueError("Sprite Image does not match 3C source component ownership")
            entries.append({
                "rectTransformPathId": tid,
                "imageComponentPathId": cid,
                "gameObjectPathId": node["gameObjectId"],
                "sourceObjectSha256": image["rawObjectSha256"],
                "spriteFile": file,
            })
            counts[scene_id] += 1
        if not entries:
            raise ValueError("No original Sprite links in candidate " + scene_id)
        matches.append({"sceneId": scene_id,
                        "bindings": sorted(entries, key=lambda x: x["imageComponentPathId"])})
    if (len(used) != EXPECTED_BINDINGS or
        len(prefab["sprites"]) != EXPECTED_BINDINGS or
        len(source_art) != len(used)):
        raise ValueError("Incomplete exact 3C Sprite mapping")
    return {
        "schemaVersion": 1, "classification": CLASSIFICATION,
        "sourceGraphSha256": verified["sourceGraphSha256"],
        "verifiedUiPlanSha256": sha(blobs["verified"]),
        "spritePrefabPlanSha256": sha(blobs["prefab"]),
        "spriteManifestSha256": sha(blobs["art"]),
        "sourceBindings": EXPECTED_BINDINGS,
        "scenes": matches,
        "stats": dict(sorted(counts.items())),
        "previewOnly": True,
        "limitations": "The 1600x900 root Canvas and Camera are PREVIEW ONLY. "
                       "Native runtime Canvas, Spine, game state and 651 single-backend "
                       "LayoutGroup values remain unverified. The 3C source Prefabs "
                       "are not modified and UI field equality must be checked in Editor.",
    }


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=ROOT)
    args = parser.parse_args()
    root = args.root.resolve()
    paths = {
        "graph": root / "output/original-unity-graph.json",
        "verified": root / "output/verified-ui-prefab-plan.json",
        "prefab": root / "output/unity-prefab-map.json",
        "art": root / "output/local-ui-art/manifest.json",
    }
    blobs = {k: p.read_bytes() for k, p in paths.items()}
    docs = {k: json.loads(v) for k, v in blobs.items()}
    result = build(docs["verified"], docs["graph"], docs["prefab"],
                   docs["art"], blobs)
    dest = root / "output/verified-visual-preview-plan.json"
    dest.parent.mkdir(parents=True, exist_ok=True)
    tmp = dest.with_suffix(".tmp")
    tmp.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n",
                   encoding="utf-8")
    tmp.replace(dest)
    print(json.dumps({"status": "VERIFIED_3D_PREVIEW_PLAN_READY",
                      "sceneCount": len(result["scenes"]),
                      "exactSourceSpriteBindings": result["sourceBindings"],
                      "verifiedUiFieldValues": 7451, "singleBackendValuesImported": 0,
                      "unityAssetsChanged": False}, sort_keys=True))


if __name__ == "__main__":
    main()
