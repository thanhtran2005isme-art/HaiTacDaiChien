#!/usr/bin/env python3
"""Compare IL2CPP TypeTree generator backends against the *exact* local XAPK.

Evidence-only. This only checks whether native backend can generate a root
schema for source-verified MonoScripts, never parses fields or writes Prefabs.
Run each backend in a separate process to isolate native library state.
"""
from __future__ import annotations

import argparse
import collections
import json
from pathlib import Path

import audit_local_ui_components as ui
import recover_managed_ui_fields as recovery

ROOT = Path(__file__).resolve().parents[1]
BACKENDS = ("AssetsTools", "AssetStudio", "AssetRipper")
ORDER = ("UnityEngine.UI.Image", "UnityEngine.UI.CanvasScaler",
         "UnityEngine.UI.Mask", "UnityEngine.UI.RectMask2D",
         "UnityEngine.UI.HorizontalLayoutGroup",
         "UnityEngine.UI.VerticalLayoutGroup",
         "UnityEngine.UI.GridLayoutGroup",
         "UnityEngine.UI.ContentSizeFitter")


def select_source_classes(scenes, limit=8):
    """Select only classes proven by source MonoScript links; no name inference."""
    counts = collections.Counter()
    for scene in scenes:
        for row in scene["components"]:
            name = row.get("className")
            asm = row.get("assembly")
            if (name in ui.FIELDS and asm and
                    row.get("scriptResolution") in
                    ("LOCAL_PATHID", "EXTERNAL_RESOLVED", "UNITYPY_DEREF",
                     "GLOBAL_XAPK_EXACT_FILE_ALIAS_PATHID")):
                counts[(asm, name)] += 1
    ordered = sorted(counts, key=lambda key: (
        ORDER.index(key[1]) if key[1] in ORDER else len(ORDER),
        -counts[key], key))
    return [(asm, name, counts[(asm, name)]) for asm, name in ordered[:limit]]


def probe(root, backend, xapk=None, factory=None):
    if backend not in BACKENDS:
        raise ValueError("Unknown TypeTree backend")
    data = json.loads((root / "output/deep-ui-source-evidence.json")
                      .read_text(encoding="utf-8"))
    if len(data["scenes"]) != 5:
        raise recovery.RecoveryBlocked("Source scenes not complete")
    ver = recovery.exact_unity_version(data["scenes"])
    xapk = xapk or next(iter(sorted(root.glob("*.xapk"))), None)
    if xapk is None:
        raise recovery.RecoveryBlocked("Canonical XAPK missing")
    if factory is None:
        from UnityPy.helpers.TypeTreeGenerator import TypeTreeGenerator
        factory = lambda ver: TypeTreeGenerator(ver, generator=backend)
    gen, proof = recovery.source_generator(xapk, ver, factory=factory)
    result = {"backend": backend, "unityVersion": ver,
              "sourceLibrarySha256": proof["library"]["sha256"],
              "sourceMetadataSha256": proof["metadata"]["sha256"],
              "sourceOnly": True, "parsesSerializedObjects": False,
              "results": []}
    try:
        loaded = gen.get_loaded_dll_names()
        if isinstance(loaded, list):
            result["loadedAssemblyCount"] = len(loaded)
            result["uiAssemblyLoaded"] = any(
                str(name).lower().removesuffix(".dll") == "unityengine.ui"
                for name in loaded)
    except Exception as exc:
        result["loadedAssemblyStatus"] = type(exc).__name__
    for assembly, cls, count in select_source_classes(data["scenes"]):
        row = {"className": cls, "sourceComponents": count}
        try:
            node = gen.get_nodes_up(assembly, cls)
            shape = recovery._tree_summary(node)
            row["root"] = shape
            row["status"] = ("HEADER_PRESENT" if
                             shape["rootLevel"] == 0 and
                             set(shape["headerNodes"]) ==
                             {"m_GameObject", "m_Script", "m_Enabled"}
                             else "INCOMPLETE_ROOT")
        except Exception as exc:
            row["status"] = "GENERATOR_" + type(exc).__name__
            row["frame"] = recovery._safe_exception_frame(exc)
        result["results"].append(row)
    return result


def main():
    ap = argparse.ArgumentParser(description=__doc__)
    ap.add_argument("--backend", choices=BACKENDS, required=True)
    ap.add_argument("--root", type=Path, default=ROOT)
    ap.add_argument("--xapk", type=Path)
    args = ap.parse_args()
    try:
        result = probe(args.root.resolve(), args.backend, args.xapk)
    except (OSError, ValueError, ImportError) as exc:
        result = {"backend": args.backend, "status": "BLOCKED",
                  "reason": type(exc).__name__, "noPrefabChanges": True}
    print(json.dumps(result, ensure_ascii=False, sort_keys=True))


if __name__ == "__main__":
    main()
