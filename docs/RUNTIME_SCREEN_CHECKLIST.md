# Runtime UI verification checklist

**Status: NOT VERIFIED IN RUNTIME.** This is a manual validation protocol, not a claim the game has been launched or any private server is compatible.

The SVG wireframes use a hypothetical 1600 x 900 viewport. The actual device size, CanvasScaler scaling, Sprite masks, Spine effects, runtime scripts and server-loaded data are not recreated.

## Authorized capture workflow on Windows (CMD)

Use a permitted device and test account. Do not bypass login, payment or anti-cheat.

    adb devices
    adb shell wm size
    adb shell wm density
    adb shell dumpsys activity activities

In the app, navigate to a target UI panel, then capture and pull it:

    adb shell screencap -p /sdcard/ui-verify.png
    adb pull /sdcard/ui-verify.png

Rename each screenshot locally, documenting actual device resolution, Android version, orientation, game build and UI state. Do not commit copyrighted screenshots or personal player information into this public repository.

## Prioritized comparison matrix

| Serialized UI hierarchy root | Static wireframe | Runtime verification | Check |
|---|---|---|---|
| Splash Canvas | [SVG](../reports/xapk/wireframes/10-Canvas.svg) | NOT TESTED | logos/loading/aspect ratio |
| Login Canvas | [SVG](../reports/xapk/wireframes/11-Canvas.svg) | NOT TESTED | safe area/buttons/text fields |
| PanelHeroInfo | [SVG](../reports/xapk/wireframes/01-PanelHeroInfo.svg) | NOT TESTED | portraits/tabs/Spine art |
| PopupShop | [SVG](../reports/xapk/wireframes/02-PopupShop.svg) | NOT TESTED | product icons/prices/scroll |
| PopupDailyQuest | [SVG](../reports/xapk/wireframes/03-PopupDailyQuest.svg) | NOT TESTED | rewards/localized text |
| PopupFormationTest | [SVG](../reports/xapk/wireframes/04-PopupFormationTest.svg) | NOT TESTED | formation/animation |
| PopupPlayerInfo3 | [SVG](../reports/xapk/wireframes/05-PopupPlayerInfo3.svg) | NOT TESTED | player cards/dynamic sprites |
| PopupTowerLevelInfo | [SVG](../reports/xapk/wireframes/06-PopupTowerLevelInfo.svg) | NOT TESTED | levels/dialog geometry |
| PopupEventMonopoly | [SVG](../reports/xapk/wireframes/07-PopupEventMonopoly.svg) | NOT TESTED | effect timing/masks |
| PopupGuildWarPlayerInfo | [SVG](../reports/xapk/wireframes/08-PopupGuildWarPlayerInfo.svg) | NOT TESTED | badges/stats/art |
| PanelEasterPrayEvent | [SVG](../reports/xapk/wireframes/09-PanelEasterPrayEvent.svg) | NOT TESTED | animation/texture |
| Generic Canvas A | [SVG](../reports/xapk/wireframes/12-Canvas.svg) | NOT TESTED | identify runtime scene |
| Generic Canvas B | [SVG](../reports/xapk/wireframes/13-Canvas.svg) | NOT TESTED | identify runtime scene |

Names in serialized roots do not guarantee separate navigable screens.

## Compare these aspects

- Geometry: positions/sizes, anchor scaling, native display aspect, safe area.
- Art: sprite identity, atlas use, nine-slicing, masks, loading placeholders.
- Spine: skeleton skin, playback/transition, particle overlays.
- Behavior: dynamic Image.sprite updates, scrolling, events and server data.
- Evidence: screenshot filename, exact in-game state and discrepancies, kept in a permitted private location.

## Completion gates

- [ ] Screen is reachable in an authorized running client.
- [ ] Device display pixels, orientation and build version documented.
- [ ] Runtime screenshot is checked against the schematic tree.
- [ ] CanvasScaler/reference resolution checked from actual runtime or original project.
- [ ] Spine objects checked for correct playback in the game.
- [ ] Missing Image sprites observed (null/static/runtime) rather than guessed.
- [ ] Discrepancies and unavailable screens logged with no private credentials.

A serialized null Sprite pointer can be intentionally empty or filled dynamically; the static APK cannot distinguish these behaviors without runtime observation.
