#!/usr/bin/env python3
"""Read-only Unity YAML navigation audit for an AssetRipper-exported project.

Requires Python 3.9+, stdlib only. Does not edit Unity assets or Git branches.
Run: py -3 tools/audit_unity_navigation.py --root . --output ..\HTDC_navigation_report
"""
from __future__ import annotations
import argparse
import csv
import re
from collections import Counter, defaultdict
from pathlib import Path

OBJECT_HEAD = re.compile(r"(?m)^--- !u!(\d+) &(-?\d+)(?: stripped)?\s*$")
SCRIPT_GUID = re.compile(r"(?m)^\s*m_Script: \{fileID: -?\d+, guid: ([a-fA-F0-9]{32}), type: \d+\}")
GAME_OBJECT = re.compile(r"(?m)^\s*m_GameObject: \{fileID: (-?\d+)\}")
FATHER = re.compile(r"(?m)^\s*m_Father: \{fileID: (-?\d+)\}")
NAME = re.compile(r"(?m)^\s*m_Name: (.*)$")
ACTIVE = re.compile(r"(?m)^\s*m_IsActive: (\d+)\s*$")
BUILDING = re.compile(r"(?m)^\s*m_BuildingType: (\d+)\s*$")
GUID = re.compile(r"(?m)^guid:\s*([a-fA-F0-9]{32})")
PERSISTENT = re.compile(r"(?m)^\s*- m_Target:\s*\{fileID:\s*(-?\d+)(?:,\s*guid:\s*([a-fA-F0-9]{32}))?[^}]*\}")

BUILDING_DESTINATIONS = {
    2: 'PopupShop / Market UI (candidate)',
    3: 'ForgeScene.unity (candidate)',
    4: 'TarvenScene.unity (candidate)',
    5: 'SummonOrbScene.unity / SummonScrollScene.unity (candidates)',
    6: 'Explore UI (not established)',
    7: 'FusingHeroScene.unity (candidate)',
    8: 'AltarScene.unity (candidate)',
    9: 'TowerNewScene.unity (candidate)',
    10: 'LuckySpinScene.unity / SuperLuckySpinScene.unity (candidates)',
    12: 'BraveTrialScene.unity / BraveTrialScene2.unity (candidates)',
    13: 'DungeonScene.unity (candidate)',
    14: 'ArenaScene.unity (candidate)',
    15: 'SealedLand.unity (candidate)',
}
METHOD_DESTINATIONS = {
    'ButtonHeroesClick': 'HeroListScene.unity / HeroListNewScene.unity (candidates)',
    'ButtonBagClick': 'ItemListScene.unity (candidate)',
    'ButtonMonsterClick': 'MonsterScene.unity (candidate)',
    'ButtonGuildClick': 'GuildScene.unity (candidate)',
    'ButtonCampaignClick': 'CampaignMapScene.unity / CampaignScene.unity (candidates)',
    'ButtonQuestClick': 'PopupDailyQuest or quest panel (candidate)',
    'ButtonEventRaidClick': 'PopupEventRaid or EventScene (candidates)',
    'ButtonEventClick': 'EventScene.unity (candidate)',
    'ButtonIAPClick': 'PopupShop or store panel (candidate)',
}
COLUMNS = [
    'source_asset','button_path','button_object_id','button_component_id',
    'button_active_local','button_active_hierarchy','button_interactable',
    'persistent_call_number','target_component_id','target_object_path',
    'target_script','target_script_guid','target_method','call_state',
    'target_assembly_type','script_method_status','destination_candidate',
    'target_scene_or_prefab_exists','audit_status','notes'
]


def prop(text: str, key: str) -> str:
    m = re.search(r'(?m)^\s*' + re.escape(key) + r':[ \t]*([^\r\n]*)', text)
    return m.group(1).strip() if m else ''


def source_objects(content: str):
    hits = list(OBJECT_HEAD.finditer(content))
    result = {}
    for i, h in enumerate(hits):
        chunk = content[h.end():hits[i + 1].start() if i + 1 < len(hits) else len(content)]
        result[h.group(2)] = {
            'fid': h.group(2), 'type': int(h.group(1)), 'text': chunk,
            'go': GAME_OBJECT.search(chunk).group(1) if GAME_OBJECT.search(chunk) else '',
            'father': FATHER.search(chunk).group(1) if FATHER.search(chunk) else '',
            'name': NAME.search(chunk).group(1).strip() if NAME.search(chunk) else '',
            'active': ACTIVE.search(chunk).group(1) if ACTIVE.search(chunk) else '',
            'script_guid': SCRIPT_GUID.search(chunk).group(1).lower() if SCRIPT_GUID.search(chunk) else '',
            'building': BUILDING.search(chunk).group(1) if BUILDING.search(chunk) else '',
        }
    return result


def click_section(chunk: str):
    lines = chunk.splitlines()
    for i, line in enumerate(lines):
        if not re.match(r'^\s*m_OnClick:\s*$', line):
            continue
        level = len(line) - len(line.lstrip())
        end = i + 1
        while end < len(lines):
            txt = lines[end]
            if txt.strip() and len(txt) - len(txt.lstrip()) <= level:
                break
            end += 1
        yield '\n'.join(lines[i:end])


def parse_calls(section: str):
    if re.search(r'(?m)^\s*m_Calls:\s*\[\]\s*$', section):
        yield {'target': '', 'method': '', 'assembly': '', 'state': '', 'empty': True}
        return
    matches = list(PERSISTENT.finditer(section))
    if not matches:
        yield {'target': '', 'method': '', 'assembly': '', 'state': '', 'empty': True}
    for i, m in enumerate(matches):
        data = section[m.end():matches[i + 1].start() if i + 1 < len(matches) else len(section)]
        yield {
            'target': m.group(1), 'external_guid': m.group(2) or '',
            'method': prop(data, 'm_MethodName'),
            'assembly': prop(data, 'm_TargetAssemblyTypeName'),
            'state': prop(data, 'm_CallState'), 'empty': False,
        }


def method_is_empty(src: str, method: str) -> str:
    if not src or not method:
        return 'UNKNOWN_NO_SCRIPT_SOURCE'
    pat = re.compile(r'(?m)^\s*(?:(?:public|private|protected|internal|static|virtual|override|new|sealed|async|extern)\s+)+' +
                     r'[\w<>,\[\]?\s]+\b' + re.escape(method) + r'\s*\([^;{}]*\)\s*\{')
    found = list(pat.finditer(src))
    if not found:
        return 'METHOD_NOT_FOUND_IN_SOURCE'
    if all(re.match(r'\s*}', src[x.end():]) for x in found):
        return 'EMPTY_BODY'
    return 'NONEMPTY_OR_UNRESOLVED_BODY'


def index_scripts(root: Path):
    names = {}
    for meta in (root / 'Assets').rglob('*.cs.meta'):
        try:
            m = GUID.search(meta.read_text(encoding='utf-8-sig', errors='replace'))
        except OSError:
            continue
        if m:
            script_path = Path(str(meta)[:-5])
            names[m.group(1).lower()] = (script_path, script_path.stem)
    return names


def candidate(obj, call, method):
    if method == 'OnClick' and obj and obj['building']:
        return BUILDING_DESTINATIONS.get(int(obj['building']), 'Unknown building destination')
    return METHOD_DESTINATIONS.get(method, 'NOT_IDENTIFIED_FROM_SOURCE')


def audit_file(root: Path, file: Path, index: dict):
    try:
        content = file.read_text(encoding='utf-8-sig', errors='replace')
    except OSError:
        return [], 'unreadable'
    if content.startswith('version https://git-lfs.github.com/spec/v1'):
        return [], 'LFS_POINTER_ONLY'
    if not content.startswith('%YAML') and '--- !u!' not in content:
        return [], 'not_Unity_YAML'
    objs = source_objects(content)
    transforms = {o['go']: o for o in objs.values() if o['go'] and o['type'] in (4, 224)}

    def route(go):
        if not go or go not in objs:
            return f'UNRESOLVED_GO({go})'
        names, seen, active = [], set(), True
        for _ in range(64):
            if not go or go in seen or go not in objs:
                break
            seen.add(go)
            g = objs[go]
            names.append(g['name'] or go)
            if g['active'] == '0':
                active = False
            t = transforms.get(go)
            if not t or not t['father'] or t['father'] == '0':
                break
            go = objs.get(t['father'], {}).get('go')
        return '/'.join(reversed(names)), active

    scene_names = set(x.name for x in (root / 'Assets/Scenes').glob('*.unity'))
    rows = []
    src_cache = {}
    for o in objs.values():
        for section in click_section(o['text']):
            for num, call in enumerate(parse_calls(section), 1):
                target = objs.get(call.get('target'))
                script_guid = target['script_guid'] if target else ''
                script_file, script_name = index.get(script_guid, (None, 'UNRESOLVED_SCRIPT_GUID'))
                if script_file and script_file not in src_cache:
                    try:
                        src_cache[script_file] = script_file.read_text(encoding='utf-8-sig', errors='replace')
                    except OSError:
                        src_cache[script_file] = ''
                status = method_is_empty(src_cache.get(script_file, ''), call['method'])
                path, active = route(o['go']) if o['go'] in objs else (f'UNRESOLVED_GO({o["go"]})', False)
                dest = candidate(target, call, call['method'])
                present = []
                for scene in re.findall(r'([A-Za-z0-9_]+\.unity)', dest):
                    present.append(f'{scene}:{"YES" if scene in scene_names else "NO"}')
                findings = []
                if call['empty']:
                    findings.append('NO_PERSISTENT_CALL')
                if not target and not call['empty']:
                    findings.append('TARGET_COMPONENT_UNRESOLVED')
                if not script_guid and target:
                    findings.append('NO_TARGET_MONOBEHAVIOUR_SCRIPT')
                if script_guid and not script_file:
                    findings.append('TARGET_SCRIPT_GUID_UNRESOLVED')
                if status == 'EMPTY_BODY':
                    findings.append('HANDLER_EMPTY')
                elif status == 'METHOD_NOT_FOUND_IN_SOURCE':
                    findings.append('METHOD_NOT_FOUND')
                if not call['assembly'] and not call['empty']:
                    findings.append('TARGET_ASSEMBLY_TYPE_EMPTY_CHECK_UNITY')
                if not active:
                    findings.append('INACTIVE_IN_HIERARCHY')
                if call['state'] not in ('2', '1') and not call['empty']:
                    findings.append('CALL_STATE_NOT_RUNTIME_OR_EDITOR')
                rows.append({
                    'source_asset': file.relative_to(root).as_posix(),
                    'button_path': path, 'button_object_id': o['go'],
                    'button_component_id': o['fid'],
                    'button_active_local': objs.get(o['go'], {}).get('active', ''),
                    'button_active_hierarchy': 'YES' if active else 'NO',
                    'button_interactable': prop(o['text'], 'm_Interactable'),
                    'persistent_call_number': num,
                    'target_component_id': call.get('target', ''),
                    'target_object_path': route(target['go'])[0] if target and target['go'] in objs else '',
                    'target_script': script_name, 'target_script_guid': script_guid,
                    'target_method': call['method'], 'call_state': call['state'],
                    'target_assembly_type': call['assembly'], 'script_method_status': status,
                    'destination_candidate': dest,
                    'target_scene_or_prefab_exists': '; '.join(present),
                    'audit_status': ' | '.join(findings) if findings else 'NEEDS_RUNTIME_TEST',
                    'notes': 'Destination candidate is NOT proven by empty IL2CPP stub'
                })
    return rows, ''


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--root', type=Path, default=Path('.'))
    parser.add_argument('--output', type=Path, default=Path('navigation-audit'))
    parser.add_argument('--home-only', action='store_true', help='Scan HomeScene.unity only')
    parser.add_argument('--include-assets', action='store_true', help='Also scan Unity YAML .asset (may take longer)')
    a = parser.parse_args()
    root = a.root.resolve()
    out = a.output.resolve()
    if not (root / 'Assets').is_dir():
        parser.error('--root must contain an Assets directory')
    if out == root or out == (root / 'Assets'):
        parser.error('Choose a separate report folder; never overwrite Unity source')
    out.mkdir(parents=True, exist_ok=True)
    files = ([root / 'Assets/Scenes/HomeScene.unity'] if a.home_only else
             sorted(list((root / 'Assets').rglob('*.unity')) + list((root / 'Assets').rglob('*.prefab')) +
                    (list((root / 'Assets').rglob('*.asset')) if a.include_assets else [])))
    scripts = index_scripts(root)
    rows = []
    skipped = Counter()
    for f in files:
        part, reason = audit_file(root, f, scripts)
        rows.extend(part)
        if reason:
            skipped[reason] += 1
    with (out / 'navigation_callbacks.csv').open('w', encoding='utf-8-sig', newline='') as dst:
        w = csv.DictWriter(dst, fieldnames=COLUMNS)
        w.writeheader()
        w.writerows(rows)
    byfile = Counter(x['source_asset'] for x in rows)
    flags = Counter(flag for r in rows for flag in r['audit_status'].split(' | '))
    summary = [
        '# Unity navigation audit — read-only', '',
        f'- Files examined: {len(files)}', f'- OnClick persistent calls/items recorded: {len(rows)}',
        f'- Referenced C# GUIDs indexed: {len(scripts)}',
        '- Scope: serialized Unity YAML + exported C# stubs only; no Unity Play Mode execution.',
        '- `destination_candidate` is inferred ONLY from scene names and remains unverified.',
        '- Empty `m_TargetAssemblyTypeName` is flagged for Unity Inspector review, not assumed fatal.',
        '- Unity LFS pointers cannot be analyzed until `git lfs pull`.', '',
        '## Issues by category', ''
    ]
    summary.extend(f'- {name}: {count}' for name, count in flags.most_common())
    summary += ['', '## OnClick counts by scene/prefab', '']
    summary.extend(f'- `{name}`: {count}' for name, count in byfile.most_common())
    if skipped:
        summary += ['', '## Skipped', '']
        summary.extend(f'- {name}: {count}' for name, count in skipped.items())
    (out / 'summary.md').write_text('\n'.join(summary) + '\n', encoding='utf-8')
    print(f'READ ONLY: {len(files)} files scanned, {len(rows)} OnClick rows, {len(scripts)} C# GUIDs')
    print(f'CSV: {out / "navigation_callbacks.csv"}')
    print(f'MD:  {out / "summary.md"}')
    if skipped:
        print(f'Skipped: {dict(skipped)}')


if __name__ == '__main__':
    main()