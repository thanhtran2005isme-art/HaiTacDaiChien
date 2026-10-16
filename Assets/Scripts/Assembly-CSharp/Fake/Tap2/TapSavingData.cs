namespace Fake.Tap2
{
	public class TapSavingData
	{
		private static TapSavingData _instance;

		private const string KEY_PLAYER_ATTACK = "TAP2_ATTACK";

		private const string KEY_PLAYER_HP = "TAP2_HP";

		private const string KEY_PLAYER_HEALING = "TAP2_HEALING";

		private const string KEY_PLAYER_STAGE = "TAP2_STAGE";

		private const string KEY_SIDE_KICK_LEVEL = "TAP2_SIDE_KICK_LV";

		private const string KEY_PLAYER_GEM = "TAP2_GEM";

		private const string KEY_PLAYER_GOLD = "TAP2_GOLD";

		private const string KEY_PLAYER_CHIP = "TAP2_CHIP";

		private const string KEY_SOUND = "TAP2_SOUND";

		private const string KEY_MUSIC = "TAP2_MUIC";

		private const string KEY_TUTORIAL_FINISH = "TAP2_TUT_FINISH";

		public static TapSavingData Instance => null;

		private TapSavingData()
		{
		}

		public void ResetGame()
		{
		}

		public void SaveTutorialFinishStatus(bool finised)
		{
		}

		public bool IsTutorialFinished()
		{
			return false;
		}

		public void SaveSoundSetting(bool soundOn)
		{
		}

		public bool GetSoundSetting()
		{
			return false;
		}

		public void SaveMusicSetting(bool soundOn)
		{
		}

		public bool GetMusicSetting()
		{
			return false;
		}

		public void SavePlayerGold(int gold)
		{
		}

		public int GetPlayerGold()
		{
			return 0;
		}

		public void SavePlayerGem(int gem)
		{
		}

		public int GetPlayerGem()
		{
			return 0;
		}

		public void SavePlayerChip(int chip)
		{
		}

		public int GetPlayerChip()
		{
			return 0;
		}

		public void SaveCurrentStage(int stageId)
		{
		}

		public int GetCurrentStage()
		{
			return 0;
		}

		public void SaveAttackLevel(int lv)
		{
		}

		public int GetAttackLevel()
		{
			return 0;
		}

		public void SaveHpLevel(int lv)
		{
		}

		public int GetHpLevel()
		{
			return 0;
		}

		public void SaveHealingLevel(int lv)
		{
		}

		public int GetHealingLevel()
		{
			return 0;
		}

		public int GetSidekickLevel(int sidekickId)
		{
			return 0;
		}

		public void SaveSidekickLevel(int sidekickId, int lv)
		{
		}
	}
}
