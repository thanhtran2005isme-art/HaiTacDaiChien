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
        b"blob " + str(len(data)).encode() + b"\\x00" + data
    ).hexdigest()


class Home2HudSourceContract(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.scene = SCENE.read_text(encoding="utf-8")
        cls.hud = HUD.read_text(encoding="utf-8")
        cls.home = ORIGINAL_HOME.read_text(encoding="utf-8")

    def test_original_scene_unchanged(self):
        self.assertEqual(
            git_blob_sha(SCENE),
            "ff2cb00d590a38d4c4b1384f15e5d5af0574d535",
        )
        self.assertEqual(self.scene.count("m_OnClick:"), 47)
        self.assertEqual(self.scene.count("m_Text: Naruto"), 6)

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

    def test_skins_hidden_only_at_runtime(self):
        self.assertIn("m_Name: ButtonSkins", self.scene)
        self.assertIn('canvas.Find("PanelHomeBottomLeft/ButtonSkins")', self.hud)
        self.assertIn("skins.gameObject.SetActive(false);", self.hud)

    def test_activity_is_inserted_between_real_shop_ship_from_source(self):
        original_order = (
            "m_Children:\\n"
            "  - {fileID: 928}\\n"  # ButtonShop
            "  - {fileID: 919}\\n"  # ButtonShip
            "  - {fileID: 975}\\n"  # ButtonSummon
            "  - {fileID: 1048}\\n"  # ButtonHeroes
            "  - {fileID: 990}"      # ButtonBag
        )
        self.assertIn(original_order, self.scene)
        self.assertIn('group.Find("ButtonShop")', self.hud)
        self.assertIn('group.Find("ButtonShip")', self.hud)
        self.assertIn("ship.GetSiblingIndex() != shop.GetSiblingIndex() + 1", self.hud)
        self.assertIn("activity.transform.SetSiblingIndex(shop.GetSiblingIndex() + 1)", self.hud)
        self.assertIn('activityLabel.text = "Hoạt động";', self.hud)
        self.assertIn("Object.Instantiate(shop.gameObject, group, false)", self.hud)

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
