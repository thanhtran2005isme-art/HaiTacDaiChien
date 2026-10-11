using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupBottomLeft : MonoEventHandler, IG2UIEventHandler
{
	public Text numberTurnRage;

	public Text numberTurnRock;

	public Text numberTurnStorm;

	public GameObject iconRage;

	public GameObject iconRock;

	public GameObject iconStorm;

	public Transform goRage;

	public Transform goRock;

	public Transform goStorm;

	private int turnRage;

	private int turnRock;

	private int turnStorm;

	private List<PopupInfomation> lstInf;

	protected override void Awake()
	{
	}

	public override void OnDestroy()
	{
	}

	protected override void Start()
	{
	}

	public InformationBraveTrail SetDtaRage()
	{
		return null;
	}

	public InformationBraveTrail SetDtaRock()
	{
		return null;
	}

	public InformationBraveTrail SetDtaStorm()
	{
		return null;
	}

	public void LoadData(int turnRage, int turnRock, int turnStorm)
	{
	}

	public void RageClick()
	{
	}

	public void RockClick()
	{
	}

	public void StormClick()
	{
	}

	public void ProcessG2Event(G2EventData eventData)
	{
	}
}
