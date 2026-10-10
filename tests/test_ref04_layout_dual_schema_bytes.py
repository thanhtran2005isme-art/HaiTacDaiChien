"""Strictly source-bound two-generator raw-field proof for REF04 LayoutGroup."""
import hashlib
import importlib
from pathlib import Path
import sys
import unittest
from types import SimpleNamespace

ROOT = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))
sys.path.insert(0, str(ROOT / "tests"))
dual = importlib.import_module("ref04_layout_dual_schema_bytes")
rawfixture = importlib.import_module("test_ref04_layout_raw_parser")


def example():
    binary, schema, fields = rawfixture.source()
    # The two independently generated schemas are represented by separate
    # objects in tests; production obtains them from two source generators.
    _, other, _ = rawfixture.source()
    source = {
        "binaryProof": {"rawObjectSha256": hashlib.sha256(binary).hexdigest()},
        "scriptPointer": {"fileId": 3, "pathId": 123},
        "gameObjectId": 12, "nativeEnabled": True,
        "fields": fields,
    }
    reader = SimpleNamespace(
        type=SimpleNamespace(name="MonoBehaviour"),
        reader=SimpleNamespace(endian="<"),
        get_raw_data=lambda: binary)
    return reader, source, schema, other, fields


class IndependentOriginalLayoutGroupFields(unittest.TestCase):
    def test_two_schemas_with_exact_same_raw_fields_verify_source_only(self):
        reader, row, studio, ripper, fields = example()
        x = dual.strict_raw_schema_agreement(
            reader, row, {"AssetStudio": studio, "AssetRipper": ripper}, fields)
        self.assertEqual(x["status"], dual.SUCCESS)
        self.assertEqual(x["sourceFieldsIndependentlyVerified"], 7)
        self.assertEqual(set(x["independentFieldNames"]), set(fields))
        self.assertEqual(x["backendEvidence"]["AssetStudio"]["sourceFieldByteSpans"],
                         x["backendEvidence"]["AssetRipper"]["sourceFieldByteSpans"])
        self.assertFalse(x["unityImportAllowed"])
        self.assertFalse(x["runtimeLayoutProven"])
        self.assertFalse(x["originalSerializedFieldValuesPublished"])

    def test_same_schema_object_cannot_masquerade_as_two_generators(self):
        reader, row, studio, _, fields = example()
        result = dual.strict_raw_schema_agreement(
            reader, row, {"AssetStudio": studio, "AssetRipper": studio}, fields)
        self.assertEqual(result["status"],
                         "BLOCKED_SHARED_SCHEMA_OBJECT_NOT_INDEPENDENT")
        self.assertEqual(result["sourceFieldsIndependentlyVerified"], 0)

    def test_missing_backend_is_blocked_without_partial_promotion(self):
        reader, row, studio, _, fields = example()
        result = dual.strict_raw_schema_agreement(
            reader, row, {"AssetStudio": studio}, fields)
        self.assertEqual(result["status"], "BLOCKED_INDEPENDENT_SCHEMAS_UNAVAILABLE")
        self.assertEqual(result["sourceFieldsIndependentlyVerified"], 0)

    def test_a_backend_field_offset_conflict_stays_blocked(self):
        reader, row, studio, ripper, fields = example()
        # A schema with a native-header alignment difference must not pass.
        ripper.m_Children[2].m_MetaFlag = 0
        result = dual.strict_raw_schema_agreement(reader, row, {
            "AssetStudio": studio, "AssetRipper": ripper}, fields)
        self.assertNotEqual(result["status"], dual.SUCCESS)
        self.assertEqual(result["sourceFieldsIndependentlyVerified"], 0)

    def test_second_backend_value_conflict_stays_blocked(self):
        reader, row, studio, ripper, fields = example()
        # Interpret m_Spacing as int rather than float with same width.
        ripper.m_Children[4].m_Type = "int"
        result = dual.strict_raw_schema_agreement(reader, row, {
            "AssetStudio": studio, "AssetRipper": ripper}, fields)
        self.assertEqual(result["status"], "BLOCKED_INDEPENDENT_SCHEMA_RAW_PARSE")

    def test_source_pointer_conflict_stays_blocked(self):
        reader, row, studio, ripper, fields = example()
        row["scriptPointer"]["pathId"] = 99999
        result = dual.strict_raw_schema_agreement(reader, row, {
            "AssetStudio": studio, "AssetRipper": ripper}, fields)
        self.assertEqual(result["status"], "BLOCKED_INDEPENDENT_SCHEMA_RAW_PARSE")
        self.assertEqual(result["sourceFieldsIndependentlyVerified"], 0)

    def test_wrong_sha_or_expected_field_list_fails_hard(self):
        reader, row, studio, ripper, fields = example()
        row["binaryProof"]["rawObjectSha256"] = "f" * 64
        with self.assertRaisesRegex(ValueError, "source SHA"):
            dual.strict_raw_schema_agreement(reader, row, {
                "AssetStudio": studio, "AssetRipper": ripper}, fields)
        row["binaryProof"]["rawObjectSha256"] = hashlib.sha256(
            reader.get_raw_data()).hexdigest()
        with self.assertRaisesRegex(ValueError, "field set"):
            dual.strict_raw_schema_agreement(reader, row, {
                "AssetStudio": studio, "AssetRipper": ripper},
                dict(fields, invented=1))

    def test_blocked_and_proved_fields_sum_without_import_permissions(self):
        _, row, _, _, fields = example()
        verified = {
            "sourceFieldCountBlocked": len(fields),
            "sourceExpectedFieldNames": sorted(fields),
            "independentSchemaRawSourceCheck": {
                "status": dual.SUCCESS, "sourceFieldsIndependentlyVerified": 7,
                "independentFieldNames": sorted(fields),
                "unityImportAllowed": False, "runtimeLayoutProven": False}}
        pending = {
            "sourceFieldCountBlocked": len(fields),
            "sourceExpectedFieldNames": sorted(fields),
            "independentSchemaRawSourceCheck": {
                "status": "BLOCKED_INDEPENDENT_SCHEMA_RAW_PARSE",
                "sourceFieldsIndependentlyVerified": 0,
                "independentFieldNames": [],
                "unityImportAllowed": False, "runtimeLayoutProven": False}}
        report = dual.report_totals([verified, pending])
        self.assertEqual(report["sourceFieldsVerifiedByTwoGeneratedSchemas"], 7)
        self.assertEqual(report["sourceFieldsMissingIndependentSchemaProof"], 7)
        self.assertFalse(report["unityImportAllowed"])
        verified["independentSchemaRawSourceCheck"]["independentFieldNames"] = (
            sorted(fields)[:-1] + ["fabricated"])
        with self.assertRaisesRegex(ValueError, "Incomplete independent"):
            dual.report_totals([verified])


if __name__ == "__main__":
    unittest.main()
