using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquipmentUI : MonoBehaviour
{
	public Image m_ImgBgr;

	public Image m_ImgItem;

	public Image m_ImgBorder;

	public GameObject m_GoCheck;

	public GameObject m_SpineBoder;

	public Text m_TextNumber;

	public List<GameObject> m_ListGoStar;

	public ItemEquipment m_ItemEquipment;

	private Action<ItemEquipment> m_ClickCallback;

	private GameObject m_GoIconClass;

	private void Start()
	{
	}

	public void SetCheck(bool check)
	{
	}

	public void SetEmpty()
	{
	}

	public void SetItemEquipment(ItemEquipment item, bool showItemNumber = true)
	{
	}

	private void LoadItemClazzIcon(HeroClass clazz)
	{
	}

	public void SetTextNumberCorlor(string textNumbCorlor)
	{
	}

	private void LoadBorderAndBackground(EquipmentQuality quality)
	{
	}

	public void SetItemGrayScale(bool enableGray)
	{
	}

	public void SetItemGrayScaleInScrollview(bool enableGray)
	{
	}

	public void SetClickCallback(Action<ItemEquipment> callback)
	{
	}

	public void OnItemClick()
	{
	}
}
