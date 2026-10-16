using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupHeroEquipmentInfo : BasePopup
{
	public ItemEquipmentUI m_EquipmentUI;

	public Text m_TextTitle;

	public Text m_TextDesc;

	public Text m_TextSet;

	public RectTransform m_TransformImageBgr;

	public GameObject m_GoGroupSet;

	public List<GameObject> m_LstGoEquipmentSet;

	public List<Text> m_LstTextSetEffect;

	private ItemEquipment m_ItemEquipment;

	private int m_HeroId;

	protected override void Start()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonRemoveClick()
	{
	}

	public void ButtonReplaceClick()
	{
	}

	public void ShowItemData(ItemEquipment item, int heroId)
	{
	}

	public static PopupHeroEquipmentInfo ShowUp(ItemEquipment item, int heroId)
	{
		return null;
	}

	private void Update()
	{
	}
}
