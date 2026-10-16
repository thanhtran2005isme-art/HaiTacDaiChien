using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellHeroLottery : MonoBehaviour
{
	public Text m_TileLimit;

	public Text m_TxtLimit;

	public GameItem m_ToItem;

	public Transform m_FromItem;

	public ScrollRect m_ScrollRect;

	private BonusModel bonus;

	private List<object> lstObjModel;

	private GameObject prefabGameItem;

	private List<ItemModel> lstFromItemModel;

	private List<ItemModel> lstToItemModel;

	public void ShowData(AccumulatedReward data)
	{
	}

	private void Awake()
	{
	}

	private void Update()
	{
	}
}
