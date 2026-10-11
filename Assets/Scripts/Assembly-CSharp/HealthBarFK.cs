using UnityEngine;
using UnityEngine.UI;

public class HealthBarFK : MonoBehaviour
{
	[HideInInspector]
	public PlayerController m_HeroScript;

	public const float SIZE_ICON = 26f;

	public const float SPACING_ICON = 5f;

	private float mScreenRatio;

	public RectTransform m_Transform;

	public GameObject manaBar;

	public Image m_ImageHp;

	public Image m_ImageMana;

	public Image[] m_Chakra;

	private float m_HealthValue;

	private void Start()
	{
	}

	public void ShowMana(bool show)
	{
	}

	public void setHpBarColor(int team)
	{
	}

	public void TestLostHealth()
	{
	}

	public void SetHeathBarValue(float value)
	{
	}

	public void SetManaBarValue(float value)
	{
	}

	private void Update()
	{
	}

	public void MapHeathBarToHero()
	{
	}
}
