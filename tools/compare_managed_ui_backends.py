#!/usr/bin/env python3
"""Compare STRICT source-verified UI field values from independent IL2CPP backends.

Inputs and any decoded values remain inside gitignored output/. Only aggregate
counts/errors should be printed in CI. No changes to prefab, binary or XAPK.
"""
from __future__ import annotations

import argparse
import collections
import json
from pathlib import Path

SUCCEEDED = "GENERATED_TYPETREE_SOURCE_VERIFIED"


def canonical_index(doc):
    if not isinstance(doc, dict) or len(doc.get("scenes", [])) != 5:
        raise ValueError("Exactly five original source scene candidates required")
    proof = doc.get("generatedBinaryProof") or {}
    if not proof.get("unityPyNativeHeader"):
        raise ValueError("Independent source-version native header check required")
    if doc.get("stats", {}).get("componentCount") != 5346:
        raise ValueError("Source graph component count is not 5346")
    if doc.get("globalMonoScriptResolution", {}).get("resolved") != 2320:
        raise ValueError("MonoScript pointer source inventory changed")
    if not proof.get("library", {}).get("sha256") or not proof.get("metadata", {}).get("sha256"):
        raise ValueError("Missing original binary provenance hashes")
    out = {}
    checked = 0
    for scene in doc["scenes"]:
        scene_id = scene["sceneId"]
        for item in scene["components"]:
            checked += 1
            key = (scene_id, item["pathId"])
            if key in out:
                raise ValueError("Duplicate component PathID in source scene")
            out[key] = item
    if checked != 5346:
        raise ValueError("Source component inventory incomplete")
    return out, proof


def compare(left, right):
    a, pa = canonical_index(left)
    b, pb = canonical_index(right)
    if set(a) != set(b):
        raise ValueError("Source component sets differ")
    for ident in ("gameUnityVersion",):
        if pa.get(ident) != pb.get(ident):
            raise ValueError("Source Unity version differs between backends")
    for ident in ("library", "metadata"):
        if pa[ident].get("sha256") != pb[ident].get("sha256"):
            raise ValueError("Source IL2CPP binaries differ between backends")
    if pa.get("backend") != "AssetStudio" or pb.get("backend") != "AssetRipper":
        raise ValueError("Must compare independent AssetStudio and AssetRipper")
    counts = collections.Counter()
    examples = collections.Counter()
    for key in sorted(a):
        lhs, rhs = a[key], b[key]
        for stable in ("kind", "className", "assembly", "gameObjectId",
                       "rectTransformId", "scriptPointer"):
            if lhs.get(stable) != rhs.get(stable):
                raise ValueError("Original source identities differ: " + stable)
        ls, rs = lhs.get("status") == SUCCEEDED, rhs.get("status") == SUCCEEDED
        if ls: counts["studioVerifiedComponents"] += 1
        if rs: counts["ripperVerifiedComponents"] += 1
        if ls and rs:
            counts["independentlyMatchedComponents"] += 1
            if lhs.get("fields") != rhs.get("fields"):
                # Never print source field content or publish source component IDs.
                examples[lhs.get("className", "UNKNOWN")] += 1
            else:
                counts["independentlyMatchedFieldValues"] += len(lhs["fields"])
        elif ls and not rs:
            counts["studioOnlyComponents"] += 1
            counts["studioOnlyFieldValues"] += len(lhs["fields"])
        elif rs and not ls:
            counts["ripperOnlyComponents"] += 1
    if examples:
        counts["conflictingComponents"] = sum(examples.values())
        raise ValueError("Independent generated TypeTrees disagree for " +
                         str(counts["conflictingComponents"]) + " components: " +
                         repr(dict(examples)))
    if counts["independentlyMatchedComponents"] < 1:
        raise ValueError("No cross-backend component comparison succeeded")
    if counts["ripperOnlyComponents"]:
        raise ValueError("AssetRipper includes components rejected by AssetStudio")
    return dict(counts)


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("assetstudio", type=Path)
    parser.add_argument("assetripper", type=Path)
    args = parser.parse_args()
    left = json.loads(args.assetstudio.read_text(encoding="utf-8"))
    right = json.loads(args.assetripper.read_text(encoding="utf-8"))
    print(json.dumps(compare(left, right), ensure_ascii=False, sort_keys=True))


if __name__ == "__main__":
    main()
