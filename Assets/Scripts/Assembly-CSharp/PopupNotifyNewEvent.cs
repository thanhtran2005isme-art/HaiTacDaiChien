using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupNotifyNewEvent : BasePopup
{
	public Transform m_TransformLoot;

	public GameObject m_GoScrollView;

	public Image imgBgr;

	public Action m_CallBackShowPopUpNotify;

	protected override void Start()
	{
	}

	public void ButtonGoTo()
	{
	}

	public void LoadData(ShopNotification notifi)
	{
	}

	public static PopupNotifyNewEvent ShowUp(ShopNotification notifi)
	{
		return null;
	}

	public override void ClosePopup()
	{
	}

	public override void OnDestroy()
	{
	}
}
