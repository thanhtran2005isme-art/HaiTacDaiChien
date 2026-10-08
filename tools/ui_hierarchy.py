#!/usr/bin/env python3
"""Map serialized Unity UI hierarchies to CSV. Metadata only; no game assets exported."""
import argparse
import collections
import csv
import json
import shutil
import tempfile
import zipfile
from pathlib import Path

def field(obj, key, default=None):
    return obj.get(key, default) if isinstance(obj, dict) else getattr(obj, key, default)

def pointer(obj):
    try:
        return int(field(obj, "m_FileID", field(obj, "file_id", 0)) or 0), int(
            field(obj, "m_PathID", field(obj, "path_id", 0)) or 0)
    except (TypeError, ValueError):
        return 0, 0

def local_pointer(obj):
    file_id, path_id = pointer(obj)
    return path_id if file_id == 0 else 0

def component_pointer(obj):
    return local_pointer(field(obj, "component", field(obj, "m_Component", obj)))

def xy(obj):
    if obj is None:
        return ""
    try:
        return f"{float(field(obj, 'x')):.3f},{float(field(obj, 'y')):.3f}"
    except (TypeError, ValueError):
        return ""

def paths_for(transforms, names):
    paths = {}
    cyclic = 0
    for tid in transforms:
        if tid in paths:
            continue
        trail, seen, cur = [], set(), tid
        while cur in transforms and cur not in paths:
            if cur in seen or len(trail) >= 150:
                cyclic += 1
                break
            seen.add(cur)
            trail.append(cur)
            cur = transforms[cur]["parent"]
        prefix = paths.get(cur, "[unresolved]" if cur or cyclic and cur in seen else "")
        for node in reversed(trail):
            name = names.get(transforms[node]["go"], "Unnamed")
            prefix = (prefix + "/" + str(name).replace("/", "_").replace("\n", "_"))[:2000]
            paths[node] = prefix
    return paths, cyclic

def subtree_counts(transforms):
    children = collections.defaultdict(list)
    for tid, item in transforms.items():
        if item["parent"] in transforms and item["parent"] != tid:
            children[item["parent"]].append(tid)
    memo = {}
    for root in transforms:
        if root in memo:
            continue
        stack, visiting = [(root, False)], set()
        while stack:
            tid, post = stack.pop()
            if tid in memo:
                continue
            if post:
                visiting.discard(tid)
                memo[tid] = 1 + sum(memo.get(c, 0) for c in children[tid])
                continue
            if tid in visiting:
                continue
            visiting.add(tid)
            stack.append((tid, True))
            stack.extend((c, False) for c in children[tid] if c not in memo and c not in visiting)
    return memo

def safe_read(reader, errors, mono=False):
    try:
        return reader.read()
    except Exception:
        if mono:
            try:
                return reader.parse_monobehaviour_head()
            except Exception as exc:
                errors["MonoBehaviour_head_" + type(exc).__name__] += 1
                return None
        errors[reader.type.name + "_read_error"] += 1
        return None

def inspect_file(objects, label, errors):
    readers = {int(reader.path_id): reader for reader in objects}
    kinds = {pid: reader.type.name for pid, reader in readers.items()}
    gameobjects, transforms, canvases, sprites, scripts = {}, {}, [], {}, {}
    for pid, reader in readers.items():
        kind = kinds[pid]
        if kind not in ("GameObject", "RectTransform", "Canvas", "Sprite", "MonoScript"):
            continue
        obj = safe_read(reader, errors)
        if obj is None:
            continue
        if kind == "GameObject":
            gameobjects[pid] = {
                "name": str(field(obj, "m_Name", "") or ""),
                "active": field(obj, "m_IsActive", ""),
                "components": [component_pointer(x) for x in (field(obj, "m_Component", []) or [])
                               if component_pointer(x)]}
        elif kind == "RectTransform":
            transforms[pid] = {
                "go": local_pointer(field(obj, "m_GameObject")),
                "parent": local_pointer(field(obj, "m_Father")),
                "anchor_min": xy(field(obj, "m_AnchorMin")),
                "anchor_max": xy(field(obj, "m_AnchorMax")),
                "pivot": xy(field(obj, "m_Pivot")),
                "size_delta": xy(field(obj, "m_SizeDelta")),
                "anchored_position": xy(field(obj, "m_AnchoredPosition")),
                "children": [local_pointer(p) for p in (field(obj, "m_Children", []) or [])],
                "scale": xy(field(obj, "m_LocalScale")),
                "rotation_z": float(field(field(obj, "m_LocalRotation"), "z", 0) or 0)}
        elif kind == "Canvas":
            canvases.append({"bundle": label, "canvas_id": pid,
                             "gameobject_id": local_pointer(field(obj, "m_GameObject")),
                             "sort_order": field(obj, "m_SortingOrder", 0)})
        elif kind == "Sprite":
            sprites[pid] = str(field(obj, "m_Name", "") or "")
        elif kind == "MonoScript":
            scripts[pid] = str(field(obj, "m_ClassName", "") or field(obj, "m_Name", "") or "")
    sibling_indices = {child: index for parent in transforms.values()
                       for index, child in enumerate(parent["children"]) if child}
    names = {gid: go["name"] for gid, go in gameobjects.items()}
    nodepaths, cyclic = paths_for(transforms, names)
    sizes = subtree_counts(transforms)
    go_transform = {value["go"]: tid for tid, value in transforms.items() if value["go"]}
    canvas_map = {}
    for canvas in canvases:
        tid = go_transform.get(canvas["gameobject_id"], 0)
        canvas["transform_id"] = tid
        canvas["name"] = names.get(canvas["gameobject_id"], "")
        canvas["path"] = nodepaths.get(tid, "")
        canvas["descendant_ui_nodes"] = sizes.get(tid, 0)
        if tid:
            canvas_map[tid] = canvas

    nodes, roots, components, links = [], [], [], []
    for tid, transform in transforms.items():
        gid = transform["go"]
        go = gameobjects.get(gid, {})
        cursor, visited, canvas = tid, set(), None
        while cursor in transforms and cursor not in visited and len(visited) < 150:
            if cursor in canvas_map:
                canvas = canvas_map[cursor]
                break
            visited.add(cursor)
            cursor = transforms[cursor]["parent"]
        row = {
            "bundle": label, "transform_id": tid, "gameobject_id": gid,
            "parent_transform_id": transform["parent"], "name": go.get("name", ""),
            "path": nodepaths.get(tid, ""), "active": go.get("active", ""),
            "canvas_id": canvas["canvas_id"] if canvas else 0,
            "canvas_name": canvas["name"] if canvas else "",
            "subtree_size": sizes.get(tid, 1),
            "component_count": len(go.get("components", [])),
            "sibling_index": sibling_indices.get(tid, -1),
            "scale": transform["scale"], "rotation_z": transform["rotation_z"]}
        for attr in ("anchor_min", "anchor_max", "pivot", "size_delta", "anchored_position"):
            row[attr] = transform[attr]
        nodes.append(row)
        if not transform["parent"] or transform["parent"] not in transforms:
            roots.append({key: row[key] for key in ("bundle", "transform_id", "name", "path", "subtree_size")})

    scanned = set()
    go_ids = {t["go"] for t in transforms.values() if t["go"]}
    missing_type_tree = 0
    for gid in go_ids:
        go = gameobjects.get(gid)
        if not go:
            continue
        path = nodepaths.get(go_transform.get(gid, 0), "")
        for cid in go["components"]:
            if cid in scanned or kinds.get(cid) != "MonoBehaviour":
                continue
            scanned.add(cid)
            reader = readers[cid]
            instance = safe_read(reader, errors, mono=True)
            if instance is None:
                continue
            script_file, script_id = pointer(field(instance, "m_Script"))
            script_name = scripts.get(script_id, "") if script_file == 0 else ""
            sprite_file, sprite_id = pointer(field(instance, "m_Sprite"))
            if not sprite_id:
                try:
                    tree = reader.read_typetree()
                    sprite_file, sprite_id = pointer(field(tree, "m_Sprite"))
                except Exception:
                    missing_type_tree += 1
            component_row = {
                "bundle": label, "gameobject_id": gid, "component_id": cid,
                "ui_path": path, "script_name": script_name, "script_file_id": script_file,
                "script_id": script_id, "sprite_file_id": sprite_file,
                "sprite_id": sprite_id,
                "sprite_name": sprites.get(sprite_id, "") if sprite_file == 0 else ""}
            components.append(component_row)
            if sprite_id:
                links.append(component_row)
    metrics = {"gameobjects": len(gameobjects), "rect_transforms": len(transforms),
               "canvases": len(canvases), "canvas_ui_linked": len(canvas_map),
               "sprite_objects": len(sprites), "scripts": len(scripts),
               "ui_nodes": len(nodes), "root_candidates": len(roots),
               "ui_mono_components": len(components),
               "sprite_references_readable": len(links),
               "mono_missing_typetree_or_sprite": missing_type_tree,
               "hierarchy_cycle_or_limit": cyclic}
    return {"metrics": metrics, "nodes": nodes, "roots": roots,
            "canvases": canvases, "components": components, "links": links}

def save_csv(path, records, columns):
    with path.open("w", encoding="utf-8", newline="") as stream:
        writer = csv.DictWriter(stream, fieldnames=columns, extrasaction="ignore")
        writer.writeheader()
        writer.writerows(records)

def main():
    import UnityPy
    parser = argparse.ArgumentParser()
    parser.add_argument("--apk-dir", type=Path, default=Path("output/apks"))
    parser.add_argument("--report-dir", type=Path, default=Path("reports/xapk"))
    args = parser.parse_args()
    args.report_dir.mkdir(parents=True, exist_ok=True)
    all_data = {key: [] for key in ("nodes", "roots", "canvases", "components", "links")}
    bundle_stats, errors = [], collections.Counter()
    for apk in sorted(args.apk_dir.glob("*.apk")):
        with zipfile.ZipFile(apk) as archive:
            for i, member in enumerate(archive.infolist()):
                if member.is_dir() or not member.filename.lower().endswith(
                        (".unity3d", ".bundle", ".assetbundle")):
                    continue
                with tempfile.TemporaryDirectory(prefix="unity_ui_") as temp:
                    path = Path(temp) / "bundle.unity3d"
                    with archive.open(member) as source, path.open("wb") as target:
                        shutil.copyfileobj(source, target)
                    label = f"{apk.stem}_{Path(member.filename).stem}"
                    try:
                        env = UnityPy.load(str(path))
                        groups = {}
                        # Unity path IDs are only unique PER SerializedFile, not globally.
                        for reader in env.objects:
                            groups.setdefault(id(reader.assets_file), []).append(reader)
                        for index, readers in enumerate(groups.values()):
                            part_label = f"{label}__file{index:03d}"
                            part = inspect_file(readers, part_label, errors)
                            bundle_stats.append({"file": part_label, **part["metrics"]})
                            for key in all_data:
                                all_data[key].extend(part[key])
                        del env
                    except Exception as exc:
                        errors["bundle_error"] += 1
                        bundle_stats.append({"file": label, "error": str(exc)[:200]})
    save_csv(args.report_dir / "ui-hierarchy.csv", all_data["nodes"],
             ["bundle", "transform_id", "gameobject_id", "parent_transform_id",
              "name", "path", "active", "canvas_id", "canvas_name", "subtree_size",
              "component_count", "anchor_min", "anchor_max", "pivot",
              "size_delta", "anchored_position", "sibling_index", "scale", "rotation_z"])
    save_csv(args.report_dir / "ui-canvases.csv", all_data["canvases"],
             ["bundle", "canvas_id", "gameobject_id", "name", "path",
              "transform_id", "sort_order", "descendant_ui_nodes"])
    save_csv(args.report_dir / "ui-root-candidates.csv", all_data["roots"],
             ["bundle", "transform_id", "name", "path", "subtree_size"])
    component_fields = ["bundle", "gameobject_id", "component_id", "ui_path",
                        "script_name", "script_file_id", "script_id",
                        "sprite_file_id", "sprite_id", "sprite_name"]
    save_csv(args.report_dir / "ui-components.csv", all_data["components"], component_fields)
    save_csv(args.report_dir / "ui-sprite-links.csv", all_data["links"], component_fields)
    totals = collections.Counter()
    for item in bundle_stats:
        totals.update({k: v for k, v in item.items() if isinstance(v, int)})
    summary = {"serialized_files": bundle_stats, "totals": dict(totals),
               "parsing_errors": dict(errors),
               "notes": ["Canvas and hierarchy roots are candidates, not verified game screens.",
                         "Sprite links missing a managed typetree remain unresolved.",
                         "Cross-file Unity PPtr references are not falsely joined.",
                         "Assets loaded remotely will not appear in XAPK analysis.",
                         "No copyrighted images, code or bundles exported to repo."]}
    (args.report_dir / "ui-hierarchy-summary.json").write_text(
        json.dumps(summary, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    canvases = sorted(all_data["canvases"], key=lambda d: d["descendant_ui_nodes"], reverse=True)
    roots = sorted(all_data["roots"], key=lambda d: d["subtree_size"], reverse=True)
    lines = ["# Unity UI hierarchy inventory", "",
             "This is structural metadata from Unity bundles, **not** a restored editable project.",
             "", "## Coverage", "", "| Item | Count |", "|---|---:|"]
    lines += [f"| {k} | {v:,} |" for k, v in totals.items()]
    lines += ["", "## Largest Canvas subtrees", "", "| Canvas | Serialized file | Nodes |",
              "|---|---|---:|"]
    lines += [f"| {c['name'].replace('|', '/')} | {c['bundle']} | {c['descendant_ui_nodes']:,} |"
              for c in canvases[:40]]
    lines += ["", "## Largest potential UI roots", "", "| Root | Serialized file | Nodes |",
              "|---|---|---:|"]
    lines += [f"| {r['name'].replace('|', '/')} | {r['bundle']} | {r['subtree_size']:,} |"
              for r in roots[:40]]
    lines += ["", "## Files",
              "- ui-hierarchy.csv: UI tree paths and RectTransform coordinates",
              "- ui-canvases.csv: Canvas metadata and subtree counts",
              "- ui-root-candidates.csv: potential hierarchy roots",
              "- ui-components.csv: UI MonoBehaviour and script references",
              "- ui-sprite-links.csv: references where sprite fields are readable",
              "- ui-hierarchy-summary.json: parse coverage and unresolved limitations", "",
              "No game UI images or proprietary binary assets were published.", ""]
    (args.report_dir / "UI_HIERARCHY_REPORT.md").write_text(
        "\n".join(lines), encoding="utf-8")
    print(json.dumps({"status": "PASS", "totals": dict(totals),
                      "errors": dict(errors)}, ensure_ascii=False))

if __name__ == "__main__":
    main()
