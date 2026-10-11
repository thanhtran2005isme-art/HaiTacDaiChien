using System;

public class ValueUpdater
{
	private float m_From;

	private float m_To;

	private Action<float> m_ValueChangedCb;

	private float m_Speed;

	private float m_CurrentValue;

	private bool m_Running;

	public ValueUpdater(Action<float> valueChangedCb)
	{
	}

	public void StopUpdater()
	{
	}

	public void StartUpdater(float fromValue, float toValue, float duration)
	{
	}

	public void Update(float deltaTime)
	{
	}
}
