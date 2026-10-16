using System;
using UnityEngine.UI;

public class PopupUnlockHeroConfirm : BasePopup
{
	public Text m_TextMessage;

	private Action<int> m_HeroUnlockSuccessCb;

	public Text m_TextTitle;

	private int m_HeroIdUnlock;

	protected override void Start()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void SetTitle(string title)
	{
	}

	public void ButtonUnlockClick()
	{
	}

	public void SetData(string message, int heroIdUnlock)
	{
	}

	public void SetUnlockSuccessCallback(Action<int> cb)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static PopupUnlockHeroConfirm ShowUp(string msg, int heroIdUnlock)
	{
		return null;
	}

	private void Update()
	{
	}
}
