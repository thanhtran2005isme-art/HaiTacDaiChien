"""Regression guard for built-in Unity modules used by optional licensed Spine-Unity 3.8.

The C# runtime itself is private/local and is NOT part of this repository.
This test verifies only manifest dependencies, not Unity compilation.
"""
import json
from pathlib import Path
import unittest


ROOT = Path(__file__).resolve().parents[1]
MANIFEST = ROOT / "unity-ui-viewer" / "Packages" / "manifest.json"
REQUIRED = {
    "com.unity.modules.animation": "1.0.0",   # Animator, AnimationClip
    "com.unity.modules.physics": "1.0.0",     # Rigidbody
    "com.unity.modules.physics2d": "1.0.0",   # Rigidbody2D, PolygonCollider2D
}
BASELINE = {
    "com.unity.ugui",
    "com.unity.modules.imgui",
    "com.unity.modules.jsonserialize",
    "com.unity.modules.unitywebrequest",
}


class UnitySpineBuiltInModuleTests(unittest.TestCase):
    def test_required_builtin_modules_are_enabled(self):
        payload = json.loads(MANIFEST.read_text(encoding="utf-8"))
        self.assertIsInstance(payload.get("dependencies"), dict)
        for package, version in REQUIRED.items():
            with self.subTest(package=package):
                self.assertEqual(payload["dependencies"].get(package), version)

    def test_existing_packages_are_preserved(self):
        deps = json.loads(MANIFEST.read_text(encoding="utf-8"))["dependencies"]
        self.assertTrue(BASELINE.issubset(deps),
                        "Do not remove baseline Web/Unity UI packages")

    def test_no_vendor_runtime_declared(self):
        deps = json.loads(MANIFEST.read_text(encoding="utf-8"))["dependencies"]
        self.assertFalse(any("spine" in key.lower() for key in deps))
        self.assertTrue((ROOT / "unity-ui-viewer" / "Assets" / "Editor" /
                         "OfflineSpine38Preview.cs").is_file())


if __name__ == "__main__":
    unittest.main()
