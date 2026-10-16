using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[HideInInspector]
	public BattleHero m_HeroScript;

	public const float SIZE_ICON = 26f;

	public const float SPACING_ICON = 5f;

	private float mScreenRatio;

	public RectTransform m_Transform;

	public Image m_ImageHp;

	public Image m_ImageRage;

	public Image m_ImageRage2;

	public Image m_ImageRage3;

	public Image m_ImageFaction;

	public Text m_TextLevel;

	public Text m_TextHpPoint;

	public GameObject m_GoRageBar;

	public GameObject m_GoGroupIconEffect;

	public GameObject m_PrefabIconEffect;

	private List<GameObject> m_LstGoIconEffect;

	private ActionRepeatTimer m_ActionCheckIconStatus;

	public GameObject m_GoSpineRageFull;

	[SerializeField]
	private Text m_TextHeroName;

	private float m_HealthValue;

	private void Start()
	{
	}

	public void TestLostHealth()
	{
	}

	public void SetDebugPoint(long hpPoint, long atkPoint, long armorPoint, long hpMax)
	{
	}

	public void SetHeathBarValue(float value)
	{
	}

	public void SetRageBarValue(float value)
	{
	}

	public void SetHeroName(string heroName)
	{
	}

	public void SetLevelAndFaction(int level, HeroFaction faction)
	{
	}

	private void Update()
	{
	}

	private void SetListIconEffect(List<ClientBuffEffectData> lstEffectData)
	{
	}

	private void MapHeathBarToHero()
	{
	}
}
