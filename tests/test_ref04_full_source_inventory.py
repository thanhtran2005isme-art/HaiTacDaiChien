"""REF04 Step 1: XAPK-only source component inventory and missing-field blockers."""
import copy
import importlib.util
import json
from pathlib import Path
import tempfile
import unittest

ROOT = Path(__file__).resolve().parents[1]
spec = importlib.util.spec_from_file_location(
    "ref04_all_src",ROOT/"tools/audit_ref04_full_source_inventory.py")
tool = importlib.util.module_from_spec(spec)
spec.loader.exec_module(tool)

FILENAME = "original_source_bundle__file25"
DIGEST = "a"*64
FILE = "e02e91470b2bd555b85e5073babfb59f.png"


def fixture():
    image_ids = list(range(1000,1265))
    extra = 1265
    ids = [10,20,2000] + image_ids + [extra]
    components = []
    decoded = []
    managed = []
    for cid in ids:
        kind = ("RectTransform" if cid == 10 else
                "Canvas" if cid == 20 else "MonoBehaviour")
        classname = ("UnityEngine.UI.HorizontalLayoutGroup"
                     if cid == 2000 else
                     "UnityEngine.UI.Image" if cid >= 1000 else None)
        record = {
            "pathId":cid,"kind":kind,
            "fieldStatus":"native" if kind!="MonoBehaviour" else
                "managed_fields_unavailable",
            "gameObjectPointer":{"fileId":0,"pathId":100},
        }
        if kind=="MonoBehaviour":
            record["rawEvidence"]={"sha256":DIGEST}
        components.append(record)
        src = {
            "pathId":cid,"kind":kind,"rectTransformId":10,
            "gameObjectId":100,
            "status":"NATIVE_FIELDS" if kind!="MonoBehaviour" else
                "NO_MANAGED_TYPETREE",
        }
        if kind=="RectTransform":
            src["fields"]={"m_AnchorMin":{"x":0,"y":0}}
        if kind=="Canvas":
            src["fields"]={"m_RenderMode":0}
        if classname:
            src["className"]=classname
        decoded.append(src)
        if cid>=1000 and cid!=2000:
            managed.append({
                "componentPathId":cid,"gameObjectPathId":100,
                "rectTransformPathId":10,"className":classname,
                "rawObjectSha256":DIGEST,
                "fields":[{"name":"m_Type","kind":"int","intValue":0}],
            })
    graph={
        "version":1,"classification":tool.GRAPH,
        "scenes":[{"sceneId":tool.SCENE,
                   "sourceSerializedFile":FILENAME,
                   "candidateRootTransform":10,
                   "nodes":[{
                       "rectTransformId":10,"gameObjectId":100,
                       "parent":{"fileId":0,"pathId":0},
                       "childTransformIds":[],"componentIds":ids,
                       "active":1,
                       "rect":{
                           "anchorMin":[0,0],"anchorMax":[1,1],
                           "pivot":[.5,.5],"sizeDelta":[0,0],
                           "anchoredPosition":[0,0],
                           "localScale":[0,0,0],
                           "localRotation":[0,0,0,1],
                       }}],
                   "components":components,
                   "stats":{"componentReferences":len(ids)}
                 }],
    }
    deep={"schemaVersion":1,"scenes":[{
        "sceneId":tool.SCENE,"sourceFile":FILENAME,
        "components":decoded
    }]}
    verified={
        "classification":tool.VERIFIED,"verifiedComponents":1108,
        "verifiedFieldValues":7451,"singleBackendExcludedComponents":93,
        "singleBackendExcludedFieldValues":651,
        "scenes":[{"sceneId":tool.SCENE,"components":managed}],
    }
    linked=[{"imageComponentPathId":cid,
             "gameObjectPathId":100,"rectTransformPathId":10,
             "sourceObjectSha256":DIGEST,"spriteFile":FILE}
            for cid in image_ids]
    visual={"classification":tool.VISUAL,
            "sourceBindings":963,
            "scenes":[{"sceneId":tool.SCENE,"bindings":linked}]}
    geometry={"classification":tool.GEOMETRY,
              "sceneId":tool.SCENE,"sourceBindings":265,
              "images":[{
                  "componentPathId":cid,"gameObjectPathId":100,
                  "rectTransformPathId":10,
                  "spriteFile":FILE,"applyGeometry":True,
                  "nativeSpriteGeometryStatus":"NATIVE_SPRITE_GEOMETRY_VERIFIED",
                  "sourceRectSize":[84,92],
                  "sourceTextureRectOffset":[0,0],
                  "pixelsPerUnit":100,"border":[0,0,0,0],
              } for cid in image_ids]}
    review={
        "classification":tool.REVIEW,
        "componentCount":93,"excludedFieldValues":651,
        "components":[{
            "sceneId":tool.SCENE,"componentPathId":2000,
            "gameObjectPathId":100,"rectTransformPathId":10,
            "rawObjectSha256":DIGEST,
            "prefabImportAllowed":False,
            "fieldNames":["m_ChildAlignment"],"fieldCount":7
        }]
    }
    hashes={k:k[0]*64 for k in ("graph","deep","verified","visual",
                               "geometry","review","native")}
    verified["sourceGraphSha256"]=hashes["graph"]
    visual["sourceGraphSha256"]=hashes["graph"]
    visual["verifiedUiPlanSha256"]=hashes["verified"]
    geometry["sourceGraphSha256"]=hashes["graph"]
    geometry["verifiedUiPlanSha256"]=hashes["verified"]
    geometry["verifiedVisualPlanSha256"]=hashes["visual"]
    geometry["nativeGeometryEvidenceSha256"]=hashes["native"]
    return graph,deep,verified,visual,geometry,review,hashes


class FullRef04Inventory(unittest.TestCase):
    def test_every_gameobject_component_and_all_266_images_present(self):
        result=tool.inventory(*fixture())
        count=result["counts"]
        self.assertEqual(count["gameObjects"],1)
        self.assertEqual(count["serializedComponentRecords"],269)
        self.assertEqual(count["dualVerifiedManagedComponents"],266)
        self.assertEqual(count["allDualVerifiedImageComponents"],266)
        self.assertEqual(count["originalSpriteLinkedImages"],265)
        self.assertEqual(count["verifiedImagesWithoutSourceSprite"],1)
        self.assertEqual(count["singlyVerifiedLayoutComponentsBlocked"],1)
        self.assertFalse(result["sourceFieldApplicationAllowed"])
        self.assertFalse(result["root"]["runtimeViewportProven"])
        self.assertEqual(result["gameObjects"][0]["rectSource"]["localScale"],[0,0,0])
        self.assertEqual(result["gameObjects"][0]["components"][0]["componentPathId"],10)

    def test_missing_source_component_identity_rejected(self):
        vals=list(fixture())
        vals[1]["scenes"][0]["components"].pop()
        with self.assertRaisesRegex(ValueError,"Incomplete original"):
            tool.inventory(*vals)

    def test_mismatched_source_image_sprite_owner_rejected(self):
        vals=list(fixture())
        vals[3]["scenes"][0]["bindings"][0]["gameObjectPathId"]=999
        with self.assertRaisesRegex(ValueError,"Image/Sprite source owner"):
            tool.inventory(*vals)

    def test_single_backend_field_values_never_promoted_to_verified(self):
        out=tool.inventory(*fixture())
        c=out["gameObjects"][0]["components"]
        layout=next(x for x in c if x["componentPathId"]==2000)
        self.assertEqual(layout["verificationStatus"],
                         "SINGLE_BACKEND_FIELD_VALUES_BLOCKED")
        self.assertEqual(layout["verifiedSerializedFields"],{})
        self.assertIn("m_ChildAlignment",layout["singleBackendFieldNames"])

    def test_invalid_document_hash_rejected(self):
        vals=list(fixture())
        vals[6]["verified"]="f"*64
        with self.assertRaisesRegex(ValueError,"missing/stale"):
            tool.inventory(*vals)

    def test_report_stays_separate_read_only_and_source_pointers_preserved(self):
        with tempfile.TemporaryDirectory() as temp:
            path=Path(temp)/"output/ref04-full-source-inventory.json"
            out=tool.inventory(*fixture())
            tool.write_report(out,path)
            doc=json.loads(path.read_text(encoding="utf-8"))
            self.assertEqual(doc["counts"]["originalSpriteLinkedImages"],265)
            self.assertTrue(path.with_suffix(".md").is_file())
            self.assertFalse(doc["unityAssetsChanged"])


if __name__=="__main__":
    unittest.main()
