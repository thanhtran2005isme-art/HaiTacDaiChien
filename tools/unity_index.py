#!/usr/bin/env python3
"""Inventory Unity object types and optional image export from APK asset bundles.

Use asset export only with the applicable rights. Results are estimates, not an
editable reconstruction of the original Unity project.
"""
import argparse
import collections
import csv
import json
import re
import tempfile
import zipfile
from pathlib import Path

import UnityPy

INTERESTING = {"Sprite", "Texture2D", "GameObject", "Canvas", "RectTransform",
               "Button", "Image", "Text", "MonoBehaviour", "AssetBundle"}
IMAGES = {"Sprite", "Texture2D"}
MAX_TOTAL_EXPORT = 1024 * 1024 * 1024

def clean(name):
    name = re.sub(r"[^A-Za-z0-9._-]+", "_", str(name))[:90].strip("._")
    return name or "unnamed"

def scan_bundle(path, label, records, totals, samples, export_dir, used):
    try:
        env = UnityPy.load(str(path))
    except Exception as err:
        samples.append({"bundle": label, "error": "Unable to open bundle: " + str(err)[:300]})
        return
    # container maps Unity path names to ObjectReaders where available.
    by_id = {}
    try:
        for item_path, reader in env.container.items():
            by_id[int(reader.path_id)] = str(item_path)
    except Exception:
        pass
    for obj in env.objects:
        kind = getattr(getattr(obj, "type", None), "name", "Unknown")
        totals[kind] += 1
        if kind not in INTERESTING and obj.path_id not in by_id:
            continue
        name = ""
        error = ""
        data = None
        if kind in INTERESTING:
            try:
                data = obj.read()
                name = str(getattr(data, "m_Name", "") or "")
            except Exception as exc:
                error = str(exc)[:150]
        container = by_id.get(int(obj.path_id), "")
        records.append((label, kind, str(obj.path_id), name, container, error))
        if kind not in IMAGES or export_dir is None or data is None:
            continue
        try:
            image = data.image
            if image is None:
                continue
            target = export_dir / label / kind / (
                clean(name or container or kind) + "_" + str(obj.path_id) + ".png")
            target.parent.mkdir(parents=True, exist_ok=True)
            image.save(target)
            used[0] += target.stat().st_size
            if used[0] > MAX_TOTAL_EXPORT:
                raise RuntimeError("image export exceeded 1 GiB cap")
        except Exception as exc:
            samples.append({"bundle": label, "object_id": str(obj.path_id),
                            "image_error": str(exc)[:150]})
            if used[0] > MAX_TOTAL_EXPORT:
                break

def main():
    p = argparse.ArgumentParser()
    p.add_argument("--apk-dir", type=Path, default=Path("output/apks"))
    p.add_argument("--report-dir", type=Path, default=Path("reports/xapk"))
    p.add_argument("--export-images", type=Path, default=None)
    a = p.parse_args()
    a.report_dir.mkdir(parents=True, exist_ok=True)
    records, samples = [], []
    totals = collections.Counter()
    used, bundles = [0], []
    with tempfile.TemporaryDirectory(prefix="unity_index_") as tmp:
        for apk in sorted(a.apk_dir.glob("*.apk")):
            try:
                archive = zipfile.ZipFile(apk)
            except (OSError, zipfile.BadZipFile) as exc:
                samples.append({"apk": apk.name, "error": str(exc)})
                continue
            with archive:
                for i, member in enumerate(archive.infolist()):
                    if member.is_dir() or not member.filename.lower().endswith(
                            (".unity3d", ".bundle", ".assetbundle")):
                        continue
                    label = clean(apk.stem + "_" + Path(member.filename).stem)
                    bundles.append({"name": label, "compressed_file": member.filename,
                                    "bytes": member.file_size})
                    # Avoid constructing an enormous in-memory copy of the Unity bundle.
                    target = Path(tmp) / (str(i) + ".unity3d")
                    with archive.open(member) as reader, target.open("wb") as writer:
                        import shutil
                        shutil.copyfileobj(reader, writer)
                    scan_bundle(target, label, records, totals, samples, a.export_images, used)
                    target.unlink(missing_ok=True)

    with (a.report_dir / "unity-objects.csv").open(
            "w", encoding="utf-8", newline="") as fp:
        writer = csv.writer(fp)
        writer.writerow(["bundle", "type", "path_id", "name", "container_path", "error"])
        writer.writerows(records)
    result = {"bundles": bundles, "object_type_counts": dict(totals.most_common()),
              "indexed_objects": len(records), "image_export_bytes": used[0],
              "errors": samples[:200],
              "note": "Sprite/Texture2D extraction does not recover Unity screen layout or code."}
    (a.report_dir / "unity-summary.json").write_text(
        json.dumps(result, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    lines = ["# Unity asset analysis", "",
             "Bundled Unity files: " + str(len(bundles)),
             "Indexed interesting objects: " + str(len(records)),
             "Image export bytes: " + str(used[0]), "",
             "## Object types", "", "| Type | Count |", "|---|---:|"]
    lines.extend("| " + kind + " | " + str(count) + " |"
                 for kind, count in totals.most_common())
    lines.extend(["", "See unity-objects.csv for paths and names.",
                  "Screen layout and runtime behavior require further reconstruction.",
                  "Game data not present in the APK may be downloaded at runtime.", ""])
    (a.report_dir / "UNITY_REPORT.md").write_text("\n".join(lines), encoding="utf-8")
    print(json.dumps({"bundles": len(bundles), "objects": len(records),
                      "types": dict(totals.most_common(15)),
                      "errors": len(samples)}, ensure_ascii=False))

if __name__ == "__main__":
    main()
