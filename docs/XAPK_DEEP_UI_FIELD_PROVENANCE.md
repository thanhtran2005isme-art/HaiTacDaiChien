# Giai đoạn giải mã sâu UI XAPK — nguồn MonoScript/IL2CPP

## Mục tiêu

Phân biệt ba loại thông tin thường bị nhầm lẫn:

1. **Đã chứng minh GameObject/Component/MonoScript nào** (bằng exact Unity SerializedFile và PathID/PPtr).
2. **Đã đọc được giá trị serialized field gốc** (typetree/native class).
3. **Chỉ biết tên lớp/tên field xuất hiện trong IL2CPP metadata** — đây chưa phải giá trị field hoặc thứ tự byte trên disk.

Không biến mục (1) hoặc (3) thành giá trị CanvasScaler/Image/Mask/LayoutGroup tự suy diễn. Không chụp ảnh từng nút để chỉnh tọa độ.

## Đã có dữ liệu gì từ XAPK?

- Báo cáo cũ xác minh Unity IL2CPP metadata standard header **v31**,
  tại `assets/bin/Data/Managed/Metadata/global-metadata.dat`;
  có `libil2cpp.so` ở APK kiến trúc arm64.
- Repo đã kiểm tra 5 cây nguồn với 1.670 RectTransform và **5.346**
  component reference. Đây là **5 ứng viên**, không phải 5 file
  `.prefab/.unity` nguồn của Unity Editor.
- Bộ dựng cũ vẫn giả định độ phân giải Canvas 1600×900, Image.Simple,
  root scale normalization; không dùng bản dựng đó làm chứng cứ gốc.
- `tools/il2cpp_refs.py` đã thống kê MonoScript classes toàn XAPK
  nhưng chưa ghép tên lớp và trạng thái field tới **từng component của 5 cây**.

## Bản sửa giai đoạn này

`tools/decode_xapk_ui_provenance.py` chạy sau
`tools/audit_original_unity_graph.py`:

- Đối chiếu source GameObject → component PathID → MonoBehaviour header
  → `m_Script` PPtr → MonoScript đúng serialized file.
- Kiểm tra owner `m_GameObject` của từng MonoBehaviour/native component;
  nếu lệch **BLOCKED** thay vì gắn nhầm.
- Chỉ xuất giá trị UI allowlist có thể đọc thực sự qua
  `read_typetree()`. Các nhóm gồm:
  `UnityEngine.UI.Image`, `CanvasScaler`, `Mask`,
  `RectMask2D`, `Horizontal/Vertical/GridLayoutGroup`,
  `ContentSizeFitter`, `AspectRatioFitter`, `CanvasGroup`.
- Canvas/RectTransform có field native thì giữ giá trị nguồn và
  trạng thái; không suy ra Camera, CanvasScaler hay runtime.
- Đọc IL2CPP metadata **v31**: xác thực magic/version và thống kê
  sự hiện diện của tên field trong binary; **chỉ coi là tên gợi ý**.
  Không tự tính field offsets hay đưa ra field values chỉ vì metadata
  có một chuỗi. Không dump `libil2cpp.so`, toàn bộ metadata hay
  mã nguồn game lên GitHub.

**Output chỉ có trên máy, không commit:**

- `output/deep-ui-source-evidence.json`: mỗi source component có
  SerializedFile, RectTransform ID, GameObject ID, component ID,
  class từ MonoScript PPtr nếu giải được, state trường và các field
  **thực sự giải mã được**.
- `output/deep-ui-source-evidence.md`: số liệu từng REF và danh sách
  tên các trường managed đã được chứng minh.

## Chạy trên Windows

Sau khi đồng bộ nhánh PR chứa phần này và chuẩn bị XAPK hợp pháp:

```cmd
cd /d C:\Users\Admin\Videos\HaiTacDaiChien
py -3 tools\audit_original_unity_graph.py
py -3 tools\decode_xapk_ui_provenance.py
type output\deep-ui-source-evidence.md
```

Hoặc chạy `CHUAN_BI_DO_HOA.bat`; dòng kết quả mới:
`IL2CPP UI source provenance: PASS`.

## Các trạng thái chính xác

| Trạng thái | Ý nghĩa |
|---|---|
| `SERIALIZED_FIELDS_VERIFIED` | Có giá trị allowlist đọc trực tiếp từ source typetree |
| `NO_MANAGED_TYPETREE` | Xác minh MonoScript class nhưng **không** đọc được trường |
| `SOURCE_SCRIPT_UNRESOLVED` | Chưa chứng minh được MonoScript từ source PPtr |
| `TYPETREE_NO_TARGET_FIELDS` | Có typetree nhưng không có trường UI trong allowlist |
| `NATIVE_FIELDS` | Trường native được đọc trực tiếp |
| `NOT_TARGET` | Component không thuộc nhóm giải mã ở giai đoạn này |

**PASS kiểm thử** chỉ có nghĩa các tham chiếu được đối chiếu và thiếu sót
được phân loại đúng; không có nghĩa phục hồi UI 100%.

## Bước sau khi phân tích kết quả thực tế

1. Trước hết đọc số liệu `SOURCE_SCRIPT_UNRESOLVED` và
   `NO_MANAGED_TYPETREE` theo REF04.
2. Nếu MonoScript đã định danh nhưng thiếu typetree, nghiên cứu
   exact-version layout metadata, field types/offsets từ các tài nguyên
   có quyền sử dụng và xác minh bằng mẫu serialized bytes/bounds.
   **Không dùng chuỗi metadata làm vị trí byte giả định.**
3. Thêm decoder chuyên biệt **chỉ khi field layout đã được kiểm chứng**;
   gắn được Image.Type/CanvasScaler/Mask/LayoutGroup vào prefab với
   nhãn `SOURCE_VERIFIED`.
4. Kiểm chứng scene cùng runtime state, Camera và Spine thật;
   không dùng thuật toán phỏng đoán để sửa khung cho giống ảnh.

Mọi tài nguyên XAPK, asset Spine, field ID chi tiết nằm tại output/
(Git ignored). Đây là công cụ đọc và phân tích, không sửa game.
