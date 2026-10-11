using UnityEngine;

public class SwiftBird : MonoBehaviour
{
	private Vector2 m_InitPosition;

	private float m_DistanceFly;

	private float m_BirdSpeed;

	private Vector2 m_NextPosition;

	[SerializeField]
	private RectTransform m_Transform;

	private bool m_Up;

	private float m_Vx;

	private float m_Vy;

	private float m_DistanceSwitchY;

	private void Start()
	{
	}

	public void InitBird(Vector2 initPosition, float distanceFly)
	{
	}

	public void FindNextPositionOrDestroy()
	{
	}

	private void Update()
	{
	}
}
