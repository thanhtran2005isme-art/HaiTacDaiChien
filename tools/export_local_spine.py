#!/usr/bin/env python3
"""Build offline, locally-private Spine 3.8 packs from the actual Unity XAPK.

Only verified skeleton JSON and exact atlas-page Texture2D names are exported.
No game files leave output/local-spine/. No network requests or runtime bundling.
Use only when authorized to access the artwork and animations.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import io
import json
import re
import shutil
import tempfile
import zipfile
from pathlib import Path

from probe_spine_payloads import classify_payload, payload_bytes

ROOT = Path(__file__).resolve().parents[1]
PAGE_NAME = re.compile(r"^[A-Za-z0-9][A-Za-z0-9_.-]{0,119}\.(?:png|webp)$", re.I)
MAX_PAGE = 32 * 1024 * 1024
MAX_TOTAL = 512 * 1024 * 1024
MAX_PIXELS = 30_000_000


def atlas_pages(contents):
    """Extract atlas page headers, not attachment names; refuse paths."""
    text = contents.decode("utf-8-sig")
    seen, result = set(), []
    for line in text.splitlines():
        entry = line.strip()
        if PAGE_NAME.fullmatch(entry) and entry not in seen:
            seen.add(entry)
            result.append(entry)
    return result


def texture_candidates(page, textures):
    """Exactly one valid candidate, exact name before stem fallback."""
    exact = textures.get(page, [])
    if len(exact) == 1:
        return exact[0]
    if exact:
        return None
    basename = Path(page).stem
    fallback = textures.get(basename, [])
    return fallback[0] if len(fallback) == 1 else None


def choose_packages(skeletons, atlases, names=None, limit=12):
    matching = set(skeletons) & set(atlases)
    if names:
        matching &= set(names)
    # Characters first rather than UI particles, without inventing skin selection.
    return sorted(matching, key=lambda name: (not name.startswith("Hero_"), name.lower()))[:limit]


def make_package_name(name):
    return hashlib.sha256(name.encode("utf-8")).hexdigest()[:24]


def collect(env):
    skeletons, atlases, textures = (collections.defaultdict(list) for _ in range(3))
    errors = collections.Counter()
    for obj in env.objects:
        kind = getattr(getattr(obj, "type", None), "name", "")
        if kind not in ("TextAsset", "Texture2D"):
            continue
        try:
            content = obj.read()
            name = str(getattr(content, "m_Name", "") or "")
            if kind == "Texture2D":
                if name:
                    textures[name].append(obj)
                continue
            raw = payload_bytes(content)
            if raw is None or not name:
                continue
            category = classify_payload(name, raw)
            if category == "spine_json_verified":
                skeletons[name].append(raw)
            if category == "atlas_text_candidate" and name.endswith(".atlas"):
                atlases[name[:-6]].append(raw)
        except Exception:
            errors["object_read_failures"] += 1
    return skeletons, atlases, textures, errors


def export_one(name, skeletons, atlases, textures, root, counter):
    if len(skeletons.get(name, [])) != 1 or len(atlases.get(name, [])) != 1:
        return None, "duplicate_skeleton_or_atlas"
    raw_json, raw_atlas = skeletons[name][0], atlases[name][0]
    try:
        data = json.loads(raw_json.decode("utf-8-sig"))
        version = str(data.get("skeleton", {}).get("spine", ""))
        animations = data.get("animations", {})
        pages = atlas_pages(raw_atlas)
    except (ValueError, UnicodeDecodeError, AttributeError):
        return None, "invalid_spine_or_atlas"
    if not version.startswith("3.8.") or not isinstance(animations, dict) or not animations:
        return None, "unsupported_version_or_no_animation"
    if not pages or len(pages) > 12:
        return None, "missing_or_excess_atlas_pages"
    selected = {page: texture_candidates(page, textures) for page in pages}
    if any(reader is None for reader in selected.values()):
        return None, "texture_missing_or_ambiguous"
    token = make_package_name(name)
    folder = root / token
    folder.mkdir(parents=True, exist_ok=True)
    written = []
    try:
        for page, reader in selected.items():
            texture = reader.read()
            image = texture.image
            if image is None or image.width * image.height > MAX_PIXELS:
                raise ValueError("texture_pixels_missing_or_too_large")
            stream = io.BytesIO()
            image.save(stream, format="PNG")
            payload = stream.getvalue()
            if not payload or len(payload) > MAX_PAGE or counter[0] + len(payload) > MAX_TOTAL:
                raise ValueError("image_exceeds_size_budget")
            # If an atlas uses WebP page names, convert to matching WebP bytes.
            if page.lower().endswith(".webp"):
                stream = io.BytesIO()
                image.save(stream, format="WEBP", lossless=True)
                payload = stream.getvalue()
            (folder / page).write_bytes(payload)
            written.append(token + "/" + page)
            counter[0] += len(payload)
        if counter[0] + len(raw_json) + len(raw_atlas) > MAX_TOTAL:
            raise ValueError("pack_exceeds_size_budget")
        (folder / "skeleton.json").write_bytes(raw_json)
        (folder / "skeleton.atlas").write_bytes(raw_atlas)
        counter[0] += len(raw_json) + len(raw_atlas)
        files = [token + "/skeleton.json", token + "/skeleton.atlas", *written]
        return {
            "id": token,
            "name": name,
            "version": version,
            "animations": sorted(animations),
            "skeleton": token + "/skeleton.json",
            "atlas": token + "/skeleton.atlas",
            "pages": written,
            "files": files,
        }, None
    except (OSError, ValueError) as exc:
        shutil.rmtree(folder, ignore_errors=True)
        return None, str(exc)


def extract(root, output, xapk, names=None, limit=12, unitypy=None):
    if unitypy is None:
        import UnityPy as unitypy
    if not xapk or not xapk.is_file() or not zipfile.is_zipfile(xapk):
        raise ValueError("No real XAPK found; run git lfs pull")
    output.mkdir(parents=True, exist_ok=True)
    packages, reasons = [], collections.Counter()
    count, exported = [0], set()
    with tempfile.TemporaryDirectory(prefix="spine_local_") as tmp:
        temp = Path(tmp)
        with zipfile.ZipFile(xapk) as outer:
            for ix, entry in enumerate(outer.infolist()):
                if entry.is_dir() or not entry.filename.lower().endswith(".apk"):
                    continue
                apk = temp / f"{ix}.apk"
                with outer.open(entry) as source, apk.open("wb") as destination:
                    shutil.copyfileobj(source, destination)
                with zipfile.ZipFile(apk) as nested:
                    for j, bundle in enumerate(nested.infolist()):
                        if bundle.is_dir() or not bundle.filename.lower().endswith(
                            (".unity3d", ".bundle", ".assetbundle")
                        ):
                            continue
                        path = temp / f"{ix}-{j}.unity3d"
                        with nested.open(bundle) as src, path.open("wb") as dst:
                            shutil.copyfileobj(src, dst)
                        try:
                            env = unitypy.load(str(path))
                            skeletons, atlases, textures, errors = collect(env)
                            reasons.update(errors)
                            for name in choose_packages(skeletons, atlases, names, limit):
                                if name in exported or len(packages) >= limit:
                                    continue
                                pack, error = export_one(
                                    name, skeletons, atlases, textures, output, count
                                )
                                if error:
                                    reasons[error] += 1
                                else:
                                    packages.append(pack)
                                    exported.add(name)
                        finally:
                            path.unlink(missing_ok=True)
                apk.unlink(missing_ok=True)
    # No stale files are served. The allowlist is rebuilt atomically.
    manifest = {
        "version": 1, "packages": packages,
        "files": [file for pack in packages for file in pack["files"]],
        "stats": {"package_count": len(packages), "total_exported_bytes": count[0],
                  "skipped": dict(reasons)},
        "note": "Spine 3.8 source packs only. Scene skin/animation selection remains unverified.",
    }
    target = output / "manifest.json"
    if not packages:
        target.unlink(missing_ok=True)
        return {"status": "NO_COMPLETE_PACK", **manifest["stats"]}
    temp_manifest = output / "manifest.tmp"
    temp_manifest.write_text(json.dumps(manifest, ensure_ascii=False, indent=2) + "\n",
                             encoding="utf-8")
    temp_manifest.replace(target)
    return {"status": "PASS", **manifest["stats"], "names": [p["name"] for p in packages]}


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--xapk", type=Path)
    parser.add_argument("--output", type=Path, default=ROOT / "output/local-spine")
    parser.add_argument("--name", action="append", help="Exact skeleton name (repeatable)")
    parser.add_argument("--limit", type=int, default=12)
    args = parser.parse_args()
    if not 1 <= args.limit <= 100:
        parser.error("--limit must be 1..100")
    xapk = args.xapk or next(iter(ROOT.glob("*.xapk")), None)
    try:
        result = extract(ROOT, args.output, xapk, args.name, args.limit)
    except (OSError, ValueError, zipfile.BadZipFile) as exc:
        parser.exit(1, "BLOCKED: " + str(exc) + "\n")
    print(json.dumps(result, ensure_ascii=False, indent=2))
    if result["status"] != "PASS":
        raise SystemExit(1)


if __name__ == "__main__":
    main()
