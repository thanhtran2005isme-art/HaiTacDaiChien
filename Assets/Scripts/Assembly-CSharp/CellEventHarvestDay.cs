using System.Collections.Generic;
using UnityEngine;

public class CellEventHarvestDay : MonoBehaviour
{
	[SerializeField]
	private Transform m_GroupReWard;

	[SerializeField]
	private GameObject m_GoQuestItem;

	[SerializeField]
	private Transform m_TfQuest;

	private List<GameObject> m_ListQuest;

	private List<GameItem> m_ListReward;

	private List<accumulatedRewardList> dataItem;

	private void Awake()
	{
	}

	public void SetData(DataEvenHarvestDay Data)
	{
	}
}
