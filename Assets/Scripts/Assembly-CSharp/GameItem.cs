using System;
using UnityEngine;
using UnityEngine.UI;

public class GameItem : MonoBehaviour
{
	public ItemConsumableUI m_ItemConsumableUI;

	public ItemEquipmentUI m_ItemEquipmentUI;

	public ItemShardUI m_ItemShardUI;

	public ItemArtifactUI m_ItemArtifactUI;

	public HeroHeadClickable m_HeroHead;

	public ItemAvatarUI m_AvatarUI;

	public ItemVipExpUI m_VipExpUI;

	public GameObject m_ActionButton;

	public GameObject m_GroupDoneEvent;

	public Text m_TextDesc;

	private object m_GameItem;

	private Action m_ActionCallback;

	public SpineObject m_SpineHighlight;

	private bool m_LoadSpineHighlight;

	private void Awake()
	{
	}

	public void SetActiveGroupDoneEvent(bool isOn)
	{
	}

	public void SetDesc(string desc)
	{
	}

	private void Start()
	{
	}

	public void ShowHighlight()
	{
	}

	public void HideHighlight()
	{
	}

	private void HideAllItemUI()
	{
	}

	public void SetItemInScrollview(object item, bool grayScale = false)
	{
	}

	public void SetItem(object item, bool grayScale = false)
	{
	}

	private Color GetColor(GGColor ggColor)
	{
		return default;
	}

	public void SetTextNumber(string textNumber, GGColor color = GGColor.NONE)
	{
	}

	public void ClearItemClickCallback()
	{
	}

	public void SetActionClick(Action callback)
	{
	}

	public void ActionClick()
	{
	}
}
