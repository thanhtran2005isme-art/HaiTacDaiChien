#!/usr/bin/env python3
"""Read-only REF04 LayoutGroup TypeTree forensics from the original XAPK.

Compare independently generated schemas and strict parse outcomes for the exact
24 source objects; NEVER promote single-backend fields or write Unity assets.
A schema match alone is NOT a field-value or runtime-layout verification.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
import struct
from pathlib import Path

import export_local_ui_layout as layout
import probe_ref04_layout_third_backend as third
import recover_managed_ui_fields as recovery
import ref04_layout_raw_parser as rawparse
import ref04_layout_dual_schema_bytes as dual_schema

ROOT = Path(__file__).resolve().parents[1]
SCENE = "REF04-home-crew"
BACKENDS = ("AssetStudio", "AssetRipper")
MAX_SCHEMA_NODES = 20000


def schema_entries(root):
    """Describe generated TypeTree structure, not guessed byte offsets/values."""
    if root is None:
        raise ValueError("Missing generated TypeTree root")
    entries = []
    stack = [(root, "root")]
    while stack:
        node, path = stack.pop()
        if len(entries) >= MAX_SCHEMA_NODES:
            raise ValueError("Generated TypeTree exceeds safe node count")
        children = getattr(node, "m_Children", None)
        if children is None:
            raise ValueError("TypeTree child list absent: " + path)
        label = {
            "path": path,
            "type": str(getattr(node, "m_Type", "")),
            "byteSize": int(getattr(node, "m_ByteSize", -1)),
            "metaFlag": int(getattr(node, "m_MetaFlag", 0)),
            "childCount": len(children),
        }
        entries.append(label)
        for index in range(len(children) - 1, -1, -1):
            child = children[index]
            name = getattr(child, "m_Name", None)
            if not isinstance(name, str):
                raise ValueError("Invalid TypeTree field name at " + path)
            stack.append((child, path + "/" + str(index) + ":" + name))
    if not entries or entries[0]["childCount"] == 0:
        raise ValueError("Empty generated TypeTree")
    return entries


def summarize_schema(entries):
    blob = json.dumps(entries, sort_keys=True, separators=(",", ":")).encode("utf-8")
    return {"sha256": hashlib.sha256(blob).hexdigest(),
            "nodeCount": len(entries)}


def first_schema_difference(a, b):
    for i in range(max(len(a), len(b))):
        left = a[i] if i < len(a) else None
        right = b[i] if i < len(b) else None
        if left != right:
            return {"index": i, "assetStudio": left, "assetRipper": right}
    return None


def inspect_object(reader, source_row, generators):
    raw_sha = hashlib.sha256(reader.get_raw_data()).hexdigest()
    if reader.type.name != "MonoBehaviour" or (
        raw_sha != source_row["binaryProof"]["rawObjectSha256"]):
        raise ValueError("REF04 LayoutGroup original object changed")
    report = {}
    strict_values = {}
    schema_nodes = {}
    for backend in BACKENDS:
        generator = generators[backend]
        entry = {"schemaStatus": "BLOCKED_NOT_PROBED",
                 "strictParseStatus": "BLOCKED_NOT_PROBED"}
        report[backend] = entry
        if generator is None:
            entry["schemaStatus"] = "BLOCKED_GENERATOR_UNAVAILABLE"
            entry["strictParseStatus"] = "BLOCKED_GENERATOR_UNAVAILABLE"
            continue
        try:
            nodes = generator.get_nodes_up(source_row["assembly"],
                                           source_row["className"])
            native = recovery.exact_source_unity_header(reader)
            merged = recovery.verified_native_header_root(nodes, native)
            desc = schema_entries(merged)
            entry["schemaStatus"] = "SCHEMA_GENERATED_SOURCE_VERSION"
            entry["schema"] = summarize_schema(desc)
            entry["_entries"] = desc
            schema_nodes[backend] = merged
        except (recovery.RecoveryBlocked, ValueError, TypeError,
                AttributeError, AssertionError) as exc:
            code = exc.code if isinstance(exc, recovery.RecoveryBlocked) else type(exc).__name__
            entry["schemaStatus"] = "BLOCKED_SCHEMA_" + code
        try:
            fields, proof = recovery.verified_fields(
                reader, source_row, generator, use_unitypy_native_header=True)
            if (proof.get("rawObjectSha256") != raw_sha or
                proof.get("nativeHeaderMethod") !=
                    "UNITYPY_EXACT_SOURCE_UNITY_VERSION" or
                not proof.get("exactSourcePointerChecked") or
                not proof.get("strictObjectSizeChecked")):
                raise ValueError("Strict parse source proof incomplete")
            strict_values[backend] = fields
            entry["strictParseStatus"] = "STRICT_SOURCE_OBJECT_PARSED"
            entry["strictFieldNames"] = sorted(fields)
        except recovery.RecoveryBlocked as exc:
            entry["strictParseStatus"] = "BLOCKED_" + exc.code
        except ValueError:
            entry["strictParseStatus"] = "BLOCKED_INCOMPLETE_SOURCE_PROOF"

    studio = report["AssetStudio"].pop("_entries", None)
    ripper = report["AssetRipper"].pop("_entries", None)
    difference = first_schema_difference(studio, ripper) if (
        studio is not None and ripper is not None) else None
    if studio is None or ripper is None:
        comparison = "BLOCKED_SCHEMA_COMPARISON"
    elif difference is not None:
        comparison = "SCHEMA_STRUCTURE_DIFF_NOT_FIELD_PROOF"
    else:
        comparison = "SCHEMAS_IDENTICAL_NOT_FIELD_PROOF"

    if len(strict_values) == 2:
        equal = (set(strict_values["AssetStudio"]) ==
                 set(strict_values["AssetRipper"]) and
                 all(type(strict_values["AssetStudio"][k]) is
                     type(strict_values["AssetRipper"][k]) and
                     strict_values["AssetStudio"][k] == strict_values["AssetRipper"][k]
                     for k in strict_values["AssetStudio"]))
        agreement = ("STRICT_SOURCE_FIELD_VALUES_AGREE_REVIEW_ONLY" if equal
                     else "BLOCKED_STRICT_FIELD_CONFLICT")
    else:
        agreement = "BLOCKED_INDEPENDENT_STRICT_PARSE"

    # This uses independently implemented struct reads on the exact raw object,
    # but the schema still derives from AssetStudio: never claim independent
    # schema recovery or allow application of those byte spans to Unity.
    raw_result = {"status": "BLOCKED_STUDIO_SCHEMA_OR_STRICT_PARSE",
                  "sourceByteSpans": {},
                  "derivedSchemaOnly": True,
                  "unityImportAllowed": False}
    if "AssetStudio" in schema_nodes and "AssetStudio" in strict_values:
        try:
            proof = rawparse.reparse_strict(
                reader.get_raw_data(), schema_nodes["AssetStudio"],
                strict_values["AssetStudio"], endian=rawparse.byte_order(reader))
            obj = proof["nativeHeader"]
            owner = recovery.refs.pptr(obj["m_GameObject"])
            script = recovery.refs.pptr(obj["m_Script"])
            expected_script = source_row["scriptPointer"]
            if (owner != (0, source_row["gameObjectId"]) or
                script != (expected_script["fileId"],
                           expected_script["pathId"]) or
                obj["m_Enabled"] != source_row.get("nativeEnabled")):
                raise rawparse.RawWalkBlocked("Raw replay source native pointer mismatch")
            raw_result = {
                "status": "RAW_BYTES_REPARSED_DERIVED_SCHEMA_REVIEW_ONLY",
                "sourceByteSpans": proof["fieldByteSpans"],
                "sourceObjectSha256": proof["fullObjectSha256"],
                "derivedSchemaOnly": True,
                "unityImportAllowed": False,
            }
        except (rawparse.RawWalkBlocked, ValueError, TypeError,
                KeyError, struct.error) as exc:
            raw_result["status"] = "BLOCKED_RAW_REPARSE_" + type(exc).__name__

    independent = dual_schema.strict_raw_schema_agreement(
        reader, source_row, schema_nodes,
        strict_values.get("AssetStudio", {}))

    return {"backendResults": report, "schemaComparison": comparison,
            "firstSchemaDifference": difference,
            "fieldAgreement": agreement,
            "rawByteReparse": raw_result,
            "independentSchemaRawSourceCheck": independent,
            "unityImportAllowed": False,
            "runtimeLayoutProven": False}


def build_report(plan, inspected, source_file, source_pair):
    if set(plan) != set(inspected) or len(plan) != third.EXPECTED_LAYOUTS:
        raise ValueError("All 24 original REF04 LayoutGroup PathIDs required")
    rows = []
    for cid, source in sorted(plan.items()):
        observation = inspected[cid]
        if observation.get("unityImportAllowed") is not False or (
            observation.get("runtimeLayoutProven") is not False):
            raise ValueError("Cannot promote forensic results into runtime fields")
        independently_checked = observation.get("independentSchemaRawSourceCheck")
        if independently_checked is None:
            independently_checked = {
                "status": "BLOCKED_INDEPENDENT_SCHEMAS_UNAVAILABLE",
                "sourceFieldsIndependentlyVerified": 0,
                "independentFieldNames": [],
                "backendEvidence": {},
                "originalSerializedFieldValuesPublished": False,
                "unityImportAllowed": False,
                "runtimeLayoutProven": False,
            }
        rows.append({
            "independentSchemaRawSourceCheck": independently_checked,
            "sourceExpectedFieldNames": sorted(source["fields"]),
            "componentPathId": cid,
            "gameObjectPathId": source["gameObjectId"],
            "rectTransformPathId": source["rectTransformId"],
            "sourceClass": source["className"],
            "sourceObjectSha256": source["binaryProof"]["rawObjectSha256"],
            "sourceFieldCountBlocked": len(source["fields"]),
            **observation,
        })
    blocked = sum(x["sourceFieldCountBlocked"] for x in rows)
    if blocked != third.EXPECTED_FIELDS:
        raise ValueError("Original 168 single-backend field inventory changed")
    independent_summary = dual_schema.report_totals(rows)
    return {
        "schemaVersion": 1,
        "classification": "REF04_LAYOUTGROUP_SCHEMA_FORENSICS_READ_ONLY",
        "sourceSerializedFile": source_file,
        "exactSourcePair": source_pair,
        "layoutGroupsInspected": len(rows),
        "sourceFieldValuesStillBlockedFromUnity": blocked,
        **independent_summary,
        "schemaComparisonCounts": dict(sorted(collections.Counter(
            x["schemaComparison"] for x in rows).items())),
        "rawByteReparseCounts": dict(sorted(collections.Counter(
            x.get("rawByteReparse", {}).get("status", "NOT_PROBED")
            for x in rows).items())),
        "fieldAgreementCounts": dict(sorted(collections.Counter(
            x["fieldAgreement"] for x in rows).items())),
        "unityImportAllowed": False,
        "runtimeAlignmentProven": False,
        "originalUiAssetsChanged": False,
        "layoutGroups": rows,
    }


def execute(root=ROOT, *, xapk=None, unitypy=None):
    if unitypy is None:
        import UnityPy as unitypy
    folder = root / "output"
    studio = json.loads((folder / "phase3b-assetstudio.json").read_text(encoding="utf-8"))
    review = json.loads((folder / "single-backend-layout-review.json").read_text(encoding="utf-8"))
    graph = json.loads((folder / "original-unity-graph.json").read_text(encoding="utf-8"))
    plan, origin_file = third.source_plan(studio, review, graph)
    version = recovery.exact_unity_version(studio["scenes"])
    xapk = xapk or next(iter(sorted(root.glob("*.xapk"))), None)
    if xapk is None:
        raise FileNotFoundError("Original XAPK missing")
    generators = {}
    source_proofs = []
    for backend in BACKENDS:
        try:
            generators[backend], proof = recovery.source_generator(
                xapk, version, backend=backend)
            source_proofs.append(proof)
        except recovery.RecoveryBlocked:
            generators[backend] = None
    if not source_proofs:
        raise ValueError("Neither original IL2CPP schema backend is available")
    if any(p["library"]["sha256"] != source_proofs[0]["library"]["sha256"] or
           p["metadata"]["sha256"] != source_proofs[0]["metadata"]["sha256"] or
           p["gameUnityVersion"] != version for p in source_proofs):
        raise ValueError("Two TypeTree generators do not share original XAPK bytes")

    scenes = json.loads(
        (root / "unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json")
        .read_text(encoding="utf-8"))["scenes"]
    scene = next(s for s in scenes if s["id"] == SCENE)
    if scene["source"] != origin_file:
        raise ValueError("REF04 SerializedFile source mismatch")
    inspected = {}

    class Interceptor:
        def load(self, buffer):
            env = unitypy.load(buffer)
            groups = collections.defaultdict(dict)
            for reader in env.objects:
                groups[id(reader.assets_file)][int(reader.path_id)] = reader
            try:
                original = layout.choose_serialized_file(scene, groups)
            except ValueError:
                return env
            if inspected:
                raise ValueError("REF04 original bundle encountered twice")
            for cid, row in sorted(plan.items()):
                reader = original.get(cid)
                if reader is None:
                    raise ValueError("Original LayoutGroup missing: " + str(cid))
                inspected[cid] = inspect_object(reader, row, generators)
            return env

    layout.build(root=root, xapk=xapk, unitypy=Interceptor())
    proof = {"unityVersion": version,
             "libil2cppSha256": source_proofs[0]["library"]["sha256"],
             "globalMetadataSha256": source_proofs[0]["metadata"]["sha256"],
             "backendsAvailable": [x for x in BACKENDS if generators[x] is not None]}
    result = build_report(plan, inspected, origin_file, proof)
    out = folder / "ref04-layout-schema-forensics.json"
    tmp = out.with_suffix(".tmp")
    tmp.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n",
                   encoding="utf-8")
    tmp.replace(out)
    print(json.dumps({
        "classification": result["classification"],
        "layoutGroupsInspected": result["layoutGroupsInspected"],
        "sourceFieldValuesStillBlockedFromUnity":
            result["sourceFieldValuesStillBlockedFromUnity"],
        "schemaComparisonCounts": result["schemaComparisonCounts"],
        "fieldAgreementCounts": result["fieldAgreementCounts"],
        "rawByteReparseCounts": result["rawByteReparseCounts"],
        "independentSchemaProofStatusCounts":
            result["independentSchemaProofStatusCounts"],
        "sourceFieldsVerifiedByTwoGeneratedSchemas":
            result["sourceFieldsVerifiedByTwoGeneratedSchemas"],
        "sourceFieldsMissingIndependentSchemaProof":
            result["sourceFieldsMissingIndependentSchemaProof"],
        "unityImportAllowed": False,
        "originalUiAssetsChanged": False,
    }, sort_keys=True))
    return result


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=ROOT)
    args = parser.parse_args()
    execute(args.root.resolve())
