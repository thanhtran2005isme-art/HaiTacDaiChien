"""Fixtures: exact scene root joining, texture provenance, Spine uncertainty."""
import importlib.util
import unittest
from pathlib import Path

source = Path(__file__).resolve().parents[1] / "tools" / "scene_visual_inventory.py"
spec = importlib.util.spec_from_file_location("scene_visual_inventory", source)
scene = importlib.util.module_from_spec(spec)
spec.loader.exec_module(scene)

def reference(name, bundle, root):
    return {"reference": name, "bundle": bundle, "root": root,
            "root_id": "12", "confidence": "candidate"}

def image(bundle, path, sid, component):
    return {"bundle": bundle, "ui_path": path, "component_id": component,
            "sprite_serialized_file": "sprites_file",
            "sprite_id": sid, "sprite_name": "Icon" + sid,
            "confidence": "probable_m_sprite"}

class TestSceneVisualInventory(unittest.TestCase):
    def test_hierarchy_boundary(self):
        self.assertTrue(scene.under_root("/Canvas/BoxPet/Card", "/Canvas"))
        self.assertTrue(scene.under_root("/Canvas", "/Canvas"))
        self.assertFalse(scene.under_root("/CanvasOther/Button", "/Canvas"))
        self.assertFalse(scene.under_root("", "/Canvas"))

    def test_exact_bundle_and_texture_dedup(self):
        references = [reference("REF01", "f029", "Canvas"),
                      reference("REF02", "f110", "PanelHeroInfo")]
        images = [image("f029", "/Canvas/BoxPet/CellPet1", "51", "101"),
                  image("f029", "/Canvas/BoxPet/CellPet2", "51", "102"),
                  image("f110", "/PanelHeroInfo/Button", "52", "103"),
                  image("f029", "/CanvasWrong/Icon", "66", "104")]
        textures = [
            {"sprite_serialized_file": "sprites_file", "sprite_id": "51",
             "texture_serialized_file": "texture_file", "texture_id": "1",
             "texture_name": "AtlasLike", "texture_size": "64x64",
             "texture_resolution": "external_resolved"},
            {"sprite_serialized_file": "sprites_file", "sprite_id": "52",
             "texture_serialized_file": "texture_file", "texture_id": "2",
             "texture_name": "Icon", "texture_size": "64x64",
             "texture_resolution": "local"},
        ]
        spine = [
            {"serialized_file": "f029", "category": "spine",
             "ui_path": "/Canvas/BoxPet/SpineMonster", "class": "SpineObject",
             "component_id": "9", "gameobject_id": "15", "typetree_status": "unavailable"},
            {"serialized_file": "f110", "category": "canvas_scaler",
             "ui_path": "/PanelHeroInfo", "class": "CanvasScaler",
             "component_id": "10", "gameobject_id": "25", "typetree_status": "unavailable"}
        ]
        missing = [{"bundle": "f029", "ui_path": "/Canvas/BoxPet/Unknown",
                    "component_id": "7", "classification": "serialized_null_sprite_pointer",
                    "gameobject": "Unknown", "active": "True"}]
        summary, sprites, skeletons, unlinked = scene.inspect(
            references, images, textures, spine, missing)
        self.assertEqual([x["linked_image_components"] for x in summary], [2, 1])
        self.assertEqual([x["unique_texture_targets"] for x in summary], [1, 1])
        self.assertEqual(summary[0]["spine_visual_component_count"], 1)
        self.assertEqual(summary[1]["spine_visual_component_count"], 0)
        self.assertEqual(summary[0]["unlinked_image_components"], 1)
        self.assertEqual(skeletons[0]["skin_status"], "unknown_not_proven_from_static_metadata")
        self.assertIsNone(summary[0]["runtime_animation_track"])
        self.assertEqual(len(sprites), 3)
        self.assertEqual(len(unlinked), 1)

    def test_missing_texture_is_explicit(self):
        summary, images, _, _ = scene.inspect(
            [reference("A", "f", "Canvas")],
            [image("f", "/Canvas/Item", "89", "10")], [], [], [])
        self.assertEqual(summary[0]["missing_texture_metadata"], 1)
        self.assertEqual(images[0]["texture_status"], "texture_metadata_missing")

    def test_reference_must_be_unique(self):
        with self.assertRaises(ValueError):
            scene.inspect([reference("A", "f", "Canvas"),
                           reference("A", "g", "Canvas")], [], [], [], [])

if __name__ == "__main__":
    unittest.main()
