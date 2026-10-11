using System;
using UnityEngine;

public class SlotHeroQuest : MonoBehaviour
{
	[SerializeField]
	private HeroHeadUI m_HeroHeadUI;

	[HideInInspector]
	public bool m_HasFilled;

	public Action<SlotHeroQuest> m_SlotClickCallback;

	[HideInInspector]
	public int m_SlotIndex;

	public HeroModel m_HeroModel;

	private void Start()
	{
	}

	public void SetSlotEmpty()
	{
	}

	public void FillHero(HeroModel hm)
	{
	}

	public void OnSlotClick()
	{
	}

	private void Update()
	{
	}
}
