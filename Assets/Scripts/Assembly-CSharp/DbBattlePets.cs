using System.Collections.Generic;

public class DbBattlePets
{
	private static DbBattlePets m_Instance;

	private List<PetBattleInfo> m_LstPetInfo;

	public static DbBattlePets GetInstance()
	{
		return null;
	}

	private DbBattlePets()
	{
	}

	public int Count()
	{
		return 0;
	}

	public List<PetBattleInfo> GetAllPets()
	{
		return null;
	}

	public PetBattleInfo GetPetInfo(int petId, bool warningIfNoSpine = true)
	{
		return null;
	}

	public void SetListBattlePetInfo(List<PetBattleInfo> lst)
	{
	}

	private void InitDb()
	{
	}
}
