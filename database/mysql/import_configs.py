#!/usr/bin/env python3
"""Deterministic, stdlib-only MySQL 8 source-config importer for HTDC.

Source -> SQL (NOT into the user's MySQL without their explicit local action).
Covers all .json and .txt under db, db_en, db_th, db_zh. Preserves every
source file as text and every decoded top-level record as native MySQL JSON.
Populates curated, typed primary-locale lookup tables when source fields exist.
Never creates player, account, currency balance or fake game server data.
"""
from __future__ import annotations

import argparse
import hashlib
import json
import re
import sys
from collections import Counter
from pathlib import Path

LOCALES = {"db": "vi", "db_en": "en", "db_th": "th", "db_zh": "zh"}
SOURCE_PARENT = Path("Assets/Resources/_pack")
MAX_KEY_LENGTH = 160


class ImportErrorDetail(ValueError):
    pass


def sql_text(value: object | None) -> str:
    """UTF8 hex literal avoids SQL_MODE quoting/escape ambiguity."""
    if value is None:
        return "NULL"
    data = str(value).encode("utf-8")
    return "CONVERT(0x" + data.hex() + " USING utf8mb4)"


def sql_number(value: object | None, *, default: int | None = None) -> str:
    if value is None or value == "":
        return "NULL" if default is None else str(default)
    try:
        x = int(str(value))
    except (TypeError, ValueError) as exc:
        raise ImportErrorDetail("Invalid integer: " + repr(value)) from exc
    return str(x)


def sql_json(data: object) -> str:
    return sql_text(json.dumps(data, ensure_ascii=False, separators=(",", ":"), allow_nan=False))


def rows_from_root(root: object) -> list[tuple[str | None, object]]:
    if isinstance(root, list):
        return [(str(x.get("id", x.get("key", i))) if isinstance(x, dict) else str(i), x)
                for i, x in enumerate(root)]
    if isinstance(root, dict):
        return [(str(key), value) for key, value in root.items()]
    return [("0", root)]


def upsert(table: str, columns: dict[str, str], pk: tuple[str, ...]) -> str:
    keys = list(columns)
    if not re.fullmatch(r"[a-z_]+", table):
        raise ImportErrorDetail("Invalid table: " + table)
    sets = [f"{key}=VALUES({key})" for key in keys if key not in pk]
    if not sets:
        raise ImportErrorDetail("Missing updatable column: " + table)
    return (f"INSERT INTO {table} ({','.join(keys)}) VALUES "
            f"({','.join(columns.values())}) ON DUPLICATE KEY UPDATE "
            + ",".join(sets) + ";\n")


def integer_from(row: dict, key: str, fallback: int | None = None) -> str:
    return sql_number(row.get(key), default=fallback)


def identity(row: dict, source: str, key: str = "id") -> int:
    value = row.get(key)
    try:
        number = int(value)
    except (TypeError, ValueError) as exc:
        raise ImportErrorDetail(f"{source}: missing/invalid {key} {value!r}") from exc
    if number < 0 or number > 2147483647:
        raise ImportErrorDetail(f"{source}: {key} exceeds signed 32-bit configuration range")
    return number


def materialize(filename: str, row: object, index: int) -> str:
    """Typed projections preserve original JSON, do not infer gameplay."""
    if not isinstance(row, dict):
        return ""
    key = filename
    source_json = sql_json(row)
    table = ""
    values: dict[str, str] = {}
    pk: tuple[str, ...] = ()
    if key == "db_heroes.json":
        table, pk = "hero_definitions", ("id",)
        skills = {k: row.get(k) for k in ("active", "passive1", "passive2", "passive3")}
        stats = {k: v for k, v in row.items() if "scale" in k or k.endswith("_origin") or k == "point"}
        values = dict(id=sql_number(identity(row,key)), avatar_id=integer_from(row,"avatar",0),
            display_name=sql_text(row.get("name","")), faction=sql_text(row.get("type","unknown")),
            hero_class=sql_text(row.get("class","")), star=integer_from(row,"star",0),
            hero_rank=integer_from(row,"rank",0),
            is_released="1" if str(row.get("release", 0)) == "1" else "0",
            stats_json=sql_json(stats), skills_json=sql_json(skills), source_json=source_json)
    elif key == "db_hero_skill.json":
        table, pk = "skill_definitions", ("source_key",)
        raw_key = row.get("id",row.get("skill_id", row.get("key", index)))
        values = dict(source_key=sql_text(str(raw_key)),
            display_name=sql_text(row.get("name", row.get("title"))),
            hero_id=integer_from(row,"hero_id"), source_json=source_json)
    elif key in ("db_item_equipment.json","db_item_consumable.json","db_item_manh.json","db_item_baovat.json"):
        table, pk = "item_definitions", ("kind","id")
        kind={"db_item_equipment.json":"equipment","db_item_consumable.json":"consumable",
              "db_item_manh.json":"shard","db_item_baovat.json":"artifact"}[key]
        values=dict(kind=sql_text(kind),id=sql_number(identity(row,key)),
            display_name=sql_text(row.get("name","")),quality=integer_from(row,"quality"),
            source_json=source_json)
    elif key == "db_pet.json":
        table, pk = "pet_definitions", ("id",)
        values=dict(id=sql_number(identity(row,key)),display_name=sql_text(row.get("name","")),
            released="1" if str(row.get("released",0))=="1" else "0",source_json=source_json)
    elif key == "db_quai.json":
        table, pk = "monster_definitions", ("id",)
        values=dict(id=sql_number(identity(row,key)),hero_link=integer_from(row,"link"),
            monster_level=integer_from(row,"level"), source_json=source_json)
    elif key == "db_battle_heroes.json":
        table, pk = "battle_hero_definitions", ("source_key",)
        values=dict(source_key=sql_text(str(row.get("key", index))),
            display_name=sql_text(row.get("name")),source_json=source_json)
    elif key in ("db_vuotai.json","db_tower.json","db_tower2.json"):
        table, pk = "stage_definitions", ("mode","id")
        mode={"db_vuotai.json":"campaign","db_tower.json":"tower",
              "db_tower2.json":"tower2"}[key]
        values=dict(mode=sql_text(mode),id=sql_number(identity(row,key)),
            stage_label=sql_text(row.get("stage")),
            recommended_power=integer_from(row,"power"),source_json=source_json)
    elif key == "db_home_quest.json":
        table,pk = "quest_definitions",("id",)
        values=dict(id=sql_number(identity(row,key)),quest_type=integer_from(row,"quest_type"),
            display_title=sql_text(row.get("title")),source_json=source_json)
    elif key == "db_hero_upgrade.json":
        table,pk="hero_upgrade_rules",("level",)
        values=dict(level=integer_from(row,"level",0),gold_required=integer_from(row,"gold",0),
            spirit_required=integer_from(row,"spirit",0),source_json=source_json)
    elif key == "db_hero_fusion.json":
        table,pk="hero_fusion_rules",("id",)
        values=dict(id=sql_number(identity(row,key)),output_hero_id=integer_from(row,"hero_id",0),
            source_json=source_json)
    elif key in ("db_pet_level_upgrade.json","db_pet_passive_upgrade.json","db_pet_ruin_upgrade.json"):
        table,pk="pet_upgrade_rules",("rule_group","level")
        values=dict(rule_group=sql_text(key.removesuffix(".json")),
            level=integer_from(row,"level",0),source_json=source_json)
    elif key == "db_vip.json":
        table,pk="vip_rules",("level",)
        values=dict(level=integer_from(row,"level",0),
            points_required=integer_from(row,"point",0),source_json=source_json)
    elif key == "db_player_exp.json":
        table,pk="player_exp_rules",("level",)
        values=dict(level=integer_from(row,"level",0),
            exp_required=integer_from(row,"exp",0),source_json=source_json)
    elif key == "db_guild_boss.json":
        table,pk="guild_boss_rules",("id",)
        values=dict(id=sql_number(identity(row,key)),source_json=source_json)
    elif key == "db_tuong_link.json":
        table,pk="hero_tier_links",("id",)
        values=dict(id=sql_number(identity(row,key)),
            hero_4star_id=integer_from(row,"id_4star"),hero_5star_id=integer_from(row,"id_5star"),
            hero_6star_id=integer_from(row,"id_6star"),hero_10star_id=integer_from(row,"id_10star"),
            source_json=source_json)
    elif key == "db_hero_stone.json":
        table,pk="hero_stone_definitions",("id",)
        values=dict(id=sql_number(identity(row,key)),
            display_name=sql_text(row.get("name")),source_json=source_json)
    elif key == "db_equipment_set.txt":
        table,pk="equipment_set_definitions",("id",)
        if "id" not in row: return ""
        values=dict(id=sql_number(identity(row,key)),
            display_name=sql_text(row.get("name")),source_json=source_json)
    if not table:
        return ""
    return upsert(table, values, pk)


def load_source(path: Path) -> tuple[str, bool, list[tuple[str | None, object]]]:
    # Retain U+FEFF BOM in raw_text for exact UTF-8 byte reconstruction.
    # Remove only for JSON parsing; SHA-256 still covers original file bytes.
    raw = path.read_text(encoding="utf-8")
    try:
        decoded = json.loads(raw.removeprefix("\ufeff"))
        return raw, True, rows_from_root(decoded)
    except json.JSONDecodeError as exc:
        if path.suffix == ".json":
            raise ImportErrorDetail(f"{path}: invalid JSON ({exc})") from exc
        # Unknown .txt configuration is retained verbatim, never guessed.
        return raw, False, [("raw_text", raw)]


def sql_for_file(path: Path, locale: str, primary: bool = False) -> tuple[str, int, int, bool]:
    raw, parsed, rows = load_source(path)
    digest = hashlib.sha256(path.read_bytes()).hexdigest()
    header = (
        "START TRANSACTION;\n"
        "INSERT INTO catalog_files(locale,file_name,source_sha256,raw_text,parsed_json,record_count) VALUES("
        + ",".join((sql_text(locale),sql_text(path.name),sql_text(digest),
                    sql_text(raw), "1" if parsed else "0", str(len(rows))))
        + ") ON DUPLICATE KEY UPDATE source_sha256=VALUES(source_sha256),"
          "raw_text=VALUES(raw_text),parsed_json=VALUES(parsed_json),"
          "record_count=VALUES(record_count),imported_at=CURRENT_TIMESTAMP(6);\n"
        "SET @htdc_file_id = (SELECT id FROM catalog_files WHERE locale="
        + sql_text(locale) + " AND file_name=" + sql_text(path.name) + ");\n"
        "DELETE FROM catalog_records WHERE file_id=@htdc_file_id;\n"
    )
    lines = [header]
    count_typed = 0
    for offset, (record_key, data) in enumerate(rows):
        if len(record_key or "") > MAX_KEY_LENGTH:
            record_key = (record_key or "")[:MAX_KEY_LENGTH]
        lines.append("INSERT INTO catalog_records(file_id,row_index,record_key,payload) VALUES("
            + ",".join(("@htdc_file_id",str(offset),sql_text(record_key),sql_json(data))) + ");\n")
        if primary and parsed and isinstance(data,dict):
            typed = materialize(path.name,data,offset)
            if typed:
                lines.append(typed)
                count_typed += 1
    lines.append("COMMIT;\n")
    return "".join(lines),len(rows),count_typed,parsed


def export(repo: Path, dest: Path, validate_only: bool) -> dict:
    base = repo / SOURCE_PARENT
    if not base.is_dir():
        raise ImportErrorDetail("Missing source data: "+str(base))
    files: list[tuple[str,Path]] = []
    for folder,locale in LOCALES.items():
        src = base / folder
        if not src.is_dir():
            raise ImportErrorDetail("Missing language directory: "+str(src))
        for file in sorted(src.iterdir()):
            if file.is_file() and file.suffix.lower() in (".json",".txt"):
                files.append((locale,file))
    if len(files) < 53:
        raise ImportErrorDetail(f"Only {len(files)} config files; expected >=53 from repo audit")
    dest.parent.mkdir(parents=True,exist_ok=True) if not validate_only else None
    out = None if validate_only else dest.open("w",encoding="utf-8",newline="\n")
    stats = Counter()
    sources = []
    try:
        if out:
            out.write("-- Generated by database/mysql/import_configs.py. DO NOT HAND EDIT.\n"
                      "-- Source UTF-8 bytes retained in catalog_files, rows in catalog_records.\n"
                      "-- MySQL 8.0.36+ / 8.4. Apply 001_schema.sql first.\n"
                      "USE htdc_game;\nSET NAMES utf8mb4;\n"
                      "SET SESSION time_zone='+00:00';\n")
        for locale,path in files:
            fragment,count,typed,parsed=sql_for_file(path,locale,locale=="vi")
            if out:
                out.write(fragment)
            stats["files"]+=1
            stats["records"]+=count
            stats["typed_records"]+=typed
            stats["raw_text_files"]+=0 if parsed else 1
            stats[f"files_{locale}"]+=1
            sources.append({"locale":locale,"file":path.name,"records":count,
                            "typed_records":typed,"parsed_json":parsed,
                            "sha256":hashlib.sha256(path.read_bytes()).hexdigest()})
    finally:
        if out:
            out.close()
    report = {"stats":dict(stats),"sources":sources}
    if not validate_only:
        report_path=dest.with_suffix(".audit.json")
        report_path.write_text(json.dumps(report,ensure_ascii=False,indent=2)+"\n",encoding="utf-8")
    return report


def main() -> int:
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--repo-root",type=Path,default=Path(__file__).resolve().parents[2])
    parser.add_argument("--output",type=Path,
        default=Path("database/mysql/generated/002_game_config_seed.sql"))
    parser.add_argument("--validate-only",action="store_true")
    args=parser.parse_args()
    dest=args.output if args.output.is_absolute() else args.repo_root / args.output
    try:
        report=export(args.repo_root,dest,args.validate_only)
    except (OSError,ImportErrorDetail,UnicodeError,ValueError) as exc:
        print("HTDC CONFIG IMPORT FAILED: "+str(exc),file=sys.stderr)
        return 1
    print(json.dumps(report["stats"],ensure_ascii=False,sort_keys=True))
    if not args.validate_only:
        print("SQL: "+str(dest))
        print("Audit: "+str(dest.with_suffix(".audit.json")))
    return 0


if __name__=="__main__":
    raise SystemExit(main())
