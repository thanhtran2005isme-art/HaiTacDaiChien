#!/usr/bin/env python3
"""Static regression for the user-requested Home2 HUD comparison.

Checks the original Unity Scene bytes and authenticated source Sprite identity.
It does NOT run Unity Editor, validate rendered pixels, or restore gameplay.
"""
import hashlib
import re
import unittest
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
SRC = ROOT / "Assets/Scripts/Assembly-CSharp"
SCENE = ROOT / "Assets/Scenes/Home2Scene.unity"
ORIGINAL_HOME = ROOT / "Assets/Scenes/HomeScene.unity"
ICON_ASSET_META = ROOT / (
    "Assets/Resources/images/home/homebutton/home_button_events.asset.meta"
)
HUD = SRC / "OfflineHome2HudCorrection.cs"
HUD_META = SRC / "OfflineHome2HudCorrection.cs.meta"


def git_blob_sha(path):
    data = path.read_bytes()
    return hashlib.sha1(
        b"blob " + str(len(data)).encode() + b"\x00" + data
    ).hexdigest()


class Home2HudSourceContract(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.scene = SCENE.read_text(encoding="utf-8")
        cls.hud = HUD.read_text(encoding="utf-8")
        cls.home = ORIGINAL_HOME.read_text(encoding="utf-8")

    def test_intentional_edit_mode_home2_scene_is_persisted(self):
        self.assertEqual(
            git_blob_sha(SCENE),
            "245a72d6797f889e7b23cb5fdf6e927b291b4550",
        )
        self.assertEqual(self.scene.count("m_OnClick:"), 48)
        self.assertEqual(self.scene.count("m_TargetAssemblyTypeName:"), 47)
        self.assertEqual(self.scene.count("m_Text: Naruto"), 6)

    def test_canvas_is_resolved_from_sibling_scene_roots(self):
        # Serialized GameObjects 12 (Scripts) and 121 (Canvas) both have
        # RectTransforms with m_Father {fileID: 0}, NOT parent/child.
        # Runtime MonoBehaviour sits under Scripts; transform.root.Find
        # cannot reach the separate Canvas scene root.
        def object_block(kind, file_id):
            pattern = re.compile(
                rf"(?ms)^--- !u!{kind} &{file_id}\n(.*?)(?=^--- !u!|\Z)"
            )
            match = pattern.search(self.scene)
            self.assertIsNotNone(match, f"Unity {kind} object {file_id}")
            return match.group(1)

        self.assertIn("m_Name: Scripts", object_block(1, 12))
        self.assertIn("m_Name: Canvas", object_block(1, 121))
        self.assertIn("m_Father: {fileID: 0}", object_block(4, 510))
        self.assertIn("m_Father: {fileID: 0}", object_block(224, 1024))
        self.assertIn("m_GameObject: {fileID: 121}", object_block(223, 908))
        self.assertIn("home.gameObject.scene.GetRootGameObjects()", self.hud)
        self.assertIn('root.name == "Canvas"', self.hud)
        self.assertIn("root.GetComponent<Canvas>() != null", self.hud)
        self.assertNotIn('home.transform.root.Find("Canvas")', self.hud)
        self.assertIn("Scene Canvas located", self.hud)

    def test_two_fb_objects_have_source_proven_sprite_identity(self):
        for name in ("ButtonTest", "ButtonFb", "PanelHomeTopRight"):
            self.assertIn("m_Name: " + name, self.scene)
        # Both serialized Image components use the same source Sprite object.
        sprite_line = (
            "m_Sprite: {fileID: 21300000, "
            "guid: d92d3ea43dc24764984b369b7761206e, type: 2}"
        )
        self.assertEqual(self.scene.count(sprite_line), 2)
        self.assertIn('social.Find("ButtonTest")', self.hud)
        self.assertIn('social.Find("ButtonFb")', self.hud)
        self.assertIn('extraImage.sprite == originalImage.sprite', self.hud)
        self.assertIn("extraFb.gameObject.SetActive(false);", self.hud)
        self.assertNotIn("originalFb.gameObject.SetActive(false)", self.hud)

    def test_skins_hidden_in_saved_scene_and_at_runtime(self):
        self.assertIn("m_Name: ButtonSkins", self.scene)
        self.assertIn('canvas.Find("PanelHomeBottomLeft/ButtonSkins")', self.hud)
        self.assertIn("skins.gameObject.SetActive(false);", self.hud)
        self.assertIn("m_IsActive: 0", self.component(106, 1))
        self.assertIn("m_IsActive: 0", self.component(10, 1))
        self.assertIn("m_IsActive: 1", self.component(82, 1))

    @classmethod
    def component(cls, component_id, class_id):
        match = re.search(
            rf"(?ms)^--- !u!{class_id} &{component_id}\\n"
            rf"(.*?)(?=^--- !u!|\\Z)", cls.scene
        )
        return match.group(1) if match else ""

    def test_persisted_activity_is_between_shop_and_ship_in_edit_mode(self):
        order = (
            "m_Children:\\n"
            "  - {fileID: 928}\\n"   # Original ButtonShop
            "  - {fileID: 3001}\\n"  # New Activity button
            "  - {fileID: 919}\\n"   # Original ButtonShip
            "  - {fileID: 975}\\n"   # Summon
            "  - {fileID: 1048}\\n"  # Heroes
            "  - {fileID: 990}"       # Bag
        )
        self.assertIn(order, self.component(935, 224))
        self.assertIn("m_Name: HTDC Offline Activity", self.component(3000, 1))
        self.assertIn("m_IsActive: 1", self.component(3000, 1))
        self.assertIn("m_Text: Hoạt động", self.component(3012, 114))
        self.assertIn(
            "m_Sprite: {fileID: 21300000, "
            "guid: 86cbb642c2b57314eabcb83baa79b219, type: 2}",
            self.component(3003, 114),
        )
        self.assertIn("m_TargetGraphic: {fileID: 3003}", self.component(3004, 114))
        self.assertIn("m_Calls: []", self.component(3004, 114))
        # Newly serialized controls have no copied source Shop callbacks.
        self.assertNotIn("ButtonShopClick", self.component(3004, 114))
        self.assertNotIn("mTerm:", self.component(3012, 114))
        self.assertIn("savedActivity = group.Find(ActivityName)", self.hud)
        self.assertIn("savedButton.onClick.AddListener(", self.hud)
        self.assertIn("eventButton.onClick.Invoke()", self.hud)

    def test_serialized_activity_references_are_valid_and_unique(self):
        ids = re.findall(r"(?m)^--- !u!\\d+ &(\\d+)$", self.scene)
        self.assertEqual(len(ids), len(set(ids)))
        self.assertEqual(len(ids), 2108)
        for obj in (3000, 3005, 3009):
            self.assertIn("GameObject:", self.component(obj, 1))
        self.assertIn("m_Father: {fileID: 935}", self.component(3001, 224))
        self.assertIn("m_Father: {fileID: 3001}", self.component(3006, 224))
        self.assertIn("m_Father: {fileID: 3006}", self.component(3010, 224))
        self.assertEqual(self.scene.count("m_Name: HTDC Offline Activity"), 1)

    def test_activity_uses_original_home_event_sprite_and_existing_popup(self):
        self.assertIn("m_Name: ButtonEvent", self.home)
        sprite_guid = "86cbb642c2b57314eabcb83baa79b219"
        self.assertIn("guid: " + sprite_guid, self.home)
        self.assertIn("guid: " + sprite_guid, ICON_ASSET_META.read_text())
        self.assertIn("images/home/homebutton/home_button_events", self.hud)
        self.assertIn("Assets/Resources/images/home/homebutton/home_button_events.asset", self.hud)
        self.assertIn('canvas.Find("PaneHomeCenter/GroupButtons/ButtonEvents")', self.hud)
        self.assertIn("UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>", self.hud)
        self.assertIn("found.Length == 1 ? found[0] : null", self.hud)
        self.assertIn("activityButton.onClick = new Button.ButtonClickedEvent();", self.hud)
        self.assertIn("eventButton.onClick.Invoke()", self.hud)
        self.assertNotIn("RemoveAllListeners()", self.hud)

    def test_scene_start_runs_offline_adapter_and_script_metadata(self):
        home_script = (SRC / "Home2Scene.cs").read_text(encoding="utf-8")
        self.assertIn("OfflineHome2HudCorrection.Apply(this);", home_script)
        self.assertIn("GetComponent<OfflineHeroHomePresenter>()", home_script)
        self.assertIn("class OfflineHome2HudCorrection", self.hud)
        self.assertIn("NEW_PROJECT_DESIGN", self.hud)
        self.assertTrue(HUD_META.exists())
        self.assertRegex(HUD_META.read_text(), r"(?m)^guid: [0-9a-f]{32}$")
        self.assertNotIn("Debug.LogWarning(", self.hud.replace(
            "UnityEngine.Debug.LogWarning(", ""
        ))
        self.assertNotIn("Debug.LogError(", self.hud.replace(
            "UnityEngine.Debug.LogError(", ""
        ))


if __name__ == "__main__":
    unittest.main()
