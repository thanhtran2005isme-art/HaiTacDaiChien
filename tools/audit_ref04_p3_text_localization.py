#!/usr/bin/env python3
"""REF04 P3 Text/font/localization *source* proof, no guessed visible strings.

The 62 original Text components must match dual AssetStudio/AssetRipper source
binary evidence and exact PathID / SHA from Step2. I2 localizers are related
only by proven source GameObject identity; proximity is NOT a binding, and no
runtime localization, font rendering, language, or screen state is fabricated.
"""
from __future__ import annotations

import argparse
import collections
import json
from pathlib import Path

import recover_managed_ui_fields as source
import ref04_il2cpp_method_index_v31 as methods

ROOT=Path(__file__).resolve().parents[1]
P3_CLASS="REF04_P3_ORIGINAL_TEXT_FONT_LOCALIZATION_SOURCE_ONLY"
TEXT_STATUS="TWO_BACKENDS_SAME_SERIALIZED_TEXT_FIELDS_NOT_IMPORTED"
P2_CLASS="REF04_P2_CANVAS_SAFEAREA_PANELHOME2_SOURCE_TRACE_NO_RUNTIME_FORMULA"
TEXT_CLASS="REF04_ORIGINAL_XAPK_TEXT_62_SOURCE_BINARY_PROBE"


def require_sha(s):
    return isinstance(s,str) and len(s)==64 and all(c in "0123456789abcdef" for c in s)

def audit(step2, probe, p2, method_metadata):
    if (step2.get("classification") !=
        "REF04_SOURCE_LAYOUT_CANVAS_TEXT_STEP2_READ_ONLY" or
        step2.get("counts",{}).get("Text")!=62 or
        step2.get("originalTextComponentsVerifiedByTwoBackends")!=62 or
        step2.get("sourceRuntimeTextProven") is not False or
        step2.get("sourceFieldApplicationAllowed") is not False or
        probe.get("classification") != TEXT_CLASS or
        probe.get("sourceTextComponents") != 62 or
        probe.get("dualBackendAgreedComponentCount") != 62 or
        probe.get("sourceSerializedFile") != step2.get("sourceSerializedFile") or
        probe.get("rawTextContentPublished") is not False or
        probe.get("runtimeTextProven") is not False or
        probe.get("unityImportAllowed") is not False or
        p2.get("classification") != P2_CLASS or
        p2.get("originalSourceSerializedFile") != step2.get("sourceSerializedFile") or
        p2.get("runtimeAlignmentProven") is not False or
        p2.get("sourceFieldApplicationAllowed") is not False or
        method_metadata.get("classification") !=
            "REF04_IL2CPP_V31_SOURCE_METHOD_INDEX_NO_CODE_MAPPING" or
        method_metadata.get("methodInventoryScope") != "p3" or
        method_metadata.get("metadataSha256") !=
            p2.get("originalIL2CPPPair",{}).get("metadataSha256") or
        probe.get("sourceBinary",{}).get("metadataSha256") !=
            p2.get("originalIL2CPPPair",{}).get("metadataSha256") or
        probe.get("sourceBinary",{}).get("librarySha256") !=
            p2.get("originalIL2CPPPair",{}).get("libil2cppSha256")):
        raise ValueError("P3 Text/IL2CPP/Step2/P2 original-XAPK source identity untrusted")
    txt=step2["componentsByCategory"]["Text"]
    loc=step2["componentsByCategory"].get("TextLocalization",[])
    by_probe={r["componentPathId"]:r for r in probe.get("textComponents",[])}
    if len(by_probe)!=62 or len(txt)!=62 or len({t["componentPathId"] for t in txt})!=62:
        raise ValueError("Duplicate or missing original Text PathIDs")
    local_by_owner=collections.defaultdict(list)
    for x in loc:
        cid=x["componentPathId"]
        if (type(cid) is not int or
            x.get("canBeAppliedToUnity") is not False or
            x.get("runtimeRulesVerified") is not False):
            raise ValueError("Untrusted original localization component")
        local_by_owner[(x["gameObjectPathId"],x["rectTransformPathId"])].append(x)
    if len({l["componentPathId"] for l in loc})!=len(loc):
        raise ValueError("Duplicate original localization components")
    texts=[]
    fonts=collections.Counter()
    with_local=0
    for t in sorted(txt,key=lambda x:x["componentPathId"]):
        q=by_probe.get(t["componentPathId"])
        fields=t.get("textSourceFieldsTwoBackendsAgreed",{})
        if (q is None or t.get("textBinaryProbeStatus")!=TEXT_STATUS or
            q.get("verificationStatus")!=TEXT_STATUS or
            q.get("sourceTextFieldEvidence")!=fields or
            t["originalObjectSha256"]!=q["sourceObjectSha256"] or
            t["gameObjectPathId"]!=q["gameObjectPathId"] or
            t["rectTransformPathId"]!=q["rectTransformPathId"] or
            t.get("canBeAppliedToUnity") is not False or
            t.get("renderedText") is not None):
            raise ValueError("Original source Text hash/field/owner disagreement")
        content=fields.get("m_Text")
        font=fields.get("m_Font")
        # Dual-backend component verification does not imply that every
        # individual source field was present. Missing entries remain BLOCKED.
        if content is not None and (
            not isinstance(content,dict) or
            not require_sha(content.get("sourceUtf8Sha256")) or
            type(content.get("utf8Bytes")) is not int or
            content["utf8Bytes"]<0 or
            type(content.get("sourceTextIsEmpty")) is not bool):
            raise ValueError("Untrusted Text source hash or length")
        if font is not None and (
            not isinstance(font,dict) or
            type(font.get("sourceFileId")) is not int or
            type(font.get("sourcePathId")) is not int or
            font["sourceFileId"]<0 or font["sourcePathId"]<0):
            raise ValueError("Untrusted Text original font pointer")
        candidates=local_by_owner[(t["gameObjectPathId"],t["rectTransformPathId"])]
        if candidates: with_local+=1
        if font is not None:
            fonts[(font["sourceFileId"],font["sourcePathId"])]+=1
        texts.append({
            "componentPathId":t["componentPathId"],
            "rectTransformPathId":t["rectTransformPathId"],
            "gameObjectPathId":t["gameObjectPathId"],
            "originalObjectSha256":q["sourceObjectSha256"],
            "sourceTextUtf8Sha256":content["sourceUtf8Sha256"] if content else None,
            "sourceTextUtf8ByteLength":content["utf8Bytes"] if content else None,
            "sourceEmptyText":content["sourceTextIsEmpty"] if content else None,
            "originalFontPointer":font,
            "originalTextFieldStatus":(
                "DUAL_BACKEND_M_TEXT_FIELD" if content else
                "BLOCKED_SOURCE_M_TEXT_FIELD_NOT_EXTRACTED"),
            "originalFontFieldStatus":(
                "DUAL_BACKEND_M_FONT_FIELD" if font else
                "BLOCKED_SOURCE_M_FONT_FIELD_NOT_EXTRACTED"),
            "originalTextFontAndStyleFieldNames":sorted(fields),
            "originalTextSourceValues":fields,
            "colocatedLocalizationComponentPathIds":[x["componentPathId"] for x in candidates],
            "localizationConnection":"SAME_ORIGINAL_GAMEOBJECT_ONLY_NO_BINDING_PROOF"
                if candidates else "NO_LOCALIZER_ON_ORIGINAL_GAMEOBJECT",
            "runtimeString":None,"runtimeLanguage":None,"runtimeFont":None,
            "runtimePlacement":None,"runtimeTextProven":False,
            "unityImportAllowed":False,
        })
    cls=method_metadata["targetClassDefinitions"]
    for row in cls:
        if row.get("nativeMethodAddressResolved") is not False or (
            row.get("runtimeExpressionProven") is not False) or any(
                v.get("nativeAddress") is not None or
                v.get("methodBodyVerified") is not False for v in row["methods"]):
            raise ValueError("Unproven P3 IL2CPP runtime method address supplied")
    return {
        "classification":P3_CLASS,"schemaVersion":1,
        "originalSourceSerializedFile":step2["sourceSerializedFile"],
        "originalIL2CPPSha256Pair":p2["originalIL2CPPPair"],
        "sourceTextComponentsIndependentlyVerified":len(texts),
        "sourceLocalizationComponents":len(loc),
        "originalTextSameGameObjectLocalizationCandidates":with_local,
        "uniqueOriginalFontPointerCount":len(fonts),
        "sourceTextStringsIndependentlyVerified":sum(
            x["sourceTextUtf8Sha256"] is not None for x in texts),
        "sourceTextFontPointersIndependentlyVerified":sum(
            x["originalFontPointer"] is not None for x in texts),
        "sourceTextFieldsVerified":True,
        "fontAssetIdentityVerified":False,
        "localizationKeyToTextBindingProven":False,
        "runtimeLanguageChosen":None,
        "runtimeTextValuesProven":False,
        "runtimeFontRenderingProven":False,
        "runtimePlacementProven":False,
        "originalTextContentPublished":False,
        "originalTextSourceEvidence":texts,
        "sourceLocalizationComponentsEvidence":[{
            "componentPathId":l["componentPathId"],
            "sourceClass":l["sourceClass"],
            "gameObjectPathId":l["gameObjectPathId"],
            "rectTransformPathId":l["rectTransformPathId"],
            "originalObjectSha256":l["originalObjectSha256"],
            "serializedVerificationStatus":l["verificationStatus"],
            "runtimeLocalizedText":None,
            "localizationBindingProven":False,
        } for l in loc],
        "localizationMethodDeclarations":cls,
        "methodBodiesVerified":0,
        "runtimeBlockers":[
            "BLOCKED_I2_LOCALIZATION_KEY_DATABASE_AND_SELECTED_LANGUAGE",
            "BLOCKED_TEXT_LOCALIZE_CHECKER_RUNTIME_UPDATES",
            "BLOCKED_FONT_ASSET_RESOLUTION_AND_RENDER_METRICS",
            "BLOCKED_NATIVE_ARM64_METHOD_POINTER_AND_RUNTIME_TEXT_ASSIGNMENT",
            "BLOCKED_RUNTIME_CANVAS_VIEWPORT_AND_UI_PLACEMENT",
        ],
        "runtimeTextAndLocalizationProven":False,
        "unityAssetsChanged":False,"charactersOrSpineChanged":False,
        "unityImportAllowed":False,
    }


def execute(root=ROOT,xapk=None):
    out=root/"output"
    step2,p2,probe=[
        json.loads((out/file).read_text(encoding="utf-8"))
        for file in ("ref04-step2-layout-canvas-text.json",
                     "ref04-p2-canvas-il2cpp-runtime-source.json",
                     "ref04-text-source-binary-evidence.json")]
    xapk=xapk or next(iter(sorted(root.glob("*.xapk"))),None)
    if xapk is None: raise FileNotFoundError("Original XAPK missing")
    source_pair=source.read_source_pair(xapk)
    meta=methods.inspect(source_pair["metadata"][0],family="p3")
    result=audit(step2,probe,p2,meta)
    dest=out/"ref04-p3-text-font-localization-source.json"
    tmp=dest.with_suffix(".tmp")
    tmp.write_text(json.dumps(result,ensure_ascii=False,indent=2)+"\n",encoding="utf-8")
    tmp.replace(dest)
    print(json.dumps({
        "classification":P3_CLASS,
        "sourceTextComponentsIndependentlyVerified":len(result["originalTextSourceEvidence"]),
        "sourceLocalizationComponents":len(result["sourceLocalizationComponentsEvidence"]),
        "sameGameObjectCandidates":result["originalTextSameGameObjectLocalizationCandidates"],
        "fontPointers":result["uniqueOriginalFontPointerCount"],
        "runtimeTextAndLocalizationProven":False,
        "unityAssetsChanged":False},sort_keys=True))
    return result

if __name__=="__main__":
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root",type=Path,default=ROOT)
    args=parser.parse_args()
    execute(args.root.resolve())
