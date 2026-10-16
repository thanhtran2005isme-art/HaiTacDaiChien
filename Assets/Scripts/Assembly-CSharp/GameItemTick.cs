using System;
using UnityEngine;

public class GameItemTick : MonoBehaviour
{
	public GameItem m_GameItemUI;

	public GameItemTickData m_GameItemTickData;

	public GameObject m_GoTick;

	private Action<int> m_ClickCb;

	private void Start()
	{
	}

	public void SetItem(GameItemTickData item, int number = -1)
	{
	}

	public void SetClickCb(Action<int> callback)
	{
	}

	public void OnClick()
	{
	}
}
