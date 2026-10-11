#!/usr/bin/env python3
"""Read-only structural tests for Home2Scene offline UI navigation.

These checks do not demonstrate Unity Editor compilation, UI render, or
the original IL2CPP game behavior. Run the Unity Play Mode matrix separately.
"""
import re
import unittest
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
SCENE_PATH = ROOT / "Assets/Scenes/Home2Scene.unity"
BRIDGE_PATH = ROOT / "Assets/Scripts/Assembly-CSharp/HomeSceneNavigationBridge.cs"
HOTKEY_PATH = ROOT / "Assets/Scripts/Assembly-CSharp/HomeSceneReturnHotkey.cs"
BRIDGE_META = ROOT / "Assets/Scripts/Assembly-CSharp/HomeSceneNavigationBridge.cs.meta"
GUID = "6486579f283b4602b1e711dc7ec913ab"
SCENES = {
    1456: "MonsterScene", 1463: "WorldScene", 1467: "LuckySpinScene",
    1483: "ForgeScene", 1496: "SummonScrollScene",
    1532: "AltarScene", 1538: "SummonOrbScene",
    1585: "ItemListScene", 1637: "CampaignMapScene",
    1699: "HeroListNewScene", 1717: "FusingHeroScene",
}
POPUPS = {
    1460: "prefabs/home/PopupDailyQuest",
    1470: "prefabs/common/PopupShop",
    1517: "prefabs/crosswordpuzzles/PopupCrosswordPuzzles",
    1521: "prefabs/friend/PopupFriend",
    1571: "prefabs/home/PopupFirstPurchase",
    1587: "prefabs/common/PopupPlayerInfo",
    1611: "prefabs/home/PopupCheckin",
    1627: "prefabs/heroskins/PopupHeroSkins",
    1657: "prefabs/common/PopupChatNew",
    1688: "prefabs/common/PopupChatNew",
    1743: "prefabs/home/PopupMailBox",
}
TOGGLES = {1444: 117, 1645: 117, 1555: 134}


class Home2NavigationChecks(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.scene = SCENE_PATH.read_text(encoding="utf-8")
        cls.bridge = BRIDGE_PATH.read_text(encoding="utf-8")
        cls.hotkey = HOTKEY_PATH.read_text(encoding="utf-8")
        cls.meta = BRIDGE_META.read_text(encoding="utf-8")
        match = re.search(
            r"(?ms)^--- !u!114 &2120\nMonoBehaviour:\n(.*?)(?=^--- !u!|\Z)",
            cls.scene,
        )
        assert match, "Home2Scene has no bridge component fileID 2120"
        cls.bridge_block = match.group(1)

    @classmethod
    def component(cls, component_id, class_id=114):
        m = re.search(
            r"(?ms)^--- !u!%d &%d\n.*?\n(.*?)(?=^--- !u!|\Z)"
            % (class_id, component_id), cls.scene,
        )
        return m.group(1) if m else None

    def test_original_callbacks_kept(self):
        self.assertEqual(self.scene.count("m_OnClick:"), 47)
        self.assertEqual(self.scene.count("m_TargetAssemblyTypeName:"), 47)

    def test_bridge_attached_to_original_scripts_game_object(self):
        scripts = self.component(12, class_id=1)
        self.assertIsNotNone(scripts)
        self.assertIn("m_Name: Scripts", scripts)
        self.assertIn("- component: {fileID: 1457}", scripts)
        self.assertEqual(scripts.count("- component: {fileID: 2120}"), 1)
        self.assertIn("m_GameObject: {fileID: 12}", self.bridge_block)
        self.assertIn("guid: " + GUID, self.bridge_block)
        self.assertIn("guid: " + GUID, self.meta)
        self.assertIn("homeScenePath: Assets/Scenes/Home2Scene.unity", self.bridge_block)

    def test_all_scene_destinations_are_real(self):
        pairs = re.findall(
            r"(?m)^  - button: \{fileID: (\d+)\}\n    scenePath: Assets/Scenes/([A-Za-z0-9]+)\.unity$",
            self.bridge_block,
        )
        self.assertEqual({int(k): v for k, v in pairs}, SCENES)
        self.assertEqual(len(pairs), len(SCENES))
        for key, value in SCENES.items():
            self.assertTrue((ROOT / "Assets/Scenes" / (value + ".unity")).is_file(), value)

    def test_popup_prefabs_exist_under_resources(self):
        pairs = re.findall(
            r"(?m)^  - button: \{fileID: (\d+)\}\n    resourcePath: (prefabs/[A-Za-z0-9/]+)$",
            self.bridge_block,
        )
        self.assertEqual({int(k): v for k, v in pairs}, POPUPS)
        self.assertEqual(len(pairs), len(POPUPS))
        for key, value in POPUPS.items():
            self.assertTrue(
                (ROOT / "Assets/Resources" / (value + ".prefab")).is_file(), value
            )

    def test_toggle_targets_are_existing_inactive_game_objects(self):
        pairs = re.findall(
            r"(?m)^  - button: \{fileID: (\d+)\}\n    target: \{fileID: (\d+)\}$",
            self.bridge_block,
        )
        self.assertEqual({int(k): int(v) for k, v in pairs}, TOGGLES)
        self.assertEqual(len(pairs), len(TOGGLES))
        for target, label in ((117, "PanelMenu"), (134, "BoxSelect")):
            block = self.component(target, class_id=1)
            self.assertIsNotNone(block)
            self.assertIn("m_Name: " + label, block)
            self.assertIn("m_IsActive: 0", block)

    def test_all_mapped_button_ids_exist_and_have_original_clicks(self):
        keys = list(SCENES) + list(POPUPS) + list(TOGGLES)
        self.assertEqual(len(keys), len(set(keys)))
        self.assertEqual(len(keys), 25)
        for button in keys:
            m = self.component(button)
            self.assertIsNotNone(m, "Missing Button component %d" % button)
            self.assertIn("m_OnClick:", m, str(button))

    def test_shared_bridge_does_not_break_home_scene(self):
        home = (ROOT / "Assets/Scenes/HomeScene.unity").read_text(encoding="utf-8")
        self.assertEqual(home.count("m_OnClick:"), 55)
        self.assertIn("public ToggleRoute[] toggleRoutes;", self.bridge)
        self.assertIn("public static bool CloseOpenToggle()", self.bridge)
        self.assertIn("if (HomeSceneNavigationBridge.CloseOpenToggle())", self.hotkey)
        self.assertIn("button.onClick.AddListener(callback)", self.bridge)
        self.assertNotIn("RemoveAllListeners", self.bridge)
        self.assertIn("UnityEngine.Debug.LogException(error, this)", self.bridge)
        self.assertIn("EditorSceneManager.LoadSceneInPlayMode", self.bridge)


if __name__ == "__main__":
    unittest.main()
