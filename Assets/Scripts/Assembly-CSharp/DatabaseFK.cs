using System.Collections.Generic;

public class DatabaseFK
{
	private static DatabaseFK instance;

	public List<PlayerData> listPlayer;

	public List<LevelCampFK> listLevelCamp;

	public List<Weapon> listWeapon;

	public List<ItemUseFK> listItemUse;

	public List<AbilityFK> listAbility;

	public List<LevelHero> listLevelHero;

	public static DatabaseFK getInstance()
	{
		return null;
	}

	public AbilityFK GetAbility(int id)
	{
		return null;
	}

	public LevelHero GetLevelHero(int level)
	{
		return null;
	}

	public LevelCampFK GetLevelCampFK(int level)
	{
		return null;
	}

	public PlayerData getPlayer(int id)
	{
		return null;
	}

	public Weapon getWeapon(int id)
	{
		return null;
	}

	public ItemUseFK getItemUse(int id)
	{
		return null;
	}

	public PlayerData getRandom()
	{
		return null;
	}

	public void LoadAbility()
	{
	}

	public void LoadItemUse()
	{
	}

	public void LoadWeapon()
	{
	}

	public void LoadLevelHero()
	{
	}

	public void LoadDataLevelCamp()
	{
	}

	public void LoadDataHero()
	{
	}
}
