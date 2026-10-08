#!/usr/bin/env python3
"""Inspect an XAPK and nested APK/OBB archives without publishing game assets."""
import argparse
import collections
import csv
import json
import shutil
import sys
import tempfile
import zipfile
from pathlib import Path, PurePosixPath

CANDIDATE_EXT = (".png", ".jpg", ".jpeg", ".webp", ".gif", ".svg",
                 ".xml", ".json", ".plist", ".atlas", ".fnt", ".ttf",
                 ".otf", ".skel", ".anim", ".prefab", ".asset", ".assets",
                 ".bundle", ".assetbundle", ".unity3d", ".bytes", ".ress")
MAX_EXPORT = 1024 * 1024 * 1024
MAX_SINGLE = 256 * 1024 * 1024

def safe_path(name):
    if name.startswith("/") or "\\" in name or "\x00" in name:
        return None
    parts = PurePosixPath(name).parts
    if not parts or any(p in (".", "..") for p in parts) or ":" in parts[0]:
        return None
    return Path(*parts)

def inspect(path, label, rows, paths, extensions, output, exported):
    try:
        archive = zipfile.ZipFile(path)
    except (zipfile.BadZipFile, OSError) as exc:
        print("WARNING: cannot scan", label, str(exc), file=sys.stderr)
        return 0
    n = 0
    with archive:
        for entry in archive.infolist():
            if entry.is_dir():
                continue
            n += 1
            name = entry.filename
            paths.add(name.lower())
            extensions[Path(name).suffix.lower() or "(none)"] += 1
            if not name.lower().endswith(CANDIDATE_EXT):
                continue
            category = ("image" if name.lower().endswith((".png", ".jpg", ".jpeg", ".webp", ".gif", ".svg"))
                        else "resource_candidate")
            rows.append((label, name, category, entry.file_size))
            if output is None or entry.file_size > MAX_SINGLE:
                continue
            rel = safe_path(name)
            if rel is None or exported[0] + entry.file_size > MAX_EXPORT:
                continue
            target = output / label / rel
            try:
                target.parent.mkdir(parents=True, exist_ok=True)
                with archive.open(entry) as src, target.open("wb") as dst:
                    shutil.copyfileobj(src, dst)
                exported[0] += entry.file_size
            except (OSError, RuntimeError, zipfile.BadZipFile) as exc:
                target.unlink(missing_ok=True)
                print("WARNING: failed to export", name, str(exc), file=sys.stderr)
    return n

def main():
    p = argparse.ArgumentParser()
    p.add_argument("xapk", type=Path)
    p.add_argument("--report-dir", type=Path, default=Path("reports/xapk"))
    p.add_argument("--apk-dir", type=Path, default=Path("output/apks"))
    p.add_argument("--extract-assets", type=Path, default=None)
    args = p.parse_args()
    with args.xapk.open("rb") as src:
        if src.read(48).startswith(b"version https://git-lfs.github.com/"):
            sys.exit("ERROR: Git LFS pointer, not XAPK bytes; run git lfs pull")
    if not zipfile.is_zipfile(args.xapk):
        sys.exit("ERROR: invalid XAPK (not a ZIP archive)")
    args.report_dir.mkdir(parents=True, exist_ok=True)
    args.apk_dir.mkdir(parents=True, exist_ok=True)
    rows, paths, extensions, summary = [], set(), collections.Counter(), []
    exported = [0]
    if args.extract_assets:
        args.extract_assets.mkdir(parents=True, exist_ok=True)
    with tempfile.TemporaryDirectory(prefix="xapk_") as temp:
        n = inspect(args.xapk, "xapk", rows, paths, extensions, args.extract_assets, exported)
        summary.append(("xapk", n))
        with zipfile.ZipFile(args.xapk) as outer:
            nested = [e for e in outer.infolist()
                      if not e.is_dir() and e.filename.lower().endswith((".apk", ".obb"))]
            for i, entry in enumerate(nested):
                rel = safe_path(entry.filename)
                if rel is None:
                    continue
                label = "%03d_%s" % (i, rel.name)
                target = Path(temp) / label
                with outer.open(entry) as src, target.open("wb") as dst:
                    shutil.copyfileobj(src, dst)
                if rel.name.lower().endswith(".apk"):
                    shutil.copy2(target, args.apk_dir / label)
                count = inspect(target, label, rows, paths, extensions,
                                args.extract_assets, exported)
                summary.append((label, count))
    all_names = "\n".join(paths)
    engines = []
    if any("libunity.so" in s or "libil2cpp.so" in s or "assets/bin/data/" in s for s in paths):
        engines.append("Unity")
    if "libcocos2dcpp.so" in all_names or any(s.endswith(".jsc") for s in paths):
        engines.append("Cocos")
    if "libflutter.so" in all_names or "assets/flutter_assets/" in all_names:
        engines.append("Flutter")
    if "libue4.so" in all_names or any(s.endswith(".pak") for s in paths):
        engines.append("Unreal")
    manifest = {
        "file": args.xapk.name,
        "bytes": args.xapk.stat().st_size,
        "engines_suspected": engines or ["undetermined"],
        "archives": [{"name": name, "entries": n} for name, n in summary],
        "ui_resource_candidates": len(rows),
        "extracted_bytes": exported[0],
        "file_extensions": dict(extensions.most_common()),
        "note": "Candidate files are not necessarily UI. Encrypted and remote resources may be absent."
    }
    (args.report_dir / "manifest.json").write_text(
        json.dumps(manifest, indent=2, ensure_ascii=False) + "\n", encoding="utf-8")
    with (args.report_dir / "ui-candidates.csv").open("w", encoding="utf-8", newline="") as f:
        writer = csv.writer(f)
        writer.writerow(["archive", "path", "category", "uncompressed_bytes"])
        writer.writerows(rows)
    md = [
        "# XAPK UI inventory", "",
        "File: " + args.xapk.name,
        "Size (bytes): " + str(args.xapk.stat().st_size),
        "Suspected engine(s): " + ", ".join(manifest["engines_suspected"]),
        "UI/engine resource candidates: " + str(len(rows)), "",
        "## Archives",
        "| Archive | Entries |", "|---|---:|"
    ]
    md.extend("| " + name + " | " + str(count) + " |" for name, count in summary)
    md.extend(["", "Paths and sizes are in ui-candidates.csv.",
               "This report does not establish that every game screen has been recovered.",
               "Assets loaded from servers and opaque engine bundles need additional analysis.",
               "No game assets or decompiled source are committed to GitHub.", ""])
    (args.report_dir / "REPORT.md").write_text("\n".join(md), encoding="utf-8")
    print(json.dumps({"status": "PASS", "candidates": len(rows),
                      "engines": manifest["engines_suspected"]}, ensure_ascii=False))

if __name__ == "__main__":
    main()
