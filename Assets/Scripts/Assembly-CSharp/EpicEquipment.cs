using System.Collections.Generic;

public class EpicEquipment
{
	public bool isLock;

	public List<long> listFeeUpgradeAssetAttack;

	public List<long> listFeeUpgradeItemAttack;

	public List<long> listFeeEnhanceAttack;

	public List<long> listFeeEnhanceHp;

	public List<long> listFeeUpgradeItemHp;

	public List<long> listFeeUpGradeAssetHp;

	public long attackLevel;

	public long attackNormal;

	public long attackTier;

	public long attackPercent;

	public long hpLevel;

	public long hpNormal;

	public long hpTier;

	public long hpPercent;

	public long nextAttackNormal;

	public long nextAttackPercent;

	public long nextHpNormal;

	public long nextHpPercent;

	public bool haveNotifyUpgradeHp;

	public bool haveNotifyEnhanceHp;

	public bool haveNotifyUpgradeATK;

	public bool haveNotifyEnhanceATK;

	public int maxLevelEnhance;

	public int maxLeveUpgrade;

	public void SetMaxLevel(int maxLevelEnhance, int maxLeveUpgrade)
	{
	}

	public void SetLock(bool isLock)
	{
	}

	public void SetNoti(bool haveNotifyUPgradeHp, bool haveNotifyEnhanceHp, bool haveNotifyEnhanceATK, bool haveNotifyUpgradeATK)
	{
	}

	public void SetCurrentData(long attackLevel, long attackNormal, long attackTier, long attackPercent, long hpLevel, long hpNormal, long hpTier, long hpPercent)
	{
	}

	public void SetNextData(long nextAttackNormal, long nextAttackPercent, long nextHpNormal, long nextHpPercent)
	{
	}

	public void SetFee(List<long> listFeeUpgradeAssetAttack, List<long> listFeeUpgradeItemAttack, List<long> listFeeUpGradeAssetHp, List<long> listFeeEnhanceHp, List<long> listFeeEnhanceAttack, List<long> listFeeUpgradeItemHp)
	{
	}
}
