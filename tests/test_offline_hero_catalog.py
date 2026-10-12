#!/usr/bin/env python3
"""Read-only source-data and C# contract checks for the offline Unity hero catalog.

These checks do NOT prove Unity compilation, JsonUtility behavior, UI rendering,
player ownership, asset resolution or gameplay.
"""
import json
import re
import unittest
from collections import Counter
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
DATA = ROOT / "Assets/Resources/_pack/db/db_heroes.json"
CODE = ROOT / "Assets/Scripts/Assembly-CSharp/OfflineHeroCatalog.cs"
META = ROOT / "Assets/Scripts/Assembly-CSharp/OfflineHeroCatalog.cs.meta"
FACTIONS = {"fortress", "abyss", "forest", "shadow", "dark", "light"}


class OfflineHeroCatalogSourceTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.rows = json.loads(DATA.read_text(encoding="utf-8"))
        cls.code = CODE.read_text(encoding="utf-8")
        cls.meta = META.read_text(encoding="utf-8")

    def test_data_is_real_array_and_ids_are_unique(self):
        self.assertIsInstance(self.rows, list)
        self.assertEqual(len(self.rows), 751)
        ids = [int(row["id"]) for row in self.rows]
        self.assertEqual(len(ids), len(set(ids)))
        self.assertTrue(all(number > 0 for number in ids))
        self.assertTrue(all(row["name"].strip() for row in self.rows))

    def test_factions_are_exactly_six_after_case_normalization(self):
        counts = Counter(row["type"].strip().lower() for row in self.rows)
        self.assertEqual(set(counts), FACTIONS)
        self.assertEqual(
            counts,
            {
                "fortress": 135,
                "abyss": 142,
                "forest": 133,
                "shadow": 131,
                "dark": 93,
                "light": 117,
            },
        )
        # At least one source variant must actually require normalization.
        self.assertTrue(any(row["type"] != row["type"].lower() for row in self.rows))

    def test_stars_are_parsed_without_inventing_missing_levels(self):
        stars = {int(row["star"]) for row in self.rows}
        self.assertEqual(stars, {0, 1, 2, 3, 4, 5, 6, 9, 10})
        self.assertTrue(all(str(int(row["star"])) == row["star"] for row in self.rows))

    def test_release_is_source_flag_not_inventory(self):
        counts = Counter(row["release"] for row in self.rows)
        self.assertEqual(counts, {0: 548, 1: 203})
        self.assertEqual(sum(counts.values()), len(self.rows))
        self.assertIn("NOT proof that the player owns", self.code)

    def test_known_heroes_preserved_verbatim(self):
        by_id = {int(row["id"]): row for row in self.rows}
        self.assertEqual(
            (by_id[458]["name"], int(by_id[458]["star"]), by_id[458]["release"]),
            ("Uta", 10, 1),
        )
        self.assertEqual(
            (by_id[524]["name"], int(by_id[524]["star"]), by_id[524]["release"]),
            ("Kozuki Hiyori", 10, 1),
        )
        self.assertEqual(
            (by_id[530]["name"], int(by_id[530]["star"]), by_id[530]["release"]),
            ("Basil Hawkins", 10, 0),
        )
        # 751 records are not 751 distinct playable/owned characters.
        self.assertLess(len({row["name"] for row in self.rows}), len(self.rows))

    def test_every_record_has_parseable_catalog_fields(self):
        for row in self.rows:
            with self.subTest(id=row["id"]):
                self.assertGreater(int(row["id"]), 0)
                self.assertGreaterEqual(int(row["avatar"]), 0)
                self.assertGreaterEqual(int(row["star"]), 0)
                self.assertGreaterEqual(int(row["rank"]), 0)
                self.assertIn(row["release"], (0, 1))
                self.assertIn(row["type"].strip().lower(), FACTIONS)

    def test_unity_source_contract_and_guid(self):
        self.assertTrue(CODE.exists())
        self.assertIn('ResourcePath = "_pack/db/db_heroes"', self.code)
        self.assertIn("Resources.Load<TextAsset>(ResourcePath)", self.code)
        self.assertIn("JsonUtility.FromJson<HeroDbEnvelope>", self.code)
        self.assertIn('"{\\\"rows\\\":" + json + "}"', self.code)
        self.assertIn("public bool TryGetById", self.code)
        self.assertIn("public List<OfflineHeroRecord> Query", self.code)
        self.assertIn("if (index.ContainsKey(id))", self.code)
        self.assertIn("result.Sort((left, right) => left.Id.CompareTo(right.Id))", self.code)
        self.assertIn('MenuItem("Tools/HTDC/Offline Heroes/Validate db_heroes.json")', self.code)
        self.assertRegex(self.meta, r"(?m)^guid: [0-9a-f]{32}$")

    def test_unity_log_exception_has_explicit_namespace(self):
        # AssetRipper project may contain another Debug class; avoid CS0117.
        self.assertIn("UnityEngine.Debug.LogException(ex);", self.code)
        self.assertNotIn("            Debug.LogException(ex);", self.code)

    def test_stage_one_does_not_claim_backend_or_gameplay(self):
        self.assertIn("NOT a player inventory", self.code)
        self.assertIn("does not mutate the scene", self.code)
        self.assertIn("NEW_PROJECT_DESIGN", self.code)


if __name__ == "__main__":
    unittest.main()
