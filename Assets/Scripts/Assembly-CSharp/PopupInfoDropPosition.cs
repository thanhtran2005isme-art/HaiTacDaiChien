using UnityEngine;
using UnityEngine.UI;

public class PopupInfoDropPosition : BasePopup
{
	public Text m_Title;

	public GameObject m_PanelDrop;

	public Transform m_LayoutElementPosition;

	public GameObject m_ItemPositon;

	private ItemModel m_ItemData;

	public void SetData(ItemModel item)
	{
	}

	public static void ShowUp(ItemModel item)
	{
	}
}
