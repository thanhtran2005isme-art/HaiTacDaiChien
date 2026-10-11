using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using pbdson;

public class Utils
{
	public static string GetCp()
	{
		return null;
	}

	public static void IosAskingForPermissionForIDFA()
	{
	}

	public static string GetClientLanguageCode()
	{
		return null;
	}

	public static byte[] GzipCompress(byte[] raw)
	{
		return null;
	}

	public static byte[] GzipDecompress(byte[] raw)
	{
		return null;
	}

	public static PbAction GenNetworkErrorPbAction(int serviceId)
	{
		return null;
	}

	public static float getAngle(Vector3 fromPosition, Vector3 toPosition)
	{
		return 0f;
	}

	public static string GetNetworkOperatorName()
	{
		return null;
	}

	public static void LoadWorldScene(PopupId popupIdOpen = PopupId.PopupNone)
	{
	}

	public static void LoadHome2Scene(PopupId popupIdOpen = PopupId.PopupNone)
	{
	}

	public static void LoadScene(string sceneName, PopupId popupIdOpen = PopupId.PopupNone)
	{
	}

	public static void LoadBattleScene(ListBattleData lstBattleData, string sceneBackAfterBattle, PopupId popupIdOpen = PopupId.PopupNone)
	{
	}

	public static PaddingInfo CalculateSafePadding(Vector2 canvasSize)
	{
		return null;
	}

	public static void LoadSpineSkeletonCanvas(SkeletonGraphic ani, string asset, string animation = "", bool loop = true)
	{
	}

	public static void LoadSpineSkeleton(SkeletonAnimation ani, string pathSkeletonData)
	{
	}

	public static long CalculateHeroPower(long attack, long armor, long hp)
	{
		return 0L;
	}

	public static GuildRole GetGuildRoleFromServerPosition(int guildPosition)
	{
		return (GuildRole)0;
	}

	public static int ToGuildServerPositionFromRole(GuildRole role)
	{
		return 0;
	}

	public static Sprite GetSpriteOfShopCurrency(ShopCurrencyType type)
	{
		return null;
	}

	public static bool IsStringEmpty(string str)
	{
		return false;
	}

	public static string FormatHourFromSec(int sec)
	{
		return null;
	}

	private static string FormatPrefixZero(long value)
	{
		return null;
	}

	public static string FormatM(long value, bool roundUp = false)
	{
		return null;
	}

	public static string FormatK(long value, bool roundUp = false)
	{
		return null;
	}

	public static string FormatVan(long value)
	{
		return null;
	}

	public static string FormatFloat(float value, int numberDigitAfterComma = 1)
	{
		return null;
	}

	public static string FormatThreeDot(long value)
	{
		return null;
	}

	public static string FormatThousandSeperator(long value)
	{
		return null;
	}

	public static GameObject LoadPrefab(string pathPrefab)
	{
		return null;
	}

	public static Sprite createSprite(string path)
	{
		return null;
	}

	public static GameObject LoadPrefabAndInstantiate(string pathPrefab, Transform parent)
	{
		return null;
	}

	public static GameObject FindCanvas()
	{
		return null;
	}

	public static Vector2 ToVector2(Vector3 v3)
	{
		return default;
	}

	public static T RandomListValue<T>(List<T> lst)
	{
		return default;
	}

	public static bool GetAChance(int percent)
	{
		return false;
	}

	public static float RandomFloat(float from, float to)
	{
		return 0f;
	}

	public static int RandomInt(int from, int to)
	{
		return 0;
	}

	public static GameDirection ReverseDirection(GameDirection direction)
	{
		return (GameDirection)0;
	}

	public static void SetGoActiveState(GameObject go, bool active)
	{
	}

	public static string Md5Sum(string input)
	{
		return null;
	}

	public static List<object> GenListObject<T>(List<T> list)
	{
		return null;
	}

	public static List<T> FilterList<T>(List<T> list, List<int> lstIndexRemove)
	{
		return null;
	}

	public static List<string> ListLongToListString(List<long> lstInput)
	{
		return null;
	}

	public static List<int> ListLongToListInt(List<long> lstInput)
	{
		return null;
	}

	public static List<long> ListIntToListLong(List<int> lstInput)
	{
		return null;
	}

	public static string ConvertListToArrayJson<T>(List<T> list)
	{
		return null;
	}

	public static string ConvertListToString<T>(List<T> list)
	{
		return null;
	}

	public static void PrintList<T>(List<T> list, bool error = false)
	{
	}

	public static void PrintArrayInt(int[] list, bool isError = false)
	{
	}

	public static List<long> ConvertListIntToLong(List<int> lstInt)
	{
		return null;
	}

	public static List<int> ConvertListLongToInt(List<long> lstLong)
	{
		return null;
	}

	public static T GetListValue<T>(List<T> lstValue, int index)
	{
		return default;
	}

	public static void ShuffleList<T>(List<T> lstInput)
	{
	}

	public static List<T> CloneList<T>(List<T> lstInput)
	{
		return null;
	}

	public static string GetLastPathElement(string pathInput)
	{
		return null;
	}

	public static void tweenScale(GameObject target, Vector3 to, float duration = 0.5f, iTween.EaseType easeType = iTween.EaseType.easeInOutBack, string tweenCompleteCallback = "", GameObject targetCallback = null)
	{
	}

	public static void tweenPosition(GameObject target, Vector3 to, float duration = 0.5f, iTween.EaseType easeType = iTween.EaseType.easeInOutBack, string tweenCompleteCallback = "", GameObject targetCallback = null)
	{
	}

	public static Sprite GetBuildingCelestial(int type)
	{
		return null;
	}

	public static Sprite GetBuildingCelestialForUpgrade(int type, int level = 1)
	{
		return null;
	}

	public static Sprite GetBuildingTech(int type)
	{
		return null;
	}

	public static string GetTitleBuildCelestial(int mode, int type = -1)
	{
		return null;
	}

	public static void CallPaymentIAP(string productId, string extraInfo = "", Action successCB = null)
	{
	}

	public static void CallIndoPaymentIAP(string productId, string extraInfo, Action successCB)
	{
	}

	public static void CallGosuPaymentIAP(string productId, string extraInfo, Action successCB)
	{
	}

	public static void SetImgSizeNativeWithPercent(Image img, float percent)
	{
	}

	public static DropPositionType GetDropPosition(int typePosition)
	{
		return (DropPositionType)0;
	}

	public static string GetLabelPosition(DropPositionType m_TypePosition, int starEqui, bool checkNgheEqui, EquipmentQuality qualityEqui)
	{
		return null;
	}

	public static void GoToPos(DropPositionType m_TypePosition, int starEqui = -1, bool checkNgheEqui = false, EquipmentQuality qualityEqui = EquipmentQuality.UNKNOWN)
	{
	}

	public static void CompleteMission7Days(DropPositionType m_TypePosition)
	{
	}
}
