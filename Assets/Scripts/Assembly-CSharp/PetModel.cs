using System.Collections.Generic;

public class PetModel
{
	public string m_PetName;

	public int m_PetId;

	public bool m_Activated;

	public int m_Level;

	public string m_PathImage;

	public int m_Tier;

	public int m_CostActivate;

	public float mp_Rage;

	public float mp_RageMax;

	public const int MAX_TIER = 4;

	public int m_ActiveSkillId;

	public int m_Skill1Level;

	public int m_Skill2Level;

	public int m_Skill3Level;

	public int m_Skill4Level;

	public int m_Ruin1Level;

	public int m_Ruin2Level;

	public int m_Ruin3Level;

	public List<PetPassiveBuff> m_ListPassive1Buff;

	public List<PetPassiveBuff> m_ListPassive2Buff;

	public List<PetPassiveBuff> m_ListPassive3Buff;

	public List<PetPassiveBuff> m_ListPassive4Buff;

	public PetModel()
	{
	}

	public PetModel(int petId, int tier, int level)
	{
	}

	public void SetRuinLevel(int ruinId, int ruinLevel)
	{
	}

	public bool HasMaxPetSkillBuff()
	{
		return false;
	}

	public void ResetToNewBornState()
	{
	}

	public PetModel(int petId, string name, string pathImage, bool activated, int costActivate)
	{
	}

	public PetBuffDescription GetPassiveBuffDescription(int skillIndex, int skillLevel)
	{
		return null;
	}

	public virtual PetModel Clone()
	{
		return null;
	}
}
