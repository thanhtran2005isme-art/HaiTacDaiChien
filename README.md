# Hải Tặc Đại Chiến (HTDC)

> **Dự án Unity đang phát triển** — mục tiêu là xây dựng và hoàn thiện một bản prototype có thể chạy offline, từng bước khôi phục giao diện và dữ liệu tướng, rồi phát triển backend riêng. Repository hiện **chưa phải bản game online hoàn chỉnh**; các chức năng cần server gốc vẫn chưa hoạt động đầy đủ.

## Giới thiệu

Hải Tặc Đại Chiến là project Unity tập trung vào giao diện, dữ liệu nhân vật và các hệ thống nền của game. Repository hiện chứa Unity project ở thư mục gốc, các Scene/Prefab/tài nguyên đã được đưa vào project, mã C# hiện có và những phần mới được phát triển độc lập để phục dựng từng tính năng.

Các phần đang được làm theo hướng **ưu tiên tính chính xác của dữ liệu nguồn, tách rõ chức năng offline mới với hành vi game gốc, và kiểm thử trước khi khẳng định đã hoạt động**.

## Trạng thái dự án

| Hạng mục | Trạng thái hiện tại |
|---|---|
| Unity project và Scene | Có project tại thư mục gốc; gồm các Scene như Home, Hero List, Battle, Login, World, Campaign và Arena. Sự hiện diện của Scene không đồng nghĩa toàn bộ luồng chơi đã hoạt động. |
| Danh mục tướng offline | Có mã đọc danh mục tướng từ JSON nguồn và lọc theo hệ, sao, trạng thái phát hành. |
| Giao diện danh sách/chi tiết tướng | Đang phát triển bằng cách tận dụng Scene, prefab và Sprite có sẵn trong project; cần xác minh trực tiếp trong Unity Editor. |
| Đội hình offline | Có lựa chọn tối đa 6 tướng, lưu cục bộ bằng Unity PlayerPrefs. Đây **không** phải kho tướng của tài khoản server. |
| Database | Có thiết kế schema độc lập gồm 84 bảng cho MySQL và biến thể cho MariaDB/XAMPP, cùng công cụ nhập cấu hình nguồn. |
| Backend API và đăng nhập online | **Chưa triển khai hoàn chỉnh, chưa kết nối với client Unity.** |
| Build/gameplay end-to-end | Chưa có xác nhận rằng toàn bộ game có thể build và chơi trọn vẹn; cần kiểm tra Unity Editor, Play Mode và các phụ thuộc thực tế. |

## Tính năng và phần việc hiện có

### Unity client

- Project sử dụng **Unity 2022.3.51f1** và C#.
- Tổ chức giao diện qua Unity uGUI, Scene, Prefab, Text/Image/Button và các tài nguyên nhân vật có sẵn trong project.
- Scene liên quan đến giao diện và phát triển gồm `Assets/Scenes/Home2Scene.unity`, `Assets/Scenes/HeroListNewScene.unity`, `Assets/Scenes/HomeScene.unity` và những Scene khác trong `Assets/Scenes/`.
- `OfflineHeroCatalog` đọc dữ liệu cấu hình tướng từ `Assets/Resources/_pack/db/db_heroes.json`. Snapshot nguồn hiện có 751 bản ghi cấu hình; đây là dữ liệu định nghĩa, không phải danh sách tướng mà người chơi sở hữu.
- `OfflineHeroListPresenter` và `OfflineHeroDetailModal` là phần triển khai mới cho danh sách và chi tiết tướng offline. Ảnh hoặc Spine chỉ hiện khi tài nguyên tương ứng thực sự có và được liên kết đúng.
- `OfflineHeroParty` lưu đội hình cục bộ tối đa 6 tướng bằng PlayerPrefs; không đồng bộ với server hoặc database.

**Lưu ý về code nguồn:** một phần script hiện có được xuất/khôi phục từ project Unity và có các phương thức còn là stub hoặc trả về giá trị mặc định. Ví dụ những lớp như `DbManager`, `NetworkController`, `PlayerInventory` và `LineUpSaveLoad` chưa thể được xem là backend hay gameplay gốc đã phục hồi đầy đủ. Các tính năng mới cần được đánh dấu rõ là triển khai độc lập, không suy diễn rằng chúng tương đương hành vi của bản game gốc.

### Nền tảng database

Thư mục `database/` chứa một **thiết kế database mới**, không phải bản sao đã xác minh của server gốc:

- Schema MySQL 8.4 gồm 84 bảng cho tài khoản, phiên đăng nhập, người chơi, tướng/vật phẩm, đội hình, tiến trình, trận đấu, nhiệm vụ, cửa hàng, guild, PvP và log giao dịch.
- Script `database/mysql/import_configs.py` nhập các file cấu hình JSON/TXT có trong repository. Snapshot được kiểm kê gồm 139 file qua các thư mục ngôn ngữ và 66.191 bản ghi đã giải mã.
- Có schema riêng tương thích mục tiêu MariaDB 10.4/XAMPP tại `database/xampp/`.

Schema và dữ liệu cấu hình **chưa tự tạo server đang chạy**. Client Unity chưa được kết nối tới API/database này; tài khoản, đăng nhập, kho tướng người chơi, tiền tệ, mua hàng và các giao dịch gameplay chưa thể coi là hoàn chỉnh hoặc an toàn cho môi trường production.

## Công nghệ

- **Game client:** Unity 2022.3.51f1, C#, Unity uGUI.
- **Dữ liệu cấu hình:** JSON/TXT, Unity Resources.
- **Lưu đội hình prototype:** PlayerPrefs (local/offline).
- **Database foundation:** MySQL 8.4; schema tương thích MariaDB 10.4/XAMPP.
- **Kiểm tra tự động:** Python unittest và GitHub Actions cho các kiểm tra tĩnh, dữ liệu và schema.

Các kiểm tra tự động không thay thế việc mở project trong Unity Editor, kiểm tra Console, chạy Play Mode hoặc xác nhận hình ảnh/animation thực tế.

## Bắt đầu với Unity

### Yêu cầu

- Git và Git LFS.
- Unity Hub và Unity Editor **2022.3.51f1**.
- Python 3.11+ nếu muốn chạy bộ kiểm thử hoặc công cụ dữ liệu.
- MySQL 8.4 hoặc XAMPP/MariaDB chỉ khi cần làm việc với phần database; không bắt buộc để mở Unity project.

### Tải project

Từ Terminal, PowerShell hoặc CMD:

```bash
git lfs install
git clone https://github.com/thanhtran2005isme-art/HaiTacDaiChien.git
cd HaiTacDaiChien
git lfs pull
```

Repository dùng Git LFS cho nhiều loại tài nguyên Unity và hình ảnh. Nếu bỏ qua bước lấy LFS, một số file có thể chỉ là pointer và không mở/import được đúng.

### Mở trong Unity

1. Mở Unity Hub, chọn **Add project from disk** và trỏ đến thư mục gốc `HaiTacDaiChien` — thư mục chứa `Assets/`, `Packages/` và `ProjectSettings/`.
2. Chọn Unity Editor **2022.3.51f1** và chờ Unity import/compile hoàn tất.
3. Mở các Scene cần kiểm tra, ví dụ `Assets/Scenes/Home2Scene.unity` hoặc `Assets/Scenes/HeroListNewScene.unity`.
4. Kiểm tra tab **Console** trước khi chạy. Khi thay đổi giao diện, kiểm tra cả Edit Mode, Play Mode và sau khi thoát Play Mode.

Không có cam kết rằng toàn bộ luồng khởi động, đăng nhập, chiến đấu hoặc kết nối server đã hoạt động chỉ bằng cách mở project. Nếu gặp lỗi import/compile, hãy ghi lại Unity version và lỗi Console đầu tiên để khoanh vùng.

## Chạy kiểm thử

Từ thư mục gốc repository:

**Windows (PowerShell/CMD):**

```powershell
py -3 -m unittest discover -s tests -p "test_*.py" -v
```

**Linux/macOS:**

```bash
python3 -m unittest discover -s tests -p 'test_*.py' -v
```

Có thể chạy từng nhóm riêng:

```bash
python3 -m unittest discover -s tests -p 'test_offline_hero_catalog.py' -v
python3 -m unittest discover -s tests -p 'test_offline_hero_ui.py' -v
python3 -m unittest discover -s tests -p 'test_home2_scene_navigation.py' -v
python3 -m unittest discover -s tests -p 'test_mysql_foundation.py' -v
python3 -m unittest discover -s tests -p 'test_xampp_mariadb_schema.py' -v
```

Các bài test trong `tests/` phần lớn kiểm tra dữ liệu, cấu trúc file, hợp đồng mã nguồn và hồi quy tĩnh. Kết quả PASS không chứng minh project compile trong Unity, animation đã render đúng, hay game có thể chơi end-to-end.

## Database: tài liệu và lệnh chạy

Nếu làm việc với phần database, hãy đọc tài liệu tương ứng trước khi import:

- [MySQL 8.4: schema và nhập dữ liệu](database/mysql/README.md)
- [MariaDB/XAMPP: schema và hướng dẫn nhập](database/xampp/README.md)

Không cần cài database để chỉ mở Unity project. File SQL sinh tự động và báo cáo kiểm toán được đặt trong thư mục generated bị Git bỏ qua. Chỉ import vào database thử nghiệm đã chuẩn bị; sao lưu trước khi thử với dữ liệu quan trọng. Không dùng schema này cho server public/production khi chưa có backend, phân quyền, kiểm thử giao dịch và rà soát bảo mật.

## Cấu trúc repository

```text
HaiTacDaiChien/
├── Assets/
│   ├── Scenes/                       # Các Scene Unity
│   ├── Scripts/Assembly-CSharp/      # C# hiện có và phần triển khai mới
│   └── Resources/_pack/db/           # Dữ liệu cấu hình game
├── Packages/                         # Unity package manifest
├── ProjectSettings/                  # Cấu hình project Unity
├── database/
│   ├── mysql/                        # Schema MySQL và importer
│   └── xampp/                        # Schema/ghi chú cho MariaDB
├── tests/                            # Python unittest và kiểm tra hồi quy
├── tools/                            # Công cụ audit/kiểm tra dữ liệu
└── docs/                             # Handoff, quyết định và quy trình phát triển
```

## Hướng phát triển tiếp theo

1. **Ổn định client:** mở và kiểm thử các Scene bằng đúng phiên bản Unity; sửa lỗi compile, tham chiếu mất, layout và thao tác người dùng.
2. **Hoàn thiện gameplay offline:** chốt dữ liệu tướng, đội hình, lưu/khôi phục trạng thái và các luồng màn hình có thể hoạt động mà không giả lập dữ liệu tài khoản.
3. **Xây backend riêng:** API đăng nhập, phiên, người chơi, inventory và đội hình; dùng dữ liệu server làm nguồn sự thật thay vì tin giá trị do client gửi lên.
4. **Kết nối database an toàn:** giao dịch nguyên tử cho tiền/vật phẩm/phần thưởng, xác minh quyền sở hữu, chống xử lý lặp và kiểm thử tích hợp.
5. **Xác minh bản build:** chỉ công bố tính năng là hoàn thành sau khi có bằng chứng từ Unity Editor/Play Mode và kiểm tra luồng sử dụng tương ứng.

## Quyền sử dụng tài nguyên

Đây là dự án nghiên cứu/phát triển, **không phải bản phát hành chính thức của Hải Tặc Đại Chiến**. Tên thương mại, artwork, âm thanh, dữ liệu game, thư viện và runtime bên thứ ba có thể thuộc quyền của chủ sở hữu tương ứng. Hãy xác minh quyền sử dụng trước khi chia sẻ hoặc phân phối bản build/tài nguyên. Không commit tài khoản cá nhân, mật khẩu, token, file `.env`, database dump có dữ liệu nhạy cảm hoặc runtime/tài nguyên thương mại khi chưa có giấy phép.

## Tài liệu thêm

- [Bàn giao và trạng thái công việc](docs/AI_HANDOFF.md)
- [Quy trình nhánh, PR và kiểm thử](docs/WORKFLOW.md)
- [Checklist xác minh giao diện trong runtime](docs/RUNTIME_SCREEN_CHECKLIST.md)
- [MySQL 8.4](database/mysql/README.md)
- [MariaDB/XAMPP](database/xampp/README.md)

---

**Nguyên tắc của project:** phân biệt rõ dữ liệu nguồn, chức năng đã triển khai offline, thiết kế backend mới và các phần chưa được xác minh trong Unity. Không đánh dấu tính năng hoàn thành chỉ dựa trên mã tĩnh hoặc tên Scene.
