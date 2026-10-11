using System;
using UnityEngine;
using UnityEngine.UI;

public class TextCountdown : MonoBehaviour
{
	public Text m_TextTime;

	private string m_Infix;

	private string m_PosInfix;

	private int m_TimeCountdown;

	private ActionRepeatTimer m_Timer;

	private Action m_CountDownToZeroCb;

	private bool haveOnlySec;

	private bool setPause;

	private void Start()
	{
	}

	public int GetTimeLeft()
	{
		return 0;
	}

	public void SetText(string text)
	{
	}

	public void SetCountDownToZeroCallback(Action cb)
	{
	}

	public void SetCountdownTime(int time, string infix = "", string posInfix = "", bool haveOnlySec = false)
	{
	}

	public void Pause(bool setPause)
	{
	}

	private void Update()
	{
	}
}
