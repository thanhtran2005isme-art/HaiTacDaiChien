# REF04 P6 — Phục dựng UI Unity offline, backend tự xây

## P6.2 bản sửa thực tế — lấp hai dải đen ở Background (2026-10-11)

**Vấn đề được tái hiện qua ảnh Game View:** sau khi Build/Open `REF04-home-crew_NEW_CLIENT_FIT_STUDY`, CanvasScaler match-scale làm hình nền biển co vào giữa, tạo khoảng trống đen hai bên trong khi HUD vẫn bám mép ngoài. Nội dung UI vẫn y hệt bản cũ vì Scene P6.2 là *copy* của P6.1. Người dùng đúng khi nói chưa thấy phục dựng thành phần nào mới. Không gọi việc đổi CanvasScaler là “giao diện được sửa hoàn chỉnh”.

Đã thêm **thao tác thực sự tác động bố cục trong client riêng**, không yêu cầu dựng Scene mới:
- `unity-ui-viewer/Assets/Scripts/Ref04ClientBackgroundCover.cs`: tìm Image **có Sprite thật, đang bật, có diện tích Rect lớn nhất** trong nhánh `Background`, đo khung của nó trong Game View và tăng `localScale` cho **toàn bộ nhánh nền** theo một hệ số đồng đều `max(screenWidth/bgWidth, screenHeight/bgHeight)`. Không thay ảnh, không đổi Text, không đặt tọa độ hay kéo méo art. Có thể **crop mép nền** ở tỉ lệ rộng; đây là **NEW_PROJECT_DESIGN**, KHÔNG phải XAPK runtime.
- `Ref04P62ClientSceneBuilder.FixClientBackground()`: **chỉ chạy trên `NEW_CLIENT_FIT_STUDY` đã có**, đối chiếu lại P5/P6 và P6.1 Study. Bắt buộc người dùng xác nhận, sao lưu nguyên Scene client trước thành `REF04-home-crew_NEW_CLIENT_FIT_BEFORE_BACKGROUND.unity` (gitignored), không cho phép chạy lại nếu backup đã tồn tại. Tìm duy nhất source UI root và GameObject `Background` có `OriginalSerializedEvidence` và Sprite-backed Image. Thêm cover, lưu Scene client. Khi lỗi, thử khôi phục từ backup; tuyệt đối không xóa Prefab/Scene P6.1 hoặc art.
- Nút **8. Sua nen P6.2 - luu ban sao truoc** ở cuối cửa sổ **P6 - REF04 offline Unity workspace**. Nếu muốn thao tác từ menu: **Tools → HaiTac Offline UI Viewer → Source XAPK → P6.2 - Fix background bars in existing client scene**.

**Để chạy trên máy Windows:**

```cmd
git pull --ff-only origin feat/ref04-p2-canvas-il2cpp-runtime-trace
py -3 -m unittest discover -s tests -p test_ref04_p62_background_cover_contract.py -v
```

Quay về Unity 2022, đợi C# biên dịch, mở cửa sổ P6, bấm nút 8 một lần rồi trở về tab Game. Không cần dựng lại P6.1/P6.2. Chụp ảnh ở **cùng Free Aspect/Scale** trước và sau sửa, cho biết dải đen hai bên đã được lấp chưa; nếu có lỗi Console, gửi **lỗi đầu tiên sau khi bấm nút 8**. Không sửa bằng tay, xóa Scene hoặc Prefab nguồn.

**Giới hạn cần nói rõ:** Bản này chỉ sửa **cách phủ nền**, KHÔNG làm xuất hiện nhân vật Spine, không khôi phục Text/Localization/Font, không sửa toàn bộ 503 layout hay nối backend mới. Các việc đó là mục tiêu tiếp theo cần thực hiện có bằng chứng, không tự gọi hoàn thành. Tests Python ở repo là *contract/static*, **chưa chứng minh code compile hoặc render đúng trong Unity**. Kiểm tra trên máy người dùng là bắt buộc.

---


## P6.2 — Sửa hiển thị Game View bằng Scene mới, KHÔNG chỉ kiểm toán nữa

**Nhận xét người dùng:** Scene P6.1 mở được nhưng UI **vẫn gần như main**: dưới màn hình bị cắt, bảng thiếu chữ, nhân vật Spine không xuất hiện. Lý do thực: P6.1 chỉ dùng lại `Ref04NativeBoundsPreviewImporter` và prefab Study đã có, Root Canvas 1600×900 PREVIEW; **P6.1 chưa triển khai UI playable/backend và không sửa hiện tượng cắt do viewport**. Các script audit PASS không phải bản UI mới.

**Thay đổi UI nhìn được trong P6.2:** tạo Scene **mới độc lập**, copy Scene P6.1 nhưng thêm `Ref04ClientViewportFit` (thuộc **NEW_PROJECT_DESIGN**) vào instance Canvas trong Scene mới. Tự chọn `CanvasScaler.matchWidthOrHeight=0` khi viewport hẹp hơn tỉ lệ 1600:900 và `=1` khi viewport rộng hơn, giữ toàn bộ các Image/child RectTransform gốc không thay đổi; **giữ hình và nội dung nguyên, chỉ thay cách fit viewport**. Scene mới phục vụ game/backend tương lai, **không phải phép đo runtime gốc**. Có thể xuất hiện letterboxing khi tỉ lệ màn hình khác thiết kế; không kéo méo Sprite.

Chạy trên Windows:

```cmd
git pull --ff-only origin feat/ref04-p2-canvas-il2cpp-runtime-trace
py -3 -m unittest discover -s tests -p test_ref04_p62_client_fit_contract.py -v
py -3 -m unittest discover -s tests -p test_ref04_p6_unity_workspace_contract.py -v
py -3 tools/ref04_p6_offline_ui_plan.py
```

Sau khi Unity Editor biên dịch, mở **Tools → HaiTac Offline UI Viewer → Source XAPK → P6 - REF04 offline Unity workspace** và thao tác:

1. Kiểm tra nguồn PASS và Prefab P6.1 hiện tại đã có (bước 4).
2. Bấm **6. Tao P6.2 NEW CLIENT viewport Study**. Script kiểm tra byte-SHA P5, 299 Image, 265 Sprite, 503 RectTransform; KHÔNG chạy build nếu thiếu chứng cứ hoặc trùng đích.
3. Bấm **7. Mo P6.2 NEW CLIENT Scene**. Mở `Assets/LocalReconstruction/Ref04OfflineClientScenes/REF04-home-crew_NEW_CLIENT_FIT_STUDY.unity` (gitignored), so sánh với bản cũ `Ref04NativeBoundsStudyScenes/...NATIVE_BOUNDS_STUDY.unity` trong cùng tỉ lệ Game View, ví dụ 16:9.
4. Chụp toàn màn Game View cả P6.1 và P6.2; **kiểm tra trước tiên HUD đáy và các phần bị cắt**. Nếu cửa sổ Game đang ở `Free Aspect / Scale 1x`, hãy đặt Game View scale phù hợp khi chụp để không nhầm cropping do editor zoom với clipping thực.
5. Khi Scene mới đã tồn tại, lệnh Tạo tự khóa để bảo vệ kết quả. Nếu lệnh tạo lỗi, script chỉ rollback Scene P6.2 vừa tạo, không đụng Scene/Prefab nguồn.

**Chưa sửa trong P6.2:** nhân vật Spine/animation, Text động, các bảng gỗ trống dữ liệu, thao tác nút hoặc backend mới. Chúng phải được phát triển riêng trong bản Unity mới và cần bằng chứng/tài nguyên tương ứng; không được kết luận đã phục dựng 100% UI gốc chỉ vì viewport vừa khung. **CI kiểm tra hợp đồng mã và Python**, còn chạy thực tế Unity Editor/PlayMode vẫn do người dùng xác nhận.

---


## Cập nhật trải nghiệm cửa sổ P6 — tự kiểm nguồn khi mở (2026-10-11)

Trong ảnh người dùng chụp sau bản sửa trước: Unity **không có lỗi Console**, Prefab + Scene đã tồn tại, nhưng ô đầu vẫn ghi `Chưa kiểm tra dữ liệu P6 offline`. Lý do là phiên bản cũ khởi tạo ô trạng thái cố định; không tự chạy kiểm chứng cho đến khi bấm bước 1, và khi Unity domain reload trạng thái lại trở về dòng mặc định.

Bản mới `Ref04P6OfflineWorkspace.OnEnable()` chạy `RefreshSourceStatus()` mỗi khi mở cửa sổ hoặc scripts nạp lại: kiểm toán hash báo cáo P5, source inventory, native Sprite geometry từ P6 private. Nếu hợp lệ, hiện **`P6 nguồn PASS: 265/265 geometry; 299 Image; 62 Text nguồn`**. Nếu không hợp lệ, hiện lỗi **cụ thể** (thiếu hash dạng phẳng, hoặc sai SHA từng báo cáo) và giữ nguyên source assets. Nút 1 đổi thành **Kiểm tra lại dữ liệu nguồn P5/P6 (chỉ đọc)**.

Thông báo “đã có Prefab + Scene” bây giờ là **một dòng trạng thái trung tính tự xuống hàng**, vì tồn tại bản Study là điều bình thường, không phải lỗi. Nút Dựng vẫn khóa để bảo vệ bản cũ; **bước 4 kiểm tra thật Prefab**, **bước 5 mở Scene**. Nếu bước 4 báo lỗi, gửi **nguyên văn lỗi** để xác định đúng component/field; không xóa hoặc ghi đè prefab khi chưa rõ nguyên nhân.

Chạy `git pull --ff-only origin feat/ref04-p2-canvas-il2cpp-runtime-trace`, `py -3 tools/ref04_p6_offline_ui_plan.py`, mở lại cửa sổ Unity. Mã còn cần máy người dùng xác nhận Editor thực tế; test tĩnh Python chỉ kiểm tra hợp đồng source.

---

## P6.1 Unity screenshot triage — nút Dựng bị khóa (2026-10-11)

Trong ảnh Unity thực tế, **3C source-study: có** và **Tight Sprite source manifest: có**. Nút **Dựng REF04 Study** mờ nhưng **Kiểm tra Prefab** và **Mở Scene** vẫn bật: theo source code, điều này xảy ra khi đã tồn tại ít nhất một trong hai asset `REF04-home-crew_NATIVE_BOUNDS_STUDY.prefab` hoặc `REF04-home-crew_NATIVE_BOUNDS_STUDY.unity`. **Không phải bằng chứng Unity đã biên dịch lỗi.**

Đã sửa cửa sổ P6:
- Đã có cả hai: hiển thị rõ `REF04 Study: đã có Prefab + Scene`, giữ Build khóa nhằm tránh overwrite. Nhấn **1. Kiểm tra dữ liệu P5/P6**, sau đó **4. Kiểm tra Prefab** và **5. Mở Scene** (chỉ nếu Audit PASS).
- Chỉ có một asset: hiển thị `REF04 Study CHƯA ĐẦY ĐỦ` kèm asset thiếu; khóa cả Build/Audit/Open. Không xóa hoặc ghi đè tự động.
- Chưa có gì và đủ 3C + manifest: mới bật Build; lệnh Build tự kiểm tra tồn tại thêm một lần ngay khi được bấm để chặn overwrite.
- Chưa xác minh SHA: trạng thái `Chưa kiểm tra dữ liệu P6 offline` là thông báo **chưa chạy bước 1**, không phải lỗi. Nếu bước 1 báo thiếu SHA dạng phẳng do báo cáo cũ, chạy lại `py -3 tools/ref04_p6_offline_ui_plan.py` để tạo file report mới.

Cập nhật mã rồi Unity sẽ tự nạp lại script. **Không chạy lại generator ảnh hoặc xóa Prefab/Scene khi chưa có kết quả bước 1/4**. Bộ test Python kiểm tra GUI source contract không thay thế biên dịch/test Unity Editor thực tế.

---

## P6.1 — Dựng bản REF04 Source Study trực tiếp trong Unity Editor (2026-10-11)

Bản P6 offline của bạn đã PASS trên XAPK thật: **265/265** Image/Sprite có geometry nguồn được xác minh; 299 Image tổng cộng (34 không có Sprite), 168 LayoutGroup field, 62 Text. Đó là bằng chứng các Sprite/Image và field nguồn, **không** phải xác nhận nguyên trạng Canvas hay Pixel-Perfect UI.

Đã thêm `unity-ui-viewer/Assets/Editor/Ref04P6OfflineWorkspace.cs` làm điểm thao tác duy nhất trong Unity Editor:

1. **Trước khi mở Unity:** chạy lệnh bên dưới để bổ sung hash dạng phẳng trong `output/ref04-p6-offline-ui-gaps.json` (Unity `JsonUtility` không đọc được dictionary). KHÔNG cần APK gốc khởi chạy, đăng nhập, ADB hoặc backend cũ.
   ```cmd
   git pull --ff-only origin feat/ref04-p2-canvas-il2cpp-runtime-trace
   py -3 -m unittest discover -s tests -p test_ref04_p6_offline_ui_plan.py -v
   py -3 -m unittest discover -s tests -p test_ref04_p6_unity_workspace_contract.py -v
   py -3 tools/ref04_p6_offline_ui_plan.py
   ```
2. **Chuẩn bị Tight Sprite preview** nếu chưa có `output/ref04-source-logical-sprite-previews/manifest.json`:
   ```cmd
   py -3 tools/prepare_ref04_native_bounds_preview.py
   ```
   Công cụ này chạy lại audit nguồn có liên quan và xuất *bản sao riêng* của các Sprite Tight đã được truy vết từ XAPK. Không được sửa hoặc gán giả dữ liệu còn thiếu.
3. Mở `unity-ui-viewer` bằng Unity Hub (repo hiện ghi nhận **Unity Editor 2022.3.21f1**). Trong Unity chọn:
   **Tools → HaiTac Offline UI Viewer → Source XAPK → P6 - REF04 offline Unity workspace**.
4. Cửa sổ có các bước **(1) Kiểm tra P5/P6 SHA** → **(2) Chuẩn bị 3C Study nếu còn thiếu** → **(3) Dựng REF04 Study** → **(4) Kiểm tra bản dựng** → **(5) Mở REF04 Study Scene**.
   - Không tự ghi đè bất cứ bản 3C study prefab/scene nào đã có, kể cả của 4 màn hình khác.
   - Bước dựng REF04 cần prefab 3C và manifest Tight Sprite từ XAPK. Nếu nguồn còn thiếu, thao tác dừng và hiển thị blocker; không đặt placeholder.
   - Bản mới đặt tại `Assets/LocalReconstruction/Ref04NativeBoundsStudyPrefabs/` và `.../Ref04NativeBoundsStudyScenes/`. Chúng là **bản làm việc private**, không phải asset gốc và không commit.
   - Bản dựng được kiểm tra 299 Image (265 Sprite + 34 Image không Sprite), Component PathID/owner/SHA và **503 RectTransform**, bao gồm sibling/anchor/size/position/scale của tất cả các child so với 3C study nguồn; **root preview được phân biệt riêng**.
5. Dùng **Scene View và Game View** trong Unity để quan sát và liệt kê chỗ cần tạo chức năng UI cho backend mới. Canvas/Camera 1600×900 ở bản Study hiện tại là **PREVIEW ONLY**, không phải tọa độ/scale gốc được phục dựng. **Không coi bản này là UI cuối hoặc pixel-perfect**.
6. Nếu đã có REF04 Native Bounds study, cửa sổ chỉ cho **Audit + Open** để bảo vệ dữ liệu hiện có; không tự build đè. Nếu thư mục 3C có bản khác nhưng thiếu REF04, sẽ từ chối chạy batch builder để không ghi đè 4 bản kia.

**Lưu ý khi triển khai:** Các điều khiển gameplay, button action, mô hình tài khoản/đội hình/kho đồ, call API qua backend của bạn là tầng `NEW_PROJECT_DESIGN` — chưa được thực hiện trong P6.1. Muốn đạt UI hoạt động thực tế, cần phát triển tầng controller/adapter riêng và test luồng chức năng. Hiện P6.1 tạo được quy trình dựng/kiểm toán Scene nguồn trên Unity, nhưng **chưa có bằng chứng Unity Editor PlayMode/build PASS trên máy người dùng**.

---


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
