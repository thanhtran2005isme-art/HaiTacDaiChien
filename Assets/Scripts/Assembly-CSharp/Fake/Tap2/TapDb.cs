using System.Collections.Generic;

namespace Fake.Tap2
{
	public class TapDb
	{
		private static TapDb _instance;

		public const int BOOST_COST = 50;

		public const float BOOST_DURATION = 10f;

		public const float BOOST_DAMAGE_SCALE = 1.5f;

		private const int HERO_BASE_ATK = 10;

		private const int HERO_BASE_HEALING = 10;

		private const int HERO_BASE_HP = 100;

		private const int HERO_COST_UP_ATK = 70;

		private const int HERO_COST_UP_HP = 50;

		private const int HERO_COST_UP_HEALING = 60;

		private List<int> LIST_BGR_ID;

		private List<int> SIDEKICK_BASE_ATK;

		private List<int> SIDEKICK_COST_UPGRADE;

		private List<int> SIDEKICK_COST_UNLOCK;

		private List<float> SIDEKICK_ATK_COOLDOWN;

		private List<int> LIST_ENEMY_SPINE_KEY;

		private const int ENEMY_BASE_ATK = 10;

		private const int ENEMY_BASE_HP = 100;

		private const float ENEMY_ATK_COOLDOWN = 2f;

		private const int BOSS_BASE_ATK = 20;

		private const int BOSS_BASE_HP = 100;

		private const float BOSS_ATK_COOLDOWN = 1.6f;

		public const int HERO_KEY_PLAYER = 193;

		public static List<int> LIST_HERO_KEY_SIDEKICK;

		private int _countIdxEnemy;

		public static TapDb Instance => null;

		public int GetBackgroundId(int stageId)
		{
			return 0;
		}

		public int GetEnemySpineKey(int stageId)
		{
			return 0;
		}

		public int GetHeroAtk(int lv)
		{
			return 0;
		}

		public int GetHeroHp(int lv)
		{
			return 0;
		}

		public int GetHeroHealing(int lv)
		{
			return 0;
		}

		public int GetSidekickAtk(int skId, int lv)
		{
			return 0;
		}

		public float GetSidekickAtkCooldown(int skId)
		{
			return 0f;
		}

		public int GetEnemyHp(int stageId)
		{
			return 0;
		}

		public int GetEnemyAtk(int stageId)
		{
			return 0;
		}

		public float GetEnemyAtkCooldown(int stageId)
		{
			return 0f;
		}

		public int GetBossHp(int stageId)
		{
			return 0;
		}

		public int GetBossAtk(int stageId)
		{
			return 0;
		}

		public float GetBossAtkCooldown(int stageId)
		{
			return 0f;
		}

		public int GetCoinReward(int stageId)
		{
			return 0;
		}

		public int GetChipReward(int stageId)
		{
			return 0;
		}

		public int GetCostUpgradeHeroAttack(int nextLv)
		{
			return 0;
		}

		public int GetCostUpgradeHeroHp(int nextLv)
		{
			return 0;
		}

		public int GetCostUpgradeHeroHealing(int nextLv)
		{
			return 0;
		}

		public int GetCostUnlockSidekick(int sidekickId)
		{
			return 0;
		}

		public int GetCostUpgradeSidekick(int sidekickId, int nextLv)
		{
			return 0;
		}
	}
}
