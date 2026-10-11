using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class ItemStatueControl : MonoBehaviour
{
	private Vector3 SCALE_ON;

	private Vector3 SCALE_OFF;

	public Text m_LabelStatue;

	public SkeletonGraphic m_SpineStatue;

	public GameItem m_ItemExchange;

	public Action<int, int> m_ItemClickCb;

	private int m_SiblingIndex;

	private List<Vector3> m_ListPointTo;

	private bool move;

	private bool moveDone;

	private int m_IndexPoint;

	private float m_TimeTransition;

	private int m_Direction;

	private StatueItem m_InfoStatue;

	public void InitStatue(StatueItem data)
	{
	}

	public void SetOrderLayer(int indexLayer)
	{
	}

	public int GetOrderLayer()
	{
		return 0;
	}

	public void MoveStatueToPoint(List<Vector3> pointTarget, float timeTransition, int direction)
	{
	}

	public void ResetUIAfterMove()
	{
	}

	public void ItemClick()
	{
	}

	public bool GetStatusMove()
	{
		return false;
	}

	public StatueItem GetDataStatue()
	{
		return null;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
