# Home2Scene — Navigation audit and offline restore

**Branch:** `feat/htdc-localization-hold`  
**Scene:** `Assets/Scenes/Home2Scene.unity`  
**Unity:** 2022.3.51f1 (AssetRipper export)  
**Type:** `NEW_PROJECT_DESIGN`, *not* verified original XAPK/IL2CPP runtime logic.

## Audit counts

- **47** serialized `m_OnClick` callbacks found, on **47** distinct Button components.
- **47/47** callbacks have a distinct offline response registered by `HomeSceneNavigationBridge` (fileID **2120**) on existing `Scripts` GO **12**. This is UI event coverage, **not** restored game logic.
- Routes consist of **11 Scene navigation**, **14 Popup previews**, **3 toggles** and **19 context-specific informative notices** using the original `PopupMessage` prefab.
- **19** callbacks are bound to an explanatory offline limitation notice because their original C# handlers need missing `HeroModel`, quest, item, account, SDK or backend state; they cannot honestly be called gameplay-ready. Three of the 22 formerly unhandled buttons now open genuine GameEvent/Nap prefabs.
- Existing 47 persistent `m_OnClick` records were not edited or removed. Sprite, Canvas, RectTransform, active defaults and existing objects were preserved.
- The HomeScene bridge and return hotkey are reused; no duplicate navigation subsystem. Home2 sets its Escape return target to `Home2Scene.unity` and enables Escape to close the current Popup/menu first.

## 11 Scene routes (candidate UI navigation)

| Button fileID | Visual button | Scene |
|---:|---|---|
| 1456 | Ship (ButtonMonsterClick) | MonsterScene |
| 1463 | World | WorldScene |
| 1467 | Lucky Spin | LuckySpinScene |
| 1483 | Forge | ForgeScene |
| 1496 | Summon Scroll | SummonScrollScene |
| 1532 | Altar | AltarScene |
| 1538 | Summon Orb | SummonOrbScene |
| 1585 | Inventory / Bag | ItemListScene |
| 1637 | Campaign | CampaignMapScene |
| 1699 | Heroes | HeroListNewScene |
| 1717 | Fusion | FusingHeroScene |

These target scene assets exist in the repo, but this is **not** proof that the choices match the old game or initialize playable gameplay.

## 14 Popup routes (existing Resources prefabs)

| Button fileID | Visual button | Resources path |
|---:|---|---|
| 1460 | Quest | prefabs/home/PopupDailyQuest |
| 1470 | Shop | prefabs/common/PopupShop |
| 1517 | Crossword treasure | prefabs/crosswordpuzzles/PopupCrosswordPuzzles |
| 1521 | Friends | prefabs/friend/PopupFriend |
| 1571 | First purchase | prefabs/home/PopupFirstPurchase |
| 1587 | Player avatar | prefabs/common/PopupPlayerInfo |
| 1611 | Rollcall / check-in **candidate** | prefabs/home/PopupCheckin |
| 1627 | Skins | prefabs/heroskins/PopupHeroSkins |
| 1657, 1688 | Chat (hot chat + button) | prefabs/common/PopupChatNew |
| 1743 | Mail | prefabs/home/PopupMailBox |
| 1477 | Events | prefabs/gameevent/PopupGameEvent |
| 1513 | Add Gem (recharge UI only) | prefabs/common/PopupNap |
| 1686 | Add Gold (recharge UI only) | prefabs/common/PopupNap |

Popup handlers open *offline previews only*. They do **not** implement quests, currency, account data, chat server, purchases, or real event claims. The prefab's existing close buttons and Escape are intended to close previews.

## 3 toggles of original Home2 objects

| Button fileID | Existing GameObject | Action |
|---:|---|---|
| 1444 | `PanelMenu` (GO 117, initially inactive) | Open/close |
| 1645 | `PanelMenu` (GO 117) | Close via existing background click |
| 1555 | `BoxSelect` (GO 134, initially inactive) | Open/close Summon submenu |

The route handler closes another open toggled menu when a different one is opened. Escape closes an open Popup first and then an open toggle.

## Remaining 19 callbacks — safe responses, business logic still unavailable

| Group | Original Button IDs | Reason |
|---|---|---|
| Boss test | 1453 | Original boss/test routing has not been verified |
| Facebook | 1576 | Requires actual external SDK / verified fanpage link |
| Achievement | 1619 | No confirmed achievement prefab or state provider |
| Quest target navigation | 1667, 1705 | Requires current HomeQuest state and correct target route |
| Reward, inventory and item-specific detail | 1754, 1805, 1822, 1831, 1968, 1971, 2081, 2115 | Requires correct item/HeroModel context; cannot show arbitrary stock item |
| Six hero ClickArea events | 1915–1920 | Requires the selected HeroModel; no hero profile loaded in extracted scene |

Each of these 19 buttons now opens the real `prefabs/common/PopupMessage.prefab` with a specific explanation of what recovered data is missing; the existing popup has working close actions in the navigation bridge. **This is an explicit unavailable-state UI**, not simulated purchases, profile contents, quest rewards or a recovered gameplay handler.

## Local Play Mode verification

1. `git switch feat/htdc-localization-hold`, `git pull --ff-only origin feat/htdc-localization-hold`, `git lfs pull`.
2. `py -3 -m unittest discover -s tests -p test_home2_scene_navigation.py -v`. Then run `py -3 -m unittest discover -s tests -p test_home_scene_navigation_pilot.py -v` to regression-check HomeScene.
3. Open `Assets/Scenes/Home2Scene.unity` in **Unity 2022.3.51f1**, clear Console and press Play.
4. Test each of the 11 Scene buttons, then `Esc` to Home2Scene and verify repeatability.
5. Test the 14 Popup buttons; each must appear, close with Close/Back or Escape, and reopen without duplicates. Verify Events, Add Gem and Add Gold open the actual prefab only (no real purchases).
6. Open/close `ButtonMenu` and `ButtonSummon`; test button for Scroll/Orb inside selection and Escape when a submenu is open.
7. Test each of the **19 context-dependent buttons** (Boss test, Facebook, Achievement, quest target, item slots, six hero click areas): inspect title, descriptive error and Close/Escape behavior. Do not mistake these notices for business-function PASS.
8. Check that original Layout/Spine/Sprites, buttons and Canvas remain unchanged. Record missing-script warnings separately from route behavior.
9. Unity static validation is not equivalent to C# compilation or Play Mode; mark **UNTESTED** until observed on the user's machine.

HomeScene's 55 persistent OnClick + 36 previously integrated routes remain intact. 206 quarantined localization assets and 206 .meta files stay outside Assets.
