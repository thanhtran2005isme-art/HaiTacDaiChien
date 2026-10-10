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

## Kết quả xác minh trên XAPK thật (Linux và Windows)

Bộ quét đã kiểm tra 5 cây với **5.346 component** và truy nguyên
**2.320/2.320 MonoScript liên kết xuyên file** bằng duy nhất 2
AssetBundle chứa nguồn MonoScript phù hợp. Không còn liên kết
MonoScript ambiguous trong phạm vi này.

| Phân loại | Component |
|---|---:|
| Native fields | 1.674 |
| UI MonoBehaviour đã xác định class nhưng thiếu managed typetree | 1.201 |
| Script/component khác | 2.471 |
| **Tổng** | **5.346** |

- Đã xác nhận `global-metadata.dat` với IL2CPP **v31** từ **APK lồng
  trong XAPK**, kiểm tra magic/version và dấu vết tên field.
- **0 managed UI field values** có thể đọc được bằng UnityPy typetree
  trong 1.201 component UI; đây là giới hạn kỹ thuật còn lại,
  **không phải bằng chứng XAPK không lưu dữ liệu**.
- Các class được định danh bằng file-alias + PathID cùng nguồn; không
  suy diễn thành Image Type, kích thước Mask, CanvasScaler resolution,
  LayoutGroup spacing hay chữ/đội hình của game.
- Mọi ID chi tiết được xuất vào `output/` tại máy chạy, không push Git.

**Chưa có kết quả sửa bố cục REF04 từ phase này.** Để có giá trị trường
managed phải bổ sung decoder binary theo schema **xác minh của đúng build**,
rồi kiểm tra nhiều mẫu serialized trước khi đưa vào Unity.

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


## Giai đoạn 3B — Kiểm tra TypeTree binary từ đúng XAPK (opt-in)

**Trạng thái:** đã có mã và regression tests; **chưa có bằng chứng khôi phục bất kỳ managed field nào từ XAPK thật tại HEAD PR**. Giai đoạn 3A chạy mặc định vẫn giữ số liệu 0 field.

Vấn đề kỹ thuật: IL2CPP metadata **v31 là phiên bản metadata runtime**, không phải phiên bản TypeTree hay bảng serialized field offsets. Offset trường trong bộ nhớ đối tượng IL2CPP **không tự động** là offset trong Unity SerializedFile. Không áp dụng tìm chuỗi, heuristic byte pattern, hay căn chỉnh tọa độ theo ảnh.

Đường giải mã có điều kiện:

1. Từ XAPK gốc hợp lệ, đọc duy nhất `global-metadata.dat` (magic + v31) và `libil2cpp.so` (ELF) trong các APK lồng; tính SHA256 và không lưu/export nhị phân.
2. Lấy đúng `unity_version` từ năm SerializedFile chứa cây UI (không sử dụng version của Unity Editor local). Nếu bị strip về `0.0.0`, có nhiều version mâu thuẫn hoặc thiếu library thì **BLOCKED**.
3. Trên máy riêng đã cài `UnityPy` và gói **tùy chọn** `TypeTreeGeneratorAPI`, tạo TypeTree từ cặp binary nguồn và phiên bản Unity chính xác. Không tải TypeTree bên ngoài dựa trên tên class đơn lẻ.
4. Chỉ giải mã các MonoBehaviour đã liên kết `Component PathID → GameObject → MonoScript PPtr → class + assembly` thực tế. `read_typetree(nodes=..., check_read=True)` phải tiêu thụ đúng kích thước object và đồng nhất `m_GameObject`, `m_Script`, `m_Enabled` với nguồn đã kiểm chứng.
5. Nếu giải mã thành công, chỉ xuất các trường UI allowlist có kiểu/giá trị hợp lệ, cùng hash nguồn + hash serialized object, trạng thái `GENERATED_TYPETREE_SOURCE_VERIFIED`. Bất kỳ bước nào lỗi giữ `NO_MANAGED_TYPETREE`/UNKNOWN và thông tin BLOCKED, không tạo giá trị fallback.

Chạy từ thư mục root (sau khi hoàn tất bước kiểm kê XAPK):

```cmd
py -3 -m pip install UnityPy Pillow TypeTreeGeneratorAPI
py -3 tools\audit_original_unity_graph.py
py -3 tools\decode_xapk_ui_provenance.py --recover-managed-fields
type output\deep-ui-source-evidence.md
```

`--recover-managed-fields` không bật trong CI thật mặc định: CI kiểm tra thuật toán fail-closed bằng dữ liệu tổng hợp, còn việc tạo TypeTree bằng parser tùy chọn có thể không hỗ trợ IL2CPP v31/build này. Báo cáo nằm trong `output/` bị ignore. Không tạo hay sửa `Assets/LocalReconstruction` trong bước này, và **không tự đưa fields vào Prefab Unity** trước khi xem kết quả có chứng cứ, test Editor và duyệt thay đổi importer riêng.

**Các trường cần kiểm chứng tiếp:** Image.Type/Color/Fill/PreserveAspect; CanvasScaler scale mode/reference resolution; Mask/RectMask2D; LayoutGroup spacing, padding, child-alignment, grid constraints. Giá trị chưa lấy được phải ghi UNKNOWN. Kết quả binary phase 3B không đồng nghĩa UI REF04/Spine/game state đã được khôi phục.


### Phân tích lỗi AssertionError tại Windows (2026-10-10)

Máy người dùng chạy opt-in: `attempted=1201`, `blocked=1201`, cùng một lỗi `Generated TypeTree failed strict object parsing: AssertionError`. Lỗi này **chưa đủ chi tiết để phân biệt** generator không tạo được nodes, root MonoBehaviour thiếu header, hay strict parser thất bại.

Bản chẩn đoán mới phân loại:

- `NODE_GENERATION_AssertionError` (phase `generate_nodes`): `generator.get_nodes_up` ném exception trước khi có TypeTree.
- `INCOMPLETE_GENERATED_ROOT` (phase `validate_root`): TypeTree root không phải `MonoBehaviour` level 0 hoặc thiếu `m_GameObject`, `m_Script`, `m_Enabled`; **không đọc** schema thiếu root như layout hợp lệ.
- `STRICT_PARSE_AssertionError` (phase `strict_parse`): TypeTree có header, nhưng parser thất bại khi `check_read=True` và phải giữ BLOCKED.

Báo cáo lưu thống kê theo `phase/code`, một mẫu lỗi mỗi loại (file Python/function, root shape) tại `output/deep-ui-source-evidence.md`; JSON ghi `binaryRecoveryPhase`, `binaryRecoveryCode`, `binaryRecoveryFrame` và `binaryRecoveryTreeShape`, không ghi byte nội dung. Sau khi `git pull` chạy lại:

```cmd
py -3 tools\decode_xapk_ui_provenance.py --recover-managed-fields
type output\deep-ui-source-evidence.md
```

Trước khi nhận kết quả đã xác minh, tuyệt đối không sửa Prefab. Hướng kiểm tra bug root được tham khảo UnityPy issue #340, nhưng không coi đây là nguyên nhân được chứng minh cho XAPK này.


## Kết quả giai đoạn 3B trên XAPK thật — 2026-10-10

Lỗi được khoanh vùng bằng báo cáo `NODE_GENERATION_AssertionError`: backend `AssetsTools` của `TypeTreeGeneratorAPI 0.0.10` ném `Sequence contains no matching element` khi tạo TypeTree, trước bước đọc bytes. Điều này không chứng minh dữ liệu serialized thiếu.

Hai backend `AssetStudio`, `AssetRipper` cùng cặp binary XAPK SHA256 có thể tạo TypeTree cho Image, CanvasScaler, Mask, Horizontal/VerticalLayoutGroup và ContentSizeFitter. Tuy nhiên TypeTree được sinh kèm **native MonoBehaviour header không khớp m_Script** trên bản Unity 2022.3.51f1. Để không đoán field offsets, công cụ có lựa chọn `--unity-native-header`: lấy native MonoBehaviour root từ **TypeTree UnityPy đúng version của SerializedFile**, giữ managed children được sinh từ IL2CPP, đọc nguyên object bằng `check_read=True`, kiểm tra lại `m_GameObject`, `m_Script`, `m_Enabled` và type/range từng field.

Trên source thật trước bước cross-backend gate:

| Backend (cùng native header nguồn) | Component qua strict check | Target field values |
|---|---:|---:|
| AssetStudio | 1.201 / 1.201 | 8.102 |
| AssetRipper | 1.108 / 1.201 | 7.451 |
| AssetRipper bị chặn | 93 | 0 |

**Cổng cross-backend thực tế đã PASS:** so sánh 5.346 source component và 2.320 MonoScript links từ cùng game/XAPK; **1.108 component, 7.451 giá trị field khớp tuyệt đối** giữa AssetStudio và AssetRipper, không có khác biệt phát hiện được. **93 component, 651 field values chỉ AssetStudio đọc được** (các Horizontal/VerticalLayoutGroup); không tuyên bố 651 values có kiểm chứng chéo. Workflow [run 37966647257](https://github.com/thanhtran2005isme-art/HaiTacDaiChien/actions/runs/37966647257) Linux/Windows PASS, bước so trùng binary source/fields là gate (không có continue-on-error).

**Lưu ý:** `AssetRipper` còn 93 LayoutGroup lỗi strict parser; không được tự bỏ điều kiện để đồng nhất số lượng. `AssetStudio` đọc được các trường LayoutGroup còn thiếu của AssetRipper nhưng vẫn cần Unity Editor visual verification trước khi đưa vào Prefab.

Quy trình kiểm chứng chéo bắt buộc trong CI: `tools/compare_managed_ui_backends.py` so đúng **source identity, GameObject, MonoScript PPtr, Unity version, SHA256 của cặp IL2CPP binary**, rồi đối chiếu giá trị serialized đã qua strict check bằng **hai backend riêng**. Nếu bất kỳ field chung nào lệch, CI FAIL; những field chỉ AssetStudio đọc được tiếp tục mang bằng chứng `single-backend`, không được ghi nhãn được hai bộ đọc xác minh.

Chạy ở máy cục bộ (tạo report riêng trong `output/`, không sửa prefabs):

```cmd
py -3 tools\audit_original_unity_graph.py
py -3 tools\decode_xapk_ui_provenance.py --recover-managed-fields --binary-backend AssetStudio --unity-native-header
type output\deep-ui-source-evidence.md
```

**Không tự động đưa dữ liệu vào Prefab.** Bước sau cần chuyển các fields đã so chéo từ private `output/` sang importer một cách có kiểm soát, kiểm tra màn hình thật/Editor riêng, xác minh Image mask/raycast/sorting và gỡ các fallback tạm. Vẫn chưa chứng minh được editor Prefab gốc hoặc runtime screen 100%.


## Giai đoạn 3C — Đưa duy nhất giá trị cross-verified vào Prefab nghiên cứu

**Giới hạn:** Năm file `SourceGraphPrefabs` giữ nguyên. Importer mới **không khôi phục được Editor Prefab gốc** mà tạo 5 bản sao `VerifiedManagedFieldPrefabs/*_VERIFIED_FIELDS_STUDY.prefab` cùng 5 Scene nghiên cứu riêng; chỉ gắn component có đúng MonoBehaviour PPtr, owner GameObject và RectTransform PathID. Canvas/CanvasRenderer chỉ được tạo nếu loại native tương ứng có trong source graph; các thuộc tính chưa đọc hoặc mặc định do Unity tự khởi tạo không phải nguồn được xác minh.

**Nguyên tắc áp dụng:** chỉ 1.108 component / 7.451 field values **trùng tuyệt đối** qua AssetStudio và AssetRipper. Gồm Image (1.052), CanvasScaler (4), Mask (41), ContentSizeFitter (11). 93 Horizontal/VerticalLayoutGroup / 651 values chỉ AssetStudio đọc được bị loại khỏi plan. Sprite, Canvas native settings, raycast target, material, Spine, runtime animation và những fields chưa xác minh cũng **không được đoán**.

Trên Windows CMD ở thư mục repository (file `output/` chỉ nằm máy cục bộ):

```cmd
py -3 -m pip install UnityPy Pillow TypeTreeGeneratorAPI
py -3 tools\audit_original_unity_graph.py
py -3 tools\decode_xapk_ui_provenance.py --recover-managed-fields --binary-backend AssetStudio --unity-native-header
copy /Y output\deep-ui-source-evidence.json output\phase3b-assetstudio.json
py -3 tools\decode_xapk_ui_provenance.py --recover-managed-fields --binary-backend AssetRipper --unity-native-header
py -3 tools\compare_managed_ui_backends.py output\phase3b-assetstudio.json output\deep-ui-source-evidence.json
py -3 tools\build_verified_ui_prefab_plan.py
```

Chỉ khi `build_verified_ui_prefab_plan.py` hoàn tất mới mở **Unity Hub** vào `unity-ui-viewer/`. Ở Unity Editor chọn lần lượt:

1. `Tools > HaiTac Offline UI Viewer > Source XAPK > Build evidence-only serialized graph prefabs`
2. `Tools > HaiTac Offline UI Viewer > Source XAPK > Apply cross-verified fields to study prefab copies`
3. `Tools > HaiTac Offline UI Viewer > Source XAPK > Audit 5 verified field study prefabs`

Sau đó mở các Scene trong `Assets/LocalReconstruction/VerifiedManagedFieldScenes/` để kiểm tra trực tiếp component và Inspector. Trên từng màn hình cần xác minh quan sát được: Canvas có đúng render mode + reference size; sprite có đúng componentID và nội dung nguồn; Image Type/Sliced, Fill, Mask stencil, raycast; ContentSizeFitter; lớp phụ thuộc Unity tự tạo; tỷ lệ zoom và Game View; mọi lỗi Console và Play Mode. **Chưa được kết luận giống runtime gốc khi thiếu ảnh tham chiếu/trạng thái runtime**. Prefab nghiên cứu có thể chưa render đúng do native Canvas, kích thước root hoặc assets chưa được tái lập — không sửa bằng cách bịa giá trị mặc định. Các đặc tính này phải qua luồng kiểm chứng nguồn riêng.

**Bảo vệ thao tác:** Script Python xuất `output/verified-ui-prefab-plan.json` private, buộc khớp SHA-256 graph, hash binary, 5 scene, đủ 1.108 object / 7.451 field, đúng 4 class cho phép, so `m_GameObject`, `m_Script`, `rawObjectSha256` và status strict đọc đủ byte. Unity Editor preflight kiểm tra class, Enum/type/range, OriginalSerializedEvidence và từng component pathID; nếu đã tồn tại study Prefab sẽ không ghi đè. Khi xảy ra lỗi trong quá trình tạo, chỉ các study outputs mới được dọn; source graph assets không đổi. Sau khi tạo có menu Audit đọc lại giá trị serialized từ chính Prefab copy.

**Lưu ý phiên bản:** Unity source là `2022.3.51f1`, dự án viewer tracked trong `ProjectVersion.txt` là `2022.3.21f1`. Để so giao diện chính xác cần mở bản sao viewer với Unity 2022.3.51f1 và ghi rõ version đã kiểm chứng, tránh mặc định coi hai patch là tương đương. CI chạy Python/contract tests, **không mở Unity Editor, không chứng thực Play Mode hay ảnh dựng**.


## Giai đoạn 3D — Bản xem trước mới có Sprite, giữ nguyên 7.451 giá trị 3C

**Không dùng lại** Scene cũ trong `LocalReconstruction/Scenes` làm kết quả 3D. Công cụ tạo 5 file **mới** dưới `Assets/LocalReconstruction/VerifiedVisualScenes/` và Prefab cùng tên trong `VerifiedVisualPrefabs/`. 5 SourceGraphPrefabs, 5 VerifiedManagedFieldPrefabs và 5 Scene Canvas cũ không bị ghi đè.

Nguồn đầu vào: `output/verified-ui-prefab-plan.json` đã PASS 3C; `output/original-unity-graph.json`; `output/unity-prefab-map.json` (963 Image→Sprite đúng PathID); `output/local-ui-art/manifest.json`; và các Sprite đã được Unity import tại `Assets/LocalReconstruction/Sprites/`. Tất cả các JSON/tài nguyên này chỉ có trên máy địa phương và bị .gitignore.

Trên CMD trong thư mục repository (đã hoàn tất các lệnh Phase 3C):

```cmd
py -3 tools\build_unity_prefab_manifest.py
py -3 tools\build_verified_visual_plan.py
```

Kết quả đạt yêu cầu: `VERIFIED_3D_PREVIEW_PLAN_READY`, `sceneCount=5`, `exactSourceSpriteBindings=963`, `singleBackendValuesImported=0`.

Trong Unity Editor, mở đúng `unity-ui-viewer/` bằng quyền người dùng bình thường, **không dùng Run as administrator**. Trước đó phải tạo 5 verified 3C study Prefabs bằng menu Phase 3C, và đã từng chạy `Reconstruct 5 local Canvas prefabs` để import Sprite source PNG. Sau đó:

1. `Tools > HaiTac Offline UI Viewer > Source XAPK > Build 5 exact-source 3D visual previews`
2. `Tools > HaiTac Offline UI Viewer > Source XAPK > Audit 5 exact-source 3D visual previews`
3. Mở **`Assets/LocalReconstruction/VerifiedVisualScenes/REF04-home-crew_VERIFIED_VISUAL_PREVIEW.unity`** hoặc 4 Scene mới cùng loại và chụp **Game View + Hierarchy + Console**.

Bản mới có wrapper Canvas `ScreenSpaceOverlay`, Camera `3D PREVIEW CAMERA - NOT ORIGINAL`, kích thước thử nghiệm 1600×900 và có thể normalize scale **chỉ trên đối tượng copy hiển thị**. Các cài đặt này là tiện ích xem trước, không phải bằng chứng runtime nguyên bản. Tất cả 963 Sprite bindings đều được kiểm chứng theo tuple `(sceneId, RectTransform PathID, Image Component PathID)` với source graph và 3C đúng GameObject. Không tự gán theo tên, Sprite GUID đoán, hoặc tạo/điều chỉnh ảnh game.

Menu **Audit 3D** so từng property có trong 7.451 field values từ 3C source Prefab với component của Prefab 3D, kiểm tra `m_Enabled` và 963 Sprite asset references. Nếu một field bị Unity thay đổi khi gắn Sprite, audit sẽ FAIL (không tuyên bố 7451 được bảo toàn). Các component không có chứng cứ chéo (93 LayoutGroup / 651 values), native Canvas chưa giải mã, Spine/animation và tính năng game vẫn **chưa phục hồi**. GUI Play Mode và mức độ giống UI gốc vẫn phải kiểm chứng riêng bằng ảnh/trạng thái runtime; Scene 3D không phải original editable Prefab.

**Quan trọng:** Công cụ không ghi đè bản 3D nếu đã tồn tại; để dựng lại cần chủ động sao lưu/di chuyển các bản xem trước hiện có, không xóa bản nguồn gốc.


## Giai đoạn 3E — Thử khung hiển thị bằng chính gốc Canvas (không dựng Canvas lồng nhau)

**Chẩn đoán trên XAPK thật:** `tools/report_source_visual_layout_gaps.py` đã kiểm tra năm candidate root. `REF01-ship-upgrade`, `REF03-islands-map-A`, `REF03-islands-map-B`, `REF04-home-crew` đều có `m_SizeDelta=(0,0)` và `m_LocalScale=(0,0,0)`; `REF02-hero-detail` có `m_SizeDelta=(0,0)`, scale=(1,1,1) nhưng anchor stretch. Đây không phải bằng chứng viewport runtime gốc. Vì vậy, cả cách lồng Canvas 1600x900 ở 3D và cách chỉ đặt root scale=1 đều không thể tự chứng minh bố cục gốc.

**Thử nghiệm 3E an toàn, không sửa dữ liệu nguồn:** Menu Unity `Tools > HaiTac Offline UI Viewer > Source XAPK > Build 5 source-root Canvas viewport studies` tạo **5 Prefab + Scene hoàn toàn mới** trong:

- `Assets/LocalReconstruction/RootCanvasViewportPrefabs/`
- `Assets/LocalReconstruction/RootCanvasViewportScenes/`

Công cụ tạo **Canvas ScreenSpaceOverlay trực tiếp trên root** của Prefab 3C được sao chép và unpack, **không thêm một Canvas cha thứ hai**. Các thay đổi `root.anchorMin/Max=(0.5,0.5)`, `pivot=(0.5,0.5)`, `anchoredPosition=(0,0)`, `sizeDelta=(1600,900)`, scale=(1,1,1) là các thông số **chỉ để xem trước**, không nhận là runtime source. Nếu gốc đã có `CanvasScaler` được xác minh, không thay giá trị của nó; chỉ thêm scaler tạm nếu chưa có.

Đồng thời, 963 liên kết Image/Sprite theo PathID được dùng lại từ plan 3D. Menu `Audit 5 source-root Canvas viewport studies` đối chiếu **7.451 source field**, **963 Sprite pointer**, cộng thêm **toàn bộ RectTransform con** với 3C source prefab (anchor, pivot, size, anchoredPosition, scale, quaternion, sibling order, activeSelf). Chỉ RectTransform root của bản xem trước được phép chuẩn hóa. Tất cả đầu ra cũ từ main, 3C và 3D đều giữ nguyên; đã tồn tại bản 3E thì chặn ghi đè.

**Chạy trên máy:** `git pull --ff-only origin feat/xapk-il2cpp-ui-field-provenance`, đóng/mở lại Unity Editor bằng quyền thường và chọn Build rồi Audit của **source-root Canvas viewport studies**. Mở `RootCanvasViewportScenes/REF04-home-crew_SOURCE_ROOT_CANVAS_PREVIEW.unity` trong Unity. Chọn tab **Game**, đặt 16:9, chụp Game View + Hierarchy + Console; so trực tiếp với bản `VerifiedVisualScenes` đang lệch. Chạy `py -3 tools/report_source_visual_layout_gaps.py` để xem báo cáo private `output/source-visual-layout-gaps.json` nếu cần.

**Chưa được nhận là giao diện game hoàn chỉnh:** 651 giá trị của 93 LayoutGroup chưa được kiểm tra chéo; Text, Spine, animation, giao diện stateful và khung hiển thị runtime chưa được phục hồi đầy đủ. Bản 3E là phép thử cơ chế viewport có kiểm toán chứng cứ, không phải fix thủ công theo ảnh hoặc bản gốc có thể chỉnh sửa.
