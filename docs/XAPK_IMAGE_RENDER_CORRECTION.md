# Khôi phục hiển thị UI dựa trên Image component thật — 2026-10-09

## Đây là sửa lỗi render, không phải khôi phục UI 100%

Bộ dựng uGUI cũ liên kết Sprite theo **đường dẫn GameObject (string)** và tự
bật `Image` khi có Sprite. Vì nhiều nút Unity có tên giống nhau, 137 Image
đúng nguồn bị bỏ qua; đồng thời Image mà asset lưu `m_Enabled=0` vẫn có thể
hiển thị sai.

Bản sửa này sử dụng chuỗi ID đọc từ XAPK:

```text
Serialized Image componentId
   -> original MonoBehaviour.m_GameObject pathId
   -> original GameObject / RectTransform pathId
   -> Sprite đúng source_file:sprite_pathId
```

Khi đường dẫn UI trùng tên, không dùng đường dẫn để chọn ngẫu nhiên.
Nếu nhiều Sprite cạnh tranh cùng component ID thì **không** tự chọn.
Xác minh thêm từ `output/local-ui-layout.json` đọc lại trên XAPK:
`sourceImageComponentId` trong plan phải trùng `componentId` của Image
trên **cùng nodeId**; nếu không, Unity dừng Reconstruct thay vì ghép nhầm.

## Kết quả CI thực tế trên XAPK (Linux/Windows)

| Scene | Original Image/Sprite links |
|---|---:|
| REF01 ship-upgrade | 32 |
| REF02 hero-detail | 574 (trước đó 460, **+114**) |
| REF03 islands A | 43 |
| REF03 islands B | 49 |
| REF04 home-crew | 265 |
| **Tổng** | **963**, tăng từ **826** |

- **11 Image bị tắt ngay trong asset**: REF01=0, REF02=9, REF03A=0,
  REF03B=1, REF04=1. Trạng thái `m_Enabled` lấy từ chính serialized
  `MonoBehaviour` header, **không phụ thuộc IL2CPP typetree**.
- **0/963 Image có managed typetree khả dụng**: chưa thể lấy `Image.Type`,
  `Color`, `Fill`, `PreserveAspect`, raycast và các field này từ
  serialized dữ liệu. Bản dựng vẫn phải dùng giá trị preview cho các
  thuộc tính không xác minh được — **không phải UI gốc hoàn chỉnh**.
- Unity đã có logic chỉ áp dụng `Sliced`/`Tiled`/`Filled` khi
  `Image.Type` được đọc thật. Kết quả XAPK hiện tại không có field đó,
  nên không tự gán kiểu mới.
- `CanvasScaler.referenceResolution` và trạng thái runtime của người
  chơi/đội hình không đọc được. Root 1600×900 vẫn là **preview giả định**,
  không được gọi chính xác 100%.
- REF04 được **thêm định danh chắc chắn**, không tăng số sprite từ 265 vì
  265 component ở REF04 đã có đường dẫn duy nhất. Khác biệt render ở
  REF04 được xác minh từ bước này là **1 Image nguồn bị tắt**.

## Cách kiểm tra trên Windows

Dùng branch của PR chứa bản sửa, đóng Unity Editor trước khi chuyển nhánh.

```cmd
cd /d C:\Users\Admin\Videos\HaiTacDaiChien
git status --short
git fetch origin
git switch fix/ref04-source-image-render-state
git pull --ff-only origin fix/ref04-source-image-render-state
CHUAN_BI_DO_HOA.bat
```

Script phải in `Original Image owner/state: PASS`, gồm số component
được đối chiếu và số Image tắt. Sau đó mở
`unity-ui-viewer` trong Unity Hub, chọn:

**Tools → HaiTac Offline UI Viewer → Reconstruct 5 local Canvas prefabs**.

Mở `REF04-home-crew.unity` trong **Game View**, đặt 16:9 và xem Console:

```text
[HaiTac Image state] REF04-home-crew:
  disabled source Image components=1, enabled state unknown=0
```

Các dòng `[HaiTac Image state]` của 4 scene còn lại giúp phân biệt
`m_Enabled` trong XAPK với trạng thái UI khi game thực sự chạy.

## Chưa khôi phục được từ XAPK tĩnh

- Cách game bật/tắt Image sau khi script chạy;
- CanvasScaler runtime, Mask/LayoutGroup, thứ tự sorting động;
- Component managed bị strip, màu sắc, text/font và Image 9-slice/Fill;
- SkeletonGraphic, skin/animation thực tế và đội hình được server chọn.

Không thể chỉ thêm hình hoặc chỉnh tọa độ theo ảnh để coi là 100%.
Cần thu được **runtime state có quyền sử dụng**, xác minh managed
field layout, và kiểm thử Game View với bản gốc ở cùng thời điểm/trạng thái.

## An toàn

XAPK, Sprite PNG, Spine runtime, source mapping chi tiết lưu trong
`output/` và `Assets/LocalReconstruction/` (Git ignored).
Không đẩy tài nguyên game thương mại vào GitHub.
