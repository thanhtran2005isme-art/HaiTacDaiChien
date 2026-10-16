using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotChat : MonoEventHandler
{
	public GameObject m_GroupMessage;

	public List<Text> m_lstChat;

	private float _timerReload;

	private float _timerHideMessage;

	protected override void Start()
	{
	}

	private void ShowListChat(List<ChatModel> lstChatModel)
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
