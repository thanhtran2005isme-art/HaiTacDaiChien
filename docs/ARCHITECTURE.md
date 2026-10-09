# Kiến trúc hiện có — HaiTacDaiChien

## Phạm vi

Repo nghiên cứu và xem trước **Unity UI metadata offline** dựa trên XAPK. Không phải game server/client gốc hay mã nguồn Unity đã phục hồi đầy đủ.

```text
HaiTacDaiChien/
├── README.md, AGENTS.md            # Điểm vào và quy trình AI
├── docs/                           # Handoff, kiến trúc, quyết định, lịch sử
├── reports/xapk/                   # CSV/JSON/SVG kiểm kê từ Unity asset
├── tools/                          # Python giải mã, đối chiếu, xuất manifest
├── tests/                          # Test Python / JS và hợp đồng trích xuất
├── web-ui-viewer/                  # HTML/CSS/JS: trình xem UI offline
├── unity-ui-viewer/
│   ├── Assets/Editor/             # Menu dựng Canvas và kiểm tra Spine
│   ├── Assets/Scripts/            # C# viewer, component chứng cứ, probe
│   └── Assets/StreamingAssets/    # ui-scenes.json: metadata tĩnh của 5 scene
├── CHUAN_BI_DO_HOA.bat             # Chuẩn bị Sprite/Spine và bằng chứng local
└── CHAY_UI_OFFLINE.bat             # Chạy web viewer localhost
```

`output/`, `unity-ui-viewer/Assets/LocalReconstruction/`, runtime Spine đã cài và các ảnh game được giải mã **nằm local/ignored**, không phải dữ liệu được tracking trên GitHub.

## Dòng dữ liệu

XAPK và Unity assets (nguồn phải được phép sử dụng) → công cụ `tools/` đọc serialized file/pathID → `reports/xapk` và `Assets/StreamingAssets/ui-scenes.json` → hai giao diện:
- **Web UI Viewer**: Python server nội bộ `tools/serve_ui_viewer.py`, trình duyệt xem thông tin và Sprite tùy nguồn hợp lệ.
- **Unity uGUI Viewer**: `UnityCanvasReconstructor.cs` dựng 5 scene/prefab với Canvas/RectTransform/Sprite và `ReconstructionEvidence`; Audit 5 scene.
- **Spine**: `trace_local_spine_links.py` kiểm tra tham chiếu nội dung; `export_local_spine.py` xuất 3.8 JSON+atlas+texture local. `OfflineSpine38Preview.cs` tạo test scene *riêng*, đòi runtime Spine-Unity hợp pháp; `SpinePreviewProbe.cs` kiểm tra track chạy, không chứng minh render bằng hình ảnh.

## Kiểm kê gốc theo SerializedFile

`tools/audit_original_unity_graph.py` đọc trực tiếp AssetBundle, đối chiếu `GameObject.m_Component`, `RectTransform.m_Father/m_Children`, component type/typetree và các dấu vết Prefab/Scene nếu có. Kết quả **local** `output/original-unity-graph.json` không kết luận các GameObject này là `.prefab`/`.unity` nguồn. Unity có menu `Source XAPK → Build evidence-only serialized graph prefabs`, tạo cấu trúc riêng trong `Assets/LocalReconstruction/SourceGraphPrefabs` và `SourceGraphScenes` **không hiển thị UI giả**. Chi tiết xem [XAPK_NATIVE_SCENE_PREFAB_RECOVERY](XAPK_NATIVE_SCENE_PREFAB_RECOVERY.md).

## Môi trường và phép thử

| Môi trường | Mục đích | Bằng chứng |
|---|---|---|
| Python/JavaScript CI | Parsing, mapping, an toàn local, logic kiểm tra | `tests/`, Actions |
| Browser localhost | Kiểm tra web UI | `CHAY_UI_OFFLINE.bat` |
| Unity 2022.3 | Dựng và xem Sprite/Canvas, Inspector, Console | Xác minh thủ công trên Editor |
| Unity 2020.3 (test riêng) | Phạm vi hỗ trợ spine-unity 3.8 | Chỉ sau khi có runtime hợp pháp và test thật |

**Không đồng nhất:** `source Spine pack=8` với 8 nhân vật; `TRACK_ADVANCING` với hình ảnh đã render; snapshot Scene với gameplay/server đang chạy.

## Tài liệu nguồn đã có

- [Unity viewer README](../unity-ui-viewer/README.md)
- [Ánh xạ REF01–REF04](SCREEN_REFERENCE_MATCHING.md)
- [Kiểm tra runtime](RUNTIME_SCREEN_CHECKLIST.md)
- [Thử nghiệm hình ảnh private](PRIVATE_VISUAL_VERIFICATION.md)

Kiểm tra mã thực tế khi sửa; tài liệu có thể trễ so với commit mới.
