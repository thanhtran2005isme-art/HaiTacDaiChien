using System.Collections.Generic;
using pbdson;

public class ArenaPlayer
{
	public int m_Rank;

	public int m_TypeAvatar;

	public int m_Avatar;

	public int m_Level;

	public string m_Playername;

	public int m_UserId;

	public int m_Power;

	public string m_ArenaPointCaption;

	public int m_ArenaPoint;

	public string m_GuildName;

	public int m_ServerId;

	public List<HeroModel> m_ListHeroModel;

	public List<PetModel> m_ListPetModel;

	public bool m_AllowShowPlayerLineUp;

	public string m_MessageForNotShowingPlayerLineup;

	public ArenaPlayer(int avatar, int level, int userId, string playername, int power, string guildName = "")
	{
	}

	public ArenaPlayer()
	{
	}

	public ArenaPlayer(PbUser pbUser)
	{
	}

	public void DisableShowingPlayerLineup(string msgForDisable)
	{
	}

	public PlayerInfoData ToPlayerInfoData()
	{
		return null;
	}

	public void ImportFromPbUser(PbUser cvPlayer)
	{
	}
}
