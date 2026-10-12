# Offline hero UI — integration test handoff

**Branch:** `feat/offline-hero-ui` (parent: `feat/offline-hero-catalog`)  
**Status:** source implementation + static CI; **Unity Editor compile and Play Mode NOT verified by GitHub Actions**.  
**Architecture:** `NEW_PROJECT_DESIGN`, never recovered original IL2CPP gameplay.

## What is implemented

- `OfflineHeroSceneRuntime` automatically attaches runtime-only presenters after opening the real
  `HeroListNewScene` or `Home2Scene`. No change to imported `.unity` files or 47 source `m_OnClick` callbacks.
- `OfflineHeroListPresenter` uses existing `CellHeroThumb`, `ScrollView/Content`, existing
  `TabFaction` six buttons, `ButtonDictionary` (all), and original `ButtonBack`.
  Layout 190×260, 10px gap, four columns is from the serialized `GridPoolGroup`, not an invented replacement UI.
  For this dictionary/selectable subset, display `release=1`, main hero ID ≤530, star ≥5,
  one highest-star released source variant **per avatar ID**: 154 eligible records → 147 distinct avatar IDs.
  This is NOT the player's inventory.
- `OfflineHeroParty` stores an **explicit** 0–6-item selection using `PlayerPrefs` key
  `htdc.offline.party.v1.ids`. Default is empty, never generates player-owned heroes.
  The list card opens source `PanelHeroInfo` on click; the small `Chọn/Bỏ đội`
  control selects/deselects separately. Capacity enforcement prevents seventh distinct avatar.
- `OfflineHeroHomePresenter` reads precisely six serialized `Home2Scene.m_ListHeroHome`
  (source IDs 1980, 1979, 1981, 1978, 1982, 1977). Replaces `Naruto` and static
  `Lv.200` *only in Play Mode*: real name/star or blank for empty positions.
  It reuses the original six CanvasHero/SkeletonGraphic and click areas, replaces only their six
  earlier missing-HeroModel notice listeners. Clicking an empty slot opens the original
  `HeroListNewScene`; clicking a selected slot opens the original `PanelHeroInfo.prefab`.
- `OfflineHeroArt` uses **exact** `avatar` ID from the JSON:
  `Assets/Resources/_pack/herohead/{avatar}.png` for source portrait;
  `Assets/Resources/_pack/heroes{2..5}/{avatar}/Hero_{avatar}_SkeletonData.asset` for Spine.
  `Resources.Load` checks existence at runtime, makes no guess/fallback to other heroes.
  Setup pose / `idle` animation if source skeleton provides it; if unavailable,
  the same avatar's portrait is used when present. Missing both: show source name and a warning.
- `OfflineHeroDetailModal` opens existing original `prefabs/heroinfo/PanelHeroInfo.prefab`,
  binds `HeroSpineInfo.m_TextHeroName` and portrait where available, displays source
  ID, faction, star, class and raw 4 skill IDs as source configuration.
  A null passive3 in hero ID 51101 remains **absent** (not changed to a fictional skill).
  Existing gameplay tabs remain stub/notice-only. Does NOT generate a working `HeroModel`
  or server statistics.

## Source-asset limitations you will see

- Garp original avatar 105: source portrait `_pack/herohead/105.png` and skeleton
  `_pack/heroes2/105/Hero_105_SkeletonData.asset` are in repo **as LFS objects**.
  Must checkout `git lfs pull`; actual Spine runtime import/render still needs Unity validation.
- Uta original avatar 456 and Hiyori avatar 522: their exact `herohead/{id}.png`
  and `Hero_{id}_SkeletonData.asset` were not found in checked `Resources` tree.
  They remain catalog entries with no substitute art. **Uta Last Wish** is not in source JSON.
- The imported `HeroListNewScene` Canvas has serialized zero scale; presenter
  adjusts only the runtime instance to scale one, leaving source Unity YAML unchanged.
- The original `HeroModel`, gameplay level, attributes/skill descriptions, account inventory,
  ownership, ranks, materials, backend, new 2026 hero assets and battle logic are **not**
  restored. View `OfflineHeroCatalog` / `OfflineHeroParty` as independent local previews.

## Local Windows Unity test

```powershell
git status --short
git fetch origin
git switch --track origin/feat/offline-hero-ui
git lfs pull
py -3 -m unittest discover -s tests -p test_offline_hero_ui.py -v
py -3 -m unittest discover -s tests -p "test_*.py" -v
```

If you already have the branch locally: `git switch feat/offline-hero-ui`;
then `git pull --ff-only origin feat/offline-hero-ui`. If `git status --short`
is not empty, back up or review your uncommitted work before switching.
Do NOT force-pull, reset or discard local work.

1. Unity version 2022.3.51f1: open repo root and inspect **Console** for compile errors first.
2. Open `Assets/Scenes/Home2Scene.unity`, press **Play**. Without previously selected heroes,
   the six original Naruto labels should be blank. Tapping an empty hero position
   should open `HeroListNewScene`.
3. Verify original list/scroll shows real hero names, star, portraits where available;
   tap each of 6 faction tabs and the all/dictionary button. Try selecting 6, then
   a seventh (must refuse), then deselect; open info via hero card.
4. Go Back and check six source hero names/star and genuine Spine/portrait where available.
   Test Garp avatar 105 to validate import; if missing, inspect Git LFS, skeleton
   references, spine-unity import/version, Console exceptions.
5. Click assigned hero → source original `PanelHeroInfo` opens, correct source
   name/faction/star/class/skill IDs; Back/Escape close without leaking overlays;
   reopen, navigate and repeat.
6. Reenter Play Mode or reopen Unity to confirm PlayerPrefs selection persists.
   Verify scene switch back, no duplicate button notices, no new error.
7. Send Unity Console (red/yellow lines), HeroList and Home2 Game View screenshots
   before marking Play Mode PASS / merging any PR.

**Important:** static GitHub CI checks only source/dataset/regression and does not
run Unity Editor or validate Spine atlas dependencies in Git LFS files.
