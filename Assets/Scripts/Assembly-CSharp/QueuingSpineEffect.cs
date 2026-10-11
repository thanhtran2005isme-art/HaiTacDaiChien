using UnityEngine;

public class QueuingSpineEffect
{
	public float m_DelayStart;

	public GameDirection m_FacingDirection;

	public Vector2 m_Position;

	public int m_SortingOrder;

	private bool _isDone;

	private float _countTime;

	public bool IsDone()
	{
		return false;
	}

	public void Tick(float deltaTime)
	{
	}
}
