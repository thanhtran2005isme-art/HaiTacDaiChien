using System;
using Spine;
using Spine.Unity;
using UnityEngine;

public class MFPortal : MonoClickable, MFSlotObjectSortable
{
	[SerializeField]
	private SkeletonAnimation m_SkeletonAnimation;

	private Spine.AnimationState m_SpineAnimationState;

	private MeshRenderer m_Meshrender;

	[SerializeField]
	private SpriteRenderer m_SpriteShadow;

	[SerializeField]
	private Transform m_Transform;

	private void Start()
	{
	}

	public void SetClickCb(Action clickCb)
	{
	}

	public Vector2 GetMFObjectPosition()
	{
		return default;
	}

	public void PlayOpen()
	{
	}

	public void PlayClose()
	{
	}

	public void SetMFObjectSortingOrder(int sortingOrder)
	{
	}

	public void DestroySlotObject(bool characterStepInTo)
	{
	}
}
