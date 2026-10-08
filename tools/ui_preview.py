#!/usr/bin/env python3
"""Render metadata-only SVG wireframes, audit unresolved Unity UI Images.

No copyrighted textures are exported. 1600x900 is a hypothetical viewport;
runtime CanvasScaler, clipping, rotation and layout groups are unsupported.
"""
import argparse
import collections
import csv
import html
import json
import math
import re
from pathlib import Path

W, H = 1600, 900
TARGETS = ["PanelHeroInfo", "PopupShop", "PopupDailyQuest", "PopupFormationTest",
           "PopupPlayerInfo3", "PopupTowerLevelInfo", "PopupEventMonopoly",
           "PopupGuildWarPlayerInfo", "PanelEasterPrayEvent"]

def rows(path):
    with path.open("r", encoding="utf-8", newline="") as f:
        return list(csv.DictReader(f))

def v2(text, default=(0.0, 0.0)):
    try:
        a, b = str(text).split(",", 1)
        return float(a), float(b)
    except (TypeError, ValueError):
        return default

def geometry(parent, row):
    x, y, width, height = parent
    am = v2(row.get("anchor_min"), (0.5, 0.5))
    ax = v2(row.get("anchor_max"), (0.5, 0.5))
    p = v2(row.get("pivot"), (0.5, 0.5))
    d = v2(row.get("size_delta"))
    pos = v2(row.get("anchored_position"))
    w = width * (ax[0]-am[0]) + d[0]
    h = height * (ax[1]-am[1]) + d[1]
    anchor_x = x + width*(am[0] + (ax[0]-am[0])*p[0]) + pos[0]
    anchor_y = y + height*(am[1] + (ax[1]-am[1])*p[1]) + pos[1]
    if not all(math.isfinite(k) for k in (w, h, anchor_x, anchor_y)):
        return None
    return anchor_x - w*p[0], anchor_y - h*p[1], w, h

def hierarchy_indices(folder):
    grouped = collections.defaultdict(dict)
    paths = {}
    for row in rows(folder / "ui-hierarchy.csv"):
        try:
            grouped[row["bundle"]][int(row["transform_id"])] = row
        except (KeyError, ValueError):
            pass
    for row in rows(folder / "ui-components.csv"):
        try:
            paths[(row["bundle"], int(row["component_id"]))] = (row.get("ui_path", ""), int(row.get("gameobject_id", 0) or 0))
        except (KeyError, ValueError):
            pass
    kinds = {}
    for row in rows(folder / "resolved-ui-components.csv"):
        try:
            kinds[(row["file"], int(row["path_id"]))] = row
        except (KeyError, ValueError):
            pass
    links = {}
    for row in rows(folder / "ui-image-sprite-links.csv"):
        try:
            links[(row["bundle"], int(row["component_id"]))] = row
        except (KeyError, ValueError):
            pass
    return grouped, paths, kinds, links

def missing_images(grouped, paths, kinds, links):
    active = {(bundle, node["path"]): node.get("active", "")
              for bundle, group in grouped.items() for node in group.values()}
    result = []
    for key, component in kinds.items():
        if component.get("ui_type") != "Image" or key in links:
            continue
        bundle, cid = key
        path, gid = paths.get(key, ("", 0))
        root = path.lstrip("/").split("/")[0] if path else ""
        result.append({"bundle": bundle, "component_id": cid, "ui_path": path,
                       "root": root, "gameobject": component.get("gameobject", ""),
                       "active": active.get((bundle, path), "unknown"), "gameobject_id": gid,
                       "finding": "no_static_sprite_pptr_found"})
    return result

def children(nodes):
    mapping = collections.defaultdict(list)
    for tid, item in nodes.items():
        try:
            parent = int(item["parent_transform_id"])
        except (ValueError, KeyError):
            continue
        if parent in nodes and parent != tid:
            mapping[parent].append(tid)
    for kids in mapping.values():
        kids.sort(key=lambda tid: (int(nodes[tid].get("sibling_index") or -1), tid))
    return mapping

def descendants(root, by_parent, max_nodes=2400):
    stack = [root]
    done = set()
    result = []
    while stack and len(result) < max_nodes:
        current = stack.pop()
        if current in done:
            continue
        done.add(current)
        result.append(current)
        stack.extend(reversed(by_parent.get(current, [])))
    return result

def choose_roots(folder, grouped):
    roots = []
    for r in rows(folder / "ui-root-candidates.csv"):
        try:
            key = r["bundle"], int(r["transform_id"]), r["name"], int(r["subtree_size"])
        except (ValueError, KeyError):
            continue
        if key[1] in grouped.get(key[0], {}):
            roots.append(key)
    selected = []
    for name in TARGETS:
        candidates = [r for r in roots if r[2] == name]
        if candidates:
            selected.append(max(candidates, key=lambda r: r[3]))
    for hint in ("000_com.mobi389.murom_data__file004",
                 "002_UnityDataAssetPack_datapack__file001"):
        candidates = [r for r in roots if r[0] == hint and r[2] == "Canvas"]
        if candidates:
            selected.append(max(candidates, key=lambda r: r[3]))
    extra = sorted((r for r in roots if r[2] == "Canvas" and r not in selected),
                   key=lambda r: -r[3])
    selected.extend(extra[:2])
    return selected

def draw_svg(root, group, kinds, links, missing, output):
    bundle, tid, root_name, _ = root
    by_parent = children(group)
    shapes = []
    bounds = {tid: (0, 0, W, H)}
    stats = collections.Counter()
    for node_id in descendants(tid, by_parent):
        if node_id == tid:
            continue
        node = group[node_id]
        try:
            parent_id = int(node["parent_transform_id"])
        except (ValueError, KeyError):
            continue
        if parent_id not in bounds:
            continue
        box = geometry(bounds[parent_id], node)
        if box is None:
            stats["invalid_rectangles"] += 1
            continue
        if box[2] <= 0 or box[3] <= 0:
            # Zero-sized intermediate UI containers still have children. Their
            # runtime dimensions may be assigned by LayoutGroup or scripts.
            # Propagate the nearest parent viewport, without drawing this node.
            bounds[node_id] = bounds[parent_id]
            stats["zero_sized_parent_fallbacks"] += 1
            continue
        bounds[node_id] = box
        stats["nodes_geometry_estimated"] += 1
        if abs(float(node.get("rotation_z") or 0)) > .0001:
            stats["nonzero_rotations_ignored"] += 1
        scale = v2(node.get("scale"), (1, 1))
        if any(abs(s-1) > .0001 for s in scale):
            stats["nonunit_scales_ignored"] += 1
        path = node["path"]
        gid = int(node.get("gameobject_id") or 0)
        linked = links.get(gid, [])
        unresolved = missing.get(gid, 0)
        tags = kinds.get(gid, set())
        if linked:
            stats["linked_images"] += len(linked)
        if unresolved:
            stats["unresolved_images"] += unresolved
        if "Button" in tags:
            stats["buttons"] += 1
        if "Text" in tags:
            stats["texts"] += 1
        if not (linked or unresolved or "Button" in tags or "Text" in tags):
            continue
        x, y, w, h = box
        if x + w < 0 or x > W or y + h < 0 or y > H:
            stats["offscreen_shapes"] += 1
            continue
        if w < 2 or h < 2:
            continue
        svg_y = H-y-h
        color = "#0d9488" if linked else "#d97706" if unresolved else (
            "#3b82f6" if "Button" in tags else "#a78bfa")
        tooltip = html.escape(path + (" | " + ", ".join(linked[:3]) if linked else ""), quote=True)
        shapes.append(f'<g><title>{tooltip}</title><rect x="{x:.1f}" y="{svg_y:.1f}" '
                      f'width="{w:.1f}" height="{h:.1f}" stroke="{color}" '
                      'stroke-width="1.2" fill="' + color + '" fill-opacity=".12"/>')
        if w >= 110 and h >= 26 and len(shapes) < 450:
            label = html.escape(node.get("name", "")[:35])
            shapes.append(f'<text x="{x+5:.1f}" y="{svg_y+16:.1f}" fill="#e2e8f0" '
                          f'font-size="12" font-family="sans-serif">{label}</text>')
        shapes.append("</g>")
    safe_title = html.escape(root_name + " | " + bundle)
    svg = [f'<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 {W} {H+76}" '
           'role="img" aria-label="Schematic of Unity UI layout">',
           f'<rect width="{W}" height="{H+76}" fill="#0f172a"/>',
           f'<rect width="{W}" height="{H}" fill="#1e293b"/>',
           f'<text x="20" y="{H+27}" font-family="sans-serif" '
           f'font-size="22" fill="#f8fafc">{safe_title}</text>',
           f'<text x="20" y="{H+53}" font-family="sans-serif" font-size="15" '
           'fill="#94a3b8">Metadata wireframe | hypothetical 1600x900 viewport | '
           'not the actual game screenshot</text>']
    svg += shapes
    svg.append("</svg>")
    output.write_text("\n".join(svg), encoding="utf-8")
    return stats

def export_csv(path, items, columns):
    with path.open("w", encoding="utf-8", newline="") as f:
        writer = csv.DictWriter(f, fieldnames=columns)
        writer.writeheader()
        writer.writerows(items)

def main():
    p = argparse.ArgumentParser()
    p.add_argument("--report-dir", type=Path, default=Path("reports/xapk"))
    args = p.parse_args()
    folder = args.report_dir
    grouped, paths, components, links = hierarchy_indices(folder)
    missing = missing_images(grouped, paths, components, links)
    num_images = sum(1 for row in components.values() if row.get("ui_type") == "Image")
    if len(links) + len(missing) != num_images:
        raise RuntimeError("Image linkage total differs from component inventory")
    export_csv(folder / "unresolved-ui-images.csv", missing,
               ["bundle", "component_id", "ui_path", "root", "gameobject", "active", "gameobject_id", "finding"])
    root_dir = folder / "wireframes"
    root_dir.mkdir(parents=True, exist_ok=True)
    typed = collections.defaultdict(lambda: collections.defaultdict(set))
    for key, record in components.items():
        path, gid = paths.get(key, ("", 0))
        if gid:
            typed[key[0]][gid].add(record["ui_type"])
    matched = collections.defaultdict(lambda: collections.defaultdict(list))
    for (bundle, cid), link in links.items():
        path, gid = paths.get((bundle, cid), ("", 0))
        if gid:
            matched[bundle][gid].append(link.get("sprite_name", ""))
    unknown = collections.defaultdict(collections.Counter)
    for row in missing:
        unknown[row["bundle"]][int(row["gameobject_id"])] += 1
    reports = []
    for i, root in enumerate(choose_roots(folder, grouped), start=1):
        bundle, tid, name, count = root
        label = re.sub(r"[^a-zA-Z0-9-]+", "-", name)[:35]
        filename = f"{i:02d}-{label}.svg"
        summary = draw_svg(root, grouped[bundle], typed[bundle], matched[bundle],
                           unknown[bundle], root_dir / filename)
        reports.append({"root": name, "bundle": bundle, "id": tid,
                        "nodes": count, "svg": "wireframes/" + filename,
                        **dict(summary)})
    stat = {"total_images": num_images, "linked_images": len(links),
            "unresolved_images": len(missing), "preview_roots": len(reports),
            "unresolved_root_distribution":
                dict(collections.Counter(r["root"] for r in missing).most_common(30)),
            "unresolved_active":
                dict(collections.Counter(r["active"] for r in missing)),
            "assumptions": [
                "1600x900 viewport arbitrary, not read from CanvasScaler",
                "Uses anchors, pivot, sizeDelta and anchoredPosition, with parent references",
                "Siblings ordered by serialized m_Children where available",
                "Rotations, scaling, Unity layout groups, masks and runtime animation omitted",
                "Nodes with zero-size rects forward the parent frame only for schematic child visibility",
                "Unmatched Sprite pointers not assigned a cause (may be null/dynamic/missing)",
                "No game image pixels, art assets or decompiled client code published"]}
    (folder / "ui-preview-summary.json").write_text(
        json.dumps(stat, indent=2, ensure_ascii=False) + "\n", encoding="utf-8")
    lines = ["# Unity UI wireframe previews", "",
             "SVGs are schematic projections of RectTransform metadata, not original game screenshots.",
             "No proprietary images embedded. Virtual parent canvas: 1600x900.", "",
             f"- Linked Image: {len(links):,}",
             f"- Unresolved Image: {len(missing):,}",
             f"- Schematic previews: {len(reports)}", "",
             "| Root | UI nodes (total) | Linked Image | Missing Image | SVG |",
             "|---|---:|---:|---:|---|"]
    for r in reports:
        lines.append(f"| {r['root']} | {r['nodes']} | {r.get('linked_images',0)} "
                     f"| {r.get('unresolved_images',0)} | [view]({r['svg']}) |")
    lines.extend(["", "See unresolved-ui-images.csv for the missing Image inventory.",
                  "Root/Canvas size, rotations, CanvasScaler, masks, animation and actual display ordering are not reconstructed.", ""])
    (folder / "UI_PREVIEW_REPORT.md").write_text("\n".join(lines), encoding="utf-8")
    print(json.dumps({"status": "PASS", "schematic_previews": len(reports),
                      "unresolved_images": len(missing), "linked": len(links)}))

if __name__ == "__main__":
    main()
