using System;
using UnityEngine;
using UnityEngine.UI;

public class DummyHeroHead : MonoBehaviour
{
	public Image m_ImageHead;

	public GameObject m_GoTick;

	public GameObject m_GoLocked;

	[HideInInspector]
	public DummyHeroModel m_HeroModel;

	public Action<DummyHeroModel> m_OnClickCb;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetHeroModel(DummyHeroModel hm)
	{
	}

	public void SetSelected(bool selected)
	{
	}

	public void OnClick()
	{
	}
}
