"""Pure unit tests for geometry, safe SVG output and unresolved classification."""
import collections
import importlib.util
import tempfile
import unittest
from pathlib import Path

root = Path(__file__).resolve().parents[1]
spec = importlib.util.spec_from_file_location("ui_preview", root / "tools" / "ui_preview.py")
preview = importlib.util.module_from_spec(spec)
spec.loader.exec_module(preview)

class LayoutTests(unittest.TestCase):
    def test_centered_fixed_rect(self):
        row = dict(anchor_min=".5,.5", anchor_max=".5,.5", pivot=".5,.5",
                   size_delta="200,100", anchored_position="0,0")
        self.assertEqual(preview.geometry((0, 0, 1600, 900), row),
                         (700, 400, 200, 100))
    def test_stretched_rect(self):
        row = dict(anchor_min="0,0", anchor_max="1,1", pivot=".5,.5",
                   size_delta="-20,-40", anchored_position="0,0")
        self.assertEqual(preview.geometry((0, 0, 1600, 900), row),
                         (10, 20, 1580, 860))
    def test_negative_rect_is_not_rendered(self):
        self.assertEqual(preview.geometry((0,0,50,40),
             dict(anchor_min="0,0", anchor_max="0,0", pivot="0,0",
                  size_delta="-20,-40", anchored_position="0,0"))[2:], (-20,-40))
    def test_cycle_protection(self):
        output = preview.descendants(1, {1:[2], 2:[1]}, 10)
        self.assertEqual(output, [1,2])
    def test_unresolved_not_misclassified(self):
        grouped = {"f":{1:{"path":"/Canvas/Icon", "active":"0"}}}
        paths = {("f",20):"/Canvas/Icon"}
        kinds = {("f",20):{"ui_type":"Image","gameobject":"Icon"},
                 ("f",21):{"ui_type":"Button","gameobject":"Btn"}}
        unresolved = preview.missing_images(grouped, paths, kinds, {})
        self.assertEqual(len(unresolved),1)
        self.assertEqual(unresolved[0]["finding"],"no_static_sprite_pptr_found")
    def test_svg_escapes_gameobject_name(self):
        row = {"name":"UI<&", "parent_transform_id":"1", "path":"/Root/UI<&",
               "anchor_min":".5,.5", "anchor_max":".5,.5", "pivot":".5,.5",
               "size_delta":"220,80", "anchored_position":"0,0",
               "scale":"1,1", "rotation_z":"0", "sibling_index":"0"}
        root_row = {"name":"Root", "parent_transform_id":"0", "path":"/Root"}
        with tempfile.TemporaryDirectory() as directory:
            path = Path(directory)/"preview.svg"
            preview.draw_svg(("f",1,"Root",2), {1:root_row,2:row},
                {"/Root/UI<&":{"Button"}}, {}, {}, path)
            data = path.read_text()
            self.assertIn("UI&lt;&amp;",data)
            self.assertNotIn("UI<&",data)

if __name__ == "__main__":
    unittest.main()
