using System;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
	public JoystickKnob m_Knob;

	public Action m_JoyStickHaltCb;

	public Action<float> m_JoyStickMoveCb;

	public Action m_AttackClickCb;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ButtonAttackClick()
	{
	}
}
