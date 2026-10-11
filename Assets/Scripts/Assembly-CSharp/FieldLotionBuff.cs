using System;
using UnityEngine;
using UnityEngine.UI;

public class FieldLotionBuff : MonoBehaviour
{
	private int m_LotionId;

	private int m_MaxNumber;

	[HideInInspector]
	private int m_NumberLotion;

	private string m_Desc;

	public Text m_TextNumber;

	public Image m_ImageLotion;

	public Slider m_Slider;

	public Action<string> m_LotionBuffClickCallback;

	private void Start()
	{
	}

	public void SetLotionInfo(int lotionId, string pathSprite, int maxNumber, string desc)
	{
	}

	public void SetNumberLotion(int number)
	{
	}

	public void AddLotion(int numberAdd)
	{
	}

	public void ButtonLotionClick()
	{
	}

	private void Update()
	{
	}
}
