#!/usr/bin/env python3
"""Resolve original UGUI Text m_Font PPtrs to exact local Font serialized objects.

An external PPtr cannot be resolved by guessing an AssetBundle filename or
copying a Unity viewer fallback. An identified Font asset is not evidence of
the font selected or glyph metrics at runtime.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
from pathlib import Path

import export_local_ui_layout as layout

ROOT=Path(__file__).resolve().parents[1]
SCENE="REF04-home-crew"
CLASS="REF04_P3_LOCAL_FONT_OBJECT_PROVENANCE_NO_RUNTIME_RENDER"

def verify_reference(ref, objects, *, assets_file=None, external_deref=None):
    if (not isinstance(ref,dict) or type(ref.get("sourceFileId")) is not int
        or type(ref.get("sourcePathId")) is not int):
        raise ValueError("Untrusted Font pointer structure")
    fid,pid=ref["sourceFileId"],ref["sourcePathId"]
    if fid<0 or pid<0:
        raise ValueError("Negative original font pointer")
    if fid!=0:
        # m_FileID is 1-based into the *original* SerializedFile externals.
        # Never substitute an unrelated font having the same PathID.
        externals=getattr(assets_file,"externals",None)
        if not isinstance(externals,(list,tuple)) or fid>len(externals):
            return {"status":"BLOCKED_EXTERNAL_SOURCE_FILE_TABLE_UNAVAILABLE",
                    "sourceFileId":fid,"sourcePathId":pid,
                    "localOriginalFontObjectVerified":False,
                    "externalOriginalFontObjectVerified":False}
        original=getattr(externals[fid-1],"path",None)
        if not isinstance(original,str) or not original:
            return {"status":"BLOCKED_EXTERNAL_SOURCE_PATH_EMPTY",
                    "sourceFileId":fid,"sourcePathId":pid,
                    "localOriginalFontObjectVerified":False,
                    "externalOriginalFontObjectVerified":False}
        normalized=original.lower().replace("\\\\","/").split("/")[-1]
        if not normalized or normalized in (".",".."):
            raise ValueError("Invalid original source external filename")
        status={"sourceFileId":fid,"sourcePathId":pid,
                "originalExternalPathSha256":hashlib.sha256(
                    original.encode("utf-8")).hexdigest(),
                "originalExternalFileNameSha256":hashlib.sha256(
                    normalized.encode("utf-8")).hexdigest(),
                "localOriginalFontObjectVerified":False,
                "externalOriginalFontObjectVerified":False}
        if external_deref is None:
            return {"status":"BLOCKED_EXTERNAL_SOURCE_DEREFERENCER_UNAVAILABLE",
                    **status}
        try:
            target=external_deref(fid,pid,assets_file)
        except (KeyError,FileNotFoundError,ValueError,LookupError):
            return {"status":"BLOCKED_ORIGINAL_EXTERNAL_DEPENDENCY_UNAVAILABLE",
                    **status}
        if target is None or type(getattr(target,"path_id",None)) is not int or (
            target.path_id != pid):
            return {"status":"BLOCKED_EXTERNAL_FONT_TARGET_PATHID_CONFLICT",
                    **status}
        target_file=getattr(target,"assets_file",None)
        actual=getattr(target_file,"name",None)
        if not isinstance(actual,str) or actual.lower().replace("\\\\","/").split("/")[-1] != normalized:
            return {"status":"BLOCKED_EXTERNAL_FILENAME_SOURCE_MISMATCH",
                    **status}
        if target.type.name!="Font":
            return {"status":"BLOCKED_EXTERNAL_POINTER_TARGET_NOT_FONT",
                    **status}
        raw=target.get_raw_data()
        if not raw or len(raw)>64*1024*1024:
            raise ValueError("Original external Font bytes missing or oversized")
        return {"status":"SOURCE_EXTERNAL_FONT_SHA256_FILENAME_MATCH_RUNTIME_UNPROVEN",
                **status,"externalOriginalFontObjectVerified":True,
                "originalNativeType":"Font","sourceRawObjectBytes":len(raw),
                "sourceRawObjectSha256":hashlib.sha256(raw).hexdigest(),
                "runtimeFontProven":False}
    if pid==0:
        return {"status":"BLOCKED_NULL_FONT_POINTER",
                "sourceFileId":fid,"sourcePathId":pid,
                "localOriginalFontObjectVerified":False}
    reader=objects.get(pid)
    if reader is None:
        return {"status":"BLOCKED_ORIGINAL_FONT_OBJECT_NOT_IN_SERIALIZED_FILE",
                "sourceFileId":fid,"sourcePathId":pid,
                "localOriginalFontObjectVerified":False}
    if reader.type.name!="Font":
        return {"status":"BLOCKED_SOURCE_POINTER_TARGET_NOT_FONT",
                "sourceFileId":fid,"sourcePathId":pid,
                "actualSourceType":reader.type.name,
                "localOriginalFontObjectVerified":False}
    raw=reader.get_raw_data()
    if not raw or len(raw)>64*1024*1024:
        raise ValueError("Original font object raw source absent or oversized")
    return {
        "status":"ORIGINAL_LOCAL_FONT_OBJECT_SOURCE_SHA256_VERIFIED",
        "sourceFileId":fid,"sourcePathId":pid,
        "originalNativeType":"Font","sourceRawObjectBytes":len(raw),
        "sourceRawObjectSha256":hashlib.sha256(raw).hexdigest(),
        "localOriginalFontObjectVerified":True,
        "runtimeFontProven":False,
    }

def source_font_identity_complete(items):
    """Exact source Font objects only; a missing/external-unresolved pointer blocks."""
    return bool(items) and all(
        x.get("localOriginalFontObjectVerified") is True or
        x.get("externalOriginalFontObjectVerified") is True for x in items)


def check_p3(p3,scene):
    if (p3.get("classification")!=
          "REF04_P3_ORIGINAL_TEXT_FONT_LOCALIZATION_SOURCE_ONLY"
        or p3.get("sourceTextComponentsIndependentlyVerified")!=62
        or p3.get("originalSourceSerializedFile")!=scene["source"]
        or p3.get("sourceTextFontPointersIndependentlyVerified")!=62
        or p3.get("runtimeTextAndLocalizationProven") is not False
        or p3.get("unityImportAllowed") is not False):
        raise ValueError("P3 original font pointers not provenance-verified")
    rows=p3["originalTextSourceEvidence"]
    if len(rows)!=62 or len({row["componentPathId"] for row in rows})!=62:
        raise ValueError("Incomplete original 62 Text components")
    pointers={}
    for row in rows:
        if (row["originalFontFieldStatus"]!="DUAL_BACKEND_M_FONT_FIELD"
            or not isinstance(row["originalFontPointer"],dict)):
            raise ValueError("Original Text Font field not independently verified")
        ref=row["originalFontPointer"]
        key=(ref["sourceFileId"],ref["sourcePathId"])
        if any(type(v) is not int for v in key):
            raise ValueError("Original Font PPtr not typed")
        pointers[key]=ref
    if len(pointers)!=p3["uniqueOriginalFontPointerCount"]:
        raise ValueError("Distinct font pointer source count conflicts with P3")
    return pointers

def execute(root=ROOT,unitypy=None):
    if unitypy is None:
        import UnityPy as unitypy
    out=root/"output"
    p3=json.loads((out/"ref04-p3-text-font-localization-source.json").read_text(encoding="utf-8"))
    scenes=json.loads((root/"unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json").read_text(encoding="utf-8"))["scenes"]
    matches=[scene for scene in scenes if scene.get("id")==SCENE]
    if len(matches)!=1:
        raise ValueError("Original REF04 candidate not uniquely identified")
    scene=matches[0]
    pointers=check_p3(p3,scene)
    found=[]
    class RecordingUnityPy:
        def load(self,blob):
            env=unitypy.load(blob)
            groups=collections.defaultdict(dict)
            for reader in env.objects:
                groups[id(reader.assets_file)][int(reader.path_id)]=reader
            try:
                objects=layout.choose_serialized_file(scene,groups)
            except ValueError:
                return env
            if found:
                raise ValueError("Duplicate REF04 source serialized bundle")
            def deref_source(fid,pid,assets_file):
                from UnityPy.classes.PPtr import PPtr
                return PPtr(m_FileID=fid,m_PathID=pid,
                            assetsfile=assets_file).deref()
            selected_file=next(r.assets_file for r in objects.values())
            found.append([verify_reference(ref,objects,assets_file=selected_file,
                                           external_deref=deref_source)
                          for _,ref in sorted(pointers.items())])
            return env
    # The original XAPK is read only. No output files are imported into Unity.
    layout.build(root=root,unitypy=RecordingUnityPy())
    if len(found)!=1:
        raise ValueError("Unable to find exact original REF04 serialized bundle")
    items=found[0]
    proof={
        "classification":CLASS,
        "originalSourceSerializedFile":scene["source"],
        "originalIL2CPPSha256Pair":p3["originalIL2CPPSha256Pair"],
        "originalFontPPtrReferences":len(items),
        "originalLocalFontRawObjectsVerified":sum(
            x["localOriginalFontObjectVerified"] for x in items),
        "originalExternalFontObjectsVerified":sum(
            x.get("externalOriginalFontObjectVerified",False) for x in items),
        "pointerResolutions":items,
        # A font in an original external SerializedFile can be source-proven
        # via its exact dependency filename, PathID, native type and raw SHA.
        # Source identity is not evidence of runtime glyphs or loaded font.
        "originalFontAssetIdentityFullyResolved":source_font_identity_complete(items),
        "runtimeFontRenderingProven":False,
        "runtimeGlyphMetricsProven":False,
        "runtimeTextProven":False,
        "unityImportAllowed":False,
        "unityAssetsChanged":False,
    }
    dest=out/"ref04-p3-original-font-object-provenance.json"
    tmp=dest.with_suffix(".tmp")
    tmp.write_text(json.dumps(proof,ensure_ascii=False,indent=2)+"\n",encoding="utf-8")
    tmp.replace(dest)
    print(json.dumps({
        "classification":CLASS,
        "originalFontPPtrReferences":len(items),
        "originalLocalFontRawObjectsVerified":proof["originalLocalFontRawObjectsVerified"],
        "sourcePointerStatuses":dict(collections.Counter(x["status"] for x in items)),
        "originalExternalFontObjectsVerified":proof["originalExternalFontObjectsVerified"],
        "runtimeFontRenderingProven":False,
    },sort_keys=True))
    return proof

if __name__=="__main__":
    cli=argparse.ArgumentParser(description=__doc__)
    cli.add_argument("--root",type=Path,default=ROOT)
    args=cli.parse_args()
    execute(args.root.resolve())
