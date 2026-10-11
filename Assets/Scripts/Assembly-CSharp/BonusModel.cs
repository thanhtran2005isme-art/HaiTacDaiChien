using System.Collections.Generic;

public class BonusModel
{
	public List<BonusHero> m_LstBonusHero;

	public List<BonusItemEquipment> m_LstBonusEquipment;

	public List<BonusItemConsumable> m_LstBonusConsumable;

	public List<BonusItemShard> m_LstBonusShard;

	public List<BonusItemArtifact> m_LstBonusArtifact;

	public List<BonusAvatar> m_LstBonusAvatar;

	public List<BonusVipExp> m_LstBonusVipExp;

	private List<object> m_LstObjectModelExtra;

	private const int BONUS_GOLD = 1;

	private const int BONUS_GEM = 2;

	private const int BONUS_MATERIAL = 3;

	private const int BONUS_EXP = 4;

	private const int BONUS_HERO = 5;

	private const int BONUS_EQUIPMENT = 6;

	private const int BONUS_ARTIFACT = 7;

	private const int BONUS_AVATAR = 8;

	private const int BONUS_VIP_EXP = 9;

	public const int MATERIAL_TYPE_CONSUMABLE = 1;

	public const int MATERIAL_TYPE_HERO_SHARD = 2;

	public const int MATERIAL_TYPE_ARTIFACT_SHARD = 3;

	public static BonusModel ParseBonusPreview(List<long> lstLong)
	{
		return null;
	}

	public static BonusModel RandomBonus(int numBonus = -1)
	{
		return null;
	}

	public bool IsEmpty()
	{
		return false;
	}

	public ItemConsumable GetFirstItemConsumable()
	{
		return null;
	}

	public void SetListObjectModelExtra(List<object> lstObjModelExtra)
	{
	}

	public object RemoveItemBonus(int indexRemove)
	{
		return null;
	}

	public List<object> ToListModelObject(bool getNegativeItemNumber = false)
	{
		return null;
	}

	public bool HasGreaterThanZeroItem()
	{
		return false;
	}

	public List<ItemModel> ToListItemModelByOrder()
	{
		return null;
	}

	private int GetTotalItemForIterate()
	{
		return 0;
	}

	public void PrintOut()
	{
	}

	private static int FetchBonusItemPreview(BonusModel bm, List<long> lstData, int cursorIndex, int rewardIndex)
	{
		return 0;
	}

	public static BonusModel ParseBonusReceived(List<long> lstLong, bool addBonusToInventory = true)
	{
		return null;
	}

	private static int FetchBonusItemReceived(BonusModel bm, List<long> lstData, int cursorIndex, int rewardIndex, bool addBonusToInventory = true)
	{
		return 0;
	}

	private static void UpdateAvatarToGameContext(int type, int avatarId)
	{
	}

	private static void UpdateVipExpToGameContext(int levelVip, int vipExp)
	{
	}

	private static void UpdateItemNumberToGameContext(int itemKey, long itemNumber)
	{
	}

	public static void TrackingLevelAchieved(int lastLevel, int nowLevel)
	{
	}

	public static void TrackingVipAchieved(int currentVip, int lastVip)
	{
	}

	public static bool CheckNumberItemEnough(object bm)
	{
		return false;
	}

	public static long GetNumberItemInInventory(object bm)
	{
		return 0L;
	}

	public static long GetNumberItemObj(object item)
	{
		return 0L;
	}
}
