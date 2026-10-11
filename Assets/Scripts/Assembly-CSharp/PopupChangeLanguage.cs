using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupChangeLanguage : BasePopup
{
	public Transform m_TfContainer;

	public GameObject m_PrefabButtonLanguage;

	private List<ButtonLanguage> _listBtnLanguage;

	private Language _languagePicked;

	private Language _languageBeforeChanged;

	private Action _langChangedCb;

	protected override void Start()
	{
	}

	public void Init()
	{
	}

	public void LanguagePick(LanguageInfo langInfo)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void SetLanguageChangedCb(Action languageChangedCb)
	{
	}

	public static PopupChangeLanguage ShowUp(Action langChangedCb)
	{
		return null;
	}
}
