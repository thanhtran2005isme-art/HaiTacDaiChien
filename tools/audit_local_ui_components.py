#!/usr/bin/env python3
"""Exact Unity Sprite borders and typed UI component availability audit.

A diagnostic and optional importer input; never uses screenshots, guesses
Image types, or exposes copyrighted artwork. Non-readable IL2CPP MonoBehaviour
fields are explicitly reported as unavailable.
"""
from __future__ import annotations
import argparse
import collections
import csv
import json
import math
from pathlib import Path

import export_local_ui_layout as layout

ROOT = layout.ROOT
CLASSES = {
    "UnityEngine.UI.CanvasScaler", "UnityEngine.UI.Image",
    "UnityEngine.UI.Mask", "UnityEngine.UI.RectMask2D",
    "UnityEngine.UI.HorizontalLayoutGroup", "UnityEngine.UI.VerticalLayoutGroup",
    "UnityEngine.UI.GridLayoutGroup", "UnityEngine.UI.ContentSizeFitter",
    "UnityEngine.UI.AspectRatioFitter", "UnityEngine.CanvasGroup",
}
FIELDS = {
    "UnityEngine.UI.CanvasScaler": [
        "m_UiScaleMode", "m_ReferenceResolution", "m_ScreenMatchMode",
        "m_MatchWidthOrHeight", "m_ReferencePixelsPerUnit", "m_ScaleFactor",
    ],
    "UnityEngine.UI.Mask": ["m_ShowMaskGraphic"],
    "UnityEngine.UI.RectMask2D": ["m_Padding", "m_Softness"],
    "UnityEngine.UI.HorizontalLayoutGroup": [
        "m_Padding", "m_Spacing", "m_ChildAlignment",
        "m_ChildControlWidth", "m_ChildControlHeight",
        "m_ChildForceExpandWidth", "m_ChildForceExpandHeight",
    ],
    "UnityEngine.UI.VerticalLayoutGroup": [
        "m_Padding", "m_Spacing", "m_ChildAlignment",
        "m_ChildControlWidth", "m_ChildControlHeight",
        "m_ChildForceExpandWidth", "m_ChildForceExpandHeight",
    ],
    "UnityEngine.UI.GridLayoutGroup": [
        "m_Padding", "m_CellSize", "m_Spacing",
        "m_StartCorner", "m_StartAxis", "m_ChildAlignment",
        "m_Constraint", "m_ConstraintCount",
    ],
    "UnityEngine.UI.ContentSizeFitter": ["m_HorizontalFit", "m_VerticalFit"],
    "UnityEngine.UI.AspectRatioFitter": ["m_AspectMode", "m_AspectRatio"],
    "UnityEngine.CanvasGroup": [
        "m_Alpha", "m_Interactable", "m_BlocksRaycasts",
        "m_IgnoreParentGroups",
    ],
    "UnityEngine.UI.Image": [
        "m_Type", "m_Color", "m_PreserveAspect", "m_FillMethod",
        "m_FillAmount", "m_FillOrigin", "m_FillClockwise",
    ],
}


def csv_rows(path):
    with path.open(encoding="utf-8", newline="") as fd:
        return list(csv.DictReader(fd))


def plain(value):
    """Only small finite JSON scalars/vectors; never dump arbitrary typetrees."""
    if isinstance(value, bool):
        return value
    if isinstance(value, int) and abs(value) < 1000000:
        return value
    if isinstance(value, float) and math.isfinite(value) and abs(value) < 1000000:
        return value
    if isinstance(value, dict):
        out = {key: plain(item) for key, item in value.items()
               if key in ("x", "y", "z", "w", "left", "right", "top", "bottom",
                          "r", "g", "b", "a", "m_Left", "m_Right", "m_Top", "m_Bottom")}
        return out if out and all(v is not None for v in out.values()) else None
    return None


def vector4(raw):
    v = layout.vector(raw, ("x", "y", "z", "w"))
    return v if v is not None and all(0 <= x < 32768 for x in v) else None


def sprite_details(reader):
    try:
        obj = reader.read()
        border = vector4(layout.get(obj, "m_Border"))
        pixels = layout.real(layout.get(obj, "m_PixelsToUnits"))
        rect = layout.get(obj, "m_Rect")
        size = layout.vector(layout.get(rect, "size"), ("x", "y"))
        if size is None:
            size = [layout.real(layout.get(rect, "width")),
                    layout.real(layout.get(rect, "height"))]
            if None in size:
                size = None
        # Unity m_Border order = left, bottom, right, top.
        result = {}
        if border is not None and size and (
            border[0] + border[2] <= size[0] and
            border[1] + border[3] <= size[1]
        ):
            result["border"] = border
        if pixels is not None and 0.001 <= pixels <= 10000:
            result["pixelsPerUnit"] = pixels
        if size and all(0 < x <= 16384 for x in size):
            result["sourceRectSize"] = size
        # Sprite texture geometry is separate from m_Rect (source logical
        # dimensions). Copy ONLY serialized numbers. Do not synthesize an
        # offset from missing PNG pixels or assume atlas trim direction.
        origin = layout.vector(rect, ("x", "y"))
        if origin is not None:
            result["sourceRectOrigin"] = origin
        for name, raw in (
            ("sourceSpriteOffset", layout.get(obj, "m_Offset")),
            ("sourceSpritePivot", layout.get(obj, "m_Pivot")),
            ("sourceTextureRectOffset", layout.get(
                layout.get(obj, "m_RD"), "textureRectOffset")),
            ("sourceAtlasRectOffset", layout.get(
                layout.get(obj, "m_RD"), "atlasRectOffset")),
        ):
            values = layout.vector(raw, ("x", "y"))
            if values is not None:
                result[name] = values
        rendering = layout.get(obj, "m_RD")
        texture_rect = layout.get(rendering, "textureRect")
        size = layout.vector(layout.get(texture_rect, "size"), ("x", "y"))
        if size is None:
            size = [layout.real(layout.get(texture_rect, "width")),
                    layout.real(layout.get(texture_rect, "height"))]
        if size and all(x is not None and 0 < x <= 16384 for x in size):
            result["sourceTextureRectSize"] = size
        origin = layout.vector(texture_rect, ("x", "y"))
        if origin is not None:
            result["sourceTextureRectOrigin"] = origin
        packing = layout.get(rendering, "settingsRaw")
        try:
            if type(packing) is int and 0 <= packing <= 0xffffffff:
                result["sourceSpriteSettingsRaw"] = packing
        except (ValueError, TypeError):
            pass
        return result
    except Exception:
        return {}


def component_script(reader):
    try:
        head = reader.parse_monobehaviour_head()
        ptr = layout.get(head, "m_Script")
        resolved = ptr.deref() if hasattr(ptr, "deref") else None
        if resolved is None or resolved.type.name != "MonoScript":
            return ""
        script = resolved.read()
        name = str(layout.get(script, "m_ClassName", "") or "")
        namespace = str(layout.get(script, "m_Namespace", "") or "")
        return namespace + "." + name if namespace else name
    except Exception:
        return ""


def audit_scene(scene, groups, sprite_links, canvas_inventory=None,
                exact_sprite_bindings=None):
    readers = layout.choose_serialized_file(scene, groups)
    expected = {node["id"]: node for node in scene["nodes"]}
    paths = collections.defaultdict(list)
    for node in scene["nodes"]:
        paths[node["path"]].append(node["id"])
    references = []
    sprites = []
    counters = collections.Counter()
    if exact_sprite_bindings:
        # Unlike the old path-based CSV join, this uses the original Image
        # Component PathID. It must be proven to belong to this GameObject's
        # RectTransform and refer to the actual source Sprite object.
        matched = set()
        source_prefix = layout.scene_bundle_prefix(scene)
        for binding in exact_sprite_bindings:
            try:
                node_id = int(binding["nodeId"])
                component_id = int(binding["imageComponentId"])
                sprite_id = int(binding["spritePathId"])
                source = str(binding["spriteSerializedFile"])
                if (node_id not in expected or
                    not isinstance(binding.get("spriteFile"), str) or
                    component_id <= 0 or sprite_id <= 0 or
                    not source.startswith(source_prefix + "__file") or
                    (node_id, component_id) in matched):
                    raise ValueError("Invalid source Sprite/component identity")
                matched.add((node_id, component_id))
                selected_id = int(source.rsplit("__file", 1)[1])
                sprite_reader = list(groups.values())[selected_id].get(sprite_id)
                image_reader = readers.get(component_id)
                node_reader = readers.get(node_id)
                if (sprite_reader is None or sprite_reader.type.name != "Sprite" or
                    image_reader is None or image_reader.type.name != "MonoBehaviour" or
                    node_reader is None or node_reader.type.name != "RectTransform"):
                    raise ValueError("Required original Sprite/Image/RectTransform unavailable")
                head = image_reader.parse_monobehaviour_head()
                image_go = layout.local_id(layout.get(head, "m_GameObject"))
                node_go = layout.local_id(
                    layout.get(node_reader.read(), "m_GameObject"))
                if not image_go or image_go != node_go:
                    raise ValueError("Image source GameObject identity differs")
                detail = sprite_details(sprite_reader)
                if not detail:
                    counters["exact_sprite_geometry_unavailable"] += 1
                    continue
                sprites.append({
                    "nodeId": node_id, "imageComponentId": component_id,
                    "spriteId": sprite_id, "sourceFile": source,
                    "spriteFile": binding["spriteFile"], **detail,
                })
                counters["exact_source_image_sprite_geometry"] += 1
            except (KeyError, ValueError, IndexError, TypeError, AttributeError):
                counters["exact_sprite_owner_or_file_unverifiable"] += 1
    else:
        # Backwards-compatible legacy path audit only when an old manifest has
        # NO PPtr-linked image rows. Never mix legacy and exact source links.
        for row in sprite_links:
            match = paths.get(row["ui_path"], [])
            if len(match) != 1:
                counters["ambiguous_sprite_path"] += 1
                continue
            target = row.get("sprite_file", "")
            try:
                bundle = layout.scene_bundle_prefix(scene)
                if target.rsplit("__file", 1)[0] != bundle:
                    counters["external_sprite_serialized_file"] += 1
                    continue
                selected = list(groups.values())[int(target.rsplit("__file", 1)[1])]
                reader = selected[int(row["sprite_id"])]
                if reader.type.name != "Sprite":
                    counters["wrong_sprite_type"] += 1
                    continue
                detail = sprite_details(reader)
                if detail:
                    sprites.append({"nodeId": match[0], "spriteId": int(row["sprite_id"]),
                                    "sourceFile": target, **detail})
                else:
                    counters["sprite_geometry_not_decodable"] += 1
            except (KeyError, ValueError, IndexError, TypeError):
                counters["sprite_object_not_found"] += 1
    ids = {node["id"] for node in scene["nodes"]}
    go_map = {}
    for node in scene["nodes"]:
        rect = readers.get(node["id"])
        if rect is None:
            continue
        try:
            go_id = layout.local_id(layout.get(rect.read(), "m_GameObject"))
            if go_id:
                go_map[go_id] = node["id"]
        except Exception:
            continue
    for reader in readers.values():
        if reader.type.name != "MonoBehaviour":
            continue
        cls = component_script(reader)
        if cls not in CLASSES:
            continue
        try:
            head = reader.parse_monobehaviour_head()
            go_id = layout.local_id(layout.get(head, "m_GameObject"))
            node_id = go_map.get(go_id)
            if node_id not in ids:
                continue
            record = {"nodeId": node_id, "componentId": int(reader.path_id),
                      "className": cls, "status": "fields_unavailable"}
            try:
                tree = reader.read_typetree()
                if isinstance(tree, dict):
                    fields = {name: plain(tree[name]) for name in FIELDS.get(cls, [])
                              if name in tree}
                    fields = {k: v for k, v in fields.items() if v is not None}
                    if fields:
                        record.update(status="serialized_fields_available", fields=fields,
                                      fieldsSummary=json.dumps(fields, ensure_ascii=False,
                                                               sort_keys=True)[:500])
            except Exception:
                pass
            references.append(record)
        except Exception:
            counters["component_inspection_failure"] += 1
    # UI type presence is known even when IL2CPP stripped private serialized
    # fields. Record these source classes without constructing guessed settings.
    recorded = {int(row["componentId"]) for row in references}
    for row in canvas_inventory or []:
        if row.get("serialized_file") != scene["source"]:
            continue
        try:
            component_id = int(row["component_id"])
            if component_id in recorded:
                continue
            matched = paths.get(row["ui_path"], [])
            if len(matched) != 1 or component_id not in readers:
                continue
            if row["class"] != "UnityEngine.UI.CanvasScaler":
                continue
            references.append({"nodeId": matched[0], "componentId": component_id,
                               "className": row["class"], "status": "fields_unavailable"})
            recorded.add(component_id)
        except (ValueError, KeyError):
            continue
    for node in scene["nodes"]:
        for class_name in ("Mask", "ScrollRect", "Slider"):
            if class_name not in (node.get("types") or []):
                continue
            references.append({"nodeId": node["id"], "componentId": 0,
                               "className": "UnityEngine.UI." + class_name,
                               "status": "class_only_fields_unavailable"})
    return {"sceneId": scene["id"], "spriteGeometry": sprites,
            "components": references, "unavailable": dict(counters)}


def build(root=ROOT, xapk=None, unitypy=None):
    if unitypy is None:
        import UnityPy as unitypy
    scenes = json.loads((root / "unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json")
                        .read_text(encoding="utf-8"))["scenes"]
    links = collections.defaultdict(list)
    for row in csv_rows(root / "reports/xapk/scene-image-texture-links.csv"):
        links[row["reference"]].append(row)
    exact = collections.defaultdict(list)
    art_manifest = root / "output/local-ui-art/manifest.json"
    if art_manifest.is_file():
        manifest = json.loads(art_manifest.read_text(encoding="utf-8"))
        for row in manifest.get("nodeBindings", []):
            if "spriteSerializedFile" in row and "spritePathId" in row:
                exact[row["sceneId"]].append(row)
    canvas_inventory = csv_rows(root / "reports/xapk/canvas-spine-components.csv")
    results = []
    class Interceptor:
        def load(self, data):
            env = unitypy.load(data)
            if not results:
                groups = collections.defaultdict(dict)
                for obj in env.objects:
                    groups[id(obj.assets_file)][int(obj.path_id)] = obj
                results.extend(audit_scene(s, groups, links[s["id"]],
                                           canvas_inventory,
                                           exact_sprite_bindings=exact[s["id"]])
                               for s in scenes)
            return env
    layout.build(root, xapk=xapk, unitypy=Interceptor())
    count = collections.Counter()
    for row in results:
        count["spriteGeometry"] += len(row["spriteGeometry"])
        count["spriteBorders"] += sum("border" in r for r in row["spriteGeometry"])
        count["nonzeroSpriteBorders"] += sum(
            "border" in r and any(v > 0 for v in r["border"])
            for r in row["spriteGeometry"])
        count["components"] += len(row["components"])
        count["readableComponentFields"] += sum(
            c["status"] == "serialized_fields_available" for c in row["components"])
    return {"version": 1, "scenes": results, "stats": dict(count),
            "policy": "Only exact Unity scene file + Sprite ID; missing fields remain unknown. "
                      "Not a runtime CanvasScaler, layout or masking reconstruction."}


def main():
    p = argparse.ArgumentParser(description=__doc__)
    p.add_argument("--root", type=Path, default=ROOT)
    p.add_argument("--xapk", type=Path)
    p.add_argument("--output", type=Path)
    args = p.parse_args()
    root = args.root.resolve()
    output = args.output or root / "output/local-ui-components.json"
    data = build(root, args.xapk)
    output.parent.mkdir(parents=True, exist_ok=True)
    tmp = output.with_suffix(".tmp")
    tmp.write_text(json.dumps(data, ensure_ascii=False, indent=2) + "\n",
                   encoding="utf-8")
    tmp.replace(output)
    print(json.dumps({"status": "PASS", **data["stats"],
                      "output": str(output)}, ensure_ascii=False))


if __name__ == "__main__":
    main()
