#!/usr/bin/env python3
"""Per-component gap manifest from verified local XAPK graph (local-only).

No guessed script names or classes. Component types and IDs are those read
from Unity serialized assets. All output stays in ignored output/.
"""
from __future__ import annotations

import argparse
import collections
import csv
import json
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
COLUMNS = ["sceneId", "serializedFile", "gameObjectPathId", "rectTransformPathId",
           "gameObjectName", "componentPathId", "componentKind", "fieldStatus",
           "monoScriptFileId", "monoScriptPathId", "workStatus"]


def classify(component):
    kind = component.get("kind", "")
    status = component.get("fieldStatus", "")
    if kind == "MISSING":
        return "MISSING_COMPONENT"
    if status in ("managed_fields_unavailable", "typetree_not_mapping", "head_only"):
        return "NEEDS_MANAGED_TYPE_TREE"
    if status not in ("native", "typetree_available"):
        return "NEEDS_SERIALIZED_FIELD_AUDIT"
    if kind == "MonoBehaviour":
        return "NEEDS_EXACT_RUNTIME_FIELD_BINDING"
    return "NATIVE_FIELDS_REQUIRE_UNITY_PARITY_TEST"


def rows_from_graph(graph):
    if graph.get("version") != 1 or len(graph.get("scenes", [])) != 5:
        raise ValueError("Missing exact five-scene graph evidence")
    result = []
    for scene in graph["scenes"]:
        components = {c["pathId"]: c for c in scene["components"]}
        for node in scene["nodes"]:
            for cid in node["componentIds"]:
                component = components.get(cid, {
                    "pathId": cid, "kind": "MISSING",
                    "fieldStatus": "missing_serialized_object"
                })
                ptr = component.get("monoScriptPointer", {})
                result.append({
                    "sceneId": scene["sceneId"],
                    "serializedFile": scene["sourceSerializedFile"],
                    "gameObjectPathId": node["gameObjectId"],
                    "rectTransformPathId": node["rectTransformId"],
                    "gameObjectName": node["name"],
                    "componentPathId": cid,
                    "componentKind": component["kind"],
                    "fieldStatus": component["fieldStatus"],
                    "monoScriptFileId": ptr.get("fileId", 0),
                    "monoScriptPathId": ptr.get("pathId", 0),
                    "workStatus": classify(component),
                })
    expected = graph["stats"]["components"]
    if len(result) != expected or len({
        (x["sceneId"], x["componentPathId"]) for x in result
    }) != expected:
        raise ValueError("GameObject/component PathIDs not uniquely matched")
    return result


def format_report(rows, graph):
    status = collections.Counter(x["workStatus"] for x in rows)
    lines = [
        "# Bao cao du lieu component Unity XAPK con thieu",
        "",
        "Source: output/original-unity-graph.json (LOCAL XAPK). "
        "KHONG PHAI file Editor .prefab/.unity goc.",
        "",
        "| Scene | Components | Managed thieu truong | Native can doi chieu |",
        "|---|---:|---:|---:|",
    ]
    for scene in graph["scenes"]:
        group = [x for x in rows if x["sceneId"] == scene["sceneId"]]
        lines.append(
            f"| {scene['sceneId']} | {len(group)} | "
            f"{sum(x['workStatus']=='NEEDS_MANAGED_TYPE_TREE' for x in group)} | "
            f"{sum(x['workStatus']=='NATIVE_FIELDS_REQUIRE_UNITY_PARITY_TEST' for x in group)} |"
        )
    lines.extend(["", "## Trang thai ban ghi", ""])
    for kind, count in sorted(status.items(), key=lambda item: (-item[1], item[0])):
        lines.append(f"- {kind}: {count}")
    lines += [
        "", "## Bang chi tiet: original-unity-component-gaps.csv", "",
        "Mot hang cho moi scene / gameObjectPathId / rectTransformPathId / "
        "componentPathId; co monoScript file/path ID va tinh trang du lieu.",
        "NEEDS_MANAGED_TYPE_TREE: khong du truong goc tu ban IL2CPP, "
        "khong duoc tu tao Image Type, LayoutGroup, Mask, Spine, Text.",
        "NEEDS_EXACT_RUNTIME_FIELD_BINDING: da doc typetree nhung van "
        "phai kiem tra script, con tro, skin va runtime state.",
        "NATIVE_FIELDS_REQUIRE_UNITY_PARITY_TEST: native type da doc "
        "nhung chua chung minh pixel-perfect Game View/Play Mode.",
        "Moi GameObject co the co nhieu component; tong ban ghi khong phai "
        "so hinh UI hien thi.", "",
    ]
    return "\n".join(lines)


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=ROOT)
    args = parser.parse_args()
    root = args.root.resolve()
    private = root / "output"
    graph = json.loads((private / "original-unity-graph.json").read_text(
        encoding="utf-8"))
    rows = rows_from_graph(graph)
    csv_file = private / "original-unity-component-gaps.csv"
    with csv_file.open("w", newline="", encoding="utf-8") as stream:
        writer = csv.DictWriter(stream, fieldnames=COLUMNS)
        writer.writeheader()
        writer.writerows(rows)
    report = private / "original-unity-gaps.md"
    report.write_text(format_report(rows, graph), encoding="utf-8")
    print(json.dumps({
        "status": "PASS", "rows": len(rows),
        "needsManagedTree": sum(
            x["workStatus"] == "NEEDS_MANAGED_TYPE_TREE" for x in rows),
        "csv": str(csv_file), "report": str(report)
    }, ensure_ascii=False))


if __name__ == "__main__":
    main()
