# HaiTacDaiChien — Bước 1: Kiểm kê điều hướng HomeScene (read-only)

**Nguồn kiểm tra:** `feat/htdc-localization-hold` trên GitHub, `Assets/Scenes/HomeScene.unity`, các file `Assets/Scripts/Assembly-CSharp/*.cs` và `.cs.meta` tương ứng. **Không sửa mã/Scene/Prefab.**

## Kết quả có bằng chứng

- **55** liên kết `m_OnClick` trong `HomeScene.unity`; mỗi target fileID đều tồn tại trong scene, các GUID script đã được đối chiếu với `.cs.meta` theo 15 class.
- **55/55** `m_CallState: 2` (RuntimeOnly), **55/55** `m_TargetAssemblyTypeName` rỗng: cần kiểm tra tính hợp lệ trong Unity Inspector; không mặc định đây là nguyên nhân chính.
- Có **26 liên kết** ở 13 công trình và NameTag; cả hai cùng trỏ về `HomeBuildingAction.OnClick()` cho mỗi công trình. `m_BuildingType` được serialize (ví dụ B_Altar=8, B_Arena=14).
- Các handler ở `HomeBuildingAction`, `PanelHomeBottom`, `PanelHomeMenu`, `PanelHomeLeft`, `PanelHomeTopRight`, `HomeScene`, `GameItem` và `PlayerAvatar` đã xem đều rỗng hoặc không có triển khai nghiệp vụ.
- File Scene/Prefab ở cột “Đích ứng viên” được chọn theo **tên hiện có**; **chưa có bằng chứng cho biết nút từng thực sự mở file đó** do handler C# bị rỗng.
- 206 file `HTDC_Localization_Hold` nằm ngoài `Assets`: **không đủ bằng chứng** để quy trách nhiệm lỗi điều hướng cho chúng.

## Toàn bộ 55 liên kết trong HomeScene

| # | Button / nút UI | Button component `fileID` | `OnClick` method | Script đích | Target component `fileID` | Scene/Prefab đích (ứng viên) |
|---:|---|---:|---|---|---:|---|
| 1 | `B_Tarven/NameTag` | `879` | `OnClick` | `HomeBuildingAction` | `1212` | TarvenScene.unity (ứng viên) |
| 2 | `PanelHomeBottom/ButtonGuild` | `887` | `ButtonGuildClick` | `PanelHomeBottom` | `1031` | GuildScene.unity (ứng viên) |
| 3 | `PanelHomeBottom/ButtonBag` | `891` | `ButtonBagClick` | `PanelHomeBottom` | `1031` | ItemListScene.unity (ứng viên) |
| 4 | `B_Dungeon/NameTag` | `919` | `OnClick` | `HomeBuildingAction` | `1205` | DungeonScene.unity (ứng viên) |
| 5 | `B_Altar` | `927` | `OnClick` | `HomeBuildingAction` | `928` | AltarScene.unity (ứng viên) |
| 6 | `PanelHomeTopRight/ButtonEndow` | `931` | `ButtonEndowClick` | `PanelHomeTopRight` | `1064` | Chưa xác minh |
| 7 | `B_Arena` | `933` | `OnClick` | `HomeBuildingAction` | `934` | ArenaScene.unity (ứng viên) |
| 8 | `B_BlackSmith/NameTag` | `936` | `OnClick` | `HomeBuildingAction` | `1098` | ForgeScene.unity (ứng viên) |
| 9 | `PanelHomeBottom/ButtonShop` | `951` | `ButtonIAPClick` | `PanelHomeBottom` | `1031` | PopupShop/Store (ứng viên) |
| 10 | `HUD/ButtonAddGem` | `955` | `ButtonAddGemClick` | `HomeScene` | `876` | Chưa xác minh |
| 11 | `B_Explore/NameTag` | `958` | `OnClick` | `HomeBuildingAction` | `1144` | Chưa xác minh |
| 12 | `PanelHomeRight/ButtonFb` | `961` | `ButtonFbClick` | `PanelHomeMenu` | `908` | Chưa xác minh |
| 13 | `B_Lucky/NameTag` | `964` | `OnClick` | `HomeBuildingAction` | `1148` | LuckySpinScene/SuperLuckySpinScene (ứng viên) |
| 14 | `B_BraveTrial/NameTag` | `978` | `OnClick` | `HomeBuildingAction` | `1172` | BraveTrialScene/BraveTrialScene2 (ứng viên) |
| 15 | `PanelHomeLeft/ButtonSupport` | `993` | `ButtonSupportClick` | `PanelHomeBottom` | `1031` | Chưa xác minh |
| 16 | `PanelHomeLeft/ButtonChat` | `997` | `ButtonChatClick` | `PanelHomeLeft` | `1202` | Chưa xác minh |
| 17 | `B_SummoningHouse` | `1007` | `OnClick` | `HomeBuildingAction` | `1008` | SummonOrbScene/SummonScrollScene (ứng viên) |
| 18 | `B_SealedLand` | `1010` | `OnClick` | `HomeBuildingAction` | `1009` | SealedLand.unity (ứng viên) |
| 19 | `PanelHomeRight/ButtonQuest` | `1022` | `ButtonQuestClick` | `PanelHomeMenu` | `908` | PopupDailyQuest (ứng viên) |
| 20 | `PanelHomeBottom/ButtonMonster` | `1025` | `ButtonMonsterClick` | `PanelHomeBottom` | `1031` | MonsterScene.unity (ứng viên) |
| 21 | `B_Arena/NameTag` | `1036` | `OnClick` | `HomeBuildingAction` | `934` | ArenaScene.unity (ứng viên) |
| 22 | `PanelHomeLeft/ButtonCheckin` | `1040` | `CheckinClick` | `PanelHomeLeft` | `1202` | Chưa xác minh |
| 23 | `B_Tower` | `1042` | `OnClick` | `HomeBuildingAction` | `1043` | TowerNewScene.unity (ứng viên) |
| 24 | `PanelHomeBottom/ButtonHeroes` | `1047` | `ButtonHeroesClick` | `PanelHomeBottom` | `1031` | HeroListScene / HeroListNewScene (ứng viên) |
| 25 | `B_Market/NameTag` | `1060` | `OnClick` | `HomeBuildingAction` | `1170` | PopupShop / giao diện chợ (ứng viên) |
| 26 | `PanelHomeRight/ButtonRaid` | `1071` | `ButtonEventRaidClick` | `PanelHomeMenu` | `908` | PopupEventRaid (ứng viên) |
| 27 | `PanelHomeLeft/ButtonMail` | `1076` | `ButtonMailClick` | `PanelHomeLeft` | `1202` | Chưa xác minh |
| 28 | `B_BlackSmith` | `1097` | `OnClick` | `HomeBuildingAction` | `1098` | ForgeScene.unity (ứng viên) |
| 29 | `PanelHomeBottom/ButtonCampaign` | `1109` | `ButtonCampaignClick` | `PanelHomeBottom` | `1031` | CampaignMapScene/CampaignScene (ứng viên) |
| 30 | `PanelHomeLeft/ButtonVIP` | `1115` | `VipClick` | `HomeScene` | `876` | Chưa xác minh |
| 31 | `B_SummoningHouse/NameTag` | `1120` | `OnClick` | `HomeBuildingAction` | `1008` | SummonOrbScene/SummonScrollScene (ứng viên) |
| 32 | `B_Tower/NameTag` | `1124` | `OnClick` | `HomeBuildingAction` | `1043` | TowerNewScene.unity (ứng viên) |
| 33 | `B_Explore` | `1143` | `OnClick` | `HomeBuildingAction` | `1144` | Chưa xác minh |
| 34 | `B_FusingHouse` | `1145` | `OnClick` | `HomeBuildingAction` | `1146` | FusingHeroScene.unity (ứng viên) |
| 35 | `B_Lucky` | `1147` | `OnClick` | `HomeBuildingAction` | `1148` | LuckySpinScene/SuperLuckySpinScene (ứng viên) |
| 36 | `B_Altar/NameTag` | `1158` | `OnClick` | `HomeBuildingAction` | `928` | AltarScene.unity (ứng viên) |
| 37 | `HUD/ButtonAddGold` | `1162` | `ButtonAddGoldClick` | `HomeScene` | `876` | Chưa xác minh |
| 38 | `B_FusingHouse/NameTag` | `1165` | `OnClick` | `HomeBuildingAction` | `1146` | FusingHeroScene.unity (ứng viên) |
| 39 | `B_Market` | `1169` | `OnClick` | `HomeBuildingAction` | `1170` | PopupShop / giao diện chợ (ứng viên) |
| 40 | `B_BraveTrial` | `1171` | `OnClick` | `HomeBuildingAction` | `1172` | BraveTrialScene/BraveTrialScene2 (ứng viên) |
| 41 | `PanelHomeRight/ButtonAchievement` | `1175` | `ButtonChallengeClick` | `PanelHomeMenu` | `908` | Chưa xác minh |
| 42 | `B_SealedLand/NameTag` | `1181` | `OnClick` | `HomeBuildingAction` | `1009` | SealedLand.unity (ứng viên) |
| 43 | `PanelHomeTopRight/ButtonEvent` | `1200` | `ButtonEventClick` | `PanelHomeTopRight` | `1064` | EventScene.unity (ứng viên) |
| 44 | `B_Dungeon` | `1204` | `OnClick` | `HomeBuildingAction` | `1205` | DungeonScene.unity (ứng viên) |
| 45 | `PanelHomeLeft/ButtonFriend` | `1207` | `ButtonFriendClick` | `PanelHomeLeft` | `1202` | Chưa xác minh |
| 46 | `B_Tarven` | `1211` | `OnClick` | `HomeBuildingAction` | `1212` | TarvenScene.unity (ứng viên) |
| 47 | `PanelHomeLeft/PlayerAvatar` | `1220` | `OnClick` | `PlayerAvatar` | `1229` | Chưa xác minh |
| 48 | `PanelHomeLeft/GiftOnline/GameItem/ActionClick` | `1222` | `ActionClick` | `GameItem` | `1225` | Chưa xác minh |
| 49 | `GiftOnline/ItemEquipmentUI` | `1273` | `OnItemClick` | `ItemEquipmentUI` | `1281` | Chưa xác minh |
| 50 | `GiftOnline/HeroHeadClickable` | `1290` | `OnHeroClick` | `HeroHeadClickable` | `1289` | Chưa xác minh |
| 51 | `GiftOnline/ItemArtifactUI` | `1293` | `OnItemClick` | `ItemArtifactUI` | `1302` | Chưa xác minh |
| 52 | `GiftOnline/ItemVipExpUI` | `1329` | `OnItemClick` | `ItemVipExpUI` | `1321` | Chưa xác minh |
| 53 | `GiftOnline/ItemAvatarUI` | `1332` | `OnItemClick` | `ItemAvatarUI` | `1335` | Chưa xác minh |
| 54 | `GiftOnline/ItemConsumableUI` | `1352` | `OnItemClick` | `ItemConsumableUI` | `1359` | Chưa xác minh |
| 55 | `GiftOnline/ItemShardUI` | `1380` | `OnItemClick` | `ItemShardUI` | `1379` | Chưa xác minh |

## Mức độ chắc chắn và lỗ hổng

| Phân loại | Trạng thái |
|---|---|
| Button → `OnClick` → target component nội Scene | SOURCE VERIFIED (55/55) |
| Target script GUID → tên class qua `.cs.meta` | SOURCE VERIFIED (15/15) |
| `m_CallState` RuntimeOnly | SOURCE VERIFIED (55/55) |
| `m_TargetAssemblyTypeName` rỗng | SOURCE VERIFIED (55/55); phải kiểm tra trong Unity |
| Handler C# có nội dung nghiệp vụ | EMPTY với 8 class đã đọc; các `Item*UI` còn phải kiểm tra từng hàm |
| Nút mở Scene/Popup đích nào | UNKNOWN / candidate; không được tự gọi là verified |
| Event/Manager đăng ký callback động từ `Start`/`Awake` | BLOCKED: nhiều hàm đã xuất chỉ còn stub |
| Unity thực sự dispatch từng `OnClick` | UNTESTED, cần Unity Play Mode và Console |
| Lua runtime, server, save-game, data, prefab loader | UNVERIFIED |

**Lưu ý phạm vi:** đây là kiểm kê toàn bộ cấu hình OnClick trong **HomeScene**, không phải tất cả các Prefab và Scene của trò chơi. Muốn có kết quả toàn dự án, chạy script `audit_unity_navigation.py` ở repo local; script chỉ đọc asset và xuất CSV/Markdown vào thư mục báo cáo riêng.

## Kiểm thử cần làm tiếp

1. Mở `HomeScene` bằng Unity **không chạy Administrator**; xem Console, chọn ButtonHeroes → phần OnClick ở Inspector để xác nhận target và method có hoạt động hay hiện Missing.
2. Quét toàn bộ `Assets/**/*.unity`, `Assets/**/*.prefab` và GUID script để lập dependency graph các nút UI liên Scene/Prefab. Không di chuyển `HTDC_Localization_Hold` vào lại Assets.
3. Kiểm tra UI manager, `HomeBuildingAction`, `PanelHomeBottom`, `PanelHomeMenu`, `BaseScene`, code loader Lua/AssetBundle trước khi dựng navigator thay thế.
4. Chỉ sau khi chọn đúng đích nghiệp vụ, triển khai từng handler trên nhánh PR riêng và test end-to-end `click → scene/popup → back → state retained`.
## Chạy công cụ sau khi kéo nhánh về máy

Mở PowerShell tại thư mục gốc repository `HaiTacDaiChien`, chạy:

```powershell
py -3 tools\audit_unity_navigation.py --root . --output ..\HTDC_navigation_report
```

Báo cáo được ghi bên ngoài repository (`..\HTDC_navigation_report`), gồm `navigation_callbacks.csv` và `summary.md`. Công cụ chỉ đọc các Scene/Prefab/Script; không sửa Unity asset, không khôi phục `HTDC_Localization_Hold` và không triển khai handler.
