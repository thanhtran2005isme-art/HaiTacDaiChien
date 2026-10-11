using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollIsland : MonoBehaviour
{
	public GameObject m_PrefabIslandFirst;

	public GameObject m_PrefabIslandUp;

	public GameObject m_PrefabIslandDown;

	private Vector2 m_SizeIsland1;

	private Vector2 m_SizeIsland;

	public Transform m_TransformContent;

	private List<GameObject> m_LstGoIsland;

	[SerializeField]
	private ScrollRect m_ScrollRect;

	private float m_LengthOffsetScrollMax;

	[HideInInspector]
	public bool m_IsAutoOnLastestStage;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void InitListIsland()
	{
	}

	public void SetIslandData(int currentLevelId, int autoLevelId, bool scrollToCurrent)
	{
	}

	public void LockTutorialAllIsland()
	{
	}

	public void LockTutorialAllIslandExludeLastestStage()
	{
	}

	public void ShowTutorialHandOnLastestStage()
	{
	}

	public void ClearHandTutorial()
	{
	}

	private void Update()
	{
	}
}
