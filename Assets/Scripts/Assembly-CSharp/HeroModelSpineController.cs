using Spine;
using Spine.Unity;

public class HeroModelSpineController : HeroModelController
{
	public SkeletonAnimation m_SpineAnimation;

	private AnimationState m_AnimationState;

	public const string ANI_IDLE = "idling";

	public const string ANI_RUN = "run";

	public const string ANI_DIE = "dead";

	public const string ANI_KNOCK_UP = "swipe";

	public const string ANI_KNOCK_BACK = "damage";

	private void Start()
	{
	}

	private void Update()
	{
	}

	public override HeroAnimation GetHeroAnimationType(string animationName)
	{
		return (HeroAnimation)0;
	}

	public override void PlayAnimationByName(string animationName)
	{
	}

	public override void PlayAnimation(HeroAnimation ani)
	{
	}

	public override void SetSortingLayerOrder(int order)
	{
	}

	public override int GetSortingLayerOrder()
	{
		return 0;
	}

	public override bool LoadHeroModel(int spineId)
	{
		return false;
	}
}
