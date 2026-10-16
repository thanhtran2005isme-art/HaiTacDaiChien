using UnityEngine;
using UnityEngine.UI;

public class DummyHealthBar : MonoBehaviour
{
	[HideInInspector]
	public DummyHeroBattle m_HeroScript;

	private float mScreenRatio;

	public RectTransform m_Transform;

	public Image m_ImageHp;

	private float m_HealthValue;

	private void Start()
	{
	}

	public void AttachToHero(DummyHeroBattle heroBattle)
	{
	}

	public void SetHeathBarValue(float value)
	{
	}

	private void Update()
	{
	}

	public void MapHeathBarToHero()
	{
	}
}
