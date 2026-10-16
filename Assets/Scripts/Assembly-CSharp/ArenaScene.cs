using UnityEngine;
using UnityEngine.UI;

public class ArenaScene : BaseScene
{
	public GameObject m_GoButtonJoin;

	public GameObject m_GoButtonTopLastSeason;

	public GameObject m_GroupTime;

	public Text m_TextTimeCapCrystal;

	public Text m_TextTimeCapTrial;

	public Text m_TextTimeCapAllStars;

	public Text m_TextTimeCrystal;

	public Text m_TextTimeTrial;

	public Text m_TextTimeAllStars;

	public Text m_TextDescCrystal;

	public Text m_TextDescTrial;

	public Text m_TextDescAllStars;

	public GameObject m_NotifyLeague;

	public GameObject m_NotifyChampion;

	public GameObject m_GoNotifyShop;

	private int m_CountdownCrystal;

	private int m_CountdownTrial;

	private int m_StatusTrial;

	private int m_StatusAllStars;

	private int m_CountdownAllStars;

	private ActionRepeatTimer m_TimerCountdown;

	private bool m_DataLoaded;

	public Transform m_PointBtnBuy;

	private GameObject m_GoHandSelect;

	public Button m_BtnArena;

	protected override void Start()
	{
	}

	protected override void ReciveTutorial(TutorialAction tutAct)
	{
	}

	public void OnButtonDonDauClick()
	{
	}

	public void OnButtonLienDauClick()
	{
	}

	public void OnButtonVoDauClick()
	{
	}

	public void ShowArenaStatus()
	{
	}

	public void ButtonBackClick()
	{
	}

	public void ButtonTopLastCrystalClick()
	{
	}

	public void ButtonTopLastTrialClick()
	{
	}

	public void ButtonShopClick()
	{
	}

	public void ButtonShopAllStarsClick()
	{
	}

	public static void ShowUp()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
