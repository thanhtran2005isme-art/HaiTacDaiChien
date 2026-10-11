using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHeadUI : MonoBehaviour
{
	public Image m_ImageHead;

	public GameObject m_GoCheck;

	public GameObject m_GoLock;

	public GameObject m_GoDeath;

	public GameObject m_GoNotify;

	public Image m_ImageFaction;

	public Text m_TextLevel;

	public Text m_TextHeroKey;

	public GameObject m_GroupStar1;

	public GameObject m_GroupStar6;

	public GameObject m_GroupStar10;

	public GameObject m_GroupStarE;

	public Text m_TextStarE;

	public List<GameObject> m_LstStar1;

	public List<GameObject> m_LstStar6;

	public Slider m_SliderHp;

	public GameObject m_GoHeadHidden;

	public void Start()
	{
	}

	public void SetHeroSecret()
	{
	}

	public void HideAllHeroInfo()
	{
	}

	public void SetHeroModel(HeroModel model, bool showHpBar = false)
	{
	}

	public void SetMonsterModel(MonsterModel model, bool showHpBar = false)
	{
	}

	public void SetHeadGrayScale(bool enable)
	{
	}

	public void SetHeadGrayScaleInScrollview(bool enable)
	{
	}

	public void SetStar(int heroStar)
	{
	}

	public void HideStar()
	{
	}

	public void SetCheck(bool check)
	{
	}

	public void SetLock(bool locked)
	{
	}

	public void SetDeath(bool death)
	{
	}

	public bool IsChecked()
	{
		return false;
	}

	public bool IsLocked()
	{
		return false;
	}

	public void SetNotify(bool showNotify)
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
