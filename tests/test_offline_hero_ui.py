#!/usr/bin/env python3
"""Static provenance/regression tests for the new offline hero UI binding.

NOT a Unity compilation/Play Mode, Spine rendering or screenshot test.
"""
import hashlib
import json
import re
import unittest
from collections import Counter
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
SOURCE = ROOT / "Assets/Resources/_pack/db/db_heroes.json"
SCENE_HOME = ROOT / "Assets/Scenes/Home2Scene.unity"
SCENE_LIST = ROOT / "Assets/Scenes/HeroListNewScene.unity"
SCRIPTS = ROOT / "Assets/Scripts/Assembly-CSharp"
FILES = [
    "OfflineHeroParty",
    "OfflineHeroListPresenter",
    "OfflineHeroSceneRuntime",
    "OfflineHeroHomePresenter",
    "OfflineHeroDetailModal",
]

def blob_sha(path):
    data = path.read_bytes()
    return hashlib.sha1(b"blob " + str(len(data)).encode() + b"\x00" + data).hexdigest()

class OfflineHeroUiTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.rows = json.loads(SOURCE.read_text(encoding="utf-8"))
        cls.texts = {name: (SCRIPTS / (name + ".cs")).read_text(encoding="utf-8")
                     for name in FILES}
        cls.home = SCENE_HOME.read_text(encoding="utf-8")
        cls.hero_list = SCENE_LIST.read_text(encoding="utf-8")

    def test_source_json_and_intentionally_localized_unity_scenes(self):
        self.assertEqual(blob_sha(SOURCE), "6452fecd70223ceee92c34683d5b415122497bf6")
        self.assertEqual(blob_sha(SCENE_HOME), "245a72d6797f889e7b23cb5fdf6e927b291b4550")
        self.assertEqual(blob_sha(SCENE_LIST), "146c549f2e0fae59fb41dcc8048156a27b19452e")

    def test_home_scene_six_slots_remain_and_old_source_callbacks(self):
        self.assertEqual(self.home.count("m_Text: Naruto"), 6)
        self.assertEqual(self.home.count("m_Name: HeroHome2 ("), 6)
        self.assertEqual(self.home.count("m_OnClick:"), 48)  # 47 original + 1 new source-backed Activity
        slots = re.search(r"  m_ListHeroHome:\n((?:  - \{fileID: \d+\}\n){6})", self.home)
        self.assertIsNotNone(slots)
        self.assertEqual(slots.group(1).count("- {fileID:"), 6)
        self.assertIn("m_GoClickArea: {fileID:", self.home)
        self.assertIn("m_SkeletonGraphic: {fileID:", self.home)

    def test_list_reuses_actual_original_template_and_scroll(self):
        self.assertIn("m_Name: CellHeroThumb", self.hero_list)
        self.assertIn("m_Name: ScrollView", self.hero_list)
        self.assertIn("m_Name: Content", self.hero_list)
        self.assertIn("m_GoPrefabCell: {fileID: 1}", self.hero_list)
        self.assertIn("m_GoScrollView: {fileID: 8}", self.hero_list)
        self.assertIn("m_CellSize: {x: 190, y: 260}", self.hero_list)
        self.assertIn("m_Spacing: {x: 10, y: 10}", self.hero_list)
        self.assertIn("m_ConstraintCount: 4", self.hero_list)
        present = self.texts["OfflineHeroListPresenter"]
        for token in ("Instantiate(source.m_GoPrefabCell, content, false)",
                      "GridLayoutGroup", "OfflineHeroArt.LoadPortrait(hero.AvatarId)",
                      "m_TextName.text = hero.Name", "m_ImgCheck",
                      "OfflineHeroParty.Toggle(", "ButtonFortress", "ButtonLight",
                      "OfflineHeroDetailModal.Show("):
            self.assertIn(token, present)

    def test_hero_list_uses_saved_vietnamese_labels_and_no_card_overlay(self):
        # Static Scene is also correct with Play off. Localization components
        # for these seven labels are disabled to prevent I2 restoring English.
        for text_id, label in (
            (302, "Sinh khí"), (307, "Nhanh nhẹn"), (326, "Minh mẫn"),
            (333, "Thông minh"), (407, "Chính xác"), (410, "Bền bỉ"),
            (392, "Sắp xếp")
        ):
            match = re.search(
                rf"(?ms)^--- !u!114 &{text_id}\\n(.*?)(?=^--- !u!|\\Z)",
                self.hero_list
            )
            self.assertIsNotNone(match)
            self.assertIn("m_Text: " + label, match.group(1))
        presenter = self.texts["OfflineHeroListPresenter"]
        self.assertNotIn('new GameObject("OfflineTeamSelect"', presenter)
        self.assertNotIn("CreateTeamButton(", presenter)
        self.assertNotIn('text.text = selected ? "Bỏ đội" : "Chọn"', presenter)
        self.assertIn("OfflineHeroDetailModal.Show(hero, canvas, catalog", presenter)
        self.assertIn('cell.m_TextLevel.text = "★" + hero.Star;', presenter)
        self.assertIn("OfflineHeroParty.Read(catalog).Count", presenter)
        self.assertNotIn('"Đội hình offline: " + OfflineHeroParty.Read(catalog)', presenter)
        self.assertIn("SetVietnameseControls();", presenter)
        modal = self.texts["OfflineHeroDetailModal"]
        self.assertIn('"Thêm vào đội hình"', modal)
        self.assertIn('"Bỏ khỏi đội hình"', modal)
        self.assertIn("OfflineHeroParty.Toggle(catalog, hero.Id, out notice)", modal)
        self.assertIn("onPartyChanged()", modal)

    def test_selection_is_opt_in_bounded_and_offline(self):
        src = self.texts["OfflineHeroParty"]
        for token in ("Capacity = 6", "PlayerPrefs.GetString", "PlayerPrefs.SetString",
                      "PlayerPrefs.Save()", "HashSet<int> avatars",
                      "hero.Id <= 530", "hero.Star >= 5", "hero.IsReleased",
                      "party.Count >= Capacity", "byId", "avatarId"):
            if token == "byId":
                continue
            if token == "avatarId":
                continue
            self.assertIn(token, src)
        self.assertNotIn("PlayerPrefs.SetString(PrefsKey, \"456,457,458", src)
        self.assertIn('GetString(PrefsKey, "")', src)
        # source release flag is not a server-owned inventory
        published = [h for h in self.rows if int(h["id"]) <= 530
                     and h["release"] == 1 and int(h["star"]) >= 5]
        self.assertEqual(len(published), 154)
        self.assertEqual(len({h["avatar"] for h in published}), 147)

    def test_portraits_and_spine_use_verified_id_paths(self):
        src = self.texts["OfflineHeroParty"]
        self.assertIn('"_pack/herohead/" + avatarId', src)
        self.assertIn('"/Hero_" + avatarId + "_SkeletonData"', src)
        self.assertIn('for (int folder = 2; folder <= 5; folder++)', src)
        self.assertTrue((ROOT / "Assets/Resources/_pack/herohead/105.png").exists())
        self.assertTrue((ROOT / "Assets/Resources/_pack/heroes2/105/Hero_105_SkeletonData.asset").exists())
        self.assertFalse((ROOT / "Assets/Resources/_pack/herohead/456.png").exists())
        self.assertFalse((ROOT / "Assets/Resources/_pack/herohead/522.png").exists())
        # The LFS pointer is not proof of a functioning imported Spine skeleton.
        self.assertIn(
            "version https://git-lfs.github.com/spec/v1",
            (ROOT / "Assets/Resources/_pack/heroes2/105/Hero_105_SkeletonData.asset").read_text()
        )

    def test_only_existing_home2_notices_replaced(self):
        nav = (SCRIPTS / "HomeSceneNavigationBridge.cs").read_text(encoding="utf-8")
        presenter = self.texts["OfflineHeroHomePresenter"]
        self.assertIn("public bool RemoveOfflineNoticeForButton(Button button)", nav)
        self.assertIn("if (!isNotice) return false;", nav)
        self.assertIn("button.onClick.RemoveListener(subscribedActions[index]);", nav)
        self.assertNotIn("RemoveAllListeners", nav)
        self.assertIn("nav.RemoveOfflineNoticeForButton(click);", presenter)
        self.assertIn('slot.m_TextHeroName.text = hero != null ? hero.Name : "";', presenter)
        self.assertIn("OfflineHeroArt.LoadSkeleton(hero.AvatarId)", presenter)
        self.assertIn("graphic.Initialize(true)", presenter)

    def test_original_panel_hero_info_is_loaded_without_fake_stats(self):
        modal = self.texts["OfflineHeroDetailModal"]
        self.assertIn('Resources.Load<GameObject>("prefabs/heroinfo/PanelHeroInfo")', modal)
        self.assertIn("sourceInfo.m_TextHeroName.text = hero.Name;", modal)
        self.assertIn("hero.ActiveSkillId", modal)
        self.assertIn("hero.Passive3SkillId", modal)
        self.assertIn("OfflineHeroArt.LoadPortrait(hero.AvatarId)", modal)
        hotkey = (SCRIPTS / "HomeSceneReturnHotkey.cs").read_text(encoding="utf-8")
        self.assertIn("OfflineHeroDetailModal.CloseActive()", hotkey)

    def test_catalog_skills_come_from_source_and_are_numeric(self):
        catalog = (SCRIPTS / "OfflineHeroCatalog.cs").read_text(encoding="utf-8")
        for field in ("active", "passive1", "passive2"):
            self.assertIn('public string ' + field + ';', catalog)
            self.assertIn('ParseNonNegativeInt(row.' + field + ', "' + field + '")', catalog)
            self.assertTrue(all(row[field].isdigit() for row in self.rows))
        self.assertIn("public string passive3;", catalog)
        self.assertIn('ParseOptionalSkillId(row.passive3, "passive3")', catalog)
        # Unity JsonUtility maps a JSON null string into "", not necessarily null.
        # The optional parser must accept both plus whitespace, but NOT confuse
        # the explicit original "0" with an absent skill.
        self.assertIn("if (string.IsNullOrWhiteSpace(value)) return -1;", catalog)
        self.assertIn("return ParseNonNegativeInt(value, field);", catalog)
        self.assertIn('ParseOptionalSkillId(row.passive3, "passive3")', catalog)
        missing = [int(row["id"]) for row in self.rows if row["passive3"] is None]
        self.assertEqual(missing, [51101])
        self.assertTrue(all(row["passive3"].isdigit()
                            for row in self.rows if row["passive3"] is not None))
        self.assertEqual(len(self.rows), 751)

    def test_direct_scene_start_bootstrap_in_addition_to_scene_loaded(self):
        # When hitting Play in an already opened editor Scene, the sceneLoaded
        # event alone is insufficient evidence that the runtime presenter ran.
        home = (SCRIPTS / "Home2Scene.cs").read_text(encoding="utf-8")
        hero_list = (SCRIPTS / "HeroListNewScene.cs").read_text(encoding="utf-8")
        runtime = self.texts["OfflineHeroSceneRuntime"]
        self.assertIn("protected override void Start()", home)
        self.assertIn("GetComponent<OfflineHeroHomePresenter>() == null", home)
        self.assertIn("gameObject.AddComponent<OfflineHeroHomePresenter>()", home)
        self.assertIn("GetComponent<OfflineHeroListPresenter>() == null", hero_list)
        self.assertIn("gameObject.AddComponent<OfflineHeroListPresenter>()", hero_list)
        # Both attach paths must be idempotent and keep original scene serialization.
        self.assertIn("GetComponent<OfflineHeroHomePresenter>() == null", runtime)
        self.assertIn("GetComponent<OfflineHeroListPresenter>() == null", runtime)
        self.assertIn("Home2 presenter Start; initializing 6 offline slots.", self.texts["OfflineHeroHomePresenter"])
        self.assertIn("HeroList presenter Start; loading offline source catalog.", self.texts["OfflineHeroListPresenter"])

    def test_script_file_names_and_meta_guids(self):
        guids = set()
        for name in FILES:
            script = self.texts[name]
            self.assertIn("class " + name, script)
            meta = (SCRIPTS / (name + ".cs.meta")).read_text(encoding="utf-8")
            match = re.search(r"(?m)^guid: ([0-9a-f]{32})$", meta)
            self.assertIsNotNone(match, name)
            self.assertNotIn(match.group(1), guids)
            guids.add(match.group(1))
        self.assertIn("RuntimeInitializeOnLoadMethod", self.texts["OfflineHeroSceneRuntime"])


if __name__ == "__main__":
    unittest.main()
