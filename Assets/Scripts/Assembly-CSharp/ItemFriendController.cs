using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFriendController : MonoBehaviour
{
	public PlayerAvatar avatar;

	public Text m_NameUser;

	public Text m_TimeOnline;

	public Text m_Level;

	public GameObject receiveBtn;

	public GameObject sendBtn;

	public GameObject bossBtn;

	public GameObject atkBtn;

	public OnPublicDataCallback smashFightBossCallback;

	public OnPublicDataCallback atkCallback;

	public OnPublicDataCallback receiveCallback;

	public OnPublicDataCallback sendCallback;

	public OnPublicDataCallback deleteCallback;

	public OnPublicMultilDataCallback bossFightCallback;

	private FriendModel friendData;

	private List<MonsterModel> listBoss;

	public void InitData(FriendModel data)
	{
	}

	public void AtkClick()
	{
	}

	public void ReceiveClick()
	{
	}

	public void SendClick()
	{
	}

	public void FightBoss()
	{
	}

	public void ItemClick()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
