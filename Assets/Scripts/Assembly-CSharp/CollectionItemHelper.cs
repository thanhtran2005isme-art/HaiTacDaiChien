using System.Collections.Generic;

public class CollectionItemHelper
{
	public static void SortListEquipment(List<ItemEquipment> lstEquipment)
	{
	}

	public static void SortListArtifact(List<ItemArtifact> lstArtifact)
	{
	}

	public static void SortListShard(List<ItemShard> lstShard)
	{
	}

	private static T GetItemByKey<T>(List<T> lstModel, int itemKey, bool cloneCopy = false) where T : ItemModel
	{
		return null;
	}

	public static ItemEquipment GetEquipmentModelByKey(List<ItemEquipment> lstModel, int itemKey, bool cloneCopy = false)
	{
		return null;
	}

	public static List<ItemEquipment> FilterEquipmentByType(List<ItemEquipment> lstModel, EquipmentType type, bool cloneCopy = false)
	{
		return null;
	}

	public static int CountItemEquipment(List<ItemEquipment> lstModel, int itemKeyCount)
	{
		return 0;
	}

	public static ItemConsumable GetConsumableModelByKey(List<ItemConsumable> lstModel, int itemKey, bool cloneCopy = false)
	{
		return null;
	}

	public static ItemShard GetShardModelByKey(List<ItemShard> lstModel, int itemKey, bool cloneCopy = false)
	{
		return null;
	}

	public static ItemArtifact GetArtifactModelByKey(List<ItemArtifact> lstArtifact, int itemKey, bool cloneCopy = false)
	{
		return null;
	}

	public static List<ItemArtifact> FilteZeroArtifact(List<ItemArtifact> lstModel, bool cloneCopy = false)
	{
		return null;
	}

	public static HeroSkin GetHeroSkinByKey(List<HeroSkin> lstSkin, int skinKey, bool cloneCopy = false)
	{
		return null;
	}
}
