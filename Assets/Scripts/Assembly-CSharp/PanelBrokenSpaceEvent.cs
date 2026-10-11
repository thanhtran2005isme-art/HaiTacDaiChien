using UnityEngine;
using UnityEngine.UI;

public class PanelBrokenSpaceEvent : MonoEventHandler
{
	public GameObject m_BgrPopup;

	public ItemMissionBrokenSpace[] m_Mission;

	public Text m_NumberMaterial;

	public Text m_TimeCountdown;

	public Text m_TimeLabel;

	public Image m_ImgMaterial;

	private GameBrokenSpaceEvent m_GameEvent;

	private bool isSmash;

	private int exchangeItems;

	private string exchangeUrl;

	private int evenId;

	private string m_textNumberHornBuy;

	private int m_MaxItem;

	public void LoadEventData(GameBrokenSpaceEvent gameEvent)
	{
	}

	public void BattleClick(ItemBrokenSpace data)
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

	private void Update()
	{
	}
}
