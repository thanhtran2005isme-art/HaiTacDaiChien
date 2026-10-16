using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class LuaLibrary
{
	public static string URL_BASE;

	public static string ConvertUrlAllEvents(string urlEvent, string eventId = "", string numberBuy = "", string typeEvent = "", string idxReward = "", string wishItem = "", string heroId = "")
	{
		return null;
	}

	public static ItemConsumable ConvertListLongDataToOneItemConsumable(List<long> lstBonusData)
	{
		return null;
	}

	public static List<long> ConvertJsonArrayToListLong(JSONArray json)
	{
		return null;
	}

	public static object[] ConvertStringToArrayObject(string input)
	{
		return null;
	}

	public static ItemConsumable ConvertJsonArrayToOneItemConsumable(JSONArray json)
	{
		return null;
	}

	public static object ConvertStringToOneObject(string str)
	{
		return null;
	}

	public static object ConvertStringToListObject(string str)
	{
		return null;
	}

	public static BonusModel ConverJsonArrayToBonusModel(JSONArray json)
	{
		return null;
	}

	public static List<BonusModel> ConverJsonArrayToListBonusModel(JSONArray json)
	{
		return null;
	}

	public static BonusModel ConverListJsonArrayToBonusModel(JSONArray json)
	{
		return null;
	}

	public static int GetNumberItemFromJsonArray(List<BonusModel> items)
	{
		return 0;
	}

	public static JSONArray ConvertStringToJsonArray(string json)
	{
		return null;
	}

	public static List<HeroModel> CreateListHeroModel()
	{
		return null;
	}

	public static void BuyPack(int m_NumberPurchaseLeft, string m_IapData, int m_Price, Action cb)
	{
	}

	public static Sprite LoadSpriteFromResource(string path)
	{
		return null;
	}

	public static List<GameItem> CreateNewListGameItem()
	{
		return null;
	}

	public static List<BonusModel> CreateListBM()
	{
		return null;
	}

	public static List<int> CreateNewListInt()
	{
		return null;
	}

	public static List<long> CreateNewListLong()
	{
		return null;
	}

	public static List<string> CreateNewListString()
	{
		return null;
	}

	public static Dictionary<int, GameObject> CreateDictionaryGameObject()
	{
		return null;
	}

	public static string[] SplitStringByKey(string str, char key = ';')
	{
		return null;
	}

	public static Sprite LoadSpriteByUrl(string url)
	{
		return null;
	}

	public static List<GameObject> CreateNewListGameObject()
	{
		return null;
	}

	public static HeroModel ParseHeroModel(object data)
	{
		return null;
	}

	public static HeroModel CreateHeroModel()
	{
		return null;
	}

	public static ItemConsumable ConvertStringToItemConsumable(string str)
	{
		return null;
	}

	public static byte[] ParseObjectToByteArray(object data)
	{
		return null;
	}
}
