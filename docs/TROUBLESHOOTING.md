# Lỗi đã gặp và cách chẩn đoán

**Khi báo lỗi:** kèm tên scene, SHA branch hiện tại, lệnh đã chạy, **toàn bộ thông báo Console liên quan**, kết quả test. Đừng kết luận thiếu dữ liệu từ một ảnh màn hình đen.

| Triệu chứng | Nguyên nhân từng gặp | Cách xác minh / xử lý |
|---|---|---|
| `No cameras rendering` trong REF01/03/04 | Scene sinh cũ không có preview Camera; rebuild bị chặn trước Save | Cập nhật mã; chạy Reconstruct, Audit; kiểm tra Hierarchy có `Local Preview Camera` |
| `Zero-scale reconstructed Canvas` | Root RectTransform serialized tỷ lệ (0,0) trong 4 scene | Đặt preview Canvas root (1,1,1) có chủ đích; giữ subtree từ dữ liệu; không coi là scale runtime gốc |
| `Missing (Mono Script)` trong Inspector | Evidence MonoBehaviour trước đây dùng chung tệp script hoặc generated prefab cũ | Tách file C# theo class, để Unity compile; rebuild **generated** scene/prefab, Audit `missing Mono Scripts=0`. Không xóa source `Assets/Scripts` |
| `source Spine packs=0` dù thư mục có file | Tham chiếu chứng cứ không được serialize hoặc gói chưa nhập, hay runtime chưa tạo asset | Phân biệt Audit `content-matched` và `imported source packs`; reimport gói local; mở Inspector đúng GameObject |
| Chỉ thấy nền biển, vị trí đội hình trống | Static Sprite/Canvas đã có nhưng chưa xác minh nhân vật, skin hay animation được chọn | Không gắn nhân vật giả; kiểm tra nguồn Spine, runtime 3.8, thử riêng skeleton/animation |
| `Spine-Unity 3.8 not installed` | Có Spine Player JS (Web) nhưng thiếu Spine-Unity C# runtime | Import runtime 3.8 có quyền sử dụng vào **bản sao/test project**; Unity 2022 chưa được hỗ trợ chính thức, cân nhắc 2020.3 |
| **Audit Spine: 5/8 PASS, 3 gói `SkeletonDataAsset.atlasAssets missing or empty`** | AtlasAsset vẫn tồn tại; skeleton có thể chưa trỏ vào AtlasAsset chính xác khi import | Trên nhánh PR #4 dùng menu **Tools → HaiTac Offline UI Viewer → Spine 3.8 → Repair EMPTY verified atlas links (local only)**. Công cụ sẽ hỏi xác nhận, chỉ liên kết mảng atlasAssets đang trống khi đã kiểm tra chuỗi `skeleton.json → skeleton_Atlas.asset → atlas text → material → PNG` thuộc cùng pack, giữ nguyên các gói đã có tham chiếu. Nếu thấy **BLOCKED**, không dùng atlas khác; gửi Console. Chạy lại read-only audit để xác nhận **8/8** trước khi Play Mode. |
| Spine-Unity popup **`Could not automatically set the AtlasAsset for "skeleton"`** trong khi Reconstruct | Bộ dựng local từng import `skeleton.json` trước atlas và texture PNG; ngoài ra thiếu vùng ảnh atlas, sai phiên bản/runtime cũng có thể gây popup | Chọn **Stop importing**, không chọn **Import without atlases**; kiểm tra đúng thư mục pack trong `Assets/LocalReconstruction/SpinePacks/<id>/`, có PNG, `skeleton.atlas.txt`, `skeleton.json`. Nhánh `fix/ref04-source-image-render-state` đã sửa import **PNG → atlas → skeleton**; đồng bộ branch rồi Reconstruct. Nếu popup còn, kiểm tra Console, tên atlas/Texture thực tế và Spine 3.8, không nối AtlasAsset của pack khác. |
| `Spine TRACK_ADVANCING` nhưng Game trống | AnimationState tiến triển không chứng minh graphic được vẽ | Kiểm tra AtlasAsset, material, texture, Game View, shader/alpha và Console |
| `WinError 32` khi giải mã XAPK | Tệp Unity bundle tạm còn mở / lock trên Windows | Dùng bộ giải mã mới đọc từ memory, xem log, không xóa bừa tệp đang dùng |
| Unity chạy Administrator | Editor có nguy cơ thực thi script elevated | Đóng Unity/Hub, chạy bình thường dưới user tiêu chuẩn |
| `git pull` mà giao diện vẫn cũ | `Assets/LocalReconstruction` bị ignore: pull chỉ lấy mã | Reconstruct và Audit lại sau khi nhập mã mới, mở lại scene |

## Nơi xem lỗi
- Unity: **Window → General → Console**; `%LOCALAPPDATA%\Unity\Editor\Editor.log` trên Windows.
- GitHub: **Actions** chạy trên SHA của nhánh/PR; test Python/JS **không** chứng minh render Unity.
- Web: trình duyệt F12 → Console/Network; server offline chạy trên `127.0.0.1:8765`.
- Git: `git status`, `git log -5 --oneline`, `git branch -vv`, `git remote -v`.

## Sau khi fix
1. Ghi triệu chứng/nguyên nhân đã **xác nhận**, không đưa suy đoán thành sự thật.
2. Nêu test chạy và ảnh/Console hoặc log tương ứng.
3. Cập nhật `AI_HANDOFF.md` (trạng thái) và PR; nếu mang tính kiến trúc, thêm `DECISIONS.md`.
