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
