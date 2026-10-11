using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelReward : MonoBehaviour
{
	public Text txtLevel;

	public Image checkRecived;

	public GameObject goDesc;

	public Transform parent;

	private List<GameObject> m_ListGoGameItem;

	public void SetData(LevelRewardMiniGameData data)
	{
	}
}
