using System;
using UnityEngine;

public class HeroSlotStone : MonoBehaviour
{
	public bool m_IsLocked;

	public GameObject m_GoImageLock;

	public GameObject m_GoImageUnlock;

	public HeroStoneUI m_HeroStoneUI;

	private Action m_OnClickCb;

	private int m_HeroId;

	private void Start()
	{
	}

	public void SetStatusLocked()
	{
	}

	public void SetStatusCanUnlocked()
	{
	}

	public void SetStone(int heroId, HeroStoneModel stone)
	{
	}

	public void SetSlotClickCallback(Action cb)
	{
	}

	public void OnSlotClick()
	{
	}
}
