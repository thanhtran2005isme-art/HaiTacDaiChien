using System.Collections.Generic;
using UnityEngine;

public class MonsterScene : BaseScene
{
	public DotIndicator m_DotIndicator;

	public GameObject m_GoButtonPrev;

	public GameObject m_GoButtonNext;

	public GameObject m_GoPointHideBoxPet;

	public GameObject m_GoBoxPet;

	public List<GameObject> m_LstGoBoxMonster;

	private List<PetModel> m_LstPetModel;

	private const int MONSTER_PER_PAGE = 3;

	private int m_TotalPage;

	private int m_CurrentPageIdx;

	private Vector2 m_PointShowBoxPet;

	protected override void Start()
	{
	}

	private void LoadListPet(List<PetModel> lstPetModel)
	{
	}

	private void LoadPage(int pageIdx)
	{
	}

	public void ButtonNextClick()
	{
	}

	public void ButtonPrevClick()
	{
	}

	public void ButtonHelpClick()
	{
	}

	private void CheckArrowPage()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void ButtonBackClick()
	{
	}

	private void Update()
	{
	}
}
