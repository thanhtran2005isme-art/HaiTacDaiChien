"""Sanity checks for documentation handoff and PR-only development policy."""
from __future__ import annotations
import importlib.util
from pathlib import Path
import re
import unittest

ROOT = Path(__file__).resolve().parents[1]
SOURCES = [
    "AGENTS.md",
    "docs/README.md",
    "docs/AI_HANDOFF.md",
    "docs/ARCHITECTURE.md",
    "docs/DECISIONS.md",
    "docs/TROUBLESHOOTING.md",
    "docs/WORKFLOW.md",
    "docs/history/2026-10.md",
    ".github/PULL_REQUEST_TEMPLATE.md",
]


class TestProjectDocs(unittest.TestCase):
    def test_entrypoints_exist_and_link_to_real_files(self):
        for source in SOURCES:
            self.assertTrue((ROOT / source).is_file(), source)
        agents = (ROOT / "AGENTS.md").read_text(encoding="utf-8")
        self.assertIn("docs/AI_HANDOFF.md", agents)
        self.assertIn("origin/main", agents)
        index = (ROOT / "docs/README.md").read_text(encoding="utf-8")
        for doc in ["AI_HANDOFF.md", "ARCHITECTURE.md", "DECISIONS.md",
                    "TROUBLESHOOTING.md", "WORKFLOW.md", "history/2026-10.md"]:
            self.assertIn(doc, index)
            self.assertTrue((ROOT / "docs" / doc).is_file())

    def test_handoff_has_required_sections_and_evidence_limits(self):
        content = (ROOT / "docs/AI_HANDOFF.md").read_text(encoding="utf-8")
        for text in ("Cập nhật:", "Nhánh ổn định:", "Đã có",
                     "Vấn đề còn lại", "Ưu tiên công việc kế tiếp",
                     "không chứng minh", "Spine", "2026-10-09"):
            self.assertIn(text, content)
        self.assertLess(len(content.splitlines()), 200,
                        "AI_HANDOFF should remain short and task-focused")

    def test_commit_history_is_traceable(self):
        history = (ROOT / "docs/history/2026-10.md").read_text(encoding="utf-8")
        self.assertIn("<!-- BEGIN AUTO COMMIT LOG -->", history)
        self.assertIn("<!-- END AUTO COMMIT LOG -->", history)
        actual = re.findall(
            r"https://github\.com/thanhtran2005isme-art/HaiTacDaiChien/commit/([0-9a-f]{40})",
            history)
        self.assertGreaterEqual(len(actual), 15)
        self.assertEqual(len(actual), len(set(actual)),
                         "Duplicate SHA in monthly history")

    def test_branch_and_pr_convention(self):
        workflow = (ROOT / "docs/WORKFLOW.md").read_text(encoding="utf-8")
        self.assertIn("git switch -c feat/ten-chuc-nang", workflow)
        self.assertIn("git push -u origin", workflow)
        self.assertIn("Squash and merge", workflow)
        self.assertIn("Require a pull request", workflow)
        self.assertIn("người quản trị bật", workflow)
        template = (ROOT / ".github/PULL_REQUEST_TEMPLATE.md").read_text(
            encoding="utf-8")
        self.assertIn("docs/AI_HANDOFF.md", template)
        self.assertIn("CI", template)
        self.assertIn("chưa test", template.lower())

    def test_no_unreviewed_bot_main_push(self):
        generator = (ROOT / ".github/workflows/unity-offline-viewer.yml"
                     ).read_text(encoding="utf-8")
        self.assertNotIn("git push origin HEAD:main", generator)
        self.assertNotIn("contents: write", generator)
        self.assertIn("contents: read", generator)
        xapk = (ROOT / ".github/workflows/xapk-ui-audit.yml").read_text(
            encoding="utf-8")
        self.assertNotIn("git push origin HEAD:main", xapk)
        self.assertNotIn("contents: write", xapk)
        self.assertIn("contents: read", xapk)
        self.assertIn("pull_request:", xapk)
        self.assertIn("pull_request:", generator)
        self.assertIn("git diff --exit-code -- unity-ui-viewer/", generator)

    def test_history_refresh_preserves_human_summary(self):
        script = ROOT / "tools/refresh_commit_history.py"
        spec = importlib.util.spec_from_file_location("commit_history", script)
        module = importlib.util.module_from_spec(spec)
        assert spec.loader is not None
        spec.loader.exec_module(module)
        before = "# Tóm tắt\nGiữ nguyên ghi chú.\n\n" + module.START + (
            "\nold\n") + module.END + "\nFooter\n"
        after = module.update_existing(before, [
            ("2026-10-09", "f" * 40, "docs: update handoff")
        ])
        self.assertIn("Giữ nguyên ghi chú.", after)
        self.assertIn("Footer", after)
        self.assertIn("/commit/" + "f" * 40, after)
        self.assertNotIn("\nold\n", after)
        self.assertEqual(after.count(module.START), 1)
        with self.assertRaises(ValueError):
            module.update_existing("# missing markers", [])
        self.assertIsNotNone(module.MONTH.fullmatch("2026-10"))
        self.assertIsNone(module.MONTH.fullmatch("../private"))


if __name__ == "__main__":
    unittest.main()
