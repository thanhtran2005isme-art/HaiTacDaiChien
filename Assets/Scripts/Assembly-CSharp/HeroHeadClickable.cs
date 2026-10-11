using System;
using UnityEngine;

public class HeroHeadClickable : MonoBehaviour
{
	private Action<HeroHeadClickable> m_ClickCallback;

	public HeroHeadUI m_HeroHeadUI;

	[HideInInspector]
	public string m_LockDesc;

	[HideInInspector]
	public HeroModel m_HeroModel;

	private void Start()
	{
	}

	public void HideHero()
	{
	}

	public void SetHeroModel(HeroModel model, bool showHpBar = false)
	{
	}

	public void SetClickCallback(Action<HeroHeadClickable> callback)
	{
	}

	public void OnHeroClick()
	{
	}

	public void SetCheck(bool check)
	{
	}

	public bool IsChecked()
	{
		return false;
	}

	public void SetLock(bool locked, string lockDesc)
	{
	}

	public bool IsLocked()
	{
		return false;
	}

	public void SetNotify(bool notify)
	{
	}

	public bool IsNotify()
	{
		return false;
	}

	private void Update()
	{
	}
}
