"""P5 end-to-end integrity tests with deliberately synthetic reports.

Fixture PASS is NOT an XAPK run. Linux source workflow must regenerate all
original reports from the same XAPK and execute the same P5 gate.
"""
from __future__ import annotations

import copy
import hashlib
import importlib
from pathlib import Path
import sys
import unittest

ROOT = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))
sys.path.insert(0, str(ROOT / "tests"))

p5 = importlib.import_module("audit_ref04_p5_source_integrity")
p2_fixture = importlib.import_module("test_ref04_p2_runtime_alignment").fixture
p2_module = importlib.import_module("audit_ref04_p2_runtime_alignment")
p4_fixture = importlib.import_module("test_ref04_p4_text_logic").fixture
p4_module = importlib.import_module("audit_ref04_p4_text_logic")
SHA = "a"*64
SHA_FIELD = hashlib.sha256(b"strict field bytes").hexdigest()


def synthetic_reports():
    step1, step2, p1_stub, methods, elf = p2_fixture()
    nodes = step1["gameObjects"]
    p3, fonts, localizers = p4_fixture()

    def replace_component(node, cid, klass):
        old = next(item for item in node["components"]
                   if item["nativeKind"] == "CanvasRenderer")
        old.update(componentPathId=cid, nativeKind="MonoBehaviour",
                   monoScriptClass=klass)
        return old

    for i, txt in enumerate(p3["originalTextSourceEvidence"]):
        original = nodes[20+i]
        replace_component(original, txt["componentPathId"], "UnityEngine.UI.Text")
        txt["gameObjectPathId"] = original["gameObjectPathId"]
        txt["rectTransformPathId"] = original["rectTransformPathId"]

    for i, item in enumerate(p3["sourceLocalizationComponentsEvidence"]):
        node = nodes[20+i] if i < 50 else nodes[90+i-50]
        replace_component(node, item["componentPathId"], item["sourceClass"])
        item["gameObjectPathId"] = node["gameObjectPathId"]
        item["rectTransformPathId"] = node["rectTransformPathId"]
        localizers["localizers"][i]["gameObjectPathId"] = node["gameObjectPathId"]
        localizers["localizers"][i]["rectTransformPathId"] = node["rectTransformPathId"]

    for txt in p3["originalTextSourceEvidence"]:
        idx = txt["componentPathId"]-2000
        txt["colocatedLocalizationComponentPathIds"] = (
            [700+idx] if idx < 50 else [])
        txt["localizationConnection"] = (
            "SAME_ORIGINAL_GAMEOBJECT_ONLY_NO_BINDING_PROOF" if idx < 50 else
            "NO_LOCALIZER_ON_ORIGINAL_GAMEOBJECT")
    p3["originalSourceSerializedFile"] = "original-file"
    fonts["originalSourceSerializedFile"] = "original-file"
    localizers["originalSourceSerializedFile"] = "original-file"
    step2["sourceRuntimeCanvasOrViewportProven"] = False

    source_txt = []
    for txt in p3["originalTextSourceEvidence"]:
        source_txt.append({
            "componentPathId": txt["componentPathId"],
            "originalObjectSha256": txt["originalObjectSha256"],
            "textSourceFieldsTwoBackendsAgreed": copy.deepcopy(
                txt["originalTextSourceValues"])})
    step2["componentsByCategory"]["Text"] = source_txt
    step2["counts"]["Text"] = 62

    fields = [f"m_StrictField{j}" for j in range(7)]
    spans = {
        field: {"offset": j*16, "length": 8,
                "rawFieldBytesSha256": SHA_FIELD}
        for j, field in enumerate(fields)
    }
    layout_rows = []
    for i in range(24):
        original = nodes[120+i]
        comp = replace_component(
            original, 4000+i, "UnityEngine.UI.VerticalLayoutGroup")
        check = {
            "status": p5.LAYOUT_STATUS,
            "sourceFieldsIndependentlyVerified": len(fields),
            "independentFieldNames": fields,
            "originalSerializedFieldValuesPublished": False,
            "unityImportAllowed": False, "runtimeLayoutProven": False,
            "backendEvidence": {
                b: {
                    "status": "FULL_ORIGINAL_OBJECT_REPARSED_SOURCE_IDENTICAL",
                    "sourceObjectSha256": SHA,
                    "sourceFieldByteSpans": copy.deepcopy(spans),
                } for b in p5.BACKENDS},
        }
        layout_rows.append({
            "componentPathId": comp["componentPathId"],
            "gameObjectPathId": original["gameObjectPathId"],
            "rectTransformPathId": original["rectTransformPathId"],
            "sourceClass": comp["monoScriptClass"],
            "sourceObjectSha256": SHA,
            "sourceExpectedFieldNames": list(fields),
            "sourceFieldCountBlocked": 7,
            "independentSchemaRawSourceCheck": check,
        })

    step1["counts"]["serializedComponentRecords"] = 1564
    layout = {
        "classification": "REF04_LAYOUTGROUP_SCHEMA_FORENSICS_READ_ONLY",
        "sourceSerializedFile": "original-file",
        "exactSourcePair": {
            "globalMetadataSha256": "b"*64, "libil2cppSha256": "c"*64},
        "layoutGroupsInspected": 24,
        "sourceFieldsVerifiedByTwoGeneratedSchemas": 168,
        "sourceFieldsMissingIndependentSchemaProof": 0,
        "sourceFieldValuesStillBlockedFromUnity": 168,
        "unityImportAllowed": False, "runtimeAlignmentProven": False,
        "originalUiAssetsChanged": False,
        "layoutGroups": layout_rows,
    }

    report_p2 = p2_module.build(step1, step2, p1_stub, methods, elf)
    report_p4 = p4_module.build(p3, fonts, localizers)
    return {
        "inventory": step1, "step2": step2, "layout": layout,
        "p2": report_p2, "p3": p3, "font": fonts,
        "localizers": localizers, "p4": report_p4,
    }


class CrossPhaseIntegrity(unittest.TestCase):
    def test_full_synthetic_cross_phase_evidence_239_items_no_runtime(self):
        proof = p5.build(synthetic_reports())
        self.assertEqual(proof["sourceCoverage"], {
            "P1OriginalLayoutGroupFields": 168,
            "P2OriginalCanvasAndRelatedComponents": 9,
            "P2ComponentsWithOriginalRawSha": 9,
            "P2IdentityOnlyComponentsWithoutRawSha": 0,
            "P4OriginalTextComponents": 62,
            "totalSourceProvenanceEntries": 239,
        })
        self.assertEqual(len(proof["fieldProvenance"]), 168)
        self.assertEqual(len(proof["textProvenance"]), 62)
        self.assertIsNone(proof["runtimeCoordinates"])
        self.assertIsNone(proof["runtimeCanvasScale"])
        self.assertIsNone(proof["runtimeTextPositions"])
        self.assertFalse(proof["assumedPreviewResolution1600x900"])
        self.assertFalse(proof["pixelPerfectUiProven"])
        self.assertFalse(proof["unityImportAllowed"])

    def test_layout_sha_owner_span_and_independent_backend_conflicts_fail(self):
        for category in ("sha", "owner", "span", "backend"):
            docs = synthetic_reports()
            row = docs["layout"]["layoutGroups"][0]
            if category == "sha":
                row["sourceObjectSha256"] = "f"*64
            elif category == "owner":
                row["gameObjectPathId"] = -1
            elif category == "span":
                row["independentSchemaRawSourceCheck"]["backendEvidence"][
                    "AssetRipper"]["sourceFieldByteSpans"]["m_StrictField0"][
                        "rawFieldBytesSha256"] = "e"*64
            else:
                del row["independentSchemaRawSourceCheck"]["backendEvidence"]["AssetStudio"]
            with self.subTest(category=category):
                with self.assertRaisesRegex(ValueError, "P5 BLOCKED"):
                    p5.build(docs)

    def test_canvas_parent_and_source_binary_pair_conflicts_fail(self):
        docs = synthetic_reports()
        docs["p2"]["sourceComponents"][0]["ancestry"][
            "originalRectTransformPathIdsLeafToAncestor"] = [123456]
        with self.assertRaisesRegex(ValueError, "P5 BLOCKED"):
            p5.build(docs)
        docs = synthetic_reports()
        docs["p2"]["originalIL2CPPPair"]["metadataSha256"] = "e"*64
        with self.assertRaisesRegex(ValueError, "P5 BLOCKED"):
            p5.build(docs)

    def test_p2_missing_raw_sha_only_allows_id_and_parent_no_field_values(self):
        docs = synthetic_reports()
        panel = next(x for x in docs["p2"]["sourceComponents"]
                     if x["category"] == "PanelHome2")
        cid = panel["componentPathId"]
        original = next(c for node in docs["inventory"]["gameObjects"]
                        for c in node["components"] if c["componentPathId"] == cid)
        panel["originalObjectSha256"] = None
        original["rawSourceObjectSha256"] = None
        # This cannot be classified as SHA-verified even when owner and
        # original serialized parent pointers still agree.
        proof = p5.build(docs)
        self.assertEqual(proof["sourceCoverage"][
            "P2IdentityOnlyComponentsWithoutRawSha"], 1)
        self.assertEqual(proof["sourceCoverage"][
            "P2ComponentsWithOriginalRawSha"], 8)
        entry = next(x for x in proof["componentProvenance"]
                     if x["componentPathId"] == cid)
        self.assertIsNone(entry["originalObjectSha256"])
        self.assertEqual(entry["originalObjectByteProvenanceStatus"],
                         "BLOCKED_RAW_OBJECT_SHA_UNAVAILABLE_IDENTITY_ONLY")
        self.assertFalse(entry["originalSerializedSourceValueProofAllowed"])
        # Forging a serialized value with no original object hash is forbidden.
        panel["originalSerializedFieldEvidence"] = {"m_Position": 1600}
        with self.assertRaisesRegex(ValueError, "raw object SHA missing"):
            p5.build(docs)
        docs = synthetic_reports()
        panel = next(x for x in docs["p2"]["sourceComponents"]
                     if x["category"] == "PanelHome2")
        panel["originalObjectSha256"] = None
        with self.assertRaisesRegex(ValueError, "bytes SHA conflict"):
            p5.build(docs)

    def test_62_text_source_stale_and_forged_font_link_fail(self):
        docs = synthetic_reports()
        docs["p4"]["sourceTextRows"][0]["originalFontRawObjectSha256"] = "e"*64
        with self.assertRaisesRegex(ValueError, "P5 BLOCKED"):
            p5.build(docs)
        docs = synthetic_reports()
        # At least one other component at node 20 may be the modified Text;
        # deliberately corrupt the exact one, irrespective of ordering.
        text_id = docs["p4"]["sourceTextRows"][0]["textComponentPathId"]
        original = next(c for c in docs["inventory"]["gameObjects"][20]["components"]
                        if c["componentPathId"] == text_id)
        original["rawSourceObjectSha256"] = "f"*64
        with self.assertRaisesRegex(ValueError, "P5 BLOCKED"):
            p5.build(docs)

    def test_localizer_wrong_owner_fails_even_if_reports_agree(self):
        docs = synthetic_reports()
        first = docs["p3"]["sourceLocalizationComponentsEvidence"][0]
        first["gameObjectPathId"] = 12345
        docs["localizers"]["localizers"][0]["gameObjectPathId"] = 12345
        docs["p3"]["originalTextSourceEvidence"][0][
            "colocatedLocalizationComponentPathIds"] = []
        docs["p3"]["originalTextSourceEvidence"][0][
            "localizationConnection"] = "NO_LOCALIZER_ON_ORIGINAL_GAMEOBJECT"
        docs["p3"]["originalTextSameGameObjectLocalizationCandidates"] = 49
        # Recomputing P4 cannot conceal mismatch to serialized source GO.
        docs["p4"] = p4_module.build(
            docs["p3"], docs["font"], docs["localizers"])
        with self.assertRaisesRegex(ValueError, "P5 BLOCKED"):
            p5.build(docs)

    def test_fabricated_runtime_coordinates_and_text_in_any_report_fail(self):
        docs = synthetic_reports()
        docs["p2"]["sourceComponents"][0]["runtimeCoordinates"] = [1600,900]
        with self.assertRaisesRegex(ValueError, "invented runtime"):
            p5.build(docs)
        docs = synthetic_reports()
        docs["p4"]["sourceTextRows"][0]["runtimeText"] = "guessed text"
        with self.assertRaisesRegex(ValueError, "invented runtime"):
            p5.build(docs)
        docs = synthetic_reports()
        docs["layout"]["layoutGroups"][0]["screenSpaceCoordinates"] = [100, 30]
        with self.assertRaisesRegex(ValueError, "invented runtime"):
            p5.build(docs)

    def test_missing_step2_or_p4_refused(self):
        docs = synthetic_reports()
        del docs["step2"]
        with self.assertRaisesRegex(ValueError, "incomplete"):
            p5.build(docs)
        docs = synthetic_reports()
        docs["p4"]["runtimeTextLogicRecovered"] = True
        with self.assertRaisesRegex(ValueError, "P5 BLOCKED"):
            p5.build(docs)


if __name__ == "__main__":
    unittest.main()
