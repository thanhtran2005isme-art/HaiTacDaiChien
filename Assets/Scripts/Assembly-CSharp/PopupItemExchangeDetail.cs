using UnityEngine;
using UnityEngine.UI;

public class PopupItemExchangeDetail : MonoEventHandler
{
	[SerializeField]
	private GameItem m_ItemRewardScript;

	public GameObject m_GoDesc;

	public Text m_TextDesc;

	public Text m_TextCost;

	public Image m_ImageFeat;

	private ItemExchangeReward m_ItemExchange;

	private PanelExchangeItemEvent m_PanelEvent;

	private int m_EventId;

	private new void Start()
	{
	}

	public void buttonCloseClick()
	{
	}

	public void buttonExchangeCLick()
	{
	}

	private void fillData(int eventId, PanelExchangeItemEvent panelEvent, ItemExchangeReward itemExchange)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static void ShowYourSelf(int eventId, PanelExchangeItemEvent panelEvent, ItemExchangeReward itemExchange)
	{
	}
}
