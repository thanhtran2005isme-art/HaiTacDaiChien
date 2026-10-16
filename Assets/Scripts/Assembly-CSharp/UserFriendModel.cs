using System.Collections.Generic;

public class UserFriendModel
{
	public int limitFriend;

	public int numberStamina;

	public int maxnumberStamina;

	public TimeStruct timeCountdownStamina;

	public long timeRefreshStamina;

	private Dictionary<int, FriendModel> dictFriend;

	public List<int> team;

	public bool haveReqApply;

	public bool haveScout;

	public void AddFriend(FriendModel friend)
	{
	}

	public void RemoveFriend(int friendId)
	{
	}

	public FriendModel GetFiendById(int id)
	{
		return null;
	}

	public List<FriendModel> GetListFriends()
	{
		return null;
	}

	public void ResetHeartAllFriend()
	{
	}

	public bool CheckEnableSendAndReceive()
	{
		return false;
	}

	public int GetNumberFriend()
	{
		return 0;
	}
}
