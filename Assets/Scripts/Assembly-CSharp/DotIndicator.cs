using System.Collections.Generic;
using UnityEngine;

public class DotIndicator : MonoBehaviour
{
	[SerializeField]
	private float DotSpacing;

	[SerializeField]
	private Vector2 DotSize;

	private List<GameObject> m_LstDotObject;

	[SerializeField]
	private GameObject m_PrefabDot;

	private void Start()
	{
	}

	public void SetNumDot(int numDot)
	{
	}

	public void SetActiveIndex(int index)
	{
	}
}
