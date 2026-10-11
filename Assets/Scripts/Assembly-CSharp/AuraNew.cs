using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuraNew : MonoBehaviour
{
	public Text plusHP;

	public Text plusAttack;

	public Text antiAttack;

	public Text antiAssassin;

	public List<Image> lstFaction;

	private const int ONE_HERO = 1;

	private const int TWO_HERO = 2;

	private const int THREE_HERO = 3;

	private const int FOUR_HERO = 4;

	private const int FIVE_HERO = 5;

	private const int SIX_HERO = 6;

	private List<float> lstdata;

	private List<int> lstMaxNumber;

	private int[] arrAnti;

	private List<HeroFaction> lstHeroFaction;

	private int count;

	private void Awake()
	{
	}

	public void SetData(List<HeroFaction> lstHeroFaction, int count)
	{
	}

	public void OnClickInformationNewAura()
	{
	}

	public void SetTextBuff(float numberPlusHp, float numberPlusAttack, float numberAntiAssassin, float numberAntiAttack)
	{
	}
}
