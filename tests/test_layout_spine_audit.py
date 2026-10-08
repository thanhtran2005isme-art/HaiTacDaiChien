"""Fixture tests for CanvasScaler and serialized Unity image pointers."""
import importlib.util
import struct
import sys
import unittest
from pathlib import Path
from types import SimpleNamespace as N

sys.path.insert(0,str(Path(__file__).resolve().parents[1]/"tools"))
path=Path(__file__).resolve().parents[1]/"tools"/"layout_spine_audit.py"
spec=importlib.util.spec_from_file_location("layout_spine_audit",path)
audit=importlib.util.module_from_spec(spec)
spec.loader.exec_module(audit)


class TestLayoutSpine(unittest.TestCase):
    def test_offset_must_repeat(self):
        self.assertEqual(audit.selected_offset([{"byte_offset":88,"single_matches":19}]),88)
        self.assertIsNone(audit.selected_offset([{"byte_offset":88,"single_matches":1}]))
        self.assertIsNone(audit.selected_offset([
            {"byte_offset":88,"single_matches":9},
            {"byte_offset":96,"single_matches":9}]))

    def test_missing_sprite_null_distinct_from_runtime(self):
        result=audit.classify_pointer(b"\0"*112,88,"<",1,lambda f,p:(None,"missing"))
        self.assertEqual(result["classification"],"serialized_null_sprite_pointer")
        self.assertNotIn("dynamic",result["classification"])

    def test_external_sprite_resolution(self):
        raw=b"\0"*88+struct.pack("<iq",1,37)+b"\0"*8
        result=audit.classify_pointer(raw,88,"<",1,lambda f,p:(
            N(type=N(name="Sprite")),"external_resolved"))
        self.assertEqual(result["classification"],"sprite_pointer_found")
        self.assertEqual(result["path_id"],37)

    def test_non_sprite_not_mislabelled(self):
        raw=b"\0"*88+struct.pack("<iq",0,37)+b"\0"*8
        result=audit.classify_pointer(raw,88,"<",1,lambda f,p:(
            N(type=N(name="Texture2D")),"local"))
        self.assertEqual(result["classification"],"points_to_non_sprite_object")

    def test_bad_external_and_unknown_endian(self):
        raw=b"\0"*88+struct.pack("<iq",4,37)
        self.assertEqual(audit.classify_pointer(raw,88,"<",1,lambda *_:(None,"x"))["classification"],
                         "invalid_pointer_at_calibrated_offset")
        self.assertEqual(audit.classify_pointer(raw,88,None,1,lambda *_:(None,"x"))["classification"],
                         "unknown_endianness")

    def test_canvas_resolution_requires_explicit_valid_type_tree(self):
        self.assertEqual(audit.parse_reference_resolution({"m_ReferenceResolution":{"x":1600,"y":900}}),
                         {"reference_width":1600.0,"reference_height":900.0,
                          "ui_scale_mode":"","screen_match_mode":"","match_width_or_height":""})
        self.assertEqual(audit.parse_reference_resolution({"m_ReferenceResolution":{"x":0,"y":900}}),{})
        self.assertEqual(audit.parse_reference_resolution({"referenceWidth":1600}),{})


if __name__=="__main__":
    unittest.main()
