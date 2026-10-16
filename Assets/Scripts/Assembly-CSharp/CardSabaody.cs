using System;
using UnityEngine;
using UnityEngine.UI;

public class CardSabaody : MonoBehaviour
{
	public Image m_ImageCard;

	public GameObject m_GoTitle;

	public GameObject m_GoOpenClose;

	public Text m_TextTitle;

	public Text m_TextTimeCaption;

	public TextCountdown m_TextCountdown;

	public GameObject m_GoRequireLv;

	public Text m_TextRequireLv;

	public GameObject m_GoLocked;

	public Text m_TextLocked;

	public Action<int> m_ClickCb;

	private CardSabaodyData _cardData;

	private void Start()
	{
	}

	public void SetCardData(CardSabaodyData cardData)
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
