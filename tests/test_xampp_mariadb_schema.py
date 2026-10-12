"""No-server integrity contract between MySQL 8.4 and XAMPP MariaDB schemas."""
import unittest
from pathlib import Path
import re

ROOT=Path(__file__).resolve().parents[1]
MYSQL=(ROOT/"database/mysql/001_schema.sql").read_text(encoding="utf-8")
MARIADB=(ROOT/"database/xampp/001_schema_mariadb.sql").read_text(encoding="utf-8")


class MariaDBSchemaTests(unittest.TestCase):
    def test_schema_is_equivalent_except_portable_collation(self):
        actual=MARIADB[MARIADB.index("-- HTDC / KaitoKidShop MySQL 8.0.36+"):]
        expected=MYSQL.replace("utf8mb4_0900_ai_ci","utf8mb4_general_ci")
        self.assertEqual(actual,expected)
        self.assertNotIn("utf8mb4_0900_ai_ci",actual)
        self.assertIn("utf8mb4_general_ci",actual)

    def test_all_84_tables_preserved(self):
        pattern=r"CREATE TABLE IF NOT EXISTS \`([a-z_]+)\`"
        mysql_names=re.findall(pattern,MYSQL)
        mariadb_names=re.findall(pattern,MARIADB)
        self.assertEqual(len(mariadb_names),84)
        self.assertEqual(mariadb_names,mysql_names)
        self.assertEqual(len(set(mariadb_names)),84)
        self.assertIn("CREATE DATABASE IF NOT EXISTS htdc_game",MARIADB)
        for table in ("catalog_files","catalog_records","hero_definitions","accounts",
                      "player_wallets","wallet_transactions","formations","formation_slots"):
            self.assertIn(table,mariadb_names)

    def test_no_forged_player_rows(self):
        self.assertNotIn("INSERT INTO players(",MARIADB)
        self.assertNotIn("INSERT INTO player_heroes(",MARIADB)
        self.assertNotIn("INSERT INTO player_wallets(",MARIADB)


if __name__=="__main__":
    unittest.main()
