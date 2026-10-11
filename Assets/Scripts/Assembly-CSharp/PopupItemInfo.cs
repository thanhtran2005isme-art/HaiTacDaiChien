using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupItemInfo : BasePopup
{
	public GameItem m_GameItem;

	public Text m_TextTitle;

	public Text m_TextInfo;

	public Text m_TextDesc;

	private static GameObject m_GoPrefab;

	public RectTransform m_TransformImageBgr;

	public GameObject m_GoGroupEquipmentSet;

	public Text m_TextEquipmentSet;

	public List<Text> m_LstTextEquipmentSetEffect;

	public GameObject m_GoButtonAction1;

	public GameObject m_GoButtonAction2;

	public GameObject m_PanelButton;

	public Text m_TextAction1;

	public Text m_TextAction2;

	public GameObject m_PanelDrop;

	public Transform m_LayoutElementPosition;

	public GameObject m_ItemPositon;

	public GameObject m_ButtonDrop;

	private ItemModel m_ItemData;

	private bool m_ClosedAfterAction;

	private Action m_Action1Cb;

	private Action m_Action2Cb;

	private Action m_ActionUpgradeCb;

	public GameObject m_GoButtonUpgrade;

	public GameObject m_GoButtonPreview;

	private HeroFaction m_HeroFactionEquipItem;

	private HeroClass m_HeroClassEquipItem;

	protected override void Start()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void LoadHeroModel(HeroModel heroModel, bool hideAction = true)
	{
	}

	public void LoadItem(ItemEquipment item, bool hideAction = true)
	{
	}

	public void LoadItem(ItemConsumable item, bool hideAction = true)
	{
	}

	private string GetRankHeroByQuality(HeroModel hm)
	{
		return null;
	}

	public void LoadItem(ItemShard item, bool hideAction = true)
	{
	}

	public void LoadItem(ItemArtifact item, bool hideAction = true)
	{
	}

	public void LoadItem(ItemAvatar item)
	{
	}

	public void SetUpgradeCallback(Action cb)
	{
	}

	public void ButtonUpgradeClick()
	{
	}

	public void HideActions()
	{
	}

	public void SetAction(string title1 = null, Action action1 = null, string title2 = null, Action action2 = null, bool closedAfterAction = true)
	{
	}

	public void ButtonAction1Click()
	{
	}

	public void ButtonAction2Click()
	{
	}

	public void ButtonPreviewClick()
	{
	}

	public void ButtonInfoDropClick()
	{
	}

	public static PopupItemInfo ShowUp(ItemModel item, HeroFaction faction = HeroFaction.UNKNOWN, HeroClass clazz = HeroClass.UNKNOWN)
	{
		return null;
	}

	public static PopupItemInfo ShowUp(HeroModel heroModel)
	{
		return null;
	}

	public void ReloadItemInfo()
	{
	}

	private void Update()
	{
	}
}
