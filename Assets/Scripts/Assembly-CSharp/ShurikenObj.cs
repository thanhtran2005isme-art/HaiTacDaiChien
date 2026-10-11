using UnityEngine;

public class ShurikenObj : MonoBehaviour
{
	public float Speed;

	private Vector3 moveDir;

	public int teamID;

	public int damage;

	public TrailRenderer trail;

	public int hitMonster;

	public int hitWall;

	public Rigidbody2D rigid;

	private Vector2 oldVelocity;

	private bool isRicochet;

	private bool isPiercingShot;

	private bool isBouncingWall;

	private bool isBolt;

	private PlayerData mData;

	private Vector2 predictedPoint;

	private GameObject lastObjectBouncing;

	private Vector3 lastPosBouncing;

	private void Start()
	{
	}

	public void setPlayerData(PlayerData mData)
	{
	}

	private void Bounce(Vector3 collisionNormal, Collision2D collision)
	{
	}

	private bool Ricochet(PlayerController mPlayer)
	{
		return false;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
	}

	public void ImpactWall()
	{
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
	}

	public void PoolObject()
	{
	}

	public void moveDirection(Vector3 position)
	{
	}

	private void Update()
	{
	}
}
