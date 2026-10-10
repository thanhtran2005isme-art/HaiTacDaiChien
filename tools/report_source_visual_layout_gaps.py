#!/usr/bin/env python3
"""Strictly audit why original XAPK UI hierarchies are not standalone screen UIs.

Only summarizes confirmed source hierarchy and component provenance. DOES NOT
infer the game runtime viewport, Spine state, anchors from screenshots, or
previously unverified LayoutGroup fields.
"""
from __future__ import annotations

import collections
import json
import math
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
GRAPH = "SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE"


def source_layout_gap_report(graph, plan):
    if graph.get("classification") != GRAPH or len(graph.get("scenes", [])) != 5:
        raise ValueError("Unverified source graph")
    if (plan.get("classification") !=
            "EXACT_SOURCE_SPRITES_ON_DUAL_VERIFIED_IMAGE_COMPONENTS" or
            len(plan.get("scenes", [])) != 5 or plan.get("sourceBindings") != 963):
        raise ValueError("Unverified dual-source Sprite plan")
    result = []
    screen_roots_unknown = 0
    for sc in graph["scenes"]:
        name = sc["sceneId"]
        roots = [node for node in sc["nodes"]
                 if node["rectTransformId"] == sc["candidateRootTransform"]]
        if len(roots) != 1:
            raise ValueError("Original candidate root missing " + name)
        node = roots[0]
        components = {c["pathId"]: c for c in sc["components"]}
        if len(components) != len(sc["components"]):
            raise ValueError("Component ID collision")
        kinds = collections.Counter(c["kind"] for c in sc["components"])
        rect = node["rect"]
        width = rect.get("sizeDelta") or []
        local_scale = rect.get("localScale") or []
        if len(width) not in (0, 2) or len(local_scale) not in (0, 3):
            raise ValueError("Source RectTransform malformed")
        if any(not math.isfinite(x) for x in width + local_scale):
            raise ValueError("Nonfinite source geometry")
        parent = node.get("parent") or {}
        external = parent.get("fileId") != 0 or parent.get("pathId") not in (None, 0)
        is_zero_scale = len(local_scale) == 3 and any(
            abs(x) < 1e-7 for x in local_scale[:2])
        inferred = (external or is_zero_scale)
        screen_roots_unknown += int(inferred)
        sprite_rows = next(s["bindings"] for s in plan["scenes"]
                           if s["sceneId"] == name)
        by_node = {n["rectTransformId"]: n for n in sc["nodes"]}
        no_native_canvas = sum(
            not any(components.get(cid, {}).get("kind") == "Canvas"
                    for cid in n.get("componentIds", []))
            for n in sc["nodes"]
            if any(components.get(cid, {}).get("kind") == "CanvasScaler"
                   for cid in n.get("componentIds", []))
        )
        original_rect_nodes = sum(
            1 for n in sc["nodes"] if
            all(len((n.get("rect") or {}).get(f) or []) == size
                for f, size in (("anchorMin", 2), ("anchorMax", 2),
                                ("pivot", 2), ("sizeDelta", 2),
                                ("anchoredPosition", 2), ("localScale", 3))))
        result.append({
            "sceneId": name,
            "nodes": len(sc["nodes"]),
            "sourceRectNodesWithCoreValues": original_rect_nodes,
            "candidateRootParent": parent,
            "rootSizeDelta": width,
            "rootScale": local_scale,
            "rootAnchorMin": rect.get("anchorMin"),
            "rootAnchorMax": rect.get("anchorMax"),
            "rootAnchoredPosition": rect.get("anchoredPosition"),
            "candidateIsRuntimeViewportProven": False,
            "rootHasExternalParentOrZeroScale": inferred,
            "nativeCanvasCount": kinds["Canvas"],
            "nativeRectMask2DCount": kinds["RectMask2D"],
            "nativeCanvasRendererCount": kinds["CanvasRenderer"],
            "managedMonoBehaviourTypesUnresolved": sum(
                1 for c in sc["components"]
                if c.get("kind") == "MonoBehaviour" and
                c.get("monoScriptPointer") is not None and
                c.get("fieldStatus") == "managed_fields_unavailable"),
            "spriteBindings": len(sprite_rows),
            "allSpriteOwnersKnown": all(
                entry["rectTransformPathId"] in by_node for entry in sprite_rows),
        })
    return {
        "schemaVersion": 1,
        "classification": "SOURCE_UI_VIEWPORT_RUNTIME_GAPS_NOT_FIXED_BY_3D",
        "candidateSceneCount": 5,
        "externalOrZeroScaleCandidateRoots": screen_roots_unknown,
        "runtimeCanvasOrViewportRecovered": False,
        "spineGameplayLayoutRecovered": False,
        "scenes": result,
        "note": "These five serialized candidate UI roots do not prove their "
                "runtime camera/canvas ancestor, device safe area, dynamic LayoutGroups "
                "or Spine state. A successful Sprite and managed-field audit alone "
                "cannot prove visually correct standalone screens.",
    }


def main():
    graph = json.loads((ROOT / "output/original-unity-graph.json").read_text(
        encoding="utf-8"))
    plan = json.loads((ROOT / "output/verified-visual-preview-plan.json").read_text(
        encoding="utf-8"))
    result = source_layout_gap_report(graph, plan)
    target = ROOT / "output/source-visual-layout-gaps.json"
    target.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n",
                      encoding="utf-8")
    print(json.dumps({
        "classification": result["classification"],
        "candidateRootsWithExternalParentOrZeroScale":
            result["externalOrZeroScaleCandidateRoots"],
        "sourceUiRuntimeViewportVerified": False,
        "scenes": [
            {"sceneId": x["sceneId"], "rootSizeDelta": x["rootSizeDelta"],
             "rootScale": x["rootScale"],
             "rootAnchorMin": x["rootAnchorMin"],
             "rootAnchorMax": x["rootAnchorMax"],
             "rootAnchoredPosition": x["rootAnchoredPosition"],
             "externalParent": x["candidateRootParent"],
             "nativeCanvasCount": x["nativeCanvasCount"],
             "nativeRectMask2DCount": x["nativeRectMask2DCount"],
             "spriteCount": x["spriteBindings"]}
            for x in result["scenes"]],
    }, ensure_ascii=False))


if __name__ == "__main__":
    main()
