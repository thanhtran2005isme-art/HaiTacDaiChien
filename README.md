# Hải Tặc — UI Viewer chạy offline không cần Unity

Có hai bản xem trước UI cùng dùng dữ liệu Unity đã trích xuất:

**A. Bản web (khuyên dùng nếu chỉ muốn mở nhanh):** chạy `CHAY_UI_OFFLINE.bat` ở thư mục gốc. Windows mở trình duyệt tại http://127.0.0.1:8765. Bạn có thể mở repo bằng VS Code để xem/sửa HTML, CSS, JS.

**B. Bản Unity:** nằm tại `unity-ui-viewer/`; chỉ cần Unity khi muốn phát triển project C# Unity uGUI, dựng scene trong Unity Editor, hoặc build game Unity.

## Chạy ngay trên Windows 10 bằng .bat

Nếu bạn chưa tải repo:

```bat
set GIT_LFS_SKIP_SMUDGE=1
git clone https://github.com/thanhtran2005isme-art/HaiTacDaiChien.git
cd HaiTacDaiChien
CHAY_UI_OFFLINE.bat
```

Nếu đã có repo:

```bat
git pull origin main
CHAY_UI_OFFLINE.bat
```

**Chỉ cần Python 3 + trình duyệt web.** Nếu `python` hoặc `py` chưa được nhận diện, cần cài Python và thêm vào PATH. Không cần Node.js, npm, Unity Editor hay chạy game GOSU. Trình duyệt được mở tự động; bạn cũng có thể tự truy cập `http://127.0.0.1:8765/`.

Nếu đang mở trong VS Code: mở terminal tại thư mục gốc rồi chạy `./CHAY_UI_OFFLINE.bat` trong PowerShell (hoặc `CHAY_UI_OFFLINE.bat` trong CMD).

## Có những chức năng gì?

- Mở **5 cây UI ứng viên** ứng với 4 ảnh đã cung cấp (nâng cấp tàu; chi tiết tướng; bản đồ A/B; trang chủ).
- Vẽ wireframe bố cục tương đối theo `RectTransform` với canvas **1600×900 giả định**.
- Click ô màu để xem GameObject, Sprite ứng viên, lớp Spine, anchor/size, tên nút và đường dẫn.
- Tìm kiếm thành phần UI; lọc Image có Sprite, Button, Spine và Image thiếu Sprite.
- Phóng to, thu nhỏ, hiện hoặc ẩn các node không active.
- Chuyển màn hình bằng menu hoặc phím trái/phải.
- Nút **Tăng cấp +1** chỉ thay đổi con số demo trong trang; không phải gameplay thật.

## Cấu trúc file

- `CHAY_UI_OFFLINE.bat` — file chạy trên Windows.
- `tools/serve_ui_viewer.py` — máy chủ Python localhost chỉ cung cấp 5 file được phép.
- `web-ui-viewer/index.html`, `style.css`, `app.js` — giao diện web.
- `unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json` — dữ liệu metadata cho cả bản web và Unity.
- `tools/build_unity_viewer_data.py` — sinh lại dữ liệu từ các báo cáo CSV.
- `tests/test_offline_web_viewer.py` — kiểm thử HTTP, quyền truy cập và dữ liệu.

Lỗi thiếu JSON: từ thư mục root, chạy `py tools/build_unity_viewer_data.py --repo-root .`.

Lỗi cổng 8765 đang dùng: mở terminal và chạy `py tools/serve_ui_viewer.py --port 8766`.

Để dừng: quay lại cửa sổ terminal đã mở .bat và nhấn `Ctrl+C`.

## Hiển thị ảnh thật từ máy (bản Web mới)

Sau khi mở `CHAY_UI_OFFLINE.bat` và vào `http://127.0.0.1:8765/`, trong cột trái bạn sẽ thấy **Ảnh của bạn**.

**Cách 1 — Tự ghép hàng loạt:** nhấn **Chọn thư mục ảnh**, chọn thư mục có PNG, JPG hoặc WebP mà bạn có quyền sử dụng. Trình duyệt sẽ tìm đúng **tên Sprite** (không tính đuôi), ví dụ nếu metadata ghi `bgr_monster` thì file có thể tên `bgr_monster.png`. Các ảnh chỉ nằm trong bộ nhớ trình duyệt, **không tải lên máy chủ**, không sao chép vào GitHub. Tên trùng nhau bị bỏ qua để tránh gắn nhầm; hiện giới hạn 750 ảnh, tổng 250 MB và tối đa 20 MB/ảnh.

**Cách 2 — Gán ảnh thủ công:** chọn một vùng UI hoặc tìm GameObject trong danh sách; ở cột bên phải chọn **Chọn ảnh cho nút**. Cách này dùng được cho nút chưa có Sprite hoặc các mục Spine mà bạn có ảnh được phép dùng. Có thể bỏ ảnh gán riêng cho một nút.

**Cách 3 — Đối chiếu với ảnh chụp:** nhấn **Chọn ảnh nền scene** tại cột trái để đặt ảnh của màn hình hiện tại phía sau các vùng UI. Dùng **Viền kiểm tra** để xem/hide đường biên các thành phần. Đây là ảnh tham chiếu, **không phải tái tạo giao diện chạy được**.

Các ảnh dùng từ máy **không được giữ lại sau khi tải lại trang**; bạn cần chọn lại thư mục. Bạn có thể tắt server và xóa toàn bộ ảnh khỏi phiên ngay trên giao diện. Có thể chọn lại thư mục sau khi cập nhật file mới mà không sửa code.

**Quan trọng:** Bản Web chưa đóng gói bất kỳ nhân vật, thuyền, texture, skin Spine hay artwork gốc của GOSU. Nếu không chọn ảnh có quyền sử dụng thì một số vùng vẫn chỉ là wireframe màu. Chỉ đổi CSS/metadata **không thể tự sinh hình ảnh của game**. File atlas chứa nhiều Sprite cần được chia thành các ảnh riêng đã có quyền sử dụng và được kiểm chứng trước khi ghép theo tên.

## Lưu ý kỹ thuật và quyền sử dụng

Đây **chỉ là công cụ xem metadata UI**, không phải game chơi được hay project Unity gốc. Các vùng màu là placeholder: chưa có hình ảnh gốc, hiệu ứng Spine, skin, bản đồ 3D, camera, animation thực tế, server hoặc gameplay. Giao diện dựng gần đúng từ thông tin tĩnh; chưa kiểm chứng tỷ lệ CanvasScaler runtime.

Không đưa asset thương mại, dữ liệu cá nhân, XAPK, mã game dịch ngược hoặc ảnh chụp game vào bản web. Máy chủ bản web chỉ bind **127.0.0.1**; ảnh chọn từ máy hiển thị qua URL `blob:` nội bộ trong browser và không có endpoint upload, không gọi API game nào.

Nếu cần một client game có thể chơi, vẫn cần phát triển gameplay, art được phép sử dụng, lưu dữ liệu và server mới.
