using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShardUI : MonoBehaviour
{
	public Image m_ImgItem;

	public GameObject m_GoCheck;

	public Text m_TextNumber;

	public Text m_TextNumber2;

	public Image m_ImgHeroFaction;

	public ItemShard m_ItemShard;

	private Action<ItemShard> m_ClickCallback;

	public List<GameObject> m_LstGoStar;

	public Slider m_SliderNumber;

	private void Start()
	{
	}

	public void SetCheck(bool check)
	{
	}

	public void SetItemShard(ItemShard item, bool showSliderPercent = true)
	{
	}

	public void SetItemGrayScale(bool enable)
	{
	}

	public void SetItemGrayScaleInScrollview(bool enable)
	{
	}

	public void SetClickCallback(Action<ItemShard> callback)
	{
	}

	public void OnItemClick()
	{
	}
}
