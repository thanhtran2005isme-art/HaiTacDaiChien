using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupTrialChest : BasePopup, IG2UIEventHandler
{
	public Text titleChest1;

	public Text turnNumberChest1;

	public Text titleChest2;

	public Text turnNumberChest2;

	public Text titleChest3;

	public Text turnNumberChest3;

	public GameObject goChest1;

	public GameObject goChest2;

	public GameObject goChest3;

	public Text txtGoChest1;

	public Text txtGoChest2;

	public Text txtGoChest3;

	private List<DataChest> lstDataChest;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public override void OnDestroy()
	{
	}

	public static PopupTrialChest ShowUp(List<DataChest> lstDataChest)
	{
		return null;
	}

	public void LoadData(List<DataChest> data)
	{
	}

	public void Chest1ClickOpen()
	{
	}

	public void Chest2ClickOpen()
	{
	}

	public void Chest3ClickOpen()
	{
	}

	public void ProcessG2Event(G2EventData eventData)
	{
	}
}
