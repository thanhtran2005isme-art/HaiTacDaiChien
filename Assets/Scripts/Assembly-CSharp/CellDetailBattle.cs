using UnityEngine;
using UnityEngine.UI;

public class CellDetailBattle : MonoBehaviour
{
	public const int TYPE_DAMAGE = 1;

	public const int TYPE_HURT = 2;

	public const int TYPE_HEAL = 3;

	public const float DURATION_RUNNER = 0.6f;

	private SliderRunner m_SliderRunner;

	private TextRunner m_TextRunner;

	public Slider m_Slider;

	public Text m_TextValue;

	public HeroHeadUI m_HeroHeadUI;

	public PetHeadUI m_PetHeadUI;

	public Image m_ImageBgr;

	public Image m_ImageForgegroundSlider;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetBackgroundColor(bool evenCell)
	{
	}

	public void SetPetModel(PetModel pm)
	{
	}

	public void SetHeroModel(HeroModel hm)
	{
	}

	public void SetDataSlider(int type, long value, long maxValue)
	{
	}
}
