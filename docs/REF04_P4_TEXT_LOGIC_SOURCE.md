# REF04 P4 — 62 Text: font, localization và nội dung động

## Phạm vi đã bổ sung ngày 2026-10-10

P4 nối **từng** `UnityEngine.UI.Text` của cây REF04 với dữ liệu XAPK nguồn, không tự dựng chữ, dịch thuật, hoặc logic HUD. Đây là bước forensic nguồn; **chưa khôi phục runtime text logic**.

Nguồn đọc theo thứ tự:
1. `output/ref04-p3-text-font-localization-source.json`: 62/62 serialized Text `m_Text` (SHA256, byte length, empty status), 62/62 `m_Font` (PPtr), original GameObject/RectTransform IDs, 52 component localization có cùng owner nếu tồn tại.
2. `output/ref04-p3-original-font-object-provenance.json`: xác thực **2 Font object nguồn**, kể cả external dependency (exact file-ID/path-ID/native Font type/raw SHA).
3. `output/ref04-p3-localizer-term-binary-probe.json`: 52 I2 localizers (26 `I2.Loc.Localize`, 26 `TextLocalizeChecker` theo chứng cứ CI trước), source hash/status của term khi xác minh được, hoặc blocker riêng khi chưa đọc được.

**Cặp `global-metadata.dat` + `libil2cpp.so` SHA256, serialized source file và object IDs phải nhất quán** trước khi lập bảng. Không lấy file thứ ba theo tên gần đúng.

## Công cụ mới

`tools/audit_ref04_p4_text_logic.py` tạo báo cáo private local:

```text
output/ref04-p4-62-text-logic-source-evidence.json
```

Các trường trong mỗi hàng:
- `textComponentPathId`, owner/RectTransform PathID (không ghép bằng tên node).
- `originalSourceTextSha256`, `originalSourceTextUtf8Bytes`, `originalTextState` cho **nội dung serialized lúc xuất XAPK**, không phải chữ hiện tại trong game.
- `originalFontPointer`, `originalFontRawObjectSha256`, `originalFontSourceKind` cho **Font object source**. `runtimeFontLoaded=null` vì chưa đo.
- `colocatedLocalizerComponentPathIds`, tình trạng probe term của chính localizer. *Same GameObject* chỉ là ứng viên: không chứng minh localizer gán chuỗi vào Text đó.
- `dynamicTextWriterStatus=UNKNOWN_NO_VERIFIED_RUNTIME_FIELD_WRITER`. Không gắn nhãn STATIC/DYNAMIC chỉ vì m_Text rỗng hay có m_Localize.
- `runtimeText=null`, `runtimeLocale=null`, `runtimeLocalizedBindingProven=false`.

Báo cáo tổng hợp số hàng đúng 62, mức bao phủ 2 Font gốc, 52 I2 components, số Text source empty/non-empty, số localizer đồng owner và tổng term có hai nguồn xác nhận. Trên XAPK từng kiểm tra trước đó **0 term được dual-verified**; không dự đoán rằng test mới sẽ nâng số này.

## Kiểm thử / CI

- `tests/test_ref04_p4_text_logic.py`: fixture tổng hợp đủ 62 Text/2 Font/52 localizers; bác bỏ sai SHA, nhầm Font type/PPtr, sai owner, forged key/term và trạng thái runtime giả.
- `.github/workflows/local-art-decode.yml`: chạy unit test trên Linux/Windows, sau bước xác minh P3 I2 + Font trên Linux chạy báo cáo P4 XAPK thật. Assert đầy đủ 62 Text, 2 Font source, 52 localizers và **0 runtime text writer proven**.

```powershell
py -3 -m unittest discover -s tests -p test_ref04_p4_text_logic.py -v
py -3 tools/audit_ref04_p4_text_logic.py
```

Lệnh audit cần báo cáo P3 ở `output/`, không hoạt động nếu thiếu XAPK/source reports. Raw game text và binary bị ignore, không đưa vào commit.

## Còn thiếu để hoàn tất P4 runtime

1. **Chọn font thật lúc render:** nguồn PPtr cho biết 2 Font native objects; thiếu device font loading, dynamic font fallback, glyph/material state, text mesh thực.
2. **Localization key/translation:** 52 I2 localizer chưa có liên kết key → Text được chứng minh. Cần hai nguồn thống nhất term/secondary term, bảng ngôn ngữ I2, selected language và fallback.
3. **Nội dung động:** phải xác minh method ownership từ IL2CPP metadata → code-registration/native method pointers → ARM64 full control flow và phép ghi vào `Text.text`, `Localize`, `TextLocalizeChecker`; nếu có thể thêm runtime instrumentation hợp pháp trên ứng dụng được phép thử.
4. **Layout đúng thực tế:** phụ thuộc Canvas/CanvasScaler/SafeAreaAdapter/PanelHome2 runtime từ P2/P3. 1600×900 preview không phải trạng thái runtime gốc.

**Trạng thái:** P4 source provenance mở rộng; `runtimeTextLogicRecovered=false`. Không thay Unity Scene/Prefab/Canvas/Text, artwork hoặc Spine. PR #7 tiếp tục Draft cho đến khi có CI của HEAD mới và bằng chứng runtime/acceptance phù hợp.
