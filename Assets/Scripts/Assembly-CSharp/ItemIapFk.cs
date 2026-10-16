using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemIapFk : MonoBehaviour
{
	public Text m_TextGem;

	public Text m_TextPrice;

	private IapInfo m_IapInfo;

	public Action<IapInfo> m_PurchaseCb;

	private void Start()
	{
	}

	public void SetIapInfo(IapInfo iapInfo)
	{
	}

	public string GetProductId()
	{
		return null;
	}

	public void SetPrice(string price)
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
