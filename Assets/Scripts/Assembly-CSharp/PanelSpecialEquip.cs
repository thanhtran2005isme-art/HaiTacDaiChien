using UnityEngine;
using UnityEngine.UI;

public class PanelSpecialEquip : MonoEventHandler
{
	[Header("Attack")]
	public Text attackLevel;

	public Text attackContent;

	public GameObject goAttackLock;

	public Image imgAttackClass;

	public GameObject goGroupAttackStar;

	public CurvedItem attackStarScript;

	public GameObject goNotyATK;

	public Text role;

	[Header("HP")]
	public Text hpLevel;

	public Text hpContent;

	public GameObject goNotifyHp;

	public GameObject goHpLock;

	public Image imgHpTier;

	public GameObject goGroupTierStar;

	public CurvedItem hpStarScript;

	public Text rank;

	private const int TYPE_ATTACK = 0;

	private const int TYPE_HP = 1;

	private HeroModel heroModel;

	private GameObject goPopupEquipActionReupOrUpgrade;

	public void OnUpgradeAttackClick()
	{
	}

	public void OnUpgradeHpClik()
	{
	}

	public void SetHeroModel(HeroModel model)
	{
	}

	private Sprite LoadImgClass(HeroClass m_class)
	{
		return null;
	}

	private Sprite LoadImgQuality(int quality)
	{
		return null;
	}

	private string GetRankHero(int quality)
	{
		return null;
	}

	public void RequestUnLockData()
	{
	}

	public void OnButtonHelpClick()
	{
	}

	public void ReLoadData()
	{
	}

	public void SetDataPanel(HeroModel heroModel)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
