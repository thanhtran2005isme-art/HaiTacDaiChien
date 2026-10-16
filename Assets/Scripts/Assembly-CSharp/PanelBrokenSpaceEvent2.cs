using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBrokenSpaceEvent2 : MonoEventHandler
{
	public Image m_BgrPopup;

	public Text m_NumberMaterial;

	public Text m_TimeCountdown;

	public Text m_TimeLabel;

	public Image m_ImgMaterial;

	private GameBrokenSpaceEventNew m_GameEvent;

	private bool isSmash;

	private int exchangeItems;

	private string exchangeUrl;

	private int evenId;

	private string m_textNumberHornBuy;

	private int m_MaxItem;

	public List<GameObject> lstStage;

	public void LoadEventData(GameBrokenSpaceEventNew gameEvent)
	{
	}

	public void BattleClick(StageBrokenSpace data)
	{
	}

	public void OnBattleClick()
	{
	}

	public void HelpClick()
	{
	}

	public void BuyClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private new void Start()
	{
	}

	private void Update()
	{
	}
}
