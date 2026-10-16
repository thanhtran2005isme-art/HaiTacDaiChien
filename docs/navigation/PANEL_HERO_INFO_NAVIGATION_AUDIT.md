# P1 — PanelHeroInfo.prefab: 79 source callbacks, offline interaction contract

**Branch:** `feat/htdc-localization-hold`  
**Unity:** `2022.3.51f1` (AssetRipper export)  
**Source:** `Assets/Resources/prefabs/heroinfo/PanelHeroInfo.prefab`  
**Scope:** `NEW_PROJECT_DESIGN` — reversible offline UI, **not** verified XAPK runtime gameplay.

## Serialized source proof

- Prefab contains **954 GameObjects, 3,997 serialized objects**, and **79 distinct Button components with 79 one-to-one persistent `m_OnClick` calls**.
- Original Git blob SHA: `e90f30707e4fccc14a350fd2eec58837cdbf765b`.
- Appended only one new `PanelHeroInfoOfflineBridge` MonoBehaviour (fileID `114898765432101234`, GUID `9d5a2c0f1b3e4d6789a0bcde12345678`) to existing root GameObject `1709254077376921` (`PanelHeroInfo`). All 79 serialized original persistent callback targets/methods, 954 original GameObjects, panel layout, Sprite references, Spine and active defaults are left untouched.
- `tests/test_panel_hero_info_navigation.py` removes **only** the new component and root reference in-memory and recomputes Git blob SHA to assert the original prefab bytes are exact.
- A static source `m_OnClick` is not proof that a button is interactive in a running Scene. Some original buttons and parent panels are inactive.

## Offline response matrix: 79/79

| Behavior | Count | Actual offline effect |
|---|---:|---|
| Original share `BoxShare` open / background close | 2 | Toggle existing `BoxShare` GameObject |
| Root `ButtonBack` | 1 | Hide the loaded PanelHeroInfo instance, no destruction or Scene navigation |
| `ButtonFilter` | 1 | Toggle existing `PanelFilter` (list content itself requires data) |
| Original `PanelPointDetail`, `PanelESkillDetail`, `BoxSkillDesc` close | 3 | Close only their existing detail GameObject |
| Hero tabs: Point, Equipment, Awaken, Breakthrough, Enable, ESkill, SpecialEquip | 7 | Show exactly one of seven **original** tab-panel GameObjects, plus explicit notice that `HeroModel` data is absent |
| Remaining data-dependent actions | 65 | Open existing `Resources/prefabs/common/PopupMessage.prefab` with context-specific unavailable-state message |
| **Total** | **79** | **79 distinct extra offline handlers** |

The **65 unavailable actions** cover hero lock/skin, share to guild/world, 6 equipment slots and 6 items, equip/unequip, level/tier upgrades, hero materials, awakening, breakthrough, regression, skills, special equipment activation/upgrade/unlock, hero filtering by faction and hero next/previous. No fake `HeroModel`, `EquipmentModel`, inventory, gold, gems, skill stats, transaction or game-server reply.

The `PanelHeroInfo.cs` exported IL2CPP method bodies such as `LoadHeroModel`, `ShowUp`, `ButtonShareToGuildClick`, `OnTabActive` and `OnButtonBackClick` are still empty/no-op. Supplemental callbacks **do not replace or remove** these original handlers.

The bridge locates the nearest parent `Canvas`, instantiates the original `PopupMessage`, sets original `TextTitle` and `TextMessage`, attaches Close actions to original `ButtonAction1` / `ButtonAction2`, and uses an invisible click-blocking layer. No new visible UI skin, custom Scene, network or independent navigation subsystem is introduced. If there is no parent Canvas or the prefab is missing, it logs an explicit blocker.

## Known gaps / safety

1. **Model-dependent content is absent:** Tabs can open visually but are not populated with real hero stats/equipment/skill details. The UI is not certified as original-runtime-complete.
2. **No trading or purchase:** No HeroModel mutations, server calls, equipment switching, currencies, unlocking or battle logic were added.
3. **Prefab lifecycle:** `ButtonBack` deactivates the instance instead of invoking unavailable owner-specific close logic. Parent Scene may require explicit reactivation; validate this in the real owner Scene.
4. **Panel inactive controls:** The seven tab page panels, hero list and detail sections have serialized initial active states; do not force-enable them merely to mark all 79 clicks PASS.
5. **Unity acceptance not verified:** C# compile, popup layering/Canvas, Escape behavior, click conflicts with the original callbacks, tab panel visual output and prefab reopening require test in Unity Editor on the exact commit.
6. **P0 / Celestial untouched:** HomeScene (55), Home2Scene (47) and CelestialScene (47) source assets were not modified by this task.

## Static and Unity Play Mode verification

From a clean Windows workspace:

```powershell
git switch feat/htdc-localization-hold
git pull --ff-only origin feat/htdc-localization-hold
git lfs pull
py -3 -m unittest discover -s tests -p test_panel_hero_info_navigation.py -v
py -3 -m unittest discover -s tests -p test_home_scene_navigation_pilot.py -v
py -3 -m unittest discover -s tests -p test_home2_scene_navigation.py -v
py -3 -m unittest discover -s tests -p test_celestial_scene_navigation.py -v
```

Open repository root in **Unity 2022.3.51f1**, clear Console and test the prefab **inside its actual owner Scene** (e.g., `HeroListNewScene` if it loads the prefab with the correct Canvas), not merely in a prefab preview without a Canvas:

1. Open share `BoxShare`, click background to close, reopen. Check source positions and no duplicate overlay.
2. Open/close `PanelFilter` and all seven tabs; verify original GameObjects show/hide exactly once, notice opens/closes, and no misleading made-up values.
3. Click visible model-dependent controls; each must show an appropriate `PopupMessage`; close via original button or Escape, reopen without stacking.
4. Use `ButtonClose` for point detail and `ButtonBack` for root, then reopen the original prefab through its parent Scene. Test inactive buttons **only** if real source-backed state activates them.
5. Confirm P0 HomeScene/Home2Scene and P1 CelestialScene smoke regression, no compile errors / `Missing Script` / Unity exception.
6. Record actual Unity Play Mode outcome separately. Static tests/CI do **not** prove full 79 playable actions.

**State after implementation:** 79/79 static route coverage; gameplay and Unity Play Mode still **UNVERIFIED**. Keep changes only on existing feature branch; never merge automatically.
