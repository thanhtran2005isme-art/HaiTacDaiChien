"""Spine payload classification guards; uses no proprietary game assets."""
import importlib.util
import pathlib
import unittest

path = pathlib.Path(__file__).resolve().parents[1] / "tools/probe_spine_payloads.py"
spec = importlib.util.spec_from_file_location("spine_probe", path)
m = importlib.util.module_from_spec(spec)
spec.loader.exec_module(m)


class TestSpineEvidence(unittest.TestCase):
    def test_spine_json(self):
        raw = b'{"skeleton":{},"bones":[{"name":"root"}],"slots":[],"animations":{"idle":{}}}'
        self.assertEqual(m.classify_payload("hero", raw), "spine_json_verified")

    def test_atlas(self):
        raw = b"character.png\nsize: 256,256\nformat: RGBA8888\nfilter: Linear,Linear\nrepeat: none\n"
        self.assertEqual(m.classify_payload("atlas", raw), "atlas_text_candidate")

    def test_cannot_assume_binary_is_spine(self):
        self.assertEqual(m.classify_payload("abc.skel", b"\x00\x01\x02"),
                         "skeleton_binary_name_only")
        self.assertEqual(m.classify_payload("none", b'{"animations":{}}'), "other")

    def test_text_bytes_only(self):
        class Data:
            m_Script = "abc"
        self.assertEqual(m.payload_bytes(Data()), b"abc")


if __name__ == "__main__":
    unittest.main()
