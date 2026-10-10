#!/usr/bin/env python3
"""REF04: cross-check original serialized LayoutGroup with TWO generated schemas.

AssetStudio and AssetRipper are distinct IL2CPP TypeTree backends using the
same exact XAPK source pair. Reparse ALL bytes using a separate struct-based
reader against each generated schema, compare field values AND byte offsets.
Neither runtime layout nor Unity-import permission follows from source proof.
"""
from __future__ import annotations

import collections
import hashlib

import il2cpp_refs as refs
import ref04_layout_raw_parser as raw

BACKENDS = ("AssetStudio", "AssetRipper")
SUCCESS = "TWO_SOURCE_SCHEMAS_RAW_FIELDS_AND_OFFSETS_AGREE_NOT_IMPORTED"


def source_safe_raw_failure(exc):
    """Stable reason categories only; never publish game bytes or field values."""
    text = str(exc)
    hints = (
        ("Raw parser did not consume exact source object bytes", "TRAILING_OBJECT_BYTES"),
        ("Serialized object ended before schema field", "SCHEMA_OVERRUN"),
        ("Raw field differs from original strict source", "FIELD_VALUE_OR_TYPE_CONFLICT"),
        ("Unrecognized source field type", "UNSUPPORTED_SOURCE_TYPE"),
        ("Source schema field consumed no bytes", "EMPTY_SOURCE_NODE"),
        ("Serialized object invalid or oversized", "OBJECT_SIZE"),
        ("Original serialized byte order unknown", "SOURCE_ENDIAN"),
        ("Original source byte order unavailable", "SOURCE_ENDIAN"),
        ("Source GameObject/MonoScript/native state changed", "SOURCE_POINTER"),
        ("Source managed field missing in raw replay", "FIELD_MISSING"),
        ("Raw replay changed original object hash", "RAW_SHA"),
        ("Incomplete source raw field span accounting", "FIELD_SPAN_INCOMPLETE"),
        ("Source string byte length invalid", "SOURCE_STRING_LENGTH"),
        ("Source array element count invalid", "SOURCE_ARRAY_LENGTH"),
        ("TypeTree depth/node budget exceeded", "TYPE_TREE_DEPTH"),
        ("Unsupported source array schema", "UNSUPPORTED_ARRAY_SCHEMA"),
        ("Ambiguous duplicate source schema field", "DUPLICATE_SCHEMA_FIELD"),
    )
    for msg, category in hints:
        if msg in text:
            return category
    return "OTHER_" + type(exc).__name__


def validate_header(parsed, source):
    head = parsed["nativeHeader"]
    ptr = source["scriptPointer"]
    if (refs.pptr(head.get("m_GameObject")) != (0, source["gameObjectId"]) or
        refs.pptr(head.get("m_Script")) != (ptr["fileId"], ptr["pathId"]) or
        head.get("m_Enabled") != source.get("nativeEnabled")):
        raise raw.RawWalkBlocked("Source GameObject/MonoScript/native state changed")


def strict_raw_schema_agreement(reader, source, schemas, expected):
    """No fallback to one decoder and no arbitrary field-coordinate search."""
    result = {
        "status": "BLOCKED_INDEPENDENT_SCHEMAS_UNAVAILABLE",
        "sourceFieldsIndependentlyVerified": 0,
        "independentFieldNames": [],
        "backendEvidence": {},
        "originalSerializedFieldValuesPublished": False,
        "unityImportAllowed": False,
        "runtimeLayoutProven": False,
    }
    source_sha = source["binaryProof"]["rawObjectSha256"]
    original_bytes = reader.get_raw_data()
    if (not original_bytes or reader.type.name != "MonoBehaviour" or
            hashlib.sha256(original_bytes).hexdigest() != source_sha):
        raise ValueError("Original REF04 serialized LayoutGroup source SHA mismatch")
    if not expected or set(expected) != set(source.get("fields", {})):
        raise ValueError("Original single-backend expected field set changed")
    if set(schemas) != set(BACKENDS) or any(schemas.get(k) is None for k in BACKENDS):
        return result
    if schemas["AssetStudio"] is schemas["AssetRipper"]:
        result["status"] = "BLOCKED_SHARED_SCHEMA_OBJECT_NOT_INDEPENDENT"
        return result
    try:
        endian = raw.byte_order(reader)
    except raw.RawWalkBlocked:
        result["status"] = "BLOCKED_SOURCE_ENDIAN_UNKNOWN"
        return result
    probes = {}
    for backend in BACKENDS:
        try:
            detail = raw.reparse_strict(
                original_bytes, schemas[backend], expected, endian=endian)
            validate_header(detail, source)
            if detail["fullObjectSha256"] != source_sha:
                raise raw.RawWalkBlocked("Raw replay changed original object hash")
        except (raw.RawWalkBlocked, ValueError, TypeError, KeyError) as exc:
            result["backendEvidence"][backend] = {
                "status": "BLOCKED_RAW_OBJECT_" + type(exc).__name__,
                "failureCategory": source_safe_raw_failure(exc)}
            result["status"] = "BLOCKED_INDEPENDENT_SCHEMA_RAW_PARSE"
            continue
        probes[backend] = detail
        result["backendEvidence"][backend] = {
            "status": "FULL_ORIGINAL_OBJECT_REPARSED_SOURCE_IDENTICAL",
            "sourceObjectSha256": detail["fullObjectSha256"],
            "sourceFieldByteSpans": detail["fieldByteSpans"],
        }
    if len(probes) != len(BACKENDS):
        return result
    a, b = (probes[k]["fieldByteSpans"] for k in BACKENDS)
    if set(a) != set(expected) or a != b:
        result["status"] = "BLOCKED_INDEPENDENT_FIELD_BYTE_SPANS_CONFLICT"
        return result
    result["status"] = SUCCESS
    result["sourceFieldsIndependentlyVerified"] = len(expected)
    result["independentFieldNames"] = sorted(expected)
    return result


def report_totals(rows):
    """Never count fields by declarations; only complete exact-source proof."""
    counts = collections.Counter()
    verified = blocked = 0
    for row in rows:
        data = row["independentSchemaRawSourceCheck"]
        count = row["sourceFieldCountBlocked"]
        if count < 1:
            raise ValueError("LayoutGroup has no expected fields")
        if data.get("unityImportAllowed") is not False or (
                data.get("runtimeLayoutProven") is not False):
            raise ValueError("Independent schema audit cannot authorize Unity")
        status = data["status"]
        actual = data["sourceFieldsIndependentlyVerified"]
        names = data["independentFieldNames"]
        if status == SUCCESS:
            if (actual != count or len(names) != count or
                    set(names) != set(row.get("sourceExpectedFieldNames", []))):
                raise ValueError("Incomplete independent source LayoutGroup evidence")
            verified += count
        else:
            if actual or names:
                raise ValueError("Partial/bad source evidence must remain BLOCKED")
            blocked += count
        counts[status] += 1
    return {
        "sourceFieldsVerifiedByTwoGeneratedSchemas": verified,
        "sourceFieldsMissingIndependentSchemaProof": blocked,
        "independentSchemaProofStatusCounts": dict(sorted(counts.items())),
        "unityImportAllowed": False,
        "runtimeLayoutProven": False,
    }
