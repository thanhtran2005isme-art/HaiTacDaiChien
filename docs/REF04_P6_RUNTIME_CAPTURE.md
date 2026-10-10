# REF04 P6 — đo ảnh giao diện runtime trên Android (source-bound; chưa có UI formula)

## Phạm vi hiện thực

P5 kiểm toán bản XAPK riêng tư đã PASS 10/10 unit test và kiểm kê 245 bản ghi nguồn, **không** phải bằng chứng tọa độ runtime. P6 còn đối chiếu trực tiếp SHA-256 của cặp `global-metadata.dat` / `libil2cpp.so` đọc lại từ XAPK với các SHA gốc trong P5; nếu không khớp sẽ dừng, không lấy ảnh của bản nguồn khác. P6 bổ sung quá trình chụp ảnh trên ứng dụng Android thật ở foreground, khóa SHA-256 ảnh PNG với cặp báo cáo P5/XAPK đang có trên cùng máy, rồi so pixel giữa bản gốc và bản dựng thử **trên đúng cùng một thiết bị và cùng kích thước ảnh**. Dữ liệu được giữ cục bộ trong thư mục Git-ignored `output/ref04-p6-runtime/`.

Tệp đã bổ sung:
- `tools/ref04_p6_runtime_capture.py` — CLI `capture` và `audit`; chỉ gọi ADB local, không thay đổi APK hoặc Unity.
- `tools/ref04_p6_runtime_evidence.py` — đối chiếu SHA, provenance filename, package foreground, cùng thiết bị, độ phân giải; dùng `compare_ref04_game_screenshots.py` đã có trong repo.
- `tests/test_ref04_p6_runtime_evidence.py` — fixture **giả lập chỉ để kiểm tra khả năng từ chối input sai**, không xác nhận đã chụp hoặc chạy game thật.

## Chuẩn bị Windows 10

Mở CMD tại thư mục root dự án và chạy:

```cmd
git pull --ff-only origin feat/ref04-p2-canvas-il2cpp-runtime-trace
py -3 -m pip install Pillow
py -3 -m unittest discover -s tests -p test_ref04_p6_runtime_evidence.py -v
adb devices
```

Nếu `adb` không có trong PATH, thay `adb` bằng `C:\platform-tools\adb.exe` và thêm `--adb C:\platform-tools\adb.exe` cho lệnh `capture`.

**Phải cài và mở đúng game XAPK gốc, điều hướng tới màn REF04 (home/crew).** Kiểm tra package thật thay vì suy đoán:

```cmd
adb shell pm list packages
adb shell dumpsys activity activities
```

Chọn tên package của game đúng từ máy. Chạy:

```cmd
py -3 tools/ref04_p6_runtime_capture.py capture --role original --package TEN_PACKAGE_GOC --capture-id original-ref04-01 --adb C:\platform-tools\adb.exe
```

Thay `TEN_PACKAGE_GOC` bằng Android application ID đầy đủ (ví dụ dạng `com.vendor.game`; **đây chỉ là minh họa, KHÔNG phải package đã xác minh**). Tool yêu cầu process đang chạy, activity/window xác nhận app ở foreground và `adb exec-out screencap -p` xuất ra PNG hợp lệ. Nếu có nhiều thiết bị, thêm `--serial <adb-device-id>`. Nếu có nhiều XAPK ở thư mục gốc, thêm `--xapk "đường-dẫn-XAPK-gốc.xapk"`. Không chấp nhận ghi đè capture-id; dùng tên mới cho lần chụp mới.

Kiểm toán 1 ảnh thật (mới chứng minh **ảnh PNG và liên kết SHA từ ADB**, chưa thể so giao diện với bản dựng):

```cmd
py -3 tools/ref04_p6_runtime_capture.py audit --original original-ref04-01
```

Nếu **đã có một bản Unity study chạy như một Android app thứ hai**, điều hướng nó tới cùng màn/trạng thái, cùng thiết bị; cung cấp package riêng của app nghiên cứu:

```cmd
py -3 tools/ref04_p6_runtime_capture.py capture --role study --package TEN_PACKAGE_UNITY_STUDY --capture-id study-ref04-01 --adb C:\platform-tools\adb.exe
py -3 tools/ref04_p6_runtime_capture.py audit --original original-ref04-01 --study study-ref04-01
```

Nếu bản Unity hiện chỉ chạy PC/Game View, **đừng dùng một ảnh PC gắn nhãn ADB**. Có thể so sơ bộ bằng `tools/compare_ref04_game_screenshots.py` nhưng không thể chứng minh cùng thiết bị / trạng thái runtime theo gate P6 Android. Việc tạo bản Unity Android riêng, nếu cần, là bước độc lập và phải giữ nguyên scene/prefab/art gốc.

## Đọc kết quả đúng

- `referenceScreenshotPngVerified=true`: ảnh PNG local khớp SHA ghi trong manifest; app từng được ADB quan sát foreground tại lúc chụp. **Không có device attestation mật mã**; metadata có thể bị người dùng sửa.
- `visualComparison`: chỉ xuất hiện nếu có đủ hai capture hợp lệ; báo `rgbAbsoluteMeanError`, tỷ lệ pixel có sai khác độ sáng, và các ảnh khác biệt/overlay, **không co giãn hoặc crop**.
- `originalXapkInstalledBytesProven=false`: SHA của cặp binary IL2CPP trong XAPK local khớp P5, nhưng **chưa đối chiếu** các split APK đang cài trên điện thoại với các split bên trong XAPK. Không được coi package name là bằng chứng mã nhị phân đang chạy.
- `sameRef04SceneStateIndependentlyProven=false`: tên trạng thái REF04 do người chụp khai báo; screenshot không chứng minh scene, sprite, gameplay state, camera và localization giống nhau.
- `runtimeCanvasScalerFormulaProven=false`, `runtimeSafeAreaAdapterFormulaProven=false`, `panelHome2RuntimeMutationsProven=false`, `dynamicTextUpdatesProven=false`: chưa có đo lường Canvas/Screen.safeArea hoặc mã native/trace động đủ chứng cứ.
- `runtimeCoordinates=null`, `pixelPerfectUiProven=false`, `unityImportAllowed=false`: tuyệt đối không lấy ảnh/số liệu đã đo để suy ra tọa độ hay cập nhật Unity asset tự động.

## Đầu việc P6 tiếp theo, còn BLOCKED

1. **XAPK↔installed-app identity**: trích manifest/split names/hashed APKs của bản gốc và APKs đang cài, phải chứng minh app chạy đúng bản XAPK; package foreground một mình chưa đủ.
2. **Original REF04 state reproducibility**: có nhiều capture cùng trạng thái theo thao tác có kiểm soát, tách vùng biến thiên Spine/animation/HUD/text để so ảnh có nghĩa; không thay hình nền hoặc tự căn chỉnh.
3. **Canvas/Screen.safeArea/PanelHome2**: có runtime tracing/telemetry bằng một phương pháp được phép và có bằng chứng phương pháp đo, sau đó đối chiếu với original device screenshots. Nguồn P2 hiện chỉ có method-name/native-address candidates, chưa chứng minh full CFG/field writes.
4. **Text động & I2 localization**: cần bằng chứng thay đổi trạng thái và liên kết tới Text source PathID; 52 localizers hiện **0 term source dual-verified**, không tự tạo nội dung.
5. **Pixel fidelity**: chỉ sau khi có capture reference và study từ cùng thiết bị/trạng thái, kiểm tra diff; ảnh bằng nhau ở một frame không chứng minh tất cả UI/logic.

**Không đưa file XAPK, PNG game, package dumps hoặc private JSON lên Git.** PR #7 tiếp tục Draft. P6 gate này là **bắt đầu thu bằng chứng runtime**, không phải chứng nhận đã khôi phục UI chính xác.
