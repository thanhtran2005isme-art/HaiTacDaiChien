# REF04 — Bước 1: Kiểm kê toàn bộ UI từ XAPK gốc (2026-10-10)

> **CHỈ ĐỌC. KHÔNG PHỎNG ĐOÁN.** Quy tắc bắt buộc: [XAPK_SOURCE_ONLY_UI_RULES.md](XAPK_SOURCE_ONLY_UI_RULES.md). Các số liệu này từ Unity SerializedFile/IL2CPP/MonoScript **bên trong XAPK thật**, không phải tọa độ do AI đặt.

## Phạm vi và bằng chứng

- Đã quét **toàn bộ 503 GameObject/RectTransform và 1.564 component** trong **cây serialized ứng viên REF04-home-crew** của XAPK.
- **Chưa khẳng định đây là toàn bộ cây UI lúc runtime chạy:** ancestor ngoài candidate, dynamic children, Canvas viewport/runtime, trạng thái dữ liệu và phép tính IL2CPP chưa được chứng minh.
- Truy vết dựa trên PathID, GameObject/RectTransform owner, MonoScript source class, thứ tự `m_Component`/`m_Children`, giá trị RectTransform đã xuất từ source, SHA-256 của source graph/3C/3D/native geometry, đối chiếu 2 backend managed TypeTree.
- Đầu ra chi tiết **không commit** vì có metadata/tên game: `output/ref04-full-source-inventory.json` và `output/ref04-full-source-inventory.md`, tạo bằng `py -3 tools/audit_ref04_full_source_inventory.py`. Từng GameObject có child order, parent PPtr, source active, RectTransform source values; từng component có original PathID, owner, MonoScript class, nguồn field đã verified, và nguyên nhân chưa verified.

## Số liệu REF04 từ XAPK thật — CI

| Loại chứng cứ | Số lượng |
|---|---:|
| GameObject / RectTransform trong candidate REF04 | 503 |
| Toàn bộ component ID tham chiếu/serialized trong candidate | **1.564 / 1.564** |
| Native RectTransform | 503 |
| Native CanvasRenderer | 385 |
| Native Canvas | 1 |
| MonoBehaviour | 675 |
| Managed component có giá trị đồng thuận hai backend | **317** |
| Giá trị managed field đồng thuận | **2.118** |
| Image có 3C verified fields | 299 |
| Image có original Sprite PPtr | 265 |
| Image 3C không có original Sprite PPtr | 34 |
| LayoutGroup mới đọc qua một backend, **CẤM nhập** | 24 |
| Số field một backend của LayoutGroup REF04, **CẤM nhập** | 168 |
| MonoBehaviour khác chưa có full dual-verified fields | 334 |
| Native component chỉ có kind/header trong báo cáo deep, chưa full source fields | 888 |
| Native component có một phần source fields | 1 |

**Lưu ý:** 888 native records chưa có đủ field không có nghĩa 503 tọa độ RectTransform bị mất: thông tin RectTransform core native được xuất **riêng** trong `gameObjects[].rectSource`. Chưa phục hồi **cách gameplay/runtime biến đổi tọa độ**.

### Những lớp UI đáng chú ý phát hiện từ MonoScript nguồn

| Class/loại | Số lượng |
|---|---:|
| `UnityEngine.UI.Text` | 62 |
| `UnityEngine.UI.Button` | 47 |
| `UnityEngine.UI.Outline` | 82 |
| `UnityEngine.UI.Slider` | 8 |
| `UnityEngine.UI.Mask` | 15 |
| `UnityEngine.UI.HorizontalLayoutGroup` | 18 |
| `UnityEngine.UI.VerticalLayoutGroup` | 6 |
| `UnityEngine.UI.CanvasScaler` | 1 |
| `UnityEngine.UI.ContentSizeFitter` | 2 |
| `UnityEngine.UI.GraphicRaycaster` | 1 |
| `SafeAreaAdapter` | 6 |
| `I2.Loc.Localize` | 26 |
| `TextLocalizeChecker` | 26 |

Đã thấy các class `PanelHome2TopLeft`, `PanelHome2TopRight`, `PanelHome2Bottom`, `PanelHome2BottomLeft`, `PanelHome2BottomRight`, `PanelHome2Center`, `PanelHome2Quest`. **Tên MonoScript chỉ xác nhận loại component**, không chứng minh nội dung logic và tọa độ runtime. Có 17 `Spine.Unity.SkeletonGraphic` nhưng nhân vật để giai đoạn khác theo yêu cầu.

## Những thứ CHƯA xác minh, TUYỆT ĐỐI không tự điền

1. Runtime viewport, parent/ancestor ngoài candidate và tham số Canvas/camera/safe-area do runtime thiết lập. Nguồn có 1 Canvas, 1 CanvasScaler nhưng **chưa chứng minh runtime screen root**. Không đặt tự phát 1600×900 hay scale=1.
2. **24 LayoutGroup / 168 field REF04** chỉ có một decoder; cần độc lập giải mã/cross-check byte và IL2CPP schema trước khi sử dụng.
3. `Text`, `Outline`, `Slider`, `Button`, `Localize` và các script tùy chỉnh còn thiếu field/text font/localization, màu/hình/trạng thái runtime. Không tạo chữ/số giả.
4. Các phép tính runtime của `PanelHome2*`, `SafeAreaAdapter`, `CanvasHero` và Lua/XLua chưa được chứng minh bằng mã IL2CPP thật; không tự dịch icon hoặc đoán giá trị HUD.
5. Trong REF04, Sprite provenance xác nhận 265 original links và 22 Tight-mesh logical-bounds **previews**, nhưng chưa có chứng cứ ảnh cuối giống runtime XAPK. Không gọi preview là asset cuối.

## Kết luận bước 1 và việc tiếp theo

**Bước 1 đã hoàn thành trong phạm vi cây candidate REF04 có chứng cứ SerializedFile.** Report có đủ **mọi Component PathID thuộc 503 GameObject** và tình trạng field rõ ràng; **KHÔNG** xem như phục hồi tất cả component/UI ở runtime. **Chưa sửa bố cục Unity**.

**Bước 2 sau khi người dùng yêu cầu:** kiểm tra/giải mã đầy đủ 24 LayoutGroup, Canvas/native field, Text/Mask và quan hệ UI, giữ byte/offset và source provenance. Nếu không đủ chứng cứ, **không dựng**, chuyển sang giải mã IL2CPP ở bước 3; vẫn thiếu thì hỏi người dùng.

**Nguồn chạy thật:** [GitHub Actions CI Linux+Windows](https://github.com/thanhtran2005isme-art/HaiTacDaiChien/actions/runs/38038668329). CI thành công ở SHA `9c065b84c8373f1c128082dddea2815d7ae7ea02`. GitHub CI **không chạy Unity Game View**, vì thế không tuyên bố UI đẹp/đúng ảnh.

### Chạy locally để lấy báo cáo chi tiết

```cmd
cd /d C:\Users\Admin\Videos\HaiTacDaiChien
git switch feat/xapk-il2cpp-ui-field-provenance
git pull --ff-only origin feat/xapk-il2cpp-ui-field-provenance
py -3 tools\audit_ref04_full_source_inventory.py
```

Nếu máy local thiếu `output/deep-ui-source-evidence.json` hoặc `output/single-backend-layout-review.json`, tool sẽ từ chối, **không thay bằng giá trị ước lượng hoặc tự tuyên bố đủ chứng cứ**; cần giải mã lại dữ liệu source thực theo workflow 3B trên XAPK, không dùng giá trị giả.
