# UI Sprite linkage coverage

Generated from **verified Sprite target objects** and UI hierarchy paths in `ui-image-sprite-links.csv`. Each row identifies a Sprite reference candidate inside a Unity UI.Image component; this is **not a reconstructed screen or a working source project**.

## Overview

| Item | Count |
|---|---:|
| All Image components | 19,643 |
| Sprite reference candidates with exactly one verified target | 16,886 |
| Corroborated (repeating offset) | 16,885 |
| Without offset corroboration | 1 |
| Unresolved Image components | 2,757 |
| UI link rows missing hierarchy path | 0 |
| Sprite link rows with unreadable name | 0 |
| Unique root groups (serialized file + root name) | 449 |

## Representative links

| UI hierarchy path | Sprite name | Confidence |
|---|---|---|
| `/Canvas/SplashInit/ImageLogo` | `logo_4_legends` | probable_m_sprite |
| `/Canvas/SplashInit/ImageBgr` | `bgr_splash2` | probable_m_sprite |
| `/Canvas/SplashTheme/AnchorTopLeft/LogoEn` | `logo_global` | probable_m_sprite |
| `/Canvas/SplashTheme/AnchorBottomLeft/ImageChar` | `ace_logo` | probable_m_sprite |
| `/Canvas/SplashTheme/AnchorRight/ImageName` | `ace_title` | probable_m_sprite |
| `/Canvas/SplashTheme/ImageBgr` | `ace_bgr` | probable_m_sprite |
| `/Canvas/SplashTheme/AnchorTopLeft/LogoVn` | `logo_small` | probable_m_sprite |
| `/Canvas/BoxRegister/PanelFieldRegister/Viewport/Layout/FieldPhone/TextPhone/FieldPhone` | `rect_25` | probable_m_sprite |
| `/Canvas/BoxRegister/BoardTitle` | `board_title_2` | probable_m_sprite |
| `/Canvas/BoxRegister/PanelFieldRegister/Viewport/Layout/FieldCMT/TextCMT/FieldCMT` | `rect_25` | probable_m_sprite |
| `/Canvas/BoxRegister/PanelFieldRegister/Viewport/Layout/FieldPassword2/TextPassword2/FieldPassword2` | `rect_25` | probable_m_sprite |
| `/Canvas/BoxRegister/PanelFieldRegister/Viewport/Layout/FieldBirth/TextBirth/FieldBirth` | `rect_25` | probable_m_sprite |
| `/Canvas/BoxRegister/ButtonRegister` | `button_green` | probable_m_sprite |
| `/Canvas/BoxRegister/PanelFieldRegister/Viewport/Layout/FieldEmail/TextMail/FieldEmail` | `rect_25` | probable_m_sprite |
| `/Canvas/BoxRegister/PanelFieldRegister/Viewport/Layout/FieldPassword1/TextPassword1/FieldPassword1` | `rect_25` | probable_m_sprite |
| `/Canvas/BoxRegister/PanelFieldRegister/Viewport/Layout/FieldUserName/TextUsn/FieldUsername` | `rect_25` | probable_m_sprite |
| `/Canvas/BoxRegister/PanelFieldRegister/Viewport/Layout/FieldFullName/TextFullName/FieldFullname` | `rect_25` | probable_m_sprite |
| `/Canvas/BoxRegister` | `box_login` | probable_m_sprite |
| `/Canvas/BoxRegister/ButtonBack` | `button_yellow` | probable_m_sprite |
| `/Canvas/PopupFormationTest2/PanelTop/PanelFormation/DropdownPet1/Template/Scrollbar/Sliding Area/Handle` | `UISprite` | probable_m_sprite |
| `/Canvas/PopupFormationTest2/PanelTop/PanelFormation/DropdownPet1/Mask/Image` | `item_none` | probable_m_sprite |
| `/Canvas/PopupFormationTest2/PanelTop/PanelFormation/DropdownPet1/Template/Scrollbar` | `Background` | probable_m_sprite |

## Root groups by linked image count

Root names (such as `Canvas`, `PanelHeroInfo` and `PopupShop`) identify **serialized hierarchy roots**, not necessarily a unique runtime screen. Repeated names are distinguished by serialized file.

| Root | Serialized file | Linked Image nodes | Unique Sprite IDs | Example Sprite names |
|---|---|---:|---:|---|
| `PanelHeroInfo` | `002_UnityDataAssetPack_datapack__file110` | 574 | 113 | `hero_star_2`, `item_transparent`, `hero_star_1`, `bgr_item_consumable`, `faction_forest` |
| `PopupPlayerInfo3` | `002_UnityDataAssetPack_datapack__file110` | 484 | 31 | `hero_star_e`, `icon_power`, `border_gear`, `border_avatar`, `mask_item` |
| `PopupGuildWarPlayerInfo` | `002_UnityDataAssetPack_datapack__file110` | 348 | 49 | `war_star_slot`, `war_star`, `round_rect_4`, `button_yellow`, `popup_body` |
| `PopupTowerLevelInfo` | `002_UnityDataAssetPack_datapack__file110` | 318 | 28 | `popup_close`, `1`, `icon_power`, `button_back`, `bgr_item_consumable` |
| `PopupEventMonopoly` | `002_UnityDataAssetPack_datapack__file110` | 308 | 35 | `dice_map_tile_green`, `button_gray`, `dice_trigger_fruit`, `dice_map_tile_red`, `dice_trigger_more_dice_2` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file011` | 302 | 55 | `faction_fortress`, `tag_title`, `button_green`, `tab_star_on`, `faction_shadow` |
| `GGEasterPrayEvent` | `002_UnityDataAssetPack_datapack__file110` | 268 | 42 | `border_equipment_2`, `item_star`, `item_placeholder`, `bgr_equipment_1`, `frame_item` |
| `PanelEasterPrayEvent` | `002_UnityDataAssetPack_datapack__file110` | 266 | 45 | `bgr_item_consumable`, `border_item`, `mask_item`, `item_transparent`, `icon_gold` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file025` | 265 | 88 | `notify_dot`, `home2_menu_right`, `UIMask`, `field_res_home`, `home2_fb` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file071` | 265 | 88 | `notify_dot`, `home2_menu_right`, `UIMask`, `field_res_home`, `home2_fb` |
| `PopupSelectReward` | `002_UnityDataAssetPack_datapack__file110` | 250 | 34 | `Background`, `frame_reward`, `mask_item`, `bgr_item_consumable`, `icon_exp_vip` |
| `PopupMasteringHero` | `002_UnityDataAssetPack_datapack__file110` | 241 | 59 | `bgr_light`, `popup_notitle_2`, `button_close_4`, `faction_abyss`, `bgr_item_consumable` |
| `PopupGuildMill` | `002_UnityDataAssetPack_datapack__file110` | 221 | 62 | `item_star`, `box_field_1`, `progress_buff_bgr`, `progress_top_2`, `round_rect_7` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file007` | 219 | 27 | `bgr_chat`, `team`, `border_aura`, `UISprite`, `bgr_1` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file003` | 183 | 37 | `InputFieldBackground`, `button_green`, `UISprite`, `item_none`, `button_red` |
| `PopupPrepareGuildWar` | `002_UnityDataAssetPack_datapack__file110` | 182 | 42 | `button_green`, `rect_15`, `tick_green`, `round_rect2`, `button_yellow` |
| `PopupGuildBossTopDmg` | `002_UnityDataAssetPack_datapack__file110` | 177 | 41 | `bgr_item_consumable`, `bgr_light`, `border_item`, `tick_green`, `item_transparent` |
| `PopupDailyQuest` | `002_UnityDataAssetPack_datapack__file110` | 176 | 38 | `round_rect2`, `popup_body`, `popup_close`, `UIMask`, `Background` |
| `PopupMFStage` | `002_UnityDataAssetPack_datapack__file110` | 176 | 30 | `rect_26`, `border_gear`, `popup_body`, `popup_close`, `button_yellow` |
| `PopupPlayerInfo` | `002_UnityDataAssetPack_datapack__file110` | 170 | 33 | `hero_5_star`, `bgr_item_consumable`, `hero_head_border`, `hero_star_1`, `frame_item` |
| `CellDailyQuest` | `002_UnityDataAssetPack_datapack__file110` | 170 | 34 | `tick_green`, `bgr_light`, `button_red`, `progress_bgr`, `progress_top_2` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file045` | 169 | 50 | `box_field_1`, `button_small_shop`, `icon_gem`, `dungeon_skip_shop`, `dungeon_battle_inactive` |
| `CellRaidChallenge` | `002_UnityDataAssetPack_datapack__file110` | 169 | 37 | `button_gray`, `button_red`, `bgr_light`, `icon_power`, `faction_light` |
| `CellGameChallenge` | `002_UnityDataAssetPack_datapack__file110` | 169 | 34 | `button_red`, `progress_bgr`, `bgr_light`, `progress_top_2`, `tick_green` |
| `PopupGuildBossRewards` | `002_UnityDataAssetPack_datapack__file110` | 169 | 36 | `Background`, `round_rect_7`, `UIMask`, `round_rect_8`, `button_close` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file035` | 168 | 82 | `129`, `xoay`, `box_field_1`, `tag_name_home`, `notify_dot` |
| `PopupBravePlayer` | `002_UnityDataAssetPack_datapack__file110` | 166 | 29 | `hero_head_border`, `mask_item`, `frame_item`, `round_rect2`, `border_gear` |
| `PopupBravePlayerNew` | `002_UnityDataAssetPack_datapack__file110` | 166 | 29 | `star_e_bgr`, `hero_head_hidden`, `hero_star_e`, `hero_star_2`, `faction_light` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file081` | 153 | 18 | `dot_path`, `button_help`, `box_gift`, `rage`, `rock` |
| `PopupDungeonStageHero` | `002_UnityDataAssetPack_datapack__file110` | 153 | 21 | `popup_close`, `round_rect_content`, `popup_body`, `hero_head_hidden`, `progress_top_2` |
| `PopupAllStarsTop4` | `002_UnityDataAssetPack_datapack__file110` | 148 | 16 | `sicbo`, `button_close`, `round_rect_13`, `line_connect_dark`, `shape2` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file053` | 141 | 13 | `dot_path`, `button_shop_brave`, `round_rect_10`, `button_help`, `bgr_brave_trial` |
| `PanelExchangeEquipEvent` | `002_UnityDataAssetPack_datapack__file110` | 140 | 21 | `25`, `item_star`, `arrow`, `bgr_equipment_1`, `border_equipment_2` |
| `PopupSpecialEquipUpgrade` | `002_UnityDataAssetPack_datapack__file110` | 140 | 25 | `hero_star_3`, `bg_12`, `item_star`, `hero_star_2`, `E_4` |
| `PopupUpgradeArtifact` | `002_UnityDataAssetPack_datapack__file110` | 139 | 24 | `item_star`, `1`, `slot_empty`, `frame_item`, `tick_green` |
| `GGEquipmentExchangeEvent` | `002_UnityDataAssetPack_datapack__file110` | 130 | 15 | `arrow`, `box_field_1`, `icon_gold`, `button_yellow`, `icon_gem` |
| `PopupShop` | `002_UnityDataAssetPack_datapack__file110` | 127 | 61 | `icon_soul_coin`, `box_field_1`, `button_green`, `bar_price`, `icon_gem` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file061` | 120 | 53 | `box_field_1`, `button_green`, `icon_quest_scroll_senior`, `icon_quest_scroll_basic`, `bgr_box_2` |
| `Popup7DaysEvent` | `002_UnityDataAssetPack_datapack__file110` | 108 | 53 | `bgr_light`, `hero_head_hidden`, `round_rect_7`, `decor`, `item_transparent` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file069` | 101 | 24 | `box_field_1`, `button_add`, `icon_progress_bar`, `icon_purple_daffodil`, `local_building` |
| `PanelDauDanhTrangEvent` | `002_UnityDataAssetPack_datapack__file110` | 100 | 43 | `button_yellow`, `button_help`, `1`, `button_gift`, `event_than_tuong` |
| `PopupTarvenQuest` | `002_UnityDataAssetPack_datapack__file110` | 98 | 42 | `tarven_star`, `button_green`, `round_rect2`, `bgr_brown`, `popup_close` |
| `PanelItemExchangeEvent` | `002_UnityDataAssetPack_datapack__file110` | 98 | 43 | `round_rect_content`, `tag_sold_out`, `bgr_light`, `1`, `limited` |
| `PopupCheckin` | `002_UnityDataAssetPack_datapack__file110` | 95 | 43 | `box_text_1`, `round_rect_content`, `round_rect_11`, `button_yellow`, `title_checkin` |
| `PanelHeroLotteryEvent` | `002_UnityDataAssetPack_datapack__file110` | 95 | 43 | `button_yellow`, `button_help`, `1`, `button_gift`, `event_than_tuong` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file085` | 94 | 17 | `bgr_hero_campaign`, `frame_campaign`, `head_1`, `BGfake1`, `mark_death` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file099` | 94 | 17 | `bgr_hero_campaign`, `border_equipment_1`, `head_1`, `bgr_stage2`, `mark_death` |
| `PopupSmashedShop` | `002_UnityDataAssetPack_datapack__file110` | 94 | 41 | `popup_body`, `box_field_1`, `UIMask`, `icon_gem`, `round_rect_3` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file027` | 93 | 38 | `faction_fortress`, `arrow_curve`, `faction_shadow`, `faction_shadow_ia`, `faction_light_ia` |
| `PopupBuyItem` | `002_UnityDataAssetPack_datapack__file110` | 92 | 40 | `button_yellow`, `popup_body`, `frame_item`, `box_field_1`, `icon_gem` |
| `PopupBuyItemFake` | `002_UnityDataAssetPack_datapack__file110` | 92 | 40 | `button_yellow`, `popup_body`, `frame_item`, `box_field_1`, `gem_icon_en` |
| `PanelEventMiniGame` | `002_UnityDataAssetPack_datapack__file110` | 91 | 37 | `UIMask`, `tick_green`, `border_item`, `bgr_item_consumable`, `item_transparent` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file047` | 90 | 47 | `hero_star_1`, `floor_replace`, `button_info2`, `button_green`, `hero_star_2` |
| `PopupSelectGameItem` | `002_UnityDataAssetPack_datapack__file110` | 90 | 38 | `tick_green`, `rect_14`, `button_yellow`, `UIMask`, `round_rect2` |
| `PopupSellItem` | `002_UnityDataAssetPack_datapack__file110` | 90 | 39 | `popup_close`, `popup_body`, `button_yellow`, `box_field_1`, `border_item` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file043` | 89 | 32 | `button_battle_log`, `progress_buff_3`, `line_edge_war_map`, `button_help_3`, `tag_war_ally` |
| `GGDauDanhTrangEvent` | `002_UnityDataAssetPack_datapack__file110` | 89 | 35 | `progress_bgr`, `bgr_light`, `round_rect_7`, `UISprite`, `tick_green` |
| `PopupDungeonBuyItem` | `002_UnityDataAssetPack_datapack__file110` | 87 | 38 | `button_yellow`, `popup_close`, `popup_body`, `icon_gem`, `round_rect_3` |
| `GGItemExchangeEvent` | `002_UnityDataAssetPack_datapack__file110` | 87 | 37 | `tag_sold_out`, `bgr_light`, `1`, `limited`, `button_yellow` |
| `ItemBonusShow` | `002_UnityDataAssetPack_datapack__file110` | 85 | 35 | `bgr_show_bonus`, `line_sm`, `UIMask`, `tick_green`, `bgr_item_consumable` |
| `GGHeroLotteryEvent` | `002_UnityDataAssetPack_datapack__file110` | 85 | 36 | `UIMask`, `round_rect_3`, `bgr_toast`, `tick_green`, `bgr_item_consumable` |
| `EasterStatue` | `002_UnityDataAssetPack_datapack__file110` | 85 | 34 | `bgr_light`, `faction_abyss`, `tick_green`, `hero_star_1`, `icon_shard` |
| `PopupItemInfo` | `002_UnityDataAssetPack_datapack__file110` | 85 | 36 | `button_preview`, `UIMask`, `round_rect_3`, `faction_abyss`, `bgr_item_consumable` |
| `GameItemEvent` | `002_UnityDataAssetPack_datapack__file110` | 84 | 33 | `bgr_light`, `tick_green`, `border_item`, `mask_item`, `bgr_item_consumable` |
| `PanelCheckinEvent` | `002_UnityDataAssetPack_datapack__file110` | 83 | 31 | `mask_item`, `hero_star_1`, `bgr_equipment_1`, `UISprite`, `frame_item` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file037` | 82 | 26 | `button_orange2`, `tab_regress_ia`, `tab_change_ia`, `tab_change`, `tab_regress` |
| `PopupFormationTest` | `002_UnityDataAssetPack_datapack__file110` | 82 | 35 | `item_none`, `Checkmark`, `UISprite`, `DropdownArrow`, `UIMask` |
| `GameItem` | `002_UnityDataAssetPack_datapack__file110` | 82 | 33 | `UISprite`, `star_e_bgr`, `hero_head_border`, `bgr_item_consumable`, `hero_5_star` |
| `GameItemFeeEquip` | `002_UnityDataAssetPack_datapack__file110` | 82 | 33 | `UISprite`, `star_e_bgr`, `item_star`, `bgr_equipment_1`, `frame_item` |
| `GameItemWrapper` | `002_UnityDataAssetPack_datapack__file110` | 82 | 33 | `faction_shadow`, `bgr_item_consumable`, `mask_item`, `icon_exp_vip`, `border_item` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file107` | 81 | 39 | `icon_chip_fake`, `boad_title_shop`, `rect_13`, `rect_bottom`, `box_field_1` |
| `PopupAllStarsTop2` | `002_UnityDataAssetPack_datapack__file110` | 81 | 16 | `shape2`, `button_close`, `round_rect_13`, `line_connect`, `line_connect_dark` |
| `PopupBattleDetail` | `002_UnityDataAssetPack_datapack__file110` | 81 | 29 | `frame_item`, `icon_vs`, `tab_active_1`, `icon_armor`, `hero_head_border` |
| `PopupHeroESkill` | `002_UnityDataAssetPack_datapack__file110` | 79 | 21 | `Background`, `chi_point`, `popup_enable`, `button_close_3`, `board_enable` |
| `HeroesLineup` | `002_UnityDataAssetPack_datapack__file110` | 78 | 6 | `faction_light`, `bgr_light`, `star_1`, `tick_green`, `star_6` |
| `PopupPet` | `002_UnityDataAssetPack_datapack__file110` | 76 | 34 | `skill_selected`, `skill_default`, `button_up`, `tag_number`, `frame_pet` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file041` | 75 | 32 | `notify_dot`, `Background`, `button_yellow`, `tab_weapon_ia`, `button_plus` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file109` | 72 | 43 | `icon_chip_fake3`, `boad_title_shop`, `notify_dot_2`, `rect_13`, `box_field_1` |
| `PopupHeroAwakenResult` | `002_UnityDataAssetPack_datapack__file110` | 68 | 27 | `arrow_2`, `round_rect_5`, `skill_default`, `popup_body`, `tag_number` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file059` | 64 | 40 | `icon_power`, `icon_hand`, `arrow_up`, `button_fk_heroes`, `button_fk_spin` |
| `ItemSetEquip` | `002_UnityDataAssetPack_datapack__file110` | 64 | 12 | `item_star`, `bgr_equipment_1`, `mask_item`, `frame_item`, `border_equipment_2` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file013` | 63 | 35 | `tab_hero_ia`, `frame_hero_thumb`, `round_rect2`, `tab_hero`, `Background` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file077` | 63 | 35 | `tab_hero_ia`, `frame_hero_thumb`, `round_rect2`, `tab_hero`, `Background` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file055` | 62 | 31 | `Background`, `UIMask`, `button_back`, `gate_7`, `gate_4` |
| `PopupFriend` | `002_UnityDataAssetPack_datapack__file110` | 62 | 36 | `bgr_lv`, `button_red`, `round_rect_7`, `button_yellow`, `button_list_apply` |
| `PopupSelectEquipmentSet` | `002_UnityDataAssetPack_datapack__file110` | 61 | 15 | `rect_27`, `popup_close`, `button_green`, `UIMask`, `popup_body` |
| `CampaignMapPart` | `002_UnityDataAssetPack_datapack__file110` | 59 | 28 | `gate_7`, `gate_4`, `campaign_map`, `gate_5`, `tag_name_home` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file017` | 58 | 15 | `progress_2`, `faction_aura`, `faction_abyss`, `UISprite`, `battle_x1` |
| `PopupGuildSkill` | `002_UnityDataAssetPack_datapack__file110` | 58 | 24 | `button_help`, `warrior_armor_break`, `box_field_1`, `tab_ranger_2`, `round_rect_3` |
| `PopupSettings` | `002_UnityDataAssetPack_datapack__file110` | 58 | 38 | `tab_account_off`, `tab_on`, `help_on`, `tab_off`, `UISprite` |
| `PopupHeroEquipmentInfo` | `002_UnityDataAssetPack_datapack__file110` | 58 | 13 | `item_star`, `bgr_equipment_1`, `tick_green`, `frame_item`, `button_close` |
| `PopupFormation` | `002_UnityDataAssetPack_datapack__file110` | 58 | 38 | `faction_shadow`, `bgr_item_consumable`, `round_rect_5`, `faction_dark`, `faction_light_ia` |
| `PopupChatNew` | `002_UnityDataAssetPack_datapack__file110` | 57 | 26 | `button_chat`, `box_chat_me`, `bgr_chat`, `bg_chat2`, `tab_chat3` |
| `PopupLunarEvent` | `002_UnityDataAssetPack_datapack__file110` | 56 | 28 | `bgr_text_alpha`, `Image_question`, `button_yellow`, `circle_on`, `bgr_show_bonus` |
| `PopupFormation3` | `002_UnityDataAssetPack_datapack__file110` | 55 | 31 | `faction_abyss_ia`, `faction_abyss`, `round_rect_5`, `frame_item`, `faction_light_ia` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file079` | 54 | 33 | `button_history`, `leaf_tower`, `button_orange2`, `trunk_1`, `trunk_1_hard` |
| `PopupHeroBriefInfo` | `002_UnityDataAssetPack_datapack__file110` | 53 | 31 | `skill_default`, `notify_dot`, `progress_top_2`, `popup_body`, `mark_death` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file101` | 49 | 32 | `icon_lock_3`, `tag_name_island`, `decor_1`, `1`, `tag_icon_fk` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file001` | 48 | 24 | `rect_25`, `UISprite`, `box_login`, `board_title_2`, `round_rect_14` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file009` | 48 | 30 | `icon_power`, `tab_arena_2`, `pole_1`, `rank_podium`, `box_left` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file049` | 46 | 21 | `icon_blessing_wood`, `hero_star_1`, `floor_replace`, `button_info2`, `button_green` |
| `PopupFormation2` | `002_UnityDataAssetPack_datapack__file110` | 46 | 32 | `faction_abyss_ia`, `faction_abyss`, `round_rect_5`, `frame_item`, `faction_light_ia` |
| `PopupBossFk` | `002_UnityDataAssetPack_datapack__file110` | 45 | 17 | `hero_shadow`, `rect_19`, `bgr_boss_1`, `round_rect_7`, `popup_close` |
| `PopupBossFk2` | `002_UnityDataAssetPack_datapack__file110` | 45 | 17 | `boss_frame_1`, `mark_death`, `button_fk`, `popup_close_fk`, `boss_frame_overlay` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file083` | 43 | 30 | `button_fk_spin`, `icon_lock_2`, `tag_name_home`, `decor_1`, `1` |
| `PopupUpgradeHeroStone` | `002_UnityDataAssetPack_datapack__file110` | 43 | 19 | `popup_body`, `box_field_1`, `icon_gem`, `round_rect2`, `box_check` |
| `PopupGuildHall` | `002_UnityDataAssetPack_datapack__file110` | 42 | 31 | `button_help`, `guild_flag_3`, `notify_dot`, `Background`, `round_rect_7` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file039` | 41 | 28 | `icon_power`, `button_plus_2`, `trophy`, `box_left`, `pole_1` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file065` | 41 | 28 | `icon_power`, `button_plus_2`, `trophy`, `pole_1`, `icon_log` |
| `PopupPrayForFire` | `002_UnityDataAssetPack_datapack__file110` | 40 | 32 | `rect_25`, `pattern`, `frame_pray_boss2`, `f2`, `UISprite` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file073` | 39 | 22 | `box_field_1`, `world_bgr_3`, `tag_name_home`, `icon_gold`, `building_brave_trial` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file031` | 38 | 28 | `faction_forest_ia`, `faction_fortress_ia`, `round_rect2`, `Background`, `faction_abyss` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file075` | 38 | 28 | `button_shop_seal_land`, `button_add_home`, `Sealed-Land`, `button_help`, `icon_seal_challenge` |
| `PopupAllStarsFinal` | `002_UnityDataAssetPack_datapack__file110` | 38 | 17 | `button_close`, `round_rect_13`, `line_connect_dark`, `line_connect`, `mask_item` |
| `PopupIdolOfRevival` | `002_UnityDataAssetPack_datapack__file110` | 38 | 29 | `button_plus_2`, `popup_close`, `hero_head_hidden`, `tick_green`, `button_yellow` |
| `PopupMFSelectRelic` | `002_UnityDataAssetPack_datapack__file110` | 37 | 17 | `button_mf_supply`, `card_relic_back`, `icon_mage`, `card_relic_front`, `tag_legendary` |
| `PopupItemExchangeDetail` | `002_UnityDataAssetPack_datapack__file110` | 37 | 13 | `button_green`, `star_1`, `bgr_light`, `tick_green`, `1` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file051` | 34 | 19 | `button_yellow`, `bar_energy_bgr`, `title_summon_scroll`, `icon_gem`, `bgr_count_basic` |
| `PopupMFChooseSupport` | `002_UnityDataAssetPack_datapack__file110` | 34 | 26 | `hero_head_hidden`, `icon_magnify`, `icon_power`, `popup_body`, `button_yellow` |
| `PopupMakeBuilding` | `002_UnityDataAssetPack_datapack__file110` | 33 | 15 | `round_rect_1`, `round_rect_7`, `icon_stone_of_void`, `mine_gem`, `building_daffodil` |
| `PopupLuckySpinFK` | `002_UnityDataAssetPack_datapack__file110` | 33 | 19 | `stone_lock_2`, `head_1`, `mask_item`, `head_3`, `icon_chip` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file029` | 32 | 17 | `icon_monster_soul`, `frame_pet`, `button_yellow`, `bgr_box_2`, `button_green` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file005` | 31 | 18 | `tag_name_home`, `building_market`, `gi_03`, `icon_ally_fk`, `building_dungeon` |
| `PopupUpgradeHeroFake` | `002_UnityDataAssetPack_datapack__file110` | 30 | 17 | `231`, `skill_191`, `box_field_1`, `icon_hp`, `skill_187` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file015` | 28 | 15 | `bgr_arena`, `arena_hanger_shop`, `notify_dot`, `tag_title_2`, `thanh_chien` |
| `PopupAllStarBet` | `002_UnityDataAssetPack_datapack__file110` | 28 | 15 | `icon_guild_coin`, `button_yellow`, `khung_chan_le`, `Rounded_Rectangle_0`, `box_text_1` |
| `PopupBattleEnd` | `002_UnityDataAssetPack_datapack__file110` | 28 | 18 | `popup`, `home2_button_bgr`, `tag_title_2`, `button_info2`, `button_yellow` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file023` | 27 | 17 | `Background`, `round_rect2`, `tab_equipment_2`, `tab_shard`, `UIMask` |
| `MFHeroHead` | `002_UnityDataAssetPack_datapack__file110` | 27 | 20 | `bar_bgr`, `faction_light`, `hero_star_2`, `hero_5_star`, `hero_head_border` |
| `PopupStoreAndBenefit` | `002_UnityDataAssetPack_datapack__file110` | 26 | 19 | `bgr_label_0`, `round_rect_7`, `icon_gem`, `home_button_vip`, `arrow_left` |
| `PopupGuildWarLog` | `002_UnityDataAssetPack_datapack__file110` | 26 | 15 | `rect_18`, `round_rect_8`, `guild_flag_9`, `round_rect_7`, `button_play` |
| `PopupHeroesFk2` | `002_UnityDataAssetPack_datapack__file110` | 26 | 22 | `icon_speed`, `box_gem`, `lock_fk`, `button_fk`, `frame_head_fk` |
| `PopupShopFakeEn` | `002_UnityDataAssetPack_datapack__file110` | 26 | 15 | `popup_close`, `gem_1_en`, `gem_3_en`, `gem_2_en`, `round_rect_7` |
| `PopupShopFk` | `002_UnityDataAssetPack_datapack__file110` | 26 | 12 | `popup_close`, `icon_4`, `round_rect_7`, `icon_11`, `icon_8` |
| `PopupGuildRaid` | `002_UnityDataAssetPack_datapack__file110` | 25 | 22 | `button_help`, `popup_close`, `popup_body`, `rect_14`, `arrow_right` |
| `HeroHeadDragDrop` | `002_UnityDataAssetPack_datapack__file110` | 25 | 18 | `hero_head_hidden`, `star_e_bgr`, `hero_star_e`, `faction_light`, `hero_star_2` |
| `GGBrokenSpaceEventV2` | `002_UnityDataAssetPack_datapack__file110` | 25 | 8 | `icon_bs_locked`, `line_bs_inactive`, `icon_bs_won`, `button_yellow`, `button_help` |
| `HeroHeadUI` | `002_UnityDataAssetPack_datapack__file110` | 25 | 18 | `hero_5_star`, `hero_head_border`, `bgr_item_consumable`, `hero_star_1`, `frame_item` |
| `HeroHeadClickable` | `002_UnityDataAssetPack_datapack__file110` | 25 | 18 | `hero_star_2`, `faction_light`, `hero_star_3`, `hero_star_1`, `tick_green` |
| `HeroMaterialReturn` | `002_UnityDataAssetPack_datapack__file110` | 25 | 18 | `hero_head_hidden`, `hero_star_e`, `hero_star_2`, `faction_light`, `notify_dot` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file095` | 24 | 14 | `field_res_home`, `tab_active`, `button_back`, `tab_inactive`, `btn_Action` |
| `PopupGuildBossAttack` | `002_UnityDataAssetPack_datapack__file110` | 24 | 23 | `button_info2`, `bar_hp_boss`, `popup_close`, `bgr_boss_1`, `progress_bgr` |
| `PopupMailBox` | `002_UnityDataAssetPack_datapack__file110` | 24 | 17 | `UIMask`, `trash_bin`, `button_green`, `popup_close`, `Background` |
| `PanelPrepareScene` | `002_UnityDataAssetPack_datapack__file110` | 24 | 24 | `loading_bgr`, `loading_fgr`, `rect_event_raid`, `faction_forest`, `arrow_restrain_2` |
| `PopupEquipActionUpgradeOrReup` | `002_UnityDataAssetPack_datapack__file110` | 24 | 11 | `item_star`, `hero_star_2`, `hero_star_3`, `bg_12`, `E_45` |
| `PopupShopFk2` | `002_UnityDataAssetPack_datapack__file110` | 24 | 14 | `popup_close_fk`, `gem_2`, `gem_1`, `rect_shop`, `gold_1` |
| `PopupAllStarsTop8` | `002_UnityDataAssetPack_datapack__file110` | 23 | 18 | `button_close`, `round_rect_13`, `rect_16`, `clan_vs`, `mask_item` |
| `PopupInformationAuraNew` | `002_UnityDataAssetPack_datapack__file110` | 23 | 12 | `popup_close`, `faction_aura`, `faction_abyss`, `rect_help`, `title_help` |
| `PopupHeroesFk` | `002_UnityDataAssetPack_datapack__file110` | 23 | 21 | `icon_speed`, `stone_lock_1`, `button_yellow`, `hero_head_border`, `bgr_avatar` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file019` | 22 | 12 | `field_res_home`, `tab_active`, `button_back`, `tab_inactive`, `btn_Action` |
| `WarMapPart0` | `002_UnityDataAssetPack_datapack__file110` | 22 | 6 | `notify_dot_3`, `war_map_a_1`, `war_map_e_1`, `notify_dot_2`, `decor_2` |
| `PopupCreateGuild` | `002_UnityDataAssetPack_datapack__file110` | 22 | 16 | `tab_active`, `tab_inactive`, `Background`, `rect_25`, `guild_flag_4` |
| `PopupUpgradeDaffodil` | `002_UnityDataAssetPack_datapack__file110` | 22 | 17 | `popup_close`, `icon_purple_daffodil`, `button_yellow`, `icon_gold`, `arrow_next` |
| `PopupUpgradeMine` | `002_UnityDataAssetPack_datapack__file110` | 22 | 17 | `progress_buff_bgr`, `round_rect2`, `progress_top_2`, `box_text_1`, `button_green` |
| `PopupPetPreview` | `002_UnityDataAssetPack_datapack__file110` | 22 | 8 | `skill_default`, `tag_number`, `round_rect_8`, `skill_selected`, `popup_body` |
| `WarMapPart1` | `002_UnityDataAssetPack_datapack__file110` | 22 | 6 | `war_map_a_1`, `war_map_e_1`, `notify_dot`, `notify_dot_3`, `decor_2` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file021` | 21 | 13 | `b_mill`, `b_member`, `b_boss`, `notify_dot`, `button_shop_guild` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file067` | 21 | 17 | `icon_spirit`, `notify_dot`, `icon_gold`, `icon_exp`, `button_yellow` |
| `PopupSmashedMerchants` | `002_UnityDataAssetPack_datapack__file110` | 21 | 12 | `frame_merchant`, `popup_close`, `round_rect2`, `img_merchant_3`, `img_merchant_1` |
| `PopupRewardShelterMissionEvent` | `002_UnityDataAssetPack_datapack__file110` | 21 | 14 | `mask_item`, `hero_star_2`, `icon_add`, `button_yellow`, `popup_close` |
| `PopupSelectServer` | `002_UnityDataAssetPack_datapack__file110` | 21 | 13 | `border_avatar`, `round_rect2`, `tag_level`, `bgr_avatar`, `UIMask` |
| `PopupSummonShard` | `002_UnityDataAssetPack_datapack__file110` | 21 | 15 | `popup_close`, `popup_body`, `button_yellow`, `InputFieldBackground`, `box_field_1` |
| `PopupAllStarBetHistory` | `002_UnityDataAssetPack_datapack__file110` | 21 | 13 | `rect_15`, `bgr_Bet`, `rect_24`, `button_close`, `round_rect_8` |
| `PopupSelectBattleCase` | `002_UnityDataAssetPack_datapack__file110` | 20 | 10 | `button_back`, `card_xx`, `bgr_back`, `tag_title_2`, `tag_name` |
| `PopupHandOfMidas` | `002_UnityDataAssetPack_datapack__file110` | 20 | 10 | `button_yellow`, `title_bar_2`, `icon_gem`, `stack_gold_3`, `popup_close` |
| `HeroMaterialInput` | `002_UnityDataAssetPack_datapack__file110` | 20 | 12 | `hero_star_2`, `mask_item`, `hero_star_1`, `bgr_item_consumable`, `faction_forest` |
| `HealthBar` | `002_UnityDataAssetPack_datapack__file110` | 20 | 9 | `bar_hp`, `bar_hp_bgr`, `bar_rage_2`, `bar_rage_3`, `faction_forest` |
| `PopupCounter` | `002_UnityDataAssetPack_datapack__file110` | 20 | 19 | `rect_event_raid`, `popup_close`, `line_sm`, `arrow_restrain_4`, `faction_abyss` |
| `WarMapPart2` | `002_UnityDataAssetPack_datapack__file110` | 20 | 4 | `war_map_a_2`, `war_map_e_2`, `notify_dot_3`, `notify_dot` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file093` | 19 | 7 | `button_shop_sabaody`, `tag_title_2`, `card_brave_trial`, `tag_name`, `bgr_sabaody` |
| `PopupHeroUpTier` | `002_UnityDataAssetPack_datapack__file110` | 19 | 11 | `arrow_2`, `round_rect_5`, `popup_body`, `icon_gold`, `icon_promotion_stone` |
| `SlotHeroQuest` | `002_UnityDataAssetPack_datapack__file110` | 19 | 12 | `UISprite`, `hero_star_3`, `slot_altar`, `mask_item`, `tick_green` |
| `PopupArenaReward` | `002_UnityDataAssetPack_datapack__file110` | 19 | 14 | `UIMask`, `round_rect_3`, `rect_13`, `tab_event_on`, `tab_event_off` |
| `HeroMaterial` | `002_UnityDataAssetPack_datapack__file110` | 19 | 11 | `hero_star_2`, `mask_item`, `hero_star_1`, `bgr_item_consumable`, `faction_forest` |
| `PopupAltarsOfTrial` | `002_UnityDataAssetPack_datapack__file110` | 18 | 12 | `rect_event_raid`, `icon_brave_badge`, `popup_body`, `healing_of_trial`, `popup_close` |
| `PopupAllStarsReward` | `002_UnityDataAssetPack_datapack__file110` | 18 | 10 | `UIMask`, `rect_13`, `rect_12`, `rect_18`, `tag_rank_1` |
| `PopupUpgradeBuilding` | `002_UnityDataAssetPack_datapack__file110` | 18 | 13 | `button_yellow`, `popup_close`, `icon_gold`, `arrow_next`, `box_text_1` |
| `PopupHeroUpgradeFk2` | `002_UnityDataAssetPack_datapack__file110` | 18 | 14 | `box_gem`, `rect`, `frame_head_fk`, `hero_head_border`, `bgr_avatar` |
| `PopupHeroUpgradeFk` | `002_UnityDataAssetPack_datapack__file110` | 18 | 14 | `box_field_1`, `round_rect_4`, `hero_head_border`, `round_rect_18`, `bgr_avatar` |
| `PanelEventRollCall` | `002_UnityDataAssetPack_datapack__file110` | 18 | 17 | `button_help`, `decor_rollcall_1`, `bgr_text_alpha`, `round_rect_12`, `decor_rollcall_3` |
| `PopupCrosswordPuzzles` | `002_UnityDataAssetPack_datapack__file110` | 18 | 9 | `btn_24`, `x1`, `tick_green_3`, `bg_24`, `char_dv` |
| `PanelAccumulatedEvent` | `002_UnityDataAssetPack_datapack__file110` | 17 | 11 | `round_rect_content`, `round_rect_7`, `progress_top_2`, `progress_bgr`, `button_green` |
| `PanelHeroList` | `002_UnityDataAssetPack_datapack__file110` | 17 | 17 | `faction_forest_ia`, `faction_shadow_ia`, `UIMask`, `rect_13`, `faction_light` |
| `PopupArenaRival` | `002_UnityDataAssetPack_datapack__file110` | 17 | 16 | `Background`, `round_rect_content`, `button_green`, `round_rect2`, `cell_arena_3` |
| `PopupChangeAvatar` | `002_UnityDataAssetPack_datapack__file110` | 17 | 14 | `round_rect2`, `bgr_item_consumable`, `button_yellow`, `mask_item`, `tick_green` |
| `PopupIdolOfBlessing` | `002_UnityDataAssetPack_datapack__file110` | 17 | 11 | `rage`, `storm`, `round_rect_3`, `rock`, `icon_brave_badge` |
| `CanvasTopMost` | `002_UnityDataAssetPack_datapack__file097` | 16 | 8 | `button_close_3`, `ico_gold`, `box_hero_list`, `title_arena`, `bar_exp` |
| `PopupAllStarsResult` | `002_UnityDataAssetPack_datapack__file110` | 16 | 15 | `Background`, `UIMask`, `button_close`, `round_rect_13`, `rect_final_4_4` |
| `PopupSummonHeroes` | `002_UnityDataAssetPack_datapack__file110` | 16 | 8 | `hero_star_1`, `hero_star_2`, `board_hero_name`, `hero_star_3`, `hero_star_e` |
| `PopupLineupFk2` | `002_UnityDataAssetPack_datapack__file110` | 16 | 10 | `slot_team`, `hero_shadow`, `button_yellow`, `btnBack`, `roun_rect6` |
| `HeroHome2` | `002_UnityDataAssetPack_datapack__file110` | 16 | 9 | `hero_shadow`, `star_e_bgr`, `hero_star_e`, `faction_abyss`, `progress_buff_bgr` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file089` | 15 | 10 | `weapon_2`, `icon_skill`, `itemuse_1`, `Joystick1`, `chakra1` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file097` | 15 | 10 | `weapon_2`, `icon_skill`, `itemuse_1`, `Joystick1`, `chakra1` |
| `PopupBossIsland` | `002_UnityDataAssetPack_datapack__file110` | 15 | 12 | `box_field_1`, `popup_body`, `button_green`, `popup_close`, `round_rect_7` |
| `GGShelterMissionEvent` | `002_UnityDataAssetPack_datapack__file110` | 15 | 8 | `button_yellow`, `title_bar_2`, `arrow_right`, `bar_event_4`, `button_gray` |
| `PopupArenaBattleRecord` | `002_UnityDataAssetPack_datapack__file110` | 15 | 15 | `Background`, `round_rect2`, `cell_arena_3`, `button_yellow`, `UIMask` |
| `PopupLineupFk` | `002_UnityDataAssetPack_datapack__file110` | 15 | 9 | `replace_estrade`, `hero_shadow`, `button_yellow`, `popup_close`, `roun_rect6` |
| `PopupMFPlayerRelics` | `002_UnityDataAssetPack_datapack__file110` | 15 | 15 | `tag_name_relic`, `222`, `faction_fortress`, `popup_body`, `frame_affect_relic` |
| `GGEquipmentSlot` | `002_UnityDataAssetPack_datapack__file110` | 15 | 9 | `frame_item`, `item_star`, `border_equipment_2`, `item_placeholder`, `tick_green` |
| `PopupHeroSkins` | `002_UnityDataAssetPack_datapack__file110` | 15 | 13 | `Background`, `mask_item`, `tab_point`, `popup_close`, `frame_hero_thumb` |
| `PopupTrialChest` | `002_UnityDataAssetPack_datapack__file110` | 14 | 8 | `shader_chest`, `button_yellow`, `round_rect_7`, `popup_close`, `popup_body` |
| `PopupBattleEndFakeEn` | `002_UnityDataAssetPack_datapack__file110` | 14 | 11 | `popup`, `tag_title_2`, `button_yellow`, `bgr_item_consumable`, `tick_green` |
| `ItemShardUI2` | `002_UnityDataAssetPack_datapack__file110` | 14 | 10 | `bgr_light`, `progress_top_2`, `hero_star_1`, `tick_green`, `icon_shard` |
| `PopupHeroStoneInfo` | `002_UnityDataAssetPack_datapack__file110` | 14 | 8 | `button_yellow`, `round_rect_8`, `popup_close`, `item_star`, `mask_item` |
| `ItemArtifactUI` | `002_UnityDataAssetPack_datapack__file110` | 14 | 9 | `item_star`, `tick_green`, `item_transparent`, `bgr_equipment_2`, `mask_item` |
| `ItemShardUI` | `002_UnityDataAssetPack_datapack__file110` | 14 | 10 | `hero_star_1`, `UISprite`, `border_item`, `bgr_item_consumable`, `item_transparent` |
| `PanelExchangeGemEvent` | `002_UnityDataAssetPack_datapack__file110` | 14 | 10 | `icon_gem`, `button_yellow`, `bgr_toast`, `round_rect_7`, `bgr_hero_info` |
| `PopupGuildApplication` | `002_UnityDataAssetPack_datapack__file110` | 14 | 13 | `Background`, `UIMask`, `button_red`, `popup_body`, `popup_close` |
| `PanelGrowthFundEvent` | `002_UnityDataAssetPack_datapack__file110` | 14 | 11 | `bgr_hero_info`, `button_green`, `button_yellow`, `button_gift`, `button_help` |
| `PopupShopChest` | `002_UnityDataAssetPack_datapack__file110` | 14 | 12 | `chest_level_1`, `UIMask`, `popup_body`, `icon_guild_coin`, `box_text_1` |
| `CanvasTopMost` | `002_UnityDataAssetPack_datapack__file089` | 13 | 8 | `button_close_3`, `ico_gold`, `button_back`, `bgr_win_lose`, `title_arena` |
| `PanelPackValueEvent` | `002_UnityDataAssetPack_datapack__file110` | 13 | 10 | `button_yellow`, `round_rect_7`, `button_green`, `round_rect_content`, `bgr_toast` |
| `ItemEquipmentUI` | `002_UnityDataAssetPack_datapack__file110` | 13 | 8 | `tick_green`, `item_star`, `mask_item`, `frame_item`, `bgr_equipment_1` |
| `PopupGuildManageMember` | `002_UnityDataAssetPack_datapack__file110` | 13 | 13 | `Background`, `UIMask`, `button_green`, `popup_close`, `round_rect_7` |
| `PopupEventRaid` | `002_UnityDataAssetPack_datapack__file110` | 13 | 9 | `button_raid_spirit`, `button_raid_gold`, `icon_question`, `button_raid_shard`, `button_yellow` |
| `PopupMFShowRelic` | `002_UnityDataAssetPack_datapack__file110` | 13 | 13 | `card_relic_back`, `UIMask`, `icon_mage`, `Background`, `tag_legendary` |
| `PanelEventFeast` | `002_UnityDataAssetPack_datapack__file110` | 13 | 10 | `button_help`, `round_rect_7`, `button_yellow`, `round_rect2`, `UIMask` |
| `PopupTowerHistory` | `002_UnityDataAssetPack_datapack__file110` | 12 | 12 | `round_rect_7`, `popup_body`, `popup_close`, `round_rect2`, `UIMask` |
| `PopupTopPlayer` | `002_UnityDataAssetPack_datapack__file110` | 12 | 12 | `tag_rank_1`, `round_rect_7`, `popup_body`, `UIMask`, `round_rect2` |
| `PopupEnemyInfo` | `002_UnityDataAssetPack_datapack__file110` | 12 | 10 | `round_rect_content`, `button_green`, `icon_stamina`, `box_field_1`, `button_yellow` |
| `HeroReplace` | `002_UnityDataAssetPack_datapack__file110` | 12 | 5 | `hero_star_2`, `faction_abyss`, `hero_star_1`, `question_mark`, `hero_star_3` |
| `PanelDiscountItemEvent` | `002_UnityDataAssetPack_datapack__file110` | 12 | 11 | `bgr_hero_info`, `round_rect_7`, `rect_16`, `bgr_discount`, `icon_gem` |
| `PopupGuildWarHistory` | `002_UnityDataAssetPack_datapack__file110` | 12 | 10 | `rect_18`, `popup_body`, `guild_flag_9`, `round_rect_7`, `popup_close` |
| `PanelFightBossEvent` | `002_UnityDataAssetPack_datapack__file110` | 12 | 12 | `1`, `event_3`, `progress_bgr`, `button_green`, `progress_buff_2` |
| `PanelGrowthFundEventV2` | `002_UnityDataAssetPack_datapack__file110` | 12 | 10 | `bgr_hero_info`, `button_green`, `button_gift`, `button_help`, `button_yellow` |
| `PanelBrokenSpacesEvent2` | `002_UnityDataAssetPack_datapack__file110` | 12 | 6 | `light_line`, `button_yellow`, `button_help`, `button_add`, `20` |
| `GGGrowthFundEventV2` | `002_UnityDataAssetPack_datapack__file110` | 12 | 6 | `button_green`, `button_yellow`, `banner_placehoder`, `progress_top_2`, `progress_bgr` |
| `PopupIdolOfHealing` | `002_UnityDataAssetPack_datapack__file110` | 12 | 11 | `button_plus_2`, `popup_close`, `icon_brave_badge`, `icon_brave_coin`, `button_yellow` |
| `PopupChangeHeroSkin` | `002_UnityDataAssetPack_datapack__file110` | 12 | 11 | `button_green`, `arrow_right`, `head_1`, `popup_close`, `hero_shadow` |
| `PopupInfoStatue` | `002_UnityDataAssetPack_datapack__file110` | 12 | 6 | `tab_off`, `popup_close 1`, `tab_on`, `round_rect2`, `UISprite` |
| `HeroStars` | `002_UnityDataAssetPack_datapack__file110` | 12 | 5 | `star_e_bgr`, `hero_star_e`, `hero_star_2`, `hero_star_1`, `hero_star_3` |
| `HeadCampaign` | `002_UnityDataAssetPack_datapack__file110` | 12 | 8 | `star_hl`, `star_grey`, `icon_battle`, `head_shadow`, `mark_death` |
| `CanvasBattle` | `002_UnityDataAssetPack_datapack__file089` | 11 | 4 | `bar_hp`, `bar_hp_bgr`, `chakra1`, `chakra` |
| `CanvasBattle` | `002_UnityDataAssetPack_datapack__file097` | 11 | 4 | `bar_hp`, `bar_hp_bgr`, `chakra1`, `chakra` |
| `PopupShowAllArtifact` | `002_UnityDataAssetPack_datapack__file110` | 11 | 11 | `button_yellow`, `button_green`, `button_violet`, `button_blue`, `button_red` |
| `PanelBrokenSpacesEvent` | `002_UnityDataAssetPack_datapack__file110` | 11 | 9 | `button_yellow`, `event_13`, `button_add`, `bgr_hero_info`, `event_14` |
| `PopupInvitationList` | `002_UnityDataAssetPack_datapack__file110` | 11 | 10 | `button_yellow`, `popup_close`, `round_rect_7`, `popup_body`, `bgr_lv` |
| `PopupUseItemConsume` | `002_UnityDataAssetPack_datapack__file110` | 11 | 9 | `popup_close`, `popup_body`, `button_yellow`, `box_field_1`, `item_transparent` |
| `DiceTile` | `002_UnityDataAssetPack_datapack__file110` | 11 | 10 | `dice_trigger_fruit`, `dice_trigger_tarot`, `dice_trigger_more_dice_1`, `dice_map_tile`, `dice_trigger_more_dice_2` |
| `PopupQuestFK` | `002_UnityDataAssetPack_datapack__file110` | 11 | 11 | `progg_empty_2`, `popup_close`, `bgr_item_consumable`, `tick_green`, `icon_gold` |
| `PopupMiLi` | `002_UnityDataAssetPack_datapack__file110` | 10 | 7 | `icon_tech_hp`, `round_rect_7`, `button_yellow`, `box_text_1`, `popup_body` |
| `PopupAtkBossScout` | `002_UnityDataAssetPack_datapack__file110` | 10 | 10 | `round_rect_18`, `icon_stamina`, `Knob`, `button_green`, `box_field_1` |
| `PopupBuyDaffodil` | `002_UnityDataAssetPack_datapack__file110` | 10 | 9 | `icon_gem`, `round_rect_3`, `button_yellow`, `icon_purple_daffodil`, `popup_body` |
| `PopupCampaignInfo` | `002_UnityDataAssetPack_datapack__file110` | 10 | 10 | `icon_gold`, `UIMask`, `round_rect_content`, `Background`, `icon_spirit` |
| `HeroStoneUI` | `002_UnityDataAssetPack_datapack__file110` | 10 | 5 | `item_star`, `mask_item`, `hero_stone_1`, `border_equipment_2`, `frame_item` |
| `PopupCampaignReward` | `002_UnityDataAssetPack_datapack__file110` | 10 | 10 | `UIMask`, `Background`, `round_rect2`, `popup_body`, `button_green` |
| `PopupBraveReward` | `002_UnityDataAssetPack_datapack__file110` | 10 | 6 | `round_rect2`, `popup_body`, `arrow_up`, `progress_top_2`, `progress_bgr` |
| `PopupSelectPet` | `002_UnityDataAssetPack_datapack__file110` | 10 | 10 | `round_rect2`, `popup_close`, `popup_body`, `UIMask`, `frame_pet` |
| `PopupSelectHero` | `002_UnityDataAssetPack_datapack__file110` | 10 | 9 | `button_yellow`, `home2_button_bgr`, `UIMask`, `round_rect2`, `popup_close` |
| `GGFightBossEvent` | `002_UnityDataAssetPack_datapack__file110` | 10 | 10 | `event_3`, `progress_bgr`, `button_green`, `progress_buff_2`, `board_title_1` |
| `PopupGuildPromote` | `002_UnityDataAssetPack_datapack__file110` | 10 | 9 | `line_decor`, `button_yellow`, `round_rect_8`, `button_close`, `mask_item` |
| `CardRelic` | `002_UnityDataAssetPack_datapack__file110` | 10 | 10 | `bgr_affect_relic`, `round_mask_2`, `72`, `tag_legendary`, `icon_mage` |
| `PopupSpecialEvent2` | `002_UnityDataAssetPack_datapack__file110` | 10 | 9 | `rect_25`, `UIMask`, `Background`, `tab_event_inactive`, `tab_event_active` |
| `PopupRankingRewards` | `002_UnityDataAssetPack_datapack__file110` | 10 | 7 | `Background`, `tag_rank_1`, `round_rect_8`, `button_close`, `line_decor` |
| `PopupShareBattle` | `002_UnityDataAssetPack_datapack__file110` | 10 | 10 | `bgr_dropbox`, `popup_close`, `popup_body`, `button_yellow`, `InputFieldBackground` |
| `PopupNotifyNew` | `002_UnityDataAssetPack_datapack__file110` | 10 | 8 | `popup_close`, `rect`, `pipe`, `UIMask`, `Background` |
| `TapPopupFeedback` | `002_UnityDataAssetPack_datapack__file110` | 10 | 6 | `btn_Action`, `rect_help`, `tap_close`, `InputFieldBackground`, `rect_14` |
| `CanvasBattle` | `002_UnityDataAssetPack_datapack__file017` | 9 | 7 | `bar_hp`, `bar_hp_bgr`, `bar_rage_2`, `bar_rage_3`, `faction_forest` |
| `PopupGuildWarReward` | `002_UnityDataAssetPack_datapack__file110` | 9 | 7 | `button_close`, `round_rect_8`, `line_decor`, `UIMask`, `Background` |
| `PopupSmash` | `002_UnityDataAssetPack_datapack__file110` | 9 | 7 | `icon_gem`, `button_yellow`, `popup_close`, `popup_body`, `round_rect_3` |
| `PopupSmashBoss` | `002_UnityDataAssetPack_datapack__file110` | 9 | 7 | `icon_gem`, `button_yellow`, `popup_close`, `popup_body`, `round_rect_3` |
| `BoxGameEventInfo` | `002_UnityDataAssetPack_datapack__file110` | 9 | 6 | `1`, `button_gift`, `event_than_tuong`, `box_gold_event`, `button_help` |
| `PopupCampaignBattleResultFk` | `002_UnityDataAssetPack_datapack__file110` | 9 | 5 | `star_slot`, `star_hl`, `button_yellow`, `rect_20`, `fk_win` |
| `PanelEvent2Package` | `002_UnityDataAssetPack_datapack__file110` | 9 | 6 | `bgr_text_alpha`, `banner_package_2`, `banner_package_1`, `bgr_event_roll_call`, `button_yellow` |
| `PanelShelterMissionEvent` | `002_UnityDataAssetPack_datapack__file110` | 8 | 7 | `button_yellow`, `progress_1`, `1`, `box_text_1`, `arrow_left` |
| `WarTent` | `002_UnityDataAssetPack_datapack__file110` | 8 | 4 | `war_star`, `war_star_slot`, `ship_member_1_1`, `base_foot` |
| `PopupListRaidChallenge` | `002_UnityDataAssetPack_datapack__file110` | 8 | 8 | `box_text_1`, `popup_body`, `popup_close`, `button_add`, `UIMask` |
| `WarStronghold` | `002_UnityDataAssetPack_datapack__file110` | 8 | 4 | `war_star`, `ship_leader_1`, `war_star_slot`, `base_foot` |
| `WarFortress` | `002_UnityDataAssetPack_datapack__file110` | 8 | 4 | `war_star`, `war_star_slot`, `ship_coleader_1`, `base_foot` |
| `PopupEditGuild` | `002_UnityDataAssetPack_datapack__file110` | 8 | 6 | `round_rect_3`, `guild_flag_4`, `round_rect2`, `button_yellow`, `popup_body` |
| `TapPopupSettings` | `002_UnityDataAssetPack_datapack__file110` | 8 | 5 | `button_music_on`, `rect_help`, `button_sound_on`, `tap_close` |
| `Canvas` | `000_com.mobi389.murom_data__file004` | 7 | 7 | `logo_4_legends`, `bgr_splash2`, `logo_global`, `ace_logo`, `ace_title` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file091` | 7 | 7 | `UISprite`, `board_title_1`, `button_mf_revine`, `button_mf_focus`, `button_back` |
| `PopupMFPlayerHeroes` | `002_UnityDataAssetPack_datapack__file110` | 7 | 7 | `box_field_1`, `popup_body`, `button_green`, `UIMask`, `Background` |
| `IslandUp` | `002_UnityDataAssetPack_datapack__file110` | 7 | 4 | `dot_path`, `island_locked`, `island`, `island_loot` |
| `PopupSelectMaterial` | `002_UnityDataAssetPack_datapack__file110` | 7 | 6 | `round_rect2`, `popup_close`, `button_green`, `Background`, `UIMask` |
| `IslandDown` | `002_UnityDataAssetPack_datapack__file110` | 7 | 4 | `island_locked`, `dot_path`, `island`, `island_loot` |
| `Panel7DaysLoginEvent` | `002_UnityDataAssetPack_datapack__file110` | 7 | 7 | `round_rect_7`, `1`, `button_green`, `box_text_1`, `button_yellow` |
| `GGBannerEvent` | `002_UnityDataAssetPack_datapack__file110` | 7 | 6 | `1`, `banner_placehoder`, `box_gold_event`, `button_yellow`, `button_gift` |
| `Tab_Event` | `002_UnityDataAssetPack_datapack__file110` | 7 | 7 | `bgr_text_3`, `icon_1`, `circle_off`, `tab_event_off`, `circle_on` |
| `PopupRename` | `002_UnityDataAssetPack_datapack__file110` | 7 | 6 | `button_green`, `round_rect_3`, `popup_body`, `icon_gem`, `popup_close` |
| `PopupShowBonusEffect` | `002_UnityDataAssetPack_datapack__file110` | 7 | 6 | `bgr_show_bonus`, `UIMask`, `line_sm`, `button_green`, `button_yellow` |
| `PanelEventHarvestDay` | `002_UnityDataAssetPack_datapack__file110` | 7 | 7 | `round_rect_7`, `round_rect2`, `UIMask`, `progress_top_2`, `progress_bgr` |
| `PanelEvent2PackVertical` | `002_UnityDataAssetPack_datapack__file110` | 7 | 5 | `banner_package_2`, `banner_package_1`, `bgr_event_roll_call`, `button_yellow`, `icon_vip` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file087` | 6 | 6 | `icon_gold`, `bgr_joystick`, `knob_joystick`, `button_punch`, `box_field_1` |
| `PopupChooseSpin` | `002_UnityDataAssetPack_datapack__file110` | 6 | 5 | `line_sm`, `casino_normal`, `button_close`, `casino_vip`, `bgr_show_bonus` |
| `PopupLevelUp` | `002_UnityDataAssetPack_datapack__file110` | 6 | 5 | `box_text_1`, `button_yellow`, `line_sm`, `round_rect_5`, `bgr_show_bonus` |
| `BraveTrialStage` | `002_UnityDataAssetPack_datapack__file110` | 6 | 6 | `base_brave_head`, `hero_head_bgr`, `round_mask`, `112`, `frame_brave_head` |
| `PopupBuyConfirm` | `002_UnityDataAssetPack_datapack__file110` | 6 | 6 | `UIMask`, `button_green`, `Background`, `popup_body`, `popup_close` |
| `PopupConfirmClearAsset` | `002_UnityDataAssetPack_datapack__file110` | 6 | 6 | `UIMask`, `Background`, `popup_body`, `button_yellow`, `UISprite` |
| `PanelTopRacingEvent` | `002_UnityDataAssetPack_datapack__file110` | 6 | 6 | `box_text_1`, `1`, `bgr_hero_info`, `button_yellow`, `Layer-65` |
| `PetHeadUI` | `002_UnityDataAssetPack_datapack__file110` | 6 | 6 | `hero_head_border`, `pet_head`, `frame_item`, `bgr_item_consumable`, `mask_item` |
| `PopupAGI` | `002_UnityDataAssetPack_datapack__file110` | 6 | 6 | `popup_close`, `popup_body`, `round_rect_7`, `box_text_1`, `button_yellow` |
| `PopupChangePassword` | `002_UnityDataAssetPack_datapack__file110` | 6 | 4 | `button_green`, `round_rect_3`, `popup_close`, `popup_body` |
| `PopupSelectHeroFk` | `002_UnityDataAssetPack_datapack__file110` | 6 | 6 | `button_yellow`, `UIMask`, `round_rect2`, `popup_close`, `popup_body` |
| `PopupGameChallenge` | `002_UnityDataAssetPack_datapack__file110` | 6 | 6 | `popup_body`, `popup_close`, `round_rect2`, `UIMask`, `Background` |
| `FactionRestrain` | `002_UnityDataAssetPack_datapack__file110` | 6 | 6 | `faction_abyss`, `faction_fortress`, `faction_light`, `faction_forest`, `faction_shadow` |
| `PopupGuildRename` | `002_UnityDataAssetPack_datapack__file110` | 6 | 5 | `button_green`, `round_rect_3`, `popup_body`, `icon_gem`, `popup_close` |
| `PopupChangeLanguage` | `002_UnityDataAssetPack_datapack__file110` | 6 | 6 | `popup_close`, `rect_help`, `flag_english`, `tick_green_3`, `btn_24` |
| `DummyHeroHead` | `002_UnityDataAssetPack_datapack__file110` | 6 | 5 | `stone_lock_1`, `hero_head_border`, `mask_item`, `tick_green_2`, `123` |
| `PopupShowSkillMessage` | `002_UnityDataAssetPack_datapack__file110` | 6 | 4 | `right_bgr`, `left_bgr`, `title_bgr`, `message_border` |
| `PopupEasterPrayRewards` | `002_UnityDataAssetPack_datapack__file110` | 6 | 5 | `tab_inactive`, `popup_close`, `round_rect_content`, `popup_body`, `tab_active` |
| `PopupHeroSkinInfo` | `002_UnityDataAssetPack_datapack__file110` | 6 | 6 | `head_1`, `popup_close`, `hero_shadow`, `mask_item`, `round_rect_13` |
| `PopupFirstPurchase` | `002_UnityDataAssetPack_datapack__file110` | 6 | 4 | `Fame_text`, `button_close_3`, `popup_first_pc`, `button_benefit` |
| `BoxAllStarsPlayer` | `002_UnityDataAssetPack_datapack__file110` | 6 | 6 | `round_rect_14`, `mask_item`, `bgr_avatar`, `tag_level`, `border_avatar` |
| `BoxAllStarsPlayerBig` | `002_UnityDataAssetPack_datapack__file110` | 6 | 6 | `round_rect_14`, `mask_item`, `bgr_avatar`, `tag_level`, `border_avatar` |
| `TapHero` | `002_UnityDataAssetPack_datapack__file110` | 6 | 3 | `progress_hp`, `progress_buff_bgr`, `hero_shadow` |
| `BraveTrialStageNew` | `002_UnityDataAssetPack_datapack__file110` | 6 | 6 | `mark_death`, `frame_brave_head`, `112`, `round_mask`, `hero_head_bgr` |
| `GGPackValueEvent` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `round_rect_3`, `button_green`, `bgr_toast`, `UIMask`, `Background` |
| `PopupDungeonSmashItem` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `UIMask`, `Background`, `popup_body`, `popup_close`, `round_rect_content` |
| `PopupMessage` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `UIMask`, `button_green`, `Background`, `popup_body`, `button_yellow` |
| `PopupUnlockHeroConfirm` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `UIMask`, `button_red`, `Background`, `popup_body`, `popup_close` |
| `PopupSelectEquipment` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `round_rect2`, `UIMask`, `popup_close`, `popup_body`, `Background` |
| `PopupTextInput` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `InputFieldBackground`, `popup_close`, `popup_body`, `button_yellow`, `round_rect_17` |
| `PopupShowRewards` | `002_UnityDataAssetPack_datapack__file110` | 5 | 4 | `bgr_text_3`, `popup_close`, `icon_add`, `popup_body` |
| `PopupShowBonus` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `UIMask`, `round_rect_content`, `popup_body`, `popup_close`, `Background` |
| `PopupBattleInfo` | `002_UnityDataAssetPack_datapack__file110` | 5 | 4 | `UIMask`, `bgr_box_2`, `Background`, `button_close` |
| `ItemConsumableUI` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `border_item`, `bgr_light`, `item_transparent`, `tick_green`, `bgr_item_consumable` |
| `PopupSelectArtifact` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `UIMask`, `popup_close`, `popup_body`, `Background`, `round_rect2` |
| `ButtonHeadlineEvent` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `bgr_event_off`, `tab_event_off`, `bgr_event_on`, `notify_dot`, `tab_event_on` |
| `PopupSendMail` | `002_UnityDataAssetPack_datapack__file110` | 5 | 4 | `bgr_text_3`, `popup_body`, `button_yellow`, `popup_close` |
| `PopupTestKeyboard` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `popup_close`, `popup`, `button_yellow`, `rect_16`, `InputFieldBackground` |
| `PopupMaintenance` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `button_green`, `popup_close`, `rect_help`, `title_help`, `rect_27` |
| `PopupSelectGuildAvatar` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `Background`, `button_close`, `UIMask`, `round_rect_8`, `guild_flag_1` |
| `PlayerAvatar` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `bgr_avatar`, `tag_level`, `item_transparent`, `border_avatar`, `mask_item` |
| `PopupGuildActivities` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `Background`, `round_rect_7`, `UIMask`, `round_rect_8`, `popup_close` |
| `PopupShowGameItems` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `popup_body`, `round_rect2`, `UIMask`, `Background`, `popup_close` |
| `PopupShowGameItems2` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `popup_body`, `round_rect2`, `UIMask`, `Background`, `popup_close` |
| `ButtonESkill` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `icon_lock_2`, `skill_frame`, `skill_selected`, `round_mask`, `skill_default` |
| `PopupMessageFK2` | `002_UnityDataAssetPack_datapack__file110` | 5 | 4 | `Background`, `button_fk`, `UIMask`, `bgr_popup_fk` |
| `PopupMessageFK` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `Background`, `button_blue`, `UIMask`, `popup_body`, `button_yellow` |
| `SplashTheme` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `logo_global`, `ace_title`, `ace_logo`, `ace_bgr`, `logo_small` |
| `PopupMFChooseDifficulty` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `card_easy`, `tag_hard`, `bgr_quest_content`, `card_hard`, `tag_easy` |
| `PopupGuildRecruit` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `round_rect_17`, `InputFieldBackground`, `popup_body`, `popup_close`, `button_yellow` |
| `CardSabaody` | `002_UnityDataAssetPack_datapack__file110` | 5 | 3 | `tag_name`, `tag_title_2`, `card_brave_trial` |
| `PopupBattleEndFk2` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `button_fk`, `round_rect_8`, `popup`, `gem_icon`, `fk_win2` |
| `PopupBattleEndFk` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `button_yellow`, `rect_20`, `popup`, `icon_gem`, `fk_win` |
| `PanelNewFeatureIntro` | `002_UnityDataAssetPack_datapack__file110` | 5 | 4 | `button_yellow`, `line_sm`, `island`, `bgr_show_bonus` |
| `PopupNotifyNewEvent` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `event_42`, `Background`, `UIMask`, `button_yellow`, `popup_close` |
| `PopupFeedbackFk2` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `button_fk`, `bgr_popup_fk`, `popup_close_fk`, `InputFieldBackground`, `rect_14` |
| `PopupFeedbackFakeEn` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `button_yellow`, `popup_body`, `popup_close`, `InputFieldBackground`, `rect_14` |
| `PopupFeedbackFk` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `button_yellow`, `popup_body`, `popup_close`, `InputFieldBackground`, `rect_14` |
| `ItemMaterial` | `002_UnityDataAssetPack_datapack__file110` | 5 | 5 | `tick_green`, `item_transparent`, `border_item`, `bgr_light`, `bgr_item_consumable` |
| `CanvasBgr` | `002_UnityDataAssetPack_datapack__file091` | 4 | 3 | `mf_background_1`, `mf_background_2`, `mf_background` |
| `PopupSettingFakeEn` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `button_music_on`, `popup_body`, `button_sound_on`, `popup_close` |
| `PopupSettingFK` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `button_music_on`, `popup_body`, `button_sound_on`, `popup_close` |
| `PopupSettingFK2` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `button_music_on_2`, `bgr_popup_fk`, `button_sound_on_2`, `popup_close_fk` |
| `Tab_Event_Sub` | `002_UnityDataAssetPack_datapack__file110` | 4 | 3 | `icon_gold`, `bgr_text_3`, `notify_dot` |
| `ItemAvatarUI` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `item_transparent`, `mask_item`, `bgr_item_consumable`, `border_item` |
| `PetHeadDragDrop` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `mask_item`, `bgr_item_consumable`, `frame_item`, `hero_head_border` |
| `PopupRewardTrailOfChampion` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `popup_close`, `round_rect_5`, `round_rect_7`, `popup` |
| `PopupGiftCode` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `round_rect_3`, `popup`, `popup_close`, `button_green` |
| `PopupNotify` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `popup_close`, `popup_body`, `round_rect_3`, `popup_title` |
| `PopupBxhEvent` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `button_close`, `bgr_text_3`, `item_none`, `bgr_hero_info` |
| `GGGemExchangeEvent` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `icon_gold`, `button_yellow`, `bgr_toast`, `round_rect_7` |
| `PopupShelterExchangeHero` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `button_yellow`, `popup_close`, `round_rect_18`, `popup_body` |
| `PopupBattleNext` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `rect_event_raid`, `button_edit`, `button_yellow`, `rect_help` |
| `ItemPotionDungeon` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `bgr_item_consumable`, `border_item`, `bgr_light`, `icon_lotion_4` |
| `ItemVipExpUI` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `icon_exp_vip`, `bgr_item_consumable`, `mask_item`, `border_item` |
| `PopupGameEvent` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `box_text_2`, `bgr_hero_info`, `bgr_hero_point`, `button_close` |
| `PopupShowBonusEffect2` | `002_UnityDataAssetPack_datapack__file110` | 4 | 3 | `bgr_show_bonus`, `line_sm`, `button_yellow` |
| `PopupShowListRewards` | `002_UnityDataAssetPack_datapack__file110` | 4 | 3 | `popup_close`, `popup_body`, `popup` |
| `PanelEventTemp` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `bgr_text_2`, `bgr_hero_info`, `button_yellow`, `bgr_light` |
| `TowerStage` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `progress_hp`, `progress_buff_bgr`, `tag_title`, `hero_shadow` |
| `PopupDiceTarotCardFake` | `002_UnityDataAssetPack_datapack__file110` | 4 | 2 | `56`, `popup_body` |
| `PanelTravelEvent` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `7`, `button_back`, `button_help`, `circle_on` |
| `PanelSpecialPackEvent` | `002_UnityDataAssetPack_datapack__file110` | 4 | 2 | `bgr_toast`, `button_yellow` |
| `GGCellAccumulatedProgress` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `round_rect_3`, `button_green`, `progress_top_2`, `progress_bgr` |
| `TutorialStory` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `new_pt_03`, `battle_skip`, `new_pt_02`, `new_pt_01` |
| `TowerHeroNew` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `hero_shadow`, `progress_buff_bgr`, `progress_hp`, `tag_title` |
| `CelesMine` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `local_mine`, `icon_progress_bar`, `icon_fill_progress`, `tag_name_home` |
| `KillStreakEffect` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `round_mask_2`, `streak_frame`, `339`, `streak_bgr` |
| `Tab_Special_Event` | `002_UnityDataAssetPack_datapack__file110` | 4 | 4 | `bgr_text_3`, `tab_off`, `tab_on`, `notify_dot` |
| `TapShopItem` | `002_UnityDataAssetPack_datapack__file110` | 4 | 2 | `gem_8`, `round_rect_14` |
| `TapPopupMessage` | `002_UnityDataAssetPack_datapack__file110` | 4 | 3 | `rect_help`, `tap_close` |
| `PopupInfoDropPosition` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `round_rect_4`, `round_rect_7`, `popup_body` |
| `ItemMonopolyInfo` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `200`, `icon_hp`, `icon_attack` |
| `PopupSafeArea` | `002_UnityDataAssetPack_datapack__file110` | 3 | 2 | `notify_dot`, `button_yellow` |
| `HeadHeroFusing` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `mask_item`, `border_item`, `hero_head_bgr` |
| `PopupHelp` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `popup_close`, `rect_help`, `title_help` |
| `PopupNap` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `button_close`, `bgr_hero_info`, `button_blue` |
| `ButtonPetSkill` | `002_UnityDataAssetPack_datapack__file110` | 3 | 2 | `skill_default`, `tag_number` |
| `DummyHealthBar` | `002_UnityDataAssetPack_datapack__file110` | 3 | 2 | `bar_hp`, `bar_hp_bgr` |
| `PanelTutorial` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `arrow_next`, `help_1`, `rect_13` |
| `PopupMenu` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `upgrade`, `rect`, `tech_info` |
| `IslandFirst` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `island_loot`, `island_locked`, `island` |
| `CanvasHero` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `progress_hp`, `progress_buff_bgr`, `hero_shadow` |
| `PanelMonthCardEvent` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `event_the_qua_nho`, `button_yellow`, `hero_name_bgr` |
| `GGMonthCardEvent` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `event_the_qua_nho`, `button_yellow`, `hero_name_bgr` |
| `ButtonSkill` | `002_UnityDataAssetPack_datapack__file110` | 3 | 2 | `skill_default`, `tag_number` |
| `PopupShowListRewardRandom` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `popup_close`, `popup_body`, `icon_add` |
| `GGAccumulatedEvent` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `round_rect_18`, `Background`, `UIMask` |
| `GGGiftCodeEvent` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `button_giftcode`, `banner_giftcode`, `input_giftcode` |
| `PanelGiftCode` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `button_giftcode`, `banner_giftcode`, `input_giftcode` |
| `ItemTab` | `002_UnityDataAssetPack_datapack__file110` | 3 | 2 | `tab_on`, `tab_off` |
| `InformationBraveTrial` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `rage`, `round_rect_8`, `line_connect_dark` |
| `CampMinion` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `local_enemy`, `building_enemy`, `base_ruin` |
| `PopupSuggest` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `popup_body`, `round_rect_content`, `popup_close` |
| `PopupExtraReward` | `002_UnityDataAssetPack_datapack__file110` | 3 | 3 | `UIMask`, `rect_help`, `Background` |
| `CanvasPop` | `002_UnityDataAssetPack_datapack__file059` | 2 | 1 | `UISprite` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file063` | 2 | 2 | `UISprite`, `Background` |
| `CanvasNoExpand` | `002_UnityDataAssetPack_datapack__file067` | 2 | 2 | `round_rect_3`, `pointer_chat` |
| `ButtonLotionHp` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `icon_lotion_1`, `frame_item` |
| `PanelLoadingFk` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `bgr_toast`, `Background` |
| `PanelLoadingInstant` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `bgr_toast`, `Background` |
| `PanelTutorialFk2` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `arrow_right`, `round_rect_9` |
| `PanelTutorialFk` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `arrow_next`, `rect_13` |
| `PanelWarningTime` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `Background`, `rect_15` |
| `TarvenQuestRequirementUI` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `tick_green`, `faction_abyss` |
| `PanelFirstPurchaseEvent` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `event_nap_lan_dau`, `button_yellow` |
| `Rate18` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `Rate_18`, `Background` |
| `ButtonLineupAura` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `border_aura`, `aura_none` |
| `PanelLoading` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `Background`, `bgr_toast` |
| `GGCellAccumulatedPurchase` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `round_rect_3`, `button_green` |
| `BrokenSpaceStage` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `icon_bs_locked`, `icon_bs_won` |
| `ItemContent` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `button_green`, `round_rect_7` |
| `TapDropItem` | `002_UnityDataAssetPack_datapack__file110` | 2 | 1 | `gold_tap2` |
| `PopupSpecialEvent` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `round_rect_content`, `button_close` |
| `BattleVs` | `002_UnityDataAssetPack_datapack__file110` | 2 | 1 | `board_name` |
| `GroupTestFont` | `002_UnityDataAssetPack_datapack__file110` | 2 | 1 | `UISprite` |
| `GGOnePackEvent` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `button_yellow`, `bgr_one_pack_placeholder` |
| `PopupHelpFk` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `popup_close`, `rect_help` |
| `PopupHelpFk2` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `popup_close_fk`, `round_rect_13` |
| `ButtonEventSpecial` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `icon_event_7_days`, `notify_dot` |
| `ButtonEvent` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `icon_event_7_days`, `notify_dot` |
| `PanelTutorialKeyHoleNew` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `tutorial_keyhole`, `UISprite` |
| `MFBossTag` | `002_UnityDataAssetPack_datapack__file110` | 2 | 2 | `boss_1`, `mf_floor_bgr` |
| `Canvas` | `002_UnityDataAssetPack_datapack__file103` | 1 | 1 | `button_back` |
| `PopupInfoBuilding` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `roun_rect6` |
| `FrameHeroPoint` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `rect_15` |
| `PopupCampaignFirst` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `161` |
| `PopupTemp` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `popup` |
| `PopupPurchaseIAP` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `rect_14` |
| `PopupBlockTouch` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `Background` |
| `ItemIconClazz` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `class_assassin` |
| `Toast` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `bgr_toast` |
| `ToastShowBonus` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `bgr_text_alpha` |
| `HandTutorialDrag` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `hand_tutorial` |
| `GGCellAccumulatedLimit` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `round_rect_3` |
| `CanvasHeroFk` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `hero_shadow` |
| `PopupDimScreen` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `Background` |
| `LootGold` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `loot_gold` |
| `CardRelicFly` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `card_relic_back` |
| `TopBar` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `board_title_1` |
| `PopupFullScreenEffect` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `Background` |
| `PanelOnePackEvent` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `button_yellow` |
| `PanelTutorialKeyHole` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `tutorial_keyhole` |
| `DiceTarotCardFake` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `56` |
| `DungeonMerchantMen` | `002_UnityDataAssetPack_datapack__file110` | 1 | 1 | `hero_shadow` |

## Validation notes

- The MonoBehaviour type tree is missing for all 19,643 examined UI.Image components; this pipeline therefore uses type-checked PPtr inference, **not a restored original type tree**.
- A single matching Sprite pointer must resolve to a genuine Unity Sprite in the correct serialized file. An offset is marked probable only when independently repeated within the same serialized file.
- Unresolved Image components may contain null references, dynamic Sprite assignments, or other cases not inferable statically.
- Runtime fidelity, layering, sprite atlases, animation and text localization remain unverified. No image binaries or extracted source code are committed by this report.
