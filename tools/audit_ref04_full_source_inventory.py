#!/usr/bin/env python3
"""REF04 complete candidate-subtree UI/component source inventory. READ ONLY.

No invented coordinates, Canvas, runtime viewport, Sprite, class, state, or
field values. All data originates in previously audited original XAPK
SerializedFile, PPtr-linked MonoScript, two-backend IL2CPP source plans, and
native Sprite geometry. The candidate subtree is NOT the complete runtime
scene; parent/ancestor and runtime mutations require further source decoding.
Writes ONLY gitignored output/ref04-full-source-inventory.{json,md}.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
SCENE = "REF04-home-crew"
GRAPH = "SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE"
VERIFIED = "TWO_BACKEND_STRICT_SOURCE_VERIFIED_UI_FIELDS"
VISUAL = "EXACT_SOURCE_SPRITES_ON_DUAL_VERIFIED_IMAGE_COMPONENTS"
GEOMETRY = "REF04_EXACT_SOURCE_IMAGE_SPRITE_GEOMETRY"
REVIEW = "SINGLE_BACKEND_UI_FIELDS_NOT_FOR_PREFAB_IMPORT"
REQUIRED_RECT = (
    "anchorMin", "anchorMax", "pivot", "sizeDelta",
    "anchoredPosition", "localScale", "localRotation",
)


def sha(blob: bytes) -> str:
    return hashlib.sha256(blob).hexdigest()


def unique(items, key, description):
    keyed = {}
    for item in items:
        ident = item[key]
        if ident in keyed:
            raise ValueError("Duplicate original " + description + ": " + str(ident))
        keyed[ident] = item
    return keyed


def selected(scenes, description):
    matches = [sc for sc in scenes if sc["sceneId"] == SCENE]
    if len(matches) != 1:
        raise ValueError("Exactly one REF04 " + description + " expected")
    return matches[0]


def inventory(graph, deep, verified, visual, geometry, review, raw_sha):
    if (graph.get("version") != 1 or graph.get("classification") != GRAPH or
            deep.get("schemaVersion") != 1 or
            verified.get("classification") != VERIFIED or
            verified.get("verifiedComponents") != 1108 or
            verified.get("verifiedFieldValues") != 7451 or
            verified.get("singleBackendExcludedComponents") != 93 or
            verified.get("singleBackendExcludedFieldValues") != 651 or
            visual.get("classification") != VISUAL or
            visual.get("sourceBindings") != 963 or
            geometry.get("classification") != GEOMETRY or
            geometry.get("sourceBindings") != 265 or
            review.get("classification") != REVIEW or
            review.get("componentCount") != 93 or
            review.get("excludedFieldValues") != 651 or
            verified.get("sourceGraphSha256") != raw_sha["graph"] or
            visual.get("sourceGraphSha256") != raw_sha["graph"] or
            visual.get("verifiedUiPlanSha256") != raw_sha["verified"] or
            geometry.get("sourceGraphSha256") != raw_sha["graph"] or
            geometry.get("verifiedUiPlanSha256") != raw_sha["verified"] or
            geometry.get("verifiedVisualPlanSha256") != raw_sha["visual"] or
            geometry.get("nativeGeometryEvidenceSha256") != raw_sha["native"]):
        raise ValueError("REF04 original XAPK evidence missing/stale/contradictory")

    g = selected(graph["scenes"], "source GameObject graph")
    d = selected(deep["scenes"], "source MonoScript + native field inventory")
    v = selected(verified["scenes"], "dual-verified UI fields")
    p = selected(visual["scenes"], "Sprite-linked Image plan")
    if geometry.get("sceneId") != SCENE:
        raise ValueError("Original Sprite native geometry scene differs")
    if g["sourceSerializedFile"] != d["sourceFile"]:
        raise ValueError("REF04 SerializedFile identities differ")
    if (len(g["components"]) != g["stats"]["componentReferences"] or
            len(d["components"]) != g["stats"]["componentReferences"]):
        raise ValueError("Incomplete original REF04 component inventory")

    nodes = unique(g["nodes"], "rectTransformId", "RectTransform PathID")
    gos = unique(g["nodes"], "gameObjectId", "GameObject PathID")
    components = unique(g["components"], "pathId", "component PathID")
    decoded = unique(d["components"], "pathId", "decoded component PathID")
    managed = unique(v["components"], "componentPathId", "3C managed component")
    linked = unique(p["bindings"], "imageComponentPathId", "source Image/Sprite PPtr")
    native_sprites = unique(geometry["images"], "componentPathId", "native Sprite")
    singly = unique(
        [x for x in review["components"] if x["sceneId"] == SCENE],
        "componentPathId", "single-backend LayoutGroup component")
    if (set(components) != set(decoded) or
            not set(managed).issubset(components) or
            not set(linked).issubset(managed) or
            set(linked) != set(native_sprites) or
            not set(singly).issubset(components) or
            set(singly).intersection(managed) or
            len(linked) != 265):
        raise ValueError("Source REF04 component and Sprite PathID sets contradict")
    for cid, c in components.items():
        if c["kind"] != decoded[cid]["kind"]:
            raise ValueError("Original source component kind differs at " + str(cid))

    root_id = g["candidateRootTransform"]
    root = nodes.get(root_id)
    if not root:
        raise ValueError("REF04 candidate root missing from source graph")
    parent_links = {}
    for node in g["nodes"]:
        tid = node["rectTransformId"]
        if tid == root_id:
            continue
        parent = node.get("parent") or {}
        if parent.get("fileId") != 0 or parent.get("pathId") not in nodes:
            raise ValueError("REF04 source child parent outside verified tree")
        parent_links[tid] = parent["pathId"]
    seen_children = set()
    for node in g["nodes"]:
        ids = node.get("childTransformIds", [])
        if len(ids) != len(set(ids)):
            raise ValueError("Duplicate XAPK child transform pointers")
        for child in ids:
            if child not in nodes:
                # Candidate subtree can omit a sibling of a selected child
                # or descendants outside this source candidate: record as
                # an open decoding gap, never assume that child doesn't exist.
                continue
            if child in seen_children or parent_links.get(child) != node["rectTransformId"]:
                raise ValueError("Original child index/parent pointers contradict")
            seen_children.add(child)
    if seen_children != set(parent_links):
        raise ValueError("XAPK original parent-child pointers incomplete")

    statuses = collections.Counter()
    kinds = collections.Counter()
    classes = collections.Counter()
    missing = collections.Counter()
    result = []
    referenced = set()
    verified_fields = 0
    unresolved_items = []
    for node in g["nodes"]:
        tid, gid = node["rectTransformId"], node["gameObjectId"]
        rect = node.get("rect", {})
        unavailable_rect = [k for k in REQUIRED_RECT if rect.get(k) is None]
        ids = node["componentIds"]
        if len(ids) != len(set(ids)) or tid not in ids:
            raise ValueError("Original GameObject component pointer list invalid")
        if any(cid in referenced for cid in ids):
            raise ValueError("One source component assigned to multiple GameObjects")
        referenced.update(ids)
        entries = []
        for index, cid in enumerate(ids):
            original = components.get(cid)
            src = decoded.get(cid)
            if original is None or src is None or src.get("rectTransformId") != tid or                     src.get("gameObjectId") != gid:
                raise ValueError("Source component-to-GameObject ownership mismatch")
            if original.get("gameObjectPointer") not in (
                None, {"fileId": 0, "pathId": gid}):
                raise ValueError("Source native component owner PPtr differs")
            native_status = src.get("status", "UNKNOWN")
            rec = {
                "sourceSerializedFile": g["sourceSerializedFile"],
                "componentPathId": cid,
                "componentOrderInGameObject": index,
                "gameObjectPathId": gid,
                "rectTransformPathId": tid,
                "nativeKind": original["kind"],
                "monoScriptPointer": original.get("monoScriptPointer"),
                "monoScriptClass": src.get("className"),
                "sourceComponentEnabled": src.get(
                    "nativeEnabled", original.get("m_Enabled")),
                "sourceStatus": native_status,
                "rawSourceObjectSha256": (original.get("rawEvidence") or {}).get("sha256"),
                "verificationStatus": "UNVERIFIED_FIELDS",
                "verifiedSerializedFields": {},
                "unresolvedReason": None,
            }
            kinds[original["kind"]] += 1
            if rec["monoScriptClass"]:
                classes[rec["monoScriptClass"]] += 1
            if cid in managed:
                m = managed[cid]
                if (m["gameObjectPathId"] != gid or m["rectTransformPathId"] != tid or
                        m["className"] != src.get("className") or
                        m["rawObjectSha256"] != rec["rawSourceObjectSha256"]):
                    raise ValueError("XAPK source 3C field ownership/hash differs")
                rec["verificationStatus"] = "TWO_BACKEND_SOURCE_VERIFIED_FIELDS"
                rec["verifiedSerializedFields"] = {
                    field["name"]: field for field in m["fields"]
                }
                rec["verifiedFieldCount"] = len(m["fields"])
                verified_fields += len(m["fields"])
            elif cid in singly:
                s = singly[cid]
                if (s["gameObjectPathId"] != gid or
                        s["rectTransformPathId"] != tid or
                        s["rawObjectSha256"] != rec["rawSourceObjectSha256"] or
                        s["prefabImportAllowed"] is not False):
                    raise ValueError("REF04 single-backend component evidence contradicts XAPK")
                rec["verificationStatus"] = "SINGLE_BACKEND_FIELD_VALUES_BLOCKED"
                rec["singleBackendFieldNames"] = s["fieldNames"]
                rec["unresolvedReason"] = "Second independent parser has not verified fields"
            elif original["kind"] != "MonoBehaviour":
                vals = src.get("fields")
                if native_status == "NATIVE_FIELDS" and isinstance(vals, dict) and vals:
                    rec["verificationStatus"] = "XAPK_NATIVE_SERIALIZED_FIELD_SUBSET"
                    rec["verifiedSerializedFields"] = vals
                else:
                    rec["verificationStatus"] = "XAPK_NATIVE_KIND_OR_HEADER_ONLY"
                    rec["unresolvedReason"] = "Complete native component fields not extracted"
            else:
                rec["unresolvedReason"] = (
                    "Managed fields not dual-verified; decode exact XAPK IL2CPP "
                    "object layout and source MonoScript")
            if cid in linked:
                binding = linked[cid]
                shape = native_sprites[cid]
                if (rec["monoScriptClass"] != "UnityEngine.UI.Image" or
                        binding["gameObjectPathId"] != gid or
                        binding["rectTransformPathId"] != tid or
                        binding["sourceObjectSha256"] != rec["rawSourceObjectSha256"] or
                        shape["gameObjectPathId"] != gid or
                        shape["rectTransformPathId"] != tid or
                        shape["spriteFile"] != binding["spriteFile"]):
                    raise ValueError("REF04 Image/Sprite source owner differs")
                rec["exactSourceSprite"] = {
                    "spriteFile": binding["spriteFile"],
                    "sourceGeometryStatus": shape["nativeSpriteGeometryStatus"],
                    "sourceRectSize": shape.get("sourceRectSize"),
                    "sourceTextureRectOffset": shape.get("sourceTextureRectOffset"),
                    "originalBorder": shape.get("border"),
                    "originalPixelsPerUnit": shape.get("pixelsPerUnit"),
                }
            statuses[rec["verificationStatus"]] += 1
            entries.append(rec)
            if rec["unresolvedReason"]:
                missing[rec["unresolvedReason"]] += 1
                unresolved_items.append({
                    "gameObjectPathId": gid,
                    "componentPathId": cid,
                    "monoScriptClass": rec.get("monoScriptClass"),
                    "reason": rec["unresolvedReason"],
                    "nextStep": "Decode from original XAPK; ask user only if source evidence remains unavailable",
                })
        parent = node.get("parent") or {}
        children = node.get("childTransformIds", [])
        result.append({
            "sourceSerializedFile": g["sourceSerializedFile"],
            "gameObjectPathId": gid,
            "rectTransformPathId": tid,
            "sourceActive": node.get("active") if node.get("active") in (0,1) else None,
            "sourceParentPointer": parent,
            "sourceChildTransformPathIdsOrdered": children,
            "sourceSiblingOrder": (
                nodes[parent["pathId"]]["childTransformIds"].index(tid)
                if tid in parent_links else None),
            "rectSource": rect,
            "rectFieldsNotExtracted": unavailable_rect,
            "componentCount": len(entries),
            "components": entries,
        })
        for field in unavailable_rect:
            missing["RectTransform." + field + " SOURCE_UNAVAILABLE"] += 1
    if referenced != set(components):
        raise ValueError("REF04 source component inventory not complete")

    all_images = {cid for cid, row in managed.items()
                  if row["className"] == "UnityEngine.UI.Image"}
    if not set(linked).issubset(all_images):
        raise ValueError("Source Image/Sprite subset contradicts 3C Image inventory")
    candidate_layouts = [x for x in result for c in x["components"]
                         if c["monoScriptClass"] in (
                             "UnityEngine.UI.HorizontalLayoutGroup",
                             "UnityEngine.UI.VerticalLayoutGroup",
                             "UnityEngine.UI.GridLayoutGroup")]
    _ = candidate_layouts
    root_parent = root.get("parent", {})
    parent_unknown = root_parent.get("fileId") != 0 or root_parent.get("pathId") != 0
    blockers = [
        {
            "field": "REF04.runtimeCanvasAncestorAndViewport",
            "verificationStatus": "BLOCKED_SOURCE_NOT_YET_RECOVERED",
            "reason": "Candidate serialized root is not proven as runtime screen root; "
                      "Canvas resolution/parent/safe area and runtime actions unavailable",
            "nextDecodeAction": "Trace original parent prefab/scene pointers and runtime IL2CPP",
        },
        {
            "field": "REF04.managedLayoutGroupsAndDynamicHUD",
            "verificationStatus": "BLOCKED_SOURCE_NOT_YET_RECOVERED",
            "reason": "Single-backend LayoutGroup fields not independently verified; "
                      "runtime layout/text/state actions not reconstructed",
            "nextDecodeAction": "Independently verify exact IL2CPP field offsets, "
                                "serialized objects and runtime layout calculations",
        },
    ]
    return {
        "schemaVersion": 1,
        "classification": "REF04_ALL_SOURCE_CANDIDATE_UI_COMPONENT_INVENTORY_READ_ONLY",
        "scope": "Every component referenced by REF04 candidate XAPK serialized "
                 "subtree; NOT an assertion that all runtime UI ancestors, "
                 "runtime-instantiated children or dynamic screens are recovered",
        "sourceProofSha256": raw_sha,
        "sceneId": SCENE,
        "sourceSerializedFile": g["sourceSerializedFile"],
        "root": {"rectTransformPathId": root_id,
                 "gameObjectPathId": root["gameObjectId"],
                 "parent": root_parent, "runtimeViewportProven": False,
                 "parentOutsideCandidateOrUnknown": parent_unknown},
        "counts": {
            "gameObjects": len(result),
            "rectTransforms": len(nodes),
            "serializedComponentRecords": len(components),
            "referencedComponentIDs": len(referenced),
            "componentKinds": dict(sorted(kinds.items())),
            "monoScriptClasses": dict(sorted(classes.items())),
            "fieldEvidenceStatuses": dict(sorted(statuses.items())),
            "dualVerifiedManagedComponents": sum(c in managed for c in components),
            "dualVerifiedManagedFieldValues": verified_fields,
            "singlyVerifiedLayoutComponentsBlocked": len(singly),
            "singleBackendLayoutFieldNamesNotImportable": sum(
                x["fieldCount"] for x in singly.values()),
            "allDualVerifiedImageComponents": len(all_images),
            "originalSpriteLinkedImages": len(linked),
            "verifiedImagesWithoutSourceSprite": len(all_images)-len(linked),
            "sourceNativeSpriteGeometryBindings": sum(
                item.get("applyGeometry") is True for item in geometry["images"]),
            "unresolvedComponentRecords": len(unresolved_items),
            "missingFieldGroups": dict(sorted(missing.items())),
        },
        "sourceFieldApplicationAllowed": False,
        "unityAssetsChanged": False,
        "requiresMoreXapkDecoding": True,
        "blockers": blockers,
        "unresolvedComponents": unresolved_items,
        "gameObjects": result,
    }


def write_report(result, destination: Path):
    destination.parent.mkdir(parents=True, exist_ok=True)
    tmp = destination.with_suffix(".tmp")
    tmp.write_text(json.dumps(result, ensure_ascii=False, indent=2)+"\n",
                   encoding="utf-8")
    tmp.replace(destination)
    count = result["counts"]
    lines = [
        "# REF04 — KIỂM KÊ UI TỪ XAPK (CHỈ ĐỌC / KHÔNG ĐOÁN)",
        "",
        "**Phạm vi:** cây ứng viên REF04 gốc, chưa chứng minh toàn bộ runtime UI. "
        "**Không áp các giá trị chưa đủ bằng chứng.**",
        "",
        "| Chứng cứ | Số lượng |", "|---|---:|",
        f"| GameObject/RectTransform | {count['gameObjects']} |",
        f"| Component reference/source object | {count['serializedComponentRecords']} |",
        f"| Managed component được hai decoder chứng minh | "
        f"{count['dualVerifiedManagedComponents']} |",
        f"| Managed field có bằng chứng đồng thuận | "
        f"{count['dualVerifiedManagedFieldValues']} |",
        f"| Image có Sprite nguồn | {count['originalSpriteLinkedImages']} |",
        f"| Image 3C không có Sprite PPtr | "
        f"{count['verifiedImagesWithoutSourceSprite']} |",
        f"| LayoutGroup chỉ một backend (CẤM nhập) | "
        f"{count['singlyVerifiedLayoutComponentsBlocked']} |",
        f"| Component chưa có field đầy đủ | {count['unresolvedComponentRecords']} |",
        "",
        "## Loại component native/managed",
    ]
    for name, number in count["componentKinds"].items():
        lines.append(f"- {name}: {number}")
    lines += ["", "## Các điều kiện CHƯA ĐƯỢC CHỨNG MINH / PHẢI GIẢI MÃ TIẾP"]
    for issue in result["blockers"]:
        lines.append(f"- **{issue['field']}**: {issue['reason']}. "
                     f"Việc tiếp theo: {issue['nextDecodeAction']}")
    lines += ["", "## Nguyên tắc",
              "Mọi GameObject → component → field và giá trị RectTransform "
              "được liệt kê trong JSON riêng tư cùng PathID gốc. "
              "Không tự thêm Canvas, chiều rộng 1600, root scale 1, "
              "LayoutGroup hay thứ tự icon tùy ý.",
              "Nếu đọc tiếp XAPK/IL2CPP không thể chứng minh được, "
              "dừng đúng field đó và hỏi người dùng.", ""]
    md = destination.with_suffix(".md")
    tmp = md.with_suffix(".tmp")
    tmp.write_text("\n".join(lines),encoding="utf-8")
    tmp.replace(md)


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=ROOT)
    args = parser.parse_args()
    root = args.root.resolve()
    names = {
        "graph": "original-unity-graph.json",
        "deep": "deep-ui-source-evidence.json",
        "verified": "verified-ui-prefab-plan.json",
        "visual": "verified-visual-preview-plan.json",
        "geometry": "ref04-static-image-geometry.json",
        "review": "single-backend-layout-review.json",
        "native": "local-ui-components.json",
    }
    raw = {k:(root / "output" / n).read_bytes() for k,n in names.items()}
    docs = {k:json.loads(data) for k,data in raw.items()}
    result = inventory(
        docs["graph"],docs["deep"],docs["verified"],
        docs["visual"],docs["geometry"],docs["review"],
        {k:sha(blob) for k,blob in raw.items()})
    path = root / "output/ref04-full-source-inventory.json"
    write_report(result,path)
    print(json.dumps({
        "status": result["classification"],
        "counts": {k:result["counts"][k] for k in (
            "gameObjects","serializedComponentRecords",
            "dualVerifiedManagedComponents","dualVerifiedManagedFieldValues",
            "allDualVerifiedImageComponents","originalSpriteLinkedImages",
            "verifiedImagesWithoutSourceSprite",
            "singlyVerifiedLayoutComponentsBlocked",
            "unresolvedComponentRecords")},
        "moreXapkDecodeNeeded": True,
        "unityAssetsChanged": False,
        "report": str(path),
    },sort_keys=True))


if __name__=="__main__":
    main()
