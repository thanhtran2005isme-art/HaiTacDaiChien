using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelESkillDetail : MonoBehaviour
{
	public GameObject m_GroupAction;

	public Image m_ImageSkill;

	public Text m_TextSkillName;

	public Text m_TextDesc;

	private ESkillInfo m_ESkillInfo;

	public Action<int, int> m_ESkillSelectCb;

	private void Start()
	{
	}

	public void SetSkillInfo(ESkillInfo skillInfo)
	{
	}

	public void ButtonActivateClick()
	{
	}

	public void BackgroundClick()
	{
	}

	private void Update()
	{
	}
}
