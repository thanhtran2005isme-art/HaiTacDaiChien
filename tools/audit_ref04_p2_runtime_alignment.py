#!/usr/bin/env python3
"""P2 original REF04 Canvas / SafeArea / PanelHome2 evidence, NO runtime guesses.

Source hierarchy and MonoScript ownership come from original XAPK source
inventory, while method names/definition tokens come from exact original
IL2CPP v31 metadata. Method metadata does NOT identify executable native code;
without independently identified ARM64 method bodies and device state, no
runtime UI formula can be claimed. This tool writes only gitignored reports.
"""
from __future__ import annotations

import argparse
import collections
import json
from pathlib import Path

import recover_managed_ui_fields as source
import ref04_il2cpp_method_index_v31 as methods

ROOT = Path(__file__).resolve().parents[1]
SCENE = "REF04-home-crew"
STEP1_CLASS = "REF04_ALL_SOURCE_CANDIDATE_UI_COMPONENT_INVENTORY_READ_ONLY"
STEP2_CLASS = "REF04_SOURCE_LAYOUT_CANVAS_TEXT_STEP2_READ_ONLY"
P1_CLASS = "REF04_LAYOUTGROUP_SCHEMA_FORENSICS_READ_ONLY"
METHOD_CLASS = "REF04_IL2CPP_V31_SOURCE_METHOD_INDEX_NO_CODE_MAPPING"
P2_CLASS = "REF04_P2_CANVAS_SAFEAREA_PANELHOME2_SOURCE_TRACE_NO_RUNTIME_FORMULA"


def family(component):
    cls = component.get("monoScriptClass") or component.get("nativeKind") or ""
    if cls == "Canvas":
        return "Canvas"
    if cls == "UnityEngine.UI.CanvasScaler":
        return "CanvasScaler"
    if cls == "SafeAreaAdapter":
        return "SafeAreaAdapter"
    if cls.startswith("PanelHome2"):
        return "PanelHome2"
    return None


def trace_path(node, nodes, canvas_transforms):
    """Prove each serialized parent pointer; never infer missing ancestors."""
    path = []
    seen = set()
    current = node
    reason = "BLOCKED_UNKNOWN_SOURCE_PARENT"
    while current is not None:
        tid = current["rectTransformPathId"]
        if tid in seen:
            raise ValueError("Cycle in original REF04 RectTransform pointers")
        seen.add(tid)
        path.append(tid)
        parent = current.get("sourceParentPointer")
        if not isinstance(parent, dict):
            reason = "BLOCKED_SOURCE_PARENT_NOT_RECORDED"
            break
        pid, fid = parent.get("pathId"), parent.get("fileId")
        if type(fid) is not int or type(pid) is not int:
            reason = "BLOCKED_SOURCE_PARENT_POINTER_INVALID"
            break
        if fid == 0 and pid == 0:
            reason = "SERIALIZED_ROOT_REACHED_RUNTIME_ROOT_UNPROVEN"
            break
        if fid != 0:
            reason = "BLOCKED_PARENT_IN_EXTERNAL_SERIALIZED_FILE"
            break
        current = nodes.get(pid)
        if current is None:
            reason = "BLOCKED_PARENT_OUTSIDE_CANDIDATE_SUBTREE"
            break
    canvas = next((i for i in path if i in canvas_transforms), None)
    return {"originalRectTransformPathIdsLeafToAncestor": path,
            "originalNearestCanvasRectTransformPathId": canvas,
            "sourceParentTraceStatus": reason,
            "runtimeCanvasOrViewportProven": False}


def verify_component_match(source_component, reported_component, node):
    if (source_component["componentPathId"] != reported_component["componentPathId"] or
        source_component["rectTransformPathId"] != node["rectTransformPathId"] or
        source_component["gameObjectPathId"] != node["gameObjectPathId"] or
        reported_component["gameObjectPathId"] != node["gameObjectPathId"] or
        reported_component["rectTransformPathId"] != node["rectTransformPathId"] or
        source_component.get("rawSourceObjectSha256") !=
            reported_component.get("originalObjectSha256") or
        reported_component.get("canBeAppliedToUnity") is not False or
        reported_component.get("runtimeRulesVerified") is not False):
        raise ValueError("Serialized Canvas/SafeArea source identity or provenance conflicts")


def build(step1, step2, p1, metadata, elf):
    if (step1.get("classification") != STEP1_CLASS or
        step1.get("sceneId") != SCENE or
        len(step1.get("gameObjects", [])) != 503 or
        step1.get("counts", {}).get("serializedComponentRecords") != 1564 or
        step1.get("sourceFieldApplicationAllowed") is not False or
        step2.get("classification") != STEP2_CLASS or
        step2.get("sourceSerializedFile") != step1.get("sourceSerializedFile") or
        step2.get("counts", {}).get("Canvas") != 1 or
        step2.get("counts", {}).get("CanvasScaler") != 1 or
        step2.get("counts", {}).get("SafeArea") != 6 or
        step2.get("independentlyVerifiedSerializedLayoutFieldValues") != 168 or
        step2.get("sourceFieldApplicationAllowed") is not False or
        p1.get("classification") != P1_CLASS or
        p1.get("sourceSerializedFile") != step1.get("sourceSerializedFile") or
        p1.get("sourceFieldsVerifiedByTwoGeneratedSchemas") != 168 or
        p1.get("sourceFieldsMissingIndependentSchemaProof") != 0 or
        p1.get("unityImportAllowed") is not False or
        metadata.get("classification") != METHOD_CLASS or
        metadata.get("metadataVersion") != 31 or
        metadata.get("metadataSha256") !=
            p1.get("exactSourcePair", {}).get("globalMetadataSha256") or
        elf.get("sha256") !=
            p1.get("exactSourcePair", {}).get("libil2cppSha256") or
        elf.get("machine") != "AARCH64_ELF64" or
        elf.get("methodPointerMappingVerified") is not False or
        metadata.get("methodCodeAddressesResolved") != 0 or
        metadata.get("runtimeFormulaRecovered") is not False):
        raise ValueError("Original REF04/P1/XAPK IL2CPP source identity or gate untrusted")

    nodes = {n["rectTransformPathId"]: n for n in step1["gameObjects"]}
    if len(nodes) != 503:
        raise ValueError("Duplicate original RectTransform PathIDs")
    all_source = {}
    for node in nodes.values():
        for c in node["components"]:
            cid = c["componentPathId"]
            if cid in all_source:
                raise ValueError("Original source Component PathID collision")
            if (c["gameObjectPathId"] != node["gameObjectPathId"] or
                    c["rectTransformPathId"] != node["rectTransformPathId"]):
                raise ValueError("Original component owner contradicts source hierarchy")
            all_source[cid] = (c, node)
    if len(all_source) != 1564:
        raise ValueError("Original component inventory incomplete")

    by_category = step2.get("componentsByCategory", {})
    reported = {}
    for category in ("Canvas", "CanvasScaler", "SafeArea"):
        for item in by_category.get(category, []):
            cid = item["componentPathId"]
            if cid in reported or cid not in all_source:
                raise ValueError("Duplicate or unknown Canvas/SafeArea source PathID")
            source_comp, node = all_source[cid]
            if family(source_comp) != ("SafeAreaAdapter" if category == "SafeArea" else category):
                raise ValueError("Original class from XAPK mismatches Step2")
            verify_component_match(source_comp, item, node)
            reported[cid] = (category, item, source_comp, node)
    if (collections.Counter(x[0] for x in reported.values()) !=
            {"Canvas": 1, "CanvasScaler": 1, "SafeArea": 6}):
        raise ValueError("Expected original Canvas/CanvasScaler/6 SafeArea components")

    canvas_transforms = {v[3]["rectTransformPathId"] for v in reported.values()
                         if v[0] == "Canvas"}
    panel_candidates = [(c, n) for c, n in all_source.values()
                        if family(c) == "PanelHome2"]
    records = []
    all_relevant = [(x[0], x[2], x[3], x[1]) for x in reported.values()]
    all_relevant.extend(("PanelHome2", c, n, None) for c, n in panel_candidates)
    for category, comp, node, item in sorted(
            all_relevant, key=lambda x:(x[0], x[1]["componentPathId"])):
        verified_fields = (item.get("verifiedSerializedFields", {}) if item else {})
        if category == "CanvasScaler" and comp["verificationStatus"] != (
                "TWO_BACKEND_SOURCE_VERIFIED_FIELDS"):
            raise ValueError("CanvasScaler original managed source fields not two-backend verified")
        if category == "Canvas" and not item.get("nativeCanvasFieldsExtracted"):
            # Empty field subset can remain blocked, but cannot be claimed proven.
            canvas_field_state = "BLOCKED_NATIVE_CANVAS_FIELDS_UNAVAILABLE"
        else:
            canvas_field_state = "SOURCE_FIELDS_IDENTIFIED_RUNTIME_MUTATIONS_UNKNOWN"
        records.append({
            "category": category, "componentPathId": comp["componentPathId"],
            "gameObjectPathId": comp["gameObjectPathId"],
            "rectTransformPathId": comp["rectTransformPathId"],
            "originalObjectSha256": comp.get("rawSourceObjectSha256"),
            "sourceClass": comp.get("monoScriptClass") or comp["nativeKind"],
            "serializedVerificationStatus": comp["verificationStatus"],
            "verifiedSerializedFieldNames": sorted(verified_fields),
            # Exact source-only values are stored in gitignored local output.
            # These must never be considered runtime viewport/insets or logged.
            "originalSerializedFieldEvidence": (verified_fields
                if category in ("Canvas", "CanvasScaler") else {}),
            "originalNativeCanvasSubset": (item.get("nativeCanvasFieldsExtracted", {})
                if category == "Canvas" and item else {}),
            "sourceFieldStatus": canvas_field_state,
            "ancestry": trace_path(node, nodes, canvas_transforms),
            "unityImportAllowed": False, "runtimeFormulaProven": False,
        })

    # Method names/tokens in original metadata are evidence of DECLARATIONS,
    # not a native function's byte address or actual executed control flow.
    method_types = metadata.get("targetClassDefinitions", [])
    if len({x["typeDefinitionIndex"] for x in method_types}) != len(method_types):
        raise ValueError("Duplicate metadata type-definition ownership")
    for x in method_types:
        if x.get("runtimeExpressionProven") is not False or (
            x.get("nativeMethodAddressResolved") is not False):
            raise ValueError("Metadata-only evidence misreported as decoded native code")
        if any(m.get("methodBodyVerified") is not False or
               m.get("nativeAddress") is not None for m in x["methods"]):
            raise ValueError("Fake native method pointer supplied")
    return {
        "schemaVersion": 1, "classification": P2_CLASS, "sceneId": SCENE,
        "originalSourceSerializedFile": step1["sourceSerializedFile"],
        "originalIL2CPPPair": {
            "metadataSha256": metadata["metadataSha256"],
            "libil2cppSha256": elf["sha256"], "sourceUnityVersion": "2022.3.51f1",
        },
        "counts": {
            "Canvas": 1, "CanvasScaler": 1, "SafeAreaAdapter": 6,
            "PanelHome2ComponentsInCandidate": len(panel_candidates),
            "metadataSafeAreaTypes": metadata["safeAreaClassDefinitions"],
            "metadataPanelHome2Types": metadata["panelHome2ClassDefinitions"],
            "metadataTargetMethods": sum(x["methodCount"] for x in method_types),
            "layoutGroupSerializedFieldsTwoSchemaVerified": 168,
        },
        "sourceComponents": records,
        "methodDeclarations": method_types,
        "runtimeBlockers": [
            {"stage":"IL2CPP_NATIVE_METHOD_BODY",
             "status":"BLOCKED_METHOD_ADDRESS_AND_INSTRUCTIONS_NOT_RESOLVED",
             "reason":"Metadata token/method name is not an ELF method code pointer"},
            {"stage":"CANVAS_RUNTIME_ANCESTOR_AND_VIEWPORT",
             "status":"BLOCKED_RUNTIME_SCENE_AND_DEVICE_GEOMETRY_NOT_VERIFIED",
             "reason":"Serialized candidate hierarchy does not prove instantiated ancestor/viewport"},
            {"stage":"SAFEAREA_RUNTIME_INSETS",
             "status":"BLOCKED_IL2CPP_SAFEAREA_FORMULA_AND_DEVICE_INSETS_NOT_VERIFIED",
             "reason":"No verified ARM64 function body and original device Screen.safeArea"},
            {"stage":"PANELHOME2_RUNTIME_MUTATIONS",
             "status":"BLOCKED_DYNAMIC_LAYOUT_CONTROL_FLOW_NOT_VERIFIED",
             "reason":"Method names do not prove calls, branch conditions or field writes"},
        ],
        "nativeFunctionPointerEvidence": None,
        "runtimeAlignmentFormula": None, "runtimeAlignmentProven": False,
        "runtimeCanvasViewportProven": False,
        "sourceFieldApplicationAllowed": False,
        "unityAssetsChanged": False, "charactersOrSpineChanged": False,
    }


def execute(root=ROOT, xapk=None):
    folder = root / "output"
    data = [json.loads((folder / p).read_text(encoding="utf-8")) for p in
            ("ref04-full-source-inventory.json",
             "ref04-step2-layout-canvas-text.json",
             "ref04-layout-schema-forensics.json")]
    xapk = xapk or next(iter(sorted(root.glob("*.xapk"))), None)
    if xapk is None:
        raise FileNotFoundError("Canonical original XAPK missing")
    found = source.read_source_pair(xapk)
    metadata = methods.inspect(found["metadata"][0])
    elf = methods.check_library_elf(found["library"][0])
    result = build(*data, metadata, elf)
    dest = folder / "ref04-p2-canvas-il2cpp-runtime-source.json"
    tmp = dest.with_suffix(".tmp")
    tmp.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n",
                   encoding="utf-8")
    tmp.replace(dest)
    md = [
        "# REF04 P2 — source Canvas/SafeArea/PanelHome2 runtime provenance",
        "",
        "**SOURCE-ONLY; no claimed runtime formula; no Unity asset changed.**",
        "",
        "| Original source category | Count |",
        "|---|---:|",
    ]
    md.extend(f"| {name} | {count} |" for name, count in result["counts"].items())
    md += ["", "## Blocked runtime questions"]
    md.extend(f"- **{x['stage']}** — {x['status']}: {x['reason']}"
              for x in result["runtimeBlockers"])
    md += ["", "Metadata method declarations are NOT ARM64 method bodies.",
           "Safe area, device screen, runtime Canvas and UI coordinates remain unverified."]
    temp = dest.with_name(dest.stem + ".md.tmp")
    temp.write_text("\n".join(md) + "\n", encoding="utf-8")
    temp.replace(dest.with_suffix(".md"))
    print(json.dumps({"classification":result["classification"],
                      "counts":result["counts"],
                      "sourceMethodBodiesVerified":0,
                      "runtimeAlignmentProven":False,
                      "unityAssetsChanged":False},sort_keys=True))
    return result


if __name__ == "__main__":
    ap = argparse.ArgumentParser(description=__doc__)
    ap.add_argument("--root", type=Path, default=ROOT)
    args = ap.parse_args()
    execute(args.root.resolve())
