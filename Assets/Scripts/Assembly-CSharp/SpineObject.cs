using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SpineObject : MonoBehaviour
{
	public SkeletonGraphic m_SkeletonGraphic;

	public GameObject m_GoMesh;

	private string m_AnimationDestroy;

	public Action m_OnDisapperCb;

	private float _timeScale;

	protected virtual void Start()
	{
	}

	public void LoadSpine(string pathSpine)
	{
	}

	public void SetPosition(Vector2 position)
	{
	}

	public void SetTimeScale(float timeScale)
	{
	}

	public void SetScale(float scale)
	{
	}

	public void SetScale(float scaleX, float scaleY)
	{
	}

	public void ShowObject()
	{
	}

	public void HideObject()
	{
	}

	public void PlayAnimationLoop(string aniName)
	{
	}

	public void PlayAnimationOnce(string aniName, string aniLoopAfter = "")
	{
	}

	public void PlayAnimationOnce(List<string> lstAniName, string aniLoopAfter = "")
	{
	}

	private void Update()
	{
	}
}
