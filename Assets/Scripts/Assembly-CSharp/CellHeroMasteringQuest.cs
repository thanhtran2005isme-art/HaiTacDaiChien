using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellHeroMasteringQuest : MonoBehaviour
{
	public Text m_TextQuestName;

	public Text m_TextPoint;

	public Text m_TextProgress;

	public Slider m_SliderProgress;

	[SerializeField]
	private Transform m_TfRewards;

	public GameObject m_GoButtonCollect;

	public GameObject m_GoCheckCollected;

	private HeroMasteringQuestData _questData;

	private List<GameObject> _listGameItemRewards;

	private void Start()
	{
	}

	public void SetQuestData(HeroMasteringQuestData questData)
	{
	}

	public void OnCollectClick()
	{
	}

	private void Update()
	{
	}
}
