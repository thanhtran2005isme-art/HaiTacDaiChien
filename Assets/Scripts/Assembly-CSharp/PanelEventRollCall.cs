using UnityEngine;
using UnityEngine.UI;

public class PanelEventRollCall : SpEventPanelBase
{
	public GameObject m_GoBoxTop;

	public GameObject m_GoScrollContainer;

	public Image m_ImgTopBanner;

	public Text m_TextVipExp;

	public Text m_TextButtonAction;

	public Image m_ImageButtonPurchase;

	public VerticalPoolGroup m_PoolItem;

	public GameObject m_GoPrefabCellItem;

	private RollCallEventData m_EventData;

	private string m_UrlEventDetail;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public override void LoadEventDetail(string url)
	{
	}

	private void ReloadEventDetail()
	{
	}

	public void ShowEvent(RollCallEventData evData)
	{
	}

	public void ButtonHelpClick()
	{
	}

	public void ButtonPurchaseClick()
	{
	}

	public override void ProcessKEvent(int serviceId, object data)
	{
	}

	private RollCallEventData ParseEventData(string json)
	{
		return null;
	}
}
