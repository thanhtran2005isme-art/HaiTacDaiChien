#!/usr/bin/env python3
"""P5 REF04 integrity gate: every audited source value must have provenance.

No estimated screen coordinates, assumed Canvas resolution or runtime layout
formula may be substituted for serialized XAPK fields. Read-only; all detailed
PathIDs, byte spans and hashes remain inside ignored output/.
"""
from __future__ import annotations

import argparse
from collections import Counter
import hashlib
import json
from pathlib import Path

import audit_ref04_p4_text_logic as p4
import ref04_p2_canvas_graph as canvas_graph

ROOT = Path(__file__).resolve().parents[1]
KIND = "REF04_P5_CROSS_PHASE_SOURCE_PROVENANCE_NO_RUNTIME_COORDINATES"
SOURCE = "REF04-home-crew"
LAYOUT_STATUS = "TWO_SOURCE_SCHEMAS_RAW_FIELDS_AND_OFFSETS_AGREE_NOT_IMPORTED"
BACKENDS = ("AssetStudio", "AssetRipper")
INPUTS = {
    "inventory": "ref04-full-source-inventory.json",
    "step2": "ref04-step2-layout-canvas-text.json",
    "layout": "ref04-layout-schema-forensics.json",
    "p2": "ref04-p2-canvas-il2cpp-runtime-source.json",
    "p3": "ref04-p3-text-font-localization-source.json",
    "font": "ref04-p3-original-font-object-provenance.json",
    "localizers": "ref04-p3-localizer-term-binary-probe.json",
    "p4": "ref04-p4-62-text-logic-source-evidence.json",
}

RUNTIME_VALUE_KEYS = frozenset((
    "runtimeCoordinates", "screenSpaceCoordinates", "runtimeCanvasScale",
    "runtimeSafeAreaFormula", "runtimeScreenInsets", "runtimeTextPositions",
    "pixelCoordinates", "pixelPerfectCoordinates", "guessedRuntimeRect",
    "runtimeAlignmentFormula", "runtimeCanvasScaleFormula",
    "runtimeSafeAreaPanelHome2Formula", "runtimeText", "runtimeString",
    "runtimeLocale", "runtimeLanguage", "runtimeSelectedFont",
    "runtimeFont", "runtimePlacement", "runtimeViewport",
))

def reject_runtime_coordinates(reports):
    """Reject numerical runtime placement/scaling even in nested source reports."""
    for name, document in reports.items():
        pending, traversed = [document], 0
        while pending:
            item = pending.pop()
            traversed += 1
            ensure(traversed < 2_000_000, "source report nesting/count unbounded")
            if isinstance(item, dict):
                for key, value in item.items():
                    if key in RUNTIME_VALUE_KEYS:
                        ensure(value is None or value is False,
                               "invented runtime coordinate/text value in " + name)
                    if isinstance(value, (list, dict)):
                        pending.append(value)
            elif isinstance(item, list):
                pending.extend(x for x in item if isinstance(x, (list, dict)))

def ensure(condition, message):
    if not condition:
        raise ValueError("P5 BLOCKED: " + message)

def sha(value):
    return isinstance(value, str) and len(value) == 64 and all(
        c in "0123456789abcdef" for c in value)

def source_objects(inventory):
    ensure(inventory.get("classification") ==
           "REF04_ALL_SOURCE_CANDIDATE_UI_COMPONENT_INVENTORY_READ_ONLY" and
           inventory.get("sceneId") == SOURCE and
           inventory.get("sourceFieldApplicationAllowed") is False,
           "original REF04 candidate source identity")
    nodes = inventory.get("gameObjects")
    ensure(isinstance(nodes, list) and len(nodes) == 503 and
           inventory.get("counts", {}).get("serializedComponentRecords") == 1564,
           "503 original RectTransforms / 1564 component inventory")
    graph, components = {}, {}
    for node in nodes:
        tid, gid = node.get("rectTransformPathId"), node.get("gameObjectPathId")
        ensure(type(tid) is int and type(gid) is int and tid not in graph,
               "duplicate/invalid RectTransform owner")
        graph[tid] = node
        for comp in node.get("components", []):
            cid = comp.get("componentPathId")
            ensure(type(cid) is int and cid not in components and
                   comp.get("gameObjectPathId") == gid and
                   comp.get("rectTransformPathId") == tid and
                   sha(comp.get("rawSourceObjectSha256")),
                   "original component ID/owner/bytes SHA")
            components[cid] = comp
    ensure(len(components) == 1564, "incomplete original component hierarchy")
    return graph, components

def parent_chain(node, nodes, canvas_ids):
    path, seen, current = [], set(), node
    reason = None
    while current is not None:
        tid = current["rectTransformPathId"]
        ensure(tid not in seen, "cyclic original parent pointers")
        seen.add(tid)
        path.append(tid)
        ptr = current.get("sourceParentPointer")
        if not isinstance(ptr, dict):
            reason = "BLOCKED_SOURCE_PARENT_NOT_RECORDED"
            break
        fid, pid = ptr.get("fileId"), ptr.get("pathId")
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
    return {
        "originalRectTransformPathIdsLeafToAncestor": path,
        "originalNearestCanvasRectTransformPathId": next(
            (tid for tid in path if tid in canvas_ids), None),
        "sourceParentTraceStatus": reason,
        "runtimeCanvasOrViewportProven": False,
    }

def layout_values(layout, components):
    ensure(layout.get("classification") ==
           "REF04_LAYOUTGROUP_SCHEMA_FORENSICS_READ_ONLY" and
           layout.get("layoutGroupsInspected") == 24 and
           layout.get("sourceFieldsVerifiedByTwoGeneratedSchemas") == 168 and
           layout.get("sourceFieldsMissingIndependentSchemaProof") == 0 and
           layout.get("sourceFieldValuesStillBlockedFromUnity") == 168 and
           layout.get("unityImportAllowed") is False and
           layout.get("runtimeAlignmentProven") is False and
           layout.get("originalUiAssetsChanged") is False,
           "P1 24 LayoutGroup and 168 serialized values not proven")
    rows, ledger, seen = layout.get("layoutGroups"), [], set()
    ensure(isinstance(rows, list) and len(rows) == 24, "P1 field inventory missing")
    for row in rows:
        cid = row.get("componentPathId")
        origin = components.get(cid)
        proof = row.get("independentSchemaRawSourceCheck")
        names = row.get("sourceExpectedFieldNames")
        if (type(cid) is not int or cid in seen or origin is None or
            not sha(row.get("sourceObjectSha256")) or
            row["sourceObjectSha256"] != origin.get("rawSourceObjectSha256") or
            row.get("gameObjectPathId") != origin["gameObjectPathId"] or
            row.get("rectTransformPathId") != origin["rectTransformPathId"] or
            row.get("sourceClass") != origin.get("monoScriptClass") or
            not isinstance(names, list) or not names or
            len(names) != len(set(names)) or
            row.get("sourceFieldCountBlocked") != len(names) or
            not isinstance(proof, dict) or
            proof.get("status") != LAYOUT_STATUS or
            proof.get("sourceFieldsIndependentlyVerified") != len(names) or
            proof.get("independentFieldNames") != sorted(names) or
            proof.get("originalSerializedFieldValuesPublished") is not False or
            proof.get("unityImportAllowed") is not False or
            proof.get("runtimeLayoutProven") is not False):
            raise ValueError("P5 BLOCKED: original LayoutGroup field identity/proof")
        seen.add(cid)
        backends = proof.get("backendEvidence")
        ensure(isinstance(backends, dict) and set(backends) == set(BACKENDS),
               "both original schema decoders mandatory")
        left, right = [], []
        for backend, target in ((BACKENDS[0], left), (BACKENDS[1], right)):
            entry = backends[backend]
            ensure(entry.get("status") == "FULL_ORIGINAL_OBJECT_REPARSED_SOURCE_IDENTICAL" and
                   entry.get("sourceObjectSha256") == row["sourceObjectSha256"] and
                   isinstance(entry.get("sourceFieldByteSpans"), dict) and
                   set(entry["sourceFieldByteSpans"]) == set(names),
                   "LayoutGroup original field spans incomplete or unmatched")
            for field in sorted(names):
                span = entry["sourceFieldByteSpans"][field]
                ensure(isinstance(span, dict) and
                       type(span.get("offset")) is int and span["offset"] >= 0 and
                       type(span.get("length")) is int and span["length"] > 0 and
                       sha(span.get("rawFieldBytesSha256")),
                       "LayoutGroup original source byte offset/length/hash")
                target.append((field, span["offset"], span["length"],
                               span["rawFieldBytesSha256"]))
        ensure(left == right, "two original source decoders disagree on byte spans")
        for field, offset, length, digest in left:
            ledger.append({
                "kind": "ORIGINAL_LAYOUTGROUP_SERIALIZED_FIELD",
                "componentPathId": cid, "originalObjectSha256": row["sourceObjectSha256"],
                "originalFieldName": field, "originalFieldOffset": offset,
                "originalFieldByteLength": length, "originalFieldBytesSha256": digest,
                "proof": LAYOUT_STATUS, "screenCoordinateOrRuntimeValue": None,
                "unityImportAllowed": False,
            })
    ensure(len(ledger) == 168 and len(seen) == 24,
           "P1 field-level source coverage incomplete")
    return ledger

def p2_components(p2, step2, nodes, components):
    ensure(p2.get("classification") ==
           "REF04_P2_CANVAS_SAFEAREA_PANELHOME2_SOURCE_TRACE_NO_RUNTIME_FORMULA" and
           p2.get("sceneId") == SOURCE and
           p2.get("runtimeAlignmentFormula") is None and
           p2.get("runtimeAlignmentProven") is False and
           p2.get("runtimeCanvasViewportProven") is False and
           p2.get("sourceFieldApplicationAllowed") is False and
           p2.get("unityAssetsChanged") is False,
           "P2 original Canvas runtime not proven")
    ensure(step2.get("classification") ==
           "REF04_SOURCE_LAYOUT_CANVAS_TEXT_STEP2_READ_ONLY" and
           step2.get("sourceSerializedFile") == p2.get("originalSourceSerializedFile") and
           step2.get("counts", {}).get("Canvas") == 1 and
           step2.get("counts", {}).get("CanvasScaler") == 1 and
           step2.get("counts", {}).get("SafeArea") == 6 and
           step2.get("sourceFieldApplicationAllowed") is False and
           step2.get("sourceRuntimeCanvasOrViewportProven") is False,
           "Step2 independent Canvas/Scaler source identity")
    source_by_id = {}
    for cat in ("Canvas", "CanvasScaler", "SafeArea"):
        for item in step2.get("componentsByCategory", {}).get(cat, []):
            cid = item.get("componentPathId")
            ensure(type(cid) is int and cid not in source_by_id,
                   "duplicate Step2 original component PathID")
            source_by_id[cid] = item
    rows = p2.get("sourceComponents")
    ensure(isinstance(rows, list), "P2 source component rows absent")
    counts = Counter(row.get("category") for row in rows)
    ensure(all(counts[name] == quantity for name, quantity in
               (("Canvas", 1), ("CanvasScaler", 1), ("SafeArea", 6))) and
           counts.get("PanelHome2", 0) == p2.get("counts", {}).get(
               "PanelHome2ComponentsInCandidate") and
           set(counts) <= {"Canvas", "CanvasScaler", "SafeArea", "PanelHome2"},
           "P2 Canvas/Scaler/SafeArea/PanelHome2 source counts")
    canvas_ids = {row["rectTransformPathId"] for row in rows
                  if row["category"] == "Canvas"}
    seen = set()
    ledger = []
    for row in rows:
        cid = row.get("componentPathId")
        original = components.get(cid)
        ensure(type(cid) is int and cid not in seen and original is not None and
               original["rawSourceObjectSha256"] == row.get("originalObjectSha256") and
               original["gameObjectPathId"] == row.get("gameObjectPathId") and
               original["rectTransformPathId"] == row.get("rectTransformPathId") and
               row.get("ancestry") == parent_chain(
                   nodes[original["rectTransformPathId"]], nodes, canvas_ids) and
               row.get("unityImportAllowed") is False and
               row.get("runtimeFormulaProven") is False,
               "P2 source owner/SHA or RectTransform ancestry not proven")
        check = source_by_id.get(cid)
        if row["category"] != "PanelHome2":
            ensure(check is not None and
                   check.get("originalObjectSha256") == row["originalObjectSha256"] and
                   check.get("gameObjectPathId") == row["gameObjectPathId"] and
                   check.get("rectTransformPathId") == row["rectTransformPathId"] and
                   check.get("verifiedSerializedFields", {}) ==
                       row.get("originalSerializedFieldEvidence", {}) and
                   (row["category"] != "Canvas" or
                    check.get("nativeCanvasFieldsExtracted", {}) ==
                        row.get("originalNativeCanvasSubset", {})),
                   "P2 original Canvas/Scaler serialized values differ from Step2")
        seen.add(cid)
        ledger.append({
            "kind": "ORIGINAL_CANVAS_SAFEAREA_PANEL_COMPONENT",
            "componentPathId": cid, "originalObjectSha256": original["rawSourceObjectSha256"],
            "originalGameObjectPathId": original["gameObjectPathId"],
            "originalRectTransformPathId": original["rectTransformPathId"],
            "sourceCategory": row["category"],
            "parentChainVerifiedAgainstOriginalXapkInventory": True,
            "runtimeCoordinates": None, "runtimeFormula": None,
        })
    rebuilt = canvas_graph.build_graph(rows)
    ensure(rebuilt == p2.get("sourceCanvasGraph"),
           "Canvas graph recomputation differs from original pointer chain")
    return ledger

def check_cross_phase(d):
    inventory, step2, layout, p2, p3, font, localizers, p4_doc = (
        d[k] for k in INPUTS)
    nodes, components = source_objects(inventory)
    reference = inventory["sourceSerializedFile"]
    ensure(all(d[k].get(key) == reference for k, key in (
        ("step2", "sourceSerializedFile"),
        ("layout", "sourceSerializedFile"),
        ("p2", "originalSourceSerializedFile"),
        ("p3", "originalSourceSerializedFile"),
        ("p4", "originalSourceSerializedFile"))),
        "cross-phase original SerializedFile mismatch")
    original_pair = layout.get("exactSourcePair", {})
    expected_meta = original_pair.get("globalMetadataSha256")
    expected_lib = original_pair.get("libil2cppSha256")
    ensure(sha(expected_meta) and sha(expected_lib) and
           p2.get("originalIL2CPPPair", {}).get("metadataSha256") == expected_meta and
           p2.get("originalIL2CPPPair", {}).get("libil2cppSha256") == expected_lib and
           p4_doc.get("originalIL2CPPSha256Pair", {}).get("metadataSha256") == expected_meta and
           p4_doc.get("originalIL2CPPSha256Pair", {}).get("libil2cppSha256") == expected_lib,
           "P1/P2/P4 exact source metadata + libil2cpp pair")
    p1_ledger = layout_values(layout, components)
    p2_ledger = p2_components(p2, step2, nodes, components)
    # P4 is regenerated from original P3/Text/Font/Term reports, never trusted
    # merely because a previously generated JSON said "verified".
    regenerated = p4.build(p3, font, localizers)
    ensure(regenerated == p4_doc, "P4 Text/font/localizer report was changed or stale")
    # Validate original localizer component identity independently of P3/P4
    # grouping. Same-owner co-location remains a source *candidate*, not a
    # verified binding to the displayed runtime Text.
    for item in p3.get("sourceLocalizationComponentsEvidence", []):
        component = components.get(item.get("componentPathId"))
        ensure(component is not None and
               component["rawSourceObjectSha256"] == item.get("originalObjectSha256") and
               component["gameObjectPathId"] == item.get("gameObjectPathId") and
               component["rectTransformPathId"] == item.get("rectTransformPathId") and
               component.get("monoScriptClass") == item.get("sourceClass"),
               "original I2 localization component owner/bytes mismatch")
    step2_text = {item["componentPathId"]: item for item in
                  step2.get("componentsByCategory", {}).get("Text", [])}
    ensure(len(step2_text) == 62 and len(
        step2.get("componentsByCategory", {}).get("Text", [])) == 62,
        "Step2 original Text component inventory incomplete")
    text_ledger = []
    for row in p4_doc["sourceTextRows"]:
        cid = row["textComponentPathId"]
        original = components.get(cid)
        step2_row = step2_text.get(cid)
        ensure(original is not None and
               original.get("monoScriptClass") == "UnityEngine.UI.Text" and
               step2_row is not None and
               step2_row.get("originalObjectSha256") == original["rawSourceObjectSha256"] and
               step2_row.get("textSourceFieldsTwoBackendsAgreed") ==
                   next(item["originalTextSourceValues"] for item in
                        p3["originalTextSourceEvidence"] if item["componentPathId"] == cid) and
               original["rawSourceObjectSha256"] == p3_text_sha(p3, cid) and
               original["gameObjectPathId"] == row["originalGameObjectPathId"] and
               original["rectTransformPathId"] == row["originalRectTransformPathId"],
               "P4 Text component owner/raw original bytes mismatch")
        text_ledger.append({
            "kind": "ORIGINAL_TEXT_SERIALIZED_VALUE_AND_FONT_PTR",
            "componentPathId": cid, "originalObjectSha256": original["rawSourceObjectSha256"],
            "originalTextUtf8Sha256": row["originalSourceTextSha256"],
            "originalTextUtf8ByteLength": row["originalSourceTextUtf8Bytes"],
            "originalFontPointer": row["originalFontPointer"],
            "originalFontRawObjectSha256": row["originalFontRawObjectSha256"],
            "textAndFontSourceVerifiedByP3P4": True,
            "screenCoordinates": None, "runtimeText": None, "runtimeFont": None,
        })
    ensure(len(text_ledger) == 62, "P4 62 Text evidence rows required")
    return p1_ledger, p2_ledger, text_ledger, reference, expected_meta, expected_lib

def p3_text_sha(p3, cid):
    matches = [row.get("originalObjectSha256") for row in
               p3.get("originalTextSourceEvidence", [])
               if row.get("componentPathId") == cid]
    ensure(len(matches) == 1 and sha(matches[0]), "P3 Text component SHA missing/ambiguous")
    return matches[0]

def build(reports):
    ensure(isinstance(reports, dict) and set(reports) == set(INPUTS),
           "P1–P4 source report set incomplete")
    reject_runtime_coordinates(reports)
    p1, p2, text, original, metadata_sha, lib_sha = check_cross_phase(reports)
    return {
        "classification": KIND,
        "sceneId": SOURCE,
        "originalSourceSerializedFile": original,
        "originalMetadataSha256": metadata_sha,
        "originalLibil2cppSha256": lib_sha,
        "sourceCoverage": {
            "P1OriginalLayoutGroupFields": len(p1),
            "P2OriginalCanvasAndRelatedComponents": len(p2),
            "P4OriginalTextComponents": len(text),
            "totalSourceProvenanceEntries": len(p1)+len(p2)+len(text),
        },
        "fieldProvenance": p1,
        "componentProvenance": p2,
        "textProvenance": text,
        "runtimeCoordinates": None,
        "runtimeCanvasScale": None,
        "runtimeSafeAreaFormula": None,
        "runtimeTextPositions": None,
        "assumedPreviewResolution1600x900": False,
        "pixelPerfectUiProven": False,
        "runtimeLayoutProven": False,
        "unityImportAllowed": False,
        "originalUiAssetsChanged": False,
        "scope": "EXACT_SERIALIZED_SOURCE_ONLY_NOT_RUNTIME_OR_VISUAL_FIDELITY",
    }

def execute(root=ROOT):
    folder = root / "output"
    reports = {}
    for name, filename in INPUTS.items():
        path = folder / filename
        ensure(path.is_file() and not path.is_symlink(),
               "required private original XAPK report unavailable: " + name)
        ensure(path.stat().st_size <= 80*1024*1024,
               "untrusted oversized source report: " + name)
        reports[name] = json.loads(path.read_text(encoding="utf-8"))
    result = build(reports)
    out = folder / "ref04-p5-cross-phase-source-integrity.json"
    tmp = out.with_suffix(".tmp")
    tmp.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n",
                   encoding="utf-8")
    tmp.replace(out)
    print(json.dumps({
        "classification": KIND,
        "sourceCoverage": result["sourceCoverage"],
        "runtimeCoordinates": None,
        "runtimeLayoutProven": False,
        "unityImportAllowed": False,
        "originalUiAssetsChanged": False,
    }, sort_keys=True))
    return result

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=ROOT)
    args = parser.parse_args()
    execute(args.root.resolve())
