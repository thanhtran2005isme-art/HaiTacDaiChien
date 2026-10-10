"""Independent struct-based raw-byte replay does not establish layout runtime."""
import hashlib
import importlib
from pathlib import Path
import struct
import sys
import unittest
from types import SimpleNamespace

ROOT = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))
raw = importlib.import_module("ref04_layout_raw_parser")


def node(name, kind, children=(), meta=0):
    return SimpleNamespace(m_Name=name, m_Type=kind,
                           m_Children=list(children), m_MetaFlag=meta)


def source():
    pointer = lambda name: node(name, "PPtr", [
        node("m_FileID", "int"), node("m_PathID", "SInt64")])
    fields = [
        pointer("m_GameObject"), pointer("m_Script"),
        node("m_Enabled", "bool", meta=raw.ALIGN_FLAG),
        node("m_Padding", "RectOffset", [
            node("m_Left", "int"), node("m_Right", "int"),
            node("m_Top", "int"), node("m_Bottom", "int")]),
        node("m_Spacing", "float"),
        node("m_ChildAlignment", "int"),
        node("m_ChildControlWidth", "bool", meta=raw.ALIGN_FLAG),
        node("m_ChildControlHeight", "bool", meta=raw.ALIGN_FLAG),
        node("m_ChildForceExpandWidth", "bool", meta=raw.ALIGN_FLAG),
        node("m_ChildForceExpandHeight", "bool", meta=raw.ALIGN_FLAG),
    ]
    blob = (
        struct.pack("<iqiq", 0, 12, 3, 123) +
        b"\x01\x00\x00\x00" +
        struct.pack("<iiii", 2, 3, 4, 5) +
        struct.pack("<fi", 1.25, 4) +
        b"\x01\x00\x00\x00" +
        b"\x00\x00\x00\x00" +
        b"\x01\x00\x00\x00" +
        b"\x00\x00\x00\x00"
    )
    expected = {
        "m_Padding": {"m_Left": 2, "m_Right": 3, "m_Top": 4, "m_Bottom": 5},
        "m_Spacing": 1.25, "m_ChildAlignment": 4,
        "m_ChildControlWidth": True, "m_ChildControlHeight": False,
        "m_ChildForceExpandWidth": True, "m_ChildForceExpandHeight": False,
    }
    return blob, node("root", "MonoBehaviour", fields), expected


class RawSourceByteWalk(unittest.TestCase):
    def test_parse_full_source_object_and_record_real_spans(self):
        blob, schema, expected = source()
        report = raw.reparse_strict(blob, schema, expected, endian="<")
        self.assertEqual(report["fullObjectBytes"], len(blob))
        self.assertEqual(report["fullObjectSha256"], hashlib.sha256(blob).hexdigest())
        self.assertEqual(report["nativeHeader"]["m_GameObject"],
                         {"m_FileID": 0, "m_PathID": 12})
        self.assertEqual(report["fieldByteSpans"]["m_Padding"]["offset"], 28)
        self.assertEqual(report["fieldByteSpans"]["m_Spacing"]["offset"], 44)
        self.assertEqual(report["fieldByteSpans"]["m_Spacing"]["length"], 4)
        self.assertEqual(report["fieldByteSpans"]["m_ChildControlWidth"]["length"], 4)
        self.assertEqual(len(report["fieldByteSpans"]), 7)
        self.assertTrue(report["derivedSchemaOnly"])
        self.assertFalse(report["unityImportAllowed"])
        self.assertFalse(report["runtimeLayoutProven"])

    def test_truncated_and_trailing_bytes_both_block(self):
        blob, schema, expected = source()
        with self.assertRaises(raw.RawWalkBlocked):
            raw.reparse_strict(blob[:-1], schema, expected, endian="<")
        with self.assertRaisesRegex(raw.RawWalkBlocked, "exact source object"):
            raw.reparse_strict(blob + b"\0", schema, expected, endian="<")

    def test_conflicting_field_value_and_type_block(self):
        blob, schema, expected = source()
        wrong = dict(expected, m_Spacing=1.5)
        with self.assertRaisesRegex(raw.RawWalkBlocked, "differs"):
            raw.reparse_strict(blob, schema, wrong, endian="<")
        wrong = dict(expected, m_ChildControlWidth=1)
        with self.assertRaisesRegex(raw.RawWalkBlocked, "differs"):
            raw.reparse_strict(blob, schema, wrong, endian="<")

    def test_field_alignment_from_actual_schema_is_not_guessed(self):
        blob, schema, expected = source()
        bad = list(schema.m_Children)
        bad[2] = node("m_Enabled", "bool")
        schema = node("root", "MonoBehaviour", bad)
        with self.assertRaises(raw.RawWalkBlocked):
            raw.reparse_strict(blob, schema, expected, endian="<")

    def test_endian_source_missing_is_blocked(self):
        with self.assertRaisesRegex(raw.RawWalkBlocked, "byte order"):
            raw.byte_order(SimpleNamespace(reader=SimpleNamespace(endian="?")))
        self.assertEqual(raw.byte_order(
            SimpleNamespace(reader=SimpleNamespace(endian=">"))), ">")

    def test_invalid_schema_array_count_and_nesting_are_blocked(self):
        arr = node("vector", "vector", [
            node("Array", "Array", [node("size", "int"),
                                   node("data", "int")])])
        x = raw.Cursor(struct.pack("<i", -1), "<")
        with self.assertRaisesRegex(raw.RawWalkBlocked, "count invalid"):
            x.walk(arr)
        x = raw.Cursor(struct.pack("<i", raw.MAX_ARRAY+1), "<")
        with self.assertRaisesRegex(raw.RawWalkBlocked, "count invalid"):
            x.walk(arr)

    def test_unsupported_schema_does_not_evaluate_fake_data(self):
        blob, schema, expected = source()
        schema.m_Children[3].m_Type = "UNKNOWN_UNVERIFIED_FIELD"
        schema.m_Children[3].m_Children = []
        with self.assertRaisesRegex(raw.RawWalkBlocked, "Unrecognized"):
            raw.reparse_strict(blob, schema, expected, endian="<")


if __name__ == "__main__":
    unittest.main()
