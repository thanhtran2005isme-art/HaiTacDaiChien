using System.Collections.Generic;
using UnityEngine;

public class AutoCampaignBgr : MonoBehaviour
{
	public float m_yOffset;

	private List<GameObject> m_LstGoMapPart;

	private Vector2 m_PartSize;

	private Vector2 m_LastPartPosition;

	private ActionRepeatTimer m_TimerCheckMap;

	[SerializeField]
	private GameObject m_GoPrefabMapPart;

	[SerializeField]
	private Camera m_Camera;

	private void Start()
	{
	}

	private void InitMap()
	{
	}

	private void CheckMap()
	{
	}

	private void Update()
	{
	}
}
