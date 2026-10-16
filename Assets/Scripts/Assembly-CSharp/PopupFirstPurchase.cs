using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupFirstPurchase : BasePopup
{
	[SerializeField]
	private Text textDay1;

	[SerializeField]
	private Text textDay2;

	[SerializeField]
	private Text textDay3;

	[SerializeField]
	private Image m_BG;

	[SerializeField]
	private Transform m_GroupReWard1;

	[SerializeField]
	private Transform m_GroupReWard2;

	[SerializeField]
	private Transform m_GroupReWard3;

	private List<GameItem> m_ListReward1;

	private List<GameItem> m_ListReward2;

	private List<GameItem> m_ListReward3;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public void LoadEventData(GameFirstPurchaseEvent eventData)
	{
	}

	public void ButtonPurchaseClick()
	{
	}

	public static void ShowUp(GameFirstPurchaseEvent eventData)
	{
	}
}
