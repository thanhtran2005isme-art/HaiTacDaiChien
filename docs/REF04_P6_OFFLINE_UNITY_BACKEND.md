# REF04 P6 — Phục dựng UI Unity offline, backend tự xây

## Mục tiêu đúng của dự án

Game Hải Tặc Đại Chiến cũ đã ngừng dịch vụ. Mục tiêu của chủ dự án là **phục dựng UI bằng Unity từ bộ XAPK đang sở hữu** và **xây dựng backend độc lập**. Không được bắt buộc đăng nhập game cũ, chụp qua ADB, có máy chủ gốc, hay giả định app cũ khởi chạy được.

Nguồn dữ liệu:
- **SOURCE_VERIFIED:** dữ liệu Unity SerializedFile, Component PathID, RectTransform, Image/Sprite, Text/font/LayoutGroup đã được kiểm tra từ XAPK; P5 kiểm toán 245 bản ghi, trong đó có 1 component chỉ có định danh và 6 tên trường không đủ raw-object SHA.
- **SOURCE_BLOCKED:** những giá trị chưa chứng minh được, đặc biệt Canvas runtime, Screen.safeArea, một số Sprite native geometry, Text localization và tọa độ runtime. Không điền tọa độ hoặc giá trị giả rồi gọi là phục dựng đúng.
- **NEW_PROJECT_DESIGN:** logic tương tác, mô hình dữ liệu, API và bố cục thay thế do chủ dự án chủ động thiết kế cho game mới; phải lưu riêng, không giả mạo là giá trị gốc của XAPK.

## Chạy P6 offline trên Windows

Trong thư mục dự án:

```cmd
git pull --ff-only origin feat/ref04-p2-canvas-il2cpp-runtime-trace
py -3 -m unittest discover -s tests -p test_ref04_p6_offline_ui_plan.py -v
py -3 tools/ref04_p6_offline_ui_plan.py
```

Không cần cài APK hoặc mở server game cũ. Lệnh thứ hai cần **báo cáo đã tạo từ XAPK trước đây** trong `output/`: `ref04-p5-cross-phase-source-integrity.json`, `ref04-full-source-inventory.json`, `ref04-static-image-geometry.json`. Báo cáo trả về được lưu ở `output/ref04-p6-offline-ui-gaps.json` (gitignored).

Nếu thiếu báo cáo `ref04-static-image-geometry.json`, chạy `py -3 tools/audit_ref04_static_ui_geometry.py` **chỉ sau khi** các bằng chứng 3C/visual plan là đúng. Không tạo JSON giả.

Báo cáo P6 phân loại 265 Image liên kết Sprite nguồn, tách những Image có `NATIVE_SPRITE_GEOMETRY_VERIFIED` với Image bị `BLOCKED`. Nó cũng ghi riêng 34 Image không có Sprite pointer, 168 field LayoutGroup, 62 Text và phân loại 15 component P2. Kết quả chỉ là **danh sách bằng chứng phục dựng**, không khẳng định bản Unity đã hiển thị giống game.

## Tiến trình UI Unity và backend riêng

1. **UI gốc:** Đọc các Sprite/Image/RectTransform/LayoutGroup đã có bằng chứng; dựng UI trong nhánh bản dựng/scene riêng. Không sửa hoặc đóng gói thay đổi vào tài nguyên XAPK/3C đã khóa. Ảnh riêng tư không commit.
2. **Các chỗ thiếu dữ liệu:** Đánh dấu `SOURCE_BLOCKED`. Khi chủ dự án muốn tiếp tục, có thể thiết kế bổ sung, nhưng phải ghi rõ là `NEW_PROJECT_DESIGN` và test ở Unity; không ghi `pixelPerfectUiProven=true`.
3. **Tương tác mới:** Thêm điều hướng đội hình, thông tin nhân vật, inventory, trang bị và các thao tác gameplay theo **thiết kế do chủ dự án chọn**. Tên nút/sự kiện game cũ từ dữ liệu chưa kiểm chứng không tự trở thành API backend.
4. **Backend riêng:** Xác định API/auth, catalog/nhân vật, đội hình, inventory, trạng thái trận đấu, kinh tế trò chơi, lỗi mạng, quyền truy cập và dữ liệu lưu trữ riêng. Frontend Unity gọi `IBackendClient`/adapter độc lập qua HTTP; không dùng endpoint, credential hoặc hành vi server gốc.
5. **Kiểm thử:** Chạy Unity EditMode/PlayMode, Android build của game mới nếu có; thử các luồng giao diện, tương tác và API backend mới. So sánh với ảnh tư liệu game gốc chỉ khi có nguồn hợp pháp, đúng trạng thái và độ phân giải; không bắt buộc.

## Không nhầm bằng chứng

`P6 offline audit PASS` nghĩa là dữ liệu nguồn/thiếu nguồn đã được kiểm kê nhất quán. Nó **không** có nghĩa hoàn thành UI Unity, gameplay, backend, screenshot pixel-perfect hoặc đo được runtime Canvas/SafeArea. Với game đã đóng cửa, runtime và giao diện tương lai của phiên bản mới sẽ thuộc thiết kế của chủ dự án; tính tương đồng không được tự khẳng định.

Các công cụ P6 ADB/screenshot đã làm ở đầu giai đoạn vẫn giữ **tùy chọn** để phục vụ ảnh tư liệu nếu một bản cài game cũ còn chạy offline, **không phải bước bắt buộc** hoặc gate của workflow phục dựng offline. PR #7 tiếp tục Draft cho tới khi được nghiệm thu.

Lưu ý: việc sử dụng lại hình ảnh và thương hiệu từ game cũ cho phát hành công khai có thể cần xin phép chủ sở hữu quyền; việc máy chủ ngừng hoạt động không tự động làm mất bản quyền.
