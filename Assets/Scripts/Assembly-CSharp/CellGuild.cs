using System;
using UnityEngine;
using UnityEngine.UI;

public class CellGuild : MonoBehaviour
{
	public Image m_ImageAvatar;

	public Text m_TextGuildName;

	public Text m_TextMember;

	public Text m_TextLevel;

	private GuildInfo m_GuildInfo;

	public Action<int> m_ApplyGuildCb;

	private void Start()
	{
	}

	public void SetData(GuildInfo guildInfo)
	{
	}

	public void ButtonApplyGuildClick()
	{
	}

	private void Update()
	{
	}
}
