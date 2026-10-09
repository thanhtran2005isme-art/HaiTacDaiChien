#!/usr/bin/env python3
"""Export an OPT-IN, local-only plan for source-verified uGUI Prefab study copies.

Never writes Unity assets. Only fields that match exactly across TWO independent
strict generated TypeTrees are exported. All generated values remain gitignored
under output/. Editor importer still independently checks the source graph SHA,
node/component ownership and field allowlist before making separate copies.
"""
from __future__ import annotations

import argparse
import hashlib
import json
import math
from collections import Counter
from pathlib import Path

import compare_managed_ui_backends as comparison

ROOT = Path(__file__).resolve().parents[1]
CLASS_FIELDS = {
    "UnityEngine.UI.Image": {
        "m_Color": "color", "m_Type": "int", "m_PreserveAspect": "bool",
        "m_FillMethod": "int", "m_FillAmount": "float",
        "m_FillOrigin": "int", "m_FillClockwise": "bool",
    },
    "UnityEngine.UI.CanvasScaler": {
        "m_UiScaleMode": "int", "m_ScreenMatchMode": "int",
        "m_ReferenceResolution": "vector2", "m_MatchWidthOrHeight": "float",
        "m_ScaleFactor": "float", "m_ReferencePixelsPerUnit": "float",
    },
    "UnityEngine.UI.Mask": {"m_ShowMaskGraphic": "bool"},
    "UnityEngine.UI.ContentSizeFitter": {
        "m_HorizontalFit": "int", "m_VerticalFit": "int",
    },
}
EXPECTED_CLASSES = {"UnityEngine.UI.Image": 1052,
                    "UnityEngine.UI.CanvasScaler": 4,
                    "UnityEngine.UI.Mask": 41,
                    "UnityEngine.UI.ContentSizeFitter": 11}
EXPECTED_EXCLUDED = 93
EXPECTED_EXCLUDED_FIELDS = 651
EXPECTED_FIELDS = 7451
CLASS_VALUES = {
    "UnityEngine.UI.Image": 7, "UnityEngine.UI.CanvasScaler": 6,
    "UnityEngine.UI.Mask": 1, "UnityEngine.UI.ContentSizeFitter": 2,
}


def _source_graph(graph):
    if (graph.get("version") != 1 or
            graph.get("classification") !=
            "SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE" or
            len(graph.get("scenes", [])) != 5):
        raise ValueError("Original source graph is unavailable or not verified")
    by_scene = {}
    for scene in graph["scenes"]:
        name = scene["sceneId"]
        if name in by_scene:
            raise ValueError("Duplicate source scene")
        nodes = {}
        for node in scene["nodes"]:
            key = node["rectTransformId"]
            if key in nodes:
                raise ValueError("Duplicate source RectTransform PathID")
            nodes[key] = node
        components = {c["pathId"]: c for c in scene["components"]}
        if len(components) != len(scene["components"]):
            raise ValueError("Duplicate source component PathID")
        by_scene[name] = (nodes, components)
    return by_scene


def _encode(name, kind, value):
    row = {"name": name, "kind": kind}
    if kind == "bool":
        if type(value) is not bool:
            raise ValueError("Boolean field type is not verified: " + name)
        row["boolValue"] = value
    elif kind == "int":
        if type(value) is not int:
            raise ValueError("Integer field type is not verified: " + name)
        row["intValue"] = value
    elif kind == "float":
        if type(value) not in (int, float) or not math.isfinite(value):
            raise ValueError("Non-finite numeric source field: " + name)
        row["floatValue"] = float(value)
    elif kind in ("color", "vector2"):
        keys = ("r", "g", "b", "a") if kind == "color" else ("x", "y")
        if (not isinstance(value, dict) or set(value) != set(keys) or
                any(type(value[k]) not in (int, float) or
                    not math.isfinite(value[k]) for k in keys)):
            raise ValueError("Vector/color malformed: " + name)
        row["floatValues"] = [float(value[k]) for k in keys]
    else:
        raise ValueError("Disallowed managed source field type")
    return row


def prepare(studio, ripper, graph, graph_sha256):
    # This checks all 5346 IDs, all source SHA256s and exact shared raw field
    # values, and will fail closed on any source or backend discrepancy.
    stats = comparison.compare(studio, ripper)
    a, pa = comparison.canonical_index(studio)
    b, _ = comparison.canonical_index(ripper)
    source = _source_graph(graph)
    if set(source) != {s["sceneId"] for s in studio["scenes"]}:
        raise ValueError("Graph and decoded source scenes differ")
    if (stats.get("independentlyMatchedComponents") != 1108 or
            stats.get("independentlyMatchedFieldValues") != EXPECTED_FIELDS or
            stats.get("studioOnlyComponents", 0) != EXPECTED_EXCLUDED or
            stats.get("studioOnlyFieldValues", 0) != EXPECTED_EXCLUDED_FIELDS):
        raise ValueError("Exact source recovery inventory changed; review before import")
    output = []
    class_counts = Counter()
    skipped = Counter()
    for scene_id in sorted(source):
        nodes, graph_components = source[scene_id]
        by_object = {node["gameObjectId"]: node for node in nodes.values()}
        if len(by_object) != len(nodes):
            raise ValueError("Duplicate source GameObject PathID")
        entries = []
        for key in sorted(x for x in a if x[0] == scene_id):
            l, r = a[key], b[key]
            if l.get("status") != comparison.SUCCEEDED:
                continue
            if r.get("status") != comparison.SUCCEEDED:
                skipped["unconfirmedComponent"] += 1
                skipped["unconfirmedFieldValues"] += len(l["fields"])
                continue
            cls = l.get("className")
            if cls not in CLASS_FIELDS:
                raise ValueError("Unknown jointly verified UI class: " + str(cls))
            if l["fields"] != r["fields"]:
                raise ValueError("Cross-backend managed fields disagree")
            if (l.get("kind") != "MonoBehaviour" or
                    type(l.get("nativeEnabled")) is not bool or
                    l.get("nativeEnabled") != r.get("nativeEnabled")):
                raise ValueError("Source native enabled flag not independently verified")
            lp, rp = l.get("binaryProof", {}), r.get("binaryProof", {})
            for k in ("rawObjectSha256", "rawObjectBytes"):
                if not lp.get(k) or lp.get(k) != rp.get(k):
                    raise ValueError("Original source object bytes not identical")
            for proof in (lp, rp):
                if (not proof.get("strictObjectSizeChecked") or
                        not proof.get("exactSourcePointerChecked") or
                        proof.get("nativeHeaderMethod") !=
                        "UNITYPY_EXACT_SOURCE_UNITY_VERSION"):
                    raise ValueError("Missing strict source binary proof")
            node = by_object.get(l.get("gameObjectId"))
            if (node is None or l.get("rectTransformId") !=
                    node["rectTransformId"] or
                    l["pathId"] not in node["componentIds"]):
                raise ValueError("Recovered field does not belong to source node")
            graph_item = graph_components.get(l["pathId"])
            if (graph_item is None or graph_item["kind"] != "MonoBehaviour" or
                    graph_item.get("gameObjectPointer") !=
                    {"fileId": 0, "pathId": node["gameObjectId"]} or
                    graph_item.get("monoScriptPointer") != l.get("scriptPointer")):
                raise ValueError("Source graph script/owner pointers contradict managed field")
            expected = CLASS_FIELDS[cls]
            if set(l["fields"]) != set(expected):
                raise ValueError("Missing or unexpected class-managed fields in source")
            fields = [_encode(field, expected[field], l["fields"][field])
                      for field in sorted(expected)]
            # Unity RequireComponent dependencies must themselves originate from
            # this GameObject's serialized native component list; never synthesize
            # an Image or Canvas simply to satisfy a dependency.
            native = {graph_components[c]["kind"] for c in node["componentIds"]
                      if c in graph_components}
            if cls == "UnityEngine.UI.Image" and "CanvasRenderer" not in native:
                raise ValueError("Verified Image lacks original CanvasRenderer")
            if cls == "UnityEngine.UI.CanvasScaler" and "Canvas" not in native:
                raise ValueError("Verified CanvasScaler lacks original Canvas")
            entries.append({
                "componentPathId": l["pathId"],
                "gameObjectPathId": node["gameObjectId"],
                "rectTransformPathId": node["rectTransformId"],
                "className": cls, "enabled": l["nativeEnabled"],
                "rawObjectSha256": lp["rawObjectSha256"],
                "fields": fields,
            })
            class_counts[cls] += 1
        output.append({"sceneId": scene_id, "components": entries})
    if dict(class_counts) != EXPECTED_CLASSES:
        raise ValueError("Recovered class distribution changed; review before import")
    if sum(len(c["fields"]) for scene in output for c in scene["components"]) != EXPECTED_FIELDS:
        raise ValueError("Cross-verified field count mismatch")
    if (skipped["unconfirmedComponent"] != EXPECTED_EXCLUDED or
            skipped["unconfirmedFieldValues"] != EXPECTED_EXCLUDED_FIELDS):
        raise ValueError("Insufficient source verification accounting")
    return {
        "schemaVersion": 1,
        "classification": "TWO_BACKEND_STRICT_SOURCE_VERIFIED_UI_FIELDS",
        "unityVersion": pa["gameUnityVersion"],
        "sourceGraphSha256": graph_sha256,
        "sourceLibrarySha256": pa["library"]["sha256"],
        "sourceMetadataSha256": pa["metadata"]["sha256"],
        "verifiedComponents": 1108,
        "verifiedFieldValues": EXPECTED_FIELDS,
        "singleBackendExcludedComponents": EXPECTED_EXCLUDED,
        "singleBackendExcludedFieldValues": EXPECTED_EXCLUDED_FIELDS,
        "scenes": output,
        "warning": "Study-only prefab copies. Original editor Prefabs/Scenes and "
                   "runtime appearance are NOT proven; Unity may add dependencies "
                   "and default fields not present in this plan.",
    }


def main():
    ap = argparse.ArgumentParser(description=__doc__)
    ap.add_argument("--root", type=Path, default=ROOT)
    ap.add_argument("--assetstudio", type=Path)
    ap.add_argument("--assetripper", type=Path)
    args = ap.parse_args()
    root = args.root.resolve()
    studio = args.assetstudio or root / "output/phase3b-assetstudio.json"
    ripper = args.assetripper or root / "output/deep-ui-source-evidence.json"
    graph_path = root / "output/original-unity-graph.json"
    raw = graph_path.read_bytes()
    result = prepare(
        json.loads(studio.read_text(encoding="utf-8")),
        json.loads(ripper.read_text(encoding="utf-8")),
        json.loads(raw), hashlib.sha256(raw).hexdigest(),
    )
    dest = root / "output/verified-ui-prefab-plan.json"
    dest.parent.mkdir(exist_ok=True, parents=True)
    temp = dest.with_suffix(".tmp")
    temp.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n",
                    encoding="utf-8")
    temp.replace(dest)
    print(json.dumps({
        "status": "STRICT_TWO_BACKEND_PREFAB_STUDY_PLAN",
        "verifiedComponents": result["verifiedComponents"],
        "verifiedFields": result["verifiedFieldValues"],
        "excludedSingleBackendComponents":
            result["singleBackendExcludedComponents"],
        "excludedSingleBackendFields":
            result["singleBackendExcludedFieldValues"],
        "prefabsChanged": False, "privateOutput": str(dest.name),
    }, sort_keys=True))


if __name__ == "__main__":
    main()
