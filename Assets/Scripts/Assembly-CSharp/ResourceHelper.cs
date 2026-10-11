using System.Collections.Generic;
using SimpleJSON;
using Spine.Unity;
using UnityEngine;

public class ResourceHelper
{
	private const string PATH_ITEM_NULL = "Images/Common/item_null";

	private const string PATH_AVATAR_NULL = "Images/Common/item_null";

	private const bool LOAD_BUILD_IN_RES_PACK_2 = false;

	public static void LoadBattleSkillSpine(SkeletonAnimation skeletonAnimation, string pathSkeleton)
	{
	}

	public static Sprite LoadEventSpriteForNewEvent(string pathImage)
	{
		return null;
	}

	public static Sprite LoadEventSprite(string pathImage, bool loadDefaultIcon = true)
	{
		return null;
	}

	public static void LoadEventSkeleton(SkeletonGraphic ani, string pathSpine)
	{
	}

	public static Sprite LoadBattleSkillSprite(string pathSprite)
	{
		return null;
	}

	public static void LoadHeroBodyEffectSpine(SkeletonAnimation skeletonAnimation, string pathSkeleton)
	{
	}

	public static Sprite LoadHeroBuffEffectSprite(int clientBuffId)
	{
		return null;
	}

	private static string AdjustPathToPack2(string pathInput)
	{
		return null;
	}

	private static int FindBundleIdForResourcePath(string pathRes)
	{
		return 0;
	}

	private static string GetAssetBundleName(int bundleId)
	{
		return null;
	}

	public static string GetHeroFactionName(HeroFaction faction)
	{
		return null;
	}

	public static string GetHeroClassname(HeroClass heroClass)
	{
		return null;
	}

	public static GameObject LoadPrefabBgrLogin()
	{
		return null;
	}

	public static Sprite LoadHeroSkinThumb(int skinKey)
	{
		return null;
	}

	public static Sprite LoadHeroSkinHead(int skinKey)
	{
		return null;
	}

	public static Sprite LoadHeroSkinArt(int skinKey)
	{
		return null;
	}

	public static void LoadHeroSkinSpine(SkeletonAnimation skeletonAnimation, int skinKey)
	{
	}

	public static void LoadHeroSkinSpineCanvas(SkeletonGraphic ani, int skinKey)
	{
	}

	public static GameObject LoadLuaPrefab(string name)
	{
		return null;
	}

	public static Sprite LoadLuaSprite(string name)
	{
		return null;
	}

	public static string LoadLuaScript(string fileName)
	{
		return null;
	}

	public static void LoadHeroSpine(SkeletonAnimation skeletonAnimation, int heroKey)
	{
	}

	public static void LoadHeroSpineCanvas(SkeletonGraphic ani, int heroKey)
	{
	}

	public static void LoadHeroSpineCanvasFk(SkeletonGraphic ani, int heroKey)
	{
	}

	public static string LoadServerLanguageContent(string fileName)
	{
		return null;
	}

	private static string LoadDbTextContentByLanguage(string fileDb, Language language)
	{
		return null;
	}

	public static string LoadDbTextContent(string fileDb)
	{
		return null;
	}

	public static Sprite LoadPetHeadSprite(int petId, int petTier)
	{
		return null;
	}

	public static Sprite LoadPetSprite(int petId, int petTier, bool blackAndWhite)
	{
		return null;
	}

	public static void LoadPetSpine(SkeletonAnimation skeletonAnimation, int petId, int petTier = 1)
	{
	}

	public static void LoadPetSpineCanvas(SkeletonGraphic ani, int petId, int petTier = 1)
	{
	}

	public static void LoadPetSpineHomeCanvas(SkeletonGraphic ani, int petId, int petTier = 1)
	{
	}

	public static Sprite LoadAvatarFrameSprite(int avtFrameKey)
	{
		return null;
	}

	public static Sprite LoadAvatarSprite(int type, int avatarId)
	{
		return null;
	}

	public static void LoadEffAvatarSpineCanvas(SkeletonGraphic ani, string pathSpine)
	{
	}

	public static Sprite LoadHeroESkillSprite(int eSkillId)
	{
		return null;
	}

	public static Sprite LoadPetSkillSprite(int petId, int iconId)
	{
		return null;
	}

	public static Sprite LoadHeroSkillSprite(int iconId)
	{
		return null;
	}

	public static Sprite LoadHeroHeadSprite(int heroKey)
	{
		return null;
	}

	public static Sprite LoadHeroArtSprite(int heroKey)
	{
		return null;
	}

	public static Sprite LoadHeroThumbSprite(int heroKey)
	{
		return null;
	}

	public static Sprite LoadHeadAvatarSprite(int type, int avatarKey)
	{
		return null;
	}

	public static Sprite LoadItemEquipmentSprite(int itemKey)
	{
		return null;
	}

	public static ItemConsumable ConvertListLongDataToOneItemConsumable(List<long> lstBonusData)
	{
		return null;
	}

	public static ItemConsumable ConvertJsonArrayToOneItemConsumable(JSONArray jarr)
	{
		return null;
	}

	public static Sprite LoadItemConsumableSprite(int itemKey)
	{
		return null;
	}

	public static Sprite LoadItemShardSprite(int itemKey)
	{
		return null;
	}

	public static Sprite LoadItemArtifactSprite(int itemKey)
	{
		return null;
	}

	public static Sprite LoadGuildAvatarSprite(int guildAvatarId)
	{
		return null;
	}

	public static Sprite LoadHeroStoneSprite(int typeStone)
	{
		return null;
	}

	public static Sprite LoadHeroQualiySprite(int heroQuality)
	{
		return null;
	}

	public static Sprite LoadFameQualiySprite(int heroQuality)
	{
		return null;
	}

	public static Sprite LoadFactionSprite(HeroFaction faction)
	{
		return null;
	}

	public static Sprite LoadClassIconSprite(HeroClass heroClass)
	{
		return null;
	}

	public static Sprite LoadClassSprite(HeroClass heroClass)
	{
		return null;
	}

	public static Sprite LoadItemByKey(object item)
	{
		return null;
	}

	public static Sprite LoadThemePrepareSprite(string nameTheme)
	{
		return null;
	}
}
