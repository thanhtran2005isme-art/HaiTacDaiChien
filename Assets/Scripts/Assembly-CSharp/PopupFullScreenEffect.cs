using System;
using UnityEngine;

public class PopupFullScreenEffect : MonoBehaviour
{
	public SpineObject m_SpineObject;

	public Action m_CloseCb;

	private void Start()
	{
	}

	public void LoadSpineEffect(string pathSpine, string animationName, Vector2 offsetSpine, float scale, float timeScale)
	{
	}

	private void AnimationCompleted()
	{
	}

	public static PopupFullScreenEffect ShowUp(string pathSpine, string animationName, Vector2 offsetSpine, float scale = 1f, float timeScale = 1f)
	{
		return null;
	}

	private void Update()
	{
	}
}
