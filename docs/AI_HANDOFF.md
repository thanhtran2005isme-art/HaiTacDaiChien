# AI HANDOFF — Hải Tặc UI Viewer

## 2026-10-12 — HeroListNewScene screenshot comparison: Vietnamese labels and clean hero cards

- User supplied running-game reference for the original HeroList and a current Unity Game View comparison. Reference has **Sắp xếp** and six Vietnamese faction captions; cards have source portrait/rank/metadata and **no overlaid Chọn button**. Current `OfflineHeroListPresenter` had created a large brown `OfflineTeamSelect` button on every card, which obstructed artwork and changed the original composition.
- **Code fix in PR #9 / `feat/offline-hero-ui`**: remove the generated `OfflineTeamSelect` controls entirely. Card click still opens the original `PanelHeroInfo` via `OfflineHeroDetailModal`; if an offline catalog is provided, an explicit **Thêm vào đội hình / Bỏ khỏi đội hình** action is now inside that detail overlay instead. Existing `OfflineHeroParty.Toggle` (max 6 distinct avatars, PlayerPrefs) and original home six slots remain functional in code. Card level field now shows `★<star>`, because source JSON includes stars but **does not contain live account level/owned inventory**. The narrow capacity control displays only `N/6`, not the prior overlapping `Đội hình offline: N/6` sentence.
- **Persistent Edit Mode fix**: intentionally update only text/related localization flags in `Assets/Scenes/HeroListNewScene.unity`. `Fortress → Nhanh nhẹn`, `Abyss → Bền bỉ`, `Forest → Chính xác`, `Shadow → Sinh khí`, `Dark → Thông minh`, `Light → Minh mẫn`, `Sort → Sắp xếp`. Previously fake static account capacity `12/100` becomes `—/6` in Edit Mode; runtime presenter supplies actual `N/6`. Seven corresponding I2 Text localization components are disabled to avoid resetting these particular labels to English when Play begins. Other source card sprite/Unity layouts stay unchanged.
- `Home2Scene.unity` was independently reserialized on this feature branch by earlier user's commit `bf338bc5` (`sửa lại bố cục home 2`), with different YAML escapes and extra objects. Kept that user change; static regression now checks serialized HUD invariants rather than stale whole-file hash/Unicode quoting. `HeroListNewScene` new scene Git blob `146c549f2e0fae59fb41dcc8048156a27b19452e`, prior blob `9631d65561998f318d3d784a9f73c0c71ed85568`.
- **QA required**: Unity 2022.3.51f1, close/reopen HeroListNewScene after git pull; Edit Mode shows Vietnamese sidebar + Sort and unchanged original card template; in Play Mode the four-column scroll cards have no Chọn overlay, clicking a hero opens detail, and the popup action adds/removes exactly one avatar while the narrow count updates and Home2 shows the selected party. Compare screenshots at the same Game View resolution; no claim of live inventory, levels 450/400 etc or pixel-perfect original. Static CI PASS does not prove visual behavior. PR #9 remains Draft/unmerged.

## 2026-10-12 — Permanent Home2 HUD fixes in Edit Mode **and** Play Mode (supersedes runtime-only handoff)

- User clarified the fixed UI must be correct with Play **off as well as on**; editing `Home2Scene.unity` was explicitly authorized for the three HUD differences. On branch `feat/offline-hero-ui` PR #9, edited the serialized Home2 Scene itself: `ButtonTest` (GO 10, extra FB) `m_IsActive=0`, `ButtonSkins` (GO 106) `m_IsActive=0`; inserted new `HTDC Offline Activity` GO 3000 directly under `GroupButton` between original `ButtonShop` and `ButtonShip`, with original game `home_button_events.asset` Sprite GUID `86cbb642c2b57314eabcb83baa79b219` and the existing source label panel/text style.
- This is a **deliberate, documented change to the serialized Scene**, unlike the earlier runtime-only correction. Original Scene blob before edit `ff2cb00d590a38d4c4b1384f15e5d5af0574d535` remains accessible in Git history; new Scene blob `245a72d6797f889e7b23cb5fdf6e927b291b4550`. All **47 old UnityEvent persistent callbacks** are preserved; there is **one additional empty** UnityEvent on the new button (48 `m_OnClick` blocks, 47 original targets). No source sprites or Prefabs were modified.
- `OfflineHome2HudCorrection.Apply` now detects the serialized Activity GameObject and binds only its click to the existing Home2 `ButtonEvents` offline popup while running. It cannot duplicate an already saved button. Player-selected hero roster remains runtime/PlayerPrefs only; editing must **not** bake account-specific hero data into the Scene.
- Static tests updated for intentional source-scene snapshot and original 47 callbacks. Require Unity 2022.3.51f1 Editor **with Play off and on** confirming one FB, no Skins, bottom order Shop → Hoạt động → Ship → Summon → Heroes → Bag, correct icon and functional Event click. GitHub CI tests do NOT prove visual rendering. Do not merge PR #9 until user verifies.
- **Supersession:** historical handoff entries saying `Home2Scene.unity` unchanged or HUD runtime-only refer to earlier commits; they are no longer true at current branch HEAD. `main` remains unchanged.

## 2026-10-12 — Follow-up from actual Unity Play Mode: Home2 HUD Canvas lookup failure

- User Play Mode Console reported `[HTDC Home2 HUD] Original Canvas not found.`; no Facebook/Skins/Activity visual changes occurred. Hero presenter DID run, logging `Offline party 6/6 loaded`, with names/stars visible and only some portraits.
- Confirmed via actual `Assets/Scenes/Home2Scene.unity` serialized object IDs: `Scripts` (GO 12, Transform 510) and `Canvas` (GO 121, RectTransform 1024, Canvas component 908) both have `m_Father: {fileID: 0}`: **two sibling Scene roots**, not parent/child. Incorrect code `home.transform.root.Find("Canvas")` searched children of Scripts and always failed.
- Fix: `OfflineHome2HudCorrection.FindSceneCanvas` now searches `home.gameObject.scene.GetRootGameObjects()` and checks the original root name and Canvas component; never looks for Canvas under Scripts or in unrelated loaded Scenes. Regression `test_canvas_is_resolved_from_sibling_scene_roots` locks original Scene structure and lookup. Runtime now logs `[HTDC Home2 HUD] Scene Canvas located: Canvas` before applying the requested HUD changes.
- Verify latest GitHub Actions static results and **user Unity Play Mode Game View** for one FB, no Skins, activity between shop and ship; if `Source event Sprite unavailable` arises, check correct Git LFS checkout/import; do not invent icon. PR #9 remains draft until verified. Original Scene/Prefabs/art unchanged.

## 2026-10-12 — User Home2 visual comparison: duplicate Fb, unwanted Skins, missing Activity

- User-provided original vs current Unity Home2 screenshots established three narrowly scoped HUD differences. Source `Home2Scene.unity` proves `ButtonTest` and `ButtonFb` use the same Facebook Sprite; `ButtonSkins` is separately serialized, and the bottom `GroupButton` has five buttons in original order.
- On `feat/offline-hero-ui` / PR #9, new **runtime-only** `OfflineHome2HudCorrection.cs` (called in `Home2Scene.Start`) disables duplicate `ButtonTest` and `ButtonSkins`, then clones the source Shop button and inserts **Hoạt động** between Shop and Ship. Uses original `home_button_events.asset` Sprite already bound in `HomeScene`, with the existing Home2 Events popup for safe offline interaction. New control is explicitly `NEW_PROJECT_DESIGN`, not a verified original game runtime route.
- Regressions: `tests/test_offline_home2_hud.py` locks source Scene blob and Sprite GUIDs; GitHub static CI must be checked on this new SHA. No original `.unity`, source PNG/Spine, original serialized button callbacks or DB are changed. **Must run Unity 2022.3.51f1 Play Mode, visually verify correct art/order and Console before PR merge.** Procedure in `docs/navigation/OFFLINE_HERO_UI.md`.


## 2026-10-12 — Home2Scene empty team still shows original Naruto labels

- **User Play Mode screenshot:** six original `Naruto` / `Lv.200` placeholders persisted; no reliable evidence of presenter activation in visible Console (999+ legacy warnings). Screenshot alone does not prove a specific failure cause or exact local HEAD.
- **Fix:** `Home2Scene.Start` and `HeroListNewScene.Start` (empty recovered C# stubs) now attach their own `OfflineHeroHomePresenter` / `OfflineHeroListPresenter` if absent, in addition to existing `OfflineHeroSceneRuntime.sceneLoaded`; explicit diagnostic `[HTDC Heroes]` Start log. These C# stub files **are modified**, but original `.unity` scene serialization, 47 Home2 callbacks and JSON are preserved.
- **Expected:** no selected offline party means six **blank** positions, not automatically populated heroes; user must select up to six source-released heroes under Tướng and return to Home2. Real imported art remains conditional on exact ID and Spine asset import.
- **Check:** GitHub CI source tests, then Unity 2022.3.51f1 Play Mode **must** verify zero red Console errors, presenter log, list, party storage, home names and artwork. Keep PR #9 draft/unmerged until confirmed. See `docs/navigation/OFFLINE_HERO_UI.md`.

## 2026-10-12 — Fix Unity Play Mode optional passive3 FormatException

- **User confirmed:** `OfflineHeroCatalog.ParseOptionalSkillId` threw `FormatException: Hero field passive3 is not an unsigned integer:` while entering `Home2Scene`.
- **Root cause:** source `db_heroes.json`, hero ID `51101` (Magellan BossC) stores `passive3: null`; Unity `JsonUtility` supplies empty string for this optional string field, bypassing old `value == null` guard.
- **Fix on `feat/offline-hero-ui`:** `string.IsNullOrWhiteSpace(value)` → internal `-1` (missing), keeps explicit source `"0"` distinct and rejects invalid nonempty IDs. Added regression in `tests/test_offline_hero_ui.py`; original source JSON, Scenes, prefabs untouched.
- **Verification:** GitHub CI static only; **needs user to `git pull --ff-only` and rerun Unity 2022.3.51f1 Play Mode**, see `docs/navigation/OFFLINE_HERO_UI.md`. Do not merge before Unity pass.

## 2026-10-12 — Original HeroList + six-slot Home2 offline UI (Unity test required)

- **Branch:** `feat/offline-hero-ui` based on `feat/offline-hero-catalog`; PR #9 draft; `main`, original scenes and prefabs not modified.
- **NEW_PROJECT_DESIGN runtime:** `OfflineHeroSceneRuntime` installs `OfflineHeroListPresenter` and `OfflineHeroHomePresenter` on original Scene scripts. 147 selectable source avatars from 154 released mainline entries. Original CellHeroThumb/ScrollView, six tab buttons, six HeroHome2/CanvasHero, original PanelHeroInfo prefab used. Click card = source detail; Chọn/Bỏ đội = PlayerPrefs opt-in party ≤6; empty slot opens list; selected slot opens info. No fake inventory or original `HeroModel`.
- **Assets:** exact hero `avatar` maps to `_pack/herohead/{avatar}` or `_pack/heroes2..5/{avatar}/Hero_{avatar}_SkeletonData`; Garp avatar 105 has LFS source, Uta avatar 456/Hiyori avatar 522 lack matching inspected source art. Missing asset never replaced with other character. `passive3=null` in ID 51101 now handled as absent (-1 internal sentinel), not invented as zero.
- **Testing:** `tests/test_offline_hero_ui.py` plus 5 previous static suites, `.github/workflows/offline-hero-ui.yml`, see `docs/navigation/OFFLINE_HERO_UI.md`. GitHub CI is STATIC ONLY; compile, Spine import, Game View and performance on Unity 2022.3.51f1 still unverified. Do not merge PR #9 before user Play Mode verification.
- **Deferred:** complete HeroModel/gameplay/stats, server inventory and ownership, skill descriptions, updated hero assets (Uta Last Wish), 2026 source version, real battles/purchases/account.

## 2026-10-12 — Offline hero catalog (giai đoạn 1, dữ liệu thật)

- **Nhánh làm việc mới:** `feat/offline-hero-catalog`, tách từ `feat/htdc-localization-hold`; không sửa `main`.
- **Đã thêm:** `Assets/Scripts/Assembly-CSharp/OfflineHeroCatalog.cs` + `.meta`, đọc `Resources/_pack/db/db_heroes.json` bằng `JsonUtility`, trả 751 bản ghi sắp tăng theo ID; chuẩn hóa sáu `type` không phân biệt hoa/thường; lọc `faction`, `star`, `release` và tra cứu theo ID. Không thay dữ liệu nguồn, không phát sinh ID, không có dữ liệu sở hữu tướng.
- **Đã xác thực từ nguồn:** 751 ID duy nhất, 203 `release=1`, 548 `release=0`; sáu hệ Fortress 135, Abyss 142, Forest 133, Shadow 131, Dark 93, Light 117. 406 tên riêng (bao gồm NPC/boss/test và nhiều cấp sao).
- **Kiểm tra:** `tests/test_offline_hero_catalog.py`, workflow `.github/workflows/offline-hero-catalog.yml`, Unity Editor menu **Tools → HTDC → Offline Heroes → Validate db_heroes.json**.
- **Giới hạn:** chỉ cung cấp repository dữ liệu + menu kiểm kê; `Home2Scene`, `HeroListNewScene`, `PanelHeroInfo` chưa được nối. Python static test PASS không chứng minh Unity Editor compile/Play Mode; cần người dùng xác minh tại máy.
- **Chi tiết:** `docs/navigation/OFFLINE_HERO_CATALOG.md`. Tiếp theo mới tích hợp danh sách tướng vào `HeroListNewScene`, tách dictionary khỏi inventory và ghép Sprite/Spine theo ID thực.

## 2026-10-12 — PanelHeroInfo 79/79 P1 offline responses

- **Branch giữ nguyên:** `feat/htdc-localization-hold`; không sửa P0 HomeScene (55), Home2Scene (47), P1 CelestialScene (47) hoặc `main`.
- **Source:** `Assets/Resources/prefabs/heroinfo/PanelHeroInfo.prefab` gốc 954 GameObject/3.997 block và 79 Button persistent `m_OnClick`. Đã gắn **một** `PanelHeroInfoOfflineBridge` mới vào root với đúng 79 bổ sung callback; giữ nguyên tất cả callback cũ, m_RectTransform, Sprite/Spine và active defaults. Original prefab Git SHA `e90f30707e4fccc14a350fd2eec58837cdbf765b` được regression test đối chiếu lại bằng cách loại phần bổ sung (source preservation).
- **14 local UI responses:** mở/đóng BoxShare; đóng panel chính, filter, detail; 7 tab bật tắt đúng 7 panel gốc cùng notice khi dữ liệu tướng không đủ. **65 callbacks còn lại** mở `PopupMessage` gốc để giải thích thiếu HeroModel/kho/trang bị/kỹ năng/server; không phát hàng/tiền, không thực hiện giao dịch.
- **Đã thêm:** `Assets/Scripts/Assembly-CSharp/PanelHeroInfoOfflineBridge.cs` + `.meta`, cập nhật `PanelHeroInfo.prefab`; test `tests/test_panel_hero_info_navigation.py`, workflow `.github/workflows/panel-hero-info-navigation.yml`, docs `docs/navigation/PANEL_HERO_INFO_NAVIGATION_AUDIT.md`.
- **Chưa xác nhận:** Unity Editor compile/Play Mode trong Scene sở hữu prefab, hoạt động mở lại sau nút Back, Canvas/popup layering, mọi control inactive, gameplay/account/backend. Test static/CI không chứng minh bản có thể chơi. Phải xem Actions của **HEAD mới nhất**.
- **Tiếp theo:** kiểm tra Unity 2022.3.51f1 bằng `py -3 -m unittest discover -s tests -p test_panel_hero_info_navigation.py -v` rồi Play Mode + Console, chưa merge; chuyển sang P1 khác khi có task tiếp theo.


## 2026-10-12 — CelestialScene 47/47 P1 source-only offline routes

- **Branch:** `feat/htdc-localization-hold`, tiếp tục branch đã có; hai P0 `HomeScene.unity` (55 callback) và `Home2Scene.unity` (47 callback) **không chỉnh sửa**.
- **Code:** thêm đúng một `HomeSceneNavigationBridge` có sẵn vào `Assets/Scenes/CelestialScene.unity`, trên `Scripts` GO 62, component fileID 1106; giữ nguyên 47 persistent `m_OnClick`. Thêm **1 Scene** trở về HomeScene, **2 PopupNap** preview (Gem/Gold), **44 PopupMessage** nói rõ thiếu `IslandModel`/`MineModel`/`BuildingModel`/`CelestialModel`/SDK và dữ liệu thật. Không thay UI, asset, bố cục hay gameplay.
- **Test:** `tests/test_celestial_scene_navigation.py` khóa từng cặp original Button target/method và tính đúng-một-lần 47 route, kiểm tra prefab tồn tại + regression P0. `.github/workflows/celestial-scene-navigation.yml` chạy static tests trên nhánh; phải xem Actions của **SHA mới nhất** trước khi tuyên bố PASS.
- **Giới hạn:** một số mỏ/công trình ở trạng thái inactive trong SerializedFile, không thể nhấn khi thiếu dữ liệu game. C# AssetRipper vẫn là các stub rỗng; 44 thông báo không tương đương khai thác/xây/nâng cấp/chiến đấu. `PopupNap` chỉ preview, không thực hiện thanh toán. **Unity Play Mode chưa được kiểm tra tại HEAD mới**.
- **Test tại Windows Unity 2022.3.51f1:** mở `Assets/Scenes/CelestialScene.unity`, kiểm tra Back, hai PopupNap/Close/Escape, PopupMessage đảo/mỏ/công trình và không có Console exception, so sánh P0. Chi tiết: `docs/navigation/CELESTIALSCENE_NAVIGATION_AUDIT.md`.
- **Tiếp theo:** nhận kết quả Unity Editor trước khi sửa UI cụ thể hoặc tuyên bố P1 gameplay PASS; không merge vào main tự động.


## 2026-10-12 — Home2Scene 47/47 click response follow-up

- **Same branch:** `feat/htdc-localization-hold`; no new branch. 47 original OnClick remain byte-for-byte unchanged.
- All 47 serialized buttons now have one extra offline response: **11 Scene**, **14 Popup** (incl. Events + Gem/Gold top-up UI), **3 Toggle**, **19 explicit unavailable-state notices** using original `PopupMessage` prefab. Shared `HomeSceneNavigationBridge.cs` supports `NoticeRoute`, no custom replacement layout and no removal of original handlers.
- **P0 click-coverage** is mapped; **P0 gameplay/business** still blocked for 19 callbacks requiring real HeroModel, item data, mission state, external SDK and server. Popup preview is **not** a confirmed transaction or complete gameplay loop.
- Regression test `tests/test_home2_scene_navigation.py` asserts exactly-once 47 route coverage and real resources. HomeScene's existing 55 OnClick + 36 routes remain serialized.
- Still **not tested in Unity 2022.3.51f1** at the new revision. Require Windows Play Mode re-test with no compile errors, all 47 controls (where actually active), no dead click or popup close problems before claiming verified P0 UI.
- Scope/evidence and test matrix: `docs/navigation/HOME2SCENE_NAVIGATION_AUDIT.md`.

## 2026-10-12 — Home2Scene navigation (P0)

- **Branch:** `feat/htdc-localization-hold`. `Assets/Scenes/Home2Scene.unity` có 47 persistent OnClick, giữ nguyên đủ 47. Component bridge `HomeSceneNavigationBridge` fileID `2120` được nối vào `Scripts` GO `12`.
- **Mới:** 11 chuyển Scene, 11 mở Popup preview qua Resources, 3 toggle từ 2 object `PanelMenu`/`BoxSelect` gốc; 25/47 callback có xử lý bổ sung. 22 callback chưa đủ bằng chứng dữ liệu/mô hình/SDK, giữ nguyên.
- **Shared code:** `HomeSceneNavigationBridge.cs` thêm `ToggleRoute` và `CloseOpenToggle`; `HomeSceneReturnHotkey.cs` mở rộng Esc đóng menu; HomeScene đã có trước giữ nguyên 55 OnClick.
- **Kiểm chứng:** serializer/GUID/target/47 callback đã đối chiếu; không có missing `m_Script: {fileID: 0}` trong Home2Scene được xuất. Chưa chạy Unity Play Mode cho Home2 mới; chưa tuyên bố đúng logic XAPK gốc.
- **Hướng dẫn:** `docs/navigation/HOME2SCENE_NAVIGATION_AUDIT.md`; `py -3 -m unittest discover -s tests -p test_home2_scene_navigation.py -v`. Đợi người dùng pull và test Unity 2022.3.51f1.
- **Bảo toàn:** main không thay đổi, `HTDC_Localization_Hold` vẫn ngoài Assets, không đụng tới binary XAPK/private.

## Bổ sung 2026-10-12 — AssetRipper HomeScene navigation pilot

- **Source hiện tại để test:** nhánh `feat/homescene-navigation-pilot`, bắt đầu từ `feat/ui-navigation-audit` (HEAD nguồn `643673b4`). Dự án AssetRipper xuất Unity **2022.3.51f1**, không phải project `unity-ui-viewer/` mô tả trong các đoạn handoff cũ phía dưới.
- **Main vẫn là `feee5b19`**, không phải baseline chứa AssetRipper; chưa merge pilot và không được pull `main` để kỳ vọng có source Unity mới.
- **Đã sửa:** giữ nguyên 55 persistent OnClick của `Assets/Scenes/HomeScene.unity`; thêm `HomeSceneNavigationBridge` vào Scripts GO, có 27 callback chuyển Scene (19 tuyến đầu được người dùng test trong Unity + 8 tuyến mới chưa xác minh) và 9 callback mở Popup prefab offline; Esc đóng Popup trước, sau đó Esc ở Scene đích quay về HomeScene. `NEW_PROJECT_DESIGN`, không tuyên bố các đích là logic game XAPK gốc.
- **Đã kiểm tra:** các nút tham chiếu component hợp lệ; 55 OnClick gốc không đổi; kiểm tra tĩnh trên GitHub. 19 tuyến đầu đã được người dùng xác nhận Play Mode hoạt động; 17 tuyến bổ sung (8 Scene + 9 Popup) chưa kiểm thử Unity Editor hoặc C# compiler.
- **Còn thiếu:** Hero/Campaign/Summon/Lucky/Brave Trial hiện đã gán SCENE ỨNG VIÊN, chưa được xác minh đúng game cũ; nhiều Popup mới chỉ có UI, chưa có data/manager; sự kiện, achievement, VIP, tài nguyên và nghiệp vụ còn thiếu. Stub C# vẫn rỗng; chưa có gameplay. `HTDC_Localization_Hold` vẫn cách ly ngoài Assets.
- **Tiếp theo:** người dùng pull trực tiếp `feat/htdc-localization-hold`, chạy `py -3 -m unittest discover -s tests -p test_home_scene_navigation_pilot.py -v`, mở HomeScene Unity 2022.3.51f1 và thử nút + Esc; gửi Console/Play Mode nếu lỗi. Xem `docs/navigation/HOMESCENE_NAVIGATION_PILOT.md`.
- **Lưu ý:** nội dung lịch sử dưới đây trước lúc import AssetRipper, vì vậy một số lệnh `.bat` và đường dẫn `unity-ui-viewer/` không còn áp dụng cho HEAD hiện tại của nhánh này.

**Cập nhật:** 2026-10-11 (Asia/Ho_Chi_Minh)
**Repo:** [thanhtran2005isme-art/HaiTacDaiChien](https://github.com/thanhtran2005isme-art/HaiTacDaiChien)
**Nhánh ổn định:** `main`
**Tuyến tích hợp:** PR #2 và #3 đã squash merge vào main; PR #4 là tuyến tổng hợp mã #5, #6, #7. Luôn xác minh HEAD/PR thực tế trước khi tiếp tục.
**Lịch sử chi tiết bảo toàn:** [history/2026-10-ref04-pr-stack-handoff.md](history/2026-10-ref04-pr-stack-handoff.md). Hồi 2026-10-09 đã có baseline 5 Canvas.

## Mục tiêu

Nguồn XAPK của Hải Tặc được phép sử dụng để tạo UI/asset **offline**, sau đó phát triển Unity client và backend **độc lập** (`NEW_PROJECT_DESIGN`). Server game cũ không còn hoạt động; không coi preview là game playable hay phục hồi UI gốc pixel-perfect. Mọi dữ liệu XAPK phải được kiểm chứng theo PathID/byte/schema; không suy đoán runtime.

## Đã có

- Web offline: `CHAY_UI_OFFLINE.bat` phục vụ `http://127.0.0.1:8765`; Unity Editor project `unity-ui-viewer/` sử dụng 2022.3.21f1, Spine 3.8 runtime tùy chọn có giấy phép.
- 5 cây UI tham chiếu gồm 1.670 RectTransform và 5.346 component refs; nguồn Sprite/Spine xuất vào `output/` local, Git ignored. Thuật toán pathID/Image owner tránh trùng đường dẫn.
- REF04 đã audit source: 503 RectTransform; 299 Image (265 Sprite-linked, 34 không có Sprite); 24 LayoutGroup và 168/168 giá trị serialized với hai schema; 62 Text, 52 I2 localizers (0 term independently verified), 2 Font source objects. Các số liệu **không chứng minh** runtime geometry, active characters hay pixel fidelity.
- P5 provenance có 245 entries gồm 168 layout fields, 15 native components (14 raw-SHA, 1 ID-only BLOCKED) và 62 Text; field thiếu source hash bị cách ly, không hợp thức hóa Unity runtime formula.
- P6 nguồn offline và Unity Editor workspace, P6.2 client-fit Scene `REF04-home-crew_NEW_CLIENT_FIT_STUDY.unity`, công cụ cover nền có backup, P6.3 **read-only** audit icon/HUD/PlayerHeroes xuất `output/ref04-p63-client-icon-layout-audit.json`. Scene/asset generated nằm trong `Assets/LocalReconstruction/` (ignored).
- Đã tích hợp 5 module Unity built-in cho Spine (`physics`, `physics2d`, `animation`, `audio`, `particlesystem`). CI Python/Linux/Windows chỉ xác minh hợp đồng mã và bằng chứng nguồn; **không chứng minh** đã compile hoặc render đúng Unity Editor.
- Baseline 2026-10-09: audit Unity local từng báo `missing Mono Scripts=0` và REF04 8 source Spine packs liên kết; không tự coi đây là test sau cùng ở commit mới.

## Vấn đề còn lại / giới hạn

1. **Unity Editor/Play Mode chưa xác nhận** đối với P6.2/P6.3 trên HEAD mới nhất. Background cover và viewport fit thuộc `NEW_PROJECT_DESIGN`; không phải CanvasScaler/SafeArea runtime nguyên gốc.
2. Icon nắm đấm lơ lửng, shadow nhân vật trống, thiếu HUD/Text/Localization; P6.3 mới **kiểm kê**, chưa di chuyển icon hay gắn nhân vật Spine.
3. Chưa có gameplay, API backend riêng, database, hoặc server hoạt động. Spine-Unity 3.8 hỗ trợ chính thức đến Unity 2020.3; project viewer dùng 2022.3 nên phải thử runtime trên bản sao phù hợp.
4. Không đưa XAPK binary/artwork, runtime Spine có điều kiện cấp phép, output private, LocalReconstruction vào commit mới. Pull Git **không** tái tạo asset local bị ignore.
5. Đối chiếu CI của chính SHA, không lấy test thành công của commit trước để công bố chất lượng runtime. Nhánh `main` yêu cầu quy trình PR, không force-push.

## Cách chạy

```cmd
git status
git fetch origin
CHUAN_BI_DO_HOA.bat
CHAY_UI_OFFLINE.bat
```

Unity: mở `unity-ui-viewer/` → `Tools → HaiTac Offline UI Viewer → Source XAPK → P6 - REF04 offline Unity workspace`. Audit icon: mở riêng P6.2 `NEW_CLIENT_FIT_STUDY`, dùng nút **9. Kiem tra icon, HUD va PlayerHeroes**, sau đó đọc Console/báo cáo private.

## Ưu tiên công việc kế tiếp

1. Xác minh trạng thái merged của chuỗi PR #2–#7 và HEAD thực tế; kiểm tra CI SHA tương ứng.
2. Chạy Unity Editor trên dữ liệu local có quyền dùng, xác minh P6 workspace/P6.2/P6.3, Console, Game View, đọc báo cáo icon owners trước khi thiết kế lại.
3. Chỉ sửa UI thuộc Scene `NEW_PROJECT_DESIGN` sau khi xác định nhóm nguồn; bổ sung Spine nhân vật và Text bằng nguồn hợp lệ, không bịa asset/tọa độ gốc.
4. Thiết kế backend mới độc lập theo task được giao sau, không tự tạo API/database hoặc tuyên bố gameplay đã có.
5. Khi thay đổi quan trọng, ghi quyết định tại `DECISIONS.md`, troubleshooting tại `TROUBLESHOOTING.md`, handoff ngắn và lịch sử ở `docs/history/`.
