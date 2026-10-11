using System;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAvatar : MonoBehaviour
{
	public Image m_ImageAvatar;

	public Text m_TextLevel;

	public GameObject m_GoTagLevel;

	public SkeletonGraphic m_EffectSpecial;

	private Action m_ClickCb;

	private void Start()
	{
	}

	public void SetEmpty()
	{
	}

	public void SetAvatarInfo(int type, int heroKey, int level = -1)
	{
	}

	public void SetAvatar(int type, int heroKey, int level = -1)
	{
	}

	public void SetAvatarFrame(int frameId)
	{
	}

	public void SetClickCallback(Action clickCb)
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
