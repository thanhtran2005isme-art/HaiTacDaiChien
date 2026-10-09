## Mục tiêu của PR
<!-- Một chức năng/sửa lỗi duy nhất; liên kết issue hoặc mô tả lý do. -->

## Thay đổi
<!-- Liệt kê file/module/luồng đã sửa; thay đổi API/scene/metadata; không chèn file private. -->

## Kiểm thử và bằng chứng
- [ ] Đã chạy test phù hợp, ghi lệnh và kết quả bên dưới.
- [ ] Đã xem kết quả CI **của HEAD PR**.
- [ ] Nếu thay đổi Unity: đã kiểm tra Console, Game View, Play Mode trên Editor thực tế **hoặc ghi rõ CHƯA TEST**.
- [ ] Không còn lỗi nghiêm trọng chưa xử lý; nếu còn, ghi rõ.

Kết quả / log rút gọn:

```text
Lệnh:
Kết quả:
Giới hạn:
```

## Handoff / tài liệu
- [ ] Đã cập nhật `docs/AI_HANDOFF.md` (việc đã làm, test, blocker, bước tiếp).
- [ ] Đã cập nhật `docs/history/YYYY-MM.md` nếu có mốc/nhóm commit cần lưu.
- [ ] Đã cập nhật `docs/DECISIONS.md` khi có quyết định kiến trúc.
- [ ] Không đưa XAPK, ảnh game, runtime Spine, file `output/`, `LocalReconstruction/` hoặc secret vào diff.

## Điều kiện merge
- [ ] Nhánh bắt đầu từ `origin/main`, chưa push thẳng `main`.
- [ ] Người dùng/người review xác nhận chức năng đạt yêu cầu nếu có UI/animation.
- [ ] Chỉ merge sau khi các check bắt buộc PASS; ưu tiên squash merge.
