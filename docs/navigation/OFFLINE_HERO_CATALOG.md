# Offline hero catalog — dữ liệu tướng nguồn, không có backend

**Branch triển khai:** `feat/offline-hero-catalog` (base: `feat/htdc-localization-hold`)  
**Phạm vi:** Giai đoạn 1 — chỉ đọc, chuẩn hóa, lọc dữ liệu tướng.  
**Nguồn:** `Assets/Resources/_pack/db/db_heroes.json` (không thay đổi).  
**Mã:** `Assets/Scripts/Assembly-CSharp/OfflineHeroCatalog.cs` và `.meta`.

## Dữ liệu kiểm kê theo nguồn GitHub hiện tại

| Trường | Phát hiện |
|---|---:|
| Bản ghi | 751 |
| ID duy nhất | 751 |
| Tên duy nhất | 406 |
| `release=1` | 203 |
| `release=0` | 548 |
| Fortress | 135 |
| Abyss | 142 |
| Forest | 133 |
| Shadow | 131 |
| Dark | 93 |
| Light | 117 |
| `star` xuất hiện | 0, 1, 2, 3, 4, 5, 6, 9, 10 |

Các giá trị `type` như `forest` và `Forest` được chuẩn hóa thành cùng enum;
giữ nguyên `SourceFaction`, `Id`, `AvatarId`, `Name`, `ClassName`, `Star`,
`Rank`, `ReleaseFlag` từ file. `release=1` chỉ là cờ dữ liệu; **không
phải** bằng chứng rằng người chơi đang sở hữu, đã mở khóa, hoặc đã nhận tướng.
Một tên có thể có nhiều ID/cấp sao; bảng còn bao gồm boss, NPC và bản thử nghiệm.
Không được hiển thị toàn bộ 751 bản ghi như 751 tướng sở hữu.

## Cách kiểm tra trong Unity 2022.3.51f1

1. Mở Unity project ở **root repo** sau khi checkout nhánh.
2. Chờ Unity hoàn tất import/compile, kiểm tra Console có lỗi C# hay không.
3. Chọn menu **Tools → HTDC → Offline Heroes → Validate db_heroes.json**.
4. Xem Console: số bản ghi, release và từng hệ/cấp sao phải khớp bảng trên.
5. Nếu không thấy menu hoặc báo `Missing Assets/Resources/_pack/db/db_heroes.json`,
   kiểm tra script/import TextAsset, đúng project root và Console.

Trong C# (chỉ ví dụ dùng API; **chưa** kết nối Scene):

```csharp
OfflineHeroCatalog catalog = OfflineHeroCatalog.LoadFromResources();

// Chỉ đọc các bản ghi có source release=1, 10 sao, hệ Forest.
List<OfflineHeroRecord> heroes = catalog.Query(
    faction: OfflineHeroFaction.Forest,
    star: 10,
    release: OfflineHeroReleaseFilter.Released);

OfflineHeroRecord hiyori;
if (catalog.TryGetById(524, out hiyori))
    Debug.Log(hiyori.Name + " / " + hiyori.Star + " sao / " + hiyori.Faction);
```

API còn có `Entries` (read-only, tăng theo ID), `Count`, `FromJson(string)`
(dùng cho kiểm thử), `Query(faction, star, release)` và `TryGetById(int)`.
Bộ đọc dùng `Resources.Load<TextAsset>("_pack/db/db_heroes")`; do
`JsonUtility` không hỗ trợ top-level array nên bọc JSON trong `{"rows":[...]}`
trước khi parse. Không ghi ngược JSON, không tạo backend hoặc tài khoản.

## Kiểm thử và giới hạn

- Chạy `py -3 -m unittest discover -s tests -p test_offline_hero_catalog.py -v` trên Windows;
  hoặc `python3 -m unittest discover -s tests -p 'test_*.py' -v` để chạy thêm regression.
- Workflow `offline-hero-catalog.yml` kiểm tra dữ liệu và mã **tĩnh**; CI không
  thay thế kiểm thử Unity Editor compile/Play Mode.
- Tests gắn với snapshot 751 bản ghi; khi thay file bằng nguồn cập nhật có bằng
  chứng, sửa thống kê và assertion theo dữ liệu thực, không tạo thêm ID giả.
- Giai đoạn này **chưa** điền tướng vào `Home2Scene`, `HeroListNewScene`,
  `PanelHeroInfo`, không tải Sprite/Spine, không có chọn đội hình, không giải
  quyết cơ chế HeroModel, inventory, SDK hay server.
- Uta Last Wish chưa tìm thấy trong DB nguồn ở thời điểm kiểm kê; không tự gán
  một ID hoặc coi Uta gốc ID 456–458 là phiên bản này.

## Bước tiếp theo, cần task riêng

Sau khi xác nhận Unity Editor load được dữ liệu: xây adapter từ
`OfflineHeroRecord` vào **UI dictionary offline** trong `HeroListNewScene`;
tách khỏi inventory tài khoản và không sửa gốc 47 callback của Home2Scene.
Tiếp tục đối chiếu avatar/Spine theo asset thực và kiểm thử Play Mode trước khi
đưa lên màn hình Home2Scene.
