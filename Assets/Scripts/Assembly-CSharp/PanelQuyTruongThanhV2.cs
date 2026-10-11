using UnityEngine;
using UnityEngine.UI;

public class PanelQuyTruongThanhV2 : MonoEventHandler
{
	public BoxGameEventInfo m_BoxEventInfo;

	public VerticalPoolGroup m_PoolGroup;

	public Text m_TextAction;

	private bool m_InitPool;

	public GameObject m_ButtonBuy;

	private DataQuyTruongThanhV2 m_GameEvent;

	private float timeRefresh;

	private bool stopUpdateBonus;

	private int m_IndexTabActive;

	private GameGrowthFundEvent eventData;

	public const int QUY_TRUONG_THANH_2_STATUS = 615;

	public void LoadEventData(DataQuyTruongThanhV2 gameEvent, bool ButtonActive = false)
	{
	}

	public void ButtonTabClick(int index)
	{
	}

	private void LoadDataTab()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void BuyPack()
	{
	}

	public void StartUpdateBonus()
	{
	}

	private void Update()
	{
	}
}
