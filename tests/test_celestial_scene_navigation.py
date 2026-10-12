#!/usr/bin/env python3
"""CelestialScene offline event wiring contract (NOT Unity Play Mode/gameplay proof).

Read-only: validates original callbacks, exact one-to-one offline route coverage,
existing Unity resources, P0 regression, and absence of fabricated gameplay.
"""
import re
import unittest
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
SCENE = ROOT / "Assets/Scenes/CelestialScene.unity"
BRIDGE = ROOT / "Assets/Scripts/Assembly-CSharp/HomeSceneNavigationBridge.cs"
BRIDGE_META = ROOT / "Assets/Scripts/Assembly-CSharp/HomeSceneNavigationBridge.cs.meta"
BRIDGE_GUID = "6486579f283b4602b1e711dc7ec913ab"

# Source Button.onClick pairs: Button fileID -> (original method target, method).
# No method name, target, event argument or persistent call may be replaced.
ORIGINAL = {
    675: (677, "IslandClick"),
    694: (766, "CollectClick"),
    706: (746, "BuildClick"),
    719: (746, "BuildClick"),
    723: (780, "BackClick"),
    727: (739, "AddClick"),
    732: (780, "TowerClick"),
    744: (789, "BuildClick"),
    748: (780, "AddDaffodil"),
    764: (780, "HelpClick"),
    769: (766, "MineClick"),
    774: (766, "MineClick"),
    784: (692, "BuildClick"),
    787: (692, "GetDaffodil"),
    796: (692, "DaffodilClick"),
    801: (686, "IslandBossClick"),
    825: (789, "BuildClick"),
    829: (678, "AddClick"),
    831: (780, "SmashClick"),
    860: (883, "CollectClick"),
    862: (891, "CollectClick"),
    868: (893, "CollectClick"),
    871: (920, "CollectClick"),
    873: (907, "CollectClick"),
    886: (883, "MineClick"),
    888: (883, "MineClick"),
    897: (891, "MineClick"),
    900: (891, "MineClick"),
    901: (893, "MineClick"),
    904: (893, "MineClick"),
    908: (920, "MineClick"),
    912: (920, "MineClick"),
    915: (907, "MineClick"),
    918: (907, "MineClick"),
    952: (950, "IslandClick"),
    955: (954, "IslandClick"),
    978: (979, "IslandClick"),
    982: (983, "IslandClick"),
    994: (996, "IslandClick"),
    1001: (1003, "IslandClick"),
    1015: (1016, "IslandClick"),
    1022: (1021, "IslandClick"),
    1027: (1029, "IslandClick"),
    1043: (1042, "IslandClick"),
    1052: (1051, "IslandClick"),
    1067: (1068, "IslandClick"),
    1078: (1077, "IslandClick"),
}
SCENE_ROUTES = {723: "HomeScene"}
POPUP_ROUTES = {727: "prefabs/common/PopupNap", 829: "prefabs/common/PopupNap"}
ISLANDS = {675, 952, 955, 978, 982, 994, 1001, 1015, 1022, 1027, 1043, 1052, 1067, 1078}
MINING_COLLECT = {694, 860, 862, 868, 871, 873}
MINING_SLOTS = {769, 886, 900, 904, 908, 915}
MINING_BUILDINGS = {774, 888, 897, 901, 912, 918}
TECH_BUILDINGS = {706, 719, 744, 825}
DAFFODIL = {784, 787, 796}
OTHER_NOTICES = {732, 748, 764, 801, 831}
NOTICES = ISLANDS | MINING_COLLECT | MINING_SLOTS | MINING_BUILDINGS | TECH_BUILDINGS | DAFFODIL | OTHER_NOTICES

BLOCK = re.compile(r"(?ms)^--- !u!(\d+) &(\d+)\n(.*?)(?=^--- !u!|\Z)")
CALL = re.compile(r"m_Target:\s*\{fileID:\s*(-?\d+)[^}]*\}[\s\S]*?m_MethodName:\s*([^\r\n]*)")


class CelestialOfflineNavigationTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.raw = SCENE.read_text(encoding="utf-8")
        cls.bridge = BRIDGE.read_text(encoding="utf-8")
        cls.meta = BRIDGE_META.read_text(encoding="utf-8")
        cls.blocks = {(int(kind), int(ident)): body
                      for kind, ident, body in BLOCK.findall(cls.raw)}
        cls.bridge_yaml = cls.blocks[(114, 1106)]

    def test_original_47_persistent_callbacks_untouched(self):
        actual = {}
        for (kind, ident), body in self.blocks.items():
            if kind != 114 or "m_OnClick:" not in body:
                continue
            matches = CALL.findall(body.split("m_OnClick:", 1)[1])
            self.assertEqual(len(matches), 1, "original callback count changed on " + str(ident))
            actual[ident] = (int(matches[0][0]), matches[0][1].strip())
        self.assertEqual(actual, ORIGINAL)
        self.assertEqual(self.raw.count("m_OnClick:"), 47)
        self.assertEqual(self.raw.count("m_TargetAssemblyTypeName:"), 47)

    def test_bridge_attached_to_original_scripts_object(self):
        scripts = self.blocks[(1, 62)]
        self.assertIn("m_Name: Scripts", scripts)
        self.assertIn("- component: {fileID: 780}", scripts)
        self.assertEqual(scripts.count("- component: {fileID: 1106}"), 1)
        self.assertIn("m_GameObject: {fileID: 62}", self.bridge_yaml)
        self.assertIn("guid: " + BRIDGE_GUID, self.bridge_yaml)
        self.assertIn("guid: " + BRIDGE_GUID, self.meta)
        self.assertIn("homeScenePath: Assets/Scenes/HomeScene.unity", self.bridge_yaml)

    def test_exactly_once_47_event_routes(self):
        destinations = re.findall(
            r"(?m)^  - button: \{fileID: (\d+)\}\n    scenePath: Assets/Scenes/(\w+)\.unity$",
            self.bridge_yaml,
        )
        popups = re.findall(
            r"(?m)^  - button: \{fileID: (\d+)\}\n    resourcePath: (prefabs/[\w/]+)$",
            self.bridge_yaml,
        )
        notices = re.findall(
            r"(?m)^  - button: \{fileID: (\d+)\}\n    title: ([^\n]+)\n    message: ([^\n]+)$",
            self.bridge_yaml,
        )
        self.assertEqual({int(k): v for k, v in destinations}, SCENE_ROUTES)
        self.assertEqual({int(k): v for k, v in popups}, POPUP_ROUTES)
        self.assertEqual({int(k) for k, _, _ in notices}, NOTICES)
        self.assertEqual((len(destinations), len(popups), len(notices)), (1, 2, 44))
        all_keys = [int(k) for k, _ in destinations] + [int(k) for k, _ in popups] + [int(k) for k, _, _ in notices]
        self.assertEqual(len(all_keys), 47)
        self.assertEqual(len(set(all_keys)), 47)
        self.assertEqual(set(all_keys), set(ORIGINAL))
        self.assertIn("toggleRoutes: []", self.bridge_yaml)

    def test_real_targets_and_no_synthetic_purchase(self):
        for scene in SCENE_ROUTES.values():
            self.assertTrue((ROOT / "Assets/Scenes" / (scene + ".unity")).is_file())
        for target in POPUP_ROUTES.values():
            prefab = ROOT / "Assets/Resources" / (target + ".prefab")
            self.assertTrue(prefab.is_file(), target)
            self.assertIn("m_Name: ButtonClose", prefab.read_text(encoding="utf-8"))
        self.assertTrue((ROOT / "Assets/Resources/prefabs/common/PopupMessage.prefab").is_file())
        self.assertIn('OpenPopup("prefabs/common/PopupMessage", title, message)', self.bridge)
        self.assertIn("public NoticeRoute[] noticeRoutes;", self.bridge)
        self.assertIn("button.onClick.AddListener(callback)", self.bridge)
        self.assertNotIn("RemoveAllListeners", self.bridge)

    def test_notices_explicitly_block_unavailable_game_state(self):
        notices = re.findall(
            r"(?m)^  - button: \{fileID: (\d+)\}\n    title: ([^\n]+)\n    message: ([^\n]+)$",
            self.bridge_yaml,
        )
        for button_id, title, message in notices:
            self.assertTrue(title.strip(), button_id)
            self.assertGreater(len(message.strip()), 30, button_id)
            self.assertTrue("Thiếu" in message or "Chưa" in message,
                            "missing honest blocker on " + button_id)
        self.assertEqual(len(NOTICES), 44)
        self.assertIn("UnityEngine.Debug.LogException(error, this)", self.bridge)

    def test_p0_scenes_still_have_original_callback_counts(self):
        home = (ROOT / "Assets/Scenes/HomeScene.unity").read_text(encoding="utf-8")
        home2 = (ROOT / "Assets/Scenes/Home2Scene.unity").read_text(encoding="utf-8")
        self.assertEqual(home.count("m_OnClick:"), 55)
        self.assertEqual(home2.count("m_OnClick:"), 48)  # Home2: 47 original + 1 new Activity
        self.assertIn("public static bool CloseOpenPopup()", self.bridge)
        self.assertIn("EditorSceneManager.LoadSceneInPlayMode", self.bridge)


if __name__ == "__main__":
    unittest.main()
