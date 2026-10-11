using UnityEngine.UI;

public class PanelEventXmaxGift : SpEventPanelBase
{
	public Image m_ImgBgr;

	private string m_UrlEventDetail;

	private XmaxGiftData gameEvent;

	public override void LoadEventDetail(string url)
	{
	}

	private void ReloadEventDetail()
	{
	}

	public void SetData(XmaxGiftData data)
	{
	}

	public void HelpClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public XmaxGiftData ParseData(string json)
	{
		return null;
	}
}
