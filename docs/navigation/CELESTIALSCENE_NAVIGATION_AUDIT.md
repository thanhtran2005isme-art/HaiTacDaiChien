# CelestialScene — 47 original OnClick and safe offline response coverage

**Priority:** P1 | **Branch:** `feat/htdc-localization-hold` | **Unity:** 2022.3.51f1
**Scene:** `Assets/Scenes/CelestialScene.unity`
**Classification:** `NEW_PROJECT_DESIGN`, *not* recovery of original XAPK/IL2CPP gameplay.

## Proven source scope

- **47** distinct serialized `Button.m_OnClick` components, all with an original `m_Target` and `m_MethodName`. All original persistent calls are **preserved without edits**.
- The exported C# handlers in `CelestialScene`, `MineController`, `TechBuildingController`, `DaffodilBuildingController`, `IslandController` and `AutoUpdateResource` are empty stubs. A valid serialized callback does not imply gameplay.
- Reuse the existing `HomeSceneNavigationBridge` component, GUID `6486579f283b4602b1e711dc7ec913ab`, attached at **fileID 1106** to original `Scripts` GameObject **62** (original CelestialScene component 780 retained).
- **47/47** original Button fileIDs have exactly one additional offline handler: **1 Scene**, **2 Popup**, **44 explicit unavailable-state notices**. No original handlers, layout, Canvas, Sprite, Spine, GUI active defaults or I2 localization references are changed.

## Available offline routes

| Callback Button fileID | Original action | Offline response | Scope |
|---:|---|---|---|
| 723 | `CelestialScene.BackClick` | `Assets/Scenes/HomeScene.unity` | Editor Scene return / candidate fallback, not verified original routing |
| 727 | Gem `AutoUpdateResource.AddClick` | `Resources/prefabs/common/PopupNap` | Existing recharge **UI preview only** |
| 829 | Gold `AutoUpdateResource.AddClick` | `Resources/prefabs/common/PopupNap` | Existing recharge **UI preview only** |

`PopupNap` contains an existing `ButtonClose`; the shared bridge adds the close response and prevents opening duplicate modal layers. All purchase/SDK/billing functions remain unimplemented. Editor fallback may open a Scene by asset path; standalone player builds require a valid Scene entry in Build Settings. The existing `EditorBuildSettings.asset` is a Git LFS object and is not rewritten.

## 44 callbacks with honest unavailable-state notices

Each opens **the original** `Resources/prefabs/common/PopupMessage.prefab` with a context-specific reason. This is a UI response, not an executed gameplay action.

| Original interaction group | Button fileIDs | Count | Missing verified data |
|---|---|---:|---|
| Island camps (`IslandController.IslandClick`) | 675, 952, 955, 978, 982, 994, 1001, 1015, 1022, 1027, 1043, 1052, 1067, 1078 | 14 | `IslandModel`, unlock/battle state, heroes |
| Boss camp | 801 | 1 | Boss `IslandModel`, HP and combat logic |
| Mine collection progress | 694, 860, 862, 868, 871, 873 | 6 | `MineModel`, cooldown and safe server reward grant |
| Mine slots | 769, 886, 900, 904, 908, 915 | 6 | `MineModel`, built/unbuilt state |
| Built mine buttons | 774, 888, 897, 901, 912, 918 | 6 | Mine level, upgrade costs and state |
| AGI/MILI research/build slots | 706, 719, 744, 825 | 4 | `BuildingModel`, upgrade material and eligibility |
| Daffodil construction, collection, building | 784, 787, 796 | 3 | `BuildingModel`, progress and item rewards |
| Tower, Daffodil purchase, help, smash | 732, 748, 764, 831 | 4 | `CelestialModel`, pricing, help config, raid conditions |
| **Total** | | **44** | |

No fake island fight, resource reward, building upgrade or purchase is performed. The original source popups `prefabs/celestialisland/PopupMakeBuilding`, `PopupUpgradeMine`, `PopupUpgradeBuilding`, `PopupBuyDaffodil` and `PopupBossIsland` are present, but opening them without the corresponding models would display unverified costs/state; they are deliberately **not** used as substitutes for real gameplay.

Some original progress/building GameObjects are **inactive** in serialized source; their Button response is registered but cannot be clicked until real game state activates those objects. Therefore `47/47` denotes static route coverage, not 47 simultaneously visible/clickable controls.

## Tests / Unity Play Mode acceptance

Run locally after checking that the working tree has no uncommitted changes:

```powershell
git switch feat/htdc-localization-hold
git pull --ff-only origin feat/htdc-localization-hold
git lfs pull
py -3 -m unittest discover -s tests -p test_celestial_scene_navigation.py -v
py -3 -m unittest discover -s tests -p test_home2_scene_navigation.py -v
py -3 -m unittest discover -s tests -p test_home_scene_navigation_pilot.py -v
```

Open repository root in **Unity 2022.3.51f1**, then open `Assets/Scenes/CelestialScene.unity`, clear Console and press Play.

1. Test Back (fileID 723) -> HomeScene and re-enter CelestialScene; verify no dead scene/back-key loop.
2. Test both Add Gem/Gold controls -> `PopupNap`; close with `ButtonClose` and `Escape`, reopen, and ensure no duplicated overlay or financial mutation.
3. Test all currently active island/camp/build buttons: each must show the appropriate `PopupMessage` description and close/reopen cleanly. Do not force inactive source objects active merely to claim runtime parity.
4. Where original progression can legitimately activate an inactive mine/building object, verify the corresponding notice too; otherwise mark **NOT REACHABLE WITHOUT GAME STATE**, not PASS.
5. Validate unchanged Canvas/Sprite/Spine positions, no `Missing Script` or C# compile errors, no `Missing Resources prefab` errors, no unexpected Unity exceptions.
6. Reopen HomeScene and Home2Scene and check their 55/47 original callbacks remain intact.

Static `tests/test_celestial_scene_navigation.py` validates exact original target/method pairs, source scene/component GUID, one-to-one route coverage (1+2+44), real prefab/scene presence, honest notices and P0 callback counts. It does **not** prove Unity C# compilation, Play Mode UI render, backend connectivity, successful payments, resource changes or original game logic.

**Status:** source changes + test committed to feature branch; Unity Play Mode on this exact revision remains **UNTESTED** until Windows Editor evidence is supplied. Do not merge or mark gameplay P1 complete on structural tests alone.
