"""REF04 source sprite geometry must never be guessed from appearance."""
import copy
import hashlib
import importlib.util
import json
from pathlib import Path
import unittest

ROOT = Path(__file__).resolve().parents[1]
spec = importlib.util.spec_from_file_location(
    "ref04imagegeom", ROOT / "tools/audit_ref04_static_ui_geometry.py")
mod = importlib.util.module_from_spec(spec)
spec.loader.exec_module(mod)


def fixture():
    sc = "REF04-home-crew"
    graph = {
        "classification": "SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE",
        "scenes": [{"sceneId": sc, "nodes": [{
            "rectTransformId": 10, "gameObjectId": 20,
            "componentIds": [100],
        }]}]
    }
    verified = {
        "classification": "TWO_BACKEND_STRICT_SOURCE_VERIFIED_UI_FIELDS",
        "verifiedFieldValues": 7451,
        "singleBackendExcludedFieldValues": 651,
        "scenes": [{"sceneId": sc, "components": [{
            "componentPathId": 100, "className": "UnityEngine.UI.Image",
            "gameObjectPathId": 20, "rectTransformPathId": 10,
            "rawObjectSha256": "a"*64,
            "fields": [{"name": "m_Type", "kind": "int", "intValue": 1}],
        }]}],
    }
    deep = {"version": 1, "scenes": [{"sceneId": sc, "spriteGeometry": [{
        "nodeId": 10, "imageComponentId": 100, "spriteId": 77,
        "spriteFile": "a"*32+".png", "border": [7, 5, 7, 5],
        "sourceRectSize": [96, 32], "pixelsPerUnit": 100,
    }]}]}
    blobs = {
        "graph": json.dumps(graph).encode(),
        "verified": json.dumps(verified).encode(),
        "visual": b'visual manifest',
        "deep": json.dumps(deep).encode(),
    }
    visual = {
        "classification": "EXACT_SOURCE_SPRITES_ON_DUAL_VERIFIED_IMAGE_COMPONENTS",
        "sourceBindings": 963,
        "sourceGraphSha256": hashlib.sha256(blobs["graph"]).hexdigest(),
        "verifiedUiPlanSha256": hashlib.sha256(blobs["verified"]).hexdigest(),
        "scenes": [{"sceneId": sc, "bindings": [{
            "imageComponentPathId": 100, "rectTransformPathId": 10,
            "gameObjectPathId": 20, "sourceObjectSha256": "a"*64,
            "spriteFile": "a"*32+".png",
        }]}],
    }
    return visual, verified, graph, deep, blobs


class TestRef04Geometry(unittest.TestCase):
    def test_valid_sliced_geometry_is_verified(self):
        d = fixture()
        x = mod.prepare(*d)
        self.assertEqual(x["counts"]["sourceGeometryVerified"], 1)
        self.assertEqual(x["counts"]["slicedWithVerifiedNonzeroBorder"], 1)
        self.assertEqual(x["images"][0]["border"], [7, 5, 7, 5])
        self.assertEqual(x["images"][0]["sourceRectSize"], [96, 32])
        self.assertTrue(x["images"][0]["applyGeometry"])

    def test_sprite_file_disagreement_is_blocked(self):
        d = fixture()
        d[3]["scenes"][0]["spriteGeometry"][0]["spriteFile"] = "b"*32+".png"
        with self.assertRaisesRegex(ValueError, "Sprite file identity"):
            mod.prepare(*d)

    def test_ambiguous_geometry_not_applied(self):
        d = fixture()
        d[3]["scenes"][0]["spriteGeometry"].append(
            copy.deepcopy(d[3]["scenes"][0]["spriteGeometry"][0]))
        row = mod.prepare(*d)["images"][0]
        self.assertEqual(row["nativeSpriteGeometryStatus"],
                         "NO_UNIQUE_NATIVE_SPRITE_GEOMETRY")
        self.assertFalse(row["applyGeometry"])
        self.assertNotIn("border", row)

    def test_invalid_border_not_applied(self):
        d = fixture()
        d[3]["scenes"][0]["spriteGeometry"][0]["border"][0] = 2000
        row = mod.prepare(*d)["images"][0]
        self.assertFalse(row["applyGeometry"])
        self.assertNotIn("pixelsPerUnit", row)

    def test_source_identity_cannot_be_changed(self):
        d = fixture()
        d[2]["scenes"][0]["nodes"][0]["componentIds"] = []
        with self.assertRaisesRegex(ValueError, "Image identity contradicted"):
            mod.prepare(*d)

    def test_nonfinite_values_not_applied(self):
        d = fixture()
        d[3]["scenes"][0]["spriteGeometry"][0]["pixelsPerUnit"] = float("nan")
        row = mod.prepare(*d)["images"][0]
        self.assertFalse(row["applyGeometry"])

    def test_image_type_must_be_independently_verified(self):
        d = fixture()
        d[1]["scenes"][0]["components"][0]["fields"][0]["intValue"] = 10
        with self.assertRaisesRegex(ValueError, "out of source range"):
            mod.prepare(*d)

    def test_all_missing_geometry_retains_original_images(self):
        d = fixture()
        d[3]["scenes"][0]["spriteGeometry"] = []
        row = mod.prepare(*d)["images"][0]
        self.assertFalse(row["applyGeometry"])
        self.assertEqual(row["verifiedImageType"], 1)
        self.assertEqual(row["componentPathId"], 100)


if __name__ == "__main__":
    unittest.main()
