# REF04 P2 — Canvas, SafeAreaAdapter, PanelHome2 from source IL2CPP

## Đúng phạm vi

P1 đã xác minh **24 LayoutGroup / 168 field serialized** trên XAPK. P2 không được lấy các field này để suy ra tọa độ lúc chạy. Chỉ phân tích original SerializedFile/PathID/source byte SHA256, tên phương thức từ original IL2CPP metadata v31 và ELF libil2cpp.so. Không chỉnh giao diện Unity.

## Công cụ

- **tools/ref04_il2cpp_method_index_v31.py:** đọc bảng string/type/method của global-metadata.dat **v31 chính xác từ XAPK**, kiểm tra class/type ownership, methodDefinitionIndex, methodToken, Unity Screen/Canvas/CanvasScaler/RectTransform, SafeAreaAdapter và PanelHome2*. Method token không phải địa chỉ mã ARM64 hay bằng chứng phép tính lúc runtime.
- **tools/audit_ref04_p2_runtime_alignment.py:** đối chiếu original 503 GameObject/1.564 component, 1 Canvas, 1 CanvasScaler, 6 SafeAreaAdapter, PanelHome2* nếu thuộc cây nguồn. Truy dấu parent pointer và Canvas ancestor chỉ khi tồn tại trong serialized hierarchy. So sánh exact SHA256 của libil2cpp.so và global-metadata.dat với báo cáo P1 và xác minh ELF64 ARM64.
- **tests/test_ref04_il2cpp_method_index_v31.py** và **tests/test_ref04_p2_runtime_alignment.py** chống metadata corrupt, sai method owner, giả code address, sai PathID/hash và suy đoán parent nằm ngoài subtree.

## Kết quả đã chạy trên original XAPK (2026-10-10)

[GitHub Actions #38049542276](https://github.com/thanhtran2005isme-art/HaiTacDaiChien/actions/runs/38049542276) **PASS** trên commit code `8330bd84` (Linux/XAPK thật, Windows và P1 168/168): 1 Canvas, 1 CanvasScaler, 6 SafeAreaAdapter, 7 PanelHome2* source components, 8 PanelHome2* metadata type definitions, 1 SafeAreaAdapter metadata type, 204 method declarations trong các type mục tiêu (gồm Canvas/Scaler/Screen/RectTransform). **0 verified native method bodies**, **0 runtime formulas**. Đây là kết quả metadata declarations + serialized hierarchy, không có công thức thực thi.

Giá trị serialized Canvas/CanvasScaler có nguồn được giữ trong báo cáo riêng **gitignored local** `originalSerializedFieldEvidence`; không log/commit các giá trị này. Những giá trị đó chưa phải runtime viewport/scaling.

## Mức chứng cứ

| Phạm vi | Có thể chứng minh | Không thể suy ra |
|---|---|---|
| Canvas/CanvasScaler | Component/serialized field và owner đúng nguồn | Viewport và scaling sau runtime mutations |
| SafeAreaAdapter | Sáu source components, ancestor chain thật | Actual Screen.safeArea, device insets, công thức áp dụng |
| PanelHome2* | Class/method definitions và token trong metadata v31 | ARM64 method body, nhánh và phép gán layout |
| LayoutGroup | P1: 168/168 field nguồn được xác minh | Runtime layout rebuild và tọa độ hiển thị |

**Báo cáo local-only:** output/ref04-p2-canvas-il2cpp-runtime-source.json + .md (ignored), không commit raw bytes/game values. Yêu cầu có output P1 gồm ref04-full-source-inventory, ref04-step2-layout-canvas-text, ref04-layout-schema-forensics rồi chạy:

```powershell
py -3 tools/audit_ref04_p2_runtime_alignment.py
py -3 -m unittest discover -s tests -p test_ref04_il2cpp_method_index_v31.py -v
py -3 -m unittest discover -s tests -p test_ref04_p2_runtime_alignment.py -v
```

## Blocker của công thức runtime

Metadata chỉ chứa *khai báo* method, không phải code. Cần ánh xạ được method token/definition index tới function address gốc từ ELF code registration; chứng minh body ARM64, các field write/condition và device/screen/safe-area state thật. Trước đó giữ **runtimeAlignmentFormula=null**, **runtimeAlignmentProven=false**, **unityImportAllowed=false**. Không được dùng độ phân giải 1600×900 mặc định hay bất kỳ tọa độ ước lượng nào. Không tạo Scene/Prefab/Canvas/HUD và chưa xử lý nhân vật.
