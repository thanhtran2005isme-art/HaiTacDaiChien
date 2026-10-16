# HomeScene navigation recovery — offline Unity UI

**Nhánh phát triển duy nhất:** `feat/htdc-localization-hold` (không merge vào `main`).
**Nguồn:** AssetRipper Unity 2022.3.51f1. Đây là `NEW_PROJECT_DESIGN`, không phải mã game XAPK gốc được phục hồi.

## Đã triển khai

- `Assets/Scenes/HomeScene.unity` giữ nguyên **55 persistent Button.OnClick** gốc, không chỉnh Layout/Sprite/Transform.
- Gắn duy nhất `HomeSceneNavigationBridge` (component fileID 1385) lên `Scripts` (GO fileID 5).
- **27 tuyến Scene**: 19 tuyến ở pilot đầu đã được người dùng xác nhận test được; thêm **8** tuyến chọn theo tên nút và Scene hiện có.
- **9 tuyến Popup**: nạp chính prefab đã xuất qua `Resources.Load`, tạo modal blocker trong suốt (không sửa giao diện HomeScene), ngăn mở trùng, hỗ trợ `ButtonClose`/`ButtonBack` và **Esc** để đóng Popup trước khi quay Scene.
- `HTDC_Localization_Hold`: **206 asset + 206 .meta**, vẫn nằm ngoài `Assets/`, không nhập lại.

## 8 tuyến Scene mới — cần xác minh trong Unity

| Button fileID | Nút/tương tác | Scene ứng viên |
| --- | --- | --- |
| 1047 | Tướng | `HeroListNewScene.unity` |
| 1109 | Chiến dịch | `CampaignMapScene.unity` |
| 964, 1147 | Công trình Vòng quay + NameTag | `LuckySpinScene.unity` |
| 978, 1171 | Công trình Thử thách + NameTag | `BraveTrialScene.unity` |
| 1007, 1120 | Nhà Triệu hồi + NameTag | `SummonOrbScene.unity` |

**Lưu ý:** Các Scene này **chỉ là ứng viên theo nhãn và tên asset**, không được khẳng định đúng tuyến điều hướng gốc. Chưa khôi phục dữ liệu/logic gameplay trong Scene đích. `HeroListScene`, `CampaignScene`, `BraveTrialScene2`, `SummonScrollScene`, `SuperLuckySpinScene` là các phiên bản liên quan cần so sánh bằng Play Mode.

## 9 tuyến Popup mới — cần xác minh trong Unity

| Button fileID | Nút/tương tác | Prefab thực tế (`Assets/Resources/`) |
| --- | --- | --- |
| 1022 | Nhiệm vụ | `prefabs/home/PopupDailyQuest.prefab` |
| 1076 | Thư | `prefabs/home/PopupMailBox.prefab` |
| 1207 | Bạn bè | `prefabs/friend/PopupFriend.prefab` |
| 1040 | Điểm danh | `prefabs/home/PopupCheckin.prefab` |
| 1071 | Đột kích | `prefabs/home/PopupEventRaid.prefab` |
| 951, 1060, 1169 | Cửa hàng + công trình Chợ | `prefabs/common/PopupShop.prefab` |
| 997 | Trò chuyện | `prefabs/common/PopupChatNew.prefab` |

Các prefab đều đã xác minh **có tồn tại trong repo**, nhưng chưa xác minh đủ dữ liệu, binding, khởi tạo, Lua/network; **mở giao diện không đồng nghĩa chức năng chat, mua hàng, điểm danh, nhiệm vụ hoạt động**. Các nút nội bộ của prefab vẫn mang C# stub và chưa được sửa nghiệp vụ.

## Unity Console follow-up: GroupButtonEvent

Sau khi người dùng xác nhận mở được PopupShop, PopupCheckin và PopupChatNew, Unity Console vẫn lặp cảnh báo `The referenced script on this Behaviour (Game Object 'GroupButtonEvent') is missing!`.

- Kiểm tra `Assets/Scenes/HomeScene.unity`: GameObject `136` chứa component `1141` với `m_Script: {fileID: 0}`, không có GUID/tên lớp/field nghiệp vụ nào để phục hồi. Toàn bộ Scene chỉ tham chiếu fileID 1141 hai lần (ở danh sách component và chính object header).
- Đã xóa **chỉ** component rỗng không thể chạy `1141` và dòng liên kết của nó; giữ `RectTransform 700`, các thành phần LayoutGroup `1140`/`1142`, toàn bộ `m_OnClick` và 36 route mới.
- Đây là sửa cảnh báo missing reference trong Scene, **không phải phục hồi logic script gốc**. Chờ Unity Play Mode xác nhận cảnh báo `GroupButtonEvent` đã biến mất.
- Các log `Creating missing CanvasRenderer/RectTransform component for Text in Placeholder ... Prefab instance` là vấn đề riêng của Unity/AssetRipper; chưa xác định SerializedFile/Prefab owner đầy đủ nên không sửa hoặc xóa placeholder tự động.

## Chưa khôi phục

- `HomeScene`: tài nguyên + Gem/Gold, VIP, FB, Support, Ưu đãi, sự kiện, Thành tựu; công trình Explore; các nút vật phẩm/callback tùy ngữ cảnh. Chưa gán đích nếu thiếu căn cứ.
- Luồng Popup con, close/back chuyên biệt, animation, dữ liệu game/backend, lưu trạng thái, tài khoản/mua hàng.
- C# stub như `HomeScene`, `BaseScene`, `PanelHomeBottom`, `BasePopup` vẫn còn phương thức rỗng.

## Pull + test

Trong PowerShell tại thư mục `HaiTacDaiChien`, bảo đảm `git status` không có thay đổi chưa lưu, chạy:

```powershell
git switch feat/htdc-localization-hold
git pull --ff-only origin feat/htdc-localization-hold
git lfs pull
py -3 -m unittest discover -s tests -p test_home_scene_navigation_pilot.py -v
```

Mở Unity **2022.3.51f1**, `Assets/Scenes/HomeScene.unity`, rồi Play. Test lần lượt các nút trong hai bảng trên. Với Popup, test click mở -> click ButtonClose/Back (nếu có) -> mở lại -> Esc đóng. Với Scene, test click chuyển -> Esc về HomeScene -> click lại. Chụp Console nếu có `Exception`, `Missing Script`, `Scene unavailable`, `Missing Resources prefab`.

**Test tĩnh / GitHub diff KHÔNG phải Unity Play Mode PASS.** Kết quả 17 tuyến mới chỉ được đánh dấu PASS sau khi người dùng chạy Unity thực tế.
