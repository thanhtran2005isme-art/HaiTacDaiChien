# HTDC / KaitoKidShop — MySQL 8.4 foundation

**Branch:** `feat/htdc-mysql-foundation` (based on `feat/offline-hero-ui` @ `2cec061c`).  
**Scope:** New independent MySQL model and import tooling only. The game-original backend is **not** recovered. PR #9, Unity scenes, sprites and gameplay remain untouched.

## Findings from GitHub source audit

- The repo's root Unity client is `Assets/`, `Packages/`, `ProjectSettings/`.
- Main config source `Assets/Resources/_pack/db`: **53 actual files** = 46 JSON + 7 TXT; companion `db_en` 50, `db_th` 18, `db_zh` 18 = **139 actual source files** across four locales. Adjacent `.meta` files are Unity import metadata, not game records.
- Known primary records: `db_heroes.json` **751**; `db_battle_heroes.json` **186**; `db_quai.json` **8647**; `db_home_quest.json` **1333**; `db_tower.json` **1201**; `db_tower2.json` **701**; `db_vuotai.json` **999**; `db_item_consumable.json` **622**; `db_hero_upgrade.json` **330**; `db_item_baovat.json` **312**; `db_item_equipment.json` **100**; `db_pet.json` **7**; `db_hero_fusion.json` **163**; `db_vip.json` **13**; `db_player_exp.json` **210**. These are static config rows, not instances owned by any user.
- Reviewed source C# types: `DbManager`, `DatabaseFK`, `NetworkController`, `BoxLogin`, `PlayerInventory`, `PlayerData`, `LineUpSaveLoad`, `OfflineHeroCatalog`, `OfflineHeroParty`. Original AssetRipper runtime methods frequently return null/default or contain no body. `OfflineHeroParty` is an independent local PlayerPrefs roster of at most 6 heroes, **not** an owned-hero inventory. No deployable backend, API endpoints, SQL server, original authentication or live wallet exists.
- `db_hero_skill.json` is large; this migration imports the **complete file from the local checkout**. Remote snippets and partial API fetches are not used to truncate or reconstruct it.

## SQL schema

`database/mysql/001_schema.sql` creates **84 MySQL 8.0.36+/8.4 tables**. InnoDB + UTF8MB4 with PK/FK/unique/indexes, explicit state machines where appropriate:

| Scope | Tables |
|---|---|
| Source provenance and audit | `schema_migrations`, `catalog_files`, `catalog_records` |
| Structured source projections | `hero_definitions`, `skill_definitions`, `item_definitions`, `pet_definitions`, `monster_definitions`, `battle_hero_definitions`, `stage_definitions`, `quest_definitions`, `hero_upgrade_rules`, `hero_fusion_rules`, `pet_upgrade_rules`, `vip_rules`, `player_exp_rules`, `guild_boss_rules`, `hero_tier_links`, `hero_stone_definitions`, `equipment_set_definitions`, `shop_product_definitions`, `reward_rule_definitions`, `factions`, `currencies` |
| Accounts and currencies | `realms`, `accounts`, `account_credentials`, `account_sessions`, `account_login_events`, `players`, `player_settings`, `player_wallets`, `wallet_transactions` |
| Owned assets and team | `player_heroes`, `player_hero_skills`, `player_hero_skins`, `player_hero_skin_equips`, `player_items`, `inventory_transactions`, `player_hero_equipment`, `formations`, `formation_slots`, `player_pets`, `player_pet_skills` |
| Progress and battles | `player_campaign_progress`, `player_tower_progress`, `battle_runs`, `battle_events`, `reward_grants`, `player_quests`, `achievement_definitions`, `player_achievements` |
| Summons, store and events | `summon_banners`, `player_summon_pity`, `summon_history`, `shop_offers`, `shop_orders`, `payment_events`, `gift_codes`, `gift_code_redemptions`, `mail_messages`, `notifications`, `game_events`, `player_event_progress`, `player_daily_login`, `player_cooldowns`, `player_statistics` |
| Guild, PvP and social | `guilds`, `guild_members`, `guild_applications`, `guild_donations`, `guild_boss_runs`, `guild_boss_damage`, `arena_seasons`, `arena_rankings`, `arena_matches`, `friend_requests`, `friendships`, `chat_rooms`, `chat_room_members`, `chat_messages` |
| Governance | `admin_accounts`, `account_bans`, `audit_logs` |

**Integrity invariants:** `formation_slots` constrains 1–6 and prevents placing a hero belonging to a different player via composite foreign keys; reward and wallet mutations have idempotency keys; individual hero instances are separate from global hero catalog; shop provider events have deduplication keys. Account/passwords/tokens use hashes, not plaintext. For game-currency and inventory updates the eventual backend **must** wrap balance/ledger writes in a transaction and use `SELECT ... FOR UPDATE`, check nonnegative balances and verify item/equipment ownership. The schema alone cannot perform these service-layer checks.

## Import all real source data without creating fake accounts

1. Clone / pull branch `feat/htdc-mysql-foundation` and verify all source JSON/TXT are present.
2. From the repo root, run:
   ```powershell
   py -3 -m unittest discover -s tests -p "test_mysql_foundation.py" -v
   py -3 database/mysql/import_configs.py --repo-root . --validate-only
   py -3 database/mysql/import_configs.py --repo-root . --output database/mysql/generated/002_game_config_seed.sql
   ```
3. Create MySQL schema in **MySQL 8.4** using the actual local installation; from CMD:
   ```cmd
   mysql --default-character-set=utf8mb4 -u root -p < database\mysql\001_schema.sql
   mysql --default-character-set=utf8mb4 -u root -p < database\mysql\generated\002_game_config_seed.sql
   ```
   If `mysql` is not in PATH, use its full executable path (e.g. your MySQL 8.4 installation) or run commands in MySQL Shell/Workbench via script mode. Do **not** publish DB passwords, `.env`, generated SQL or imported commercial source data.
4. Verify in a MySQL client:
   ```sql
   SELECT COUNT(*) AS tables_total FROM information_schema.tables WHERE table_schema='htdc_game';
   SELECT COUNT(*) AS source_files FROM htdc_game.catalog_files;
   SELECT COUNT(*) AS heroes_total FROM htdc_game.hero_definitions;
   SELECT COUNT(*) AS player_count FROM htdc_game.players;
   SELECT locale,COUNT(*) FROM htdc_game.catalog_files GROUP BY locale;
   ```
   Expected on unchanged source checkout: **84 tables**, **139 imported source files**, **751 hero definitions**, **0 players**. Generated `database/mysql/generated/002_game_config_seed.audit.json` records SHA-256/row counts for **every** file.

### Idempotence and fidelity

- `catalog_files.raw_text` stores the full original UTF-8 text content, including nested JSON strings and otherwise opaque configuration; `source_sha256` hashes the original byte stream.
- `catalog_records` preserves all decoded top-level rows from all 139 sources, even nonstandard top-level maps; key names are kept as source data. TXT that is not valid JSON is retained as a single JSON-string record rather than treated as structured rows.
- Typed tables are populated from the **primary `db/` language only** where key/type meaning was inspected. Every typed row also carries `source_json`; all unprojected fields remain queryable in `catalog_records`. No account, player-owned hero, level 450, money, purchase or guild is fabricated.
- Reimport is repeatable via `ON DUPLICATE KEY UPDATE`, with a transaction **per source file** and original records refreshed by file ID. Typed rows intentionally stay in place when removed upstream (they may be referenced by player data); use a reviewed maintenance migration for deletions. **Back up live databases before changes**.
- Generator output may be large. It is a local artifact under ignored `database/mysql/generated/`, and is not committed. MySQL `max_allowed_packet` must be large enough for the longest source JSON file; at least 16 MiB is recommended in local import.

## Boundaries / follow-up work

- 84 tables are a broad independent game foundation, **not a verified recreation of the original server schema**. Source data does not reveal server-only business rules, reward transaction semantics, anti-cheat, match resolution, ownership, purchase validation, paywall, payment compliance or security protocols.
- There is **no real backend**, no API contract, no server process, no authentication, no game logic implementation. The importer does **not** run automatically against the user's MySQL instance.
- Before enabling gameplay, define backend stack, auth/rate limits, validated economy and reward policy, migrations/versioning, transactional services, API contracts, integration tests, observability and secret management.
- CI workflow `.github/workflows/mysql-foundation.yml` runs Python regression, whole-source validation and a disposable MySQL 8.4 schema+import check. A green CI does not imply that an external user's MySQL instance has been migrated.
- The broader `docs/ARCHITECTURE.md` contains legacy viewer paths; its stale description does not change the checked-out Unity source of this branch.
