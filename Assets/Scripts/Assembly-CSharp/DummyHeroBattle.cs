using System;
using UnityEngine;

public class DummyHeroBattle : MonoRendererSortable
{
	private Vector2 SIZE_PLAYER_BASE;

	[SerializeField]
	private HeroModelController m_ModelController;

	[SerializeField]
	private GameObject m_GoDirection;

	private float m_HeroScale;

	[SerializeField]
	private SpriteRenderer m_SpireRenderShadow;

	[SerializeField]
	private SpriteRenderer m_SpireRenderSelection;

	private GameDirection m_FacingDirection;

	private Transform m_TfHero;

	[HideInInspector]
	public int m_HeroSpineId;

	[HideInInspector]
	public int m_TeamIdBattle;

	public GameObject m_GoPointHealthBar;

	private HeroActionBattle m_ActiveHeroAction;

	private Action<HeroAnimation> m_AnimationCompletedCb;

	private Vector2 _posHealthBar;

	private Vector2 m_SizeHero;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void SetColliderTrigger(bool isTrigger)
	{
	}

	public void LoadHeroInfo(int heroIdInBattle, int spineId)
	{
	}

	public void SetAnimationCompletedCb(Action<HeroAnimation> aniCompletedCb)
	{
	}

	public void SetScale(float scale)
	{
	}

	public Vector2 GetHeroSize()
	{
		return default;
	}

	public void DestroyHero()
	{
	}

	public Vector2 GetPointHeathBar()
	{
		return default;
	}

	public override void SetSortingOrder(int order)
	{
	}

	public override int GetSortingOrder()
	{
		return 0;
	}

	public override Vector2 GetPositionForSorting()
	{
		return default;
	}

	public GameDirection GetFacingDirection()
	{
		return (GameDirection)0;
	}

	public bool IsFacingRight()
	{
		return false;
	}

	public void SetFacingDirection(GameDirection direction)
	{
	}

	public void SetFaceToPoint(Vector2 pointFaceTo)
	{
	}

	public void SetSelected(bool selected)
	{
	}

	public void SetPosition(Vector2 position)
	{
	}

	public Vector2 GetPosition()
	{
		return default;
	}

	public Transform GetTransform()
	{
		return null;
	}

	public void SetHeroActive(bool active)
	{
	}

	public bool IsActive()
	{
		return false;
	}

	public void PlayRun()
	{
	}

	public void PlayIdle()
	{
	}

	public void PlayAttack(string animationName)
	{
	}

	public void PlayDie()
	{
	}

	public void PlayKnockUp()
	{
	}

	public void PlayKnockBack()
	{
	}

	public void PauseAnimation()
	{
	}

	public void SetAction(HeroActionBattle heroActionNew)
	{
	}

	public Vector2 GetPointHeadBar()
	{
		return default;
	}

	public Vector2 GetPointFireProjectile()
	{
		return default;
	}

	public Vector2 GetPointSkillHit()
	{
		return default;
	}

	public Vector2 GetPointLostHpEffect()
	{
		return default;
	}

	private void Update()
	{
	}
}
