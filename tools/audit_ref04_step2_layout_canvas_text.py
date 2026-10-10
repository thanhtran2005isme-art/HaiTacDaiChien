#!/usr/bin/env python3
"""REF04 Step 2: Canvas/LayoutGroup/Text source verification map (READ ONLY).

No guessed 1600x900, fallback position or runtime text. Source of truth is the
full original REF04 candidate XAPK component inventory from step 1; third-backend
recheck is optional and cannot automatically authorize a Unity import.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
from pathlib import Path

ROOT=Path(__file__).resolve().parents[1]
SCENE="REF04-home-crew"
CLASSES={
    "Canvas": ("Canvas",),
    "CanvasScaler": ("UnityEngine.UI.CanvasScaler",),
    "LayoutGroup": ("UnityEngine.UI.HorizontalLayoutGroup",
                    "UnityEngine.UI.VerticalLayoutGroup",
                    "UnityEngine.UI.GridLayoutGroup"),
    "Text": ("UnityEngine.UI.Text",),
    "TextLocalization": ("I2.Loc.Localize","TextLocalizeChecker"),
    "SafeArea": ("SafeAreaAdapter",),
    "UIControl": ("UnityEngine.UI.ContentSizeFitter",
                  "UnityEngine.UI.Mask","UnityEngine.UI.Outline",
                  "UnityEngine.UI.Button","UnityEngine.UI.Slider",
                  "UnityEngine.UI.LayoutElement"),
}
CANVAS_FIELDS=("m_Enabled","m_RenderMode","m_SortingOrder",
               "m_OverrideSorting","m_TargetDisplay","m_PixelPerfect")
TEXT_REQUIRED=("m_Text","m_Font","m_FontSize","m_FontStyle",
               "m_Alignment","m_Color","m_RaycastTarget")
RUNTIME_BLOCKERS={
    "Canvas":"Runtime screen ancestor/viewport and dynamic Canvas mutations",
    "CanvasScaler":"Device resolution, safe area and runtime scaler changes",
    "LayoutGroup":"Source LayoutGroup fields may still be single-backend only; runtime rebuilding unknown",
    "Text":"Original string/font/localization and runtime text assignment not dual-verified",
    "TextLocalization":"Localization database/selected language/runtime overrides unverified",
    "SafeArea":"IL2CPP safe-area calculation, actual device insets and runtime mutations unverified",
    "UIControl":"Runtime button/slider/mask/outline updates and sibling layout unverified",
}


def inventory(step1, third=None, text_probe=None, schema_probe=None):
    if (step1.get("classification") !=
            "REF04_ALL_SOURCE_CANDIDATE_UI_COMPONENT_INVENTORY_READ_ONLY" or
        step1.get("sceneId") != SCENE or
        step1.get("sourceFieldApplicationAllowed") is not False or
        step1.get("counts",{}).get("gameObjects") != 503 or
        step1.get("counts",{}).get("serializedComponentRecords") != 1564 or
        step1.get("counts",{}).get("allDualVerifiedImageComponents") != 299):
        raise ValueError("REF04 original XAPK full candidate inventory not proven")
    if third is not None:
        if (third.get("classification") !=
             "REF04_THIRD_BACKEND_SOURCE_LAYOUTGROUP_RECHECK_READ_ONLY" or
            third.get("sourceSerializedFile") != step1["sourceSerializedFile"] or
            third.get("ref04LayoutGroupComponents") != 24 or
            third.get("sourceLayoutFieldNamesExamined") != 168 or
            third.get("unityImportAllowed") is not False or
            len(third.get("layoutGroups",[])) != 24):
            raise ValueError("Third-backend source LayoutGroup recheck untrusted")
        by_third={item["componentPathId"]:item for item in third["layoutGroups"]}
        if len(by_third)!=24:
            raise ValueError("Third-backend PathID collision")
    else:
        by_third={}
    if text_probe is not None:
        if (text_probe.get("classification") !=
                "REF04_ORIGINAL_XAPK_TEXT_62_SOURCE_BINARY_PROBE" or
            text_probe.get("sourceSerializedFile") !=
                step1["sourceSerializedFile"] or
            text_probe.get("sourceTextComponents") != 62 or
            text_probe.get("unityImportAllowed") is not False or
            text_probe.get("rawTextContentPublished") is not False or
            len(text_probe.get("textComponents",[]))!=62):
            raise ValueError("Source Text binary probe does not match original XAPK")
        by_text={r["componentPathId"]:r for r in text_probe["textComponents"]}
        if len(by_text)!=62:
            raise ValueError("Original Text source PathIDs not unique")
    else:
        by_text={}

    if schema_probe is not None:
        if (schema_probe.get("classification") !=
                "REF04_LAYOUTGROUP_SCHEMA_FORENSICS_READ_ONLY" or
            schema_probe.get("sourceSerializedFile") !=
                step1["sourceSerializedFile"] or
            schema_probe.get("layoutGroupsInspected") != 24 or
            schema_probe.get("sourceFieldValuesStillBlockedFromUnity") != 168 or
            schema_probe.get("unityImportAllowed") is not False or
            schema_probe.get("runtimeAlignmentProven") is not False or
            schema_probe.get("originalUiAssetsChanged") is not False or
            len(schema_probe.get("layoutGroups", [])) != 24):
            raise ValueError("REF04 original LayoutGroup schema evidence untrusted")
        by_schema = {row["componentPathId"]: row
                     for row in schema_probe["layoutGroups"]}
        if len(by_schema) != 24:
            raise ValueError("Source schema forensic component IDs not unique")
    else:
        by_schema = {}

    found={group:[] for group in CLASSES}
    identity=set()
    for node in step1["gameObjects"]:
        tid,gid=node["rectTransformPathId"],node["gameObjectPathId"]
        for component in node["components"]:
            cid=component["componentPathId"]
            if cid in identity:
                raise ValueError("Duplicate source component PathID")
            identity.add(cid)
            if component["gameObjectPathId"]!=gid or component["rectTransformPathId"]!=tid:
                raise ValueError("Original component/source RectTransform owner changed")
            classname=component.get("monoScriptClass") or component["nativeKind"]
            for name,labels in CLASSES.items():
                if classname not in labels:
                    continue
                fields=component.get("verifiedSerializedFields",{})
                third_record=by_third.get(cid)
                if third_record is not None and (
                    name!="LayoutGroup" or
                    third_record["sourceObjectSha256"] != component.get(
                        "rawSourceObjectSha256") or
                    third_record["gameObjectPathId"]!=gid or
                    third_record["rectTransformPathId"]!=tid or
                    third_record["className"]!=classname
                ):
                    raise ValueError("Third backend source LayoutGroup ID/hash conflicts")
                item={
                    "sourceSerializedFile":step1["sourceSerializedFile"],
                    "componentPathId":cid,"gameObjectPathId":gid,
                    "rectTransformPathId":tid,"sourceClass":classname,
                    "nativeEnabled":component.get("sourceComponentEnabled"),
                    "verificationStatus":component["verificationStatus"],
                    "originalObjectSha256":component.get("rawSourceObjectSha256"),
                    "verifiedSerializedFields":fields,
                    "sourceRectTransformValues":node["rectSource"],
                    "sourceSiblingOrder":node["sourceSiblingOrder"],
                    "originalGameObjectActive":node["sourceActive"],
                    "canBeAppliedToUnity":False,
                    "runtimeRulesVerified":False,
                    "runtimeBlocker":RUNTIME_BLOCKERS[name],
                }
                if name=="Canvas":
                    item["nativeCanvasFieldsExtracted"]= {
                        key:fields[key] for key in CANVAS_FIELDS if key in fields
                    }
                    item["nativeCanvasFieldsUnverifiedOrAbsent"]=[
                        key for key in CANVAS_FIELDS if key not in fields
                    ]
                if name=="Text":
                    text_record=by_text.get(cid)
                    if text_record is not None and (
                        text_record["gameObjectPathId"]!=gid or
                        text_record["rectTransformPathId"]!=tid or
                        text_record["sourceObjectSha256"] !=
                            component.get("rawSourceObjectSha256")):
                        raise ValueError("Source Text component ID/source hash differs")
                    item["textBinaryProbeStatus"]=(
                        text_record["verificationStatus"] if text_record else
                        "NOT_YET_RUN")
                    # A single strict decoded source snapshot is NOT evidence
                    # of final runtime text, font, localization or position.
                    text_source_fields=(
                        text_record.get("sourceTextFieldEvidence",{})
                        if text_record else {})
                    independently_agreed=(
                        text_record is not None and
                        text_record["verificationStatus"]==
                            "TWO_BACKENDS_SAME_SERIALIZED_TEXT_FIELDS_NOT_IMPORTED")
                    # Distinguish restored original Text values from all
                    # runtime/localized text. 3C did not target Text class,
                    # but the independent Text-specific XAPK binary recheck
                    # may verify its original serialized field subset.
                    item["textSourceFieldsTwoBackendsAgreed"]=(
                        text_source_fields if independently_agreed else {})
                    item["textSourceFieldsSingleBackendOnly"]=(
                        text_source_fields if not independently_agreed else {})
                    item["textFieldsNotDoubleVerified"]=[
                        key for key in TEXT_REQUIRED
                        if key not in (text_source_fields if
                                       independently_agreed else {})
                    ]
                    # Never invent a localized string or font from the component name.
                    item["renderedText"] = None
                if name=="LayoutGroup":
                    item["singleBackendFieldNamesNotImportable"]=component.get(
                        "singleBackendFieldNames",[])
                    item["thirdBackendSourceVerification"] = (
                        third_record["thirdBackendStatus"] if third_record else
                        "NOT_YET_RUN")
                    evidence=by_schema.get(cid)
                    if evidence is not None and (
                        evidence["sourceObjectSha256"] !=
                            component.get("rawSourceObjectSha256") or
                        evidence["gameObjectPathId"] != gid or
                        evidence["rectTransformPathId"] != tid or
                        evidence["sourceClass"] != classname or
                        evidence["sourceFieldCountBlocked"] != len(
                            item["singleBackendFieldNamesNotImportable"]) or
                        evidence["unityImportAllowed"] is not False or
                        evidence["runtimeLayoutProven"] is not False or
                        evidence.get("rawByteReparse", {}).get(
                            "unityImportAllowed", False) is not False):
                        raise ValueError("REF04 LayoutGroup schema source ID/hash conflicts")
                    item["layoutSchemaComparison"] = (
                        evidence["schemaComparison"] if evidence else "NOT_YET_RUN")
                    item["layoutStrictFieldAgreement"] = (
                        evidence["fieldAgreement"] if evidence else "NOT_YET_RUN")
                    item["layoutRawByteReplayStatus"] = (
                        evidence.get("rawByteReparse", {}).get("status", "NOT_YET_RUN")
                        if evidence else "NOT_YET_RUN")
                    # Offsets are diagnostic, tied to the AssetStudio-generated
                    # schema; they cannot prove independent source field layout.
                    item["layoutRawByteReplayDerivedSchemaOnly"] = True
                found[name].append(item)
    if len(identity)!=1564:
        raise ValueError("REF04 source component traversal incomplete")
    counts={k:len(v) for k,v in found.items()}
    if counts["Canvas"]!=1 or counts["CanvasScaler"]!=1 or (
        counts["LayoutGroup"]!=24) or counts["Text"]!=62 or (
        counts["SafeArea"]!=6):
        raise ValueError("REF04 Canvas/Layout/Text original class inventory changed")
    if set(by_third)!={i["componentPathId"] for i in found["LayoutGroup"]} and third is not None:
        raise ValueError("Third backend did not cover all source LayoutGroup IDs")
    if text_probe is not None and set(by_text)!={
        i["componentPathId"] for i in found["Text"]
    }:
        raise ValueError("Text binary probe did not cover 62 exact source PathIDs")
    if schema_probe is not None and set(by_schema)!={
        i["componentPathId"] for i in found["LayoutGroup"]
    }:
        raise ValueError("Schema forensics did not cover all 24 source LayoutGroups")

    blockers=[{
        "category":name,"sourceComponents":counts[name],
        "status":"SOURCE_FIELDS_INCOMPLETE_OR_RUNTIME_UNVERIFIED",
        "reason":RUNTIME_BLOCKERS[name],
        "nextStep":"Decode original serialized XAPK or original IL2CPP source binary; "
                   "if still unobtainable, ask user rather than guess",
    } for name in CLASSES]
    return {
        "schemaVersion":1,
        "classification":"REF04_SOURCE_LAYOUT_CANVAS_TEXT_STEP2_READ_ONLY",
        "sceneId":SCENE,
        "sourceSerializedFile":step1["sourceSerializedFile"],
        "counts":counts,
        "componentCount":1564,
        "layoutGroupFieldValuesStillBlockedFromUnity":sum(
            len(c["singleBackendFieldNamesNotImportable"])
            for c in found["LayoutGroup"]),
        "thirdBackendRunProvided":third is not None,
        "layoutSchemaForensicsProvided":schema_probe is not None,
        "layoutSchemaComparisonCounts":(
            schema_probe["schemaComparisonCounts"] if schema_probe else {}),
        "layoutRawByteReparseCounts":(
            schema_probe.get("rawByteReparseCounts", {}) if schema_probe else {}),
        "sourceTextBinaryProbeProvided":text_probe is not None,
        "originalTextComponentsVerifiedByTwoBackends":sum(
            c.get("textBinaryProbeStatus")==
            "TWO_BACKENDS_SAME_SERIALIZED_TEXT_FIELDS_NOT_IMPORTED"
            for c in found["Text"]),
        "sourceRuntimeCanvasOrViewportProven":False,
        "sourceRuntimeTextProven":False,
        "sourceRuntimeLayoutProven":False,
        "unityAssetsChanged":False,
        "sourceFieldApplicationAllowed":False,
        "blocks":blockers,
        "componentsByCategory":found,
    }


def write(report,path):
    path.parent.mkdir(parents=True,exist_ok=True)
    tmp=path.with_suffix(".tmp")
    tmp.write_text(json.dumps(report,ensure_ascii=False,indent=2)+"\n",
                   encoding="utf-8")
    tmp.replace(path)
    lines=["# REF04 — BƯỚC 2: Canvas, LayoutGroup, Text từ XAPK (READ ONLY)",
           "",
           "**Không sửa Unity, không đặt tọa độ/Canvas/HUD phỏng đoán.**",
           "",
           "| Nhóm | Số component nguồn |","|---|---:|"]
    for category,num in report["counts"].items():
        lines.append(f"| {category} | {num} |")
    lines.extend(["",f"**24 LayoutGroup / {report['layoutGroupFieldValuesStillBlockedFromUnity']} field** vẫn chưa được tự động đưa vào Unity.",
                  "",
                  "## Tình trạng bằng chứng"])
    for cat, group in report["componentsByCategory"].items():
        statuses=collections.Counter(x["verificationStatus"] for x in group)
        lines.append(f"- **{cat}**: "+", ".join(f"{k}: {v}" for k,v in sorted(statuses.items())))
    lines.extend(["","## Thiếu và không được tự điền"])
    for block in report["blocks"]:
        lines.append(f"- **{block['category']}** — {block['reason']}. "
                     f"Tiếp: {block['nextStep']}")
    lines.extend(["","Không coi MonoScript class hoặc tọa độ RectTransform serialized "
                  "là bằng chứng của trạng thái runtime cuối. "
                  "Nếu không giải mã được tiếp từ XAPK, dừng và hỏi người dùng.",""])
    tmp=path.with_name(path.stem+".md.tmp")
    tmp.write_text("\n".join(lines),encoding="utf-8")
    tmp.replace(path.with_suffix(".md"))


def main():
    p=argparse.ArgumentParser(description=__doc__)
    p.add_argument("--root",type=Path,default=ROOT)
    args=p.parse_args()
    root=args.root.resolve()
    step1path=root/"output/ref04-full-source-inventory.json"
    step1=json.loads(step1path.read_text(encoding="utf-8"))
    thirdpath=root/"output/ref04-layout-third-backend-evidence.json"
    third=(json.loads(thirdpath.read_text(encoding="utf-8"))
           if thirdpath.is_file() else None)
    textpath=root/"output/ref04-text-source-binary-evidence.json"
    text=(json.loads(textpath.read_text(encoding="utf-8"))
          if textpath.is_file() else None)
    schemapath=root/"output/ref04-layout-schema-forensics.json"
    schema=(json.loads(schemapath.read_text(encoding="utf-8"))
            if schemapath.is_file() else None)
    report=inventory(step1,third,text,schema)
    dest=root/"output/ref04-step2-layout-canvas-text.json"
    write(report,dest)
    print(json.dumps({
        "classification":report["classification"],
        "counts":report["counts"],
        "unverifiedLayoutGroupFields":
            report["layoutGroupFieldValuesStillBlockedFromUnity"],
        "thirdBackendRunProvided":report["thirdBackendRunProvided"],
        "layoutSchemaForensicsProvided":report["layoutSchemaForensicsProvided"],
        "sourceTextBinaryProbeProvided":report["sourceTextBinaryProbeProvided"],
        "originalTextComponentsVerifiedByTwoBackends":report[
            "originalTextComponentsVerifiedByTwoBackends"],
        "unityAssetsChanged":False,
        "runtimeUIProven":False,
    },sort_keys=True))


if __name__=="__main__":
    main()
