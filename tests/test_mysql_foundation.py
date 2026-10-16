"""MySQL 8 foundational data-model/importer regression tests (no live DB)."""
import importlib.util
import json
import re
import tempfile
import unittest
from pathlib import Path

ROOT=Path(__file__).resolve().parents[1]
FILE=ROOT/"database/mysql/import_configs.py"
SPEC=importlib.util.spec_from_file_location("htdc_mysql_importer",FILE)
imp=importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(imp)

class MySqlTests(unittest.TestCase):
    def test_schema_and_security(self):
        s=(ROOT/"database/mysql/001_schema.sql").read_text(encoding="utf-8")
        names=re.findall(r"CREATE TABLE IF NOT EXISTS .([a-z_]+).",s)
        self.assertEqual(len(names),84)
        self.assertEqual(len(names),len(set(names)))
        required=("catalog_files","catalog_records","hero_definitions","item_definitions",
                  "accounts","account_credentials","account_sessions","players",
                  "player_heroes","player_items","player_wallets","wallet_transactions",
                  "formations","formation_slots","battle_runs","battle_events",
                  "player_quests","guilds","arena_rankings","payment_events","audit_logs")
        for x in required:self.assertIn(x,names)
        self.assertIn("utf8mb4_0900_ai_ci",s)
        self.assertIn("UNIQUE KEY uq_reward_idempotency",s)
        self.assertIn("CHECK(slot_no BETWEEN 1 AND 6)",s)
        self.assertNotIn("INSERT INTO players(",s)
        self.assertNotIn("INSERT INTO player_wallets(",s)

    def test_utf8_unicode_sql_literals_and_invalid_json(self):
        v="Việt Nam ' \\ \n 😀"
        lit=imp.sql_text(v)
        self.assertEqual(bytes.fromhex(lit.split("0x")[1].split(" ")[0]).decode(),v)
        self.assertEqual(imp.sql_text(None),"NULL")
        with tempfile.TemporaryDirectory() as d:
            p=Path(d)/"bad.json"
            p.write_text('{"bad":',encoding="utf-8")
            with self.assertRaises(imp.ImportErrorDetail):imp.load_source(p)
            q=p.with_suffix(".txt")
            q.write_text("{not json}",encoding="utf-8")
            self.assertFalse(imp.load_source(q)[1])

    def test_utf8_bom_is_preserved_in_raw_source(self):
        with tempfile.TemporaryDirectory() as tmp:
            p=Path(tmp)/"source.json"
            payload="\ufeff"+json.dumps([{"name":"Đảo Hải Tặc"}],ensure_ascii=False)
            p.write_text(payload,encoding="utf-8")
            raw,parsed,rows=imp.load_source(p)
            self.assertEqual(raw,payload)
            self.assertTrue(parsed)
            self.assertEqual(rows[0][1]["name"],"Đảo Hải Tặc")

    def test_real_config_folders_and_hero_import(self):
        counts={}
        for folder,locale in imp.LOCALES.items():
            p=ROOT/"Assets/Resources/_pack"/folder
            counts[locale]=sum(1 for f in p.iterdir()
                              if f.suffix in (".json",".txt") and f.is_file())
        self.assertEqual(counts,{"vi":53,"en":50,"th":18,"zh":18})
        heroes=json.loads((ROOT/"Assets/Resources/_pack/db/db_heroes.json")
                         .read_text(encoding="utf-8-sig"))
        self.assertEqual(len(heroes),751)
        sql=imp.materialize("db_heroes.json",heroes[0],0)
        self.assertIn("INSERT INTO hero_definitions",sql)
        self.assertIn("source_json",sql)
        self.assertNotIn("INSERT INTO player_heroes(",sql)

    def test_idempotent_source_seed_and_no_forged_accounts(self):
        with tempfile.TemporaryDirectory() as temp:
            base=Path(temp)/"Assets/Resources/_pack"
            for folder in imp.LOCALES:(base/folder).mkdir(parents=True)
            (base/"db"/"db_heroes.json").write_text(
                json.dumps([{"id":"1","avatar":"92","name":"Killer",
                             "type":"fortress","class":"assassin",
                             "star":"6","rank":"1","release":1}]),encoding="utf-8")
            (base/"db"/"db_item_consumable.json").write_text(
                '[{"id":"1","name":"Đá EXP"}]',encoding="utf-8")
            (base/"db"/"db_FAQ.txt").write_text("non-json text",encoding="utf-8")
            for i in range(50):
                (base/"db"/f"f_{i:02d}.json").write_text("[]",encoding="utf-8")
            (base/"db_en"/"lang.json").write_text("[]",encoding="utf-8")
            (base/"db_th"/"lang.json").write_text("[]",encoding="utf-8")
            (base/"db_zh"/"lang.json").write_text("[]",encoding="utf-8")
            dest=Path(temp)/"seed.sql"
            report=imp.export(Path(temp),dest,False)
            self.assertEqual(report["stats"]["files"],56)
            self.assertEqual(report["stats"]["typed_records"],2)
            self.assertEqual(report["stats"]["raw_text_files"],1)
            source=dest.read_text(encoding="utf-8")
            self.assertEqual(source.count("START TRANSACTION;"),56)
            self.assertEqual(source.count("COMMIT;"),56)
            self.assertIn("DELETE FROM catalog_records",source)
            self.assertIn("ON DUPLICATE KEY UPDATE",source)
            self.assertNotIn("INSERT INTO players(",source)
            self.assertTrue(dest.with_suffix(".audit.json").exists())

if __name__=="__main__":
    unittest.main()
