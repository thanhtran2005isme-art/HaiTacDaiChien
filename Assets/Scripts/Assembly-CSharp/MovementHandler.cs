using UnityEngine;

public class MovementHandler
{
	private float m_VelocityX;

	private float m_VelocityY;

	private float m_AccelerationX;

	private float m_AccelerationY;

	private RectTransform m_Transform;

	private bool m_EdgeBounceBack;

	private float m_xLimitLeft;

	private float m_xLimitRight;

	private float m_yLimitTop;

	private float m_yLimitBottom;

	private const float DISTANCE_BORDER_BOUNCE = 70f;

	public MovementHandler(RectTransform transform)
	{
	}

	public void SetLimitedHorizontal(float xLimitLeft, float xLimitRight, bool edgeBounceBack = true)
	{
	}

	public void SetLimitedVertical(float yLimitTop, float yLimitBottom)
	{
	}

	public void Stop()
	{
	}

	public void SetPosistion(Vector2 position)
	{
	}

	public void SetVelocity(Vector2 v, Vector2 a)
	{
	}

	private void CheckBounceBackX()
	{
	}

	private void CheckBounceBackY()
	{
	}

	public void UpdateMovement(float deltaTime)
	{
	}
}
