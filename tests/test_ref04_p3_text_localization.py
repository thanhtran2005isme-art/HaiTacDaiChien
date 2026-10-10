"""P3: original Text/font PPtrs and colocated localizers are not runtime text."""
import copy
import hashlib
import importlib
from pathlib import Path
import sys
import unittest

ROOT=Path(__file__).resolve().parents[1]
sys.path.insert(0,str(ROOT/"tools"))
p3=importlib.import_module("audit_ref04_p3_text_localization")
SHA="a"*64
M="b"*64
L="c"*64

def fixture():
    textrows=[]
    probe=[]
    for i in range(62):
        cid=3000+i
        txt=b""
        fields={
            "m_Text":{"sourceUtf8Sha256":hashlib.sha256(txt).hexdigest(),
                      "utf8Bytes":0,"sourceTextIsEmpty":True},
            "m_Font":{"sourceFileId":0,"sourcePathId":800},
            "m_FontSize":22, "m_Alignment":4}
        textrows.append({
            "componentPathId":cid,"gameObjectPathId":100+i,
            "rectTransformPathId":900+i,"originalObjectSha256":SHA,
            "textBinaryProbeStatus":p3.TEXT_STATUS,
            "textSourceFieldsTwoBackendsAgreed":copy.deepcopy(fields),
            "canBeAppliedToUnity":False,"renderedText":None,
        })
        probe.append({
            "componentPathId":cid,"gameObjectPathId":100+i,
            "rectTransformPathId":900+i,"sourceObjectSha256":SHA,
            "verificationStatus":p3.TEXT_STATUS,
            "sourceTextFieldEvidence":fields})
    loc=[
        {"componentPathId":4000,
         "gameObjectPathId":100,"rectTransformPathId":900,
         "sourceClass":"I2.Loc.Localize","originalObjectSha256":SHA,
         "verificationStatus":"UNVERIFIED_FIELDS",
         "canBeAppliedToUnity":False,"runtimeRulesVerified":False}
    ]
    step2={"classification":"REF04_SOURCE_LAYOUT_CANVAS_TEXT_STEP2_READ_ONLY",
           "sourceSerializedFile":"original",
           "counts":{"Text":62,"TextLocalization":1},
           "originalTextComponentsVerifiedByTwoBackends":62,
           "sourceRuntimeTextProven":False,
           "sourceFieldApplicationAllowed":False,
           "componentsByCategory":{"Text":textrows,"TextLocalization":loc}}
    text={"classification":p3.TEXT_CLASS,
          "sourceSerializedFile":"original","sourceTextComponents":62,
          "dualBackendAgreedComponentCount":62,
          "rawTextContentPublished":False,"runtimeTextProven":False,
          "unityImportAllowed":False,
          "sourceBinary":{"metadataSha256":M,"librarySha256":L},
          "textComponents":probe}
    p2={"classification":p3.P2_CLASS,"originalSourceSerializedFile":"original",
        "originalIL2CPPPair":{"metadataSha256":M,"libil2cppSha256":L,
                             "sourceUnityVersion":"2022.3.51f1"},
        "runtimeAlignmentProven":False,"sourceFieldApplicationAllowed":False}
    method={"classification":"REF04_IL2CPP_V31_SOURCE_METHOD_INDEX_NO_CODE_MAPPING",
            "methodInventoryScope":"p3","metadataSha256":M,
            "targetClassDefinitions":[{
                "className":"TextLocalizeChecker","namespace":"",
                "nativeMethodAddressResolved":False,
                "runtimeExpressionProven":False,
                "methods":[{"methodBodyVerified":False,"nativeAddress":None}]}]}
    return step2,text,p2,method

class TextLocalizationGuards(unittest.TestCase):
    def test_62_original_texts_not_runtime_and_colocation_not_binding(self):
        got=p3.audit(*fixture())
        self.assertEqual(got["sourceTextComponentsIndependentlyVerified"],62)
        self.assertEqual(got["originalTextSameGameObjectLocalizationCandidates"],1)
        self.assertEqual(got["uniqueOriginalFontPointerCount"],1)
        self.assertFalse(got["localizationKeyToTextBindingProven"])
        self.assertFalse(got["runtimeTextAndLocalizationProven"])
        self.assertFalse(got["fontAssetIdentityVerified"])
        self.assertTrue(got["sourceTextFieldsVerified"])
        self.assertFalse(got["unityImportAllowed"])
        text=got["originalTextSourceEvidence"][0]
        self.assertEqual(text["colocatedLocalizationComponentPathIds"],[4000])
        self.assertIsNone(text["runtimeString"])
        self.assertEqual(text["localizationConnection"],
                         "SAME_ORIGINAL_GAMEOBJECT_ONLY_NO_BINDING_PROOF")

    def test_verified_localizer_term_names_do_not_prove_language_or_text_binding(self):
        step2,text,p2,metadata=fixture()
        local=step2["componentsByCategory"]["TextLocalization"][0]
        local["verificationStatus"]="TWO_BACKEND_SOURCE_VERIFIED_FIELDS"
        local["verifiedSerializedFields"]={"m_Term":{"sourceValueSha256":"a"*64},
                                           "m_RandomUnrelatedField":{"present":True}}
        out=p3.audit(step2,text,p2,metadata)
        self.assertEqual(out["sourceLocalizationComponentsWithDualBackendFields"],1)
        self.assertEqual(out["sourceLocalizationComponentsEvidence"][0][
            "sourceKeyOrTermFieldNameCandidates"],["m_Term"])
        self.assertFalse(out["localizationKeyToTextBindingProven"])
        self.assertIsNone(out["runtimeLanguageChosen"])

    def test_unverified_localizer_cannot_claim_managed_field_values(self):
        step2,text,p2,metadata=fixture()
        local=step2["componentsByCategory"]["TextLocalization"][0]
        local["verifiedSerializedFields"]={"m_Term":"fabricated"}
        with self.assertRaisesRegex(ValueError,"Unverified localizer"):
            p3.audit(step2,text,p2,metadata)

    def test_wrong_text_sha_rejected(self):
        step2,text,p2,metadata=fixture()
        text["textComponents"][0]["sourceObjectSha256"]="f"*64
        with self.assertRaisesRegex(ValueError,"source Text"):
            p3.audit(step2,text,p2,metadata)

    def test_missing_font_field_is_blocked_only_for_font_not_fake_font(self):
        step2,text,p2,metadata=fixture()
        step2["componentsByCategory"]["Text"][0][
            "textSourceFieldsTwoBackendsAgreed"].pop("m_Font")
        text["textComponents"][0]["sourceTextFieldEvidence"].pop("m_Font")
        result=p3.audit(step2,text,p2,metadata)
        first=result["originalTextSourceEvidence"][0]
        self.assertIsNone(first["originalFontPointer"])
        self.assertEqual(first["originalFontFieldStatus"],
                         "BLOCKED_SOURCE_M_FONT_FIELD_NOT_EXTRACTED")
        self.assertEqual(result["sourceTextFontPointersIndependentlyVerified"],61)
        self.assertFalse(result["sourceTextFieldsVerified"])
        self.assertFalse(result["runtimeFontRenderingProven"])

    def test_source_backend_field_conflict_is_never_accepted(self):
        step2,text,p2,metadata=fixture()
        text["textComponents"][0]["sourceTextFieldEvidence"]["m_Font"]["sourcePathId"]=90
        with self.assertRaisesRegex(ValueError,"Text hash/field"):
            p3.audit(step2,text,p2,metadata)

    def test_fake_runtime_method_body_not_accepted(self):
        step2,text,p2,metadata=fixture()
        metadata["targetClassDefinitions"][0]["methods"][0]["nativeAddress"]=123
        with self.assertRaisesRegex(ValueError,"Unproven P3 IL2CPP"):
            p3.audit(step2,text,p2,metadata)

    def test_wrong_binary_source_pair_blocks(self):
        step2,text,p2,metadata=fixture()
        text["sourceBinary"]["metadataSha256"]="f"*64
        with self.assertRaisesRegex(ValueError,"source identity"):
            p3.audit(step2,text,p2,metadata)

    def test_colocation_does_not_infer_runtime_binding(self):
        step2,text,p2,metadata=fixture()
        loc=step2["componentsByCategory"]["TextLocalization"][0]
        loc["gameObjectPathId"]=123456
        got=p3.audit(step2,text,p2,metadata)
        self.assertEqual(got["originalTextSameGameObjectLocalizationCandidates"],0)
        self.assertFalse(got["localizationKeyToTextBindingProven"])

if __name__=="__main__":
    unittest.main()
