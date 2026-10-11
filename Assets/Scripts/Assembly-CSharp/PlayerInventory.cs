using System.Collections.Generic;
using pbdson;

public class PlayerInventory
{
	public const int ITEM_KEY_BASIC_SUMMON_SCROLL = 14;

	public const int ITEM_KEY_HEROIC_SUMMON_SCROLL = 15;

	public const int ITEM_KEY_PROPHET_ORB = 16;

	private List<HeroModel> m_LstHeroModel;

	private List<PetModel> m_LstPetModel;

	public List<ItemEquipment> m_LstItemEquipment;

	public List<ItemConsumable> m_LstItemConsumable;

	public List<ItemShard> m_LstItemShard;

	public List<ItemArtifact> m_LstItemArtifact;

	private List<HeroSkin> m_LstHeroSkin;

	private static PlayerInventory m_Instance;

	public static PlayerInventory GetInstance()
	{
		return null;
	}

	private PlayerInventory()
	{
	}

	public void ClearInventory()
	{
	}

	public List<HeroModel> GetListHeroModel(bool sort = true)
	{
		return null;
	}

	public void DebugESKill()
	{
	}

	public void UpdateFromBonusModel(BonusModel bm)
	{
	}

	public PetModel GetPetModel(int petId)
	{
		return null;
	}

	public void UpdatePetTier(int petId, int petTier)
	{
	}

	public void UpdatePetLevel(int petId, int petLevel)
	{
	}

	public int GetPetTier(int petId)
	{
		return 0;
	}

	public List<int> GetListIdOfAllHeroes()
	{
		return null;
	}

	public HeroModel GetHeroModelById(int heroId)
	{
		return null;
	}

	public List<HeroModel> GetListHeroModelById(List<int> lstHeroId)
	{
		return null;
	}

	public void AddHeroModel(HeroModel hm)
	{
	}

	public void SetHeroModels(List<HeroModel> lstHm)
	{
	}

	public void SetPetModels(List<PetModel> lstPet)
	{
	}

	public void UpdateLockStatusCrystalCrown(List<int> lstHeroIdCrystal)
	{
	}

	public void UpdateLockStatusTrialOfChampion(List<int> lstHeroIdLocked)
	{
	}

	public void UpdateLockStatusAllStarsLeague(List<int> lstHeroIdLocked)
	{
	}

	public void UpdateHeroData(PbHero pbHero)
	{
	}

	public void RemoveHeroModel(int heroId)
	{
	}

	public void RemoveHeroModels(List<int> lstHeroId)
	{
	}

	private void CheckCanFuseHero5StarToNotify()
	{
	}

	public void SetListSkin(List<HeroSkin> lstHeroSkin)
	{
	}

	public List<HeroSkin> GetAllHeroSkins()
	{
		return null;
	}

	public List<HeroSkin> GetHeroSkinByHeroKey(int heroKey)
	{
		return null;
	}

	public void SetHeroSkinEquiped(int heroIdEquip, int newSkinKey)
	{
	}

	private void ReturnHeroSkin(int skinKeyReturn, int numberReturn)
	{
	}

	private void EquipHeroSkin(int skinKeyEquip)
	{
	}

	public bool IsHeroSkinAvaiable(int skinKey)
	{
		return false;
	}

	public void AddItemArtifact(int artifactKey, int star, int numberAdd)
	{
	}

	public void RemoveItemArtifact(int artifactkey, int star, int numberRemove)
	{
	}

	public void ReturnArtifact(int artKey, int artStar, int numberReturn = 1)
	{
	}

	public bool HasShardToMerge()
	{
		return false;
	}

	public ItemShard GetShard(int shardKey, int shardType)
	{
		return null;
	}

	public void AddItemShard(int shardKey, int shardType, int numberAdd)
	{
	}

	public void RemoveItemShard(int shardKey, int shardType, int numberRemove)
	{
	}

	public ItemEquipment GetItemEquipment(int equipmentKey)
	{
		return null;
	}

	public long CountItemEquipment(int itemEquipmentKey)
	{
		return 0L;
	}

	public void AddItemEquipment(int equipmentKey, int numberAdd)
	{
	}

	public void RemoveItemEquipment(int equipmentKey, int numberRemove)
	{
	}

	public void ReturnItemEquipment(int equipmentKey, int numberReturn = 1)
	{
	}

	public void UpdateNumberItemUsed()
	{
	}

	public void SetItemEquipmentModels(List<ItemEquipment> lstItem)
	{
	}

	public void SetItemArtifactModels(List<ItemArtifact> lstItem)
	{
	}

	public void SetItemConsumableModels(List<ItemConsumable> lstItem)
	{
	}

	public void SetItemShardModels(List<ItemShard> lstItem)
	{
	}

	public long GetNumberItemConsumable(int itemConsumekey)
	{
		return 0L;
	}

	public long CountItemConsumable(int itemKey)
	{
		return 0L;
	}

	public void SetNumberItemConsumable(int itemKey, long number)
	{
	}

	public void SetItemConsumable(int itemKey, long numberCurrent)
	{
	}

	public void AddItemConsumable(int itemKey, long numberAdd)
	{
	}

	public void RemoveItemConsumable(int itemKey, int numberRemove)
	{
	}
}
