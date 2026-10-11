using System.Collections.Generic;
using pbdson;

public class ChatModel
{
	public const int TYPE_MSG_NORMAL = 0;

	public const int TYPE_MSG_SHOW_HERO = 1;

	public const int TYPE_MSG_CLAN_RECRUIT = 2;

	public const int TYPE_MSG_SHARE_BATTLE = 3;

	public int index;

	public int clanIcon;

	public string screenName;

	public string message;

	public string nameInfo;

	public long reqTime;

	public int type;

	public int userId;

	public int avatarId;

	public int typeAvatar;

	public int msgId;

	public int result;

	public int level;

	public int vip;

	public string nameClan;

	public int clanId;

	public PbTeamHeroInfo infoHero;

	public List<HeroModel> listTeam;

	public CommonVector otherInfo;

	public ChatModel()
	{
	}

	public ChatModel(ChatMsg chatMsg)
	{
	}
}
