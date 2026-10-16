using System.Collections.Generic;
using UnityEngine;

public class BoxRegressHero : MonoEventHandler
{
	public HeroMaterialSlot m_HeroMaterialInput;

	public ItemMaterial m_ItemStoneInput;

	private int m_StoneCost;

	public Transform m_TfOutput;

	public GameObject m_GoBoxInput;

	public GameObject m_GoBoxOutPut;

	private HeroModel m_HmMaterialInput;

	public SpineObject m_SpineOutPut;

	public GameObject m_GoPointDownInput;

	public GameObject m_GoPointUpInput;

	public GameObject m_GoPointDownOutput;

	public GameObject m_GoPointUpOutput;

	protected override void Start()
	{
	}

	public void SetHeroToRegress(HeroModel hm)
	{
	}

	public void MakeAppearAnimation()
	{
	}

	public void CalculateHeroReturn()
	{
	}

	private int GetHeroKeyPuppet9StarByFaction(HeroFaction faction)
	{
		return 0;
	}

	private int GetHeroKeyPuppet10StarByFaction(HeroFaction faction)
	{
		return 0;
	}

	public void ButtonRegressClick()
	{
	}

	public List<object> DeleteMaterialHeroInput()
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
