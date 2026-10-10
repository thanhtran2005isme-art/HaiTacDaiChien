"""REF04 62 original Text MonoBehaviours are decoded only with source byte proof."""
import hashlib
import importlib.util
from pathlib import Path
from unittest.mock import patch
import unittest

ROOT=Path(__file__).resolve().parents[1]
spec=importlib.util.spec_from_file_location(
    "ref04text",ROOT/"tools/probe_ref04_text_source_binary.py")
tool=importlib.util.module_from_spec(spec)
spec.loader.exec_module(tool)
RAW=b"exact serialized XAPK text object bytes"
SHA=hashlib.sha256(RAW).hexdigest()


def documents():
    cs=[]
    ds=[]
    for cid in range(100,162):
        cs.append({
            "pathId":cid,"kind":"MonoBehaviour",
            "rawEvidence":{"sha256":SHA},
            "monoScriptPointer":{"fileId":1,"pathId":55},
            "gameObjectPointer":{"fileId":0,"pathId":200},
        })
        ds.append({
            "pathId":cid,"kind":"MonoBehaviour",
            "className":tool.CLASS,"assembly":"UnityEngine.UI",
            "scriptResolution":"GLOBAL_XAPK_EXACT_FILE_ALIAS_PATHID",
            "scriptPointer":{"fileId":1,"pathId":55},
            "gameObjectId":200,"rectTransformId":300,
            "nativeEnabled":True,
        })
    return (
        {"classification":"SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE",
         "scenes":[{"sceneId":tool.SCENE,
                    "sourceSerializedFile":"XAPK_test","components":cs}]},
        {"schemaVersion":1,
         "scenes":[{"sceneId":tool.SCENE,"sourceFile":"XAPK_test",
                    "components":ds}]},
    )


class Reader:
    def __init__(self,doc):
        self.doc=doc
    def get_raw_data(self):
        return RAW
    def read_typetree(self,**kwargs):
        if kwargs.get("check_read") is not True:
            raise AssertionError("Must full-object strict check")
        return self.doc


class Generator:
    def get_nodes_up(self,assembly,classname):
        if assembly!="UnityEngine.UI" or classname!=tool.CLASS:
            raise ValueError("unexpected source")
        return object()


class TextProbe(unittest.TestCase):
    def test_exact_62_real_component_ids_and_script_pointers_required(self):
        graph,deep=documents()
        rows,name=tool.source_text_nodes(deep,graph)
        self.assertEqual(len(rows),62)
        self.assertEqual(name,"XAPK_test")
        deep["scenes"][0]["components"][2]["scriptPointer"]={
            "fileId":2,"pathId":55}
        with self.assertRaisesRegex(ValueError,"not proven"):
            tool.source_text_nodes(deep,graph)

    def test_source_string_is_hashed_not_exposed_or_promoted(self):
        graph,deep=documents()
        rows,_=tool.source_text_nodes(deep,graph)
        cid=100
        row,original=rows[cid]
        content={
            "m_GameObject":{"m_FileID":0,"m_PathID":200},
            "m_Script":{"m_FileID":1,"m_PathID":55},
            "m_Enabled":True,
            "m_Text":"Only original XAPK snapshot",
            "m_Font":{"m_FileID":0,"m_PathID":876},
            "m_FontSize":18,"m_Alignment":4,
            "m_Color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},
        }
        with patch.object(tool.binary,"exact_source_unity_header",
                          return_value=object()),patch.object(
                              tool.binary,"verified_native_header_root",
                              return_value=object()):
            status,fields=tool.strict_text_probe(
                Reader(content),row,original,Generator())
        self.assertEqual(status,"SOURCE_TEXT_STRICT_SINGLE_BACKEND_NOT_IMPORTED")
        self.assertNotIn("Only original XAPK snapshot",str(fields))
        self.assertEqual(fields["m_Text"]["utf8Bytes"],
                         len("Only original XAPK snapshot"))
        self.assertEqual(fields["m_Font"]["sourcePathId"],876)
        self.assertEqual(fields["m_FontSize"],18)
        sample={n:(status,fields) for n in rows}
        out=tool.report(rows,sample,{"backend":"AssetStudio"})
        self.assertFalse(out["unityImportAllowed"])
        self.assertFalse(out["rawTextContentPublished"])
        self.assertFalse(out["runtimeTextProven"])

    def test_original_serialized_sha_changed_is_blocked(self):
        graph,deep=documents()
        rows,_=tool.source_text_nodes(deep,graph)
        row,orig=rows[100]
        orig["rawEvidence"]["sha256"]="0"*64
        status,fields=tool.strict_text_probe(
            Reader({}),row,orig,Generator())
        self.assertEqual(status,"BLOCKED_SOURCE_SHA256_CHANGED")
        self.assertFalse(fields)

    def test_no_missing_text_components_are_silently_skipped(self):
        graph,deep=documents()
        rows,_=tool.source_text_nodes(deep,graph)
        with self.assertRaisesRegex(ValueError,"not probed"):
            tool.report(rows,{100:("BLOCKED_PARSE",{})},{})


if __name__=="__main__":
    unittest.main()
