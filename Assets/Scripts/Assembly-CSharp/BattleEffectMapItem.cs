using System.Collections.Generic;
using SimpleJSON;

public class BattleEffectMapItem
{
	public int m_EffectId;

	public EffectPositionType m_PositionEffect;

	public bool m_IsBodyEffect;

	public List<EffectInfo> m_ListEffectInfo;

	public BattleEffectMapItem(int effectId)
	{
	}

	public BattleEffectMapItem(JSONClass jsonObject)
	{
	}

	public JSONClass ToJSONObject()
	{
		return null;
	}

	public void ImportFromJSONObject(JSONClass jsonObj)
	{
	}
}
