-- HTDC XAMPP / MariaDB 10.4+ compatible SCHEMA: 84 tables.
-- Generated from database/mysql/001_schema.sql (MySQL 8.4) with portable
-- utf8mb4_unicode_ci collation. Do not use the MySQL-only collation
-- utf8mb4_0900_ai_ci in XAMPP's MariaDB 10.4.
-- Independent new design, NOT original game-server database.
-- Use from CMD: C:\xampp\mysql\bin\mysql.exe -u root -p < database\xampp\001_schema_mariadb.sql
-- Import the same generated database/mysql/generated/002_game_config_seed.sql afterwards.
-- HTDC / KaitoKidShop MySQL 8.0.36+ / 8.4 foundation; NEW_PROJECT_DESIGN.
-- This creates schema only. It does NOT recover the original game's server.
-- Apply using: mysql --default-character-set=utf8mb4 -u root -p < database/mysql/001_schema.sql
CREATE DATABASE IF NOT EXISTS htdc_game CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE htdc_game;
SET NAMES utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `schema_migrations` (
  version VARCHAR(100) PRIMARY KEY,
  checksum_sha256 CHAR(64) NOT NULL,
  applied_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `catalog_files` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  locale VARCHAR(8) NOT NULL,
  file_name VARCHAR(128) NOT NULL,
  source_sha256 CHAR(64) NOT NULL,
  raw_text LONGTEXT NOT NULL,
  parsed_json BOOLEAN NOT NULL,
  record_count INT UNSIGNED NOT NULL DEFAULT 0,
  imported_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  UNIQUE KEY uq_catalog_file (locale,file_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `catalog_records` (
  file_id BIGINT UNSIGNED NOT NULL,
  row_index INT UNSIGNED NOT NULL,
  record_key VARCHAR(160) DEFAULT NULL,
  payload JSON NOT NULL,
  PRIMARY KEY (file_id,row_index),
  KEY ix_catalog_record_key (record_key),
  CONSTRAINT fk_catalog_rows_file FOREIGN KEY (file_id) REFERENCES catalog_files(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `hero_definitions` (
  id INT UNSIGNED PRIMARY KEY,
  avatar_id INT UNSIGNED NOT NULL,
  display_name VARCHAR(180) NOT NULL,
  faction VARCHAR(30) NOT NULL,
  hero_class VARCHAR(40) NOT NULL DEFAULT '',
  star TINYINT UNSIGNED NOT NULL,
  hero_rank INT UNSIGNED NOT NULL DEFAULT 0,
  is_released BOOLEAN NOT NULL,
  stats_json JSON NOT NULL,
  skills_json JSON NOT NULL,
  source_json JSON NOT NULL,
  KEY ix_hero_avatar (avatar_id),
  KEY ix_hero_faction_star (faction,star,is_released)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `skill_definitions` (
  source_key VARCHAR(128) PRIMARY KEY,
  display_name VARCHAR(255),
  hero_id INT UNSIGNED,
  source_json JSON NOT NULL,
  KEY ix_skill_hero (hero_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `item_definitions` (
  kind ENUM('equipment','consumable','shard','artifact') NOT NULL,
  id INT UNSIGNED NOT NULL,
  display_name VARCHAR(200) NULL,
  quality SMALLINT DEFAULT NULL,
  source_json JSON NOT NULL,
  PRIMARY KEY(kind,id),
  KEY ix_item_name(display_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `pet_definitions` (
  id INT UNSIGNED PRIMARY KEY,
  display_name VARCHAR(200) NOT NULL,
  released BOOLEAN NOT NULL DEFAULT FALSE,
  source_json JSON NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `monster_definitions` (
  id INT UNSIGNED PRIMARY KEY,
  hero_link INT DEFAULT NULL,
  monster_level SMALLINT UNSIGNED DEFAULT NULL,
  source_json JSON NOT NULL,
  KEY ix_monster_link(hero_link)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `battle_hero_definitions` (
  source_key VARCHAR(128) PRIMARY KEY,
  display_name VARCHAR(200),
  source_json JSON NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `stage_definitions` (
  mode ENUM('campaign','tower','tower2') NOT NULL,
  id INT UNSIGNED NOT NULL,
  stage_label VARCHAR(64),
  recommended_power BIGINT UNSIGNED DEFAULT NULL,
  source_json JSON NOT NULL,
  PRIMARY KEY(mode,id),
  KEY ix_stage_label(stage_label)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `quest_definitions` (
  id INT UNSIGNED PRIMARY KEY,
  quest_type SMALLINT UNSIGNED,
  display_title VARCHAR(255),
  source_json JSON NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `hero_upgrade_rules` (
  level SMALLINT UNSIGNED PRIMARY KEY,
  gold_required BIGINT UNSIGNED NOT NULL DEFAULT 0,
  spirit_required BIGINT UNSIGNED NOT NULL DEFAULT 0,
  source_json JSON NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `hero_fusion_rules` (
  id INT UNSIGNED PRIMARY KEY,
  output_hero_id INT UNSIGNED NOT NULL,
  source_json JSON NOT NULL,
  KEY ix_fusion_output(output_hero_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `pet_upgrade_rules` (
  rule_group VARCHAR(40) NOT NULL,
  level SMALLINT UNSIGNED NOT NULL,
  source_json JSON NOT NULL,
  PRIMARY KEY(rule_group,level)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `vip_rules` (
  level SMALLINT UNSIGNED PRIMARY KEY,
  points_required BIGINT UNSIGNED NOT NULL DEFAULT 0,
  source_json JSON NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_exp_rules` (
  level SMALLINT UNSIGNED PRIMARY KEY,
  exp_required BIGINT UNSIGNED NOT NULL DEFAULT 0,
  source_json JSON NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `guild_boss_rules` (
  id INT UNSIGNED PRIMARY KEY,
  source_json JSON NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `hero_tier_links` (
  id INT UNSIGNED PRIMARY KEY,
  hero_4star_id INT UNSIGNED DEFAULT NULL,
  hero_5star_id INT UNSIGNED DEFAULT NULL,
  hero_6star_id INT UNSIGNED DEFAULT NULL,
  hero_10star_id INT UNSIGNED DEFAULT NULL,
  source_json JSON NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `hero_stone_definitions` (
  id INT UNSIGNED PRIMARY KEY,
  display_name VARCHAR(200),
  source_json JSON NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `equipment_set_definitions` (
  id INT UNSIGNED PRIMARY KEY,
  display_name VARCHAR(200),
  source_json JSON NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `shop_product_definitions` (
  product_code VARCHAR(150) PRIMARY KEY,
  product_group VARCHAR(60),
  source_json JSON NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `reward_rule_definitions` (
  rule_group VARCHAR(60) NOT NULL,
  rule_key VARCHAR(150) NOT NULL,
  source_json JSON NOT NULL,
  PRIMARY KEY(rule_group,rule_key)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `factions` (
  code VARCHAR(30) PRIMARY KEY,
  display_name VARCHAR(40) NOT NULL,
  position_order SMALLINT UNSIGNED NOT NULL UNIQUE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `currencies` (
  code VARCHAR(40) PRIMARY KEY,
  display_name VARCHAR(100) NOT NULL,
  premium BOOLEAN NOT NULL DEFAULT FALSE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `realms` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  realm_code VARCHAR(60) NOT NULL UNIQUE,
  display_name VARCHAR(100) NOT NULL,
  region VARCHAR(40) NOT NULL DEFAULT 'VN',
  state ENUM('ACTIVE','MAINTENANCE','CLOSED') NOT NULL DEFAULT 'ACTIVE',
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  updated_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `accounts` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  username VARCHAR(80) NOT NULL UNIQUE,
  email VARCHAR(254) DEFAULT NULL,
  email_verified_at DATETIME(6) DEFAULT NULL,
  status ENUM('ACTIVE','SUSPENDED','BANNED','CLOSED') NOT NULL DEFAULT 'ACTIVE',
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  updated_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  UNIQUE KEY uq_account_email(email)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `account_credentials` (
  account_id BIGINT UNSIGNED PRIMARY KEY,
  password_hash VARCHAR(255) NOT NULL,
  password_algorithm VARCHAR(35) NOT NULL DEFAULT 'argon2id',
  password_changed_at DATETIME(6),
  failed_attempts SMALLINT UNSIGNED NOT NULL DEFAULT 0,
  locked_until DATETIME(6),
  CONSTRAINT fk_cred_account FOREIGN KEY(account_id) REFERENCES accounts(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `account_sessions` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  account_id BIGINT UNSIGNED NOT NULL,
  refresh_token_hash CHAR(64) NOT NULL UNIQUE,
  device_id VARCHAR(150),
  ip_address VARBINARY(16),
  expires_at DATETIME(6) NOT NULL,
  revoked_at DATETIME(6),
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  KEY ix_session_account(account_id,expires_at),
  CONSTRAINT fk_session_account FOREIGN KEY(account_id) REFERENCES accounts(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `account_login_events` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  account_id BIGINT UNSIGNED NULL,
  username_attempted VARCHAR(80),
  success BOOLEAN NOT NULL,
  failure_reason VARCHAR(120),
  source_ip VARBINARY(16),
  occurred_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  KEY ix_login_account(account_id,occurred_at),
  CONSTRAINT fk_login_account FOREIGN KEY(account_id) REFERENCES accounts(id) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `players` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  account_id BIGINT UNSIGNED NOT NULL,
  realm_id BIGINT UNSIGNED NOT NULL,
  nickname VARCHAR(80) NOT NULL,
  player_level SMALLINT UNSIGNED NOT NULL DEFAULT 1,
  exp BIGINT UNSIGNED NOT NULL DEFAULT 0,
  vip_level SMALLINT UNSIGNED NOT NULL DEFAULT 0,
  avatar_id INT UNSIGNED NULL,
  last_seen_at DATETIME(6),
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  updated_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  UNIQUE KEY uq_player_account_realm(account_id,realm_id),
  UNIQUE KEY uq_player_nickname_realm(realm_id,nickname),
  CONSTRAINT fk_player_account FOREIGN KEY(account_id) REFERENCES accounts(id),
  CONSTRAINT fk_player_realm FOREIGN KEY(realm_id) REFERENCES realms(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_settings` (
  player_id BIGINT UNSIGNED PRIMARY KEY,
  language_code VARCHAR(10) NOT NULL DEFAULT 'vi',
  music_enabled BOOLEAN NOT NULL DEFAULT TRUE,
  sound_enabled BOOLEAN NOT NULL DEFAULT TRUE,
  auto_battle_enabled BOOLEAN NOT NULL DEFAULT FALSE,
  extra JSON,
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  updated_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  CONSTRAINT fk_settings_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_wallets` (
  player_id BIGINT UNSIGNED NOT NULL,
  currency_code VARCHAR(40) NOT NULL,
  balance BIGINT UNSIGNED NOT NULL DEFAULT 0,
  version BIGINT UNSIGNED NOT NULL DEFAULT 0,
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  updated_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  PRIMARY KEY(player_id,currency_code),
  CONSTRAINT fk_wallet_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE,
  CONSTRAINT fk_wallet_currency FOREIGN KEY(currency_code) REFERENCES currencies(code)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `wallet_transactions` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  player_id BIGINT UNSIGNED NOT NULL,
  currency_code VARCHAR(40) NOT NULL,
  change_amount BIGINT NOT NULL,
  balance_after BIGINT UNSIGNED NOT NULL,
  reason VARCHAR(80) NOT NULL,
  request_key VARCHAR(120) NOT NULL,
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  UNIQUE KEY uq_wallet_request(player_id,currency_code,request_key),
  KEY ix_wallet_history(player_id,created_at),
  CONSTRAINT fk_wallet_tx_wallet FOREIGN KEY(player_id,currency_code) REFERENCES player_wallets(player_id,currency_code)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_heroes` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  player_id BIGINT UNSIGNED NOT NULL,
  hero_definition_id INT UNSIGNED NOT NULL,
  hero_level SMALLINT UNSIGNED NOT NULL DEFAULT 1,
  exp BIGINT UNSIGNED NOT NULL DEFAULT 0,
  ascension_level SMALLINT UNSIGNED NOT NULL DEFAULT 0,
  star TINYINT UNSIGNED NOT NULL,
  locked BOOLEAN NOT NULL DEFAULT FALSE,
  acquired_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  updated_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  KEY ix_player_hero(player_id,hero_definition_id),
  UNIQUE KEY uq_player_hero_owner(id,player_id),
  CONSTRAINT fk_hero_owner FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE,
  CONSTRAINT fk_hero_definition FOREIGN KEY(hero_definition_id) REFERENCES hero_definitions(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_hero_skills` (
  hero_id BIGINT UNSIGNED NOT NULL,
  skill_key VARCHAR(128) NOT NULL,
  skill_level SMALLINT UNSIGNED NOT NULL DEFAULT 1,
  PRIMARY KEY(hero_id,skill_key),
  CONSTRAINT fk_hero_skill_hero FOREIGN KEY(hero_id) REFERENCES player_heroes(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_hero_skins` (
  player_id BIGINT UNSIGNED NOT NULL,
  skin_id INT UNSIGNED NOT NULL,
  owned_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  PRIMARY KEY(player_id,skin_id),
  CONSTRAINT fk_hero_skin_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_hero_skin_equips` (
  hero_id BIGINT UNSIGNED PRIMARY KEY,
  skin_id INT UNSIGNED NOT NULL,
  changed_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  CONSTRAINT fk_skin_equip_hero FOREIGN KEY(hero_id) REFERENCES player_heroes(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_items` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  player_id BIGINT UNSIGNED NOT NULL,
  item_kind ENUM('equipment','consumable','shard','artifact','misc') NOT NULL,
  item_definition_id INT UNSIGNED NOT NULL,
  quantity BIGINT UNSIGNED NOT NULL DEFAULT 0,
  upgrade_level SMALLINT UNSIGNED NOT NULL DEFAULT 0,
  locked BOOLEAN NOT NULL DEFAULT FALSE,
  metadata_json JSON,
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  updated_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  KEY ix_player_item(player_id,item_kind,item_definition_id),
  CONSTRAINT fk_player_item_owner FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `inventory_transactions` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  player_id BIGINT UNSIGNED NOT NULL,
  item_id BIGINT UNSIGNED NOT NULL,
  delta BIGINT NOT NULL,
  quantity_after BIGINT UNSIGNED NOT NULL,
  reason VARCHAR(80) NOT NULL,
  request_key VARCHAR(120) NOT NULL,
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  UNIQUE KEY uq_inventory_request(player_id,item_id,request_key),
  CONSTRAINT fk_inv_tx_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE,
  CONSTRAINT fk_inv_tx_item FOREIGN KEY(item_id) REFERENCES player_items(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_hero_equipment` (
  hero_id BIGINT UNSIGNED NOT NULL,
  slot_no TINYINT UNSIGNED NOT NULL,
  item_id BIGINT UNSIGNED NOT NULL UNIQUE,
  PRIMARY KEY(hero_id,slot_no),
  CONSTRAINT fk_equipped_hero FOREIGN KEY(hero_id) REFERENCES player_heroes(id) ON DELETE CASCADE,
  CONSTRAINT fk_equipped_item FOREIGN KEY(item_id) REFERENCES player_items(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `formations` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  player_id BIGINT UNSIGNED NOT NULL,
  formation_type VARCHAR(40) NOT NULL DEFAULT 'DEFAULT',
  formation_index TINYINT UNSIGNED NOT NULL DEFAULT 1,
  display_name VARCHAR(80),
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  updated_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  UNIQUE KEY uq_formation_owner(id,player_id),
  UNIQUE KEY uq_formation_type(player_id,formation_type,formation_index),
  CONSTRAINT fk_formation_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `formation_slots` (
  formation_id BIGINT UNSIGNED NOT NULL,
  player_id BIGINT UNSIGNED NOT NULL,
  slot_no TINYINT UNSIGNED NOT NULL,
  player_hero_id BIGINT UNSIGNED NOT NULL,
  PRIMARY KEY(formation_id,slot_no),
  UNIQUE KEY uq_formation_hero(formation_id,player_hero_id),
  CONSTRAINT chk_formation_slot CHECK(slot_no BETWEEN 1 AND 6),
  CONSTRAINT fk_slot_formation FOREIGN KEY(formation_id,player_id) REFERENCES formations(id,player_id) ON DELETE CASCADE,
  CONSTRAINT fk_slot_hero FOREIGN KEY(player_hero_id,player_id) REFERENCES player_heroes(id,player_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_pets` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  player_id BIGINT UNSIGNED NOT NULL,
  pet_definition_id INT UNSIGNED NOT NULL,
  pet_level SMALLINT UNSIGNED NOT NULL DEFAULT 1,
  evolution_level SMALLINT UNSIGNED NOT NULL DEFAULT 0,
  unlocked_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  UNIQUE KEY uq_player_pet(player_id,pet_definition_id),
  CONSTRAINT fk_pet_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE,
  CONSTRAINT fk_pet_definition FOREIGN KEY(pet_definition_id) REFERENCES pet_definitions(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_pet_skills` (
  player_pet_id BIGINT UNSIGNED NOT NULL,
  skill_slot TINYINT UNSIGNED NOT NULL,
  skill_level SMALLINT UNSIGNED NOT NULL DEFAULT 1,
  PRIMARY KEY(player_pet_id,skill_slot),
  CONSTRAINT fk_pet_skill_player FOREIGN KEY(player_pet_id) REFERENCES player_pets(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_campaign_progress` (
  player_id BIGINT UNSIGNED NOT NULL,
  stage_id INT UNSIGNED NOT NULL,
  stars TINYINT UNSIGNED NOT NULL DEFAULT 0,
  best_clear_ms INT UNSIGNED,
  cleared_at DATETIME(6),
  PRIMARY KEY(player_id,stage_id),
  CONSTRAINT fk_campaign_progress_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_tower_progress` (
  player_id BIGINT UNSIGNED NOT NULL,
  tower_mode ENUM('tower','tower2') NOT NULL,
  highest_stage_id INT UNSIGNED NOT NULL DEFAULT 0,
  last_claimed_stage_id INT UNSIGNED NOT NULL DEFAULT 0,
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  updated_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  PRIMARY KEY(player_id,tower_mode),
  CONSTRAINT fk_tower_progress_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `battle_runs` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  player_id BIGINT UNSIGNED NOT NULL,
  mode VARCHAR(40) NOT NULL,
  stage_id INT UNSIGNED NULL,
  opponent_player_id BIGINT UNSIGNED NULL,
  formation_snapshot JSON NOT NULL,
  rng_seed BIGINT UNSIGNED DEFAULT NULL,
  state ENUM('STARTED','WON','LOST','ABORTED') NOT NULL DEFAULT 'STARTED',
  result_json JSON,
  started_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  completed_at DATETIME(6),
  KEY ix_battle_player(player_id,started_at),
  CONSTRAINT fk_battle_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE,
  CONSTRAINT fk_battle_opponent FOREIGN KEY(opponent_player_id) REFERENCES players(id) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `battle_events` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  battle_id BIGINT UNSIGNED NOT NULL,
  turn_no SMALLINT UNSIGNED NOT NULL,
  event_order SMALLINT UNSIGNED NOT NULL,
  actor_ref VARCHAR(80),
  event_type VARCHAR(60) NOT NULL,
  payload JSON NOT NULL,
  UNIQUE KEY uq_battle_event(battle_id,turn_no,event_order),
  CONSTRAINT fk_battle_event_run FOREIGN KEY(battle_id) REFERENCES battle_runs(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `reward_grants` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  player_id BIGINT UNSIGNED NOT NULL,
  reason VARCHAR(80) NOT NULL,
  reference_type VARCHAR(40),
  reference_id VARCHAR(100),
  idempotency_key VARCHAR(128) NOT NULL,
  reward_snapshot JSON NOT NULL,
  granted_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  UNIQUE KEY uq_reward_idempotency(player_id,idempotency_key),
  CONSTRAINT fk_reward_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_quests` (
  player_id BIGINT UNSIGNED NOT NULL,
  quest_definition_id INT UNSIGNED NOT NULL,
  progress_json JSON NOT NULL,
  status ENUM('ACTIVE','COMPLETED','CLAIMED') NOT NULL DEFAULT 'ACTIVE',
  completed_at DATETIME(6),
  claimed_at DATETIME(6),
  PRIMARY KEY(player_id,quest_definition_id),
  CONSTRAINT fk_player_quest_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE,
  CONSTRAINT fk_player_quest_def FOREIGN KEY(quest_definition_id) REFERENCES quest_definitions(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `achievement_definitions` (
  code VARCHAR(80) PRIMARY KEY,
  display_name VARCHAR(200) NOT NULL,
  condition_json JSON NOT NULL,
  reward_json JSON NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_achievements` (
  player_id BIGINT UNSIGNED NOT NULL,
  achievement_code VARCHAR(80) NOT NULL,
  progress BIGINT UNSIGNED NOT NULL DEFAULT 0,
  claimed_at DATETIME(6),
  PRIMARY KEY(player_id,achievement_code),
  CONSTRAINT fk_ach_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE,
  CONSTRAINT fk_ach_def FOREIGN KEY(achievement_code) REFERENCES achievement_definitions(code)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `summon_banners` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  banner_code VARCHAR(80) NOT NULL UNIQUE,
  starts_at DATETIME(6) NOT NULL,
  ends_at DATETIME(6) NOT NULL,
  cost_json JSON NOT NULL,
  pool_json JSON NOT NULL,
  CHECK(ends_at>starts_at)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_summon_pity` (
  player_id BIGINT UNSIGNED NOT NULL,
  banner_id BIGINT UNSIGNED NOT NULL,
  pity_count INT UNSIGNED NOT NULL DEFAULT 0,
  total_pulls BIGINT UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY(player_id,banner_id),
  CONSTRAINT fk_pity_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE,
  CONSTRAINT fk_pity_banner FOREIGN KEY(banner_id) REFERENCES summon_banners(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `summon_history` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  player_id BIGINT UNSIGNED NOT NULL,
  banner_id BIGINT UNSIGNED NOT NULL,
  acquired_hero_id BIGINT UNSIGNED NULL,
  result_json JSON NOT NULL,
  request_key VARCHAR(120) NOT NULL,
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  UNIQUE KEY uq_summon_request(player_id,request_key),
  CONSTRAINT fk_summon_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE,
  CONSTRAINT fk_summon_banner FOREIGN KEY(banner_id) REFERENCES summon_banners(id),
  CONSTRAINT fk_summon_hero FOREIGN KEY(acquired_hero_id) REFERENCES player_heroes(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `shop_offers` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  offer_code VARCHAR(100) NOT NULL UNIQUE,
  display_name VARCHAR(200) NOT NULL,
  price_json JSON NOT NULL,
  reward_json JSON NOT NULL,
  purchase_limit INT UNSIGNED,
  starts_at DATETIME(6),
  ends_at DATETIME(6),
  enabled BOOLEAN NOT NULL DEFAULT TRUE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `shop_orders` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  player_id BIGINT UNSIGNED NOT NULL,
  offer_id BIGINT UNSIGNED NOT NULL,
  request_key VARCHAR(120) NOT NULL,
  state ENUM('PENDING','PAID','FULFILLED','CANCELLED','REFUNDED') NOT NULL DEFAULT 'PENDING',
  price_snapshot JSON NOT NULL,
  reward_snapshot JSON NOT NULL,
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  updated_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  UNIQUE KEY uq_shop_request(player_id,request_key),
  CONSTRAINT fk_shop_player FOREIGN KEY(player_id) REFERENCES players(id),
  CONSTRAINT fk_shop_offer FOREIGN KEY(offer_id) REFERENCES shop_offers(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `payment_events` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  order_id BIGINT UNSIGNED NOT NULL,
  provider VARCHAR(50) NOT NULL,
  provider_event_id VARCHAR(180) NOT NULL,
  event_payload JSON NOT NULL,
  received_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  UNIQUE KEY uq_payment_event(provider,provider_event_id),
  CONSTRAINT fk_pay_order FOREIGN KEY(order_id) REFERENCES shop_orders(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `gift_codes` (
  code_hash CHAR(64) PRIMARY KEY,
  reward_json JSON NOT NULL,
  max_redemptions INT UNSIGNED NOT NULL DEFAULT 1,
  redeemed_count INT UNSIGNED NOT NULL DEFAULT 0,
  expires_at DATETIME(6),
  active BOOLEAN NOT NULL DEFAULT TRUE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `gift_code_redemptions` (
  player_id BIGINT UNSIGNED NOT NULL,
  code_hash CHAR(64) NOT NULL,
  redeemed_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  PRIMARY KEY(player_id,code_hash),
  CONSTRAINT fk_redeem_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE,
  CONSTRAINT fk_redeem_code FOREIGN KEY(code_hash) REFERENCES gift_codes(code_hash)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `mail_messages` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  player_id BIGINT UNSIGNED NOT NULL,
  subject VARCHAR(200) NOT NULL,
  body TEXT,
  attachments_json JSON,
  read_at DATETIME(6),
  claimed_at DATETIME(6),
  expires_at DATETIME(6),
  sent_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  KEY ix_mail_inbox(player_id,sent_at),
  CONSTRAINT fk_mail_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `notifications` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  player_id BIGINT UNSIGNED NOT NULL,
  notification_type VARCHAR(60) NOT NULL,
  payload JSON NOT NULL,
  read_at DATETIME(6),
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  KEY ix_notify_player(player_id,created_at),
  CONSTRAINT fk_notify_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `guilds` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  realm_id BIGINT UNSIGNED NOT NULL,
  display_name VARCHAR(120) NOT NULL,
  description TEXT,
  owner_player_id BIGINT UNSIGNED NOT NULL,
  guild_level SMALLINT UNSIGNED NOT NULL DEFAULT 1,
  exp BIGINT UNSIGNED NOT NULL DEFAULT 0,
  currency BIGINT UNSIGNED NOT NULL DEFAULT 0,
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  updated_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  UNIQUE KEY uq_guild_realm_name(realm_id,display_name),
  CONSTRAINT fk_guild_realm FOREIGN KEY(realm_id) REFERENCES realms(id),
  CONSTRAINT fk_guild_owner FOREIGN KEY(owner_player_id) REFERENCES players(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `guild_members` (
  guild_id BIGINT UNSIGNED NOT NULL,
  player_id BIGINT UNSIGNED NOT NULL UNIQUE,
  guild_role ENUM('OWNER','OFFICER','MEMBER') NOT NULL DEFAULT 'MEMBER',
  joined_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  PRIMARY KEY(guild_id,player_id),
  CONSTRAINT fk_gmember_guild FOREIGN KEY(guild_id) REFERENCES guilds(id) ON DELETE CASCADE,
  CONSTRAINT fk_gmember_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `guild_applications` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  guild_id BIGINT UNSIGNED NOT NULL,
  player_id BIGINT UNSIGNED NOT NULL,
  status ENUM('PENDING','APPROVED','REJECTED','CANCELLED') NOT NULL DEFAULT 'PENDING',
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  decided_at DATETIME(6),
  KEY ix_gapply_status(guild_id,status),
  CONSTRAINT fk_gapply_guild FOREIGN KEY(guild_id) REFERENCES guilds(id) ON DELETE CASCADE,
  CONSTRAINT fk_gapply_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `guild_donations` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  guild_id BIGINT UNSIGNED NOT NULL,
  player_id BIGINT UNSIGNED NOT NULL,
  currency_code VARCHAR(40) NOT NULL,
  amount BIGINT UNSIGNED NOT NULL,
  request_key VARCHAR(120) NOT NULL,
  donated_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  UNIQUE KEY uq_gdonation(player_id,request_key),
  CONSTRAINT fk_gdon_guild FOREIGN KEY(guild_id) REFERENCES guilds(id),
  CONSTRAINT fk_gdon_player FOREIGN KEY(player_id) REFERENCES players(id),
  CONSTRAINT fk_gdon_currency FOREIGN KEY(currency_code) REFERENCES currencies(code)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `guild_boss_runs` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  guild_id BIGINT UNSIGNED NOT NULL,
  boss_definition_id INT UNSIGNED NOT NULL,
  current_hp BIGINT UNSIGNED NOT NULL,
  started_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  ended_at DATETIME(6),
  CONSTRAINT fk_gboss_guild FOREIGN KEY(guild_id) REFERENCES guilds(id),
  CONSTRAINT fk_gboss_def FOREIGN KEY(boss_definition_id) REFERENCES guild_boss_rules(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `guild_boss_damage` (
  run_id BIGINT UNSIGNED NOT NULL,
  player_id BIGINT UNSIGNED NOT NULL,
  total_damage BIGINT UNSIGNED NOT NULL DEFAULT 0,
  last_attack_at DATETIME(6),
  PRIMARY KEY(run_id,player_id),
  CONSTRAINT fk_gboss_dmg_run FOREIGN KEY(run_id) REFERENCES guild_boss_runs(id) ON DELETE CASCADE,
  CONSTRAINT fk_gboss_dmg_player FOREIGN KEY(player_id) REFERENCES players(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `arena_seasons` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  realm_id BIGINT UNSIGNED NOT NULL,
  display_name VARCHAR(100),
  starts_at DATETIME(6) NOT NULL,
  ends_at DATETIME(6) NOT NULL,
  CONSTRAINT fk_arena_realm FOREIGN KEY(realm_id) REFERENCES realms(id),
  CHECK(ends_at>starts_at)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `arena_rankings` (
  season_id BIGINT UNSIGNED NOT NULL,
  player_id BIGINT UNSIGNED NOT NULL,
  points BIGINT NOT NULL DEFAULT 0,
  wins INT UNSIGNED NOT NULL DEFAULT 0,
  losses INT UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY(season_id,player_id),
  KEY ix_arena_points(season_id,points),
  CONSTRAINT fk_arena_season FOREIGN KEY(season_id) REFERENCES arena_seasons(id) ON DELETE CASCADE,
  CONSTRAINT fk_arena_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `arena_matches` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  season_id BIGINT UNSIGNED NOT NULL,
  attacker_id BIGINT UNSIGNED NOT NULL,
  defender_id BIGINT UNSIGNED NOT NULL,
  battle_run_id BIGINT UNSIGNED DEFAULT NULL,
  result ENUM('ATTACKER','DEFENDER','DRAW','ABORTED') NOT NULL,
  played_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  CONSTRAINT fk_arena_match_season FOREIGN KEY(season_id) REFERENCES arena_seasons(id),
  CONSTRAINT fk_arena_attacker FOREIGN KEY(attacker_id) REFERENCES players(id),
  CONSTRAINT fk_arena_defender FOREIGN KEY(defender_id) REFERENCES players(id),
  CONSTRAINT fk_arena_battle FOREIGN KEY(battle_run_id) REFERENCES battle_runs(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `friend_requests` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  requester_player_id BIGINT UNSIGNED NOT NULL,
  addressee_player_id BIGINT UNSIGNED NOT NULL,
  state ENUM('PENDING','ACCEPTED','REJECTED','CANCELLED') NOT NULL DEFAULT 'PENDING',
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  UNIQUE KEY uq_friend_request(requester_player_id,addressee_player_id),
  CHECK(requester_player_id<>addressee_player_id),
  CONSTRAINT fk_friend_requester FOREIGN KEY(requester_player_id) REFERENCES players(id) ON DELETE CASCADE,
  CONSTRAINT fk_friend_addressee FOREIGN KEY(addressee_player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `friendships` (
  player_low_id BIGINT UNSIGNED NOT NULL,
  player_high_id BIGINT UNSIGNED NOT NULL,
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  PRIMARY KEY(player_low_id,player_high_id),
  CHECK(player_low_id<player_high_id),
  CONSTRAINT fk_friend_low FOREIGN KEY(player_low_id) REFERENCES players(id) ON DELETE CASCADE,
  CONSTRAINT fk_friend_high FOREIGN KEY(player_high_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `chat_rooms` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  room_type ENUM('GLOBAL','REALM','GUILD','PRIVATE') NOT NULL,
  realm_id BIGINT UNSIGNED,
  guild_id BIGINT UNSIGNED,
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  CONSTRAINT fk_chat_realm FOREIGN KEY(realm_id) REFERENCES realms(id),
  CONSTRAINT fk_chat_guild FOREIGN KEY(guild_id) REFERENCES guilds(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `chat_room_members` (
  room_id BIGINT UNSIGNED NOT NULL,
  player_id BIGINT UNSIGNED NOT NULL,
  joined_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  PRIMARY KEY(room_id,player_id),
  CONSTRAINT fk_member_room FOREIGN KEY(room_id) REFERENCES chat_rooms(id) ON DELETE CASCADE,
  CONSTRAINT fk_member_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `chat_messages` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  room_id BIGINT UNSIGNED NOT NULL,
  sender_player_id BIGINT UNSIGNED NOT NULL,
  message TEXT NOT NULL,
  sent_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  deleted_at DATETIME(6),
  KEY ix_chat_room_ts(room_id,sent_at),
  CONSTRAINT fk_chat_message_room FOREIGN KEY(room_id) REFERENCES chat_rooms(id) ON DELETE CASCADE,
  CONSTRAINT fk_chat_message_sender FOREIGN KEY(sender_player_id) REFERENCES players(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `game_events` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  event_code VARCHAR(100) NOT NULL UNIQUE,
  display_name VARCHAR(200) NOT NULL,
  rules_json JSON NOT NULL,
  starts_at DATETIME(6) NOT NULL,
  ends_at DATETIME(6) NOT NULL,
  active BOOLEAN NOT NULL DEFAULT TRUE,
  CHECK(ends_at>starts_at)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_event_progress` (
  player_id BIGINT UNSIGNED NOT NULL,
  event_id BIGINT UNSIGNED NOT NULL,
  progress_json JSON NOT NULL,
  claimed_json JSON,
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  updated_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  PRIMARY KEY(player_id,event_id),
  CONSTRAINT fk_event_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE,
  CONSTRAINT fk_event_game FOREIGN KEY(event_id) REFERENCES game_events(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_daily_login` (
  player_id BIGINT UNSIGNED NOT NULL,
  login_day DATE NOT NULL,
  streak SMALLINT UNSIGNED NOT NULL DEFAULT 1,
  reward_grant_id BIGINT UNSIGNED,
  PRIMARY KEY(player_id,login_day),
  CONSTRAINT fk_login_reward_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE,
  CONSTRAINT fk_login_reward_grant FOREIGN KEY(reward_grant_id) REFERENCES reward_grants(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_cooldowns` (
  player_id BIGINT UNSIGNED NOT NULL,
  cooldown_key VARCHAR(100) NOT NULL,
  ready_at DATETIME(6) NOT NULL,
  PRIMARY KEY(player_id,cooldown_key),
  CONSTRAINT fk_cooldown_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `player_statistics` (
  player_id BIGINT UNSIGNED NOT NULL,
  stat_key VARCHAR(80) NOT NULL,
  stat_value BIGINT NOT NULL DEFAULT 0,
  updated_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  PRIMARY KEY(player_id,stat_key),
  CONSTRAINT fk_stat_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `admin_accounts` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  account_id BIGINT UNSIGNED NOT NULL UNIQUE,
  role ENUM('SUPPORT','GM','ADMIN','SUPER_ADMIN') NOT NULL,
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  CONSTRAINT fk_admin_account FOREIGN KEY(account_id) REFERENCES accounts(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `account_bans` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  account_id BIGINT UNSIGNED NOT NULL,
  admin_id BIGINT UNSIGNED NOT NULL,
  reason VARCHAR(500) NOT NULL,
  expires_at DATETIME(6),
  revoked_at DATETIME(6),
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  CONSTRAINT fk_ban_account FOREIGN KEY(account_id) REFERENCES accounts(id),
  CONSTRAINT fk_ban_admin FOREIGN KEY(admin_id) REFERENCES admin_accounts(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `audit_logs` (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  actor_account_id BIGINT UNSIGNED NULL,
  player_id BIGINT UNSIGNED NULL,
  action VARCHAR(100) NOT NULL,
  entity_type VARCHAR(60) NOT NULL,
  entity_key VARCHAR(120) NOT NULL,
  before_json JSON,
  after_json JSON,
  trace_id VARCHAR(120),
  created_at DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  KEY ix_audit_actor(actor_account_id,created_at),
  KEY ix_audit_entity(entity_type,entity_key),
  CONSTRAINT fk_audit_actor FOREIGN KEY(actor_account_id) REFERENCES accounts(id) ON DELETE SET NULL,
  CONSTRAINT fk_audit_player FOREIGN KEY(player_id) REFERENCES players(id) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Non-account reference data only; no fabricated player, wallet or inventory rows.
INSERT INTO factions(code,display_name,position_order) VALUES
 ('fortress','Nhanh nhẹn',1),('abyss','Bền bỉ',2),('forest','Chính xác',3),
 ('shadow','Sinh khí',4),('dark','Thông minh',5),('light','Minh mẫn',6)
ON DUPLICATE KEY UPDATE display_name=VALUES(display_name), position_order=VALUES(position_order);

-- These are NEW currency identifiers for the independent server and must be
-- aligned to a verified economic specification before any paid gameplay.
INSERT INTO currencies(code,display_name,premium) VALUES
 ('gold','Vàng',0),('gems','Kim cương',1)
ON DUPLICATE KEY UPDATE display_name=VALUES(display_name), premium=VALUES(premium);
