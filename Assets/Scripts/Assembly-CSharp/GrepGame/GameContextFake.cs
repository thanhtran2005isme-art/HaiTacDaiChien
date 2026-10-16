namespace GrepGame
{
	public class GameContextFake
	{
		private static GameContextFake m_Instance;

		public long m_Diamond;

		public long m_Gold;

		public int m_CurrentHomeStage;

		public long m_CurrentHeroHp;

		public long m_CurrentHeroAttack;

		public int m_TurnAttack;

		private int m_LevelHero;

		private int m_StepGoldUpgrade;

		private int m_StepHpUpgrade;

		private int m_StepAttackUpgrade;

		public int m_CurrentTurnDice;

		public static GameContextFake GetInstance()
		{
			return null;
		}

		public int GetCurrentLevelHero()
		{
			return 0;
		}

		public void SetCurrentLevelHero(int newLevel)
		{
		}

		public int GetGoldEquiToNextLevel(int currentLevel)
		{
			return 0;
		}

		public int GetHpAddToNextLevel()
		{
			return 0;
		}

		public int GetAtkAddToNextLevel()
		{
			return 0;
		}

		public void SaveData()
		{
		}

		public void ResetData()
		{
		}

		public void GetData()
		{
		}
	}
}
