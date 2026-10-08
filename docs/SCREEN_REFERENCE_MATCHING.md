# Đối chiếu 4 ảnh giao diện người dùng đã cung cấp

**Trạng thái:** Đã đối chiếu bằng mắt nội dung 4 ảnh với danh mục Unity trích xuất từ XAPK. Đây là **bằng chứng tham chiếu hình ảnh**, không phải kết quả chạy APK trong môi trường kiểm thử hay một bản khôi phục giao diện hoàn chỉnh.

**Quyền và dữ liệu:** Không lưu các ảnh chụp người dùng vào repo công khai. Không sao chép ảnh nhân vật, tên tài khoản, số tiền, nội dung chat hoặc video. Các hình wireframe dưới đây là SVG suy ra từ RectTransform, không chứa artwork game.

## Kết quả ghép theo screenshot

| Tham chiếu | Nội dung quan sát | Cây Unity ứng viên | Bằng chứng định danh từ XAPK | Độ tin cậy / hạn chế |
|---|---|---|---|---|
| REF01 | Màn hình 3 thẻ tàu, nút nâng cấp/chi tiết, điều hướng trái-phải, tài nguyên trên cùng | Canvas trong datapack `file029` | `/Canvas/BoxPet/CellPet1..3`, mỗi cell có `SpineMonster`, `ImageFrame`, `ButtonDetail`, `ButtonActivate`; có `ButtonPrev` và `ButtonNext` | **Ứng viên rất mạnh** về cấu trúc. Chưa xác nhận chính xác model tàu, tên tàu hoặc chữ trên nút gán động |
| REF02 | Giao diện chi tiết nhân vật với phần hình nhân vật lớn bên trái, bảng thông số/kỹ năng bên phải | `/PanelHeroInfo` trong datapack `file110` | 954 RectTransform; `HeroBox`, `RightPart`, `PanelHeroList`, nhóm `BoxInfo`, `PanelPoint`, `PanelESkill`, `ButtonPrev`, `ButtonNext` | **Ứng viên rất mạnh** về bố cục; chưa thể khớp skin / animation Spine Nami & Robin từ dữ liệu tĩnh |
| REF03 | Bản đồ đại dương với các đảo, tên khu vực và biểu tượng thông báo | Canvas `file083` **hoặc** `file101` | `/Canvas/ScrollView/Viewport/Content/ImageBgr/DumbIsland*`, `TagName/TextIsland`, nhóm `TopBar` và `GroupRightButton` | **Hai biến thể chưa phân biệt**; tên đảo và vị trí hiển thị có thể lấy động từ game |
| REF04 | Trang chủ bãi biển, đội hình nhiều nhân vật, sự kiện, tài nguyên, các nút điều hướng bên phải/bên dưới | Canvas `file025` (cấu trúc tương tự `file071`) | `PlayerHeroes` (211 nút con), `PanelHomeTopLeft`, `PanelHomeBottom`, `PanelHomeTopRight`, `TopResource`, `Background/SpineShipPlayer` | **Ứng viên rất mạnh** về nhóm giao diện; 2 serialized file gần như trùng cấu trúc, chưa chứng minh bản nào hoạt động tại runtime |

## SVG wireframe chuyên biệt

Các file này được sinh từ metadata sau khi workflow XAPK UI audit chạy thành công. **Không phải ảnh thật** và chưa tái dựng đúng 9-slice, CanvasScaler, Mask, Render Order, Spine skeleton hoặc hiệu ứng.

| Tham chiếu | Sơ đồ |
|---|---|
| REF01 – 3 thẻ tàu | [REF01-ship-upgrade.svg](../reports/xapk/wireframes/REF01-ship-upgrade.svg) |
| REF02 – thông tin nhân vật | [REF02-hero-detail.svg](../reports/xapk/wireframes/REF02-hero-detail.svg) |
| REF03 – bản đồ, ứng viên A | [REF03-islands-map-A.svg](../reports/xapk/wireframes/REF03-islands-map-A.svg) |
| REF03 – bản đồ, ứng viên B | [REF03-islands-map-B.svg](../reports/xapk/wireframes/REF03-islands-map-B.svg) |
| REF04 – trang chủ đội hình | [REF04-home-crew.svg](../reports/xapk/wireframes/REF04-home-crew.svg) |

Bảng máy đọc: [ui-screenshot-reference-previews.csv](../reports/xapk/ui-screenshot-reference-previews.csv).

## Những phần đã quan sát từ ảnh nhưng chưa có trong wireframe

1. **REF01:** ba tàu đồ họa chi tiết, khung mạ kim loại, nút kính lúp, nền nội thất và hệ thống tài nguyên. Cần xác nhận model tàu hiển thị qua `SpineMonster` và logic bật/tắt nút; không gán sprite theo suy đoán.
2. **REF02:** nhân vật/skin cỡ lớn, hiệu ứng động, số liệu, các tab trang bị và biểu tượng kỹ năng. Các trường này có thể phụ thuộc Spine + dữ liệu nhân vật tại runtime.
3. **REF03:** địa hình các đảo, nước biển, nhãn khu vực, scroll và vị trí chấm thông báo. Hai Canvas ứng viên có thể tương ứng hai trang/phiên bản khác nhau.
4. **REF04:** hình nhân vật full-body, cảnh nền, ship/wave, thanh sự kiện/quest và nhiều biểu tượng trạng thái. Cần các scene/prefab và runtime animation để dựng giống ảnh.

**Phải bỏ qua:** Thanh thời lượng và điều khiển video xuất hiện chồng lên REF02 và REF04; đây không phải component Unity trong game. Nội dung tài khoản/cấp độ/tiền là dữ liệu phiên chạy, không phải dữ liệu UI cố định.

## Cấp độ xác minh

- **Đã xem bằng mắt:** 4 hình minh họa trực quan, đúng thứ tự do người dùng gửi.
- **Đã tìm bằng metadata:** các đường dẫn GameObject/RectTransform nói trên trong XAPK và các tham chiếu Sprite liên quan.
- **Chưa xác nhận:** scene runtime chính xác, kết quả so khớp pixel, hoạt ảnh, kích thước Canvas thực, thao tác nút, màn hình nào tải asset qua mạng hoặc biến thể prefab nào game đang dùng.

## Việc cần làm kế tiếp

- Có **4 ảnh chụp sạch** từ chính trò chơi (không thanh YouTube, tên tài khoản riêng tư được che) và có thông số pixel/thiết bị; sử dụng nguồn mà người dùng có quyền cung cấp.
- Với REF01, kiểm tra asset/prefab `BoxPet`, ngữ nghĩa `ButtonActivate`, `SpineMonster` và nhãn nút runtime.
- Với REF02, đối chiếu skeleton/skin hoặc sprite nhân vật thực tế với `PanelHeroInfo`.
- Với REF03, thử so vị trí đảo với `file083` và `file101` để chọn đúng biến thể.
- Với REF04, so `file025` và `file071`, xem model nhân vật/ship qua Spine để tránh dựng scene chỉ có icon.
- Kiểm tra tọa độ theo đúng CanvasScaler và tỷ lệ màn hình thật; chỉ sau đó mới tính sai lệch layout và xác nhận fidelity.

**Không suy diễn:** Có ảnh tham khảo không đồng nghĩa đã lấy được source Unity, đồ họa có quyền sử dụng, hay server tương thích.
