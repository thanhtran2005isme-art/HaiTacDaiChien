using System.Collections.Generic;

public class GuildWarPlayer
{
	public int m_Index;

	public int m_PlayerId;

	public string m_PlayerName;

	public int m_AvatarId;

	public int m_TypeAvatar;

	public int m_Level;

	public int m_StarLost;

	public int m_ClanId;

	public int m_Power;

	public List<HeroModel> m_LstHeroModel;

	public GuildRole m_Role;

	public WarSide m_Side;

	public List<object> m_LstItemModelReward;

	public PetModel m_PetModel;

	public WarAttackInfo m_InfoAttack1;

	public WarAttackInfo m_InfoAttack2;

	public WarAttackInfo m_InfoDefense;

	public GuildWarPlayer(int index, int playerId, string playerName, int typeAvt, int avtId, int level, GuildRole role)
	{
	}
}
