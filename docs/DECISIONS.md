## 2026-10-12 — BẮT BUỘC: UI Unity phải đúng cả Edit Mode (tắt Play) lẫn Play Mode (bật Play)

**Quy tắc áp dụng cho toàn bộ sửa UI của KaitoKidShop, bắt đầu từ PR #9:**

1. **Cấm xem chỉ sửa trong Start/Awake/OnEnable hoặc instantiate khi Play là hoàn tất UI.** Những thay đổi cố định (chữ tiếng Việt, icon, thứ tự nút, kích thước, anchor, Canvas scale, bật/tắt nút, nền và vị trí layout) phải được **lưu trực tiếp vào Scene/Prefab** được commit. Mở Unity khi chưa Play phải thấy kết quả đúng.
2. **Dữ liệu động cần có cách xem trước trung thực ở Edit Mode**: dùng asset, ảnh, ID và text thực có nguồn; đánh dấu rõ EditorOnly/preview, không giả mạo tài khoản, cấp nhân vật, sở hữu hay gameplay. Khi Play phải loại bỏ preview và hiển thị logic/dữ liệu thật, không nhân đôi đối tượng.
3. Với mỗi yêu cầu UI, **kiểm tra cả hai trạng thái** trên cùng branch/SHA, cùng Game View resolution: (A) tắt Play, đóng/mở lại Scene, quan sát Scene View + Game View; (B) bật Play kiểm tra tương tác/nội dung/Console; (C) tắt Play lần nữa, xác minh UI không quay về bản cũ.
4. **Không xóa callback gốc vô cớ** và không ghi đè thay đổi local chưa lưu. Được sửa bản Scene gốc đã import nếu cần đáp ứng UI, nhưng diff phải giới hạn/giải thích, có regression test và Git history khôi phục. Chỉ chấp nhận kết quả "UI PASS" khi kiểm tra Unity Editor thực tế; static GitHub CI PASS là điều kiện cần, không đủ.
5. Nếu screenshot Edit Mode hiện chữ/ảnh cũ nhưng GitHub Scene đã có bản mới, phải kiểm tra **git HEAD, git status, Unity file đang mở và re-open Scene**, không tự nhận code runtime đã giải quyết lỗi. Cảnh báo khi local Scene bị chỉnh khác và không được ép reset/checkout làm mất công việc của người dùng.

**Áp dụng tại PR #9 / `feat/offline-hero-ui`:** `HeroListNewScene.unity` đã lưu bảy nhãn tiếng Việt, counter `—/6`, Canvas scale `(1,1,1)` và **8 thẻ preview EditorOnly** từ source `db_heroes.json` + portrait Sprite đúng avatar. `OfflineHeroListPresenter.Start()` ẩn và hủy preview trước khi dựng list runtime, không gộp preview vào đội hình hay PlayerPrefs. Đây là sửa cả Edit/Play, không chỉ runtime; `main` chưa thay đổi.

## 2026-10-10 — REF04 P5: bắt buộc provenance trước khi dùng giá trị UI

- **Vấn đề:** báo cáo đơn lẻ có thể được sao chép từ XAPK khác hoặc bị chỉnh ID/hash/tọa độ, khiến một giá trị preview trông như dữ liệu runtime thật.
- **Quyết định:** chỉ chấp nhận kiểm toán tổng hợp khi đối chiếu exact SerializedFile, cặp binary SHA, component owner/RectTransform ID và từng byte-span/hash gốc của 168 LayoutGroup fields qua hai schema; Text và Font/localizer phải tái đối chiếu xuyên Step2/P3/P4. Giữ nguồn private trong `output/`.
- **Giới hạn:** xác minh serialized value không có nghĩa runtime scale/Screen.safeArea/Text/placement đã phục hồi. `runtimeCoordinates`, `runtimeCanvasScale`, `runtimeSafeAreaFormula`, `runtimeTextPositions` tiếp tục `null`; `1600×900` không được dùng làm runtime ground truth. Không tự sửa Unity hoặc merge PR Draft.
- **Hệ quả:** nếu thiếu báo cáo nguồn, byte hash không trùng, có mutation không chứng minh hoặc giá trị tọa độ tự điền, P5 **FAIL/BLOCKED**; yêu cầu chạy XAPK thật từ CI rồi xác minh thiết bị/Unity để chuyển sang runtime.

## 2026-10-10 — Phase 3B: native Unity MonoBehaviour header + managed IL2CPP source schema

- **Bằng chứng:** Unity nguồn 2022.3.51f1, IL2CPP metadata v31. `AssetsTools` fail 1.201/1.201 lúc sinh schema; `AssetStudio` và `AssetRipper` tạo schema từ cùng cặp binary nhưng phát sinh sai khác ở native MonoBehaviour header.
- **Lựa chọn:** lấy native MonoBehaviour header từ UnityPy TypeTree đúng version của file nguồn, ghép với **managed TypeTree sinh từ IL2CPP**, chỉ chấp nhận khi parse hết object (`check_read=True`), kiểm tra GameObject/MonoScript PPtr, enabled và mọi field type/range.
- **Kết quả lần đầu:** AssetStudio 1.201 component / 8.102 field values; AssetRipper 1.108 / 7.451. 93 LayoutGroup chỉ AssetStudio parse strict thành công; giữ source provenance riêng cho từng backend.
- **Quyết định an toàn:** cross-verify trên từng (scene, component PathID, field), source SHA và PPtr qua hai backend; CI phải FAIL nếu bất đồng. Tài nguyên nhị phân không upload GitHub. Không tự áp dụng UI values vào Prefab trước visual/Unity Editor audit.

# Quyết định kỹ thuật đang áp dụng

> Mỗi quyết định mới ghi **ngày, vấn đề, lựa chọn, hệ quả**; chi tiết triển khai ở PR/commit. Không dùng file này để ghi mọi lần chỉnh text.

## 2026-10-09 — Giai đoạn 3B phải giải mã đủ object từ binary đúng build
- **Vấn đề:** metadata IL2CPP v31 và field offset trong memory không chứng minh byte offset của Unity SerializedFile.
- **Quyết định:** opt-in TypeTreeGeneratorAPI dùng chính libil2cpp.so + global-metadata.dat từ XAPK có xác minh SHA và Unity version nguồn; chỉ đọc thành công nếu strict full-object parser, owner ID, script PPtr, enabled flags và kiểu field khớp. Nếu thiếu dữ liệu hoặc công cụ không hỗ trợ thì BLOCKED.
- **Hệ quả:** mọi giá trị quản lý chưa đọc vẫn UNKNOWN; không cập nhật Prefab tự động, không đưa binary lên Git, test synthetic không được xem là kiểm chứng XAPK thật.

## 2026-10-09 — Không suy diễn UI managed fields từ metadata strings
- **Vấn đề:** Image/CanvasScaler/Mask/LayoutGroup trong asset build IL2CPP thiếu type trees; biết MonoScript class không chứng minh serialized field values.
- **Quyết định:** đối chiếu theo exact source PPtr và chỉ nhập giá trị typetree có bằng chứng; metadata v31 chỉ kiểm tra header/tên trường và trạng thái. Thiếu trường là `NO_MANAGED_TYPETREE`, không thay bằng config giả để gọi là UI gốc.
- **Tiếp theo:** xác minh binary schema/field offsets cụ thể của build trước khi triển khai decoder giá trị, không cam kết 100% fidelity từ metadata tĩnh.

## 2026-10-09 — Ghép Sprite bằng source component ID, không bằng path string
- **Vấn đề:** 137 Image thuộc các nút trùng tên bị bỏ qua; Image bị tắt trong serialized data được preview vô tình hiện.
- **Lựa chọn:** xác minh Image component → GameObject → RectTransform qua ID gốc; kế hoạch dựng và bằng chứng layout phải có component ID trùng nhau. Áp dụng m_Enabled khi đọc được; không suy luận Image Type/CanvasScaler khi thiếu IL2CPP typetree.
- **Hệ quả:** sửa được liên kết và một phần render, nhưng không tuyên bố UI 100%; cần runtime evidence.

## 2026-10-09 — Dừng coi Canvas preview là UI gốc
- **Vấn đề:** bản xem thử đang đặt root `(1,1,1)`, CanvasScaler `1600×900`, Image `Simple`, thiếu các trường được serialize và runtime Spine.
- **Quyết định:** trích xuất trực tiếp GameObject/Component/Transform/PPtr, kiểm kê bằng chứng Prefab/Scene; tạo riêng **Prefab nghiên cứu cấu trúc**, giữ nguyên giá trị gốc kể cả root zero-scale; KHÔNG tự gắn Image, Mask, CanvasScaler, Text hay Spine giả. Năm scene tham chiếu vẫn là **ứng viên**, chưa được chứng minh là file Editor gốc.
- **Hệ quả:** Scene nghiên cứu có thể trống hình, nhưng không khiến người xem hiểu nhầm là UI gốc chính xác. Cần xác minh từng managed type trước khi bật hiển thị.

## 2026-10-09 — Một chức năng, một nhánh ngắn hạn và một PR
- **Vấn đề:** lịch sử push thẳng lên `main` khó đánh giá, AI lần sau thiếu ngữ cảnh vì phải đọc nhiều commit.
- **Lựa chọn:** `main` là nhánh ổn định; làm trên `feat/`, `fix/`, `docs/`, `test/`, `chore/` từ `origin/main`; CI + kết quả thực tế được chấp nhận rồi mới squash merge. Cập nhật `AI_HANDOFF` và history trong PR.
- **Hệ quả:** thay đổi phát sinh có PR để dễ kiểm tra/rollback. **Branch protection trên GitHub cần bật trong Settings bởi quản trị viên**; tài liệu/CI riêng lẻ không thể bảo đảm mọi tài khoản đều bị chặn push thẳng.

## 2026-10-09 — Handoff ngắn, lịch sử ở file riêng
- **Lựa chọn:** `AGENTS.md` chỉ điểm đọc; `AI_HANDOFF.md` ghi tối đa trạng thái + mục tiêu hiện hành + blocker; `ARCHITECTURE`, `DECISIONS`, `TROUBLESHOOTING` và `history/YYYY-MM.md` chứa thông tin bổ sung. `git log` vẫn là dữ liệu commit gốc.
- **Hệ quả:** AI phiên sau đọc ít file trước, không nhầm nhật ký dài với trạng thái hiện tại.

## 2026-10-09 — Không dựng đồ họa / Spine bằng suy đoán
- **Vấn đề:** Sprite hiển thị không có nghĩa khôi phục runtime gốc. XAPK tĩnh thiếu trạng thái nhân vật, mask/layout động, Spine skin/track.
- **Lựa chọn:** Sprite/Transform bằng pathID đã xác minh; Spine content chain chỉ báo bằng chứng nguồn. Tạo scene test Spine riêng, **không gắn ngẫu nhiên vào REF04**.
- **Hệ quả:** tiến độ chậm hơn ghép hình thủ công nhưng hạn chế dựng sai. Không tuyên bố khôi phục 100%.

## 2026-10-09 — Spine 3.8 runtime nằm local, không đưa lên GitHub
- **Lựa chọn:** runtime tải và sử dụng theo giấy phép tương ứng; tránh đưa lên repo. Dự án Unity 2022.3 vẫn mở, nhưng spine-unity 3.8 chỉ được hỗ trợ chính thức đến Unity 2020.3; dùng bản sao test khi cần.
- **Hệ quả:** CI Python không thể thay Unity Play Mode/Console và kiểm tra hình ảnh.

## 2026-10-09 — CI không tự commit vào main
- **Lựa chọn:** hai workflow build metadata Unity và kiểm kê XAPK chỉ **kiểm tra** kết quả sinh; nếu file tracked cần cập nhật thì tác giả đưa thay đổi vào feature branch/PR.
- **Hệ quả:** không xảy ra thay đổi `main` âm thầm từ bot; người làm phải chạy generator trong nhánh và commit kết quả trước khi merge.
