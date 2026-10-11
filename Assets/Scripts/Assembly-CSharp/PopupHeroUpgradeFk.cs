using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupHeroUpgradeFk : MonoBehaviour
{
	public Action m_ClosedCb;

	public Text m_TextLevel;

	public Text m_TextName;

	public Text m_TextAtk;

	public Text m_TextHp;

	public Text m_TextSpeed;

	public Text m_TextGoldUpgrade;

	public GameObject m_GroupUpgrade;

	public GameObject m_GroupMax;

	public Image m_ImgHead;

	private DummyHeroModel m_HeroModel;

	public SpineObject m_EffectUpgrade;

	private void Start()
	{
	}

	private void Awake()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void LoadModel(DummyHeroModel hm)
	{
	}

	public void ButtonUpgradeClick()
	{
	}

	private void ReloadHeroData()
	{
	}

	private int GetGoldToUpgrade(int level)
	{
		return 0;
	}

	public static PopupHeroUpgradeFk ShowUp(DummyHeroModel hm)
	{
		return null;
	}

	private void Update()
	{
	}
}
