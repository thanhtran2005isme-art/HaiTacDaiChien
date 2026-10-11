using UnityEngine.UI;

public class PanelGiftCode : MonoEventHandler
{
	public InputField inputGiftCode;

	public Image imageBanner;

	private GiftCodeEventData giftCodeEvent;

	private const int EVENT_GIFT_CODE = 2000;

	protected override void Awake()
	{
	}

	public void LoadEvent(GiftCodeEventData gameEvent)
	{
	}

	public void OnButtonActionClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
