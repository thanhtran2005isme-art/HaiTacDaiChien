using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;

public class TowerHero : MonoBehaviour
{
	[SerializeField]
	private SkeletonAnimation m_SkeletonAnimation;

	private Spine.AnimationState m_SpineAnimationState;

	private MeshRenderer m_Meshrender;

	[SerializeField]
	public GameObject m_GoMesh;

	private const int STATE_IDLE = 1;

	private const int STATE_MOVE = 2;

	private const float SPEED_VALUE = 6.1f;

	private Transform m_Transform;

	private int m_State;

	private float m_VelocityX;

	private float m_VelocityY;

	private List<Vector2> m_LstPointDestination;

	private Vector2 m_PointDestination;

	private Vector2 m_PointOrigin;

	protected string m_StrAniRun;

	protected string m_StrAniIdle;

	public Action m_ReachDestinationCallback;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void SetHeroInfo(HeroBattleInfo hi)
	{
	}

	private void PlayAniIdle()
	{
	}

	private void PlayAniRun()
	{
	}

	public void PlayAnimation(string aniName, bool loop = true)
	{
	}

	public void SetLayerOrder(int layerOrder)
	{
	}

	public void SetPosition(Vector2 position)
	{
	}

	public void SetListDestination(List<Vector2> lstPointDest)
	{
	}

	private void MoveToNextPoint()
	{
	}

	public void Stop()
	{
	}

	public void FaceLeft()
	{
	}

	public void FaceRight()
	{
	}

	private void Update()
	{
	}
}
