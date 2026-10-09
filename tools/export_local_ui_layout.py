#!/usr/bin/env python3
"""Extract *verified* Unity UI layout evidence from a local authorized XAPK.

Unlike screenshots or HTML wireframes, this reads serialized RectTransforms,
Canvas settings and (only if typetrees are present) uGUI Image properties.
Never invents CanvasScaler or runtime state. Keeps output under gitignored
output/, does not export art, and never contacts a game server.
"""
from __future__ import annotations

import argparse
import collections
import json
import math
import re
import shutil
import tempfile
import zipfile
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
MAX_BUNDLE = 1024 * 1024 * 1024
MAX_ARCHIVE = 1024 * 1024 * 1024
SAFE_ID = re.compile(r"^REF[0-9A-Za-z-]+$")
IMAGE_FIELDS = {
    "m_Type": "type", "m_PreserveAspect": "preserveAspect",
    "m_FillMethod": "fillMethod", "m_FillAmount": "fillAmount",
    "m_FillOrigin": "fillOrigin", "m_FillClockwise": "fillClockwise",
    "m_RaycastTarget": "raycastTarget",
}


def get(value, field, default=None):
    return value.get(field, default) if isinstance(value, dict) else getattr(value, field, default)


def local_id(value):
    """Only local Unity serialized references, never ambiguous external links."""
    try:
        file_id = int(get(value, "m_FileID", -1))
        path_id = int(get(value, "m_PathID", 0))
        return path_id if file_id == 0 and path_id > 0 else 0
    except (ValueError, TypeError):
        return 0


def real(value):
    try:
        x = float(value)
        return x if math.isfinite(x) and abs(x) <= 1000000 else None
    except (ValueError, TypeError):
        return None


def vector(value, fields):
    arr = [real(get(value, field)) for field in fields]
    return arr if None not in arr else None


def quaternion(value):
    v = vector(value, ("x", "y", "z", "w"))
    if v is None:
        return None
    norm = sum(x * x for x in v)
    return v if 0.5 <= norm <= 1.5 else None


def valid_color(raw):
    value = vector(raw, ("r", "g", "b", "a"))
    return value if value and all(0 <= v <= 4 for v in value) else None


def scene_bundle_prefix(scene):
    return scene["source"].rsplit("__file", 1)[0]


def choose_serialized_file(scene, groups):
    """Require transform-ID coverage, not unsafe file order or Sprite-name guesses."""
    nodes = scene["nodes"]
    wanted = {int(node["id"]) for node in nodes}
    minimum = max(2, math.ceil(len(wanted) * 0.8))
    # ui_hierarchy.py originally assigned __fileNNN by enumerating each
    # SerializedFile *in encounter order* inside a Unity bundle. Path IDs can
    # legitimately repeat in multiple SerializedFiles containing UI prefabs,
    # so a global "best matching IDs" heuristic is unsafe.
    if "__file" not in scene.get("source", ""):
        raise ValueError("Serialized file identity missing from source metadata")
    try:
        index = int(scene["source"].rsplit("__file", 1)[1])
    except (TypeError, ValueError) as exc:
        raise ValueError("Malformed serialized file index") from exc
    parts = list(groups.values())
    if index < 0 or index >= len(parts):
        raise ValueError("Serialized file index out of range for " + scene["id"])
    readers = parts[index]
    root = readers.get(int(scene["rootTransform"]))
    covered = sum(readers.get(pid) is not None and
                  readers[pid].type.name == "RectTransform" for pid in wanted)
    if root is None or root.type.name != "RectTransform" or covered < minimum:
        raise ValueError("Serialized file order or RectTransform coverage changed for " +
                         scene["id"] + " (" + str(covered) + "/" +
                         str(len(wanted)) + ")")
    return readers


def verified_scene(scene, readers, image_rows):
    expected = {int(node["id"]): node for node in scene["nodes"]}
    go_to_node = {}
    nodes, canvases, images, image_bindings = [], [], [], []
    failure = collections.Counter()
    for node_id, source in expected.items():
        reader = readers.get(node_id)
        if reader is None or reader.type.name != "RectTransform":
            raise ValueError("Transform missing after file selection: " + str(node_id))
        try:
            obj = reader.read()
            gameobject = local_id(get(obj, "m_GameObject"))
            # Do not accept collisions between GameObjects in the same file.
            if not gameobject or gameobject in go_to_node:
                raise ValueError("Unverifiable GameObject for Transform " + str(node_id))
            go_to_node[gameobject] = node_id
            father = local_id(get(obj, "m_Father"))
            if node_id != int(scene["rootTransform"]) and father != int(source["parent"]):
                raise ValueError("Parent pointer disagreement: " + str(node_id))
            rotation = quaternion(get(obj, "m_LocalRotation"))
            local_scale = vector(get(obj, "m_LocalScale"), ("x", "y", "z"))
            local_position = vector(get(obj, "m_LocalPosition"), ("x", "y", "z"))
            evidence = {"nodeId": node_id}
            if rotation is not None:
                evidence["rotation"] = rotation
            if local_scale is not None:
                evidence["localScale"] = local_scale
            if local_position is not None:
                evidence["localPositionZ"] = local_position[2]
                evidence["hasLocalPositionZ"] = True
            if len(evidence) > 1:
                nodes.append(evidence)
        except (ValueError, TypeError, AttributeError) as exc:
            raise ValueError(scene["id"] + ": cannot verify Transform " +
                             str(node_id) + ": " + str(exc)[:100]) from exc
    for reader in readers.values():
        if reader.type.name != "Canvas":
            continue
        try:
            obj = reader.read()
            node_id = go_to_node.get(local_id(get(obj, "m_GameObject")))
            if node_id is None:
                continue
            details = {"nodeId": node_id}
            # Built-in Canvas fields are readable independently of the
            # managed uGUI Image typetree. Preserve only fields actually
            # serialized in this Unity version.
            for source, dest in (("m_SortingOrder", "sortingOrder"),
                                 ("m_OverrideSorting", "overrideSorting"),
                                 ("m_PixelPerfect", "pixelPerfect"),
                                 ("m_Enabled", "enabled"),
                                 ("m_RenderMode", "renderMode"),
                                 ("m_TargetDisplay", "targetDisplay")):
                value = get(obj, source)
                if isinstance(value, (bool, int)):
                    # JsonUtility deserializes C# bool fields from true/false,
                    # not reliably from numeric 0/1 in player-build assets.
                    details[dest] = (bool(value) if dest in (
                        "overrideSorting", "pixelPerfect", "enabled")
                        else int(value))
                    details["has" + dest[0].upper() + dest[1:]] = True
            canvases.append(details)
        except Exception:
            failure["canvas_read_failure"] += 1
    # An Image MonoBehaviour typetree can be stripped from production IL2CPP.
    # Only use its real serialized properties when both the owning GO and path
    # are verified. Never infer them from appearance or sprite file names.
    paths = collections.defaultdict(list)
    for node in scene["nodes"]:
        paths[node["path"]].append(int(node["id"]))
    bindings_by_node = collections.defaultdict(dict)
    for item in image_rows:
        try:
            reader = readers.get(int(item["component_id"]))
            if reader is None or reader.type.name != "MonoBehaviour":
                failure["image_component_not_found"] += 1
                continue
            head = reader.parse_monobehaviour_head()
            owner = local_id(get(head, "m_GameObject"))
            resolved_node = go_to_node.get(owner)
            # Duplicate paths/names are normal in Unity; the original
            # MonoBehaviour.m_GameObject pointer disambiguates safely.
            # Still require that the source path lists this exact node.
            if resolved_node is None or resolved_node not in paths.get(item["ui_path"], []):
                failure["image_gameobject_mismatch"] += 1
                continue
            # The original MonoBehaviour header carries m_Enabled even when
            # IL2CPP strips the managed Image typetree.
            cid = int(item["component_id"])
            enabled = get(head, "m_Enabled")
            binding = {"nodeId": resolved_node, "componentId": cid,
                       "gameObjectId": owner}
            if isinstance(enabled, (int, bool)) and enabled in (0, 1):
                binding.update(hasEnabled=True, enabled=bool(enabled))
            else:
                binding["hasEnabled"] = False
                failure["image_enabled_unavailable"] += 1
            previous = bindings_by_node[resolved_node].get(cid)
            if previous is not None and previous != binding:
                raise ValueError("Conflicting serialized Image header: " + str(cid))
            bindings_by_node[resolved_node][cid] = binding
            tree = reader.read_typetree()
            if not isinstance(tree, dict):
                failure["image_typetree_unavailable"] += 1
                continue
            result = {"nodeId": resolved_node}
            for raw, output in IMAGE_FIELDS.items():
                if raw in tree and isinstance(tree[raw], (bool, float, int)):
                    number = real(tree[raw])
                    if number is not None:
                        result[output] = tree[raw]
                        result["has" + output[0].upper() + output[1:]] = True
            color = valid_color(tree.get("m_Color"))
            if color is not None:
                result["color"] = color
                result["hasColor"] = True
            if len(result) > 1:
                images.append(result)
            else:
                failure["image_properties_unavailable"] += 1
        except Exception:
            failure["image_typetree_unavailable"] += 1
    # A Unity GameObject can show only one uGUI Image in this preview.
    # An ambiguous source component must not be selected by guesswork.
    for node_id, unique in bindings_by_node.items():
        if len(unique) == 1:
            image_bindings.extend(unique.values())
        else:
            failure["ambiguous_image_component_per_node"] += 1
    return {"sceneId": scene["id"], "nodes": nodes, "canvases": canvases,
            "images": images, "imageBindings": sorted(
                image_bindings, key=lambda row: row["nodeId"]),
            "limitations": dict(failure)}


def scan_bundle(data, scenes, links, unitypy):
    env = unitypy.load(data)  # in memory: no Windows temp-file locking
    groups = collections.defaultdict(dict)
    for obj in env.objects:
        groups[id(obj.assets_file)][int(obj.path_id)] = obj
    results = []
    for scene in scenes:
        readers = choose_serialized_file(scene, groups)
        result = verified_scene(scene, readers, links.get(scene["id"], []))
        results.append(result)
    return results


def build(root=ROOT, xapk=None, unitypy=None):
    if unitypy is None:
        import UnityPy as unitypy
    if xapk is None:
        xapk = next(iter(sorted(root.glob("*.xapk"))), None)
    if xapk is None or not xapk.is_file() or not zipfile.is_zipfile(xapk):
        raise ValueError("Full valid local XAPK is required (git lfs pull).")
    scenes = json.loads((root / "unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json")
                        .read_text(encoding="utf-8"))["scenes"]
    if len(scenes) != 5 or any(not SAFE_ID.fullmatch(s["id"]) for s in scenes):
        raise ValueError("Expected exactly five verified candidate scene identifiers")
    links = collections.defaultdict(list)
    import csv
    with (root / "reports/xapk/scene-image-texture-links.csv").open(
            newline="", encoding="utf-8") as stream:
        for row in csv.DictReader(stream):
            links[row["reference"]].append(row)
    pending = collections.defaultdict(list)
    for scene in scenes:
        pending[scene_bundle_prefix(scene)].append(scene)
    result, examined = [], 0
    with tempfile.TemporaryDirectory(prefix="haitac_layout_") as temporary:
        with zipfile.ZipFile(xapk) as outer:
            # Existing Unity inventory labels index *APK files only*, not all
            # outer XAPK members (which also include icons/manifest metadata).
            apks = [member for member in outer.infolist() if
                    not member.is_dir() and member.filename.lower().endswith(".apk")]
            for index, member in enumerate(apks):
                if member.file_size > MAX_ARCHIVE:
                    raise ValueError("Oversized APK encountered")
                stem = Path(member.filename).stem
                apk_path = Path(temporary) / (str(index) + ".apk")
                with outer.open(member) as source, apk_path.open("wb") as destination:
                    shutil.copyfileobj(source, destination)
                try:
                    with zipfile.ZipFile(apk_path) as apk:
                        for asset in apk.infolist():
                            if asset.is_dir() or not asset.filename.lower().endswith(
                                (".unity3d", ".bundle", ".assetbundle")
                            ):
                                continue
                            label = f"{index:03}_{stem}_{Path(asset.filename).stem}"
                            if label not in pending:
                                continue
                            if asset.file_size > MAX_BUNDLE:
                                raise ValueError("Oversized Unity bundle: " + label)
                            with apk.open(asset) as stream:
                                blob = stream.read()
                            examined += 1
                            result.extend(scan_bundle(blob, pending.pop(label), links, unitypy))
                            del blob
                finally:
                    apk_path.unlink(missing_ok=True)
    if pending or len(result) != 5:
        raise ValueError("Missing scene asset bundle(s): " + ", ".join(sorted(pending)))
    stats = {"scenes": len(result), "bundlesRead": examined,
             "verifiedTransforms": sum(len(s["nodes"]) for s in result),
             "verifiedCanvases": sum(len(s["canvases"]) for s in result),
             "imageTypetrees": sum(len(s["images"]) for s in result),
             "exactImageBindings": sum(len(s["imageBindings"]) for s in result),
             "disabledSourceImages": sum(1 for s in result for row in
                  s["imageBindings"] if row.get("hasEnabled") and not row["enabled"]),
             "unknownImageEnabled": sum(1 for s in result for row in
                  s["imageBindings"] if not row.get("hasEnabled")),
             "canvasWithEnabledEvidence": sum(1 for s in result for c in
                  s["canvases"] if c.get("hasEnabled")),
             "disabledCanvases": sum(1 for s in result for c in
                  s["canvases"] if c.get("hasEnabled") and not c["enabled"]),
             "canvasWithRenderModeEvidence": sum(1 for s in result for c in
                  s["canvases"] if c.get("hasRenderMode")),
             "imageBindingsByScene": {
                 s["sceneId"]: len(s["imageBindings"]) for s in result
             },
             "disabledImagesByScene": {
                 s["sceneId"]: sum(1 for row in s["imageBindings"] if
                     row.get("hasEnabled") and not row["enabled"]) for s in result
             },
             "unavailable": dict(sum((collections.Counter(s["limitations"])
                                      for s in result), collections.Counter()))}
    return {"version": 2, "source": "XAPK serialized Unity assets (local only)",
            "scenes": sorted(result, key=lambda s: s["sceneId"]),
            "stats": stats,
            "policy": "Only verified serialized values; no inferred screen size, layout rules, "
                      "runtime states, animation, or fallback CSS."}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=ROOT)
    parser.add_argument("--xapk", type=Path)
    parser.add_argument("--output", type=Path)
    args = parser.parse_args()
    root = args.root.resolve()
    try:
        data = build(root, args.xapk)
        target = args.output or root / "output/local-ui-layout.json"
        target.parent.mkdir(parents=True, exist_ok=True)
        temporary = target.with_suffix(".tmp")
        temporary.write_text(json.dumps(data, ensure_ascii=False, indent=2) + "\n",
                             encoding="utf-8")
        temporary.replace(target)
        print(json.dumps({"status": "PASS", **data["stats"],
                          "output": str(target)}, ensure_ascii=False))
    except (OSError, ValueError, TypeError, KeyError, zipfile.BadZipFile) as exc:
        parser.exit(1, "BLOCKED: " + str(exc)[:400] + "\n")


if __name__ == "__main__":
    main()
