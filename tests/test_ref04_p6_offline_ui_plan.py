"""P6 offline source reconstruction tests; no original Android game/server."""
from __future__ import annotations

import json
from pathlib import Path
import sys
import tempfile
import unittest

ROOT = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))
import ref04_p6_offline_ui_plan as p6

S = "a" * 64


def fixture():
    nodes = [
        {"gameObjectPathId": i + 100, "rectTransformPathId": i + 1000,
         "components": []}
        for i in range(503)
    ]
    geom = []
    for i in range(1564):
        node = nodes[i % 503]
        is_image = i < 265
        component = {
            "componentPathId": i + 5000,
            "gameObjectPathId": node["gameObjectPathId"],
            "rectTransformPathId": node["rectTransformPathId"],
            "monoScriptClass": ("UnityEngine.UI.Image" if is_image
                                else "UnrelatedSourceComponent"),
            "rawSourceObjectSha256": S,
            "verificationStatus": (
                "TWO_BACKEND_SOURCE_VERIFIED_FIELDS" if is_image
                else "UNVERIFIED_FIELDS"),
        }
        node["components"].append(component)
        if not is_image:
            continue
        valid = i < 200
        row = {
            "componentPathId": component["componentPathId"],
            "gameObjectPathId": component["gameObjectPathId"],
            "rectTransformPathId": component["rectTransformPathId"],
            "sourceObjectSha256": S,
            "spriteFile": ("%032x" % (i + 1)) + ".png",
            "nativeSpriteGeometryStatus": (
                "NATIVE_SPRITE_GEOMETRY_VERIFIED" if valid
                else "NO_UNIQUE_NATIVE_SPRITE_GEOMETRY"),
            "applyGeometry": valid,
        }
        if valid:
            row.update(sourceRectSize=[42.0, 81.0],
                       border=[0.0, 0.0, 0.0, 0.0],
                       pixelsPerUnit=100.0)
        geom.append(row)
    common = "original-private-serialized"
    p5 = {
        "classification": p6.P5_KIND, "sceneId": p6.SCENE,
        "originalSourceSerializedFile": common,
        "sourceCoverage": {
            "P1OriginalLayoutGroupFields": 168,
            "P2OriginalCanvasAndRelatedComponents": 15,
            "P2ComponentsWithOriginalRawSha": 14,
            "P2IdentityOnlyComponentsWithoutRawSha": 1,
            "P2SourceFieldNamesExcludedWithoutRawSha": 6,
            "P4OriginalTextComponents": 62,
            "totalSourceProvenanceEntries": 245,
        },
        "runtimeCoordinates": None, "runtimeLayoutProven": False,
        "unityImportAllowed": False, "originalUiAssetsChanged": False,
    }
    inventory = {
        "classification": p6.INV_KIND, "sceneId": p6.SCENE,
        "sourceSerializedFile": common,
        "sourceFieldApplicationAllowed": False,
        "counts": {
            "gameObjects": 503, "serializedComponentRecords": 1564,
            "allDualVerifiedImageComponents": 299,
            "originalSpriteLinkedImages": 265,
            "verifiedImagesWithoutSourceSprite": 34,
        },
        "gameObjects": nodes,
    }
    geometry = {
        "classification": p6.GEO_KIND, "sceneId": p6.SCENE,
        "sourceBindings": 265, "images": geom,
    }
    return p5, inventory, geometry


class OfflineRef04Ui(unittest.TestCase):
    def test_synthetic_source_ledger_has_gaps_without_android_or_server(self):
        result = p6.build(*fixture())
        c = result["counts"]
        self.assertEqual(c["spriteLinkedImages"], 265)
        self.assertEqual(c["sourceGeometryVerified"], 200)
        self.assertEqual(c["sourceGeometryBlocked"], 65)
        self.assertEqual(c["verifiedImagesWithoutSourceSprite"], 34)
        self.assertEqual(c["layoutSerializedFieldsTwoSchemaVerified"], 168)
        self.assertEqual(c["originalTextSourceComponents"], 62)
        self.assertFalse(result["requiresOriginalAndroidGameToRun"])
        self.assertFalse(result["requiresOriginalGameServer"])
        self.assertTrue(result["newBackendIsAuthoritativeForNewGameplay"])
        self.assertFalse(result["originalGameBackendBehaviorVerified"])
        self.assertFalse(result["originalXapkPixelPerfectMatchProven"])
        self.assertIsNone(result["runtimeCoordinates"])
        self.assertEqual(len(result["spriteGeometryAudit"]), 265)

    def test_wrong_image_sha_or_owner_never_enters_unity_source_ledger(self):
        for field, value in (
            ("sourceObjectSha256", "b" * 64),
            ("gameObjectPathId", -1),
            ("rectTransformPathId", -1),
            ("componentPathId", 999999),
        ):
            p5, inventory, geometry = fixture()
            geometry["images"][0][field] = value
            with self.subTest(field=field), self.assertRaisesRegex(
                    ValueError, "P6 OFFLINE BLOCKED"):
                p6.build(p5, inventory, geometry)

    def test_unverified_geometry_cannot_be_pretended_verified(self):
        p5, inventory, geometry = fixture()
        geometry["images"][250]["applyGeometry"] = True
        with self.assertRaisesRegex(ValueError, "geometry proof status"):
            p6.build(p5, inventory, geometry)
        p5, inventory, geometry = fixture()
        del geometry["images"][1]["pixelsPerUnit"]
        with self.assertRaisesRegex(ValueError, "geometry field missing"):
            p6.build(p5, inventory, geometry)

    def test_p5_runtime_fabrication_or_wrong_source_file_is_rejected(self):
        p5, inventory, geometry = fixture()
        p5["runtimeCoordinates"] = [100, 900]
        with self.assertRaisesRegex(ValueError, "P5 exact"):
            p6.build(p5, inventory, geometry)
        p5, inventory, geometry = fixture()
        inventory["sourceSerializedFile"] = "different-xapk"
        with self.assertRaisesRegex(ValueError, "SerializedFile"):
            p6.build(p5, inventory, geometry)

    def test_real_file_io_does_not_require_device_or_live_apk(self):
        p5, inventory, geometry = fixture()
        with tempfile.TemporaryDirectory() as folder:
            root = Path(folder)
            (root / "output").mkdir()
            for name, doc in (
                ("ref04-p5-cross-phase-source-integrity.json", p5),
                ("ref04-full-source-inventory.json", inventory),
                ("ref04-static-image-geometry.json", geometry),
            ):
                (root / "output" / name).write_text(
                    json.dumps(doc), encoding="utf-8")
            report = p6.execute(root)
            self.assertEqual(report["mode"],
                             "OFFLINE_XAPK_SOURCE_TO_NEW_UNITY_CLIENT")
            out = root / "output/ref04-p6-offline-ui-gaps.json"
            self.assertTrue(out.exists())
            saved = json.loads(out.read_text(encoding="utf-8"))
            self.assertFalse(saved["requiresOriginalGameServer"])
            self.assertEqual(saved["counts"]["P2IdentityOnlyComponents"], 1)
            self.assertTrue(saved["sourceReportSha256"]["p5"])


if __name__ == "__main__":
    unittest.main()
