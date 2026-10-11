using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSettingFAQ : MonoBehaviour
{
	private const int TAB_HELP = 0;

	private const int TAB_FAQ = 1;

	private const int TAB_NOTIFY = 2;

	public Transform m_TabGroup;

	public VerticalPoolGroup m_PoolItem;

	public GameObject m_ListItem;

	public GameObject m_Content;

	public GameObject m_ContentNotify;

	public Text m_TextContent;

	public Text m_TextNotify;

	public Text m_TitleContent;

	public ScrollRect m_ScrollContent;

	private int currTab;

	private void InitTab()
	{
	}

	private void TabOnClick(int tabId)
	{
	}

	private void InitPool()
	{
	}

	public void ItemHelpClick(object data)
	{
	}

	public void ItemFAQClick(object data)
	{
	}

	private string FormFAQ(List<FAQModel.QuestModel> lstQuest)
	{
		return null;
	}

	public void ItemContentClick()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
