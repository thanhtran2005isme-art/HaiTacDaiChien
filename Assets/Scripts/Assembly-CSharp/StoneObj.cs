using UnityEngine;

public class StoneObj : MonoBehaviour
{
	private bool rotate;

	private bool isDone;

	private Vector3 lastPos;

	public int teamID;

	public int damage;

	public float speed;

	public TrailRenderer trail;

	private Vector3 pos1;

	private Vector3 pos2;

	private Vector3 pos3;

	private float t;

	public float time;

	public float sizeAlert;

	private GameObject shadow;

	public CircleCollider2D collider;

	private PlayerData mData;

	private float heightCollider;

	private void OnTriggerEnter2D(Collider2D collision)
	{
	}

	public void CreateEffectNova()
	{
	}

	public void ExplosionNova(Vector3 fromPosition, Vector3 toPosition)
	{
	}

	public void setPlayerData(PlayerData mData)
	{
	}

	public float fly(Vector3 pos, bool rotate, float height, float size)
	{
		return 0f;
	}

	public Quaternion getRotation(Vector3 pointFrom, Vector3 pointTo)
	{
		return default;
	}

	public void Explosion()
	{
	}

	private void LateUpdate()
	{
	}
}
