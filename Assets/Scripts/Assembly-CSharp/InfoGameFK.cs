using System.Collections.Generic;

public class InfoGameFK
{
	public int m_LevelState;

	public int m_LevelTower;

	public int m_LevelAtk;

	public int m_LevelHp;

	public int m_LevelHand;

	public int m_IdBoss;

	public int m_HeroSelected;

	public int m_CurrState;

	public List<int> m_ListHeroUnlock;

	public GameState gameState;

	public int m_IdBgrBattle;

	public List<BossFk> m_LstBossStage;

	public List<HeroFkModel> dataHeroFk;

	private static InfoGameFK m_Instance;

	public int LevelPlayer
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int LevelAtk
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int LevelHp
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int LevelHand
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public string StateTower
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int GoldPlayer
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int GemPlayer
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int ChipPlayer
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public string ListHeroUnlock
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int IDBoss
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int TimeKill
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int TimeTap
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int KillQuestCompleted
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int TapQuestCompleted
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public static InfoGameFK GetIntance()
	{
		return null;
	}

	private InfoGameFK()
	{
	}

	public int GetCurrentBossIndex()
	{
		return 0;
	}

	public void SaveCurrentBossIndex(int bossIdx)
	{
	}

	private void LoadDb()
	{
	}

	public int checkRewardBoss(int level)
	{
		return 0;
	}

	public void setRewardBoss(int level)
	{
	}

	public void GetInfoPlayer()
	{
	}

	public void SaveData()
	{
	}
}
