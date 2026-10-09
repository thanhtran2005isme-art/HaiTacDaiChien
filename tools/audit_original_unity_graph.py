#!/usr/bin/env python3
"""Inspect ORIGINAL serialized Unity GameObject/Prefab/Scene evidence from local XAPK.

The five REF IDs are candidate subtree locations, NOT .prefab/.unity originals.
No simulated Canvas, screenshots, art, binary blobs or runtime state are exported.
All output stays in gitignored output/, including game-object names.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
import os
import tempfile
from pathlib import Path

import export_local_ui_layout as layout

ROOT = layout.ROOT
UI_DB = "unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json"
TYPES_EVIDENCE = frozenset((
    "Prefab", "PrefabInstance", "SceneAsset", "AssetBundle",
))
COMPONENT_NATIVE = frozenset((
    "RectTransform", "Transform", "Canvas", "CanvasRenderer",
    "Camera", "SpriteRenderer", "Mask", "RectMask2D", "Animator",
    "Animation", "ParticleSystem", "MeshRenderer", "MeshFilter",
))
SOURCE_PTRS = ("m_CorrespondingSourceObject", "m_PrefabInstance",
               "m_PrefabAsset")
STATUS_FIELDS = ("m_IsActive", "m_Enabled")


def pointer(value):
    if value is None:
        return {"fileId": 0, "pathId": 0}
    try:
        file_id = int(layout.get(value, "m_FileID",
                                 layout.get(value, "file_id", 0)) or 0)
        path_id = int(layout.get(value, "m_PathID",
                                 layout.get(value, "path_id", 0)) or 0)
    except (TypeError, ValueError):
        return {"fileId": 0, "pathId": 0}
    if path_id < 0 or file_id < 0:
        return {"fileId": 0, "pathId": 0}
    return {"fileId": file_id, "pathId": path_id}


def component_id(entry):
    raw = layout.get(entry, "component",
                     layout.get(entry, "m_Component", entry))
    ref = pointer(raw)
    return ref["pathId"] if ref["fileId"] == 0 else 0


def read_serialized(reader):
    """No fabricated managed fields when IL2CPP MonoBehaviour has no typetree."""
    kind = reader.type.name
    try:
        if kind == "MonoBehaviour":
            return reader.parse_monobehaviour_head(), "head_only"
        return reader.read(), "native"
    except Exception as exc:
        return None, type(exc).__name__


def raw_digest(reader):
    """Only hash and byte length; NEVER export reconstructed C# or game data."""
    try:
        blob = reader.get_raw_data()
        return {"byteLength": len(blob),
                "sha256": hashlib.sha256(blob).hexdigest()}
    except Exception:
        return {"byteLength": None, "sha256": None}


def records_for_scene(scene, readers, source_type_counts):
    by_id = readers
    known = {int(n["id"]): n for n in scene["nodes"]}
    if len(known) != len(scene["nodes"]):
        raise ValueError("Duplicate source RectTransform IDs")
    nodes = []
    gameobjects = set()
    component_ids = set()
    errors = collections.Counter()
    # Store full originals only when existing source transforms can be verified.
    for tid, metadata in known.items():
        transform_reader = by_id.get(tid)
        if transform_reader is None or transform_reader.type.name != "RectTransform":
            raise ValueError(f"{scene['id']}: missing RectTransform {tid}")
        transform, _ = read_serialized(transform_reader)
        if transform is None:
            raise ValueError(f"{scene['id']}: can't read RectTransform {tid}")
        gid = layout.local_id(layout.get(transform, "m_GameObject"))
        father = pointer(layout.get(transform, "m_Father"))
        if not gid or gid in gameobjects:
            raise ValueError(f"{scene['id']}: missing/duplicate GameObject for {tid}")
        if tid != int(scene["rootTransform"]) and (
            father["fileId"] != 0 or father["pathId"] != int(metadata["parent"])
        ):
            raise ValueError(f"{scene['id']}: RectTransform parent mismatch {tid}")
        go_reader = by_id.get(gid)
        if go_reader is None or go_reader.type.name != "GameObject":
            raise ValueError(f"{scene['id']}: no actual GameObject {gid}")
        go, _ = read_serialized(go_reader)
        if go is None:
            raise ValueError(f"{scene['id']}: unreadable GameObject {gid}")
        gameobjects.add(gid)
        linked = []
        for record in (layout.get(go, "m_Component", []) or []):
            cid = component_id(record)
            if cid <= 0:
                errors["external_or_invalid_component_ref"] += 1
                continue
            if cid in linked:
                raise ValueError(f"{scene['id']}: repeated component ID {cid}")
            linked.append(cid)
        if tid not in linked:
            # A RectTransform MUST be listed on its owning original GameObject.
            raise ValueError(f"{scene['id']}: Transform not in GameObject components {tid}")
        child_ptrs = [pointer(x) for x in
                      (layout.get(transform, "m_Children", []) or [])]
        child_local = [x["pathId"] for x in child_ptrs if x["fileId"] == 0]
        if len(child_local) != len(set(child_local)):
            raise ValueError(f"{scene['id']}: repeated source child pointer {tid}")
        anchor = layout.vector(layout.get(transform, "m_AnchorMin"), ("x", "y"))
        anchor_max = layout.vector(layout.get(transform, "m_AnchorMax"), ("x", "y"))
        pivot = layout.vector(layout.get(transform, "m_Pivot"), ("x", "y"))
        delta = layout.vector(layout.get(transform, "m_SizeDelta"), ("x", "y"))
        pos = layout.vector(layout.get(transform, "m_AnchoredPosition"), ("x", "y"))
        scale = layout.vector(layout.get(transform, "m_LocalScale"), ("x", "y", "z"))
        quat = layout.quaternion(layout.get(transform, "m_LocalRotation"))
        child_paths = [x for x in child_local if x in known]
        for cid in child_paths:
            expected = int(known[cid]["parent"])
            if expected != tid:
                raise ValueError(f"{scene['id']}: parent/child pointers disagree {cid}")
        node = {
            "rectTransformId": tid, "gameObjectId": gid,
            "parent": father, "name": str(layout.get(go, "m_Name", "") or "")[:256],
            "active": (int(layout.get(go, "m_IsActive")) if isinstance(
                layout.get(go, "m_IsActive"), (int, bool)) else -1),
            "componentIds": linked, "childTransformIds": child_local,
            "prefabPointers": {key: pointer(layout.get(go, key))
                              for key in SOURCE_PTRS},
            "rect": {"anchorMin": anchor, "anchorMax": anchor_max,
                     "pivot": pivot, "sizeDelta": delta, "anchoredPosition": pos,
                     "localScale": scale, "localRotation": quat}
        }
        nodes.append(node)
        component_ids.update(linked)

    if len(nodes) != len(known):
        raise ValueError("Partial scene node inventory")
    components = []
    for cid in sorted(component_ids):
        reader = by_id.get(cid)
        if reader is None:
            errors["missing_component_object"] += 1
            components.append({"pathId": cid, "kind": "MISSING",
                               "fieldStatus": "missing_serialized_object"})
            continue
        kind = reader.type.name
        record = {"pathId": cid, "kind": kind}
        obj, status = read_serialized(reader)
        record["fieldStatus"] = status
        if obj is not None:
            record["gameObjectPointer"] = pointer(layout.get(obj, "m_GameObject"))
            for name in STATUS_FIELDS:
                value = layout.get(obj, name)
                if isinstance(value, (bool, int)):
                    record[name] = int(value)
            for name in SOURCE_PTRS:
                candidate = pointer(layout.get(obj, name))
                if candidate["fileId"] or candidate["pathId"]:
                    record[name] = candidate
            if kind == "MonoBehaviour":
                # This is a Unity reference to a MonoScript, NOT source code.
                record["monoScriptPointer"] = pointer(layout.get(obj, "m_Script"))
                try:
                    tree = reader.read_typetree()
                    record["fieldStatus"] = ("typetree_available" if isinstance(tree, dict)
                                              else "typetree_not_mapping")
                    # Never copy managed fields wholesale; many include player/game data.
                    record["fieldNames"] = sorted(k for k in tree
                                                  if isinstance(k, str))[:128] if isinstance(
                        tree, dict) else []
                except Exception:
                    record["fieldStatus"] = "managed_fields_unavailable"
                record["rawEvidence"] = raw_digest(reader)
        else:
            errors["component_read_failed"] += 1
        components.append(record)

    tally = collections.Counter(c["kind"] for c in components)
    statuses = collections.Counter(c["fieldStatus"] for c in components)
    source_links = sum(
        any(p["pathId"] != 0 or p["fileId"] != 0
            for p in n["prefabPointers"].values()) for n in nodes
    )
    scene_types = {k: source_type_counts.get(k, 0) for k in sorted(TYPES_EVIDENCE)}
    return {
        "sceneId": scene["id"],
        "sourceSerializedFile": scene["source"],
        "candidateRootTransform": int(scene["rootTransform"]),
        "provenance": {
            "sourceStatus": "serialized_asset_hierarchy_verified",
            "originalEditorPrefabOrScene": "NOT_PROVEN",
            "sourceTypeCounts": scene_types,
            "gameObjectsWithPrefabPointers": source_links,
            "warning": "Compiled Unity AssetBundle GameObjects and components are NOT "
                       "the editable source .prefab/.unity project. Prefab/Scene identity "
                       "requires explicit source-asset evidence; no GameObject guessing."
        },
        "nodes": nodes, "components": components,
        "stats": {
            "nodes": len(nodes), "gameObjects": len(gameobjects),
            "componentReferences": len(component_ids),
            "serializedComponents": sum(c["kind"] != "MISSING" for c in components),
            "componentKinds": dict(sorted(tally.items())),
            "fieldStatus": dict(sorted(statuses.items())),
            "errors": dict(errors),
        }
    }


def extract(root=ROOT, xapk=None, unitypy=None):
    if unitypy is None:
        import UnityPy as unitypy
    source = json.loads((root / UI_DB).read_text(encoding="utf-8"))
    scenes = source["scenes"]
    if len(scenes) != 5:
        raise ValueError("Expected five candidate root hierarchies")
    examined = []
    observed = set()

    class RecordingUnityPy:
        def load(self, blob):
            environment = unitypy.load(blob)
            groups = collections.defaultdict(dict)
            for reader in environment.objects:
                groups[id(reader.assets_file)][int(reader.path_id)] = reader
            for scene in scenes:
                if scene["id"] in observed:
                    continue
                try:
                    by_id = layout.choose_serialized_file(scene, groups)
                except ValueError:
                    continue
                counts = collections.Counter(r.type.name for r in by_id.values())
                examined.append(records_for_scene(scene, by_id, counts))
                observed.add(scene["id"])
            return environment

    # This is the same reader used by the XAPK-tested RectTransform exporter.
    source_result = layout.build(root, xapk=xapk, unitypy=RecordingUnityPy())
    if len(examined) != len(scenes) or len(observed) != len(scenes):
        raise ValueError("Missing original serialized hierarchy in real Unity bundle")
    if sum(s["stats"]["nodes"] for s in examined) != source_result["stats"]["verifiedTransforms"]:
        raise ValueError("Unity source hierarchy count mismatch")
    totals = collections.Counter()
    for item in examined:
        totals["nodes"] += item["stats"]["nodes"]
        totals["components"] += item["stats"]["componentReferences"]
        totals["missingComponents"] += item["stats"]["errors"].get("missing_component_object", 0)
        totals["managedFieldsUnavailable"] += item["stats"]["fieldStatus"].get(
            "managed_fields_unavailable", 0)
    return {
        "version": 1,
        "source": "verified serialized GameObject/Component pointers in local Unity AssetBundle",
        "classification": "SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE",
        "policy": "No fake prefab/scene provenance, no source code/art, "
                  "no guessed CanvasScaler, UI runtime state or animation.",
        "scenes": sorted(examined, key=lambda s: s["sceneId"]),
        "stats": dict(totals),
    }


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=ROOT)
    parser.add_argument("--xapk", type=Path, help="Valid, authorized local XAPK; do not upload.")
    parser.add_argument("--output", type=Path, help="Default output/original-unity-graph.json")
    args = parser.parse_args()
    root = args.root.resolve()
    output = (args.output or root / "output/original-unity-graph.json").resolve()
    # Output must stay inside ignored output/ to avoid leaking game names/metadata.
    private = (root / "output").resolve()
    if not output.is_relative_to(private):
        parser.error("Output must remain under the gitignored output/ folder")
    try:
        result = extract(root=root, xapk=args.xapk)
    except (OSError, ValueError, KeyError, TypeError) as exc:
        parser.exit(1, "BLOCKED: " + str(exc)[:350] + "\n")
    output.parent.mkdir(parents=True, exist_ok=True)
    with tempfile.NamedTemporaryFile(dir=output.parent, prefix=".native_ui_",
                                     suffix=".tmp", mode="w", encoding="utf-8",
                                     delete=False) as writer:
        json.dump(result, writer, ensure_ascii=False, indent=2)
        writer.write("\n")
        temp_name = writer.name
    os.replace(temp_name, output)
    print(json.dumps({"status": "PASS", "file": str(output), **result["stats"],
                      "provenance": result["classification"]}, ensure_ascii=False))


if __name__ == "__main__":
    main()
