"""REF04 P2 original hierarchy + IL2CPP source identity: no UI output."""
import importlib
from pathlib import Path
import sys
import unittest

ROOT=Path(__file__).resolve().parents[1]
sys.path.insert(0,str(ROOT/"tools"))
p2=importlib.import_module("audit_ref04_p2_runtime_alignment")
S="a"*64
M="b"*64
L="c"*64


def fixture():
    nodes=[{
        "gameObjectPathId":100+i,"rectTransformPathId":1000+i,
        "sourceParentPointer":{"fileId":0,"pathId":0 if i==0 else 1000},
        "components":[],
    } for i in range(503)]
    def comp(cid, idx, native, cls=None, status="UNVERIFIED_FIELDS"):
        n=nodes[idx]
        d={"componentPathId":cid,"gameObjectPathId":n["gameObjectPathId"],
           "rectTransformPathId":n["rectTransformPathId"],
           "nativeKind":native,"monoScriptClass":cls,
           "rawSourceObjectSha256":S,"verificationStatus":status}
        n["components"].append(d)
        return d
    canvas=comp(500,0,"Canvas",status="XAPK_NATIVE_SERIALIZED_FIELD_SUBSET")
    scaler=comp(501,0,"MonoBehaviour","UnityEngine.UI.CanvasScaler",
                status="TWO_BACKEND_SOURCE_VERIFIED_FIELDS")
    safe=[comp(510+i,i+1,"MonoBehaviour","SafeAreaAdapter") for i in range(6)]
    panel=comp(600,9,"MonoBehaviour","PanelHome2Right")
    for i in range(1564-9):
        comp(10000+i,10+(i%493),"CanvasRenderer")
    def reported(c,category,fields):
        return {"componentPathId":c["componentPathId"],
                "gameObjectPathId":c["gameObjectPathId"],
                "rectTransformPathId":c["rectTransformPathId"],
                "originalObjectSha256":S,"canBeAppliedToUnity":False,
                "runtimeRulesVerified":False,
                "verifiedSerializedFields":fields,
                "nativeCanvasFieldsExtracted":{"m_RenderMode":0} if category=="Canvas" else {},
                }
    step1={"classification":p2.STEP1_CLASS,"sceneId":p2.SCENE,
           "sourceSerializedFile":"original-file",
           "sourceFieldApplicationAllowed":False,
           "counts":{"serializedComponentRecords":1564},
           "gameObjects":nodes}
    step2={"classification":p2.STEP2_CLASS,"sceneId":p2.SCENE,
           "sourceSerializedFile":"original-file",
           "counts":{"Canvas":1,"CanvasScaler":1,"SafeArea":6},
           "independentlyVerifiedSerializedLayoutFieldValues":168,
           "sourceFieldApplicationAllowed":False,
           "componentsByCategory":{
               "Canvas":[reported(canvas,"Canvas",{"m_RenderMode":0})],
               "CanvasScaler":[reported(scaler,"CanvasScaler",{"m_ScaleFactor":{}})],
               "SafeArea":[reported(x,"SafeArea",{}) for x in safe],
           }}
    p1={"classification":p2.P1_CLASS,"sourceSerializedFile":"original-file",
        "sourceFieldsVerifiedByTwoGeneratedSchemas":168,
        "sourceFieldsMissingIndependentSchemaProof":0,
        "unityImportAllowed":False,
        "exactSourcePair":{"globalMetadataSha256":M,"libil2cppSha256":L}}
    method={"classification":p2.METHOD_CLASS,"metadataVersion":31,
            "metadataSha256":M,"methodCodeAddressesResolved":0,
            "runtimeFormulaRecovered":False,
            "safeAreaClassDefinitions":1,"panelHome2ClassDefinitions":1,
            "targetClassDefinitions":[
                {"typeDefinitionIndex":10,"methodCount":1,
                 "nativeMethodAddressResolved":False,"runtimeExpressionProven":False,
                 "methods":[{"name":"ApplySafeArea","methodBodyVerified":False,
                             "nativeAddress":None}]}]}
    elf={"sha256":L,"machine":"AARCH64_ELF64",
         "methodPointerMappingVerified":False}
    return step1,step2,p1,method,elf


class RuntimeTraceGuards(unittest.TestCase):
    def test_actual_source_pointer_hierarchy_only_no_runtime_assumption(self):
        step1,step2,p1,method,elf=fixture()
        result=p2.build(step1,step2,p1,method,elf)
        self.assertEqual(result["counts"]["Canvas"],1)
        self.assertEqual(result["counts"]["CanvasScaler"],1)
        self.assertEqual(result["counts"]["SafeAreaAdapter"],6)
        self.assertEqual(result["counts"]["PanelHome2ComponentsInCandidate"],1)
        safe=next(x for x in result["sourceComponents"]
                  if x["category"]=="SafeArea")
        self.assertEqual(safe["ancestry"]["originalRectTransformPathIdsLeafToAncestor"],
                         [1001,1000])
        self.assertEqual(safe["ancestry"]["originalNearestCanvasRectTransformPathId"],
                         1000)
        self.assertFalse(result["runtimeAlignmentProven"])
        self.assertIsNone(result["runtimeAlignmentFormula"])
        self.assertFalse(result["sourceFieldApplicationAllowed"])
        self.assertFalse(result["unityAssetsChanged"])

    def test_external_parent_never_faked_into_canvas(self):
        step1,step2,p1,method,elf=fixture()
        node=step1["gameObjects"][1]
        node["sourceParentPointer"]={"fileId":3,"pathId":6666}
        result=p2.build(step1,step2,p1,method,elf)
        safe=next(x for x in result["sourceComponents"]
                  if x["componentPathId"]==510)
        self.assertIsNone(safe["ancestry"]["originalNearestCanvasRectTransformPathId"])
        self.assertEqual(safe["ancestry"]["sourceParentTraceStatus"],
                         "BLOCKED_PARENT_IN_EXTERNAL_SERIALIZED_FILE")

    def test_xapk_metadata_sha_conflict_and_fabricated_code_pointer_block(self):
        step1,step2,p1,method,elf=fixture()
        method["metadataSha256"]="f"*64
        with self.assertRaisesRegex(ValueError,"source identity"):
            p2.build(step1,step2,p1,method,elf)
        method["metadataSha256"]=M
        method["targetClassDefinitions"][0]["methods"][0]["nativeAddress"]=12345
        with self.assertRaisesRegex(ValueError,"Fake native method pointer"):
            p2.build(step1,step2,p1,method,elf)

    def test_original_component_owner_and_p1_gate_cannot_be_bypassed(self):
        step1,step2,p1,method,elf=fixture()
        p1["sourceFieldsVerifiedByTwoGeneratedSchemas"]=167
        with self.assertRaisesRegex(ValueError,"source identity"):
            p2.build(step1,step2,p1,method,elf)
        p1["sourceFieldsVerifiedByTwoGeneratedSchemas"]=168
        step2["componentsByCategory"]["SafeArea"][0]["originalObjectSha256"]="f"*64
        with self.assertRaisesRegex(ValueError,"provenance conflicts"):
            p2.build(step1,step2,p1,method,elf)


if __name__=="__main__":
    unittest.main()
