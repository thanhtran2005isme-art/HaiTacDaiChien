using System.Collections.Generic;
using UnityEngine;

public class PopupHeroSkins : BasePopup
{
	private const int TAB_PLAYER_SKIN = 1;

	private const int TAB_DICTIONARY = 2;

	public GridPoolGroup m_GridPool;

	public GameObject m_GoGroupSkin;

	public GameObject m_GoGroupNoSkin;

	public TabManager m_TabManager;

	public GameObject m_GoCellSkin;

	protected override void Start()
	{
	}

	public void InitData()
	{
	}

	private void OnTabButtonClick(int buttonTabId)
	{
	}

	public void OnSkinClick(HeroSkin heroSkin)
	{
	}

	private void LoadSkin(List<HeroSkin> lstSkin)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public static void ShowUp()
	{
	}

	private void Update()
	{
	}
}
