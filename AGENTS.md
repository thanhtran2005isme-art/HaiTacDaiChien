# Hướng dẫn cho AI / coding agent — HaiTacDaiChien

> **Điểm đọc đầu tiên.** Tài liệu này là chỉ dẫn làm việc, **không** thay cho kiểm tra mã và Git hiện tại.

## Đọc theo thứ tự (không cần đọc toàn bộ lịch sử)
1. `AGENTS.md` (file này).
2. [docs/AI_HANDOFF.md](docs/AI_HANDOFF.md) — tình trạng hiện tại, việc gần nhất, việc tiếp theo, rủi ro.
3. [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) — các thành phần, luồng dữ liệu, lệnh chạy.
4. [docs/DECISIONS.md](docs/DECISIONS.md) — quyết định kỹ thuật còn hiệu lực.
5. [docs/TROUBLESHOOTING.md](docs/TROUBLESHOOTING.md) khi có lỗi.
6. [docs/WORKFLOW.md](docs/WORKFLOW.md) — cách tạo nhánh, PR, kiểm thử, merge.
7. [docs/history/](docs/history/) và `git log` **chỉ khi cần** xem chi tiết commit.

## Quy tắc bắt buộc khi sửa
- Trước khi sửa: `git status`, `git fetch origin`, đọc `docs/AI_HANDOFF.md` và mã liên quan; phân biệt dữ liệu đã kiểm chứng và giả định.
- **Không commit/push trực tiếp lên `main`.** Mỗi chức năng/fix/docs task dùng **một nhánh ngắn hạn từ `origin/main`**, tên `feat/...`, `fix/...`, `docs/...`, `test/...` hoặc `chore/...`; push nhánh lên `origin`; tạo PR về `main`.
- Thay đổi nhỏ nhưng độc lập vẫn dùng PR. Không tạo nhiều nhánh dài hạn hoặc trộn nhiều chức năng vào một PR.
- Với PR: ghi rõ mục tiêu, file thay đổi, bằng chứng kiểm thử, phần chưa kiểm chứng, vấn đề còn lại; cập nhật `docs/AI_HANDOFF.md` và bổ sung `docs/history/YYYY-MM.md` khi có mốc quan trọng. Thêm quyết định vào `docs/DECISIONS.md` nếu đổi kiến trúc/giới hạn.
- Chỉ **merge sau khi CI bắt buộc PASS và người yêu cầu xác nhận kết quả**, đặc biệt với giao diện/animation trong Unity phải thử trên Editor thực tế. Nếu chưa xác nhận, để PR mở; **không coi CI Python là kiểm thử render Unity**.
- Ưu tiên **squash merge** một chức năng thành một commit dễ truy vết. Sau merge đồng bộ `main`; xóa nhánh tính năng đã merge khi phù hợp.
- Không xóa/sửa các tài nguyên local bị ignore chỉ vì Git không nhìn thấy. Không tự force-push, reset --hard hoặc ghi đè thay đổi chưa lưu.
- Không đưa XAPK/artwork thương mại, runtime Spine có điều kiện cấp phép, `output/`, `Assets/LocalReconstruction/`, tài khoản/credential vào GitHub. Xem `.gitignore`.

## Nguồn sự thật khi mâu thuẫn
1. Mã đang ở `origin/main` / commit thực tế và kết quả công cụ vừa chạy.
2. CI mới nhất và log Unity thực tế (mỗi loại chứng minh một phạm vi khác nhau).
3. `docs/AI_HANDOFF.md`, tài liệu kỹ thuật và lịch sử commit.

**Sau mỗi PR:** cập nhật handoff ngắn gọn: `Cập nhật lúc`, `HEAD/PR`, `Đã làm`, `Đã kiểm thử`, `Chưa xong`, `Bước tiếp theo`. Không biến handoff thành nhật ký nhiều nghìn dòng.
