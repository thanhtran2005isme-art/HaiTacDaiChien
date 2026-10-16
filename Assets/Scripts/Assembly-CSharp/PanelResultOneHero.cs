using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelResultOneHero : MonoBehaviour
{
	[SerializeField]
	private Image aHero;

	[SerializeField]
	private GameObject m_ItemPool;

	[SerializeField]
	private VerticalPoolGroup m_VerPool;

	[SerializeField]
	private ToggleGroup m_ToggleGroup;

	private List<long> data;

	private long maxValue;

	private List<DataItemSlider> lstSlider;

	private void Awake()
	{
	}

	public void Init(List<long> data, int heroKey)
	{
	}

	public void ButtoToggerClick(int id)
	{
	}

	public void ButtonBackClick()
	{
	}
}
