using System;
using System.Collections.Generic;

public class ChatCategoryData
{
	public List<ChatModel> m_ListChatModel;

	public long m_LastReqTime;

	public DateTime m_TimeSetDataAtClient;

	public ChatCategory m_ChatCategory;

	public float ElapsedSecondFromLastChatData()
	{
		return 0f;
	}

	public void MarkNeedRequestChat()
	{
	}

	public int CountChatLine()
	{
		return 0;
	}

	public List<ChatModel> GetLastChatLine(int count)
	{
		return null;
	}
}
