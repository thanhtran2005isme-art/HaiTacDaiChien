using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelEpicEquipmentStrengthen : MonoEventHandler
{
	public GameObject goInfoMax;

	public GameObject goInfoNotMax;

	public Text textLevelCurrent;

	public Text textLevelNext;

	public Image icon;

	public CurvedItem groupStarScript;

	public FiledResourceItem[] arrFee;

	private const int TAB_ATTACK = 0;

	private const int TAB_HP = 1;

	private HeroModel heroModel;

	private int typeRoleRank;

	public Action cbUpgradeSuscess;

	public void SetInfo(bool checkMax)
	{
	}

	public void OnButtonUpgradeClick()
	{
	}

	private void ReloadData()
	{
	}

	public void SetData(HeroModel heroModel, int typeTabRankRole)
	{
	}

	private Sprite LoadImgClass(HeroClass m_class)
	{
		return null;
	}

	private Sprite LoadImgQuality(int quality)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
