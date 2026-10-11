using System;
using UnityEngine;
using UnityEngine.UI;

public class CellServer : MonoBehaviour
{
	public Text m_TextServerName;

	public GameObject m_GoNew;

	public GameObject m_GoAvatar;

	public Image m_ImageAvatar;

	public Text m_TextLevel;

	public Text m_TextPlayerName;

	private ServerInfo m_ServerInfo;

	private Action<ServerInfo> m_ClickCallback;

	private void Start()
	{
	}

	public void SetServerInfo(ServerInfo si)
	{
	}

	public void OnClick()
	{
	}

	public void SetClickCallback(Action<ServerInfo> cb)
	{
	}

	private void Update()
	{
	}
}
