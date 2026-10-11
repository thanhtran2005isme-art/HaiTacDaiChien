"""Phase 3D: reject mismatched source Sprite, GameObject or 3C component IDs."""
from __future__ import annotations

import copy
import hashlib
import importlib.util
import json
from pathlib import Path
import unittest
from unittest import mock

ROOT = Path(__file__).resolve().parents[1]
SPEC = importlib.util.spec_from_file_location(
    "visualplan", ROOT / "tools/build_verified_visual_plan.py")
module = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(module)


def inputs():
    scene_ids = ["REF01-ship-upgrade", "REF02-hero-detail",
                 "REF03-islands-map-A", "REF03-islands-map-B",
                 "REF04-home-crew"]
    f = "a" * 32 + ".png"
    nodes = []
    graph_scenes, ui_scenes, spr, art_rows = [], [], [], []
    for i, scene in enumerate(scene_ids):
        node, go, cid = 100+i, 200+i, 300+i
        graph_scenes.append({"sceneId": scene, "nodes": [
            {"rectTransformId": node, "gameObjectId": go,
             "componentIds": [node, cid]}]})
        ui_scenes.append({"sceneId": scene, "components": [{
            "rectTransformPathId": node, "gameObjectPathId": go,
            "componentPathId": cid, "className": "UnityEngine.UI.Image",
            "rawObjectSha256": "b"*64,
        }]})
        spr.append({"sceneId": scene, "nodeId": node,
                    "sourceImageComponentId": cid, "spriteFile": f})
        art_rows.append({"sceneId": scene, "nodeId": node,
                         "imageComponentId": cid, "spriteFile": f})
    graph = {
        "classification":
            "SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE",
        "scenes": graph_scenes,
    }
    plan = {
        "schemaVersion": 1,
        "classification": "TWO_BACKEND_STRICT_SOURCE_VERIFIED_UI_FIELDS",
        "verifiedComponents": 1108, "verifiedFieldValues": 7451,
        "singleBackendExcludedFieldValues": 651,
        "sourceGraphSha256": "",
        "scenes": ui_scenes,
    }
    prefab = {"schemaVersion": 2,
              "scenes": [{"sceneId": x} for x in scene_ids],
              "sprites": spr}
    art = {"version": 1, "files": [f], "nodeBindings": art_rows,
           "stats": {"exact_node_bindings": 5}}
    docs = {"graph": graph, "verified": plan, "prefab": prefab, "art": art}
    blobs = {k: json.dumps(docs[k], sort_keys=True).encode() for k in docs}
    plan["sourceGraphSha256"] = hashlib.sha256(blobs["graph"]).hexdigest()
    blobs["verified"] = json.dumps(plan, sort_keys=True).encode()
    return docs, blobs


class VerifiedVisualPlanTests(unittest.TestCase):
    def setUp(self):
        self.docs, self.blobs = inputs()
        self.p = self.docs

    def call(self):
        with mock.patch.object(module, "EXPECTED_BINDINGS", 5):
            return module.build(
                self.p["verified"], self.p["graph"], self.p["prefab"],
                self.p["art"], self.blobs)

    def test_all_source_images_bound_by_pathid_without_values(self):
        output = self.call()
        self.assertEqual(output["sourceBindings"], 5)
        self.assertEqual(sum(len(s["bindings"]) for s in output["scenes"]), 5)
        self.assertEqual(output["classification"], module.CLASSIFICATION)
        self.assertTrue(output["previewOnly"])
        self.assertEqual(output["verifiedUiPlanSha256"],
                         hashlib.sha256(self.blobs["verified"]).hexdigest())
        self.assertNotIn("fields", json.dumps(output))
        self.assertFalse(any("Spine" in r for r in output["scenes"]))

    def test_swapping_component_on_sprite_manifest_is_blocked(self):
        self.p["prefab"]["sprites"][0]["sourceImageComponentId"] += 6
        with self.assertRaisesRegex(ValueError, "Sprite-to-Image"):
            self.call()

    def test_forged_source_gameobject_binding_is_blocked(self):
        self.p["verified"]["scenes"][0]["components"][0]["gameObjectPathId"] += 1
        with self.assertRaisesRegex(ValueError, "3C source component ownership"):
            self.call()

    def test_missing_component_in_original_graph_is_blocked(self):
        self.p["graph"]["scenes"][0]["nodes"][0]["componentIds"] = [100]
        with self.assertRaisesRegex(ValueError, "3C source component ownership"):
            self.call()

    def test_changed_3c_file_fingerprint_is_blocked(self):
        self.p["verified"]["sourceGraphSha256"] = "f"*64
        with self.assertRaisesRegex(ValueError, "source documents"):
            self.call()

    def test_invalid_sprite_filenames_are_blocked(self):
        self.p["prefab"]["sprites"][0]["spriteFile"] = "../private.png"
        with self.assertRaisesRegex(ValueError, "Sprite-to-Image"):
            self.call()

    def test_no_source_values_from_single_backend(self):
        self.p["verified"]["singleBackendExcludedFieldValues"] = 0
        with self.assertRaisesRegex(ValueError, "source documents"):
            self.call()

    def test_duplicate_source_binding_is_blocked(self):
        self.p["art"]["nodeBindings"].append(
            copy.deepcopy(self.p["art"]["nodeBindings"][0]))
        with self.assertRaisesRegex(ValueError, "identities duplicate"):
            self.call()


if __name__ == "__main__":
    unittest.main()
