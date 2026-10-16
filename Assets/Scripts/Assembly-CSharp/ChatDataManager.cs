using System.Collections.Generic;

public class ChatDataManager
{
	private static ChatDataManager _instance;

	private Dictionary<ChatCategory, ChatCategoryData> _dictChatData;

	public static ChatDataManager Instance => null;

	private ChatDataManager()
	{
	}

	public void Clear()
	{
	}

	public void AddChat(ChatCategory chatCat, List<ChatModel> lstChatModel, long lastRequestTime)
	{
	}

	public ChatCategoryData GetChatData(ChatCategory chatCat)
	{
		return null;
	}
}
