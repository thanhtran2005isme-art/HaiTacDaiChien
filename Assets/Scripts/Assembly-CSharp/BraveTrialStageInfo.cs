using System.Collections.Generic;

public class BraveTrialStageInfo
{
	public int m_StageId;

	public int m_Avatar;

	public int m_TypeAvatar;

	public string m_PlayerName;

	public int m_PlayerLevel;

	public string m_GuildName;

	public List<HeroModel> m_LstHeroModel;

	public BonusModel m_BonusModel;

	public bool m_Enable;

	public int m_Power;

	public int m_PetId;

	public BraveTrialStageInfo(int stageId, int typeAvatar, int avatarId, string playerName, string guildName, int power, BonusModel bm, List<HeroModel> lstHeroModel, bool enable)
	{
	}
}
