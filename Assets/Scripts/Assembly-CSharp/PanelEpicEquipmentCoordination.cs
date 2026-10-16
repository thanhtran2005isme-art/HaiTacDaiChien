using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEpicEquipmentCoordination : MonoEventHandler
{
	public Text currentPercent;

	public Text nextPercent;

	public Image currentIcon;

	public Image nextIcon;

	public CurvedItem currentStar;

	public CurvedItem nextStar;

	public FiledResourceItem[] arrFeeAsset;

	public Transform parentFeeItem;

	private const int TAB_ATTACK = 0;

	private const int TAB_HP = 1;

	private int typeRankRole;

	private HeroModel heroModel;

	private List<GameObject> lstGoItemFee;

	public Action cbUpgradeSusscess;

	private void ReloadData()
	{
	}

	public void SetData(HeroModel heroModel, int typeRankRole)
	{
	}

	public void OnButtonUpgradeClick()
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
