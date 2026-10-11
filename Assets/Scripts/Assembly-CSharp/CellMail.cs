using System;
using UnityEngine;
using UnityEngine.UI;

public class CellMail : MonoBehaviour
{
	public GameObject m_GoBgrActive;

	public GameObject m_GoBgrInactive;

	public Text m_TextTitle;

	public Text m_TextTime;

	public Image m_MailIcon;

	public Action<MailContent> m_MailClickCb;

	private MailContent m_Mail;

	public GameObject m_GoTagUnread;

	private void Start()
	{
	}

	public void SetMail(MailContent mail)
	{
	}

	public void MailClick()
	{
	}

	private void Update()
	{
	}
}
