using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemAvatarUI : MonoBehaviour
{
	public Image m_ImageAvatar;

	public ItemAvatar m_ItemAvt;

	private Action<ItemAvatar> m_ClickCallback;

	private void Start()
	{
	}

	public void SetItemAvatar(ItemAvatar itemAvatar)
	{
	}

	public void SetClickCallback(Action<ItemAvatar> callback)
	{
	}

	public void OnItemClick()
	{
	}
}
