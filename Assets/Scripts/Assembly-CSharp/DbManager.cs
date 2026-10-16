using System.Collections.Generic;

public class DbManager
{
	private static DbManager m_Instance;

	private List<UpgradeHeroData> m_ListUpgradeHeroData;

	private List<UpgradePetLevelData> m_ListUpgradePetLevelData;

	private List<UpgradePetPassiveData> m_ListUpgradePetPassiveData;

	private List<PetRuinScale> m_ListPetRuinScale;

	private List<UpgradePetRuinData> m_ListUpgradePetRuinData;

	public List<HeroModel> m_ListHeroModel;

	private List<HeroSkin> m_ListHeroSkin;

	private List<SkillModel> m_ListSkillModel;

	public List<ESkillModel> m_ListESkillModel;

	public List<ItemEquipment> m_ListItemEquipment;

	public List<ItemConsumable> m_ListItemConsumable;

	public List<ItemShard> m_ListItemShard;

	public List<ItemArtifact> m_ListItemArtifact;

	public List<ItemAvatar> m_ListItemSpecialAvatar;

	private List<CampaignLevelInfo> m_ListCampaignLevelInfo;

	private List<HeroStarLink> m_ListHeroStarLink;

	private List<TowerLevelInfo> m_ListTowerLevelInfo;

	private List<TowerLevelInfo> m_ListTower2LevelInfo;

	private List<MonsterModel> m_ListMonsterModel;

	private List<HeroStoneModel> m_ListHeroStoneModel;

	private List<ArtifactUpgradeInfo> m_ListArtifactUpgradeInfo;

	private List<PlayerExp> m_ListPlayerExp;

	private List<EquipmentSetData> m_ListEquipmentSet;

	private List<string> m_ListCharacterName;

	private List<GuildBossStage> m_ListGuildBossStage;

	public List<PetModel> m_ListPetModel;

	public BenefitObject[] m_benefit;

	private List<ItemIAPModel> m_ListItemIAP;

	public List<string> m_ListActiveIapProducts;

	private string m_PrefixPackProductId;

	private string m_PrefixMonthProductId;

	public List<RewardArenaModel> listDailyReward;

	public List<RewardArenaModel> listSeasonReward;

	public List<RewardArenaModel> listTrailOfChampionReward;

	public List<AllStarsPointPerHour> m_ListAllStarsPointPerHour;

	public List<AllStarsSeasonRewards> m_ListAllStarsSeasonRewards;

	public List<AllStarsKnockOutRewards> m_ListAllStarKnockOutRewards;

	public List<TowerUpgradeModel> listConfigTowerUpgrade;

	public List<AgiAndMiliUpgradeModel> listConfigTechBuildUpgrade;

	public List<TechCelestialUpgradeModel> listConfigTechUpgrade;

	public List<DaffodilUpgradeModel> listConfigDaffodilUpgrade;

	public List<MineUpgradeModel> listConfigMineUpgrade;

	public List<HelpModel> listHelpUser;

	public List<FAQModel> listFAQ;

	public List<SuggestModel> listSuggest;

	public CfgSpecialEvent cfgSpecialEvent;

	public List<DbBuffAndDeBuff> lstBuffAndDebuff;

	public List<PrayForFireFactionReward> m_ListPpfReward;

	public Dictionary<int, List<Message>> dictionaryMessage;

	public List<DbNotification> lstDbNotification;

	public List<HomeQuest> m_ListHomeQuest;

	public List<MFRelicInfo> m_ListMfRelicInfo;

	public List<LuaPlaceholderInfo> m_ListLuaPlaceHolderInfo;

	public bool HasInitDb { get; set; }

	public static DbManager GetInstance()
	{
		return null;
	}

	public static void ClearInstance()
	{
	}

	private DbManager()
	{
	}

	public void LoadSplashDb()
	{
	}

	public void LoadAllDb()
	{
	}

	private void LoadDbMfRelic()
	{
	}

	private void LoadDbLuaPlaceholder()
	{
	}

	private void LoadDbCharacterName()
	{
	}

	private void LoadDbBuffAndDebuff()
	{
	}

	private void LoadDbHomeQuest()
	{
	}

	private void LoadDbSkillMessage()
	{
	}

	private void LoadDbBattleHeroesSkin()
	{
	}

	private void LoadDbBattleHeroes()
	{
	}

	private void LoadDbBattlePets()
	{
	}

	private void LoadDbHeroBattleEffects()
	{
	}

	private void LoadDbAura()
	{
	}

	private void LoadDbHeroStarLink()
	{
	}

	private void LoadDbPet()
	{
	}

	private void LoadGuildBossStage()
	{
	}

	private void LoadGuildBossStage2()
	{
	}

	private void LoadDbEquipmentSet()
	{
	}

	private void LoadDbTower2Level()
	{
	}

	private void LoadDbTowerLevel()
	{
	}

	private void LoadDbArtifactUpdateData()
	{
	}

	private void LoadDbMonster()
	{
	}

	private void LoadDbPlayerExp()
	{
	}

	private void LoadDbHeroStone()
	{
	}

	private void LoadDbCampaignLevelInfo()
	{
	}

	public int CountTotalHeroModel()
	{
		return 0;
	}

	public void LoadDbHeroSkins()
	{
	}

	public void LoadDbHeroes()
	{
	}

	private void LoadDbHeroESkill()
	{
	}

	private void LoadDbHeroSkill()
	{
	}

	private void LoadDbItemEquipment()
	{
	}

	private void LoadDbItemConsumable()
	{
	}

	private void LoadDbItemShard()
	{
	}

	private void LoadDbItemArtifact()
	{
	}

	private void LoadDbItemAvatar()
	{
	}

	public void addEffectToArtifact(int star, string jsonPoint, ItemArtifact artifact)
	{
	}

	private void LoadDbUpgradeHeroData()
	{
	}

	private void LoadDbUpgradePetLevelData()
	{
	}

	private void LoadDbUpgradePetRuinData()
	{
	}

	private void LoadDbPetRuinScale()
	{
	}

	private void LoadDbUpgradePetPassiveData()
	{
	}

	private void LoadDbFusingHeroData()
	{
	}

	private void LoadDbVipBenefit()
	{
	}

	private void LoadActiveProducts()
	{
	}

	private void LoadConfigIAPItem()
	{
	}

	private void LoadCelestialUpgradeConfig()
	{
	}

	private void LoadHelpUser()
	{
	}

	private void LoadFAQ()
	{
	}

	private void LoadAllStarsSeasonRewards()
	{
	}

	private void LoadAllStarsKnockOutRewards()
	{
	}

	private void LoadAllStarsPointPerHour()
	{
	}

	private void LoadArenaRewardConfig()
	{
	}

	private void LoadSuggest()
	{
	}

	private void LoadCfgSpecialEvent()
	{
	}

	private List<long> ConvertJsonArrToListLong(string strArrJson)
	{
		return null;
	}

	public string GetRandomCharacterName()
	{
		return null;
	}

	private void LoadDbNotification()
	{
	}

	public HomeQuest GetHomeQuest(int questId)
	{
		return null;
	}

	public UpgradeHeroData GetLevelUpgradeRequirement(int level)
	{
		return null;
	}

	public HeroModel GetHeroModel(int heroKey)
	{
		return null;
	}

	public HeroModel GetHeroModelCopy(int heroKey)
	{
		return null;
	}

	public HeroModel GetHeroMaxModelCopy(int heroKey)
	{
		return null;
	}

	public List<PetModel> GetListAllPetModel()
	{
		return null;
	}

	public PetModel GetPetModel(int petId)
	{
		return null;
	}

	public UpgradePetLevelData GetPetLevelUpgradeRequirement(int level)
	{
		return null;
	}

	public UpgradePetPassiveData GetPetPassiveUpgradeRequirement(int levelId)
	{
		return null;
	}

	public UpgradePetRuinData GetPetRuinUpgradeRequirement(int ruinLevel)
	{
		return null;
	}

	public PetRuinScale GetRuinScaleForPet(int petId)
	{
		return null;
	}

	public PlayerExp GetPlayerExp(int level)
	{
		return null;
	}

	public List<HeroSkin> GetAllHeroSkins(bool getUnReleased = false)
	{
		return null;
	}

	public HeroSkin GetHeroSkin(int skinKey)
	{
		return null;
	}

	public ItemEquipment GetItemEquipment(int itemKey)
	{
		return null;
	}

	public ItemEquipment GetItemEquipmentCopy(int itemKey)
	{
		return null;
	}

	public ItemArtifact GetItemArtifact(int itemKey)
	{
		return null;
	}

	public ItemArtifact GetItemArtifactCopy(int itemKey)
	{
		return null;
	}

	public List<ItemArtifact> GetAllArtifactMaxStar()
	{
		return null;
	}

	public ItemConsumable GetItemConsumable(int itemKey)
	{
		return null;
	}

	public ItemConsumable GetItemConsumableCopy(int itemKey)
	{
		return null;
	}

	public ItemShard GetItemShard(int itemKey, int shardType)
	{
		return null;
	}

	public ItemShard GetItemShardCopy(int itemKey, int shardType)
	{
		return null;
	}

	private int GetNumShardToSummonHero(int heroStar)
	{
		return 0;
	}

	public int GetTotalCampaignLevel()
	{
		return 0;
	}

	public CampaignLevelInfo GetCampaignLevelInfo(int levelId)
	{
		return null;
	}

	public SkillModel GetSkillModel(int skillId)
	{
		return null;
	}

	public int GetKeyHero10StarFromHero5Star(int keyHero5Star)
	{
		return 0;
	}

	public int GetKeyHero10StarFromHero6Star(int keyHero6Star)
	{
		return 0;
	}

	public bool CanAwakenTo10Star(int keyHero6Star)
	{
		return false;
	}

	public int GetKeyHero5StarFromHero4Star(int keyHero4Star)
	{
		return 0;
	}

	public int GetKeyHero5StarFromHero10Star(int keyHero10Star)
	{
		return 0;
	}

	public int GetKeyHero5StarFromHero6789Star(int keyHero6Star)
	{
		return 0;
	}

	public int GetKeyHero6StarFromHero5Star(int keyHero5Star)
	{
		return 0;
	}

	public int GetKeyHero4StarFromHero5Star(int keyHero5Star)
	{
		return 0;
	}

	public TowerLevelInfo GetTowerLevel(int levelId)
	{
		return null;
	}

	public int CountTower1Level()
	{
		return 0;
	}

	public TowerLevelInfo GetTower2Level(int levelId)
	{
		return null;
	}

	public int CountTower2Level()
	{
		return 0;
	}

	public MonsterModel GetMonsterModelCopy(int monsterId)
	{
		return null;
	}

	public EquipmentSetData GetEquipmentSet(int equipmentKey)
	{
		return null;
	}

	public HeroStoneModel GetHeroStoneModelCopy(int stoneId)
	{
		return null;
	}

	public FusingHeroSet GetFusingHeroSet(int heroKeyFusing)
	{
		return null;
	}

	public int GetHeroKeyFusingUsingMatterialHero(int heroKeyMatterial)
	{
		return 0;
	}

	public GuildBossStage GetGuildBossStage(int stageId)
	{
		return null;
	}

	public int GetTotalGuildBoss()
	{
		return 0;
	}

	public BenefitObject GetBenefit(int levelVIP)
	{
		return null;
	}

	public int CountLevelVIP()
	{
		return 0;
	}

	public List<ItemIAPModel> GetListIAPItem()
	{
		return null;
	}

	public ItemIAPModel GetIAPMonthItem(string keyFind)
	{
		return null;
	}

	public ItemIAPModel GetIAPItem(string keyFind)
	{
		return null;
	}

	public ItemIAPModel GetInfoIAPByProductId(string productId)
	{
		return null;
	}

	public ItemIAPModel GetInfoIAPBySuffix(string iapSuffix)
	{
		return null;
	}

	public bool IsProductIapValid(string productId)
	{
		return false;
	}

	public string MapProductIdToPurchase(string inputProductId)
	{
		return null;
	}

	public ArtifactUpgradeInfo GetArtifactUpgradeInfo(ArtifactType artifactType)
	{
		return null;
	}

	public RewardArenaModel GetRewardDailyArenaByRank(int rank)
	{
		return null;
	}

	public MFRelicInfo GetMfRelicInfo(int relicId)
	{
		return null;
	}

	public LuaPlaceholderInfo GetLuaPlaceholder(int placeId)
	{
		return null;
	}

	public int GetCapacityMineByLevelAndType(int level, int typeMine)
	{
		return 0;
	}
}
