# REF04 — BƯỚC 2: Canvas, LayoutGroup, Text, SafeArea từ đúng XAPK

## P1 tiếp — đọc lại raw bytes/offsets LayoutGroup, không import Unity

- `tools/ref04_layout_raw_parser.py` là bộ đọc `struct` độc lập với hàm `UnityPy.read_typetree`, kiểm tra từng scalar/array/string, alignment 4-byte đúng cờ TypeTree, byte order của `SerializedFile`, ranh giới dữ liệu, full-object exact consumption và SHA-256 từng trường. **Không đoán offset**; chỉ ghi offset phát hiện bằng cách đi đúng thứ tự TypeTree đang được khảo sát.
- `tools/probe_ref04_layout_schema_forensics.py` thử đọc raw bytes XAPK cho **từng original LayoutGroup PathID** khi AssetStudio schema và strict parser khả dụng; so giá trị từng field với strict source record, đồng thời xác minh `m_GameObject`, `m_Script`, `m_Enabled` và raw object SHA. Ghi `rawByteReparseCounts`, `rawByteReparse.sourceByteSpans` trong local-only `output/ref04-layout-schema-forensics.json`. Trường hợp không đọc được ghi rõ `BLOCKED_*`.
- **Giới hạn không thay đổi:** raw-byte replay là một bộ đọc giá trị độc lập, nhưng vẫn sử dụng TypeTree **sinh từ AssetStudio**. Nó **không phải schema độc lập thứ hai** và không chứng minh phép căn chỉnh runtime. Kể cả trạng thái `RAW_BYTES_REPARSED_DERIVED_SCHEMA_REVIEW_ONLY`, 24 LayoutGroup / 168 field **vẫn không được tự đưa vào Unity**. Cần bằng chứng schema độc lập theo IL2CPP/serialized object hoặc nguồn hợp lệ khác trước khi bỏ BLOCKED.
- **CI thành công** trên code commit `33d879c0` — [GitHub Actions run 38044547014](https://github.com/thanhtran2005isme-art/HaiTacDaiChien/actions/runs/38044547014), gồm Linux/XAPK thật và Windows guards. Không công khai giá trị UI hoặc source raw bytes, không tạo/sửa asset.


## Bổ sung — chẩn đoán schema của 24 LayoutGroup (source-only)

- Script `tools/probe_ref04_layout_schema_forensics.py` đọc đúng các đối tượng LayoutGroup có `PathID`/SHA256 gốc từ XAPK, cùng cặp `libil2cpp.so`/`global-metadata.dat` và Unity version nguồn.
- Tạo TypeTree độc lập bằng **AssetStudio** và **AssetRipper**, ghép native MonoBehaviour header đã truy vết, báo SHA256 cấu trúc, số node, và điểm đầu tiên khác nhau trong hai schema nếu tìm thấy. Thử strict parse có xác nhận `m_GameObject/m_Script/m_Enabled`, raw-object hash và kích thước object; không chấp nhận parser đọc một phần.
- Phân biệt `SCHEMA_STRUCTURE_DIFF_NOT_FIELD_PROOF`, `SCHEMAS_IDENTICAL_NOT_FIELD_PROOF`, `BLOCKED_SCHEMA_COMPARISON` với **chứng cứ giá trị field thực**. Dù hai schema trùng và strict parse đồng thuận, trường hợp này vẫn được ghi `REVIEW_ONLY`; không tự import hoặc xác nhận runtime layout.
- Kết quả ghi **chỉ ở local/gitignored**: `output/ref04-layout-schema-forensics.json`, gồm chính xác 24 component/168 field bị chặn, 0 Unity assets sửa. Báo cáo được liên kết tới `output/ref04-step2-layout-canvas-text.json` với đối chiếu từng PathID/GO/RectTransform/SHA/class và giữ nguyên `canBeAppliedToUnity=false`.
- Lệnh sau khi đã chuẩn bị xong output phase 3B và single-backend-review từ XAPK: `py -3 tools/probe_ref04_layout_schema_forensics.py` rồi `py -3 tools/audit_ref04_step2_layout_canvas_text.py`.
- Đây là **công cụ điều tra nguyên nhân schema/strict parse**, không phải kết luận đã giải được 168 field. Nếu AssetRipper tiếp tục parse lỗi, cần sử dụng khác biệt TypeTree làm đầu mối để kiểm tra binary offsets và alignment độc lập; không chỉnh bố cục Unity theo suy đoán. Canvas runtime, Text localization, SafeArea, PanelHome2 vẫn chưa giải xong.


> **SOURCE-ONLY / NO-GUESS / READ-ONLY.** Phải đọc [docs/XAPK_SOURCE_ONLY_UI_RULES.md](XAPK_SOURCE_ONLY_UI_RULES.md) trước khi sửa. **Bước 2 chưa chứng minh runtime UI pixel-perfect hoặc đủ điều kiện dựng UI.** Không tự đặt Canvas, tọa độ, chiều rộng màn hình, văn bản, font hay anchor.

## 1. Dữ liệu gốc thuộc cây ứng viên REF04 (không phải toàn bộ runtime)

Đọc từ [bước 1](REF04_STEP1_SOURCE_UI_INVENTORY.md): 503 GameObject, 1.564 component. Bằng chứng được giữ theo `SerializedFile`, `GameObject/RectTransform/Component PathID`, `MonoScript m_Script`, byte SHA và trường đã verify.

| Nhóm trong SerializedFile XAPK | Số lượng |
|---|---:|
| Native Canvas | 1 |
| Managed CanvasScaler | 1 |
| HorizontalLayoutGroup | 18 |
| VerticalLayoutGroup | 6 |
| **Tổng LayoutGroup** | **24** |
| UI.Text | **62** |
| I2.Loc.Localize và TextLocalizeChecker | 52 |
| SafeAreaAdapter | 6 |
| UI controls (Button/Outline/Slider/Mask/ContentSizeFitter/LayoutElement) | 155 |

`tools/audit_ref04_step2_layout_canvas_text.py` đọc báo cáo đầy đủ của bước 1 và xuất riêng **`output/ref04-step2-layout-canvas-text.json` + `.md`** (gitignored): mỗi component có owner, original Source RectTransform/sibling index, class/type, trạng thái kiểm chứng, các field thật đã đọc được, khuyết điểm còn lại. Không tạo file Unity.

## 2. 24 LayoutGroup / 168 field — vẫn CẤM nhập

Nguồn AssetStudio từ XAPK đã đọc strict 24 LayoutGroup với 168 giá trị, nhưng decoder AssetRipper bị `STRICT_PARSE_ValueError` trên chính các source object đó. Không phải dữ liệu trực tiếp từ một backend là đủ.

Đã thêm `tools/probe_ref04_layout_third_backend.py` để thử backend độc lập thứ ba `AssetsTools` với đúng bộ `libil2cpp.so + global-metadata.dat`, Unity version XAPK, `Component PathID`, SHA256 object và `check_read=True`. Kết quả từ CI/XAPK thật: **24/24 `BLOCKED_NODE_GENERATION_AssertionError`**. Không có giá trị nào được tự động promote/import. Chi tiết: `output/ref04-layout-third-backend-evidence.json` (private).

**Việc phải giải mã tiếp**: xem TypeTree generator không hỗ trợ LayoutGroup ở backend nào, kiểm tra định dạng đối tượng serialized, alignment/offset native từ IL2CPP để có đối chứng độc lập theo byte; nếu thất bại, báo người dùng field cụ thể chứ **không** lấy số Unity mặc định.

## 3. 62 Text / chuỗi / font

Đã thêm `tools/probe_ref04_text_source_binary.py`: kiểm tra tất cả 62 original UI.Text MonoBehaviour qua source `MonoScript`, raw object SHA256, `m_GameObject`, `m_Script`, `m_Enabled`, strict `read_typetree(check_read=True)`. Chỉ nhận field thực có trong object; chuỗi nguồn không xuất nguyên văn vào GitHub hoặc console mà chỉ lưu hash SHA256 và byte count **trong báo cáo local-only**. Font ghi original PPtr FileID/PathID, không chọn font khác. Giá trị có thể có: `m_Text`, `m_Font`, `m_FontSize`, `m_FontStyle`, `m_Alignment`, `m_LineSpacing`, `m_Color`, `m_RaycastTarget`.

**Kết quả trên XAPK thật:** cả **62/62** `UnityEngine.UI.Text` đã được parse strict đầy đủ bởi **AssetStudio và AssetRipper**, các source target fields đã có trong serialized object **trùng nhau giữa hai decoder**: `TWO_BACKENDS_SAME_SERIALIZED_TEXT_FIELDS_NOT_IMPORTED` (CI [38040389041](https://github.com/thanhtran2005isme-art/HaiTacDaiChien/actions/runs/38040389041)). Giá trị có chứng cứ được ghi riêng vào `textSourceFieldsTwoBackendsAgreed`; field không có trong dữ liệu được đánh dấu `textFieldsNotDoubleVerified`. **Không** tự tạo hoặc áp chuỗi/font. Dữ liệu serialized Text vẫn **không chứng minh câu chữ/phông hiển thị trong runtime** (I2 Localization, dữ liệu game, ngôn ngữ, animation, script).

File private: `output/ref04-text-source-binary-evidence.json`.

## 4. Canvas, CanvasScaler, SafeArea

- Native Canvas: chỉ giữ `m_Enabled`, `m_RenderMode`, `m_SortingOrder`, `m_OverrideSorting`, `m_TargetDisplay`, `m_PixelPerfect` **nếu đã trích trực tiếp**; field thiếu = không chứng minh.
- CanvasScaler: giá trị managed chỉ lấy từ kế hoạch **3C hai backend đồng thuận**, 6 source field đã chứng minh. Không xem source snapshot là viewport khi chạy.
- 6 SafeAreaAdapter và `PanelHome2*` đã định danh MonoScript; **chưa có bằng chứng phép tính runtime** cho insets, screen size hoặc vị trí HUD. Những giá trị này phải tiếp tục giải mã IL2CPP (bước 3), không dùng 1600×900/scale=1.
- Text/localization: 52 component ngôn ngữ và dữ liệu live chưa chứng minh. Không tự tạo chữ hay icon để lấp chỗ trống.

## 5. Lệnh xem bản báo cáo trên máy local (không sửa Unity)

Chỉ chạy khi đã có các report source bước 1 và source pair IL2CPP từ XAPK gốc:

```cmd
cd /d C:\Users\Admin\Videos\HaiTacDaiChien
git switch feat/xapk-il2cpp-ui-field-provenance
git pull --ff-only origin feat/xapk-il2cpp-ui-field-provenance
py -3 tools\probe_ref04_layout_third_backend.py
py -3 tools\probe_ref04_text_source_binary.py
py -3 tools\audit_ref04_step2_layout_canvas_text.py
```

Các probe có thể trả `BLOCKED`; đây **không phải** lệnh sửa UI và **không chứng minh giao diện gốc đã phục dựng**. Nếu bị thiếu nguồn/TypeTree phải báo thiếu và tiếp tục giải mã từ XAPK, không thay bằng giá trị giả.

## 6. Tiêu chí kết thúc

- **Đã** xác định source PathID và nguồn chứng cứ cho toàn bộ component bố cục/Text/Canvas trong candidate.
- **Đã** kiểm chứng hai backend đồng nhất cho các field nguồn của **62/62 Text** (không phải text runtime).
- **Chưa** phục hồi 168 LayoutGroup fields bằng decoder thứ hai; chưa chứng minh final runtime Text/localization + SafeArea/Canvas ancestor; không claim 100% UI.
- Bước tiếp theo: tìm bằng chứng independent cho LayoutGroup và giải mã đúng logic IL2CPP tính HUD/SafeArea; chỉ dựng UI sau khi field có giá trị gốc được chứng minh.
- Không làm nhân vật/Spine ở giai đoạn này.
