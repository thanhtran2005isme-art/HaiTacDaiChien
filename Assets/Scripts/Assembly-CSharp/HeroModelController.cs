using System;
using UnityEngine;

public abstract class HeroModelController : MonoBehaviour
{
	public Action<string> m_AnimationCompletedCb;

	public abstract void PlayAnimationByName(string animationName);

	public abstract void PlayAnimation(HeroAnimation heroAni);

	public abstract bool LoadHeroModel(int heroKey);

	public abstract void SetSortingLayerOrder(int order);

	public abstract int GetSortingLayerOrder();

	public abstract HeroAnimation GetHeroAnimationType(string animationName);

	public void SetScale(float scale)
	{
	}
}
