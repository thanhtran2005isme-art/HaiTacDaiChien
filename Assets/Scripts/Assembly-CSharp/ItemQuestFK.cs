using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemQuestFK : MonoBehaviour
{
	public Text m_Title;

	public Text m_Progress;

	public Image m_Icon;

	public Image m_BgrFill;

	public GameObject m_Completed;

	public Action<QuestFk> m_ActionCB;

	public GameObject m_ItemReward;

	public Text m_TextReward;

	private QuestFk data;

	public void InitData(TYPE_QUEST type, QuestFk model, bool completed = false)
	{
	}

	public void ItemClick()
	{
	}
}
