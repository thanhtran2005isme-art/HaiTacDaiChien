"""Tests for metadata magic, ambiguous Unity externals and UI categorization."""
import collections
import importlib.util
import struct
import unittest
from pathlib import Path
from types import SimpleNamespace as N

src = Path(__file__).resolve().parents[1] / "tools" / "il2cpp_refs.py"
spec = importlib.util.spec_from_file_location("il2cpp_refs", src)
mod = importlib.util.module_from_spec(spec)
spec.loader.exec_module(mod)

def ptr(file_id, path_id):
    return N(m_FileID=file_id, m_PathID=path_id)

class TestIl2CppRefs(unittest.TestCase):
    def test_standard_metadata_magic(self):
        prefix = struct.pack("<IIIIII", mod.IL2CPP_MAGIC, 29, 32, 8, 40, 24)
        result = mod.metadata_header(prefix, 128)
        self.assertTrue(result["magic_matches"])
        self.assertEqual(result["version"], 29)
        self.assertEqual(result["plausible_pairs"], 2)

    def test_corrupt_metadata_not_misread(self):
        result = mod.metadata_header(b"notameta", 900)
        self.assertEqual(result["state"], "nonstandard_or_obfuscated")
        self.assertEqual(mod.metadata_header(b"abc", 3)["state"], "too_short")

    def test_crossfile_explicit_and_local(self):
        original = N(externals=[N(path="archive:/CAB-123/myScript")])
        target = object()
        readers = {(id(original), 51): "local", (21, 99): target}
        aliases = collections.defaultdict(set, {"myscript": {21}})
        self.assertEqual(mod.resolve_pointer(ptr(0, 51), original, readers, aliases),
                         ("local", "local"))
        self.assertEqual(mod.resolve_pointer(ptr(1, 99), original, readers, aliases),
                         (target, "external_resolved"))
        self.assertEqual(mod.resolve_pointer(ptr(1, 0), original, readers, aliases),
                         (None, "null"))

    def test_ambiguous_file_unresolved(self):
        original = N(externals=[N(path="assets/scripts.assets")])
        readers = {(31, 7): object(), (32, 7): object()}
        aliases = {"scripts.assets": {31, 32}}
        self.assertEqual(mod.resolve_pointer(ptr(1, 7), original, readers, aliases),
                         (None, "ambiguous_external_name"))

    def test_missing_file_not_fake_match(self):
        original = N(externals=[N(path="resources.assets")])
        self.assertEqual(mod.resolve_pointer(ptr(1, 17), original, {}, {}),
                         (None, "external_file_missing"))

    def test_ui_classification(self):
        self.assertEqual(mod.classify("UnityEngine.UI.Button"), "Button")
        self.assertEqual(mod.classify("TMPro.TextMeshProUGUI"), "TextMeshProUGUI")
        self.assertEqual(mod.classify("Game.CustomBattle"), "Other")

if __name__ == "__main__":
    unittest.main()
