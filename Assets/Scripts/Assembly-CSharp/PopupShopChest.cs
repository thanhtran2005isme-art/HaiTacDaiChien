using System.Collections.Generic;
using UnityEngine.UI;

public class PopupShopChest : BasePopup, IG2UIEventHandler
{
	public Image img_Chest;

	public VerticalPoolGroup m_PoolReward;

	public Text m_TxtTitle;

	public Text m_TxtTotalTurn;

	public Text m_TxtCoin;

	private long level;

	private long index;

	private long numberCost;

	private List<DataCellShopChest> lstData;

	protected override void Start()
	{
	}

	public override void OnDestroy()
	{
	}

	public static PopupShopChest ShowPopup(List<DataCellShopChest> lstDataOfOneChest, long level)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void LoadData(List<DataCellShopChest> lstData, long level)
	{
	}

	public void ProcessG2Event(G2EventData eventData)
	{
	}
}
