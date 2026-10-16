using System;
using UnityEngine;

public class HeroSlotFk : MonoBehaviour
{
	public Action<int> m_HeroLineupRemoveCb;

	public CanvasHeroFk m_CanvasHero;

	[HideInInspector]
	public int m_HeroSpineId;

	private void Start()
	{
	}

	public void RemoveHero()
	{
	}

	public void SetHero(int spineId)
	{
	}

	public int GetHeroSpineIdInSlot()
	{
		return 0;
	}

	public bool HasFillHero()
	{
		return false;
	}

	private void Update()
	{
	}
}
