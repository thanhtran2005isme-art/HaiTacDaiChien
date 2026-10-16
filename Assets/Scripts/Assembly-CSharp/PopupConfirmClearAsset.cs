using System.Collections.Generic;
using UnityEngine.UI;

public class PopupConfirmClearAsset : BasePopup
{
	public Text m_TextTitle;

	public Text m_TextMessage;

	private int _countTapMade;

	protected override void Start()
	{
	}

	private void Init()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonClearClick()
	{
	}

	public void ForceToReLogin()
	{
	}

	public void ButtonHiddenClick()
	{
	}

	private void ShowListAsset(List<AssetInfo> lstAssetInfo)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static PopupConfirmClearAsset ShowUp()
	{
		return null;
	}
}
