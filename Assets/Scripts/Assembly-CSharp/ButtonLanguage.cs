using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLanguage : MonoBehaviour
{
	public Image m_ImageFlag;

	public Text m_TextName;

	public GameObject m_GoTick;

	public Action<LanguageInfo> m_ClickCb;

	private LanguageInfo _languageInfo;

	private void Start()
	{
	}

	public void SetData(LanguageInfo langInfo)
	{
	}

	public LanguageInfo GetLanguageInfo()
	{
		return null;
	}

	public void SetTicked(bool tickOn)
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
