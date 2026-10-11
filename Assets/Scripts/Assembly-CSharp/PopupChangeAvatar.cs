using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupChangeAvatar : BasePopup
{
	private const int TAB_NORMAL = 0;

	private const int TAB_SPECIAL = 1;

	public Text m_TextTitle;

	public GridPoolGroup m_PoolAvatar;

	public GameObject m_GoCellAvatar;

	public GameObject m_GoButtonSave;

	public GameObject m_GoButtonSaveDis;

	public Transform m_TabGroup;

	public Action<int, int> m_ChangeAvatarSuccessCb;

	private int m_CurrentAvatarId;

	private int m_CurrentType;

	private int m_TypeAvatarSelected;

	private int m_SelectedAvatarId;

	private List<AvatarInfo> m_LstAvatarNormal;

	private List<AvatarInfo> m_LstAvatarSpecial;

	private int currTab;

	private void TabClick(int idTab)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void SetAvatarSelected(int type, int avtId)
	{
	}

	public void SetData(int type, int currentAvatarId)
	{
	}

	public void ButtonChangeClick()
	{
	}

	public static PopupChangeAvatar ShowUp(int type, int currentAvatarId)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
