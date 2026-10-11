"""Third-backend REF04 layout recheck: source identity, strict comparison, no UI changes."""
from pathlib import Path
import importlib.util
import sys
import unittest

ROOT=Path(__file__).resolve().parents[1]
sys.path.insert(0,str(ROOT/"tools"))
spec=importlib.util.spec_from_file_location(
    "ref04third",ROOT/"tools/probe_ref04_layout_third_backend.py")
mod=importlib.util.module_from_spec(spec)
spec.loader.exec_module(mod)
H="a"*64

def fixture():
    rows=[]
    for n in range(24):
        cid=300+n
        fields={
            "m_Padding":{"m_Left":1,"m_Right":2,"m_Top":3,"m_Bottom":4},
            "m_Spacing":10.0,"m_ChildAlignment":4,
            "m_ChildControlWidth":True,"m_ChildControlHeight":True,
            "m_ChildForceExpandWidth":False,
            "m_ChildForceExpandHeight":False,
        }
        rows.append({
            "pathId":cid,"kind":"MonoBehaviour",
            "className":("UnityEngine.UI.HorizontalLayoutGroup" if n<18
                         else "UnityEngine.UI.VerticalLayoutGroup"),
            "gameObjectId":100+n,"rectTransformId":200+n,
            "status":"GENERATED_TYPETREE_SOURCE_VERIFIED",
            "binaryProof":{
                "rawObjectSha256":H,"exactSourcePointerChecked":True,
                "strictObjectSizeChecked":True,
                "nativeHeaderMethod":"UNITYPY_EXACT_SOURCE_UNITY_VERSION",
            },
            "fields":fields,
        })
    studio={"scenes":[{"sceneId":mod.SCENE,"components":rows}]}
    review={"classification":
                "SINGLE_BACKEND_UI_FIELDS_NOT_FOR_PREFAB_IMPORT",
            "componentCount":93,"excludedFieldValues":651,
            "components":[{
                "sceneId":mod.SCENE,"componentPathId":x["pathId"],
                "gameObjectPathId":x["gameObjectId"],
                "rectTransformPathId":x["rectTransformId"],
                "className":x["className"],
                "rawObjectSha256":H,
                "fieldNames":sorted(x["fields"]),"fieldCount":7,
                "prefabImportAllowed":False,
            }for x in rows]}
    graph={"classification":
               "SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE",
           "scenes":[{"sceneId":mod.SCENE,
                      "sourceSerializedFile":"real_xapk_file",
                      "components":[{"pathId":x["pathId"],
                                     "rawEvidence":{"sha256":H}}
                                    for x in rows]}]}
    return studio,review,graph


class ThirdLayout(unittest.TestCase):
    def test_24_exact_source_proofs_keep_unimportable_even_on_agreement(self):
        a,b,c=fixture()
        plan,source=mod.source_plan(a,b,c)
        self.assertEqual(len(plan),24)
        self.assertEqual(source,"real_xapk_file")
        samples={cid:("DECODED",entry["fields"],entry["binaryProof"])
                 for cid,entry in plan.items()}
        output=mod.classify(a,b,c,samples,{
            "backend":"AssetsTools","generator":"EXACT_XAPK_IL2CPP_BINARY_PAIR"})
        self.assertEqual(output["ref04LayoutGroupComponents"],24)
        self.assertEqual(output["sourceLayoutFieldNamesExamined"],168)
        self.assertFalse(output["unityImportAllowed"])
        self.assertFalse(output["runtimeLayoutProven"])
        self.assertEqual(output["statusCounts"][
            "THIRD_BACKEND_FULL_SOURCE_OBJECT_AGREES_NOT_IMPORTED"],24)

    def test_wrong_field_value_cannot_be_promoted(self):
        a,b,c=fixture()
        plan,_=mod.source_plan(a,b,c)
        cid=next(iter(plan))
        changed=dict(plan[cid]["fields"])
        changed["m_Spacing"]=333.0
        status=mod.compare_candidate(plan[cid],changed,plan[cid]["binaryProof"])
        self.assertEqual(status,"THIRD_BACKEND_FIELD_VALUES_DISAGREE")

    def test_missing_proof_blocks_even_if_values_agree(self):
        a,b,c=fixture()
        row=a["scenes"][0]["components"][0]
        self.assertEqual(mod.compare_candidate(
            row,row["fields"],{"rawObjectSha256":H}),
            "THIRD_BACKEND_SOURCE_OBJECT_PROOF_UNVERIFIED")

    def test_missing_one_layout_component_fails_closed(self):
        a,b,c=fixture()
        b["components"].pop()
        with self.assertRaisesRegex(ValueError,"count/fields changed"):
            mod.source_plan(a,b,c)

    def test_broken_source_raw_sha_fails_closed(self):
        a,b,c=fixture()
        c["scenes"][0]["components"][0]["rawEvidence"]["sha256"]="b"*64
        with self.assertRaisesRegex(ValueError,"contradicts XAPK"):
            mod.source_plan(a,b,c)


if __name__=="__main__":
    unittest.main()
