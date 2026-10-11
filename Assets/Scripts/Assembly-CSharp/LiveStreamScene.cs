using UnityEngine;
using UnityEngine.UI;
using pbdson;

public class LiveStreamScene : BaseScene
{
	public GameObject popupSetting;

	public Dropdown mDropTypeBattle;

	public InputField mUser1;

	public InputField mUser2;

	public LiveSteamTeam team1;

	public LiveSteamTeam team2;

	public GameObject mode;

	private static PbUser pUser1;

	private static PbUser pUser2;

	private static int currentMode;

	private static int userID1;

	private static int userID2;

	protected override void Start()
	{
	}

	public void ChangeTeam(int indexTeam)
	{
	}

	public void Fight()
	{
	}

	public void ViewInfor()
	{
	}

	public void ShowSetting(bool show)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
