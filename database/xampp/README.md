# HTDC / KaitoKidShop — cài MySQL qua XAMPP (MariaDB)

## Xác nhận trước khi nhập

XAMPP trên Windows ghi nút **MySQL** trong Control Panel nhưng thường chạy **MariaDB**. Bản MySQL 8.4 dùng `utf8mb4_0900_ai_ci` có thể không chạy trên MariaDB 10.4; PR #10 có riêng bản tương thích `database/xampp/001_schema_mariadb.sql`. Đây vẫn là **84 bảng**, bảo toàn quan hệ/key/index và dùng cùng công cụ import nguồn.

**Quan trọng:** MySQL 8.4 và XAMPP MariaDB là **hai server khác nhau**. Nếu cả hai muốn dùng TCP 3306, XAMPP báo "MySQL shutdown unexpectedly" hoặc port occupied. Kiểm tra server và chọn một cổng duy nhất. **Không xóa data, không thay tên thư mục `mysql/data`, không dừng database đang phục vụ ứng dụng khác khi chưa sao lưu.**

## Bước 1 — Khởi động XAMPP và xác định phiên bản

1. Mở **XAMPP Control Panel** (Run as administrator nếu cần).
2. Start **MySQL**; nếu muốn dùng phpMyAdmin thì Start cả **Apache**.
3. Mở `http://localhost/phpmyadmin/`, chọn tab SQL, chạy:

   ```sql
   SELECT VERSION() AS server_version, @@version_comment AS database_engine, @@port AS db_port;
   ```

4. Đối với MariaDB 10.4+ sử dụng file `database/xampp/001_schema_mariadb.sql`; nếu kết quả là MySQL 8.4 thực sự, dùng `database/mysql/001_schema.sql` thay thế.

## Bước 2 — Đồng bộ đúng nhánh chứa database

Trước khi chuyển nhánh hoặc pull, kiểm tra `git status --short`, commit/stash những sửa Unity local cần giữ. Nhánh MySQL dựa trên branch Hero UI nhưng **chưa merge PR #9 / main**.

```powershell
git fetch origin
git switch --track origin/feat/htdc-mysql-foundation
git lfs pull
```

Nếu nhánh đã tồn tại ở máy, thay lệnh `switch --track` bằng `git switch feat/htdc-mysql-foundation` và `git pull --ff-only origin feat/htdc-mysql-foundation`.

## Bước 3 — Tạo seed SQL từ toàn bộ dữ liệu nguồn

Mở PowerShell tại **thư mục gốc repo**:

```powershell
py -3 database/mysql/import_configs.py --repo-root . --validate-only
py -3 database/mysql/import_configs.py --repo-root . --output database/mysql/generated/002_game_config_seed.sql
```

Importer đọc thực tế **139 file JSON/TXT** trong các thư mục `db`, `db_en`, `db_th`, `db_zh`. Bản kiểm chứng CI: 66.191 records, 20.591 nguồn được chiếu ra bảng riêng, 751 định nghĩa tướng, 17 vật phẩm `name=NULL`. Không tạo tài khoản hay nhân vật đã sở hữu giả.

## Bước 4 — Nhập bằng chính MySQL/MariaDB CLI của XAMPP

Mở **CMD** ở thư mục gốc repo; ký hiệu `<` là chuyển hướng input của **CMD**, không phải PowerShell 5.1:

```cmd
"C:\xampp\mysql\bin\mysql.exe" -h 127.0.0.1 -P 3306 -u root -p < database\xampp\001_schema_mariadb.sql
"C:\xampp\mysql\bin\mysql.exe" -h 127.0.0.1 -P 3306 -u root -p < database\mysql\generated\002_game_config_seed.sql
```

- Nếu cài XAMPP ở nơi khác, thay `C:\xampp` bằng đường dẫn thực.
- Nếu XAMPP đã đổi port, thay `-P 3306` bằng số port MySQL/MariaDB của XAMPP.
- Nếu root XAMPP chưa có mật khẩu, `-p` sẽ yêu cầu nhập; đối với môi trường **local tạm thời** có thể bỏ `-p`, nhưng nên thiết lập mật khẩu root và tài khoản ứng dụng ít quyền trước khi kết nối backend. Không để MariaDB/XAMPP mở ra internet.
- Dùng CLI vì seed chứa hàng chục nghìn records và có thể vượt giới hạn upload của phpMyAdmin. phpMyAdmin thích hợp **xem và kiểm tra bảng** sau khi nhập.
- Nếu xuất hiện `max_allowed_packet` hoặc timeout, kiểm tra `my.ini` của MariaDB, giới hạn bộ nhớ/dung lượng và kích thước các file JSON trước khi tiếp tục. Không tự xóa/chia seed làm mất record.
- Nếu gặp `Unknown collation utf8mb4_0900_ai_ci`, bạn đã dùng **nhầm** schema MySQL 8.4; phải dùng file `database/xampp/001_schema_mariadb.sql` như trên.

## Bước 5 — Mở phpMyAdmin để kiểm tra

Mở `http://localhost/phpmyadmin/` → database `htdc_game` → tab **SQL**:

```sql
SELECT VERSION() AS database_version, @@port AS server_port;
SELECT COUNT(*) AS tables_total FROM information_schema.tables WHERE table_schema='htdc_game';
SELECT COUNT(*) AS source_files FROM htdc_game.catalog_files;
SELECT COUNT(*) AS source_records FROM htdc_game.catalog_records;
SELECT COUNT(*) AS hero_definitions FROM htdc_game.hero_definitions;
SELECT COUNT(*) AS player_accounts FROM htdc_game.players;
SELECT locale,COUNT(*) AS file_count FROM htdc_game.catalog_files GROUP BY locale;
```

Đối với checkout nguồn không đổi, kỳ vọng: **84 bảng**, **139 tệp**, **66.191 bản ghi**, **751 tướng**, **0 người chơi**. Không có user nào được tự động thêm.

## Kiểm thử tương thích

`.github/workflows/xampp-mariadb.yml` thực thi schema và import qua MariaDB 10.4 (gần dòng MariaDB đi kèm XAMPP) trong Docker CI. Script MySQL 8.4 gốc được kiểm thử riêng, **không bị thay đổi**. CI pass là bằng chứng cho image MariaDB đã thử, **không thể khẳng định chắc chắn mọi bản XAMPP cài trên máy đều tương thích**.

## Những phần chưa có

84 bảng là nền tảng lưu trữ được thiết kế lại. **Chưa có backend API đăng nhập, kho tướng thật, tính tiền/vật phẩm, chiến đấu và đồng bộ với Unity**. Đội hình offline đang lưu trong PlayerPrefs vẫn độc lập với database XAMPP cho tới khi có backend. Không nhập SQL này lên website thật/public hoặc dùng làm game server production khi chưa audit bảo mật, phân quyền, giao dịch.

Xem thêm `database/mysql/README.md` để tra cứu schema và nguồn dữ liệu.
