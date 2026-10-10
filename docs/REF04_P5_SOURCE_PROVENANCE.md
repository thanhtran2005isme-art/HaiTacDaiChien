## P5 raw-object SHA triage (2026-10-11)

Some source-graph components carry an explicit null raw-object SHA because the native raw object could not be read. P5 retains those as `BLOCKED_RAW_OBJECT_SHA_UNAVAILABLE_IDENTITY_ONLY` after comparing exact original component PathID, owner and parent chain. Such records cannot prove any original serialized field values or native Canvas subset; attempting to do so fails the audit. P5 counts `P2ComponentsWithOriginalRawSha` separately from `P2IdentityOnlyComponentsWithoutRawSha`. This does not prove every P2 object's raw bytes or any runtime coordinates. LayoutGroup 168 field spans and 62 Text source fields continue to require full original SHA.

# REF04 P5 — Kiểm thử tổng hợp truy xuất nguồn từng giá trị

**Trạng thái:** Source-forensics gate, chưa chứng minh UI runtime/pixel-perfect. PR #7 Draft. Ngày 2026-10-10.

## Mục đích

Không chấp nhận một tuyên bố 'đã khôi phục tọa độ/giao diện chính xác' khi thiếu bằng chứng gốc. Mỗi giá trị *thuộc phạm vi đã kiểm kê* phải nối được tới đúng original XAPK SerializedFile/Component PathID, raw source SHA-256 và loại chứng cứ tương ứng; các giá trị runtime chưa đo phải là `null`/unproven, không được điền dự phòng.

**Phân biệt ba tầng:** (1) original serialized source ≠ (2) logic runtime ≠ (3) tọa độ pixel trên thiết bị. Bằng chứng tầng (1) không đủ để khẳng định (2)/(3).

## Công cụ và nguồn

`tools/audit_ref04_p5_source_integrity.py` chỉ đọc các báo cáo **gitignored local** từ đúng XAPK:

| Báo cáo | Điều kiện |
|---|---|
| `ref04-full-source-inventory.json` | 503 GameObject/RectTransform và 1.564 component; owner/ID/raw-object SHA |
| `ref04-step2-layout-canvas-text.json` | Canvas, CanvasScaler, SafeArea, Text đã được kiểm kê riêng |
| `ref04-layout-schema-forensics.json` | P1: 24 LayoutGroup, 168 serialized fields được hai backend chứng minh |
| `ref04-p2-canvas-il2cpp-runtime-source.json` | P2/P3: Canvas/Scaler/SafeArea/PanelHome2 và quan hệ parent nguồn |
| `ref04-p3-text-font-localization-source.json` | 62 Text, m_Text hash, Font PPtr, source localizers |
| `ref04-p3-original-font-object-provenance.json` | 2 Font source objects, kể cả external dependencies |
| `ref04-p3-localizer-term-binary-probe.json` | 52 I2 localizers; term chưa đủ chứng cứ thì BLOCKED |
| `ref04-p4-62-text-logic-source-evidence.json` | P4: 62 Text, Font, I2 method-declaration hints; không nhận là runtime writer |

**P5 không tin nhãn 'verified' tự khai báo.** Nó đối chiếu:
- SHA cặp `global-metadata.dat` / `libil2cpp.so` thống nhất giữa P1, P2, P4.
- P1 từng LayoutGroup component ID/owner/object SHA với original inventory; từng field-name/byte offset/byte length/field-byte SHA của **AssetStudio và AssetRipper phải trùng tuyệt đối**.
- P2 từng Component ID/owner/object SHA và chuỗi `RectTransform.m_Father` tái tính từ original graph; Canvas/Scaler fields phải khớp Step2. Có parent ngoài candidate thì giữ BLOCKED.
- P4 tái tạo báo cáo 62 Text từ nguồn P3 + Font + 52 localizer, đối chiếu lại canonical report và 62 original Text/Font source values qua Step2/original inventory, đồng thời kiểm tra source ID/hash của localizer.
- Chặn tọa độ, scale, SafeArea formula, Text runtime không có chứng cứ kể cả khi bị chèn sâu vào báo cáo.

Mỗi bản ghi ledger P1/P2/P4 chỉ đưa original ID/hash/offset/field-name, không đưa text/game strings, tọa độ hay nhị phân thương mại lên Git. **Báo cáo chi tiết chỉ ở thư mục `output/`.**

## Kết quả kỳ vọng và giới hạn

Bộ đối chiếu nghiêm ngặt ở REF04 hiện có phạm vi dự kiến **168 field serialized P1 + 15 component P2 + 62 Text P4 = 245 provenance entries**, với 2 Font objects/52 I2 localizer đã được P4 kiểm tra. Đây là **độ bao phủ của các hạng mục đã chọn**, không phải khôi phục mọi thành phần trong game. Phải kiểm tra thật trên XAPK mới xác nhận các số này ở HEAD mới.

Đầu ra `output/ref04-p5-cross-phase-source-integrity.json` giữ bắt buộc:

- `runtimeCoordinates=null`
- `runtimeCanvasScale=null`
- `runtimeSafeAreaFormula=null`
- `runtimeTextPositions=null`
- `assumedPreviewResolution1600x900=false`
- `pixelPerfectUiProven=false`
- `runtimeLayoutProven=false`
- `unityImportAllowed=false`
- `originalUiAssetsChanged=false`

**Source-provenance PASS không đồng nghĩa gameplay/Unity Editor render PASS.** Độ phân giải `1600×900` của web viewer chỉ là khung preview, tuyệt đối không thay thế bằng chứng runtime.

## Lệnh test

Từ repository đã có XAPK hợp pháp và các report P1–P4 mới cùng source:

```powershell
py -3 -m unittest discover -s tests -p test_ref04_p5_source_integrity.py -v
py -3 tools/audit_ref04_p5_source_integrity.py
```

CI `.github/workflows/local-art-decode.yml` chạy unit test P5 Windows và Linux; trên Linux bắt buộc chạy P5 **sau** tạo đủ các report P1–P4 từ XAPK thật, xác nhận các count/mức chứng cứ và không đổi source UI. Không chấp nhận chỉ PASS fixture unit tests.

## Còn thiếu

- Chứng cứ thực thi native method ownership/full ARM64 CFG của Canvas/SafeArea/PanelHome2 và cập nhật Text.
- State runtime thật (Screen.safeArea, CanvasScaler effective scale, Font/selected language, viewport và thiết bị).
- Unity Editor Console/Play Mode và ảnh đối chiếu ở thiết bị phù hợp.
- CI của **HEAD PR #7 mới nhất** và xác nhận của người dùng trước merge.

**Không merge, không tự tạo Scene/Prefab/Canvas/Text, không đoán tọa độ.**
