using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class LibEvent : MonoBehaviour
{
	public static string URL_BASE;

	public static string stringClipBoardKhoiLoiOnlyXmasSnowFlake;

	public static Action cb_Update_Parent_Popup;

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

	public static void ShowListGameItem(List<BonusModel> lstItemBonus, Transform parent, List<GameItem> items, bool activeBG = true)
	{
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

	public static void ParseDataBuyItem(string json, string TextView = "", int ITEM_KEY = 0)
	{
	}

	public static List<HeroModel> CreateListHeroModel()
	{
		return null;
	}

	public static void BuyPack(int m_NumberPurchaseLeft, string m_IapData, int m_Price, Action cb)
	{
	}

	public static void PoolItem(VerticalPoolGroup poolItem)
	{
	}

	public static void SetActiveGameObject(GameObject obj, bool value)
	{
	}

	public static List<GameItem> CreateNewListGameItem()
	{
		return null;
	}

	public static List<BonusModel> CreateListBM()
	{
		return null;
	}

	public static List<BonusModel> AddItemToListBM(List<BonusModel> m_listItem, BonusModel bm)
	{
		return null;
	}

	public static JSONArray ConvertStringToJsonArray(string json)
	{
		return null;
	}

	public static Vector2 CreateNewVector2(float x, float y)
	{
		return default;
	}

	public static void ConverStringAndShowItem(string json)
	{
	}

	public static void DestroyChildGameObject(GameObject obj)
	{
	}

	public static Color CreateNewColor(float r, float g, float b, float a = 255f)
	{
		return default;
	}

	public static Sprite LoadSpriteFromResource(string path)
	{
		return null;
	}

	public static List<Sprite> LoadListSpriteToJsonArray(string url)
	{
		return null;
	}

	public static GameObject LoadGameObjectChild(GameObject obj, int index)
	{
		return null;
	}

	public static bool CheckNullString(string str)
	{
		return false;
	}

	public static Sprite GetSpriteFromStringJson(string str)
	{
		return null;
	}

	public static long GetPriceFromStringJson(string str)
	{
		return 0L;
	}

	public static long GetNumberFromStringJson(string str)
	{
		return 0L;
	}

	public static List<HeroModel> ParseListHeroKeyToListHeroModel(List<int> lstHeroKey)
	{
		return null;
	}

	public static ItemConsumable GetItemConsumableBonusSnowFlake(List<HeroModel> lstHero, string take3Sao)
	{
		return null;
	}

	public static List<object> CaculaterPreviewBonusSnowFlake(List<HeroModel> lstHero, string take3Sao, string take4Sao, string take5Sao)
	{
		return null;
	}

	public static string CovertUrlForSnowFlake(string url, List<int> lstHeroId)
	{
		return null;
	}

	public static List<string> LoadListItemToJsonArray(string url)
	{
		return null;
	}

	public static Vector3 CreateNewVector3(float x, float y, float z)
	{
		return default;
	}

	public void AddTurn(string exchangeUrl, string jsonCost, int maxItem, string m_textNumberHornBuy, int numberCanBuy, int eventId)
	{
	}

	public void ResquestBuyTurn(int numberTurn, string exchangeUrl, int evenId)
	{
	}

	public static List<int> CreateNewListInt()
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

	public static string[] SplitStringByKey(string str, string key)
	{
		return null;
	}

	public static Sprite LoadSpriteByUrl(string url)
	{
		return null;
	}

	public static void SetItemAfterRequest(List<object> lstiIem, bool checkAdd = true)
	{
	}
}
