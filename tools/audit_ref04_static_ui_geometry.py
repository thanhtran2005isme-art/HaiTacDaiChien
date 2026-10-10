#!/usr/bin/env python3
"""Strict REF04-only native Sprite border/PPU inventory for source-linked UI.

All assets remain in gitignored output/. A border is admissible only if the
original XAPK Sprite geometry has a unique original RectTransform owner and
matches the exact Image/Sprite mapping independently proven in Phase 3D.
This NEVER changes Unity artwork, 3C Prefabs, or a Canvas viewport.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
import math
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
SCENE = "REF04-home-crew"
CLASS = "UnityEngine.UI.Image"


def digest(raw: bytes) -> str:
    return hashlib.sha256(raw).hexdigest()


def numeric_array(value, length):
    if (not isinstance(value, list) or len(value) != length or
            any(type(v) not in (float, int) or not math.isfinite(v)
                for v in value)):
        return False
    return True


def prepare(visual, verified, graph, deep, blobs):
    if (visual.get("classification") !=
            "EXACT_SOURCE_SPRITES_ON_DUAL_VERIFIED_IMAGE_COMPONENTS" or
            visual.get("sourceBindings") != 963 or
            verified.get("classification") !=
            "TWO_BACKEND_STRICT_SOURCE_VERIFIED_UI_FIELDS" or
            verified.get("verifiedFieldValues") != 7451 or
            verified.get("singleBackendExcludedFieldValues") != 651 or
            visual.get("sourceGraphSha256") != digest(blobs["graph"]) or
            visual.get("verifiedUiPlanSha256") != digest(blobs["verified"]) or
            graph.get("classification") !=
            "SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE" or
            deep.get("version") != 1):
        raise ValueError("Original strict XAPK evidence is missing or stale")
    art = [s for s in visual["scenes"] if s["sceneId"] == SCENE]
    ui = [s for s in verified["scenes"] if s["sceneId"] == SCENE]
    roots = [s for s in graph["scenes"] if s["sceneId"] == SCENE]
    details = [s for s in deep["scenes"] if s["sceneId"] == SCENE]
    if not all(len(s) == 1 for s in (art, ui, roots, details)):
        raise ValueError("REF04 source scene is ambiguous")
    art, ui, roots, details = (s[0] for s in (art, ui, roots, details))
    source_nodes = {n["rectTransformId"]: n for n in roots["nodes"]}
    components = {c["componentPathId"]: c for c in ui["components"]}
    raw = collections.defaultdict(list)
    for shape in details.get("spriteGeometry", []):
        # Refuse path/name-only legacy records. Each native Sprite border
        # must be attached to the same exact Image Component PathID as the
        # independently decoded 3C Image and Phase 3D Sprite binding.
        if "imageComponentId" in shape:
            raw[(shape["nodeId"], shape["imageComponentId"])].append(shape)
    output, counts = [], collections.Counter()
    for link in art["bindings"]:
        image_id = link["imageComponentPathId"]
        node_id = link["rectTransformPathId"]
        node = source_nodes.get(node_id)
        component = components.get(image_id)
        if (not node or not component or
                component["className"] != CLASS or
                image_id not in node["componentIds"] or
                component["gameObjectPathId"] != node["gameObjectId"] or
                component["rawObjectSha256"] != link["sourceObjectSha256"] or
                component["rectTransformPathId"] != node_id):
            raise ValueError("REF04 verified source Image identity contradicted")
        source_fields = {f["name"]: f for f in component["fields"]}
        if "m_Type" not in source_fields or source_fields["m_Type"]["kind"] != "int":
            raise ValueError("REF04 Image Type missing from both source backends")
        type_id = source_fields["m_Type"]["intValue"]
        if type_id not in (0, 1, 2, 3):
            raise ValueError("REF04 Image.Type out of source range")
        row = {
            "rectTransformPathId": node_id,
            "componentPathId": image_id,
            "gameObjectPathId": node["gameObjectId"],
            "sourceObjectSha256": component["rawObjectSha256"],
            "spriteFile": link["spriteFile"],
            "verifiedImageType": type_id,
            "nativeSpriteGeometryStatus": "UNVERIFIED",
            "applyGeometry": False,
        }
        choices = raw[(node_id, image_id)]
        if len(choices) != 1:
            row["nativeSpriteGeometryStatus"] = (
                "NO_UNIQUE_NATIVE_SPRITE_GEOMETRY")
            counts["unverifiedUniqueSpriteGeometry"] += 1
        else:
            shape = choices[0]
            border = shape.get("border")
            size = shape.get("sourceRectSize")
            ppu = shape.get("pixelsPerUnit")
            if (not numeric_array(border, 4) or
                not numeric_array(size, 2) or
                type(ppu) not in (float, int) or not math.isfinite(ppu) or
                not 0.001 <= ppu <= 10000 or
                min(size) <= 0 or max(size) > 16384 or
                min(border) < 0 or
                border[0] + border[2] > size[0] or
                border[1] + border[3] > size[1]):
                row["nativeSpriteGeometryStatus"] = "INVALID_OR_INCOMPLETE_NATIVE_GEOMETRY"
                counts["invalidOrIncompleteGeometry"] += 1
            else:
                row.update({
                    "nativeSpriteGeometryStatus": "NATIVE_SPRITE_GEOMETRY_VERIFIED",
                    "applyGeometry": True,
                    "border": [float(v) for v in border],
                    "sourceRectSize": [float(v) for v in size],
                    "pixelsPerUnit": float(ppu),
                })
                counts["sourceGeometryVerified"] += 1
        if type_id == 1:
            counts["slicedImage"] += 1
            if row["applyGeometry"] and any(row["border"]):
                counts["slicedWithVerifiedNonzeroBorder"] += 1
            if not row["applyGeometry"]:
                counts["slicedMissingVerifiedGeometry"] += 1
        counts["imageBindings"] += 1
        output.append(row)
    if len({r["componentPathId"] for r in output}) != len(output):
        raise ValueError("Duplicate source-linked Image on REF04")
    if not output:
        raise ValueError("No source-linked REF04 Images")
    return {
        "schemaVersion": 1,
        "classification": "REF04_EXACT_SOURCE_IMAGE_SPRITE_GEOMETRY",
        "sceneId": SCENE,
        "sourceGraphSha256": visual["sourceGraphSha256"],
        "verifiedUiPlanSha256": visual["verifiedUiPlanSha256"],
        "verifiedVisualPlanSha256": digest(blobs["visual"]),
        "nativeGeometryEvidenceSha256": digest(blobs["deep"]),
        "sourceBindings": len(output),
        "counts": dict(sorted(counts.items())),
        "images": output,
        "limitations": "Native Sprite geometry is available for unique source node "
                       "records only; missing/ambiguous geometries stay UNVERIFIED "
                       "and must not be auto-applied. Game runtime Canvas, text, "
                       "Spine, LayoutGroups and screen pixel-perfectness are not proven.",
    }


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=ROOT)
    args = parser.parse_args()
    root = args.root.resolve()
    names = {
        "graph": "output/original-unity-graph.json",
        "verified": "output/verified-ui-prefab-plan.json",
        "visual": "output/verified-visual-preview-plan.json",
        "deep": "output/local-ui-components.json",
    }
    blobs = {n: (root / path).read_bytes() for n, path in names.items()}
    docs = {n: json.loads(blob) for n, blob in blobs.items()}
    result = prepare(docs["visual"], docs["verified"],
                     docs["graph"], docs["deep"], blobs)
    target = root / "output/ref04-static-image-geometry.json"
    tmp = target.with_suffix(".tmp")
    tmp.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n",
                   encoding="utf-8")
    tmp.replace(target)
    print(json.dumps({"status": "REF04_SOURCE_IMAGE_GEOMETRY_AUDITED",
                      "counts": result["counts"],
                      "sourceBindings": result["sourceBindings"],
                      "unityAssetsChanged": False}, sort_keys=True))


if __name__ == "__main__":
    main()
