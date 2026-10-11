using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGuildAvatar : MonoBehaviour
{
	[HideInInspector]
	public int m_GuildAvatarId;

	public Image m_ImageAvatar;

	public Action<int> m_AvatarClickCb;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetAvatar(int avatarId)
	{
	}

	public void OnClick()
	{
	}
}
