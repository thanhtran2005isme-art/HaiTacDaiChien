using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupInformationAuraNew : BasePopup, IG2UIEventHandler
{
	public List<GameObject> lstContent;

	public List<GameObject> lstAntiGroup;

	public Text descHP;

	public Text descAttack;

	public Text descAntiAssassin;

	public Text descAntiAttack;

	public AuraNew m_AuraNew;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public override void OnDestroy()
	{
	}

	public void LoadData(List<int> lstNumberHero, List<float> lstData, int[] arrNumberHero, List<HeroFaction> lstHeroFaction, int count)
	{
	}

	public static GameObject ShowPopup(List<int> lstNumberHero, List<float> lstData, int[] arrAnti, List<HeroFaction> lstHeroFaction, int count)
	{
		return null;
	}

	public void ProcessG2Event(G2EventData eventData)
	{
	}
}
