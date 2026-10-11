using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemHeroFK : MonoBehaviour
{
	public Image m_IconHero;

	public Image m_Focus;

	public Text m_NameHero;

	public Text m_Cost;

	public GameObject m_Lock;

	public Action<HeroFkModel> m_ActionCB;

	private HeroFkModel data;

	public void InitData(HeroFkModel model, bool isSelect = false)
	{
	}

	public void ItemClick()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
