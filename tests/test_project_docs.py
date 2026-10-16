"""Repository documentation consistency tests.

These checks validate README paths and scope only; they do not test Unity runtime.
"""
import re
import unittest
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
README = ROOT / "README.md"
HANDOFF = ROOT / "docs" / "AI_HANDOFF.md"


class ProjectDocsTests(unittest.TestCase):
    def test_root_contains_unity_project_files(self):
        self.assertTrue((ROOT / "Assets").is_dir())
        self.assertTrue((ROOT / "Packages" / "manifest.json").is_file())
        version = (ROOT / "ProjectSettings" / "ProjectVersion.txt").read_text(
            encoding="utf-8"
        )
        self.assertIn("2022.3.51f1", version)

    def test_readme_describes_current_project_and_limits(self):
        content = README.read_text(encoding="utf-8")
        self.assertIn("# Hải Tặc Đại Chiến (HTDC)", content)
        self.assertIn("Unity 2022.3.51f1", content)
        self.assertIn("chưa phải bản game online hoàn chỉnh", content)
        self.assertIn("chưa được kết nối tới API/database này", content)
        self.assertIn("OfflineHeroCatalog", content)
        self.assertIn("OfflineHeroParty", content)

    def test_readme_does_not_promote_removed_root_viewer_commands(self):
        content = README.read_text(encoding="utf-8")
        self.assertNotIn("CHAY_UI_OFFLINE.bat", content)
        self.assertNotIn("web-ui-viewer/", content)
        self.assertNotIn("unity-ui-viewer/", content)

    def test_relative_markdown_links_exist(self):
        content = README.read_text(encoding="utf-8")
        links = re.findall(r"(?<!!)\[[^\]]+\]\(([^)]+)\)", content)
        missing = []
        for target in links:
            if target.startswith(("http://", "https://", "mailto:", "#")):
                continue
            target = target.split("#", 1)[0].split("?", 1)[0]
            if not target:
                continue
            if not (ROOT / target).exists():
                missing.append(target)
        self.assertEqual([], missing, "README links missing from repo: " + ", ".join(missing))

    def test_readme_documents_setup_and_static_test_scope(self):
        content = README.read_text(encoding="utf-8")
        self.assertIn("git lfs install", content)
        self.assertIn("git lfs pull", content)
        self.assertIn("unittest discover", content)
        self.assertIn("không chứng minh project compile trong Unity", content)

    def test_handoff_records_current_project_context(self):
        content = HANDOFF.read_text(encoding="utf-8")
        self.assertTrue(content.startswith("# AI HANDOFF — Hải Tặc Đại Chiến Unity project"))
        self.assertIn("## 2026-10-16 — Viết lại README", content)
        self.assertIn("bba09406fbe261a62f079f4be283b7bbb21ca94d", content)


if __name__ == "__main__":
    unittest.main()
