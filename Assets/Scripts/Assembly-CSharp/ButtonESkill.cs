using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonESkill : MonoBehaviour
{
	public Image m_ImageSkill;

	public GameObject m_GoCoverOverlay;

	public GameObject m_GoFrameSelected;

	public GameObject m_GoIconLock;

	private ESkillInfo m_ESkillInfo;

	public Action<ESkillInfo> m_ButtonClickCb;

	private void Start()
	{
	}

	public void SetSkillData(ESkillInfo skillInfo)
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
