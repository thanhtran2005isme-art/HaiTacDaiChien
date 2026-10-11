public class PanelEvent2Package : SpEventPanelBase
{
	public BoxPurchasePackage m_BoxPackage1;

	public BoxPurchasePackage m_BoxPackage2;

	private TwoPackageEventData m_EventData;

	private string m_UrlEventDetail;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private void PurchasePackageSuccess()
	{
	}

	public override void LoadEventDetail(string url)
	{
	}

	private void ReloadEventDetail()
	{
	}

	public void ShowEvent(TwoPackageEventData evData)
	{
	}

	public override void ProcessKEvent(int serviceId, object data)
	{
	}

	private TwoPackageEventData ParseEventData(string json)
	{
		return null;
	}
}
