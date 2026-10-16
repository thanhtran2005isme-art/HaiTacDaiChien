using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellPackEvent : MonoBehaviour
{
	public Text m_TextVipExp;

	public Text m_TextNumbBuy;

	public Text m_TextAction;

	public OnPublicDataCallback itemBuyCB;

	public Transform m_TfRewards;

	public ScrollRect m_ScrollRect;

	private PackItem m_Reward;

	private int m_EventId;

	private string productId;

	private BonusModel bonus;

	private List<object> lstObjModel;

	private GameObject prefabGameItem;

	private void Awake()
	{
	}

	public void ShowData(PackItem packItem)
	{
	}

	public void ButtonActionClick()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
