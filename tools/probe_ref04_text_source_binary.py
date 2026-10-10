#!/usr/bin/env python3
"""Read-only XAPK source Text MonoBehaviour strict TypeTree probe for REF04.

Never inserts fake text/font/layout. Exact source Unity/IL2CPP pair, Script
pointer, GameObject owner, raw component SHA and check_read=True required.
Any source string is represented ONLY by UTF-8 SHA and length (private report);
runtime text, localization language and final HUD state are NOT inferred.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
import math
from pathlib import Path

import decode_xapk_ui_provenance as source
import export_local_ui_layout as layout
import il2cpp_refs as refs
import recover_managed_ui_fields as binary

ROOT=Path(__file__).resolve().parents[1]
SCENE="REF04-home-crew"
CLASS="UnityEngine.UI.Text"
FIELDS=("m_Text","m_Font","m_FontSize","m_FontStyle",
        "m_Alignment","m_LineSpacing","m_Color","m_RaycastTarget")
# Unity 2022.3 UGUI Text stores FontData within the Text MonoBehaviour.
# Strictly follow only actual source child fields; no synthesized font defaults.
FONTDATA_FIELDS=frozenset(("m_Font","m_FontSize","m_FontStyle",
                           "m_Alignment","m_LineSpacing"))
TOTAL=62


def source_text_nodes(deep,graph):
    if (graph.get("classification") !=
            "SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE" or
        deep.get("schemaVersion") != 1):
        raise ValueError("XAPK source graph/deep classification mismatch")
    d=[x for x in deep["scenes"] if x["sceneId"]==SCENE]
    g=[x for x in graph["scenes"] if x["sceneId"]==SCENE]
    if len(d)!=1 or len(g)!=1 or d[0]["sourceFile"]!=g[0]["sourceSerializedFile"]:
        raise ValueError("REF04 original source scene is not uniquely identified")
    graph_rows={x["pathId"]:x for x in g[0]["components"]}
    text=[x for x in d[0]["components"] if x.get("className")==CLASS]
    if len(text)!=TOTAL:
        raise ValueError("REF04 XAPK Text count differs from source")
    rows={}
    for row in text:
        cid=row["pathId"]
        original=graph_rows.get(cid)
        if (cid in rows or original is None or
            original["kind"]!="MonoBehaviour" or
            original.get("rawEvidence",{}).get("sha256") is None or
            row["kind"]!="MonoBehaviour" or
            row.get("assembly") is None or
            row.get("scriptPointer")!=original.get("monoScriptPointer") or
            row.get("gameObjectId")!=
                original.get("gameObjectPointer",{}).get("pathId") or
            row.get("scriptResolution") not in
                ("LOCAL_PATHID","EXTERNAL_RESOLVED","UNITYPY_DEREF",
                 "GLOBAL_XAPK_EXACT_FILE_ALIAS_PATHID")):
            raise ValueError("Original XAPK Text owner/MonoScript/bytes not proven")
        rows[cid]=(row,original)
    return rows,g[0]["sourceSerializedFile"]


def strict_text_probe(reader,row,original,generator):
    raw=reader.get_raw_data()
    if hashlib.sha256(raw).hexdigest()!=original["rawEvidence"]["sha256"]:
        return "BLOCKED_SOURCE_SHA256_CHANGED",{}
    try:
        native=binary.exact_source_unity_header(reader)
        generated=generator.get_nodes_up(row["assembly"],row["className"])
        if generated is None:
            return "BLOCKED_NO_SOURCE_TEXT_TYPETREE",{}
        nodes=binary.verified_native_header_root(generated,native)
        obj=reader.read_typetree(nodes=nodes,check_read=True)
    except Exception as exc:
        return "BLOCKED_STRICT_SOURCE_TEXT_PARSE_"+type(exc).__name__,{}
    if (not isinstance(obj,dict) or
        refs.pptr(obj.get("m_GameObject"))!=(0,row["gameObjectId"]) or
        refs.pptr(obj.get("m_Script"))!=(
            row["scriptPointer"]["fileId"],row["scriptPointer"]["pathId"]) or
        obj.get("m_Enabled")!=row.get("nativeEnabled")):
        return "BLOCKED_TEXT_NATIVE_HEADER_OR_OWNER_MISMATCH",{}
    selected={}
    fontdata=obj.get("m_FontData")
    if fontdata is not None and not isinstance(fontdata,dict):
        return "BLOCKED_SOURCE_TEXT_FONTDATA_NOT_MAPPING",{}
    for key in FIELDS:
        in_root=key in obj
        in_fontdata=key in FONTDATA_FIELDS and isinstance(fontdata,dict) and key in fontdata
        if in_root and in_fontdata:
            return "BLOCKED_SOURCE_TEXT_DUPLICATE_FONT_FIELD_LOCATIONS",{}
        if not in_root and not in_fontdata:
            continue
        val=obj[key] if in_root else fontdata[key]
        exact_path=key if in_root else "m_FontData."+key
        if key=="m_Text":
            if not isinstance(val,str):
                return "BLOCKED_SOURCE_TEXT_TYPE_MISMATCH",{}
            rawtext=val.encode("utf-8")
            selected[key]={"sourceUtf8Sha256":hashlib.sha256(rawtext).hexdigest(),
                           "utf8Bytes":len(rawtext),
                           "sourceTextIsEmpty":not bool(val)}
        elif key=="m_Font":
            fileid,pathid=refs.pptr(val)
            if fileid<0 or pathid<0:
                return "BLOCKED_SOURCE_TEXT_FONT_POINTER_INVALID",{}
            selected[key]={"sourceFileId":fileid,"sourcePathId":pathid,
                           "sourceFieldPath":exact_path}
        elif key=="m_Color":
            color=source.ui.plain(val)
            if not isinstance(color,dict) or set(color)!={"r","g","b","a"}:
                return "BLOCKED_SOURCE_TEXT_COLOR_INVALID",{}
            selected[key]=color
        elif type(val) in (int,bool,float) and (
            type(val) is not float or math.isfinite(val)):
            selected[key]=val
        else:
            return "BLOCKED_SOURCE_TEXT_FIELD_TYPE_"+key,{}
    if not selected:
        return "BLOCKED_NO_SOURCE_TEXT_FIELDS",{}
    return "SOURCE_TEXT_STRICT_SINGLE_BACKEND_NOT_IMPORTED",selected


def compare_decoders(a, b):
    """Accept matching strict parsed source fields only, never infer runtime UI."""
    primary,original_fields=a
    secondary,second_fields=b
    if primary!="SOURCE_TEXT_STRICT_SINGLE_BACKEND_NOT_IMPORTED":
        return primary,{},secondary
    if secondary!="SOURCE_TEXT_STRICT_SINGLE_BACKEND_NOT_IMPORTED":
        return primary,original_fields,secondary
    if original_fields != second_fields:
        return "BLOCKED_TEXT_INDEPENDENT_BACKEND_FIELD_CONFLICT",{},secondary
    return ("TWO_BACKENDS_SAME_SERIALIZED_TEXT_FIELDS_NOT_IMPORTED",
            original_fields,secondary)


def report(rows, results, binary_proof):
    if set(rows)!=set(results):
        raise ValueError("Full REF04 original Text inventory was not probed")
    by_status=collections.Counter()
    items=[]
    for cid,(src,original) in sorted(rows.items()):
        values=results[cid]
        if len(values) not in (2,3):
            raise ValueError("Text source backend result invalid")
        outcome,fields=values[:2]
        secondary=values[2] if len(values)==3 else "NOT_RUN"
        by_status[outcome]+=1
        items.append({
            "componentPathId":cid,"rectTransformPathId":src["rectTransformId"],
            "gameObjectPathId":src["gameObjectId"],
            "sourceObjectSha256":original["rawEvidence"]["sha256"],
            "sourceMonoScriptPointer":src["scriptPointer"],
            "verificationStatus":outcome,
            "secondIndependentBackendStatus":secondary,
            "sourceTextFieldEvidence":fields,
            "notProven":"Runtime language, localize, panel state, "
                        "dynamic text assignment and pixel-perfect font",
            "unityImportAllowed":False,
        })
    return {
        "schemaVersion":1,
        "classification":"REF04_ORIGINAL_XAPK_TEXT_62_SOURCE_BINARY_PROBE",
        "sourceTextComponents":TOTAL,
        "sourceBinary":binary_proof,
        "statuses":dict(sorted(by_status.items())),
        "dualBackendAgreedComponentCount":by_status[
            "TWO_BACKENDS_SAME_SERIALIZED_TEXT_FIELDS_NOT_IMPORTED"],
        "rawTextContentPublished":False,
        "runtimeTextProven":False,
        "unityImportAllowed":False,
        "textComponents":items,
    }


def execute(root=ROOT,*,xapk=None,unitypy=None):
    if unitypy is None:
        import UnityPy as unitypy
    o=root/"output"
    graph=json.loads((o/"original-unity-graph.json").read_text(encoding="utf-8"))
    deep=json.loads((o/"deep-ui-source-evidence.json").read_text(encoding="utf-8"))
    rows,source_file=source_text_nodes(deep,graph)
    scene=next(s for s in json.loads((root/
        "unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json")
        .read_text(encoding="utf-8"))["scenes"] if s["id"]==SCENE)
    if scene["source"]!=source_file:
        raise ValueError("REF04 source serialized bundle mismatch")
    xapk=xapk or next(iter(sorted(root.glob("*.xapk"))),None)
    if xapk is None:
        raise FileNotFoundError("Original XAPK absent")
    version=binary.exact_unity_version(deep["scenes"])
    generator,proof=binary.source_generator(xapk,version,backend="AssetStudio")
    second_generator=None
    second_issue="NOT_RUN"
    try:
        second_generator,proof2=binary.source_generator(
            xapk,version,backend="AssetRipper")
        if (proof2["library"]["sha256"]!=proof["library"]["sha256"] or
            proof2["metadata"]["sha256"]!=proof["metadata"]["sha256"] or
            proof2["gameUnityVersion"]!=proof["gameUnityVersion"]):
            raise ValueError("Second source generator used different XAPK")
    except binary.RecoveryBlocked as exc:
        second_issue="BLOCKED_SECOND_BACKEND_GENERATOR_"+exc.code
    results={}
    class Interceptor:
        def load(self,blob):
            env=unitypy.load(blob)
            groups=collections.defaultdict(dict)
            for reader in env.objects:
                groups[id(reader.assets_file)][int(reader.path_id)]=reader
            try:
                data=layout.choose_serialized_file(scene,groups)
            except ValueError:
                return env
            if results:
                raise ValueError("Duplicate REF04 source bundle during text recheck")
            for cid,(row,original) in sorted(rows.items()):
                reader=data.get(cid)
                if reader is None or reader.type.name!="MonoBehaviour":
                    raise ValueError("Original source Text component disappeared")
                first=strict_text_probe(reader,row,original,generator)
                if second_generator is None:
                    results[cid]=(first[0],first[1],second_issue)
                else:
                    second=strict_text_probe(reader,row,original,second_generator)
                    results[cid]=compare_decoders(first,second)
            return env
    layout.build(root=root,xapk=xapk,unitypy=Interceptor())
    proof_small={
        "sourcePair":"EXACT_XAPK_IL2CPP_BINARY_PAIR",
        "backends":(["AssetStudio","AssetRipper"]
                    if second_generator is not None else ["AssetStudio"]),
        "gameUnityVersion":version,
        "librarySha256":proof["library"]["sha256"],
        "metadataSha256":proof["metadata"]["sha256"],
    }
    doc=report(rows,results,proof_small)
    doc["sourceSerializedFile"]=source_file
    target=o/"ref04-text-source-binary-evidence.json"
    tmp=target.with_suffix(".tmp")
    tmp.write_text(json.dumps(doc,ensure_ascii=False,indent=2)+"\n",
                   encoding="utf-8")
    tmp.replace(target)
    print(json.dumps({
        "classification":doc["classification"],
        "sourceTextComponents":TOTAL,
        "statuses":doc["statuses"],
        "dualBackendAgreedComponentCount":doc[
            "dualBackendAgreedComponentCount"],
        "rawTextContentPublished":False,
        "unityAssetsChanged":False,
    },sort_keys=True))
    return doc


if __name__=="__main__":
    p=argparse.ArgumentParser(description=__doc__)
    p.add_argument("--root",type=Path,default=ROOT)
    args=p.parse_args()
    execute(args.root.resolve())
