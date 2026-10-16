using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLotionHp : MonoBehaviour
{
	private int m_LotionId;

	[HideInInspector]
	private int m_NumberLotion;

	private string m_Desc;

	public Text m_TextNumber;

	public Image m_ImageLotion;

	public Action<int, int, string> m_LotionHpClickCallback;

	private void Start()
	{
	}

	public void SetLotionInfo(int lotionId, string pathSprite, string desc)
	{
	}

	public void SetNumberLotion(int number)
	{
	}

	public void AddLotion(int number)
	{
	}

	public void ButtonLotionClick()
	{
	}

	private void Update()
	{
	}
}
