using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemIAP : MonoBehaviour
{
	public Text txt_Value;

	public Text txt_Price;

	public Text txt_Desc;

	public Text txt_ExpVip;

	public Text txt_BonusMonth;

	public Text txt_TitleMonthly;

	public Text txt_BonusWeek;

	public Text txt_TitleWeek;

	public Text txt_TitleFirsTimes;

	public Image img_Icon;

	public GameObject m_PanelMonth;

	public GameObject m_PanelWeek;

	public GameObject m_InfoButton;

	public GameObject m_FirstTimes;

	public GameObject m_PaneVipExp;

	private Action<ItemIAPModel> itemClickCallback;

	private ItemIAPModel m_iapModel;

	public void InitItem(ItemIAPModel info, Action<ItemIAPModel> itemClickCB = null)
	{
	}

	public void InfoClick()
	{
	}

	public void ItemClick()
	{
	}
}
