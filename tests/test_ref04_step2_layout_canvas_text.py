"""No guessed REF04 Step 2 Canvas/LayoutGroup/Text fields or positions."""
import importlib.util
from pathlib import Path
import tempfile
import unittest

ROOT=Path(__file__).resolve().parents[1]
spec=importlib.util.spec_from_file_location(
    "ref04step2",ROOT/"tools/audit_ref04_step2_layout_canvas_text.py")
tool=importlib.util.module_from_spec(spec)
spec.loader.exec_module(tool)
H="a"*64


def full_inventory():
    names=(["UnityEngine.UI.CanvasScaler"]+
           ["UnityEngine.UI.HorizontalLayoutGroup"]*18+
           ["UnityEngine.UI.VerticalLayoutGroup"]*6+
           ["UnityEngine.UI.Text"]*62+
           ["SafeAreaAdapter"]*6+
           ["UnityEngine.UI.Image"]*299+
           ["UnityEngine.UI.Button"]*47)
    names+=["UnverifiedSourceScript"]*(675-len(names))
    self_id=2000
    managed=[]
    for name in names:
        fields={"m_UiScaleMode":{"name":"m_UiScaleMode","kind":"int",
                                 "intValue":1}} if name=="UnityEngine.UI.CanvasScaler" else {}
        rec={
            "componentPathId":self_id,"gameObjectPathId":1,
            "rectTransformPathId":1000,"nativeKind":"MonoBehaviour",
            "monoScriptClass":name,"sourceComponentEnabled":True,
            "verificationStatus":("SINGLE_BACKEND_FIELD_VALUES_BLOCKED"
                if "LayoutGroup" in name else "UNVERIFIED_FIELDS"),
            "verifiedSerializedFields":fields,
            "rawSourceObjectSha256":H,
        }
        if "LayoutGroup" in name:
            rec["singleBackendFieldNames"]=[
                "m_Padding","m_Spacing","m_ChildAlignment",
                "m_ChildControlWidth","m_ChildControlHeight",
                "m_ChildForceExpandWidth","m_ChildForceExpandHeight"]
        managed.append(rec)
        self_id+=1
    native=[{
        "componentPathId":i,"gameObjectPathId":1,
        "rectTransformPathId":1000,"nativeKind":"CanvasRenderer",
        "monoScriptClass":None,"sourceComponentEnabled":None,
        "verificationStatus":"XAPK_NATIVE_KIND_OR_HEADER_ONLY",
        "verifiedSerializedFields":{},"rawSourceObjectSha256":None,
    } for i in range(500,885)]
    rects=[{
        "componentPathId":i,"gameObjectPathId":1,
        "rectTransformPathId":1000,"nativeKind":"RectTransform",
        "monoScriptClass":None,"sourceComponentEnabled":None,
        "verificationStatus":"XAPK_NATIVE_KIND_OR_HEADER_ONLY",
        "verifiedSerializedFields":{},"rawSourceObjectSha256":None,
    } for i in range(1000,1503)]
    canvas=[{
        "componentPathId":400,"gameObjectPathId":1,
        "rectTransformPathId":1000,"nativeKind":"Canvas",
        "monoScriptClass":None,"sourceComponentEnabled":True,
        "verificationStatus":"XAPK_NATIVE_SERIALIZED_FIELD_SUBSET",
        "verifiedSerializedFields":{"m_RenderMode":0},
        "rawSourceObjectSha256":None,
    }]
    all_parts=canvas+native+rects+managed
    assert len(all_parts)==1564
    nodes=[{
        "rectTransformPathId":1000,"gameObjectPathId":1,
        "rectSource":{"anchorMin":[0,0],"anchorMax":[1,1],
                      "sizeDelta":[0,0],"localScale":[0,0,0]},
        "sourceSiblingOrder":None,"sourceActive":1,
        "components":all_parts,
    }]
    return {
        "classification":
            "REF04_ALL_SOURCE_CANDIDATE_UI_COMPONENT_INVENTORY_READ_ONLY",
        "sceneId":tool.SCENE,"sourceFieldApplicationAllowed":False,
        "sourceSerializedFile":"XAPK-file",
        "counts":{"gameObjects":503,"serializedComponentRecords":1564,
                  "allDualVerifiedImageComponents":299},
        "gameObjects":nodes,
    }


class Step2(unittest.TestCase):
    def test_classify_actual_xapk_component_classes_but_do_not_apply_fields(self):
        report=tool.inventory(full_inventory())
        self.assertEqual(report["counts"]["Canvas"],1)
        self.assertEqual(report["counts"]["CanvasScaler"],1)
        self.assertEqual(report["counts"]["LayoutGroup"],24)
        self.assertEqual(report["counts"]["Text"],62)
        self.assertEqual(report["counts"]["SafeArea"],6)
        self.assertEqual(report["layoutGroupFieldValuesStillBlockedFromUnity"],168)
        self.assertFalse(report["sourceRuntimeLayoutProven"])
        self.assertFalse(report["sourceRuntimeTextProven"])
        self.assertFalse(report["sourceFieldApplicationAllowed"])
        self.assertFalse(report["unityAssetsChanged"])
        self.assertEqual(report["componentsByCategory"]["Canvas"][0][
            "nativeCanvasFieldsExtracted"]["m_RenderMode"],0)
        self.assertEqual(report["componentsByCategory"]["Text"][0][
            "renderedText"],None)
        self.assertEqual(report["componentsByCategory"]["CanvasScaler"][0][
            "sourceRectTransformValues"]["localScale"],[0,0,0])

    def test_text_is_not_synthesized_from_script_name(self):
        report=tool.inventory(full_inventory())
        texts=report["componentsByCategory"]["Text"]
        self.assertEqual(len(texts),62)
        self.assertTrue(all(x["renderedText"] is None for x in texts))
        self.assertTrue(all(x["canBeAppliedToUnity"] is False for x in texts))
        self.assertTrue(all("m_Font" in x["textFieldsNotDoubleVerified"]
                            for x in texts))
        self.assertEqual(report["originalTextComponentsVerifiedByTwoBackends"],0)

    def test_third_backend_id_or_sha_conflict_blocks(self):
        src=full_inventory()
        layout=next(x for x in src["gameObjects"][0]["components"]
                    if (x.get("monoScriptClass") or "").endswith("LayoutGroup"))
        third={
            "classification":"REF04_THIRD_BACKEND_SOURCE_LAYOUTGROUP_RECHECK_READ_ONLY",
            "sourceSerializedFile":"XAPK-file",
            "ref04LayoutGroupComponents":24,"sourceLayoutFieldNamesExamined":168,
            "unityImportAllowed":False,
            "layoutGroups":[{
                "componentPathId":c["componentPathId"],
                "sourceObjectSha256":H,"gameObjectPathId":1,
                "rectTransformPathId":1000,
                "className":c["monoScriptClass"],
                "thirdBackendStatus":"THIRD_BACKEND_FULL_SOURCE_OBJECT_AGREES_NOT_IMPORTED",
            } for c in src["gameObjects"][0]["components"]
                if "LayoutGroup" in (c.get("monoScriptClass") or "")],
        }
        self.assertTrue(tool.inventory(src,third)["thirdBackendRunProvided"])
        third["layoutGroups"][0]["sourceObjectSha256"]="b"*64
        with self.assertRaisesRegex(ValueError,"source LayoutGroup ID/hash"):
            tool.inventory(src,third)

    def test_62_binary_rounded_trip_text_source_fields_do_not_prove_runtime(self):
        src=full_inventory()
        texts=[c for c in src["gameObjects"][0]["components"] if
               c.get("monoScriptClass")=="UnityEngine.UI.Text"]
        report_rows=[{
            "componentPathId":c["componentPathId"],
            "gameObjectPathId":c["gameObjectPathId"],
            "rectTransformPathId":c["rectTransformPathId"],
            "sourceObjectSha256":H,
            "verificationStatus":
                "TWO_BACKENDS_SAME_SERIALIZED_TEXT_FIELDS_NOT_IMPORTED",
            "sourceTextFieldEvidence":{
                "m_Text":{"sourceUtf8Sha256":"a"*64,"utf8Bytes":12},
                "m_Font":{"sourceFileId":0,"sourcePathId":456},
                "m_FontSize":22,
            },
        } for c in texts]
        probe={
            "classification":"REF04_ORIGINAL_XAPK_TEXT_62_SOURCE_BINARY_PROBE",
            "sourceSerializedFile":"XAPK-file","sourceTextComponents":62,
            "unityImportAllowed":False,"rawTextContentPublished":False,
            "textComponents":report_rows,
        }
        result=tool.inventory(src,None,probe)
        self.assertEqual(result["originalTextComponentsVerifiedByTwoBackends"],62)
        self.assertFalse(result["sourceRuntimeTextProven"])
        item=result["componentsByCategory"]["Text"][0]
        self.assertEqual(item["textSourceFieldsTwoBackendsAgreed"]["m_FontSize"],22)
        self.assertIn("m_Alignment",item["textFieldsNotDoubleVerified"])
        self.assertNotIn("m_Font",item["textFieldsNotDoubleVerified"])
        self.assertIsNone(item["renderedText"])
        self.assertFalse(item["canBeAppliedToUnity"])

    def test_missing_component_fails_closed(self):
        src=full_inventory()
        src["gameObjects"][0]["components"].pop()
        with self.assertRaisesRegex(ValueError,"traversal incomplete"):
            tool.inventory(src)

    def test_private_output_json_and_md(self):
        with tempfile.TemporaryDirectory() as temp:
            path=Path(temp)/"output/ref04-step2-layout-canvas-text.json"
            tool.write(tool.inventory(full_inventory()),path)
            self.assertTrue(path.is_file())
            self.assertTrue(path.with_suffix(".md").is_file())


if __name__=="__main__":
    unittest.main()
