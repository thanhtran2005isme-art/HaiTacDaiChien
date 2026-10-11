# HomeScene navigation pilot — NEW_PROJECT_DESIGN

Branch feat/homescene-navigation-pilot is based on feat/ui-navigation-audit.
These routes are a NEW offline implementation and are NOT verified original XAPK/IL2CPP behavior.
HomeScene retains all 55 original Unity persistent OnClick callbacks, visual transforms, sprites and layout.
One HomeSceneNavigationBridge MonoBehaviour is added to the Scripts GameObject (GO 5, component 1385).
The bridge registers 19 additional runtime Button callbacks for existing scene-name candidates:
GuildScene, ItemListScene, MonsterScene, AltarScene, ArenaScene, DungeonScene,
FusingHeroScene, TarvenScene, SealedLand, ForgeScene and TowerNewScene.
Building body and NameTag buttons are mapped to the same candidate scene.
Editor Play Mode falls back to EditorSceneManager.LoadSceneInPlayMode if a scene is not in Build Settings.
A persistent keyboard-only helper returns to HomeScene on Escape in the Game View.
No original OnClick has been removed; no Localization Hold asset is reimported.

## Unverified and intentionally untouched

- Hero selection: HeroListScene versus HeroListNewScene.
- Campaign: CampaignScene versus CampaignMapScene.
- Lucky/Brave Trial/Summoning House and all unnamed Popup routes remain ambiguous.
- The 19 routes are explicitly **candidates**, not a claim of original behavior.
- Stubs for BaseScene, HomeScene, Lua/backend logic and Popup state remain empty.
- Building to gameplay, account state, asset loader, save and network remain unimplemented.
- Build Settings is stored using LFS and was not modified; packaged player builds need configured scenes.

## Pull and test on local Windows

From an unchanged working tree in C:\Users\Admin\Videos\HTDC\HaiTacDaiChien:

    git fetch origin
    git switch --track origin/feat/homescene-navigation-pilot
    git lfs pull
    py -3 -m unittest discover -s tests -p test_home_scene_navigation_pilot.py -v

Open Assets/Scenes/HomeScene.unity in Unity 2022.3.51f1 as a standard Windows user.
Open Console, start Play, click Guild/Bag/Monster and Altar/Arena/Dungeon/Tarven/Forge/
FusingHouse/SealedLand/Tower (both building body and NameTag), press Escape to return.
Confirm actual Unity compile and scene transitions manually; Python checks do NOT prove this.
If Unity reports errors or freezes, stop, preserve logs and do not mark PASS.
