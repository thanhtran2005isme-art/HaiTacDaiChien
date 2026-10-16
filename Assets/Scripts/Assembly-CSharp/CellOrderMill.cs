using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellOrderMill : MonoBehaviour
{
	public Text m_TextTitle;

	public Text m_TextCostUpdate;

	public Image m_ImageOrderScroll;

	public List<GameObject> m_LstGoStar;

	public Slider m_SliderProgress;

	public List<GameObject> m_LstGoGameItem;

	public GameObject m_GoButtonStart;

	public GameObject m_GoButtonUpdate;

	public RectTransform m_TransformAction;

	public TextCountdown m_TextCountDown;

	private OrderMillInfo m_OrderMill;

	private void Start()
	{
	}

	public void SetData(OrderMillInfo orderInfo)
	{
	}

	public void ButtonStartClick()
	{
	}

	public void ButtonUpgradeClick()
	{
	}

	private void Update()
	{
	}
}
