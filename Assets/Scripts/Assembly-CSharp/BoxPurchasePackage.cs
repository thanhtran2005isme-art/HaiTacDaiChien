using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxPurchasePackage : MonoEventHandler
{
	public Action m_PurchaseSuccessCb;

	public Image m_ImageBanner;

	public Text m_TextVipExp;

	public Text m_TextButtonAction;

	public Image m_ImageButtonPurchase;

	public Text m_TextLimit;

	public RectTransform m_TfItemContainer;

	private List<GameObject> m_LstGoGameItem;

	private PackageDetail m_PackageDetail;

	private float timeRefresh;

	private bool stopUpdateBonus;

	public void SetBoxData(PackageDetail packageDetail)
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
