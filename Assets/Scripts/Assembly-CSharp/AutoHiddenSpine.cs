using Spine.Unity;
using UnityEngine;

public class AutoHiddenSpine : MonoBehaviour
{
	[SerializeField]
	private SkeletonAnimation m_SkeletonAnimation;

	private void OnBecameVisible()
	{
	}

	private void OnBecameInvisible()
	{
	}
}
