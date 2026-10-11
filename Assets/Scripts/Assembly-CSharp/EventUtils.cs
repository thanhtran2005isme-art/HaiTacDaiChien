using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventUtils
{
	private const string PARAM_USER_ID_PLACE_HOLDER = "{userId}";

	private const string PARAM_SERVER_ID_PLACE_HOLDER = "{serverId}";

	private const string PARAM_EVENT_ID_PLACE_HOLDER = "{eventId}";

	private static int _countGenService;

	public const int SERVICE_STARTER_VALUE = 10000;

	public static int GenServiceEventStarter()
	{
		return 0;
	}

	public static string GetNameSceneEvent()
	{
		return null;
	}

	public static void CenterContentToScrollView(ScrollRect scrollRect)
	{
	}

	public static void DisableScrollViewIfContentNotFull(ScrollRect scrollRect)
	{
	}

	public static string ReplaceUrlEventCommonParameters(string urlRaw)
	{
		return null;
	}

	public static string ReplaceUrlEventParametersWithEventId(string urlRaw, int eventId)
	{
		return null;
	}

	public static List<long> ParseListJSONLong(string jArrLong)
	{
		return null;
	}

	public static string GetIapPriceLocalized(string productId)
	{
		return null;
	}

	public static void FillListItemModelToListGameItem(List<object> lstItemModel, List<GameObject> lstGoGameItem, Transform tfItemContainer, float gameItemScale = 1f)
	{
	}

	public static void FillListItemModelToListGameItem2(List<object> lstItemModel, List<GameObject> lstGoGameItemWrapper, Transform tfItemContainer, float gameItemScale = 1f)
	{
	}

	public static List<GameObject> FillListItemModelToListGameItem2(List<object> lstItemModel, Transform tfItemContainer, float gameItemScale = 1f, bool checkOn = false)
	{
		return null;
	}
}
