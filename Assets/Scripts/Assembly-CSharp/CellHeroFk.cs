using System;
using UnityEngine;
using UnityEngine.UI;

public class CellHeroFk : MonoBehaviour
{
	public Text m_TextHeroName;

	public Text m_TextAtk;

	public Text m_TextHp;

	public Text m_TextSpeed;

	public Image m_ImageHero;

	public Action<DummyHeroModel> m_HeroSelectCb;

	private DummyHeroModel m_HeroModel;

	public GameObject m_GoLocked;

	private void Start()
	{
	}

	public void SetData(DummyHeroModel model)
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
