# Quyết định kỹ thuật đang áp dụng

> Mỗi quyết định mới ghi **ngày, vấn đề, lựa chọn, hệ quả**; chi tiết triển khai ở PR/commit. Không dùng file này để ghi mọi lần chỉnh text.

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
