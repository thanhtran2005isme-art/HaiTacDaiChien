# AI HANDOFF — Hải Tặc UI Viewer

## Bổ sung 2026-10-12 — AssetRipper HomeScene navigation pilot

- **Source hiện tại để test:** nhánh `feat/homescene-navigation-pilot`, bắt đầu từ `feat/ui-navigation-audit` (HEAD nguồn `643673b4`). Dự án AssetRipper xuất Unity **2022.3.51f1**, không phải project `unity-ui-viewer/` mô tả trong các đoạn handoff cũ phía dưới.
- **Main vẫn là `feee5b19`**, không phải baseline chứa AssetRipper; chưa merge pilot và không được pull `main` để kỳ vọng có source Unity mới.
- **Đã sửa:** giữ nguyên 55 persistent OnClick của `Assets/Scenes/HomeScene.unity`; thêm `HomeSceneNavigationBridge` vào Scripts GO, có 19 callback điều hướng thử tới 11 Scene hiện có; Esc quay về HomeScene. `NEW_PROJECT_DESIGN`, không tuyên bố các đích là logic game XAPK gốc.
- **Đã kiểm tra:** các nút tham chiếu component hợp lệ; 55 OnClick gốc không đổi; khác biệt chỉ ở 7 file mã, meta, Scene, test, tài liệu; chưa chạy Unity Editor Play Mode hoặc C# compiler.
- **Còn thiếu:** đường đích mơ hồ (Heroes, Campaign, Shop, Summon, Lucky, Brave Trial, Popup); Scene đích có thể thiếu runtime data/manager; các C# stub gốc vẫn rỗng, chưa có gameplay. `HTDC_Localization_Hold` vẫn cách ly ngoài Assets.
- **Tiếp theo:** người dùng pull pilot, chạy `py -3 -m unittest discover -s tests -p test_home_scene_navigation_pilot.py -v`, mở HomeScene Unity 2022.3.51f1 và thử nút + Esc; gửi Console/Play Mode nếu lỗi. Xem `docs/navigation/HOMESCENE_NAVIGATION_PILOT.md`.
- **Lưu ý:** nội dung lịch sử dưới đây trước lúc import AssetRipper, vì vậy một số lệnh `.bat` và đường dẫn `unity-ui-viewer/` không còn áp dụng cho HEAD hiện tại của nhánh này.

**Cập nhật:** 2026-10-11 (Asia/Ho_Chi_Minh)
**Repo:** [thanhtran2005isme-art/HaiTacDaiChien](https://github.com/thanhtran2005isme-art/HaiTacDaiChien)
**Nhánh ổn định:** `main`
**Tuyến tích hợp:** PR #2 và #3 đã squash merge vào main; PR #4 là tuyến tổng hợp mã #5, #6, #7. Luôn xác minh HEAD/PR thực tế trước khi tiếp tục.
**Lịch sử chi tiết bảo toàn:** [history/2026-10-ref04-pr-stack-handoff.md](history/2026-10-ref04-pr-stack-handoff.md). Hồi 2026-10-09 đã có baseline 5 Canvas.

## Mục tiêu

Nguồn XAPK của Hải Tặc được phép sử dụng để tạo UI/asset **offline**, sau đó phát triển Unity client và backend **độc lập** (`NEW_PROJECT_DESIGN`). Server game cũ không còn hoạt động; không coi preview là game playable hay phục hồi UI gốc pixel-perfect. Mọi dữ liệu XAPK phải được kiểm chứng theo PathID/byte/schema; không suy đoán runtime.

## Đã có

- Web offline: `CHAY_UI_OFFLINE.bat` phục vụ `http://127.0.0.1:8765`; Unity Editor project `unity-ui-viewer/` sử dụng 2022.3.21f1, Spine 3.8 runtime tùy chọn có giấy phép.
- 5 cây UI tham chiếu gồm 1.670 RectTransform và 5.346 component refs; nguồn Sprite/Spine xuất vào `output/` local, Git ignored. Thuật toán pathID/Image owner tránh trùng đường dẫn.
- REF04 đã audit source: 503 RectTransform; 299 Image (265 Sprite-linked, 34 không có Sprite); 24 LayoutGroup và 168/168 giá trị serialized với hai schema; 62 Text, 52 I2 localizers (0 term independently verified), 2 Font source objects. Các số liệu **không chứng minh** runtime geometry, active characters hay pixel fidelity.
- P5 provenance có 245 entries gồm 168 layout fields, 15 native components (14 raw-SHA, 1 ID-only BLOCKED) và 62 Text; field thiếu source hash bị cách ly, không hợp thức hóa Unity runtime formula.
- P6 nguồn offline và Unity Editor workspace, P6.2 client-fit Scene `REF04-home-crew_NEW_CLIENT_FIT_STUDY.unity`, công cụ cover nền có backup, P6.3 **read-only** audit icon/HUD/PlayerHeroes xuất `output/ref04-p63-client-icon-layout-audit.json`. Scene/asset generated nằm trong `Assets/LocalReconstruction/` (ignored).
- Đã tích hợp 5 module Unity built-in cho Spine (`physics`, `physics2d`, `animation`, `audio`, `particlesystem`). CI Python/Linux/Windows chỉ xác minh hợp đồng mã và bằng chứng nguồn; **không chứng minh** đã compile hoặc render đúng Unity Editor.
- Baseline 2026-10-09: audit Unity local từng báo `missing Mono Scripts=0` và REF04 8 source Spine packs liên kết; không tự coi đây là test sau cùng ở commit mới.

## Vấn đề còn lại / giới hạn

1. **Unity Editor/Play Mode chưa xác nhận** đối với P6.2/P6.3 trên HEAD mới nhất. Background cover và viewport fit thuộc `NEW_PROJECT_DESIGN`; không phải CanvasScaler/SafeArea runtime nguyên gốc.
2. Icon nắm đấm lơ lửng, shadow nhân vật trống, thiếu HUD/Text/Localization; P6.3 mới **kiểm kê**, chưa di chuyển icon hay gắn nhân vật Spine.
3. Chưa có gameplay, API backend riêng, database, hoặc server hoạt động. Spine-Unity 3.8 hỗ trợ chính thức đến Unity 2020.3; project viewer dùng 2022.3 nên phải thử runtime trên bản sao phù hợp.
4. Không đưa XAPK binary/artwork, runtime Spine có điều kiện cấp phép, output private, LocalReconstruction vào commit mới. Pull Git **không** tái tạo asset local bị ignore.
5. Đối chiếu CI của chính SHA, không lấy test thành công của commit trước để công bố chất lượng runtime. Nhánh `main` yêu cầu quy trình PR, không force-push.

## Cách chạy

```cmd
git status
git fetch origin
CHUAN_BI_DO_HOA.bat
CHAY_UI_OFFLINE.bat
```

Unity: mở `unity-ui-viewer/` → `Tools → HaiTac Offline UI Viewer → Source XAPK → P6 - REF04 offline Unity workspace`. Audit icon: mở riêng P6.2 `NEW_CLIENT_FIT_STUDY`, dùng nút **9. Kiem tra icon, HUD va PlayerHeroes**, sau đó đọc Console/báo cáo private.

## Ưu tiên công việc kế tiếp

1. Xác minh trạng thái merged của chuỗi PR #2–#7 và HEAD thực tế; kiểm tra CI SHA tương ứng.
2. Chạy Unity Editor trên dữ liệu local có quyền dùng, xác minh P6 workspace/P6.2/P6.3, Console, Game View, đọc báo cáo icon owners trước khi thiết kế lại.
3. Chỉ sửa UI thuộc Scene `NEW_PROJECT_DESIGN` sau khi xác định nhóm nguồn; bổ sung Spine nhân vật và Text bằng nguồn hợp lệ, không bịa asset/tọa độ gốc.
4. Thiết kế backend mới độc lập theo task được giao sau, không tự tạo API/database hoặc tuyên bố gameplay đã có.
5. Khi thay đổi quan trọng, ghi quyết định tại `DECISIONS.md`, troubleshooting tại `TROUBLESHOOTING.md`, handoff ngắn và lịch sử ở `docs/history/`.
