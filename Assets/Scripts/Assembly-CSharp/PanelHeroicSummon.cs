using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHeroicSummon : MonoBehaviour
{
	[Serializable]
	private class DataButtonScroll
	{
		public Image m_Icon;

		public Text m_TextData;

		public Image m_IconGem;
	}

	public GameObject m_GoButton1Scroll;

	public GameObject m_GoButton1Free;

	public GameObject m_GoButton10Scroll;

	public TextCountdown m_TextCountDown;

	public GameObject m_GroupCountDown;

	private int m_Gem1Scroll;

	private int m_Gem10Scroll;

	[SerializeField]
	private List<DataButtonScroll> m_DataButton;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void InitUI()
	{
	}

	private void ShowButton1Free()
	{
	}

	public void SetDataHeroSummon(int m_Gem1Scroll, int m_Gem10Scroll)
	{
	}

	private void ShowButton1Scroll()
	{
	}

	public void SetHeroicSummonData(int countDownTime)
	{
	}

	public void ButtonSummon1FreeClick()
	{
	}

	public void ButtonSummon1ScrollClick()
	{
	}

	public void ButtonSummon10ScrollClick()
	{
	}

	private void Update()
	{
	}
}
