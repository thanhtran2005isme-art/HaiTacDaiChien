using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemArtifactUI : MonoBehaviour
{
	public Image m_ImgBgr;

	public Image m_ImgItem;

	public Image m_ImgBorder;

	public GameObject m_GoCheck;

	public Text m_TextNumber;

	public GameObject m_SpineBorder;

	public List<GameObject> m_ListGoStar;

	public ItemArtifact m_ItemEquipment;

	public Image m_ImgHeroFaction;

	private Action<ItemArtifact> m_ClickCallback;

	private void Start()
	{
	}

	public void SetCheck(bool check)
	{
	}

	public void SetItemArtifact(ItemArtifact item, bool showItemNumber = true)
	{
	}

	private void LoadBorderAndBackground(ArtifactType type)
	{
	}

	public void SetItemGrayScale(bool enable)
	{
	}

	public void SetItemGrayScaleInScrollview(bool enable)
	{
	}

	public void SetClickCallback(Action<ItemArtifact> callback)
	{
	}

	public void OnItemClick()
	{
	}
}
