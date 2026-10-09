#!/usr/bin/env python3
"""Refresh the bounded Git commit listing inside docs/history/YYYY-MM.md.

Edits only the marked AUTO COMMIT LOG block; human context is preserved.
Requires a local Git repository. Does not commit, push or contact networks.
"""
from __future__ import annotations

import argparse
from datetime import datetime, timezone
from pathlib import Path
import re
import subprocess

ROOT = Path(__file__).resolve().parents[1]
START = "<!-- BEGIN AUTO COMMIT LOG -->"
END = "<!-- END AUTO COMMIT LOG -->"
MONTH = re.compile(r"^\d{4}-(0[1-9]|1[0-2])$")
SHA = re.compile(r"^[a-f0-9]{40}$")
REPO_LINK = "https://github.com/thanhtran2005isme-art/HaiTacDaiChien"


def git_commits(repo: Path, branch: str, month: str, max_commits: int):
    if max_commits < 1 or max_commits > 300:
        raise ValueError("max_commits must be 1..300")
    if not re.fullmatch(r"[A-Za-z0-9_./-]+", branch) or branch.startswith("-"):
        raise ValueError("Unsafe git ref")
    try:
        output = subprocess.check_output(
            ["git", "-C", str(repo), "log", branch,
             "--format=%H%x1f%cI%x1f%s", "--max-count=" + str(max_commits)],
            text=True, encoding="utf-8", stderr=subprocess.PIPE
        )
    except (OSError, subprocess.CalledProcessError) as exc:
        raise ValueError("Git branch is unavailable. Run git fetch origin first.") from exc
    entries = []
    for line in output.splitlines():
        parts = line.split("\x1f", 2)
        if len(parts) != 3 or not SHA.fullmatch(parts[0]):
            continue
        timestamp = datetime.fromisoformat(parts[1]).astimezone(timezone.utc)
        if timestamp.strftime("%Y-%m") != month:
            continue
        subject = parts[2].replace("\\", "\\\\").replace("|", "\\|").strip()
        entries.append((timestamp.strftime("%Y-%m-%d"), parts[0], subject))
    return entries


def render(entries):
    lines = [START]
    last = ""
    for date, sha, subject in entries:
        if date != last:
            lines += ["", "### " + date, ""]
            last = date
        lines.append("- [`" + sha[:9] + "`](" + REPO_LINK +
                     "/commit/" + sha + ") — " + subject)
    lines.extend(["", END])
    return "\n".join(lines)


def update_existing(original: str, entries):
    if original.count(START) != 1 or original.count(END) != 1:
        raise ValueError("Missing or duplicated history markers")
    first = original.index(START)
    last = original.index(END) + len(END)
    if first >= last:
        raise ValueError("Invalid history marker order")
    return original[:first] + render(entries) + original[last:]


def main():
    p = argparse.ArgumentParser(description=__doc__)
    p.add_argument("--month", required=True, help="YYYY-MM, UTC commit date")
    p.add_argument("--branch", default="origin/main")
    p.add_argument("--max-commits", type=int, default=100)
    p.add_argument("--check", action="store_true",
                   help="exit with 1 if the recorded entries are outdated")
    args = p.parse_args()
    if not MONTH.fullmatch(args.month):
        p.error("--month must be YYYY-MM")
    path = ROOT / "docs" / "history" / (args.month + ".md")
    entries = git_commits(ROOT, args.branch, args.month, args.max_commits)
    if path.exists():
        before = path.read_text(encoding="utf-8")
    else:
        before = ("# Lịch sử thay đổi — " + args.month +
                  "\n\nSnapshot commit `main`. Git log là nguồn sự thật.\n\n" +
                  START + "\n" + END + "\n")
    after = update_existing(before, entries)
    if args.check:
        if before != after:
            raise SystemExit("STALE: " + str(path) +
                             ". Refresh in your feature branch and commit.")
        print("PASS: history up to date:", args.month, len(entries))
        return
    path.parent.mkdir(parents=True, exist_ok=True)
    if before != after:
        path.write_text(after, encoding="utf-8")
    print("History:", path.relative_to(ROOT), "commits:", len(entries),
          "changed:", before != after)


if __name__ == "__main__":
    main()
