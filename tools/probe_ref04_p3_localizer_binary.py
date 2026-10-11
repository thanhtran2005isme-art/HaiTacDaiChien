#!/usr/bin/env python3
"""Source-only original I2 localizer term probe via two IL2CPP-derived schemas.

52 Localize/TextLocalizeChecker components are target candidates. A successful
serialized string hash is not the runtime selected-language translation or
proof it writes any particular Text. All XAPK source strings stay private.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
from pathlib import Path

import decode_xapk_ui_provenance as source
import export_local_ui_layout as layout
import il2cpp_refs as refs
import recover_managed_ui_fields as binary
import ref04_layout_raw_parser as raw_replay

ROOT=Path(__file__).resolve().parents[1]
SCENE="REF04-home-crew"
CLASS="REF04_P3_ORIGINAL_I2_TERMS_TWO_GENERATOR_SOURCE_PROBE"
TERMS=frozenset(("mTerm","m_Term","Term","mTermSecondary",
                 "SecondaryTerm","mSecondaryTerm","m_SecondaryTerm"))

def strict_probe(reader, original_row, original_record, generator):
    raw=reader.get_raw_data()
    if hashlib.sha256(raw).hexdigest()!=original_record["originalObjectSha256"]:
        return "BLOCKED_SOURCE_RAW_SHA_MISMATCH",{}
    try:
        nodes=generator.get_nodes_up(original_row["assembly"],original_row["className"])
        if nodes is None: return "BLOCKED_SOURCE_TYPETREE_MISSING",{}
        native=binary.exact_source_unity_header(reader)
        merged=binary.verified_native_header_root(nodes,native)
        parsed=reader.read_typetree(nodes=merged,check_read=True)
    except binary.RecoveryBlocked as exc:
        # Stable source-parser phase, never source bytes or game text.
        return "BLOCKED_SOURCE_SCHEMA_"+exc.phase+"_"+exc.code,{}
    except Exception as exc:
        return "BLOCKED_SOURCE_SCHEMA_PARSE_"+type(exc).__name__,{}
    ptr=original_row["scriptPointer"]
    if (not isinstance(parsed,dict) or
        refs.pptr(parsed.get("m_GameObject"))!=(0,original_row["gameObjectId"]) or
        refs.pptr(parsed.get("m_Script"))!=(ptr["fileId"],ptr["pathId"]) or
        parsed.get("m_Enabled")!=original_row.get("nativeEnabled")):
        return "BLOCKED_SOURCE_MONOSCRIPT_OR_OWNER",{}
    fields={}
    for name in sorted(TERMS.intersection(parsed)):
        value=parsed[name]
        if not isinstance(value,str):
            return "BLOCKED_ORIGINAL_TERM_FIELD_TYPE",{}
        text=value.encode("utf-8")
        fields[name]={"originalUtf8Sha256":hashlib.sha256(text).hexdigest(),
                      "originalByteLength":len(text)}
    return "STRICT_SOURCE_PARSED_SOURCE_TERM_KEYS_UNVERIFIED_RUNTIME",fields

def safe_ripper_raw_failure(exc):
    """Stable failure category only; never leak original game bytes or values."""
    msg=str(exc)
    if isinstance(exc,raw_replay.RawWalkBlocked):
        patterns=(
            ("Unrecognized source field type: ","UNSUPPORTED_SCHEMA_LEAF_TYPE"),
            ("Serialized object ended before schema field","TYPE_TREE_OVERRUN"),
            ("Source array element count invalid","UNTRUSTED_ARRAY_LENGTH"),
            ("Source string byte length invalid","UNTRUSTED_STRING_LENGTH"),
            ("Source schema field consumed no bytes","EMPTY_TYPE_TREE_NODE"),
            ("Incomplete source TypeTree node","INCOMPLETE_SOURCE_NODE"),
            ("Unsupported source array schema","UNSUPPORTED_ARRAY_SCHEMA"),
            ("TypeTree depth/node budget exceeded","TYPE_TREE_LIMIT"),
            ("Ambiguous duplicate source schema field","DUPLICATE_SOURCE_FIELD"),
            ("Original serialized object invalid or oversized","ORIGINAL_OBJECT_SIZE"),
        )
        for prefix,cat in patterns:
            if msg.startswith(prefix):
                return cat
    return "OTHER_"+type(exc).__name__


def raw_ripper_probe(reader,original_row,original_record,generator):
    """Independent struct-based walk of all original bytes under Ripper schema.

    This does NOT invent missing fields or validate runtime localization;
    it is only attempted after UnityPy's strict Ripper decoder returns EOF.
    """
    source_bytes=reader.get_raw_data()
    if hashlib.sha256(source_bytes).hexdigest()!=original_record["originalObjectSha256"]:
        return "BLOCKED_RAW_RIPPER_SOURCE_SHA_MISMATCH",{}
    try:
        nodes=generator.get_nodes_up(original_row["assembly"],original_row["className"])
        if nodes is None:
            return "BLOCKED_RAW_RIPPER_TYPETREE_MISSING",{}
        schema=binary.verified_native_header_root(
            nodes,binary.exact_source_unity_header(reader))
        cur=raw_replay.Cursor(source_bytes,raw_replay.byte_order(reader))
        parsed=cur.walk(schema)
        if cur.pos!=len(source_bytes):
            return "BLOCKED_RAW_RIPPER_UNCONSUMED_OBJECT_BYTES",{}
    except (binary.RecoveryBlocked,raw_replay.RawWalkBlocked,
            UnicodeError,ValueError,KeyError,TypeError) as exc:
        return "BLOCKED_RAW_RIPPER_"+safe_ripper_raw_failure(exc),{}
    ptr=original_row["scriptPointer"]
    if (not isinstance(parsed,dict) or
        refs.pptr(parsed.get("m_GameObject"))!=(0,original_row["gameObjectId"]) or
        refs.pptr(parsed.get("m_Script"))!=(ptr["fileId"],ptr["pathId"]) or
        parsed.get("m_Enabled")!=original_row.get("nativeEnabled")):
        return "BLOCKED_RAW_RIPPER_OWNER_POINTER_MISMATCH",{}
    fields={}
    for name in sorted(TERMS.intersection(parsed)):
        val=parsed[name]
        if not isinstance(val,str):
            return "BLOCKED_RAW_RIPPER_TERM_NOT_STRING",{}
        try:
            term=val.encode("utf-8")
        except UnicodeError:
            return "BLOCKED_RAW_RIPPER_UTF8_INVALID",{}
        fields[name]={"originalUtf8Sha256":hashlib.sha256(term).hexdigest(),
                      "originalByteLength":len(term)}
    return "STRICT_RIPPER_RAW_SOURCE_FULL_OBJECT_TERM_HASH_ONLY",fields

def compare_two(primary,secondary):
    if primary[0]!="STRICT_SOURCE_PARSED_SOURCE_TERM_KEYS_UNVERIFIED_RUNTIME":
        return primary[0],{}
    if secondary[0] not in (
        "STRICT_SOURCE_PARSED_SOURCE_TERM_KEYS_UNVERIFIED_RUNTIME",
        "STRICT_RIPPER_RAW_SOURCE_FULL_OBJECT_TERM_HASH_ONLY"):
        return "BLOCKED_INDEPENDENT_SOURCE_SCHEMA_"+secondary[0],{}
    if primary[1]!=secondary[1]:
        return "BLOCKED_SOURCE_TERM_HASH_OR_FIELD_CONFLICT",{}
    return ("DUAL_SCHEMA_RIPPER_RAW_REPARSED_LOCALIZER_TERMS_SOURCE_ONLY"
            if secondary[0]=="STRICT_RIPPER_RAW_SOURCE_FULL_OBJECT_TERM_HASH_ONLY"
            else "DUAL_BACKEND_LOCALIZER_TERMS_SOURCE_ONLY"),primary[1]

def select_rows(deep,graph,step2):
    a=[x for x in deep["scenes"] if x["sceneId"]==SCENE]
    g=[x for x in graph["scenes"] if x["sceneId"]==SCENE]
    targets=step2["componentsByCategory"].get("TextLocalization",[])
    if (len(a)!=1 or len(g)!=1 or
        step2.get("classification")!=
           "REF04_SOURCE_LAYOUT_CANVAS_TEXT_STEP2_READ_ONLY" or
        a[0]["sourceFile"]!=g[0]["sourceSerializedFile"] or
        a[0]["sourceFile"]!=step2["sourceSerializedFile"] or
        len(targets)!=52):
        raise ValueError("Original REF04 localization source inventory untrusted")
    by_deep={x["pathId"]:x for x in a[0]["components"]}
    by_graph={x["pathId"]:x for x in g[0]["components"]}
    rows={}
    for x in targets:
        cid=x["componentPathId"]
        src=by_deep.get(cid)
        raw=by_graph.get(cid)
        if (cid in rows or src is None or raw is None or
            src.get("className") not in ("I2.Loc.Localize","TextLocalizeChecker") or
            src.get("className")!=x.get("sourceClass") or
            src.get("kind")!="MonoBehaviour" or
            raw.get("kind")!="MonoBehaviour" or
            src.get("gameObjectId")!=x["gameObjectPathId"] or
            src.get("rectTransformId")!=x["rectTransformPathId"] or
            raw.get("rawEvidence",{}).get("sha256")!=x["originalObjectSha256"] or
            src.get("scriptPointer")!=raw.get("monoScriptPointer") or
            src.get("scriptResolution") not in
              ("LOCAL_PATHID","EXTERNAL_RESOLVED","UNITYPY_DEREF",
               "GLOBAL_XAPK_EXACT_FILE_ALIAS_PATHID") or
            not src.get("assembly") or
            x.get("canBeAppliedToUnity") is not False):
            raise ValueError("Original localizer source owner or raw bytes unverified")
        rows[cid]=(src,x)
    return rows,g[0]["sourceSerializedFile"]

def execute(root=ROOT,unitypy=None):
    if unitypy is None:
        import UnityPy as unitypy
    folder=root/"output"
    deep=json.loads((folder/"deep-ui-source-evidence.json").read_text(encoding="utf-8"))
    graph=json.loads((folder/"original-unity-graph.json").read_text(encoding="utf-8"))
    step2=json.loads((folder/"ref04-step2-layout-canvas-text.json").read_text(encoding="utf-8"))
    rows,source_file=select_rows(deep,graph,step2)
    xapk=next(iter(sorted(root.glob("*.xapk"))),None)
    if xapk is None:raise FileNotFoundError("Original XAPK unavailable")
    version=binary.exact_unity_version(deep["scenes"])
    g1,p1=binary.source_generator(xapk,version,backend="AssetStudio")
    g2,p2=binary.source_generator(xapk,version,backend="AssetRipper")
    for kind in ("library","metadata"):
        if p1[kind]["sha256"]!=p2[kind]["sha256"]:
            raise ValueError("Source IL2CPP backend pair hashes contradict")
    if p1["gameUnityVersion"]!=p2["gameUnityVersion"]:
        raise ValueError("IL2CPP source Unity version mismatch")
    scene_matches=[x for x in json.loads((root/
        "unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json"
        ).read_text(encoding="utf-8"))["scenes"] if x["id"]==SCENE]
    if len(scene_matches)!=1 or scene_matches[0]["source"]!=source_file:
        raise ValueError("Original REF04 serializedfile selection changed")
    results={}
    class Interceptor:
        def load(self,blob):
            env=unitypy.load(blob)
            groups=collections.defaultdict(dict)
            for reader in env.objects:
                groups[id(reader.assets_file)][int(reader.path_id)]=reader
            try:
                objects=layout.choose_serialized_file(scene_matches[0],groups)
            except ValueError:
                return env
            if results:
                raise ValueError("REF04 localizer source bundle duplicated")
            for cid,(src,original) in sorted(rows.items()):
                reader=objects.get(cid)
                if reader is None or reader.type.name!="MonoBehaviour":
                    raise ValueError("Source localizer serialized component absent")
                a=strict_probe(reader,src,original,g1)
                b=strict_probe(reader,src,original,g2)
                if (src["className"]=="I2.Loc.Localize" and
                    a[0]=="STRICT_SOURCE_PARSED_SOURCE_TERM_KEYS_UNVERIFIED_RUNTIME" and
                    b[0]=="BLOCKED_SOURCE_SCHEMA_PARSE_EOFError"):
                    b=raw_ripper_probe(reader,src,original,g2)
                result,fields=compare_two(a,b)
                results[cid]={
                    "componentPathId":cid,
                    "gameObjectPathId":original["gameObjectPathId"],
                    "originalObjectSha256":original["originalObjectSha256"],
                    "sourceClass":src["className"],
                    "verificationStatus":result,
                    "sourceTermFieldDigests":fields,
                    "runtimeTranslationProven":False,
                    "unityImportAllowed":False,
                }
            return env
    layout.build(root=root,xapk=xapk,unitypy=Interceptor())
    if len(results)!=52:
        raise ValueError("Incomplete 52 original localization components")
    counts=collections.Counter(v["verificationStatus"] for v in results.values())
    by_class=collections.defaultdict(collections.Counter)
    for item in results.values():
        by_class[item["sourceClass"]][item["verificationStatus"]]+=1
    proof={
        "classification":CLASS,
        "originalSourceSerializedFile":source_file,
        "originalSourceMetadataSha256":p1["metadata"]["sha256"],
        "originalSourceLibSha256":p1["library"]["sha256"],
        "localizersChecked":52,
        "sourceTermFieldsTwoBackendVerified":sum(
            len(v["sourceTermFieldDigests"]) for v in results.values()),
        "sourceStatusCounts":dict(sorted(counts.items())),
        "sourceStatusByClass":{
            key:dict(sorted(value.items())) for key,value in sorted(by_class.items())},
        "localizers":list(results.values()),
        "sourceTermValuesPublished":False,
        "runtimeLanguageOrTranslationProven":False,
        "localizerToTextTargetVerified":False,
        "unityImportAllowed":False,
        "unityAssetsChanged":False,
    }
    dest=folder/"ref04-p3-localizer-term-binary-probe.json"
    tmp=dest.with_suffix(".tmp")
    tmp.write_text(json.dumps(proof,ensure_ascii=False,indent=2)+"\n",encoding="utf-8")
    tmp.replace(dest)
    print(json.dumps({
        "classification":CLASS,
        "localizersChecked":52,
        "dualBackendLocalizerStatus":counts["DUAL_BACKEND_LOCALIZER_TERMS_SOURCE_ONLY"],
        "dualSourceRipperRawReparseStatus":
            counts["DUAL_SCHEMA_RIPPER_RAW_REPARSED_LOCALIZER_TERMS_SOURCE_ONLY"],
        "sourceTermFieldsTwoBackendVerified":proof["sourceTermFieldsTwoBackendVerified"],
        "sourceStatusCounts":proof["sourceStatusCounts"],
        "sourceStatusByClass":proof["sourceStatusByClass"],
        "runtimeLanguageOrTranslationProven":False,
        "originalUIAssetsChanged":False,
    },sort_keys=True))
    return proof

if __name__=="__main__":
    cli=argparse.ArgumentParser(description=__doc__)
    cli.add_argument("--root",type=Path,default=ROOT)
    args=cli.parse_args()
    execute(args.root.resolve())
