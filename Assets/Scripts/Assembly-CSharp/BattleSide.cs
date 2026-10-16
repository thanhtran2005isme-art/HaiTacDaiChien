using System.Collections.Generic;
using UnityEngine;

public class BattleSide : MonoBehaviour, IHeroDragDropListener
{
	public List<BattleSlot> m_LstBattleSlot;

	public GameObject m_GoPointPet;

	private List<GameObject> m_LstGoHero;

	private GameObject m_GoBattlePet;

	private List<BattleHero> m_LstHeroScript;

	private GameDirection m_Side;

	public GameObject m_GoPointCastSkill;

	public const float WIDTH_BATTLE_FIELD = 4f;

	public const float HEIGHT_BATTLE_FIELD = 5f;

	private void Start()
	{
	}

	public void InitField()
	{
	}

	public Vector2 GetPointCastSkill()
	{
		return default;
	}

	public void SetHeroPlaceHolderVisibility(bool visible)
	{
	}

	public void SetHeroes(GameDirection side, List<GameObject> lstGoHero, GameObject goPet)
	{
	}

	public void SetDetectDragDropOn(bool dragDropOn)
	{
	}

	public BattleSlot GetBattleSlotMouseIsOver()
	{
		return null;
	}

	public BattleHero GetSelectedHero()
	{
		return null;
	}

	public void OnHeroDrag(GameObject goHeroDrag)
	{
	}

	public void ClearHeroSelected()
	{
	}

	public void OnHeroDrop(GameObject goHeroDrag)
	{
	}

	private void Update()
	{
	}
}
