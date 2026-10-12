#!/usr/bin/env python3
"""PanelHeroInfo 79-event offline binding: strict source-preservation regression.

Static contract ONLY; cannot prove Unity Editor, HeroModel or server gameplay.
"""
import hashlib
import re
import unittest
from collections import Counter
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
PREFAB = ROOT / "Assets/Resources/prefabs/heroinfo/PanelHeroInfo.prefab"
SCRIPT = ROOT / "Assets/Scripts/Assembly-CSharp/PanelHeroInfoOfflineBridge.cs"
META = ROOT / "Assets/Scripts/Assembly-CSharp/PanelHeroInfoOfflineBridge.cs.meta"
SOURCE_SHA = "e90f30707e4fccc14a350fd2eec58837cdbf765b"
BRIDGE_ID = "114898765432101234"
ROOT_GO = "1709254077376921"
GUID = "9d5a2c0f1b3e4d6789a0bcde12345678"
TABS = {
    "114182939637751607": "1083739296819378",  # point
    "114769000420084825": "1719342369648492",  # equipment
    "114275608186828602": "1760480127920944",  # awaken
    "114696562466561301": "1433881075115309",  # breakthrough
    "114224515126651668": "1251666133023005",  # enable
    "114774516583502458": "1808008904197051",  # E-skill
    "114977941605037310": "1177900317477814",  # special equipment
}
FUNCTIONAL = {
    "114157225796802554": 1,  # open original BoxShare
    "114849003914875799": 2,  # close BoxShare
    "114417611521646659": 3,  # back closes prefab instance
    "114708998942925673": 4,  # original PanelFilter toggle
    "114959207276301250": 5,  # PointDetail close (NOT the root)
    "114919956442135330": 6,  # ESkillDetail close
    "114194641596278120": 7,  # original BoxSkillDesc close
}
BLOCK = re.compile(r"(?ms)^--- !u!(\d+) &(\d+)\n(.*?)(?=^--- !u!|\Z)")
ROUTE = re.compile(
    r"(?m)^  - button: \{fileID: (\d+)\}\n"
    r"    kind: (\d+)\n"
    r"    target: \{fileID: (\d+)\}\n"
    r"    title: ([^\n]*)\n"
    r"    message: ([^\n]*)$"
)


class PanelHeroInfoOfflineContract(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.text = PREFAB.read_text(encoding="utf-8")
        cls.script = SCRIPT.read_text(encoding="utf-8")
        cls.meta = META.read_text(encoding="utf-8")
        cls.blocks = {(klass, uid): body for klass, uid, body in BLOCK.findall(cls.text)}
        cls.component = cls.blocks[("114", BRIDGE_ID)]
        cls.routes = ROUTE.findall(cls.component)

    def test_original_prefab_is_byte_for_byte_preserved(self):
        self.assertEqual(self.text.count("--- !u!114 &" + BRIDGE_ID + "\n"), 1)
        self.assertEqual(self.text.count("  - component: {fileID: " + BRIDGE_ID + "}\n"), 1)
        marker = "\n--- !u!114 &" + BRIDGE_ID + "\n"
        self.assertEqual(self.text.count(marker), 1)
        before, _ = self.text.split(marker, 1)
        old_source = (before + "\n").replace(
            "  - component: {fileID: " + BRIDGE_ID + "}\n", "", 1
        ).encode("utf-8")
        git_blob = hashlib.sha1(
            b"blob " + str(len(old_source)).encode("ascii") + b"\0" + old_source
        ).hexdigest()
        self.assertEqual(git_blob, SOURCE_SHA)

    def test_exact_original_79_persistent_events_remain(self):
        self.assertEqual(self.text.count("m_OnClick:"), 79)
        self.assertEqual(self.text.count("m_TargetAssemblyTypeName:"), 79)
        original = {}
        for (kind, ident), body in self.blocks.items():
            if kind != "114" or "m_OnClick:" not in body:
                continue
            callback_part = body.split("m_OnClick:", 1)[1]
            methods = re.findall(r"m_MethodName: ([^\r\n]+)", callback_part)
            targets = re.findall(r"- m_Target: \{fileID: (\d+)\}", callback_part)
            self.assertEqual(len(methods), 1, ident)
            self.assertEqual(len(targets), 1, ident)
            original[ident] = (targets[0], methods[0])
        self.assertEqual(len(original), 79)
        self.assertEqual(len(self.routes), 79)
        routed = [button_id for button_id, *_ in self.routes]
        self.assertEqual(len(set(routed)), 79)
        self.assertEqual(set(routed), set(original))

    def test_only_source_existing_tabs_and_explicit_responses(self):
        actual = {button: (int(kind), target) for button, kind, target, _, _ in self.routes}
        for button, target in TABS.items():
            self.assertEqual(actual[button], (8, target))
            self.assertIn(("1", target), self.blocks, target)
        for button, kind in FUNCTIONAL.items():
            self.assertEqual(actual[button], (kind, "0"))
        self.assertEqual(Counter(int(kind) for _, kind, _, _, _ in self.routes),
                         {0: 65, 1: 1, 2: 1, 3: 1, 4: 1,
                          5: 1, 6: 1, 7: 1, 8: 7})
        self.assertIn("m_GoPanelEquipment: {fileID: 1719342369648492}", self.text)
        self.assertIn("m_GoBoxShare: {fileID: 1808113985169905}", self.text)
        for target in (
            "1808113985169905", "1750672133102756",
            "1235180746187507", "1100532463973045",
            "1415925016355036",
        ):
            self.assertIn(("1", target), self.blocks, target)

    def test_explicit_blockers_never_claim_gameplay_or_purchase(self):
        for button, kind, target, title, message in self.routes:
            if int(kind) in (0, 8):
                self.assertTrue(title.strip(), button)
                self.assertGreater(len(message.strip()), 35, button)
                self.assertTrue(
                    any(key in message for key in ("Thiếu", "Chưa", "thiếu", "chưa")),
                    "Missing honest blocker: " + button,
                )
            else:
                self.assertEqual((title, message, target), ("", "", "0"))
        self.assertIn('Resources.Load<GameObject>(resourcePath)', self.script)
        self.assertIn('const string resourcePath = "prefabs/common/PopupMessage"', self.script)
        self.assertIn("SetVisible(shareBox, true)", self.script)
        self.assertIn("SetVisible(shareBox, false)", self.script)
        self.assertIn("ShowOriginalTab(response.target)", self.script)
        self.assertIn("registered.Add(response.button)", self.script)
        self.assertNotIn("RemoveAllListeners", self.script)
        self.assertNotIn("SceneManager.LoadScene", self.script)
        self.assertNotIn("HeroModel(", self.script)

    def test_root_bridge_and_original_resources_exist(self):
        root = self.blocks[("1", ROOT_GO)]
        self.assertIn("m_Name: PanelHeroInfo", root)
        self.assertEqual(root.count("- component: {fileID: " + BRIDGE_ID + "}"), 1)
        self.assertIn("m_GameObject: {fileID: " + ROOT_GO + "}", self.component)
        self.assertIn("guid: " + GUID, self.component)
        self.assertIn("guid: " + GUID, self.meta)
        self.assertIn("shareBox: {fileID: 1808113985169905}", self.component)
        self.assertIn("filterPanel: {fileID: 1750672133102756}", self.component)
        self.assertIn("pointDetail: {fileID: 1235180746187507}", self.component)
        self.assertIn("skillDetail: {fileID: 1100532463973045}", self.component)
        self.assertIn("skillDescription: {fileID: 1415925016355036}", self.component)
        self.assertEqual(
            re.findall(r"(?m)^  - \{fileID: (\d+)\}$", self.component),
            list(TABS.values()),
        )
        msg = ROOT / "Assets/Resources/prefabs/common/PopupMessage.prefab"
        self.assertTrue(msg.is_file())
        content = msg.read_text(encoding="utf-8")
        self.assertIn("m_Name: TextTitle", content)
        self.assertIn("m_Name: TextMessage", content)
        self.assertIn("m_Name: ButtonAction1", content)

    def test_p0_and_celestial_scene_regression_counts(self):
        home = (ROOT / "Assets/Scenes/HomeScene.unity").read_text(encoding="utf-8")
        celestial = (ROOT / "Assets/Scenes/CelestialScene.unity").read_text(encoding="utf-8")
        self.assertEqual(home.count("m_OnClick:"), 55)
        self.assertEqual(celestial.count("m_OnClick:"), 47)
        self.assertIn("--- !u!114 &1106", celestial)


if __name__ == "__main__":
    unittest.main()
