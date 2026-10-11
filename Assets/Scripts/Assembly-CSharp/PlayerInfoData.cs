using System.Collections.Generic;

public class PlayerInfoData
{
	public string m_PlayerName;

	public string m_GuildName;

	public int m_UserId;

	public List<HeroModel> m_LstHeroModel;

	public List<PetModel> m_LstPetModel;

	public int m_PlayerLevel;

	public int m_PlayerAvatarId;

	public int m_TypeAvatar;

	public int m_Power;

	public PlayerInfoData(int userId, string playerName, int typeAvatar, int avatarId, int playerLevel, string guildName = "", List<HeroModel> lstHeroModel = null, int power = -1)
	{
	}

	public PetModel GetPetModel(int petIndex)
	{
		return null;
	}
}
