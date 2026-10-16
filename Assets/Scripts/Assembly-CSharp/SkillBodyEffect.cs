using Spine;
using Spine.Unity;
using UnityEngine;

public class SkillBodyEffect : BattleObject
{
	[SerializeField]
	private SkeletonAnimation m_SkeletonAnimation;

	private Spine.AnimationState m_SpineAnimationState;

	private MeshRenderer m_Meshrender;

	private BattleHero m_BattleHero;

	private Vector2 m_OffsetEffect;

	private int m_OffsetOrder;

	private ActionRepeatTimer m_ActionUpdateOrder;

	public override void Start()
	{
	}

	public void SetLayerOrder(int orderInLayer)
	{
	}

	public int GetLayerOrder()
	{
		return 0;
	}

	public void SetHeroAttachedOn(BattleHero battleHero, Vector2 offsetEffect, int offsetOrder)
	{
	}

	public void PlayAnimationLoop(string animationStart, string animationLoop)
	{
	}

	public void PlayAnimationLoop(string aniName)
	{
	}

	public bool LoadSpine(string pathSkeleton, string keyToPool)
	{
		return false;
	}

	public override void PoolThis()
	{
	}

	private void ForceffectAttachToHero()
	{
	}

	private void Update()
	{
	}
}
