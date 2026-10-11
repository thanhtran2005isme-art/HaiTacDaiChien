using System.Collections.Generic;

public class PlayerGuildWarRegister
{
	public int m_Index;

	public int m_PlayerId;

	public string m_Playername;

	public int m_AvatarId;

	public int m_TypeAvatar;

	public int m_Level;

	public int m_Power;

	public List<HeroModel> m_LstHeroModel;

	public PetModel m_PetModel;

	public bool m_SelectForWar;

	public PlayerGuildWarRegister(int playerId, string playername, int level, int typeAvt, int avatarId, int power, List<HeroModel> lstModel)
	{
	}
}
