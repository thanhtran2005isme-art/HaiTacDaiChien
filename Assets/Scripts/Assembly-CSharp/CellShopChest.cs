using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellShopChest : MonoEventHandler
{
	public Button m_BtnOpen;

	public Text m_TxtCoin;

	public Transform m_parent;

	public Image m_ImageCoin;

	private long numberCost;

	private List<GameObject> m_ListGoGameItem;

	private long levelChest;

	private long indexCell;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public void SetData(DataCellShopChest data, long levelChest)
	{
	}

	public void OpenChestOfCell()
	{
	}
}
