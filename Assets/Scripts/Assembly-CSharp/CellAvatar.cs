using System;
using UnityEngine;
using UnityEngine.UI;

public class CellAvatar : MonoBehaviour
{
	public Image m_ImageAvatar;

	public GameObject m_GoCheck;

	public GameObject m_EffectSpecial;

	public Action<AvatarInfo> m_ClickCallback;

	private AvatarInfo m_AvatarInfo;

	private void Start()
	{
	}

	public void SetAvatarInfo(AvatarInfo avtInfo)
	{
	}

	public void OnClick()
	{
	}
}
