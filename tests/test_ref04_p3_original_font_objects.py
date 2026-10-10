"""P3 original Font pointer identity tests; never infer runtime font use."""
from pathlib import Path
import hashlib
import importlib
import sys
import unittest

sys.path.insert(0,str(Path(__file__).resolve().parents[1]/"tools"))
probe=importlib.import_module("probe_ref04_p3_original_font_objects")

class Kind:
    def __init__(self,name):
        self.name=name

class Original:
    def __init__(self,kind,raw=b"ORIGINAL FONT"):
        self.type=Kind(kind)
        self._raw=raw
    def get_raw_data(self):
        return self._raw

class FontProof(unittest.TestCase):
    def test_exact_same_file_font_object_hash(self):
        got=probe.verify_reference(
            {"sourceFileId":0,"sourcePathId":901},
            {901:Original("Font",b"original font source")})
        self.assertEqual(got["status"],"ORIGINAL_LOCAL_FONT_OBJECT_SOURCE_SHA256_VERIFIED")
        self.assertEqual(got["sourceRawObjectSha256"],
                         hashlib.sha256(b"original font source").hexdigest())
        self.assertTrue(got["localOriginalFontObjectVerified"])
        self.assertFalse(got["runtimeFontProven"])

    def test_external_pointer_is_not_resolved_via_local_path_collision(self):
        got=probe.verify_reference(
            {"sourceFileId":1,"sourcePathId":901},
            {901:Original("Font")})
        self.assertEqual(got["status"],"BLOCKED_EXTERNAL_SOURCE_FILE_TABLE_UNAVAILABLE")
        self.assertFalse(got["localOriginalFontObjectVerified"])

    def test_original_external_font_pathid_filename_and_sha(self):
        class Ref:
            path="archive:/CAB-abcd/sharedfont.assets"
        class Owner:
            externals=[Ref()]
        class FontReader(Original):
            path_id=901
            class Serialized:
                name="sharedfont.assets"
            assets_file=Serialized()
        target=FontReader("Font",b"external native font")
        result=probe.verify_reference(
            {"sourceFileId":1,"sourcePathId":901},{},
            assets_file=Owner(),
            external_deref=lambda fid,pid,file:target)
        self.assertEqual(result["status"],
                         "SOURCE_EXTERNAL_FONT_SHA256_FILENAME_MATCH_RUNTIME_UNPROVEN")
        self.assertTrue(result["externalOriginalFontObjectVerified"])
        self.assertFalse(result["localOriginalFontObjectVerified"])
        self.assertFalse(result["runtimeFontProven"])
        self.assertEqual(result["sourceRawObjectSha256"],
                         hashlib.sha256(b"external native font").hexdigest())

    def test_external_font_wrong_serialized_file_name_blocks_even_when_pathid_matches(self):
        class Ref:
            path="archive:/CAB-one/font.assets"
        class Owner:
            externals=[Ref()]
        class FontReader(Original):
            path_id=901
            class Serialized:
                name="unrelated.assets"
            assets_file=Serialized()
        result=probe.verify_reference(
            {"sourceFileId":1,"sourcePathId":901},{},assets_file=Owner(),
            external_deref=lambda fid,pid,file:FontReader("Font"))
        self.assertEqual(result["status"],
                         "BLOCKED_EXTERNAL_FILENAME_SOURCE_MISMATCH")

    def test_null_unavailable_and_wrong_type_block(self):
        source={901:Original("Texture2D")}
        self.assertEqual(probe.verify_reference(
            {"sourceFileId":0,"sourcePathId":0},source)["status"],
            "BLOCKED_NULL_FONT_POINTER")
        self.assertEqual(probe.verify_reference(
            {"sourceFileId":0,"sourcePathId":902},source)["status"],
            "BLOCKED_ORIGINAL_FONT_OBJECT_NOT_IN_SERIALIZED_FILE")
        self.assertEqual(probe.verify_reference(
            {"sourceFileId":0,"sourcePathId":901},source)["status"],
            "BLOCKED_SOURCE_POINTER_TARGET_NOT_FONT")

    def test_untrusted_pointer_fails_closed(self):
        with self.assertRaisesRegex(ValueError,"Font pointer"):
            probe.verify_reference({"sourceFileId":"0","sourcePathId":901},{})
        with self.assertRaisesRegex(ValueError,"Negative"):
            probe.verify_reference({"sourceFileId":0,"sourcePathId":-1},{})

if __name__=="__main__":
    unittest.main()
