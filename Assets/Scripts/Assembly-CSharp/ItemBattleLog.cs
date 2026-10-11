using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBattleLog : MonoBehaviour
{
	[SerializeField]
	private Text m_TextTeam1;

	[SerializeField]
	private Text m_TextTeam2;

	[SerializeField]
	private Text m_TextWin;

	[SerializeField]
	private Text m_TextLost;

	[SerializeField]
	private Text m_TextAtkPower1;

	[SerializeField]
	private Text m_TextAtkPower2;

	[SerializeField]
	private GameObject m_GoStatusWin;

	[SerializeField]
	private GameObject m_GoStatusLost;

	[SerializeField]
	private GameObject m_DoiHinh1;

	[SerializeField]
	private GameObject m_DoiHinh2;

	[SerializeField]
	private List<Image> Team1;

	[SerializeField]
	private List<Image> Team2;

	[SerializeField]
	private PlayerAvatar m_Avatar1;

	[SerializeField]
	private PlayerAvatar m_Avatar2;

	public Action<DataItemBattleLog> m_ItemClickCB;

	public Action<DataItemBattleLog> m_ItemAllClickCB;

	private DataItemBattleLog itemData;

	public void SetData(DataItemBattleLog data)
	{
	}

	public void ButtonShowInfoClick()
	{
	}

	public void ButtonCloseDoiHinhClick()
	{
	}

	public void ButtonShowAllInfoClick()
	{
	}
}
