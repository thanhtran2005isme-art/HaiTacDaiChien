#!/usr/bin/env python3
"""Static safety checks for HomeScene navigation recovery pilot (not a Unity runtime test)."""
import re
import unittest
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
SCENE = ROOT / "Assets/Scenes/HomeScene.unity"
SCRIPT = ROOT / "Assets/Scripts/Assembly-CSharp/HomeSceneNavigationBridge.cs"
META = ROOT / "Assets/Scripts/Assembly-CSharp/HomeSceneNavigationBridge.cs.meta"
GUID = "6486579f283b4602b1e711dc7ec913ab"
ROUTES = {
    887: "GuildScene", 891: "ItemListScene", 1025: "MonsterScene",
    927: "AltarScene", 1158: "AltarScene",
    933: "ArenaScene", 1036: "ArenaScene",
    1204: "DungeonScene", 919: "DungeonScene",
    1145: "FusingHeroScene", 1165: "FusingHeroScene",
    1211: "TarvenScene", 879: "TarvenScene",
    1010: "SealedLand", 1181: "SealedLand",
    1097: "ForgeScene", 936: "ForgeScene",
    1042: "TowerNewScene", 1124: "TowerNewScene",
    1047: "HeroListNewScene", 1109: "CampaignMapScene",
    964: "LuckySpinScene", 1147: "LuckySpinScene",
    978: "BraveTrialScene", 1171: "BraveTrialScene",
    1007: "SummonOrbScene", 1120: "SummonOrbScene",
}
POPUP_ROUTES = {
    1022: "prefabs/home/PopupDailyQuest",
    1076: "prefabs/home/PopupMailBox",
    1207: "prefabs/friend/PopupFriend",
    1040: "prefabs/home/PopupCheckin",
    1071: "prefabs/home/PopupEventRaid",
    951: "prefabs/common/PopupShop",
    1060: "prefabs/common/PopupShop",
    1169: "prefabs/common/PopupShop",
    997: "prefabs/common/PopupChatNew",
}

class HomeNavigationChecks(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.scene = SCENE.read_text(encoding="utf-8")
        cls.script = SCRIPT.read_text(encoding="utf-8")
        cls.meta = META.read_text(encoding="utf-8")

    def test_original_onclick_count_preserved(self):
        self.assertEqual(self.scene.count("m_OnClick:"), 55)
        self.assertEqual(self.scene.count("m_TargetAssemblyTypeName:"), 55)

    def test_bridge_attached_to_scripts_gameobject(self):
        go = re.search(r"(?ms)^--- !u!1 &5\nGameObject:\n(.*?)(?=^--- !u!)", self.scene)
        self.assertIsNotNone(go)
        self.assertIn("m_Name: Scripts", go.group(1))
        self.assertEqual(go.group(1).count("- component: {fileID: 1385}"), 1)
        bridge = re.search(r"(?ms)^--- !u!114 &1385\nMonoBehaviour:\n(.*?)(?=^--- !u!|\Z)", self.scene)
        self.assertIsNotNone(bridge)
        self.assertIn("m_GameObject: {fileID: 5}", bridge.group(1))
        self.assertIn("guid: " + GUID, bridge.group(1))
        self.assertIn("guid: " + GUID, self.meta)

    def test_unique_valid_buttons_and_scene_assets(self):
        bridge = re.search(r"(?ms)^--- !u!114 &1385\nMonoBehaviour:\n(.*?)(?=^--- !u!|\Z)", self.scene)
        entries = re.findall(
            r"(?m)^  - button: \{fileID: (\d+)\}\n    scenePath: Assets/Scenes/([A-Za-z0-9]+)\.unity$",
            bridge.group(1))
        actual = {int(k): v for k, v in entries}
        self.assertEqual(len(actual), len(entries))
        self.assertEqual(actual, ROUTES)
        for button, name in ROUTES.items():
            m = re.search(r"(?ms)^--- !u!114 &%d\nMonoBehaviour:\n(.*?)(?=^--- !u!|\Z)" % button, self.scene)
            self.assertIsNotNone(m, str(button))
            self.assertIn("m_OnClick:", m.group(1), str(button))
            self.assertTrue((ROOT / "Assets/Scenes" / (name + ".unity")).is_file(), name)

    def test_popup_routes_and_resources(self):
        bridge = re.search(r"(?ms)^--- !u!114 &1385\nMonoBehaviour:\n(.*?)(?=^--- !u!|\Z)", self.scene)
        self.assertIsNotNone(bridge)
        body = bridge.group(1)
        self.assertIn("  popupRoutes:\n", body)
        candidates = re.findall(
            r"(?m)^  - button: \{fileID: (\d+)\}\n    resourcePath: (prefabs/[A-Za-z0-9/]+)$",
            body,
        )
        actual = {int(key): path for key, path in candidates}
        self.assertEqual(actual, POPUP_ROUTES)
        self.assertEqual(len(candidates), len(actual))
        self.assertTrue(set(ROUTES).isdisjoint(POPUP_ROUTES))
        for button, path in POPUP_ROUTES.items():
            self.assertTrue((ROOT / "Assets/Resources" / (path + ".prefab")).is_file(), path)
            match = re.search(
                r"(?ms)^--- !u!114 &%d\nMonoBehaviour:\n(.*?)(?=^--- !u!|\Z)" % button,
                self.scene,
            )
            self.assertIsNotNone(match, str(button))
            self.assertIn("m_OnClick:", match.group(1), str(button))

    def test_popup_close_is_safe_and_original_callbacks_kept(self):
        self.assertIn('Resources.Load<GameObject>(resourcePath)', self.script)
        self.assertIn('activePopupLayer', self.script)
        self.assertIn('blocker.raycastTarget = true;', self.script)
        self.assertIn('button.onClick.AddListener(() => { CloseOpenPopup(); });', self.script)
        self.assertIn('public static bool CloseOpenPopup()', self.script)
        hotkey = (ROOT / "Assets/Scripts/Assembly-CSharp/HomeSceneReturnHotkey.cs").read_text(encoding="utf-8")
        self.assertIn('if (HomeSceneNavigationBridge.CloseOpenPopup())', hotkey)
        self.assertNotIn('RemoveAllListeners', self.script)

    def test_group_button_event_missing_script_stub_removed(self):
        # AssetRipper serialized an unusable MonoBehaviour with m_Script: {fileID: 0}.
        # Keep the two original Unity layout behaviours untouched.
        owner = re.search(
            r"(?ms)^--- !u!1 &136\nGameObject:\n(.*?)(?=^--- !u!)",
            self.scene,
        )
        self.assertIsNotNone(owner)
        self.assertIn("m_Name: GroupButtonEvent", owner.group(1))
        self.assertIn("- component: {fileID: 1140}", owner.group(1))
        self.assertIn("- component: {fileID: 1142}", owner.group(1))
        self.assertNotIn("- component: {fileID: 1141}", owner.group(1))
        self.assertNotRegex(self.scene, r"(?m)^--- !u!114 &1141$")
        self.assertNotRegex(self.scene, r"(?m)^  m_Script: \{fileID: 0\}$")

    def test_bridge_uses_real_unity_debug_not_decompiled_stub(self):
        # AssetRipper project defines its own global Debug class without LogException.
        # Unqualified Debug calls silently disappear or can fail compilation.
        self.assertIn("UnityEngine.Debug.LogException(error, this);", self.script)
        for method in ("Log(", "LogWarning(", "LogError(", "LogException("):
            self.assertNotIn("            Debug." + method, self.script)
        self.assertNotIn("Debug.LogException(error, this);", self.script.replace(
            "UnityEngine.Debug.LogException(error, this);", ""))

    def test_no_original_event_replacement(self):
        self.assertIn("button.onClick.AddListener(callback)", self.script)
        self.assertNotIn("RemoveAllListeners", self.script)
        self.assertIn("EditorSceneManager.LoadSceneInPlayMode", self.script)
        self.assertTrue((ROOT / "Assets/Scripts/Assembly-CSharp/HomeSceneReturnHotkey.cs").is_file())

if __name__ == "__main__":
    unittest.main()
