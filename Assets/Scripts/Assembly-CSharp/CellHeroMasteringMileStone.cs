using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellHeroMasteringMileStone : MonoBehaviour
{
	public Text m_TextPoint;

	public Transform m_TfRewards;

	public GameObject m_GoMileStoneFgr;

	public GameObject m_GoKnob;

	public Image m_ImageMileStoneProgressFgr;

	public GameObject m_GoCheckCollected;

	public GameObject m_GoLocked;

	public GameObject m_GoTagRewardAvail;

	private List<GameObject> m_LstGoGameObjReward;

	public Action<int> m_CellClickCb;

	private HeroMasteringMileStone _mileStoneData;

	private void Start()
	{
	}

	public void LoadData(HeroMasteringMileStone data)
	{
	}

	public void OnClick()
	{
	}

	public void SetCurrentPoint(int currentPoint)
	{
	}

	private void Update()
	{
	}
}
