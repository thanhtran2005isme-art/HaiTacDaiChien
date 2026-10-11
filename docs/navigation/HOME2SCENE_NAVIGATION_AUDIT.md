# Home2Scene — Navigation audit and offline restore

**Branch:** `feat/htdc-localization-hold`  
**Scene:** `Assets/Scenes/Home2Scene.unity`  
**Unity:** 2022.3.51f1 (AssetRipper export)  
**Type:** `NEW_PROJECT_DESIGN`, *not* verified original XAPK/IL2CPP runtime logic.

## Audit counts

- **47** serialized `m_OnClick` callbacks found, on **47** distinct Button components.
- **25** callbacks additionally wired through a single new `HomeSceneNavigationBridge` component (fileID **2120**) attached to existing `Scripts` GO **12**.
- Routes consist of **11 Scene navigation**, **11 Popup previews**, and **3 toggles** of original, previously inactive objects.
- **22** callbacks intentionally remain untouched; their original C# handlers are stubs or require missing game state.
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

## 11 Popup routes (existing Resources prefabs)

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

Popup handlers open *offline previews only*. They do **not** implement quests, currency, account data, chat server, purchases, or real event claims. The prefab's existing close buttons and Escape are intended to close previews.

## 3 toggles of original Home2 objects

| Button fileID | Existing GameObject | Action |
|---:|---|---|
| 1444 | `PanelMenu` (GO 117, initially inactive) | Open/close |
| 1645 | `PanelMenu` (GO 117) | Close via existing background click |
| 1555 | `BoxSelect` (GO 134, initially inactive) | Open/close Summon submenu |

The route handler closes another open toggled menu when a different one is opened. Escape closes an open Popup first and then an open toggle.

## Remaining 22 callbacks — deliberately untouched

| Group | Original Button IDs | Reason |
|---|---|---|
| Boss test | 1453 | ButtonTest / boss workflow not verified |
| Events | 1477 | Exact EventScene vs Popup/Event system not verified |
| Gem and Gold top-up | 1513, 1686 | Real money/currency purchase flow unavailable |
| Facebook | 1576 | External SDK/URL unavailable |
| Achievement | 1619 | No proven destination |
| Contextual quest targets | 1667, 1705 | Depend on HomeQuest data |
| Reward, item, equipment details | 1754, 1805, 1822, 1831, 1968, 1971, 2081, 2115 | Depend on model/item data |
| Six visible hero ClickArea events | 1915–1920 | HeroModel-bound detail action, not a generic Scene change |

Never silently wire these to an unrelated placeholder. They remain a separate game-state/asset-recovery task.

## Local Play Mode verification

1. `git switch feat/htdc-localization-hold`, `git pull --ff-only origin feat/htdc-localization-hold`, `git lfs pull`.
2. `py -3 -m unittest discover -s tests -p test_home2_scene_navigation.py -v`.
3. Open `Assets/Scenes/Home2Scene.unity` in **Unity 2022.3.51f1**, clear Console and press Play.
4. Test each of the 11 Scene buttons, then `Esc` to Home2Scene and verify repeatability.
5. Test the 11 Popup buttons; each must appear, close with Close/Back or Escape, and reopen without duplicates.
6. Open/close `ButtonMenu` and `ButtonSummon`; test button for Scroll/Orb inside selection and Escape when a submenu is open.
7. Check that original Layout/Spine/Sprites, buttons and Canvas remain unchanged. Record missing-script warnings separately from route behavior.
8. Unity static validation is not equivalent to C# compilation or Play Mode; mark **UNTESTED** until observed on the user's machine.

HomeScene's 55 persistent OnClick + 36 previously integrated routes remain intact. 206 quarantined localization assets and 206 .meta files stay outside Assets.
