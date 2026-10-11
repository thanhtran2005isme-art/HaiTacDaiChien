#!/usr/bin/env python3
"""Source-only REF04 Canvas/Scaler/SafeArea/PanelHome2 parent graph.

Serialized ancestry is not evidence of instantiated runtime hierarchy or
SafeArea formula. Never import this report as Unity layout.
"""
from __future__ import annotations
from collections import Counter

KINDS = ("Canvas", "CanvasScaler", "SafeArea", "PanelHome2")
# Unity CanvasScaler serialized configuration names. Presence only; no scale math.
SCALER_CONFIG_FIELDS = frozenset(("m_UiScaleMode", "m_ReferenceResolution",
    "m_ScreenMatchMode", "m_MatchWidthOrHeight", "m_ScaleFactor",
    "m_ReferencePixelsPerUnit", "m_PhysicalUnit", "m_FallbackScreenDPI",
    "m_DefaultSpriteDPI", "m_DynamicPixelsPerUnit"))

def build_graph(records):
    if not isinstance(records, list):
        raise ValueError("Original source component records missing")
    by_kind = {kind: [] for kind in KINDS}
    seen = set()
    for row in records:
        if not isinstance(row, dict) or row.get("category") not in by_kind:
            raise ValueError("Unexpected REF04 source component category")
        cid = row.get("componentPathId")
        gid = row.get("gameObjectPathId")
        tid = row.get("rectTransformPathId")
        ancestry = row.get("ancestry")
        if (type(cid) is not int or type(gid) is not int or
            type(tid) is not int or cid in seen or
            not isinstance(ancestry, dict)):
            raise ValueError("Original component identity/ancestry invalid")
        seen.add(cid)
        path = ancestry.get("originalRectTransformPathIdsLeafToAncestor")
        if (not isinstance(path, list) or not path or path[0] != tid or
            any(type(x) is not int for x in path) or len(path) != len(set(path))):
            raise ValueError("Original parent path invalid or cyclic")
        nearest = ancestry.get("originalNearestCanvasRectTransformPathId")
        if nearest is not None and nearest not in path:
            raise ValueError("Canvas ancestor absent from source pointer path")
        if ancestry.get("runtimeCanvasOrViewportProven") is not False:
            raise ValueError("Serialized hierarchy promoted to runtime")
        if row.get("unityImportAllowed") is not False or row.get("runtimeFormulaProven") is not False:
            raise ValueError("Source evidence promoted to generated UI")
        by_kind[row["category"]].append(row)
    if len(by_kind["Canvas"]) != 1 or len(by_kind["CanvasScaler"]) != 1 or len(by_kind["SafeArea"]) != 6:
        raise ValueError("Original REF04 Canvas/Scaler/SafeArea inventory mismatch")
    canvas = by_kind["Canvas"][0]
    scaler = by_kind["CanvasScaler"][0]
    root = canvas["rectTransformPathId"]
    if canvas["ancestry"]["originalNearestCanvasRectTransformPathId"] != root:
        raise ValueError("Canvas did not resolve itself on original path")
    declared_scaler_fields = scaler.get("verifiedSerializedFieldNames")
    if not isinstance(declared_scaler_fields, list) or not all(
            isinstance(item, str) for item in declared_scaler_fields):
        raise ValueError("CanvasScaler verified serialized field inventory missing")
    scale_keys = set(declared_scaler_fields) & SCALER_CONFIG_FIELDS
    panels = {row["rectTransformPathId"] for row in by_kind["PanelHome2"]}
    safes = {row["rectTransformPathId"] for row in by_kind["SafeArea"]}
    statuses = Counter()
    children = []
    for kind in ("CanvasScaler", "SafeArea", "PanelHome2"):
        for row in sorted(by_kind[kind], key=lambda x: x["componentPathId"]):
            path = row["ancestry"]["originalRectTransformPathIdsLeafToAncestor"]
            canvas_index = path.index(root) if root in path else None
            original_canvas = (row["ancestry"]["originalNearestCanvasRectTransformPathId"] == root
                               and canvas_index is not None)
            if canvas_index is None and row["ancestry"]["originalNearestCanvasRectTransformPathId"] is not None:
                raise ValueError("Unknown Canvas unexpectedly assigned to source component")
            status = "SOURCE_CANVAS_ANCESTOR_VERIFIED" if original_canvas else "SOURCE_CANVAS_ANCESTOR_NOT_VERIFIED"
            statuses[kind + ":" + status] += 1
            children.append({
                "category": kind,
                "componentPathId": row["componentPathId"],
                "originalGameObjectPathId": row["gameObjectPathId"],
                "originalRectTransformPathId": row["rectTransformPathId"],
                "originalParentRectTransformPathId": path[1] if len(path) > 1 else None,
                "originalCanvasAncestorConfirmed": original_canvas,
                "originalCanvasDistanceInParentEdges": canvas_index if original_canvas else None,
                "serializedParentTraceStatus": row["ancestry"].get("sourceParentTraceStatus"),
                "serializedSafeAreaAncestorRectTransformPathIds": sorted(set(path) & safes),
                "serializedPanelHome2AncestorRectTransformPathIds": sorted(set(path) & panels),
                "runtimeScriptBindingProven": False,
                "runtimePositionOrScaleProven": False,
            })
    colocated = (scaler["gameObjectPathId"] == canvas["gameObjectPathId"] and
                 scaler["rectTransformPathId"] == root)
    return {
        "classification": "REF04_ORIGINAL_CANVAS_COMPONENT_PARENT_GRAPH_SOURCE_ONLY",
        "sourceCanvas": {
            "componentPathId": canvas["componentPathId"],
            "gameObjectPathId": canvas["gameObjectPathId"],
            "rectTransformPathId": root,
            "originalParentPath": canvas["ancestry"]["originalRectTransformPathIdsLeafToAncestor"],
            "serializedParentTraceStatus": canvas["ancestry"].get("sourceParentTraceStatus"),
        },
        "sourceCanvasScaler": {
            "componentPathId": scaler["componentPathId"],
            "gameObjectPathId": scaler["gameObjectPathId"],
            "rectTransformPathId": scaler["rectTransformPathId"],
            "originalSameGameObjectAsCanvas": colocated,
            "originalSameGameObjectStatus": ("SOURCE_SAME_GAMEOBJECT_VERIFIED" if colocated
                                             else "SOURCE_DIFFERENT_GAMEOBJECT"),
            "sourceVerifiedSerializedFieldNames": scaler["verifiedSerializedFieldNames"],
            "originalSerializedScaleConfigFieldNames": sorted(scale_keys),
            "originalSerializedScaleConfigMissingFieldNames": sorted(SCALER_CONFIG_FIELDS - scale_keys),
            "originalSerializedScaleConfigurationSourceOnly": True,
            "runtimeScaleModeAndEffectiveScaleUnproven": True,
        },
        "relatedComponents": children,
        "sourceAncestryCounts": dict(sorted(statuses.items())),
        "sourceHierarchyOnly": True,
        "safeAreaPanelHome2RuntimeCallBindingProven": False,
        "screenSafeAreaDeviceStateProven": False,
        "runtimeCanvasScaleFormula": None,
        "runtimeSafeAreaPanelHome2Formula": None,
        "runtimeLayoutProven": False,
        "unityImportAllowed": False,
    }
