using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStoneUI : MonoBehaviour
{
	public Image m_ImgBgr;

	public Image m_ImgStone;

	public Image m_ImgBorder;

	public List<GameObject> m_ListGoStar;

	private HeroStoneModel m_Stone;

	private Action<HeroStoneModel> m_ClickCallback;

	private void Start()
	{
	}

	public void SetStone(HeroStoneModel stone)
	{
	}

	private void LoadBorderAndBackground(int stoneId)
	{
	}

	public void SetClickCallback(Action<HeroStoneModel> callback)
	{
	}

	public void OnItemClick()
	{
	}
}
