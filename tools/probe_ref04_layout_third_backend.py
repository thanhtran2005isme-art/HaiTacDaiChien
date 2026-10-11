#!/usr/bin/env python3
"""REF04 LayoutGroup: independently retry 24 blocked source objects with AssetsTools.

Uses ONLY the exact original XAPK libil2cpp.so+global-metadata.dat, Unity
2022.3.51f1 SerializedFile MonoBehaviour header, Component PathIDs and raw
SHA256. Cross-compares strict full-object third-backend field values to
previously source-verified AssetStudio values. Never imports values to Unity.
Every unresolved/conflicting component remains BLOCKED.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
from pathlib import Path

import decode_xapk_ui_provenance as deep_source
import export_local_ui_layout as layout
import recover_managed_ui_fields as recovery

ROOT = Path(__file__).resolve().parents[1]
SCENE = "REF04-home-crew"
KINDS = (
    "UnityEngine.UI.HorizontalLayoutGroup",
    "UnityEngine.UI.VerticalLayoutGroup",
)
EXPECTED_LAYOUTS = 24
EXPECTED_FIELDS = 168


def source_plan(studio, review, graph):
    if (review.get("classification") !=
            "SINGLE_BACKEND_UI_FIELDS_NOT_FOR_PREFAB_IMPORT" or
        review.get("componentCount") != 93 or
        review.get("excludedFieldValues") != 651 or
        graph.get("classification") !=
            "SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE"):
        raise ValueError("Previous original XAPK graph/layout review not verified")
    matches = [s for s in studio["scenes"] if s["sceneId"] == SCENE]
    roots = [s for s in graph["scenes"] if s["sceneId"] == SCENE]
    if len(matches) != 1 or len(roots) != 1:
        raise ValueError("REF04 source scene/graph not unique")
    source_rows = {x["pathId"]: x for x in matches[0]["components"]}
    native_rows = {x["pathId"]: x for x in roots[0]["components"]}
    if len(source_rows) != len(matches[0]["components"]) or (
        len(native_rows) != len(roots[0]["components"])):
        raise ValueError("Source component PathIDs not unique")
    review_rows = [x for x in review["components"] if x["sceneId"] == SCENE]
    if (len(review_rows) != EXPECTED_LAYOUTS or
        sum(x["fieldCount"] for x in review_rows) != EXPECTED_FIELDS):
        raise ValueError("REF04 LayoutGroup source count/fields changed")
    out = {}
    for item in review_rows:
        cid = item["componentPathId"]
        row = source_rows.get(cid)
        origin = native_rows.get(cid)
        if (cid in out or row is None or origin is None or
                item["className"] not in KINDS or
                item["prefabImportAllowed"] is not False or
                row.get("className") != item["className"] or
                row.get("status") != "GENERATED_TYPETREE_SOURCE_VERIFIED" or
                row.get("gameObjectId") != item["gameObjectPathId"] or
                row.get("rectTransformId") != item["rectTransformPathId"] or
                row.get("binaryProof", {}).get("rawObjectSha256") !=
                    item["rawObjectSha256"] or
                origin.get("rawEvidence", {}).get("sha256") !=
                    item["rawObjectSha256"] or
                set(row.get("fields", {})) != set(item["fieldNames"]) or
                len(row["fields"]) != item["fieldCount"] or
                row.get("kind") != "MonoBehaviour"):
            raise ValueError("REF04 blocked layout source record contradicts XAPK")
        out[cid] = row
    return out, roots[0]["sourceSerializedFile"]


def compare_candidate(studio_row, new_fields, proof):
    if (new_fields is None or
        set(new_fields) != set(studio_row["fields"]) or
        len(new_fields) != len(studio_row["fields"])):
        return "THIRD_BACKEND_FIELD_SET_DISAGREES"
    if (proof.get("rawObjectSha256") !=
            studio_row["binaryProof"]["rawObjectSha256"] or
        not proof.get("exactSourcePointerChecked") or
        not proof.get("strictObjectSizeChecked") or
        proof.get("nativeHeaderMethod") !=
            "UNITYPY_EXACT_SOURCE_UNITY_VERSION"):
        return "THIRD_BACKEND_SOURCE_OBJECT_PROOF_UNVERIFIED"
    if any(type(new_fields[k]) is not type(studio_row["fields"][k]) or
           new_fields[k] != studio_row["fields"][k]
           for k in studio_row["fields"]):
        return "THIRD_BACKEND_FIELD_VALUES_DISAGREE"
    return "THIRD_BACKEND_FULL_SOURCE_OBJECT_AGREES_NOT_IMPORTED"


def classify(studio, review, graph, samples, source_proof):
    out, source_file = source_plan(studio, review, graph)
    rows = []
    for cid, expected in sorted(out.items()):
        evidence = samples.get(cid)
        if evidence is None:
            raise ValueError("Third backend did not visit exact REF04 LayoutGroup")
        status, fields, checked = evidence
        if status == "DECODED":
            status = compare_candidate(expected, fields, checked)
        elif not str(status).startswith("BLOCKED_"):
            raise ValueError("Invalid source decode outcome")
        rows.append({
            "componentPathId": cid,
            "gameObjectPathId": expected["gameObjectId"],
            "rectTransformPathId": expected["rectTransformId"],
            "className": expected["className"],
            "sourceObjectSha256": expected["binaryProof"]["rawObjectSha256"],
            "sourceFieldNames": sorted(expected["fields"]),
            "sourceFieldCount": len(expected["fields"]),
            "thirdBackendStatus": status,
            "unityImportAllowed": False,
        })
    counts=collections.Counter(x["thirdBackendStatus"] for x in rows)
    return {
        "schemaVersion": 1,
        "classification": "REF04_THIRD_BACKEND_SOURCE_LAYOUTGROUP_RECHECK_READ_ONLY",
        "sourceSerializedFile": source_file,
        "sourceBackend": "AssetStudio",
        "thirdBackend": "AssetsTools",
        "exactSourcePair": source_proof,
        "ref04LayoutGroupComponents": len(rows),
        "sourceLayoutFieldNamesExamined": sum(x["sourceFieldCount"] for x in rows),
        "statusCounts": dict(sorted(counts.items())),
        "sourceFieldValuesPublished": False,
        "unityImportAllowed": False,
        "runtimeLayoutProven": False,
        "limitation": "A third strict parse and source-field comparison does "
                      "not prove Unity runtime layout, safe-area parent or "
                      "text state. No previously blocked LayoutGroup field "
                      "is automatically added to an Editor Prefab.",
        "layoutGroups": rows,
    }


def execute(root=ROOT, *, xapk=None, unitypy=None):
    if unitypy is None:
        import UnityPy as unitypy
    folder = root / "output"
    studio = json.loads((folder/"phase3b-assetstudio.json").read_text(encoding="utf-8"))
    review = json.loads((folder/"single-backend-layout-review.json").read_text(encoding="utf-8"))
    graph = json.loads((folder/"original-unity-graph.json").read_text(encoding="utf-8"))
    plan, origin_file = source_plan(studio, review, graph)
    version = recovery.exact_unity_version(studio["scenes"])
    xapk = xapk or next(iter(sorted(root.glob("*.xapk"))), None)
    if xapk is None:
        raise FileNotFoundError("Original canonical XAPK unavailable")
    generator, proof = recovery.source_generator(xapk,version,backend="AssetsTools")
    scenes = json.loads((root/"unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json")
                       .read_text(encoding="utf-8"))["scenes"]
    scene = next(x for x in scenes if x["id"]==SCENE)
    if scene["source"] != origin_file:
        raise ValueError("REF04 serialized source bundle differs")
    samples = {}
    class Interceptor:
        def load(self, buffer):
            env = unitypy.load(buffer)
            groups = collections.defaultdict(dict)
            for reader in env.objects:
                groups[id(reader.assets_file)][int(reader.path_id)] = reader
            try:
                original=layout.choose_serialized_file(scene,groups)
            except ValueError:
                return env
            if samples:
                raise ValueError("Ambiguous repeated original REF04 bundle")
            for cid, expected in sorted(plan.items()):
                reader=original.get(cid)
                if (reader is None or reader.type.name!="MonoBehaviour" or
                    hashlib.sha256(reader.get_raw_data()).hexdigest() !=
                        expected["binaryProof"]["rawObjectSha256"]):
                    raise ValueError("Original LayoutGroup PathID/raw object bytes changed")
                try:
                    fields, record=recovery.verified_fields(
                        reader,expected,generator,use_unitypy_native_header=True)
                    samples[cid]=("DECODED",fields,record)
                except recovery.RecoveryBlocked as err:
                    samples[cid]=("BLOCKED_"+err.code,None,None)
            return env

    layout.build(root=root,xapk=xapk,unitypy=Interceptor())
    report=classify(studio,review,graph,samples,{
        "generator":proof["generator"],"backend":proof["backend"],
        "gameUnityVersion":proof["gameUnityVersion"],
        "librarySha256":proof["library"]["sha256"],
        "metadataSha256":proof["metadata"]["sha256"],
    })
    path=folder/"ref04-layout-third-backend-evidence.json"
    tmp=path.with_suffix(".tmp")
    tmp.write_text(json.dumps(report,ensure_ascii=False,indent=2)+"\n",
                   encoding="utf-8")
    tmp.replace(path)
    print(json.dumps({
        "classification":report["classification"],
        "ref04LayoutGroupComponents":report["ref04LayoutGroupComponents"],
        "sourceLayoutFieldNamesExamined":report["sourceLayoutFieldNamesExamined"],
        "statusCounts":report["statusCounts"],
        "unityImportAllowed":False,
        "unityAssetsChanged":False,
    },sort_keys=True))
    return report


if __name__=="__main__":
    p=argparse.ArgumentParser(description=__doc__)
    p.add_argument("--root",type=Path,default=ROOT)
    args=p.parse_args()
    execute(args.root.resolve())
