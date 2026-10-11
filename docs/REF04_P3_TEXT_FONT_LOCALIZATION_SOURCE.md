# REF04 P3 — Text, font và localization từ XAPK

## P3 — Bằng chứng nguồn Text và Font dependency gốc (10/10/2026)

- [CI source #38053733211](https://github.com/thanhtran2005isme-art/HaiTacDaiChien/actions/runs/38053733211): 62/62 Text, 62/62 `m_Text`, 62/62 `m_Font`, 52 localizers, **2 con trỏ Font riêng biệt**. Trong Step2 **0/52 localizers được hai backend xác minh field**; vì vậy không được tự suy ra Term/translation/locale.
- [CI Font #38054361981](https://github.com/thanhtran2005isme-art/HaiTacDaiChien/actions/runs/38054361981): cả **2 con trỏ Font** là tham chiếu external SerializedFile, được UnityPy dereference theo đúng bảng `externals` nguồn; target file name + `PathID` + type `Font` + raw SHA-256 đều khớp; **2/2 external Font object identity (by filename/source pointer) resolved**, nhưng không chứng minh đúng font đã kích hoạt hay chữ render trên thiết bị.
- `tools/probe_ref04_p3_original_font_objects.py` không đoán Font fallback; fileId=0 chỉ so local raw object, fileId khác 0 phải qua original external record. Cùng tên file chỉ là bằng chứng tên theo UnityPy, không thay thế xác minh loaded device font.
- P3 còn `runtimeLanguageChosen=null`, `runtimeTextAndLocalizationProven=false`, `runtimeFontRenderingProven=false`. Chưa có key/database I2 và language fallback/field assignment đã chứng minh.


## Kết quả kiểm thử XAPK gốc và trạng thái P3

[GitHub Actions #38051186270](https://github.com/thanhtran2005isme-art/HaiTacDaiChien/actions/runs/38051186270) xác nhận **62/62 original Text components**, **62/62 `m_Text` source fields**, **62/62 `m_Font` source pointers**, **52 original localization components** và **2 con trỏ Font khác nhau**. Đây là đếm các con trỏ nguồn, **chưa xác minh 2 Font asset đã được tải hoặc render**. Không công bố chuỗi game nguyên bản.

P3 hiện phân biệt `sourceTextFieldsVerified` với `runtimeTextAndLocalizationProven`: thiếu bất kỳ `m_Text` hay `m_Font` thì đánh dấu riêng `BLOCKED` và giảm số đếm field, không bù mặc định. Các `m_Term/m_SecondaryTerm` của localizer chỉ được liệt kê tên field khi source đã được hai backend xác minh; **cùng GameObject không đủ chứng minh Text nhận translation term**. Tiếng/locale đang chọn, fallback, text động, metric font và vị trí runtime vẫn không xác minh được.


## Phạm vi

P3 kiểm chứng **62 UnityEngine.UI.Text** và các component liên quan I2 localization trong cùng cây REF04, dựa trên original SerializedFile/Component PathID/SHA-256 và cặp IL2CPP v31 **trùng với P1/P2**. P3 **không** dựng UI Unity, không tự thay chuỗi, không tự gán font hay ngôn ngữ.

## Báo cáo và kiểm tra

- `tools/audit_ref04_p3_text_localization.py` ghép **62 Text** với bằng chứng hai backend của `output/ref04-text-source-binary-evidence.json` và Step2 source inventory. Mỗi Text lưu chỉ dấu hash UTF-8, số byte nguyên bản, m_Font PPtr và các field style nguồn đã đối chiếu. Không log text gốc, không coi đây là chuỗi lúc game chạy.
- Dùng **cùng GameObject/RectTransform PathID đã chứng minh** để tìm component `I2.Loc.Localize` hoặc `TextLocalizeChecker` ở cùng GameObject. Đây là **ứng viên đồng vị trí**, không phải chứng cứ mapping localization key hoặc runtime update.
- `tools/ref04_il2cpp_method_index_v31.py` có chế độ `family="p3"` để kiểm kê các method khai báo trong `I2.Loc.Localize`, `I2.Loc.LocalizationManager`, `TextLocalizeChecker`, `UnityEngine.UI.Text`, `UnityEngine.Font` nếu có trong metadata gốc. Chỉ dùng metadata ownership/token, không suy từ tên method ra control flow.
- Bản ghi riêng `output/ref04-p3-text-font-localization-source.json` được lưu trong `output/` gitignored, không commit data nguồn, chuỗi game hoặc file binary.
- `tests/test_ref04_p3_text_localization.py` kiểm thử chặt SHA, PPtr, nội dung Text, sai metadata source pair và không tự gán localization. CI Linux dùng XAPK thật, Windows kiểm thử fixture.

## Các bằng chứng cần bổ sung mới được chốt runtime P3

1. Font PPtr phải được giải đến **đúng Font asset/source dependency** có path và SHA, sau đó mới chứng minh font được tải thực tế và metric rendering trên thiết bị.
2. Localization key phải được đọc từ managed fields/nguồn từ gói I2, chứng minh liên kết key với Text cụ thể, từ đó trace selected language, fallback và cập nhật qua code.
3. Nội dung HUD Text động chỉ được suy từ **verified ARM64 method body + state/inputs thực tế**; Text serialized không được coi là runtime.
4. Text placement cần runtime Canvas + SafeArea từ P2, không mặc định 1600×900 hoặc hình ảnh preview.

**Trạng thái không được nâng sai:** `runtimeTextAndLocalizationProven=false`, `runtimeLanguageChosen=null`, `runtimePlacementProven=false`, `unityImportAllowed=false`. Chưa sửa Scene/Prefab/UI/characters/Spine.

### Lệnh chạy

```powershell
py -3 tools/audit_ref04_p3_text_localization.py
py -3 -m unittest discover -s tests -p test_ref04_p3_text_localization.py -v
```

Cần chạy P1 và P2 source report trước, với đúng XAPK gốc. Nếu thiếu nguồn, fail-closed. Bộ phân tích báo cáo **số liệu từ dữ liệu thật** chứ không giả lập nội dung hay vị trí.
