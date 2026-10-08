#!/usr/bin/env python3
"""Audit CanvasScaler, Spine and unresolved UI.Image serialized pointers.

Static metadata only. Never infer actual viewport size or export game artwork.
"""
import argparse
import collections
import csv
import json
import math
import shutil
import struct
import tempfile
import zipfile
from pathlib import Path
from il2cpp_refs import build_file_registry, get, pptr, resolve_pointer
from sprite_links import endian_of


def load_csv(path):
    with path.open("r", encoding="utf-8", newline="") as f:
        return list(csv.DictReader(f))


def store_csv(path, rows, cols):
    with path.open("w", encoding="utf-8", newline="") as f:
        writer = csv.DictWriter(f, fieldnames=cols, extrasaction="ignore")
        writer.writeheader()
        writer.writerows(rows)


def selected_offset(offsets):
    qualified = [(int(i["byte_offset"]), int(i["single_matches"])) for i in offsets or []
                 if int(i["single_matches"]) >= 2]
    if not qualified:
        return None
    top = max(n for _, n in qualified)
    leaders = [off for off, n in qualified if n == top]
    return leaders[0] if len(leaders) == 1 else None


def classify_pointer(raw, offset, endian, external_count, resolver):
    if offset is None:
        return {"classification":"uncalibrated_offset"}
    if endian not in ("<", ">"):
        return {"classification":"unknown_endianness"}
    if offset < 0 or len(raw) < offset+12:
        return {"classification":"component_too_short"}
    fid, pid = struct.unpack_from(endian+"iq", raw, offset)
    result = {"file_id":fid, "path_id":pid}
    if fid == 0 and pid == 0:
        result["classification"] = "serialized_null_sprite_pointer"
    elif fid < 0 or fid > external_count or pid <= 0:
        result["classification"] = "invalid_pointer_at_calibrated_offset"
    else:
        target, status = resolver(fid, pid)
        result["resolution_status"] = status
        if target is None:
            result["classification"] = "nonzero_unresolved_reference"
        else:
            kind = getattr(getattr(target, "type", None), "name", "Unknown")
            result["target_kind"] = kind
            result["classification"] = ("sprite_pointer_found" if kind=="Sprite"
                                        else "points_to_non_sprite_object")
    return result


def parse_reference_resolution(tree):
    if not isinstance(tree, dict):
        return {}
    value = tree.get("m_ReferenceResolution")
    if not isinstance(value, dict):
        return {}
    try:
        x, y = float(value["x"]), float(value["y"])
        if not all(math.isfinite(n) and 0 < n <= 100000 for n in (x,y)):
            return {}
        return {"reference_width":x,"reference_height":y,
                "ui_scale_mode":tree.get("m_UiScaleMode", ""),
                "screen_match_mode":tree.get("m_ScreenMatchMode", ""),
                "match_width_or_height":tree.get("m_MatchWidthOrHeight", "")}
    except (TypeError, KeyError, ValueError):
        return {}


def audit(apks, report):
    summary = json.loads((report/"ui-image-sprite-summary.json").read_text(encoding="utf-8"))
    offsets = {r["serialized_file"]:selected_offset(r.get("leading_offsets",[]))
               for r in summary.get("offset_calibration",[])}
    missing = load_csv(report/"unresolved-ui-images.csv")
    positions = {}
    for r in load_csv(report/"ui-hierarchy.csv"):
        try:
            positions[(r["bundle"],int(r["gameobject_id"]))] = r["path"]
        except (KeyError,ValueError):
            pass
    canvases = set()
    for r in load_csv(report/"ui-canvases.csv"):
        try:
            canvases.add((r["bundle"],int(r["gameobject_id"])))
        except (KeyError, ValueError):
            pass
    image_details, class_counts, attached, clips, media = [],collections.Counter(),[],[],[]
    with tempfile.TemporaryDirectory(prefix="unity_runtime_audit_") as tmp:
        environments=[]
        for apk in apks:
            with zipfile.ZipFile(apk) as archive:
                for obj in archive.infolist():
                    if obj.is_dir() or not obj.filename.lower().endswith(
                            (".unity3d",".bundle",".assetbundle")):
                        continue
                    target=Path(tmp)/("bundle_"+str(len(environments))+".unity3d")
                    with archive.open(obj) as src, target.open("wb") as dst:
                        shutil.copyfileobj(src,dst)
                    import UnityPy
                    environments.append((apk.stem+"_"+Path(obj.filename).stem,
                                         UnityPy.load(str(target))))
        objects, files, labels, aliases=build_file_registry(environments)
        lookup={(labels[k],int(path_id)):reader for (k,path_id),reader in objects.items()}
        scripts={}
        for (identity,pid), reader in objects.items():
            if reader.type.name!="MonoScript":
                continue
            try:
                item=reader.read()
                name=str(get(item,"m_ClassName","") or "")
                space=str(get(item,"m_Namespace","") or "")
                scripts[(identity,pid)]=(space+"." if space else "")+name
            except Exception:
                continue
        for r in missing:
            key=(r["bundle"],int(r["component_id"]))
            reader=lookup.get(key)
            result=dict(r)
            offset=offsets.get(key[0])
            result["calibrated_offset"]="" if offset is None else offset
            if reader is None:
                result["classification"]="missing_serialized_component"
            else:
                try:
                    def resolver(fid,pid):
                        return resolve_pointer({"m_FileID":fid,"m_PathID":pid},
                                               reader.assets_file,objects,aliases)
                    result.update(classify_pointer(reader.get_raw_data(),offset,
                                  endian_of(reader),len(get(reader.assets_file,"externals",[]) or []),
                                  resolver))
                except Exception as exc:
                    result["classification"]="raw_read_error_"+type(exc).__name__
            image_details.append(result)
        for (identity,pid),reader in objects.items():
            kind=reader.type.name
            label=labels[identity]
            if kind=="AnimationClip":
                try:
                    data=reader.read()
                    clips.append({"serialized_file":label,"clip_id":pid,
                                  "name":str(get(data,"m_Name","") or ""),
                                  "sample_rate":get(data,"m_SampleRate",""),
                                  "legacy":get(data,"m_Legacy","")})
                except Exception:
                    clips.append({"serialized_file":label,"clip_id":pid,
                                  "name":"","sample_rate":"","legacy":""})
                continue
            if kind!="MonoBehaviour":
                continue
            try:
                head=reader.parse_monobehaviour_head()
                script,reason=resolve_pointer(get(head,"m_Script"),
                                               reader.assets_file,objects,aliases)
            except Exception:
                continue
            if script is None or script.type.name!="MonoScript":
                continue
            cls=scripts.get((id(script.assets_file),int(script.path_id)),"")
            is_canvas=cls=="CanvasScaler" or cls.endswith(".CanvasScaler")
            is_spine=cls.startswith("Spine.") or cls.startswith("SpineObject")
            if not (is_canvas or is_spine):
                continue
            if is_spine:
                class_counts[cls]+=1
            fid,gid=pptr(get(head,"m_GameObject"))
            path=positions.get((label,gid),"") if fid==0 else ""
            info={"serialized_file":label,"component_id":pid,"class":cls,
                  "category":"canvas_scaler" if is_canvas else "spine",
                  "gameobject_id":gid if fid==0 else "",
                  "ui_path":path,
                  "same_gameobject_as_canvas":(label,gid) in canvases if fid==0 else False,
                  "typetree_status":"unavailable"}
            if is_canvas:
                try:
                    tree=reader.read_typetree()
                    info["typetree_status"]="available"
                    info.update(parse_reference_resolution(tree))
                except Exception:
                    pass
            attached.append(info)
    if len(image_details)!=len(missing):
        raise AssertionError("Some unresolved Images disappeared")
    counts=collections.Counter(r["classification"] for r in image_details)
    canvas_scalers=[r for r in attached if r["category"]=="canvas_scaler"]
    verified=[r for r in canvas_scalers if "reference_width" in r]
    data={"unresolved_images_audited":len(image_details),
          "unresolved_image_reasons":dict(counts),
          "canvas_scaler_components":len(canvas_scalers),
          "canvas_scaler_linked_to_canvas":sum(bool(r["same_gameobject_as_canvas"])
                                              for r in canvas_scalers),
          "canvas_scaler_readable_reference_resolution":len(verified),
          "spine_component_counts":dict(class_counts.most_common()),
          "spine_component_total":sum(class_counts.values()),
          "animation_clips":len(clips),
          "notes":["CanvasScaler reference resolution is not physical runtime display resolution",
                   "A null Image m_Sprite reference does not prove runtime assignment",
                   "Spine component class counts do not prove animation playback",
                   "All findings derived from static XAPK without executing the game",
                   "No game graphics or proprietary source files exported"]}
    store_csv(report/"image-null-pointer-audit.csv",image_details,
              ["bundle","component_id","ui_path","root","gameobject","active",
               "gameobject_id","calibrated_offset","file_id","path_id",
               "classification","target_kind","resolution_status"])
    store_csv(report/"canvas-spine-components.csv",attached,
              ["serialized_file","component_id","class","category","gameobject_id",
               "ui_path","same_gameobject_as_canvas","typetree_status",
               "reference_width","reference_height","ui_scale_mode",
               "screen_match_mode","match_width_or_height"])
    store_csv(report/"animation-clip-properties.csv",clips,
              ["serialized_file","clip_id","name","sample_rate","legacy"])
    (report/"layout-spine-summary.json").write_text(
        json.dumps(data,ensure_ascii=False,indent=2)+"\n",encoding="utf-8")
    md=["# CanvasScaler / Spine / Image pointer audit","",
        "Static metadata only. Runtime sizes, layout and animation remain unverified.","",
        "## Canvas", "", "| Metric | Count |","|---|---:|",
        "| CanvasScaler components | "+str(data["canvas_scaler_components"])+" |",
        "| Attached to Canvas GameObject | "+str(data["canvas_scaler_linked_to_canvas"])+" |",
        "| Reference resolution read from explicit typetree | "+str(len(verified))+" |",
        "", "## Unlinked Images", "",
        "| Classification | Count |","|---|---:|"]
    for name,n in counts.most_common():
        md.append("| "+name+" | "+str(n)+" |")
    md+=["","A serialized null pointer can reflect an intentionally empty Image or runtime setting.",
         "It does NOT automatically indicate an error or dynamic sprite assignment.","",
         "## Spine components", "", "| Class | Instances |", "|---|---:|"]
    for cls,n in class_counts.most_common():
        md.append("| "+cls+" | "+str(n)+" |")
    md+=["","## Additional outputs","",
         "- image-null-pointer-audit.csv",
         "- canvas-spine-components.csv",
         "- animation-clip-properties.csv",
         "- layout-spine-summary.json",""]
    (report/"LAYOUT_SPINE_AUDIT_REPORT.md").write_text("\n".join(md),encoding="utf-8")
    return data


def main():
    p=argparse.ArgumentParser()
    p.add_argument("--apk-dir",type=Path,default=Path("output/apks"))
    p.add_argument("--report-dir",type=Path,default=Path("reports/xapk"))
    args=p.parse_args()
    apks=sorted(args.apk_dir.glob("*.apk"))
    if not apks:
        p.error("No unpacked APKs")
    data=audit(apks,args.report_dir)
    print(json.dumps({"status":"PASS",**data},ensure_ascii=False))


if __name__=="__main__":
    main()
