using System.Collections.Generic;

public class BattleEffectData
{
	public EffectPositionType m_EffectPosType;

	public long m_PointHpChanged;

	public long m_PointRageChanged;

	public HpLostType m_HpLostType;

	public long m_PointHpNew;

	public long m_PointRageNew;

	public List<EffectInfo> m_LstEffectInfo;

	public int m_EffectId;

	public bool m_IsBodyEffect;

	public override string ToString()
	{
		return null;
	}
}
