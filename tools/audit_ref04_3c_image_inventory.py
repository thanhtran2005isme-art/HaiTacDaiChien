#!/usr/bin/env python3
"""Distinguish all cross-verified REF04 3C Image components from Sprite-linked images.

A 3C Image can have no original Sprite binding (e.g., a filled or runtime image).
The 265 original Sprite pointers therefore MUST NOT be treated as the count of
all managed Image components stored in the 3C Prefab.
"""
from __future__ import annotations

import json
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
SCENE = "REF04-home-crew"
CLASS = "UnityEngine.UI.Image"


def reconcile(verified: dict, visual: dict, geometry: dict) -> dict:
    if (verified.get("classification") !=
            "TWO_BACKEND_STRICT_SOURCE_VERIFIED_UI_FIELDS" or
            verified.get("verifiedComponents") != 1108 or
            verified.get("verifiedFieldValues") != 7451 or
            visual.get("classification") !=
            "EXACT_SOURCE_SPRITES_ON_DUAL_VERIFIED_IMAGE_COMPONENTS" or
            visual.get("sourceBindings") != 963 or
            geometry.get("classification") !=
            "REF04_EXACT_SOURCE_IMAGE_SPRITE_GEOMETRY" or
            geometry.get("sourceBindings") != 265):
        raise ValueError("Unproven REF04 source plan classification/count")
    scenes3c = [s for s in verified.get("scenes", [])
                if s["sceneId"] == SCENE]
    scenes3d = [s for s in visual.get("scenes", [])
                if s["sceneId"] == SCENE]
    if len(scenes3c) != 1 or len(scenes3d) != 1:
        raise ValueError("Unverified REF04 scene identity")
    rows = [r for r in scenes3c[0]["components"]
            if r["className"] == CLASS]
    indexed = {r["componentPathId"]: r for r in rows}
    links = scenes3d[0]["bindings"]
    image_ids = {r["componentPathId"] for r in geometry["images"]}
    visual_ids = {r["imageComponentPathId"] for r in links}
    if (len(indexed) != len(rows) or len(links) != 265 or
            len(image_ids) != 265 or len(visual_ids) != 265 or
            image_ids != visual_ids or not image_ids.issubset(indexed)):
        raise ValueError("Original Sprite-linked Image identity not a subset of 3C")
    for entry in geometry["images"]:
        image = indexed[entry["componentPathId"]]
        if (image["rectTransformPathId"] != entry["rectTransformPathId"] or
                image["gameObjectPathId"] != entry["gameObjectPathId"] or
                image["rawObjectSha256"] != entry["sourceObjectSha256"]):
            raise ValueError("Original bound Image differs from 3C identity")
    return {
        "status": "REF04_3C_IMAGE_INVENTORY_VERIFIED",
        "source3cAllImageComponentCount": len(rows),
        "sourceImageWithSpriteCount": len(image_ids),
        "source3cImageWithoutSpriteCount": len(rows)-len(image_ids),
        "all265SourceBoundIdsPresentIn3C": True,
        "sameComponentIdSetRequiredInUnityPrefab": True,
        "note": "The 3C all-Image inventory is not required to equal the "
                "265 independent original Sprite bindings.",
    }


def main():
    root = ROOT / "output"
    docs = [
        json.loads((root / name).read_text(encoding="utf-8"))
        for name in ("verified-ui-prefab-plan.json",
                     "verified-visual-preview-plan.json",
                     "ref04-static-image-geometry.json")
    ]
    out = reconcile(*docs)
    target = root / "ref04-3c-image-inventory.json"
    target.write_text(json.dumps(out,ensure_ascii=False,indent=2)+"\n",
                      encoding="utf-8")
    print(json.dumps(out,sort_keys=True))


if __name__ == "__main__":
    main()
