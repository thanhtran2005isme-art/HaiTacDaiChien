using System.Collections.Generic;

public class HeroBattleEffect
{
	public int m_TeamId;

	public int m_SlotIndex;

	public int m_EffectId;

	public List<long> m_EffectData;

	public const int EFFECT_ALTER_HP = 3;

	public const int EFFECT_ALTER_RAGE = 5;

	public const int EFFECT_ALTER_ATTACK = 1;

	public const int EFFECT_ALTER_ARMOR = 2;

	public const int EFFECT_ALTER_SPEED = 4;

	public const int EFFECT_ALTER_SKILL_DMG = 6;

	public const int EFFECT_ALTER_PRECISION = 7;

	public const int EFFECT_ALTER_BLOCK = 8;

	public const int EFFECT_ALTER_CRIT = 9;

	public const int EFFECT_ALTER_CRIT_DMG = 10;

	public const int EFFECT_ALTER_ARMOR_BREAK = 11;

	public const int EFFECT_ALTER_CONTROL_IMMUNE = 12;

	public const int EFFECT_ALTER_REDUCE_DMG = 13;

	public const int EFFECT_ALTER_HOLY_DMG = 14;

	public const int EFFECT_STUN = 50;

	public const int EFFECT_SILENCE = 51;

	public const int EFFECT_PETRIFY = 60;

	public const int EFFECT_FREEZE = 62;

	public const int EFFECT_DOT_POISON = 54;

	public const int EFFECT_DOT_BLEED = 53;

	public const int EFFECT_DOT_BURN = 52;

	public const int EFFECT_ROUND_MARK = 58;

	public const int EFFECT_LIGHTING_MARK_SKEREI = 59;

	public const int EFFECT_CRIT_STRIKE_MARK = 63;

	public const int EFFECT_REVINE = 61;

	public const int EFFECT_TAKE_MORE_DMG_MARK = 64;

	public const int EFFECT_CONTINUOUS_HEALING = 65;

	public const int EFFECT_WEAKEN = 66;

	public const int EFFECT_LUBU_MARK = 71;

	public const int EFFECT_FEAR = 72;

	public const int EFFECT_DISABLED = 73;

	public const int EFFECT_ENERGY_SEAL = 75;

	public const int EFFECT_GIAI_PHAU = 76;

	public const int EFFECT_SUY_NHUOC = 77;

	public const int EFFECT_MIEN_NHIEM = 79;

	public const int EFFECT_FORCE_SHIELD = 83;

	public const int EFFECT_THOI_MIEN = 85;

	public const int EFFECT_DEADLY_FLOWER = 87;

	public const int EFFECT_COUNTER_SHIELD = 78;

	public const int EFFECT_CAM_LO = 102;

	public const int EFFECT_LOAN_KHONG = 103;

	public const int EFFECT_ROUND_MARK2 = 104;

	public const int EFFECT_THEO_DOI = 108;

	public const int EFFECT_THOI_MIEN2 = 109;

	public const int EFFECT_KIN_EMON1 = 110;

	public const int EFFECT_KIN_EMON2 = 111;

	public const int EFFECT_ROUND_MARK16 = 112;

	public const int EFFECT_DICH_BENH = 113;

	public const int EFFECT_MIEN_NHIEM2 = 114;

	public const int EFFECT_DEADLY_FLOWER2 = 117;

	public const int EFFECT_ENERGY_SEAL5 = 118;

	public const int EFFECT_LOAN_KHONG2 = 119;

	public const int EFFECT_ICE_MARK = 126;

	public const int EFFECT_DEAL_DMG_SHIELD = 156;

	public const int EFFECT_MAX_DMG_HP = 157;

	public const int EFFECT_TUYEN_MARK = 200;

	public const int EFFECT_DOT_TRUE_HP = 161;

	public const int EFFECT_HY_VONG = 202;

	public const int EFFECT_ME_HOAC = 201;

	public const int EFFECT_ENERGY_SEAL9 = 203;

	public const int EFFECT_SHIELD_MAXHP = 205;

	public const int EFFECT_BUFF_MAXHP = 204;

	public const int EFFECT_DIRECT_DOT_BURN = 67;

	public const int EFFECT_DIRECT_DOT_BLEED = 68;

	public const int EFFECT_DIRECT_DOT_POISON = 69;

	public const int EFFECT_IMMUNE_DAMAGE = 70;

	public const int EFFECT_BLOCK_ATTACK = 100;

	public const int EFFECT_KILL_STREAK = 84;

	public const int EFFECT_CAM_LO2 = 120;

	public const int EFFECT_KHIEN_TU_TRUONG = 121;

	public const int EFFECT_KHIEN_PICA_GEM_BOSS = 123;

	public const int EFFECT_142 = 142;

	public const int EFFECT_122 = 122;

	public const int EFFECT_124 = 124;

	public const int EFFECT_207 = 207;

	public const int BLACK_BURN = 211;

	public const int SHIELD_BY_ATTACK = 213;

	public const int EFFECT_BLOCK_REVIVE = 216;

	public const int EFFECT_PUDDING_MARK = 217;

	public const int EFFECT_SHARE_DMG = 218;

	public const int EFFECT_230 = 230;

	public const int EFFECT_540_0_STACK = 228;

	public const int EFFECT_540_1_STACK = 1228;

	public const int EFFECT_540_2_STACK = 2228;

	public const int EFFECT_540_3_STACK = 3228;

	public const int EFFECT_540_4_STACK = 4228;

	public const int EFFECT_540_5_STACK = 5228;

	public const int EFFECT_236 = 236;

	public const int EFFECT_237 = 237;

	public const int EFFECT_238 = 238;

	public const int EFFECT_222 = 222;

	public const int EFFECT_239 = 239;

	public const int EFFECT_241 = 241;

	public const int EFFECT_244 = 244;

	public const int EFFECT_245 = 245;

	public const int EFFECT_498_0_STACK = 1246;

	public const int EFFECT_498_1_STACK = 2246;

	public const int EFFECT_498_2_STACK = 3246;

	public const int EFFECT_247 = 247;

	public const int EFFECT_248 = 248;

	public const int EFFECT_249 = 249;

	public const int EFFECT_255 = 255;

	public const int EFFECT_256 = 256;

	public const int EFFECT_257 = 257;

	public const int EFFECT_258 = 258;

	public const int EFFECT_251 = 251;

	public const int EFFECT_253 = 253;

	public const int EFFECT_254 = 254;

	public const int EFFECT_1254 = 1254;

	public const int EFFECT_2254 = 2254;

	public const int EFFECT_259 = 259;

	public const int EFFECT_1262 = 1262;

	public const int EFFECT_2262 = 2262;

	public const int EFFECT_3262 = 3262;

	public const int EFFECT_263 = 263;

	public const int EFFECT_265 = 265;

	public const int EFFECT_267 = 267;

	public const int EFFECT_269 = 269;

	public const int EFFECT_270 = 270;

	public const int EFFECT_272 = 272;

	public const int EFFECT_273 = 273;

	public const int EFFECT_276 = 276;

	public const int EFFECT_277 = 277;

	public const int EFFECT_279 = 279;

	public const int EFFECT_281 = 281;

	public const int EFFECT_283 = 283;

	public const int EFFECT_1283 = 1283;

	public const int EFFECT_2283 = 2283;

	public const int EFFECT_3283 = 3283;

	public const int EFFECT_284 = 284;

	public const int EFFECT_285 = 285;

	public static string getStrEffect(int id)
	{
		return null;
	}

	private static int[] GetClientCoupleBuffEffectId(int absEffectId)
	{
		return null;
	}

	public static bool IsHeroBuffEffect(int clientEffectId)
	{
		return false;
	}

	public static BattleEffectData GenBattleEffectData(HeroBattleEffect battleEffect, bool debug = false)
	{
		return null;
	}
}
