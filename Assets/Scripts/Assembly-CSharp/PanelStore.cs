using System.Collections.Generic;
using UnityEngine;

public class PanelStore : MonoEventHandler
{
	public Transform panelItemIAP;

	public GameObject itemIAPPreb;

	private List<ItemIAPModel> lstIAPModel;

	private List<ItemIAP> listItemIAP;

	private float timeRefresh;

	private bool stopUpdateBonus;

	private PanelLoadingInstant m_PanelLoading;

	protected override void Start()
	{
	}

	public void InitPanel()
	{
	}

	public void initUI()
	{
	}

	private void itemIAPClick(ItemIAPModel iapModel)
	{
	}

	public void buyIAP(ItemIAPModel iapModel)
	{
	}

	private void CBEventIAPSuccess(string receipt, int itemId)
	{
	}

	private void CBEventIAPFailed(string productId)
	{
	}

	public bool isVnOperator(string sim_name)
	{
		return false;
	}

	public void StartUpdateBonus()
	{
	}

	public bool checkConditionPurchase()
	{
		return false;
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
