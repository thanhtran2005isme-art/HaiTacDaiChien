using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellGroupESkills : MonoBehaviour
{
	public List<GameObject> m_LstGoSkill;

	public Text m_TextTitle;

	[HideInInspector]
	public int m_GroupSkillIndex;

	private GroupESkills m_GroupESkill;

	public SpineObject m_SpineEffectActivate;

	private void Start()
	{
	}

	public void SetSkillActive(int skillIndex)
	{
	}

	public void SetButtonSkillClickDelegate(Action<ESkillInfo> clickCb)
	{
	}

	public void SetData(GroupESkills groupESkill)
	{
	}

	private void Update()
	{
	}
}
