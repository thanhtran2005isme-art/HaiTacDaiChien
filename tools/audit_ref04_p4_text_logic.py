#!/usr/bin/env python3
"""REF04 P4: source-only matrix of 62 Text, original Font and I2 provenance.

Never infer runtime language, selected Text translation or dynamic Text writer
from co-location, original m_Text, font identity or metadata declarations.
All per-object digests/IDs remain in gitignored output/.
"""
from __future__ import annotations

import argparse
from collections import Counter, defaultdict
import hashlib
import json
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
P3_CLASS = "REF04_P3_ORIGINAL_TEXT_FONT_LOCALIZATION_SOURCE_ONLY"
FONT_CLASS = "REF04_P3_LOCAL_FONT_OBJECT_PROVENANCE_NO_RUNTIME_RENDER"
LOCALIZER_CLASS = "REF04_P3_ORIGINAL_I2_TERMS_TWO_GENERATOR_SOURCE_PROBE"
P4_CLASS = "REF04_P4_TEXT_62_SOURCE_FONT_LOCALIZATION_DYNAMIC_EVIDENCE_ONLY"
VERIFIED_TERM = frozenset((
    "DUAL_BACKEND_LOCALIZER_TERMS_SOURCE_ONLY",
    "DUAL_SCHEMA_RIPPER_RAW_REPARSED_LOCALIZER_TERMS_SOURCE_ONLY",
))
EMPTY_SHA = hashlib.sha256(b"").hexdigest()


def sha_ok(value):
    return isinstance(value, str) and len(value) == 64 and all(
        c in "0123456789abcdef" for c in value)


def pointer_key(ref):
    if (not isinstance(ref, dict) or
        type(ref.get("sourceFileId")) is not int or
        type(ref.get("sourcePathId")) is not int or
        ref["sourceFileId"] < 0 or ref["sourcePathId"] <= 0):
        raise ValueError("Original Font PPtr invalid")
    return ref["sourceFileId"], ref["sourcePathId"]


def _check_documents(p3, font_proof, term_proof):
    if (p3.get("classification") != P3_CLASS or
        p3.get("sourceTextComponentsIndependentlyVerified") != 62 or
        p3.get("sourceTextStringsIndependentlyVerified") != 62 or
        p3.get("sourceTextFontPointersIndependentlyVerified") != 62 or
        p3.get("sourceTextFieldsVerified") is not True or
        p3.get("sourceLocalizationComponents") != 52 or
        p3.get("uniqueOriginalFontPointerCount") != 2 or
        p3.get("methodBodiesVerified") != 0 or
        p3.get("originalTextContentPublished") is not False or
        p3.get("runtimeTextAndLocalizationProven") is not False or
        p3.get("localizationKeyToTextBindingProven") is not False or
        p3.get("runtimeTextValuesProven") is not False or
        p3.get("runtimeLanguageChosen") is not None or
        p3.get("unityImportAllowed") is not False):
        raise ValueError("Original 62 Text source contract untrusted")
    if (font_proof.get("classification") != FONT_CLASS or
        font_proof.get("originalSourceSerializedFile") !=
            p3.get("originalSourceSerializedFile") or
        font_proof.get("originalIL2CPPSha256Pair") !=
            p3.get("originalIL2CPPSha256Pair") or
        font_proof.get("originalFontPPtrReferences") != 2 or
        font_proof.get("originalFontAssetIdentityFullyResolved") is not True or
        font_proof.get("runtimeFontRenderingProven") is not False or
        font_proof.get("unityImportAllowed") is not False):
        raise ValueError("Original Font source identity contract untrusted")
    pair = p3.get("originalIL2CPPSha256Pair", {})
    if (not sha_ok(pair.get("metadataSha256")) or
        not sha_ok(pair.get("libil2cppSha256")) or
        term_proof.get("classification") != LOCALIZER_CLASS or
        term_proof.get("originalSourceSerializedFile") !=
            p3.get("originalSourceSerializedFile") or
        term_proof.get("originalSourceMetadataSha256") != pair["metadataSha256"] or
        term_proof.get("originalSourceLibSha256") != pair["libil2cppSha256"] or
        term_proof.get("localizersChecked") != 52 or
        term_proof.get("sourceTermValuesPublished") is not False or
        term_proof.get("runtimeLanguageOrTranslationProven") is not False or
        term_proof.get("localizerToTextTargetVerified") is not False or
        term_proof.get("unityImportAllowed") is not False):
        raise ValueError("Original I2 localizer source pair or runtime gate untrusted")


def build(p3, font_proof, term_proof):
    _check_documents(p3, font_proof, term_proof)
    originals = p3.get("originalTextSourceEvidence")
    localizers = p3.get("sourceLocalizationComponentsEvidence")
    font_objects = font_proof.get("pointerResolutions")
    term_objects = term_proof.get("localizers")
    if (not isinstance(originals, list) or len(originals) != 62 or
        not isinstance(localizers, list) or len(localizers) != 52 or
        not isinstance(font_objects, list) or len(font_objects) != 2 or
        not isinstance(term_objects, list) or len(term_objects) != 52):
        raise ValueError("Incomplete original Text/Font/I2 input rows")
    fonts = {}
    for row in font_objects:
        key = pointer_key(row)
        if key in fonts or (row.get("localOriginalFontObjectVerified") is not True and
                            row.get("externalOriginalFontObjectVerified") is not True) or (
                row.get("localOriginalFontObjectVerified") is True and
                row.get("externalOriginalFontObjectVerified") is True) or (
                row.get("originalNativeType") != "Font" or
                not sha_ok(row.get("sourceRawObjectSha256")) or
                type(row.get("sourceRawObjectBytes")) is not int or
                row["sourceRawObjectBytes"] <= 0 or
                row.get("runtimeFontProven") is not False):
            raise ValueError("Original Font object proof missing, duplicate or inconsistent")
        fonts[key] = row
    if len(fonts) != 2:
        raise ValueError("Expected two distinct source Font objects")
    term_by_id = {}
    for row in term_objects:
        cid = row.get("componentPathId")
        fields = row.get("sourceTermFieldDigests")
        status = row.get("verificationStatus")
        if (type(cid) is not int or cid in term_by_id or
            not isinstance(fields, dict) or
            (bool(fields) and status not in VERIFIED_TERM) or
            row.get("runtimeTranslationProven") is not False or
            row.get("unityImportAllowed") is not False):
            raise ValueError("Original I2 localizer term evidence untrusted")
        for item in fields.values():
            if (not isinstance(item, dict) or
                not sha_ok(item.get("originalUtf8Sha256")) or
                type(item.get("originalByteLength")) is not int or
                item["originalByteLength"] < 0):
                raise ValueError("Untrusted original I2 term hash")
        term_by_id[cid] = row
    if sum(len(x["sourceTermFieldDigests"]) for x in term_by_id.values()) != (
            term_proof.get("sourceTermFieldsTwoBackendVerified")):
        raise ValueError("I2 original term verification count inconsistent")
    local_by_id = {}
    local_by_owner = defaultdict(list)
    for item in localizers:
        cid = item.get("componentPathId")
        origin = term_by_id.get(cid)
        if (type(cid) is not int or cid in local_by_id or origin is None or
            not isinstance(item.get("sourceClass"), str) or
            item["sourceClass"] not in ("I2.Loc.Localize", "TextLocalizeChecker") or
            item.get("sourceClass") != origin.get("sourceClass") or
            item.get("gameObjectPathId") != origin.get("gameObjectPathId") or
            item.get("originalObjectSha256") != origin.get("originalObjectSha256") or
            not sha_ok(item.get("originalObjectSha256")) or
            type(item.get("gameObjectPathId")) is not int or
            type(item.get("rectTransformPathId")) is not int or
            item.get("runtimeLocalizedText") is not None or
            item.get("localizationBindingProven") is not False):
            raise ValueError("Original localizer owner/class/hash conflicts with P3")
        if "rectTransformPathId" in origin and (
                item["rectTransformPathId"] != origin["rectTransformPathId"]):
            raise ValueError("Original localizer RectTransform owner conflict")
        local_by_id[cid] = item
        local_by_owner[(item["gameObjectPathId"], item["rectTransformPathId"])].append(cid)
    source_font_counts = Counter()
    source_text_state = Counter()
    rows = []
    seen = set()
    for text in sorted(originals, key=lambda r: r.get("componentPathId", -1)):
        cid = text.get("componentPathId")
        ref = text.get("originalFontPointer")
        key = pointer_key(ref)
        style = text.get("originalTextSourceValues", {})
        if (type(cid) is not int or cid in seen or
            type(text.get("gameObjectPathId")) is not int or
            type(text.get("rectTransformPathId")) is not int or
            not sha_ok(text.get("originalObjectSha256")) or
            not sha_ok(text.get("sourceTextUtf8Sha256")) or
            type(text.get("sourceTextUtf8ByteLength")) is not int or
            text["sourceTextUtf8ByteLength"] < 0 or
            type(text.get("sourceEmptyText")) is not bool or
            text["sourceEmptyText"] != (text["sourceTextUtf8ByteLength"] == 0) or
            (text["sourceEmptyText"] and text["sourceTextUtf8Sha256"] != EMPTY_SHA) or
            text.get("originalTextFieldStatus") != "DUAL_BACKEND_M_TEXT_FIELD" or
            text.get("originalFontFieldStatus") != "DUAL_BACKEND_M_FONT_FIELD" or
            key not in fonts or
            text.get("runtimeString") is not None or
            text.get("runtimeLanguage") is not None or
            text.get("runtimeFont") is not None or
            text.get("runtimeTextProven") is not False or
            text.get("unityImportAllowed") is not False or
            not isinstance(style, dict) or
            style.get("m_Font") != ref or
            not isinstance(style.get("m_Text"), dict) or
            style["m_Text"].get("sourceUtf8Sha256") != text["sourceTextUtf8Sha256"] or
            style["m_Text"].get("utf8Bytes") != text["sourceTextUtf8ByteLength"] or
            style["m_Text"].get("sourceTextIsEmpty") != text["sourceEmptyText"]):
            raise ValueError("Original Text source hash/Font/runtime evidence conflicts")
        seen.add(cid)
        colocated = sorted(local_by_owner.get(
            (text["gameObjectPathId"], text["rectTransformPathId"]), []))
        if (sorted(text.get("colocatedLocalizationComponentPathIds", [])) != colocated or
            text.get("localizationConnection") != (
                "SAME_ORIGINAL_GAMEOBJECT_ONLY_NO_BINDING_PROOF" if colocated else
                "NO_LOCALIZER_ON_ORIGINAL_GAMEOBJECT")):
            raise ValueError("Original localizer Text co-location does not match source")
        status = "ORIGINAL_EMPTY_TEXT_SOURCE" if text["sourceEmptyText"] else (
            "ORIGINAL_NONEMPTY_TEXT_SOURCE")
        source_text_state[status] += 1
        source_font_counts[key] += 1
        rows.append({
            "textComponentPathId": cid,
            "originalGameObjectPathId": text["gameObjectPathId"],
            "originalRectTransformPathId": text["rectTransformPathId"],
            "originalSourceTextSha256": text["sourceTextUtf8Sha256"],
            "originalSourceTextUtf8Bytes": text["sourceTextUtf8ByteLength"],
            "originalTextState": status,
            "originalTextStyleSourceFieldNames": sorted(style),
            "originalFontPointer": dict(ref),
            "originalFontRawObjectSha256": fonts[key]["sourceRawObjectSha256"],
            "originalFontSourceKind": ("EXTERNAL_SOURCE_FONT" if
                fonts[key].get("externalOriginalFontObjectVerified") else "LOCAL_SOURCE_FONT"),
            "colocatedLocalizerComponentPathIds": colocated,
            "colocatedLocalizerTermStatuses": [
                term_by_id[k]["verificationStatus"] for k in colocated],
            "localizedAssignmentStatus": ("COLOCATED_SOURCE_CANDIDATE_ONLY" if
                colocated else "NO_SOURCE_COLOCATED_LOCALIZER"),
            "dynamicTextWriterStatus": "UNKNOWN_NO_VERIFIED_RUNTIME_FIELD_WRITER",
            "runtimeText": None, "runtimeLocale": None, "runtimeSelectedFont": None,
            "runtimeTextMutationProven": False, "runtimeLocalizedBindingProven": False,
        })
    if len(seen) != 62 or set(source_font_counts) != set(fonts):
        raise ValueError("Source 62 Text to two original Font objects incomplete")
    if (p3.get("originalTextSameGameObjectLocalizationCandidates") != sum(
            bool(x["colocatedLocalizerComponentPathIds"]) for x in rows)):
        raise ValueError("Original Text/localizer colocation total mismatch")
    return {
        "classification": P4_CLASS,
        "originalSourceSerializedFile": p3["originalSourceSerializedFile"],
        "originalIL2CPPSha256Pair": p3["originalIL2CPPSha256Pair"],
        "sourceTextComponentsVerified": len(rows),
        "sourceFontObjectsVerified": len(fonts),
        "originalFontUsage": [
            {"sourceFileId": k[0], "sourcePathId": k[1],
             "originalTextComponentsReferencingFont": source_font_counts[k],
             "fontRawObjectSha256": fonts[k]["sourceRawObjectSha256"],
             "runtimeFontLoadProven": False, "runtimeFontLoaded": None}
            for k in sorted(fonts)],
        "sourceTextInitialStates": dict(sorted(source_text_state.items())),
        "sourceLocalizerComponentsChecked": len(local_by_id),
        "sourceTermFieldsIndependentlyVerified": term_proof["sourceTermFieldsTwoBackendVerified"],
        "originalSameOwnerLocalizationCandidates": sum(
            bool(x["colocatedLocalizerComponentPathIds"]) for x in rows),
        "sourceTextRows": rows,
        "runtimeDynamicTextWritersIndependentlyProven": 0,
        "runtimeLocalizedAssignmentsIndependentlyProven": 0,
        "runtimeLocale": None,
        "runtimeFontRenderingProven": False,
        "runtimeTextValuesProven": False,
        "runtimeTextLogicRecovered": False,
        "unityImportAllowed": False,
        "originalTextStringsPublished": False,
    }


def execute(root=ROOT):
    output = root / "output"
    inputs = [
        "ref04-p3-text-font-localization-source.json",
        "ref04-p3-original-font-object-provenance.json",
        "ref04-p3-localizer-term-binary-probe.json",
    ]
    p3, fonts, terms = [
        json.loads((output / name).read_text(encoding="utf-8"))
        for name in inputs]
    report = build(p3, fonts, terms)
    dest = output / "ref04-p4-62-text-logic-source-evidence.json"
    tmp = dest.with_suffix(".tmp")
    tmp.write_text(json.dumps(report, ensure_ascii=False, indent=2) + "\n",
                   encoding="utf-8")
    tmp.replace(dest)
    print(json.dumps({
        "classification": P4_CLASS,
        "sourceTextComponentsVerified": report["sourceTextComponentsVerified"],
        "sourceFontObjectsVerified": report["sourceFontObjectsVerified"],
        "sourceLocalizerComponentsChecked": report["sourceLocalizerComponentsChecked"],
        "sourceTermFieldsIndependentlyVerified": report["sourceTermFieldsIndependentlyVerified"],
        "runtimeTextLogicRecovered": False,
        "unityImportAllowed": False,
    }, sort_keys=True))
    return report


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=ROOT)
    args = parser.parse_args()
    execute(args.root.resolve())
