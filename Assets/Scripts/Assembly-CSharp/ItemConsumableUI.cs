using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemConsumableUI : MonoBehaviour
{
	public Image m_ImgItem;

	public GameObject m_GoCheck;

	public Text m_TextNumber;

	public ItemConsumable m_ItemConsumable;

	private Action<ItemConsumable> m_ClickCallback;

	private void Start()
	{
	}

	public void SetCheck(bool check)
	{
	}

	public void SetItemConsumable(ItemConsumable item)
	{
	}

	public void SetItemGrayScale(bool enable)
	{
	}

	public void SetItemGrayScaleInScrollview(bool enable)
	{
	}

	public void SetClickCallback(Action<ItemConsumable> callback)
	{
	}

	public void OnItemClick()
	{
	}
}
