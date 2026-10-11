using System;
using UnityEngine;
using UnityEngine.UI;

public class CellHeroSkin : MonoBehaviour
{
	public GameObject m_GoTagNumber;

	public Image m_ImageSkin;

	public Text m_TextNumber;

	public Action<HeroSkin> m_ClickCallback;

	private HeroSkin _heroSkin;

	private void Start()
	{
	}

	public void LoadData(HeroSkin heroSkin)
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
