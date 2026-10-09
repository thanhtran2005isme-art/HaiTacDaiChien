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
- `tools/serve_ui_viewer.py` — máy chủ Python localhost chỉ cung cấp 4 file được phép.
- `web-ui-viewer/index.html`, `style.css`, `app.js` — giao diện web.
- `unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json` — dữ liệu metadata cho cả bản web và Unity.
- `tools/build_unity_viewer_data.py` — sinh lại dữ liệu từ các báo cáo CSV.
- `tests/test_offline_web_viewer.py` — kiểm thử HTTP, quyền truy cập và dữ liệu.

Lỗi thiếu JSON: từ thư mục root, chạy `py tools/build_unity_viewer_data.py --repo-root .`.

Lỗi cổng 8765 đang dùng: mở terminal và chạy `py tools/serve_ui_viewer.py --port 8766`.

Để dừng: quay lại cửa sổ terminal đã mở .bat và nhấn `Ctrl+C`.

## Lưu ý kỹ thuật và quyền sử dụng

Đây **chỉ là công cụ xem metadata UI**, không phải game chơi được hay project Unity gốc. Các vùng màu là placeholder: chưa có hình ảnh gốc, hiệu ứng Spine, skin, bản đồ 3D, camera, animation thực tế, server hoặc gameplay. Giao diện dựng gần đúng từ thông tin tĩnh; chưa kiểm chứng tỷ lệ CanvasScaler runtime.

Không đưa asset thương mại, dữ liệu cá nhân, XAPK, mã game dịch ngược hoặc ảnh chụp game vào bản web. Tất cả request của bản web ở **127.0.0.1**, không gọi bất kỳ API game nào.

Nếu cần một client game có thể chơi, vẫn cần phát triển gameplay, art được phép sử dụng, lưu dữ liệu và server mới.
