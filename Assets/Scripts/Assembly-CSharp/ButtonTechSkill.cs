using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTechSkill : MonoBehaviour
{
	private Action<ButtonTechSkill> m_ActionClickCb;

	[SerializeField]
	private Image m_ImageOn;

	[SerializeField]
	private Image m_ImageOff;

	[SerializeField]
	private Text m_TextLevel;

	[HideInInspector]
	public string m_PathSpriteOn;

	[HideInInspector]
	public string m_PathSpriteOff;

	public TechSkillInfo m_TechSkillInfo;

	public Image m_ImageArrow;

	private void Start()
	{
	}

	public void SetImageOnOff(string pathSpriteOn, string pathSpriteOff)
	{
	}

	public void SetTechData(TechSkillInfo techInfo)
	{
	}

	public void SetClickCallback(Action<ButtonTechSkill> clickCb)
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
