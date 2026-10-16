using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemVipExpUI : MonoBehaviour
{
	public Text m_TextNumber;

	public ItemVipExp m_ItemVipExp;

	private Action<ItemVipExp> m_ClickCallback;

	private void Start()
	{
	}

	public void SetVipExp(ItemVipExp vipExp)
	{
	}

	public void SetClickCallback(Action<ItemVipExp> callback)
	{
	}

	public void OnItemClick()
	{
	}
}
