# Quy trình Git — một chức năng → một nhánh → PR → main

Mục tiêu là có **một `main` ổn định**, nhánh ngắn hạn của từng tính năng/fix, lịch sử dễ tìm và không làm mất dữ liệu đang sửa.

## 1. Bắt đầu chức năng mới

Trong CMD, từ repo (không có thay đổi local chưa lưu):

```cmd
git status
git fetch origin
git switch main
git pull --ff-only origin main
git switch -c feat/ten-chuc-nang
git push -u origin feat/ten-chuc-nang
```

Dùng `fix/...`, `docs/...`, `test/...`, `chore/...` theo loại thay đổi. **Nhánh phải xuất phát từ `origin/main` cập nhật**, và `git status` phải sạch trước khi đổi nhánh; nếu chưa sạch, commit/stash/copy an toàn, không reset/xóa.

## 2. Làm và ghi chép

- Chỉ sửa một chức năng/fix có phạm vi cụ thể.
- Commit dễ hiểu: `feat(scope): ...`, `fix(scope): ...`, `docs(scope): ...`.
- Cập nhật `docs/AI_HANDOFF.md` về tình trạng, thay đổi quan trọng, test, blocker. Khi cần bổ sung mốc theo tháng vào `docs/history/YYYY-MM.md`. Kiến trúc thay đổi thì cập nhật `docs/DECISIONS.md`.
- Không thêm file rác, ZIP ảnh, XAPK, token, Spine runtime, `output/` hoặc `Assets/LocalReconstruction/`.
- Kiểm thử theo phạm vi: Python/JS CI, Unity Console/Play Mode khi sửa C# uGUI/animation; ghi rõ phần nào không kiểm chứng.
- `git push` **lên nhánh tính năng**, không dùng `git push origin main`.

## 3. Tạo Pull Request

```cmd
git add <cac-file-da-kiem-tra>
git commit -m "feat(scope): mo ta thay doi"
git push -u origin feat/ten-chuc-nang
```

Trên GitHub chọn **Compare & pull request**: `base: main`, `compare: feat/ten-chuc-nang`. Điền mẫu PR trong `.github/PULL_REQUEST_TEMPLATE.md`: thay đổi, dữ liệu nguồn, test thực tế, hạn chế, tài liệu đã cập nhật.

**Chỉ merge khi**:
- CI liên quan đã PASS (không chỉ CI của một commit cũ).
- Người sử dụng xác nhận tính năng hoạt động/OK nếu có UI, logic quan trọng hoặc animation.
- Không có lỗi P0/P1 hoặc tài nguyên bí mật/bản quyền trong diff.
- Handoff và docs đã cập nhật để cuộc chat sau tiếp tục được.

Ưu tiên **Squash and merge** để 1 PR chức năng thành 1 commit sạch; với công việc cần giữ nhiều commit độc lập, chọn merge phù hợp và ghi lý do.

## 4. Sau khi merge

```cmd
git switch main
git pull --ff-only origin main
git branch -d feat/ten-chuc-nang
git fetch --prune
```

Xóa nhánh remote đã merge trên trang GitHub khi phù hợp, không xóa nhánh chưa merge. Kiểm tra `main` có PR merge commit/changes, `docs/AI_HANDOFF.md` ghi đúng trạng thái. `git log` là lịch sử tuyệt đối; docs/history là sổ tay đọc nhanh.

## 5. Bảo vệ nhánh main trên GitHub — cần người quản trị bật

Vào **Repository → Settings → Rules → Rulesets** (hoặc **Branches → Add branch protection rule** tùy giao diện), target `main`, bật:
- **Require a pull request before merging**;
- **Require status checks to pass** (chọn workflow thật sự được chạy với PR);
- chặn force-push/xóa branch; hạn chế bypass nếu cần;
- bật **Automatically delete head branches** sau merge nếu phù hợp.

Repo hiện dùng GitHub connector không có quyền thay đổi thiết lập branch protection; **không tuyên bố đã bật/enforce** nếu chưa xác minh trong Settings. Workflow kiểm thử không thể tự ngăn push trực tiếp khi không có rule.

## 6. Lưu ý riêng: metadata tự sinh

Workflow `.github/workflows/unity-offline-viewer.yml` **không tự push thẳng `main`**. Nếu dữ liệu Unity `ui-scenes.json` thay đổi vì generator, chạy `python tools/build_unity_viewer_data.py --repo-root .` trên nhánh chức năng rồi commit kết quả vào PR. Không dùng bot để ghi đè main.

## 7. ChatGPT phiên sau

Yêu cầu: “Đọc `AGENTS.md`, `docs/AI_HANDOFF.md` và `docs/ARCHITECTURE.md`; kiểm tra HEAD/PR mở; tiếp tục đúng task hiện hành, **tạo nhánh từ main trước khi sửa**, test và xin xác nhận trước khi merge”. Không cần đọc mọi commit trừ khi truy vết lỗi.
