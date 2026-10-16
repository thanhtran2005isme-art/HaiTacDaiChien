public class GuildMemberInfo
{
	public int m_Rank;

	public string m_PlayerName;

	public GuildRole m_Role;

	public int m_AvatarId;

	public int m_UserId;

	public int m_Level;

	public int m_TypeAvatar;

	public long m_GoldDonate;

	public bool m_AllowKick;

	public bool m_AllowPromote;

	public long m_CountTimeLastAction;

	public GuildMemberInfo(int userId, int typeAvt, int avatarId, string playername, int level = -1, GuildRole role = GuildRole.MEMBER)
	{
	}

	public string GetStrOnlineTime()
	{
		return null;
	}

	public static string GetGuildRoleText(GuildRole guildRole)
	{
		return null;
	}

	public void CalculateActionAllow(GuildRole myRole)
	{
	}
}
