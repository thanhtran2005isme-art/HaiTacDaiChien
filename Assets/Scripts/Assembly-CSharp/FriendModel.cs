using System.Collections.Generic;
using pbdson;

public class FriendModel
{
	public const int STT_NO_RECEIVE = 0;

	public const int STT_HAVE_RECEIVE = 1;

	public const int STT_RECEIVED = 2;

	public const int STT_DISABLE_GIVE = 0;

	public const int STT_ENABLE_GIVE = 1;

	public const int STT_GAVE = 2;

	public PbUser userInfo;

	public int receiveHeartStt;

	public int giveHeartStatus;

	public int limitLevelAtkBoss;

	public List<HeroModel> listHero;

	public List<PetModel> lstPetModel;

	public BossObject boss;

	public long timeLastLogin;

	public int power;

	public FriendModel(PbUser pbUser)
	{
	}

	public bool CheckHaveTeam()
	{
		return false;
	}

	public FriendModel ShallowCopy()
	{
		return null;
	}
}
