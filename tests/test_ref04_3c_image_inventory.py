"""A 3C Image with no native Sprite must not break REF04 UI reconstruction."""
import importlib.util
from pathlib import Path
import unittest

ROOT = Path(__file__).resolve().parents[1]
spec = importlib.util.spec_from_file_location(
    "ref04_inventory", ROOT / "tools/audit_ref04_3c_image_inventory.py")
mod = importlib.util.module_from_spec(spec)
spec.loader.exec_module(mod)


def fixtures():
    ims = []
    for c in range(100, 365):
        ims.append({
            "className": "UnityEngine.UI.Image",
            "componentPathId": c,
            "rectTransformPathId": c + 1000,
            "gameObjectPathId": c + 2000,
            "rawObjectSha256": "a" * 64,
        })
    # An extra verified but sprite-less Image component has no native Sprite PPtr.
    ims.append({
        "className": "UnityEngine.UI.Image",
        "componentPathId": 999,
        "rectTransformPathId": 1999,
        "gameObjectPathId": 2999,
        "rawObjectSha256": "b" * 64,
    })
    verified = {
        "classification": "TWO_BACKEND_STRICT_SOURCE_VERIFIED_UI_FIELDS",
        "verifiedComponents": 1108,
        "verifiedFieldValues": 7451,
        "scenes": [{"sceneId": "REF04-home-crew",
                    "components": ims}],
    }
    refs = [{
        "imageComponentPathId": c,
    } for c in range(100,365)]
    visual = {
        "classification": "EXACT_SOURCE_SPRITES_ON_DUAL_VERIFIED_IMAGE_COMPONENTS",
        "sourceBindings": 963,
        "scenes": [{"sceneId": "REF04-home-crew", "bindings": refs}],
    }
    geometry = {
        "classification": "REF04_EXACT_SOURCE_IMAGE_SPRITE_GEOMETRY",
        "sourceBindings": 265,
        "images": [{
            "componentPathId": c,
            "rectTransformPathId": c+1000,
            "gameObjectPathId": c+2000,
            "sourceObjectSha256": "a"*64,
        } for c in range(100,365)],
    }
    return verified, visual, geometry


class Ref04InventoryTests(unittest.TestCase):
    def test_265_original_sprite_links_are_a_subset_not_all_3c_images(self):
        output = mod.reconcile(*fixtures())
        self.assertEqual(output["source3cAllImageComponentCount"], 266)
        self.assertEqual(output["sourceImageWithSpriteCount"], 265)
        self.assertEqual(output["source3cImageWithoutSpriteCount"], 1)

    def test_source_sprite_link_cannot_disappear_from_3c(self):
        a,b,c = fixtures()
        a["scenes"][0]["components"].pop(0)
        with self.assertRaisesRegex(ValueError, "not a subset"):
            mod.reconcile(a,b,c)

    def test_visual_and_geometry_component_sets_must_agree(self):
        a,b,c = fixtures()
        c["images"][0]["componentPathId"] = 999
        with self.assertRaisesRegex(ValueError, "not a subset"):
            mod.reconcile(a,b,c)

    def test_source_gameobject_identity_remains_strict(self):
        a,b,c = fixtures()
        c["images"][0]["gameObjectPathId"] += 1
        with self.assertRaisesRegex(ValueError, "differs from 3C"):
            mod.reconcile(a,b,c)


if __name__ == "__main__":
    unittest.main()
