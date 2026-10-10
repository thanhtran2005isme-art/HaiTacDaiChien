"""Fail-closed REF04 LayoutGroup schema forensics: source only, no Unity edits."""
import hashlib
import importlib
from pathlib import Path
import sys
import unittest
from types import SimpleNamespace
from unittest.mock import patch

ROOT = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))
tool = importlib.import_module("probe_ref04_layout_schema_forensics")


def node(name, kind="int", children=(), byte_size=4):
    return SimpleNamespace(m_Name=name, m_Type=kind, m_ByteSize=byte_size,
                           m_MetaFlag=0, m_Children=list(children))


def fake_plan():
    return {
        n: {"gameObjectId": 123, "rectTransformId": 456,
            "className": "UnityEngine.UI.HorizontalLayoutGroup",
            "binaryProof": {"rawObjectSha256": "a" * 64},
            "fields": {"field" + str(i): i for i in range(7)}}
        for n in range(1000, 1024)
    }


class FakeGenerator:
    def __init__(self, root, name):
        self.root = root
        self.name = name

    def get_nodes_up(self, assembly, classname):
        return self.root


class Ref04LayoutSchemaForensics(unittest.TestCase):
    def test_schema_preserves_structural_order_and_first_difference(self):
        a = node("root", "MonoBehaviour", [
            node("m_GameObject", "PPtr"), node("m_Spacing", "float")])
        b = node("root", "MonoBehaviour", [
            node("m_GameObject", "PPtr"), node("m_Padding", "RectOffset")])
        rows_a = tool.schema_entries(a)
        rows_b = tool.schema_entries(b)
        self.assertEqual(len(rows_a), 3)
        self.assertEqual(rows_a[2]["path"], "root/1:m_Spacing")
        self.assertNotEqual(tool.summarize_schema(rows_a)["sha256"],
                            tool.summarize_schema(rows_b)["sha256"])
        self.assertEqual(tool.first_schema_difference(rows_a, rows_b)["index"], 2)
        self.assertIsNone(tool.first_schema_difference(rows_a, rows_a))

    def test_empty_or_invalid_schema_is_rejected(self):
        with self.assertRaisesRegex(ValueError, "Empty"):
            tool.schema_entries(node("root", "MonoBehaviour"))
        malformed = node("root", "MonoBehaviour", [node("m_Spacing")])
        malformed.m_Children[0].m_Children = None
        with self.assertRaisesRegex(ValueError, "child list absent"):
            tool.schema_entries(malformed)

    def test_exact_original_raw_sha_is_required(self):
        reader = SimpleNamespace(
            type=SimpleNamespace(name="MonoBehaviour"),
            get_raw_data=lambda: b"actual original bytes")
        row = {"binaryProof": {"rawObjectSha256": "a" * 64}}
        with self.assertRaisesRegex(ValueError, "original object changed"):
            tool.inspect_object(reader, row, {"AssetStudio": None,
                                              "AssetRipper": None})

    def test_schema_match_does_not_prove_layout_fields_or_runtime(self):
        raw = b"binary source layout"
        digest = hashlib.sha256(raw).hexdigest()
        tree = node("root", "MonoBehaviour", [
            node("m_GameObject", "PPtr"), node("m_Padding", "RectOffset")])
        reader = SimpleNamespace(
            type=SimpleNamespace(name="MonoBehaviour"), get_raw_data=lambda: raw)
        row = {"binaryProof": {"rawObjectSha256": digest},
               "assembly": "Assembly-CSharp",
               "className": "UnityEngine.UI.HorizontalLayoutGroup"}
        generators = {
            "AssetStudio": FakeGenerator(tree, "AssetStudio"),
            "AssetRipper": FakeGenerator(tree, "AssetRipper"),
        }

        def probe(_reader, _row, generator, **kwargs):
            if generator.name == "AssetRipper":
                raise tool.recovery.RecoveryBlocked(
                    "cannot parse original", code="STRICT_PARSE_ValueError")
            return ({"m_Spacing": 12.0}, {
                "rawObjectSha256": digest,
                "nativeHeaderMethod": "UNITYPY_EXACT_SOURCE_UNITY_VERSION",
                "strictObjectSizeChecked": True,
                "exactSourcePointerChecked": True,
            })

        with patch.object(tool.recovery, "exact_source_unity_header",
                          return_value=tree), patch.object(
                              tool.recovery, "verified_native_header_root",
                              side_effect=lambda root, native: root), patch.object(
                                  tool.recovery, "verified_fields", side_effect=probe):
            result = tool.inspect_object(reader, row, generators)
        self.assertEqual(result["schemaComparison"],
                         "SCHEMAS_IDENTICAL_NOT_FIELD_PROOF")
        self.assertEqual(result["fieldAgreement"],
                         "BLOCKED_INDEPENDENT_STRICT_PARSE")
        self.assertEqual(result["backendResults"]["AssetRipper"]["strictParseStatus"],
                         "BLOCKED_STRICT_PARSE_ValueError")
        self.assertFalse(result["unityImportAllowed"])
        self.assertFalse(result["runtimeLayoutProven"])

    def test_exact_24_source_ids_and_all_168_fields_required(self):
        plan = fake_plan()
        observed = {cid: {
            "schemaComparison": "BLOCKED_SCHEMA_COMPARISON",
            "fieldAgreement": "BLOCKED_INDEPENDENT_STRICT_PARSE",
            "unityImportAllowed": False,
            "runtimeLayoutProven": False} for cid in plan}
        report = tool.build_report(plan, observed, "exact-source", {"pair": "sha"})
        self.assertEqual(report["layoutGroupsInspected"], 24)
        self.assertEqual(report["sourceFieldValuesStillBlockedFromUnity"], 168)
        self.assertFalse(report["unityImportAllowed"])
        self.assertFalse(report["runtimeAlignmentProven"])
        self.assertFalse(report["originalUiAssetsChanged"])
        observed.pop(1000)
        with self.assertRaisesRegex(ValueError, "All 24"):
            tool.build_report(plan, observed, "exact-source", {})

    def test_forensics_can_never_promote_single_backend_values(self):
        plan = fake_plan()
        observed = {cid: {
            "schemaComparison": "SCHEMAS_IDENTICAL_NOT_FIELD_PROOF",
            "fieldAgreement": "STRICT_SOURCE_FIELD_VALUES_AGREE_REVIEW_ONLY",
            "unityImportAllowed": False,
            "runtimeLayoutProven": False} for cid in plan}
        observed[1000]["unityImportAllowed"] = True
        with self.assertRaisesRegex(ValueError, "Cannot promote"):
            tool.build_report(plan, observed, "exact-source", {})


if __name__ == "__main__":
    unittest.main()
