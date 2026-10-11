using System.Collections.Generic;

public class BattleEffectMapper
{
	private static BattleEffectMapper m_Instance;

	public List<BattleEffectMapItem> m_LstBodyEffect;

	public List<int> m_LstBattleBuffId;

	public static BattleEffectMapper GetInstance()
	{
		return null;
	}

	private BattleEffectMapper()
	{
	}

	public void SetListEffectMapper(List<BattleEffectMapItem> lstEffectMap)
	{
	}

	public void SetListBattleBuff(List<int> lstBuffId)
	{
	}

	public int CountBattleEffect()
	{
		return 0;
	}

	public int CountBattleBuff()
	{
		return 0;
	}

	public bool IsBuffEffect(int effectId)
	{
		return false;
	}

	public BattleEffectMapItem GetBattleEffectMapItem(int effectId)
	{
		return null;
	}

	public void GenListBattleBodyEffect()
	{
	}

	public void GenListBattleBuff()
	{
	}
}
