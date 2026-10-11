#!/usr/bin/env python3
"""One-shot preparation of REF04 native Tight Sprite UI preview from local XAPK.

Runs only the source component re-audit affected by the new m_RD fields, the
REF04 metadata join and source-offset preview export. Existing source PNGs,
XAPK, original 3C Prefabs and Unity Scenes are never edited.
"""
from __future__ import annotations

import json
import subprocess
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
STEPS = (
    "audit_local_ui_components.py",
    "audit_ref04_static_ui_geometry.py",
    "build_ref04_native_bounds_sprites.py",
)


def prepare(root=ROOT, runner=subprocess.run):
    if not (root / "output/local-ui-art/manifest.json").is_file():
        raise FileNotFoundError(
            "Exact source art missing; run export_local_ui_art.py first.")
    if not (root / "output/verified-ui-prefab-plan.json").is_file():
        raise FileNotFoundError(
            "3C source field plan missing; finish original 3C phase first.")
    if not (root / "output/verified-visual-preview-plan.json").is_file():
        raise FileNotFoundError(
            "3D exact Image pointer plan missing; finish original source plan first.")
    for index, step in enumerate(STEPS,1):
        print(f"[REF04 SOURCE] {index}/{len(STEPS)}: {step}",flush=True)
        runner([sys.executable,"-u",str(root/"tools"/step)],
               cwd=str(root),check=True)
    doc=json.loads((root/"output/ref04-source-logical-sprite-previews/manifest.json")
                   .read_text(encoding="utf-8"))
    if (doc.get("classification") !=
            "REF04_SOURCE_TIGHT_MESH_LOGICAL_BOUNDS_PREVIEW" or
        doc.get("originalImageBindings")!=265 or
        doc.get("spriteFiles")!=88 or
        doc["counts"].get("UNPACKED_TIGHT_SOURCE_LOGICAL_BOUNDS_PREVIEW")!=22 or
        doc["counts"].get("ALREADY_RECT_COMPATIBLE_NO_CHANGES")!=66):
        raise RuntimeError("Source REF04 native bounds inventory changed: no UI import")
    print(json.dumps({
        "status":"REF04_NATIVE_BOUNDS_SOURCE_PREVIEW_READY",
        "originalImageBindings":265,
        "sourceProvenTightSpritePreviewFiles":22,
        "unmodifiedOriginalSpriteFiles":66,
        "originalSourcePngsModified":0,
        "UnityPrefabsOrScenesModified":0,
        "runtimeVisualFidelityProven":False,
    },sort_keys=True),flush=True)
    return doc


if __name__=="__main__":
    prepare()
