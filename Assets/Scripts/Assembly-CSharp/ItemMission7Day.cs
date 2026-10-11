using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMission7Day : MonoBehaviour
{
	public Text m_Title;

	public Text m_LabelAction;

	public ScrollRect m_ScrollRectReward;

	public GameObject m_ButtonAction;

	public Text m_TextNoAction;

	public Action<MissionItem> actionCb;

	private MissionItem missionData;

	private BonusModel bonus;

	private List<object> lstObjModel;

	private GameObject prefabGameItem;

	private void Awake()
	{
	}

	public void InitData(MissionItem mission)
	{
	}

	public void ActionClick()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
