using UnityEngine;

public class BattleObject : MonoPoolItem
{
	public Transform m_Transform;

	public GameObject m_GoMesh;

	public GameObject m_GoScale;

	public virtual void Start()
	{
	}

	public void SetScale(float scale)
	{
	}

	public Vector2 GetSize()
	{
		return default;
	}

	public void SetFacingDirection(GameDirection direction)
	{
	}

	public GameDirection GetFacingDirection()
	{
		return (GameDirection)0;
	}

	public void SetPosition(Vector2 pos)
	{
	}

	public Vector2 GetPosition()
	{
		return default;
	}

	public void SetRotation(float degree)
	{
	}
}
