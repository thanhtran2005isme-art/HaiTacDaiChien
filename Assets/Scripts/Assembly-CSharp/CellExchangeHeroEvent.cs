using System;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class CellExchangeHeroEvent : MonoBehaviour
{
	public Text m_ActionLabel;

	public Action<ExchangeHeroItem> m_ExchangeCallback;

	public SkeletonGraphic m_SkeletonHero;

	private ExchangeHeroItem m_ItemExchange;

	private string pathSkeletonHero;

	private float widthViewport;

	private float widthElement;

	private ScrollRect m_Scrollview;

	public void ShowData(ExchangeHeroItem itemExchange, ScrollRect scrollview)
	{
	}

	public void ActionClick()
	{
	}

	private void Update()
	{
	}

	private bool CheckVisible()
	{
		return false;
	}
}
