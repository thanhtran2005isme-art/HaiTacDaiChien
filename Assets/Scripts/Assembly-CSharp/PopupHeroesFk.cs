using UnityEngine;
using UnityEngine.UI;

public class PopupHeroesFk : MonoBehaviour
{
	public GameObject m_PrefabCell;

	public GameObject m_GoScrollView;

	public CanvasHeroFk m_CanvasHero;

	public GridPoolGroup m_PoolGrid;

	public GameObject m_GoButtonSelect;

	public GameObject m_GoButtonUnlock;

	public Text m_TextCostUnlock;

	public DummyHeroModel m_CurrentHeroModel;

	public SpineObject m_EffectUnlock;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void OnHeroSelected(DummyHeroModel heroFk)
	{
	}

	private void LoadHero(DummyHeroModel heroFk)
	{
	}

	public void ButtonUnlockClick()
	{
	}

	public void ButtonUpgradeClick()
	{
	}

	public void ClosePopup()
	{
	}

	public void ButtonHelpClick()
	{
	}

	public static PopupHeroesFk ShowUp()
	{
		return null;
	}

	private void Update()
	{
	}
}
