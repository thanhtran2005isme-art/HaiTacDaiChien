"""P4: 62 Text -> two original Font sources -> 52 I2 source-localizers.

Synthetic fixtures only. No fixture proves actual runtime text, font loading,
selected locale or dynamic field writers.
"""
import copy
import hashlib
import importlib
from pathlib import Path
import sys
import unittest

ROOT = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))
p4 = importlib.import_module("audit_ref04_p4_text_logic")
SHA = "a" * 64
M = "b" * 64
L = "c" * 64


def fixture():
    pair = {"metadataSha256": M, "libil2cppSha256": L,
            "sourceUnityVersion": "2022.3.51f1"}
    fonts = []
    for pid in (900, 901):
        fonts.append({
            "sourceFileId": 1, "sourcePathId": pid,
            "externalOriginalFontObjectVerified": True,
            "localOriginalFontObjectVerified": False,
            "originalNativeType": "Font", "sourceRawObjectBytes": 64,
            "sourceRawObjectSha256": hashlib.sha256(
                str(pid).encode("ascii")).hexdigest(),
            "runtimeFontProven": False})
    local = []
    term = []
    for i in range(52):
        cid = 700 + i
        game_object = (100 + i) if i < 50 else 10000 + i
        rect = (500 + i) if i < 50 else 20000 + i
        cls = "I2.Loc.Localize" if i < 26 else "TextLocalizeChecker"
        local.append({
            "componentPathId": cid,
            "sourceClass": cls, "gameObjectPathId": game_object,
            "rectTransformPathId": rect, "originalObjectSha256": SHA,
            "runtimeLocalizedText": None, "localizationBindingProven": False})
        term.append({
            "componentPathId": cid, "sourceClass": cls,
            "gameObjectPathId": game_object, "rectTransformPathId": rect,
            "originalObjectSha256": SHA,
            "verificationStatus": "BLOCKED_SOURCE_TYPETREE_MISSING",
            "sourceTermFieldDigests": {},
            "runtimeTranslationProven": False, "unityImportAllowed": False})
    text = []
    for i in range(62):
        raw = (b"Hello source only" if i == 0 else b"")
        content_field = {
            "sourceUtf8Sha256": hashlib.sha256(raw).hexdigest(),
            "utf8Bytes": len(raw), "sourceTextIsEmpty": not bool(raw)}
        font_ref = {"sourceFileId": 1, "sourcePathId": 900 if i < 31 else 901}
        candidates = [700 + i] if i < 50 else []
        text.append({
            "componentPathId": 2000+i, "gameObjectPathId": 100+i,
            "rectTransformPathId": 500+i, "originalObjectSha256": SHA,
            "sourceTextUtf8Sha256": content_field["sourceUtf8Sha256"],
            "sourceTextUtf8ByteLength": len(raw), "sourceEmptyText": not bool(raw),
            "originalFontPointer": font_ref,
            "originalTextFieldStatus": "DUAL_BACKEND_M_TEXT_FIELD",
            "originalFontFieldStatus": "DUAL_BACKEND_M_FONT_FIELD",
            "originalTextSourceValues": {"m_Text": content_field, "m_Font": font_ref,
                                         "m_FontSize": 20},
            "colocatedLocalizationComponentPathIds": candidates,
            "localizationConnection": (
                "SAME_ORIGINAL_GAMEOBJECT_ONLY_NO_BINDING_PROOF" if candidates else
                "NO_LOCALIZER_ON_ORIGINAL_GAMEOBJECT"),
            "runtimeString": None, "runtimeLanguage": None, "runtimeFont": None,
            "runtimeTextProven": False, "unityImportAllowed": False,
        })
    p3 = {
        "classification": p4.P3_CLASS, "originalSourceSerializedFile": "realSource",
        "originalIL2CPPSha256Pair": pair,
        "sourceTextComponentsIndependentlyVerified": 62,
        "sourceTextStringsIndependentlyVerified": 62,
        "sourceTextFontPointersIndependentlyVerified": 62,
        "sourceTextFieldsVerified": True,
        "sourceLocalizationComponents": 52,
        "uniqueOriginalFontPointerCount": 2,
        "originalTextSameGameObjectLocalizationCandidates": 50,
        "methodBodiesVerified": 0,
        "originalTextContentPublished": False,
        "runtimeTextAndLocalizationProven": False,
        "localizationKeyToTextBindingProven": False,
        "runtimeTextValuesProven": False,
        "runtimeLanguageChosen": None,
        "unityImportAllowed": False,
        "originalTextSourceEvidence": text,
        "sourceLocalizationComponentsEvidence": local,
    }
    font_proof = {
        "classification": p4.FONT_CLASS,
        "originalSourceSerializedFile": "realSource",
        "originalIL2CPPSha256Pair": pair,
        "originalFontPPtrReferences": 2,
        "originalFontAssetIdentityFullyResolved": True,
        "pointerResolutions": fonts,
        "runtimeFontRenderingProven": False,
        "unityImportAllowed": False,
    }
    term_proof = {
        "classification": p4.LOCALIZER_CLASS,
        "originalSourceSerializedFile": "realSource",
        "originalSourceMetadataSha256": M,
        "originalSourceLibSha256": L,
        "localizersChecked": 52,
        "sourceTermFieldsTwoBackendVerified": 0,
        "sourceTermValuesPublished": False,
        "runtimeLanguageOrTranslationProven": False,
        "localizerToTextTargetVerified": False,
        "unityImportAllowed": False,
        "localizers": term,
    }
    return p3, font_proof, term_proof


class TextP4SourceChecks(unittest.TestCase):
    def test_full_62_text_and_two_fonts_source_only(self):
        report = p4.build(*fixture())
        self.assertEqual(report["sourceTextComponentsVerified"], 62)
        self.assertEqual(report["sourceFontObjectsVerified"], 2)
        self.assertEqual(report["sourceLocalizerComponentsChecked"], 52)
        self.assertEqual(report["originalSameOwnerLocalizationCandidates"], 50)
        self.assertEqual(report["sourceTextInitialStates"],
                         {"ORIGINAL_EMPTY_TEXT_SOURCE": 61,
                          "ORIGINAL_NONEMPTY_TEXT_SOURCE": 1})
        self.assertEqual([row["originalTextComponentsReferencingFont"]
                          for row in report["originalFontUsage"]], [31,31])
        self.assertIsNone(report["originalFontUsage"][0]["runtimeFontLoaded"])
        self.assertFalse(report["runtimeTextLogicRecovered"])
        self.assertFalse(report["originalTextStringsPublished"])
        self.assertFalse(report["unityImportAllowed"])

    def test_empty_serialized_text_never_claimed_dynamic(self):
        out = p4.build(*fixture())
        self.assertEqual(out["sourceTextRows"][1]["originalTextState"],
                         "ORIGINAL_EMPTY_TEXT_SOURCE")
        self.assertEqual(out["sourceTextRows"][1]["dynamicTextWriterStatus"],
                         "UNKNOWN_NO_VERIFIED_RUNTIME_FIELD_WRITER")
        self.assertIsNone(out["sourceTextRows"][1]["runtimeText"])
        self.assertEqual(out["sourceTextRows"][0]["localizedAssignmentStatus"],
                         "COLOCATED_SOURCE_CANDIDATE_ONLY")
        self.assertFalse(out["sourceTextRows"][0]["runtimeLocalizedBindingProven"])

    def test_wrong_font_target_or_pointer_mismatch_blocks(self):
        p3, fonts, loc = fixture()
        fonts["pointerResolutions"][1]["originalNativeType"] = "Texture2D"
        with self.assertRaisesRegex(ValueError, "Font object"):
            p4.build(p3, fonts, loc)
        p3, fonts, loc = fixture()
        p3["originalTextSourceEvidence"][0]["originalFontPointer"]["sourcePathId"] = 989
        with self.assertRaisesRegex(ValueError, "Text source"):
            p4.build(p3, fonts, loc)

    def test_corrupted_text_length_empty_digest_and_identity_block(self):
        for key, value in (
            ("sourceTextUtf8ByteLength", 123),
            ("sourceEmptyText", False),
            ("originalObjectSha256", "notSHA"),
        ):
            p3, font, terms = fixture()
            p3["originalTextSourceEvidence"][1][key] = value
            with self.assertRaisesRegex(ValueError, "Text source"):
                p4.build(p3, font, terms)
        p3, font, terms = fixture()
        p3["originalTextSourceEvidence"][1]["componentPathId"] = 2000
        with self.assertRaisesRegex(ValueError, "Text source"):
            p4.build(p3, font, terms)

    def test_localizer_class_sha_and_colocation_must_match(self):
        p3, fonts, terms = fixture()
        terms["localizers"][0]["sourceClass"] = "Unrelated"
        with self.assertRaisesRegex(ValueError, "localizer owner"):
            p4.build(p3, fonts, terms)
        p3, fonts, terms = fixture()
        p3["originalTextSourceEvidence"][0]["colocatedLocalizationComponentPathIds"] = []
        with self.assertRaisesRegex(ValueError, "co-location"):
            p4.build(p3, fonts, terms)

    def test_term_unverified_cannot_gain_hashes_or_runtime_binding(self):
        p3, fonts, terms = fixture()
        terms["localizers"][0]["sourceTermFieldDigests"] = {
            "mTerm": {"originalUtf8Sha256": SHA, "originalByteLength": 12}}
        terms["sourceTermFieldsTwoBackendVerified"] = 1
        with self.assertRaisesRegex(ValueError, "term evidence"):
            p4.build(p3, fonts, terms)
        p3, fonts, terms = fixture()
        terms["localizers"][0]["verificationStatus"] = (
            "DUAL_BACKEND_LOCALIZER_TERMS_SOURCE_ONLY")
        terms["localizers"][0]["sourceTermFieldDigests"] = {
            "mTerm": {"originalUtf8Sha256": SHA, "originalByteLength": 12}}
        terms["sourceTermFieldsTwoBackendVerified"] = 1
        report = p4.build(p3, fonts, terms)
        self.assertEqual(report["sourceTermFieldsIndependentlyVerified"], 1)
        self.assertEqual(report["runtimeLocalizedAssignmentsIndependentlyProven"], 0)
        self.assertIsNone(report["runtimeLocale"])

    def test_wrong_il2cpp_pair_or_faked_runtime_proof_blocks(self):
        p3, fonts, terms = fixture()
        terms["originalSourceLibSha256"] = "f"*64
        with self.assertRaisesRegex(ValueError, "source pair"):
            p4.build(p3, fonts, terms)
        p3, fonts, terms = fixture()
        p3["runtimeTextAndLocalizationProven"] = True
        with self.assertRaisesRegex(ValueError, "source contract"):
            p4.build(p3, fonts, terms)
        p3, fonts, terms = fixture()
        fonts["runtimeFontRenderingProven"] = True
        with self.assertRaisesRegex(ValueError, "Font source"):
            p4.build(p3, fonts, terms)


if __name__ == "__main__":
    unittest.main()
