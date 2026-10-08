"""Pure fixture tests for Unity hierarchy, IDs, and sprite metadata."""
import importlib.util
from pathlib import Path
from types import SimpleNamespace as N
import unittest

spec = importlib.util.spec_from_file_location(
    "ui_hierarchy", Path(__file__).resolve().parents[1] / "tools" / "ui_hierarchy.py")
ui = importlib.util.module_from_spec(spec)
spec.loader.exec_module(ui)

def p(path_id, file_id=0):
    return N(m_FileID=file_id, m_PathID=path_id)

class Reader:
    def __init__(self, pid, kind, data):
        self.path_id = pid
        self.type = N(name=kind)
        self.data = data
    def read(self):
        return self.data
    def read_typetree(self):
        return self.data

class UiHierarchyTests(unittest.TestCase):
    def test_pointer_locality(self):
        self.assertEqual(ui.pointer(p(91)), (0, 91))
        self.assertEqual(ui.local_pointer(p(91, 2)), 0)
        self.assertEqual(ui.component_pointer(N(component=p(7))), 7)

    def test_cycle_safe(self):
        nodes = {1: {"go": 10, "parent": 2},
                 2: {"go": 11, "parent": 1}}
        result, cycles = ui.paths_for(nodes, {10: "A", 11: "B"})
        self.assertEqual(len(result), 2)
        self.assertGreater(cycles, 0)

    def test_fixture_canvas_hierarchy_and_sprite_link(self):
        nodes = [
            Reader(10, "GameObject", N(m_Name="MainCanvas", m_IsActive=True,
                                       m_Component=[N(component=p(100)), N(component=p(500))])),
            Reader(11, "GameObject", N(m_Name="Shop", m_IsActive=True,
                                       m_Component=[N(component=p(101)), N(component=p(300))])),
            Reader(100, "RectTransform", N(m_GameObject=p(10), m_Father=p(0))),
            Reader(101, "RectTransform", N(m_GameObject=p(11), m_Father=p(100))),
            Reader(500, "Canvas", N(m_GameObject=p(10), m_SortingOrder=0)),
            Reader(700, "MonoScript", N(m_ClassName="Image")),
            Reader(800, "Sprite", N(m_Name="ShopIcon")),
            Reader(300, "MonoBehaviour", N(m_GameObject=p(11),
                                            m_Script=p(700), m_Sprite=p(800))),
        ]
        part = ui.inspect_file(nodes, "fixture_file", __import__("collections").Counter())
        self.assertEqual(part["metrics"]["rect_transforms"], 2)
        self.assertEqual(part["metrics"]["canvas_ui_linked"], 1)
        self.assertEqual(part["metrics"]["sprite_references_readable"], 1)
        self.assertEqual(part["links"][0]["sprite_name"], "ShopIcon")
        path = next(n["path"] for n in part["nodes"] if n["name"] == "Shop")
        self.assertEqual(path, "/MainCanvas/Shop")
        self.assertEqual(part["canvases"][0]["descendant_ui_nodes"], 2)

    def test_independent_serialized_file_path_ids(self):
        one = [Reader(7, "GameObject", N(m_Name="First", m_Component=[])),
               Reader(9, "RectTransform", N(m_GameObject=p(7), m_Father=p(0)))]
        two = [Reader(7, "GameObject", N(m_Name="Second", m_Component=[])),
               Reader(9, "RectTransform", N(m_GameObject=p(7), m_Father=p(0)))]
        from collections import Counter
        a = ui.inspect_file(one, "file_1", Counter())
        b = ui.inspect_file(two, "file_2", Counter())
        self.assertEqual(a["nodes"][0]["name"], "First")
        self.assertEqual(b["nodes"][0]["name"], "Second")

if __name__ == "__main__":
    unittest.main()
