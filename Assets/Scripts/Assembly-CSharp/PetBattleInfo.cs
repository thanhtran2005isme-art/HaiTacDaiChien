using SimpleJSON;

public class PetBattleInfo
{
	public int m_PetId;

	public int m_SpineId;

	public string m_PetName;

	public BaseSkill m_Skill;

	public int m_TeamId;

	public float m_RageMax;

	public float m_Rage;

	public bool m_JumpOutAfterSkill;

	public float m_ForceDuratinSkill;

	public PetBattleInfo(JSONClass jsonObject)
	{
	}

	public PetBattleInfo(int petId, int spineId)
	{
	}

	public PetBattleInfo ShallowClone()
	{
		return null;
	}

	public JSONClass ToJSONObject()
	{
		return null;
	}

	public void ImportFromJSONObject(JSONClass jsonObj)
	{
	}
}
