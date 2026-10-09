# Quyết định kỹ thuật đang áp dụng

> Mỗi quyết định mới ghi **ngày, vấn đề, lựa chọn, hệ quả**; chi tiết triển khai ở PR/commit. Không dùng file này để ghi mọi lần chỉnh text.

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
