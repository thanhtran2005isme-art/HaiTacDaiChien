using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupStoreAndBenefit : MonoEventHandler
{
	public Text percentExpVIP;

	public Text txt_CurrVIP;

	public Text txt_NextVIP;

	public Text numberGemReq;

	public Text txt_Action;

	public Image imgFillExpVIP;

	public PanelStore panelStore;

	public PanelBenefit panelBenefit;

	public GameObject m_GroupReq;

	public GameObject m_GroupMax;

	private string currAction;

	public Action m_CloseCb;

	public void initFirstUI(string actionTarget)
	{
	}

	public void ActionClick()
	{
	}

	public void UpdateInfoVIP()
	{
	}

	public void CloseClick()
	{
	}

	private new void Start()
	{
	}

	private void Update()
	{
	}

	public static PopupStoreAndBenefit ShowUp(string actionTarget)
	{
		return null;
	}
}
