"""Typed Spine asset pointer candidate scanner tests."""
import importlib.util
import struct
import sys
import unittest
from pathlib import Path
from types import SimpleNamespace as N

path = Path(__file__).resolve().parents[1] / "tools"
sys.path.insert(0, str(path))
spec = importlib.util.spec_from_file_location("spine_asset_probe", path / "spine_asset_probe.py")
probe = importlib.util.module_from_spec(spec)
spec.loader.exec_module(probe)

class TestSpinePointerProbe(unittest.TestCase):
    def test_only_typed_monobehaviour_candidates(self):
        fileobj = N()
        obj = N(assets_file=fileobj, path_id=55)
        raw = b"\0" * 20 + struct.pack("<iq", 1, 55) + b"\0" * 12
        typed = {(id(fileobj), 55): "Spine.Unity.SkeletonDataAsset"}
        hits = probe.find_typed_targets(raw, "<", 1,
            lambda f, p: (obj, "external_resolved") if (f, p) == (1, 55) else (None, "missing"),
            typed)
        self.assertEqual(len(hits), 1)
        self.assertEqual(hits[0][0], 20)
        self.assertEqual(hits[0][2], "Spine.Unity.SkeletonDataAsset")

    def test_non_spine_target_rejected(self):
        target = N(assets_file=N(), path_id=31)
        raw = struct.pack("<iq", 0, 31)
        typed = {(id(target.assets_file), 31): "UnityEngine.UI.Image"}
        self.assertEqual(probe.find_typed_targets(raw, "<", 0,
            lambda *_: (target, "local"), typed), [])

    def test_external_id_bound_is_enforced(self):
        target = N(assets_file=N(), path_id=77)
        raw = struct.pack("<iq", 6, 77)
        typed = {(id(target.assets_file), 77): "Spine.Unity.SpineAtlasAsset"}
        self.assertEqual(probe.find_typed_targets(raw, "<", 3,
            lambda *_: (target, "fake"), typed), [])

    def test_big_endian_and_wrong_byte_order(self):
        target = N(assets_file=N(), path_id=19)
        raw = struct.pack(">iq", 1, 19)
        typed = {(id(target.assets_file), 19): "Spine.Unity.SpineAtlasAsset"}
        resolver = lambda f, p: (target, "external") if (f,p)==(1,19) else (None,"missing")
        self.assertEqual(len(probe.find_typed_targets(raw, ">", 1, resolver, typed)), 1)
        self.assertEqual(probe.find_typed_targets(raw, "unknown", 1, resolver, typed), [])

    def test_multimatch_retained_not_assigned(self):
        f = N()
        a = N(assets_file=f, path_id=88)
        b = N(assets_file=f, path_id=99)
        raw = struct.pack("<iq", 0, 88) + struct.pack("<iq", 0, 99)
        typed = {(id(f), 88): "Spine.Unity.SkeletonDataAsset",
                 (id(f), 99): "Spine.Unity.SpineAtlasAsset"}
        hits = probe.find_typed_targets(raw, "<", 0,
            lambda f,p: (a if p==88 else b, "local"), typed)
        self.assertEqual(len(hits), 2)
        self.assertEqual({h[2] for h in hits}, set(typed.values()))

if __name__ == "__main__":
    unittest.main()
