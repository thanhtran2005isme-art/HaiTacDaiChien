# AI HANDOFF — Hải Tặc UI Viewer

**Cập nhật:** 2026-10-09 (Asia/Ho_Chi_Minh)  
**Repo:** [thanhtran2005isme-art/HaiTacDaiChien](https://github.com/thanhtran2005isme-art/HaiTacDaiChien)  
**Nhánh ổn định:** `main`  
**HEAD của main lúc lập handoff:** [8b4c3ac6](https://github.com/thanhtran2005isme-art/HaiTacDaiChien/commit/8b4c3ac6c11779fc7f4b88fd2c485be72d9bfafc)  
**Đợt bàn giao hiện tại:** cấu trúc docs và quy trình PR được thực hiện tại [PR #1](https://github.com/thanhtran2005isme-art/HaiTacDaiChien/pull/1). **Kiểm tra trạng thái merged của PR #1 và HEAD `main` trước khi làm tiếp**; SHA bên trên là mốc trước PR, không tự coi là HEAD mới nhất.

## Mục tiêu và giới hạn

Khảo sát tài nguyên Unity trong XAPK Hải Tặc (được phép sử dụng), hiển thị UI offline bằng Web/Unity; ưu tiên **Sprite, quan hệ GameObject và animation thực** thay vì wireframe/phỏng đoán. Đây **không phải** Unity source gốc, game client/server gốc hoặc bản dựng pixel-perfect.

## Đã có
- **Web UI offline:** `CHAY_UI_OFFLINE.bat` → `127.0.0.1:8765` (viewer; Spine JS 3.8 tùy chọn).
- **Đồ họa từ XAPK local:** `CHUAN_BI_DO_HOA.bat`; `output/local-ui-art`, `output/local-spine`, `output/local-ui-layout.json` và các JSON bằng chứng. Tất cả private/ignored.
- **Unity viewer:** `unity-ui-viewer/` với 5 scene Canvas ứng viên; `Assets/LocalReconstruction/` sinh ở máy local; có Camera preview và Sprite thật đã khớp định danh.
- **Kiểm kê đã báo cáo:** 1.670 RectTransform qua 5 scene; 826 vị trí Sprite, 230 ảnh Sprite nguồn, 32 `SkeletonGraphic` ứng viên; 13 chuỗi nội dung skeleton/atlas có bằng chứng, trong đó REF04 có 8 gói local có thể liên kết. Các con số này thuộc báo cáo XAPK/kiểm thử, **không chứng minh pixel fidelity**.
- **Ảnh Unity do người sử dụng thử ngày 2026-10-09:** REF04 hiển thị 242 Sprite, 1 Camera, 1 Canvas; Audit báo `missing Mono Scripts=0`, `content-matched=8`, `imported source packs=8`. Đây là kết quả trên môi trường local lúc đó, **không phải kiểm thử tự động đa máy**.
- **Giai đoạn 2B:** mã để phát hiện Spine-Unity 3.8 và tạo scene thử animation riêng trong `Assets/Editor/OfflineSpine38Preview.cs`, cùng `SpinePreviewProbe.cs`; chưa có bằng chứng animation thực tế đã render thành công trong Unity.
- **Lỗi đang sửa (2026-10-09):** Sau khi cài runtime Spine vào Unity local, Console phát sinh nhiều `CS1069` cho `Rigidbody`, `Rigidbody2D`, `Animator`, `AnimationClip`, `PolygonCollider2D`, `HingeJoint2D`. Đối chiếu `Packages/manifest.json` trên `main` cho thấy thiếu các Unity built-in modules `physics`, `physics2d`, `animation`. Nhánh `fix/unity-spine38-builtin-modules` bổ sung 3 dependency `1.0.0` + test manifest; **chưa xác nhận Unity Editor compile PASS**. PR và CI cần được kiểm tra trước merge; không commit vendor code trong `Assets/Spine`.

## Vấn đề còn lại / giới hạn chứng cứ
1. Unity project hiện dùng **2022.3**, trong khi spine-unity 3.8 chính thức hỗ trợ đến Unity 2020.3. Thử runtime trong **bản sao project**; không coi Python CI là chứng cứ tương thích Unity Editor.
2. Chưa xác minh skin/animation đang chạy và sáu nhân vật được chọn ở REF04; **không tự lấp vị trí bằng nhân vật đoán**.
3. CanvasScaler runtime, đầy đủ Mask/LayoutGroup/Image typetree, logic kích hoạt và trạng thái server chưa khôi phục được; `1600×900` là khung thử, không phải xác nhận của game.
4. Các `Spine-Unity` runtime/art/ảnh game có bản quyền **không được commit lên GitHub**.
5. Scene/Pefab được tạo local thường bị Git ignore: `git pull` **không tự tạo lại**; cần chạy Reconstruct trong Unity sau khi đổi script.

## Cách chạy / kiểm tra nhanh
```cmd
cd /d C:\Users\Admin\Videos\HaiTacDaiChien
git fetch origin
git status
CHUAN_BI_DO_HOA.bat
CHAY_UI_OFFLINE.bat
```
Unity: Unity Hub → `unity-ui-viewer` → Tools → HaiTac Offline UI Viewer → **Reconstruct 5 local Canvas prefabs** → **Audit 5 generated Canvas scenes**. Test Spine: **Spine 3.8 → Diagnose and preview real animations** (chỉ khi đã cài runtime có quyền sử dụng, thử scene tách biệt).

**Kiểm thử:** `.github/workflows/`, `tests/`; xem **CI của đúng SHA trong PR**. Không nói `PASS Unity animation` nếu mới qua unit test/CI không chứa Unity Editor.

## Ưu tiên công việc kế tiếp
1. Đối chiếu trạng thái [PR #1](https://github.com/thanhtran2005isme-art/HaiTacDaiChien/pull/1): nếu đã merge, bắt đầu chức năng mới **từ `origin/main`**, xác nhận hai workflow metadata/reports không còn tự push main.
2. Hoàn tất PR `fix/unity-spine38-builtin-modules` sau CI PASS và **xác nhận thực tế từ Unity**: mở lại project, Console hết `CS1069`, gửi lỗi khác nếu có. Chỉ sau đó tiếp tục thử Spine-Unity 3.8 hợp pháp trên bản sao project tương thích; thu Console/Play Mode và ảnh render.
3. Tìm dữ liệu runtime/binding đủ tin cậy để xác định chính xác GameObject và animation; cập nhật kết quả có căn cứ.
4. Cập nhật file này sau khi merge mỗi PR; chuyển chi tiết commit vào [history](history/2026-10.md).

**Quy tắc phiên sau:** Trước khi bắt đầu hãy xem PR mở, HEAD `main`, `git status`, file này và `AGENTS.md`; ưu tiên vấn đề đang được xác minh thay vì làm lại bước đã PASS.
