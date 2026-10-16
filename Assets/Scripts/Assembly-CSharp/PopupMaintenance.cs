using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupMaintenance : BasePopup
{
	public Text m_TextMessage;

	public Text m_TextEndTime;

	public GameObject m_GoButtonGoTo;

	public Action m_CloseCb;

	private string _strUrlInfo;

	protected override void Start()
	{
	}

	public void SetData(string msg, string endTime, string urlInfo)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonGoToClick()
	{
	}

	public static PopupMaintenance ShowUp(string msg, string endTime, string urlInfo)
	{
		return null;
	}

	private void Update()
	{
	}
}
