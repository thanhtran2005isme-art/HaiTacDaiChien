using System;
using UnityEngine;
using UnityEngine.UI;

public class DungeonLotion : MonoBehaviour
{
	[HideInInspector]
	public int m_LotionId;

	private Action<int> m_LotionClickCb;

	public SpineObject m_EffectShowUp;

	public Image m_ImageLotion;

	private void Start()
	{
	}

	public void SetLotionId(int lotionId)
	{
	}

	public void OnLotionClick()
	{
	}

	public void SetClickCallback(Action<int> cb)
	{
	}

	public void Show()
	{
	}

	public void Hide()
	{
	}

	private void Update()
	{
	}
}
