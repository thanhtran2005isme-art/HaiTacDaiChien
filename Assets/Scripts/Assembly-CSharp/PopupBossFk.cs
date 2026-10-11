using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupBossFk : MonoBehaviour
{
	public List<GameObject> m_LstGoCellBoss;

	private int m_CurrentPageIdx;

	private const int BOSS_PER_PAGE = 3;

	private int m_TotalBoss;

	private int m_TotalPage;

	public Text m_TextPage;

	public GameObject m_GoButtonNext;

	public GameObject m_GoButtonPrev;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void NextClick()
	{
	}

	public void PrevClick()
	{
	}

	private void CheckPage()
	{
	}

	public void LoadPage(int currentPageIndex)
	{
	}

	public static PopupBossFk ShowUp()
	{
		return null;
	}

	public void ButtonHelpClick()
	{
	}

	public void ClosePopup()
	{
	}

	private void Update()
	{
	}
}
