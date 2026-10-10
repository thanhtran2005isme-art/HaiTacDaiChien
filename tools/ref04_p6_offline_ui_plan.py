#!/usr/bin/env python3
"""P6 OFFLINE REF04 UI restoration audit for a discontinued online game.

The original Android game/server DOES NOT need to run. All actual UI evidence
comes from gitignored P5/XAPK-derived reports, and the development target is a
new Unity client with an independently owned backend. No UI positions, runtime
formulas or server behavior are inferred from source-only proof.
"""
from __future__ import annotations

import argparse
from collections import Counter
import hashlib
import json
from pathlib import Path
import re

ROOT = Path(__file__).resolve().parents[1]
KIND = "REF04_P6_OFFLINE_XAPK_TO_UNITY_SOURCE_GAP_AUDIT"
P5_KIND = "REF04_P5_CROSS_PHASE_SOURCE_PROVENANCE_NO_RUNTIME_COORDINATES"
INV_KIND = "REF04_ALL_SOURCE_CANDIDATE_UI_COMPONENT_INVENTORY_READ_ONLY"
GEO_KIND = "REF04_EXACT_SOURCE_IMAGE_SPRITE_GEOMETRY"
SCENE = "REF04-home-crew"
HEX = re.compile(r"[a-f0-9]{64}\Z")
SPRITE = re.compile(r"[a-f0-9]{32}\.png\Z")
NAMES = {
    "p5": "ref04-p5-cross-phase-source-integrity.json",
    "inventory": "ref04-full-source-inventory.json",
    "geometry": "ref04-static-image-geometry.json",
}
MAX_REPORT_BYTES = 80 * 1024 * 1024


def ensure(test, message):
    if not test:
        raise ValueError("P6 OFFLINE BLOCKED: " + message)


def digest(data):
    return hashlib.sha256(data).hexdigest()


def build(p5, inventory, geometry, report_sha=None):
    """Create audit-only gap ledger. No Android, server, Unity Editor or XAPK boot."""
    cv = p5.get("sourceCoverage", {})
    co = inventory.get("counts", {})
    ensure(p5.get("classification") == P5_KIND and
           p5.get("sceneId") == SCENE and
           cv.get("totalSourceProvenanceEntries") == 245 and
           cv.get("P1OriginalLayoutGroupFields") == 168 and
           cv.get("P2OriginalCanvasAndRelatedComponents") == 15 and
           cv.get("P4OriginalTextComponents") == 62 and
           cv.get("P2ComponentsWithOriginalRawSha", 0) +
           cv.get("P2IdentityOnlyComponentsWithoutRawSha", 0) == 15 and
           p5.get("runtimeCoordinates") is None and
           p5.get("runtimeLayoutProven") is False and
           p5.get("unityImportAllowed") is False and
           p5.get("originalUiAssetsChanged") is False,
           "P5 exact original source audit unavailable / runtime claims forged")
    ensure(inventory.get("classification") == INV_KIND and
           inventory.get("sceneId") == SCENE and
           inventory.get("sourceFieldApplicationAllowed") is False and
           co.get("gameObjects") == 503 and
           co.get("serializedComponentRecords") == 1564 and
           co.get("allDualVerifiedImageComponents") == 299 and
           co.get("originalSpriteLinkedImages") == 265 and
           co.get("verifiedImagesWithoutSourceSprite") == 34,
           "original source inventory incomplete")
    ensure(geometry.get("classification") == GEO_KIND and
           geometry.get("sceneId") == SCENE and
           geometry.get("sourceBindings") == 265 and
           len(geometry.get("images", [])) == 265,
           "265 verified source Sprite link records missing")
    origin = inventory.get("sourceSerializedFile")
    ensure(bool(origin) and p5.get("originalSourceSerializedFile") == origin,
           "P5 original SerializedFile does not match offline inventory")

    originals = {}
    nodes = inventory.get("gameObjects")
    ensure(isinstance(nodes, list) and len(nodes) == 503,
           "source node count missing")
    for node in nodes:
        gid, tid = node.get("gameObjectPathId"), node.get("rectTransformPathId")
        ensure(type(gid) is int and type(tid) is int,
               "original GameObject/RectTransform id invalid")
        for component in node.get("components", []):
            cid = component.get("componentPathId")
            ensure(type(cid) is int and cid not in originals and
                   component.get("gameObjectPathId") == gid and
                   component.get("rectTransformPathId") == tid,
                   "original component owner or ID collision")
            originals[cid] = component
    ensure(len(originals) == 1564, "original component records incomplete")
    seen = set()
    review = []
    counts = Counter()
    for row in geometry["images"]:
        cid = row.get("componentPathId")
        component = originals.get(cid)
        ensure(type(cid) is int and cid not in seen and component is not None and
               row.get("gameObjectPathId") == component.get("gameObjectPathId") and
               row.get("rectTransformPathId") == component.get("rectTransformPathId") and
               row.get("sourceObjectSha256") == component.get("rawSourceObjectSha256") and
               isinstance(row.get("sourceObjectSha256"), str) and
               HEX.fullmatch(row["sourceObjectSha256"]) and
               component.get("monoScriptClass") == "UnityEngine.UI.Image" and
               component.get("verificationStatus") ==
                   "TWO_BACKEND_SOURCE_VERIFIED_FIELDS",
               "Sprite source identity/object SHA/dual-backend Image mismatch")
        sprite = row.get("spriteFile")
        ensure(isinstance(sprite, str) and SPRITE.fullmatch(sprite),
               "source Sprite file is missing or a noncanonical filename")
        verified_geometry = row.get("applyGeometry") is True
        ensure(type(row.get("applyGeometry")) is bool and
               ((row.get("nativeSpriteGeometryStatus") ==
                 "NATIVE_SPRITE_GEOMETRY_VERIFIED") == verified_geometry),
               "Sprite geometry proof status conflicts with import gate")
        if verified_geometry:
            for key in ("sourceRectSize", "pixelsPerUnit", "border"):
                ensure(key in row, "verified Sprite geometry field missing: " + key)
        seen.add(cid)
        counts["spriteLinkedImages"] += 1
        counts["sourceGeometryVerified" if verified_geometry
               else "sourceGeometryBlocked"] += 1
        # Record names and IDs for the user's private UI engineering backlog,
        # not coordinates or values guessed from absent runtime state.
        review.append({
            "originalImageComponentPathId": cid,
            "originalGameObjectPathId": component["gameObjectPathId"],
            "originalRectTransformPathId": component["rectTransformPathId"],
            "originalImageObjectSha256": component["rawSourceObjectSha256"],
            "spriteFile": sprite,
            "spriteGeometrySourceStatus":
                "ORIGINAL_NATIVE_GEOMETRY_VERIFIED" if verified_geometry
                else "BLOCKED_NATIVE_GEOMETRY_UNVERIFIED",
            "geometryMayBeAppliedToSourceOnlyStudy":
                verified_geometry,
            "runtimeCoordinates": None,
        })
    ensure(len(seen) == 265, "source Sprite bindings duplicate")
    counts["verifiedImagesWithoutSourceSprite"] = 34
    counts["allDualVerifiedImages"] = 299
    counts["otherNonSpriteSourceComponents"] = 1564 - 265
    counts["layoutSerializedFieldsTwoSchemaVerified"] = 168
    counts["originalTextSourceComponents"] = 62
    counts["P2RawShaBackedComponents"] = cv["P2ComponentsWithOriginalRawSha"]
    counts["P2IdentityOnlyComponents"] = cv["P2IdentityOnlyComponentsWithoutRawSha"]
    counts["P2ExcludedFieldNamesWithoutSha"] = cv.get(
        "P2SourceFieldNamesExcludedWithoutRawSha", 0)

    return {
        "classification": KIND,
        "sceneId": SCENE,
        "originalSourceSerializedFile": origin,
        "sourceReportSha256": report_sha or {},
        "counts": dict(sorted(counts.items())),
        "spriteGeometryAudit": review,
        "sourceOnlyFieldPromotionAllowed": False,
        "mode": "OFFLINE_XAPK_SOURCE_TO_NEW_UNITY_CLIENT",
        "requiresOriginalAndroidGameToRun": False,
        "requiresOriginalGameServer": False,
        "newBackendIsAuthoritativeForNewGameplay": True,
        "originalGameBackendBehaviorVerified": False,
        "uiSourceProvenanceAndNewDesignMustBeLabeledSeparately": True,
        "originalXapkPixelPerfectMatchProven": False,
        "runtimeCanvasScale": None,
        "runtimeSafeAreaFormula": None,
        "runtimeTextPositions": None,
        "runtimeCoordinates": None,
        "originalUiAssetsChanged": False,
        "unityAssetsChanged": False,
        "guidance": [
            "Use original verified Sprite/GameObject/Image identities as the source restoration base.",
            "Keep Canvas/viewport and any unverified placement marked BLOCKED until independently resolved.",
            "Design new UI behaviors and backend contracts explicitly as NEW_PROJECT_DESIGN, never ORIGINAL_XAPK_VERIFIED.",
            "Do not treat preview 1600x900, guessed coordinates, translation or dynamic Text as original.",
            "Original live APK and server are NOT required; historical captures, if available, are optional visual references.",
            "Unity Game View and future independent-backend flows require separate acceptance tests.",
        ],
    }


def execute(root=ROOT):
    root = Path(root).resolve()
    reports = {}
    source_hashes = {}
    for key, name in NAMES.items():
        path = root / "output" / name
        ensure(path.is_file() and not path.is_symlink() and
               path.stat().st_size <= MAX_REPORT_BYTES,
               "missing/unsafe private report: " + key)
        contents = path.read_bytes()
        source_hashes[key] = digest(contents)
        reports[key] = json.loads(contents)
    result = build(reports["p5"], reports["inventory"], reports["geometry"],
                   source_hashes)
    output = root / "output/ref04-p6-offline-ui-gaps.json"
    ensure(not output.is_symlink(), "private P6 output cannot be symlink")
    temp = output.with_suffix(".tmp")
    temp.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n",
                    encoding="utf-8")
    temp.replace(output)
    print(json.dumps({
        "classification": KIND, "mode": result["mode"],
        "counts": result["counts"],
        "requiresOriginalGameServer": False,
        "requiresOriginalAndroidGameToRun": False,
        "runtimeCoordinates": None,
        "originalXapkPixelPerfectMatchProven": False,
        "unityAssetsChanged": False,
    }, ensure_ascii=False, sort_keys=True))
    return result


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=ROOT)
    execute(parser.parse_args().root)
