using System.Collections.Generic;
using UnityEngine;

public class SkillEditor : MonoBehaviour
{
	public enum SkillType
	{
		NO_IDENTIFY = 0,
		SKILL_MELEE = 1,
		SKILL_RANGE = 2,
		SKILL_AOE = 3,
		SKILL_LASER = 4
	}

	public BaseSkill mSkill;

	public SkillType mSkillType;

	public string m_AnimationName;

	public float m_DelayCastEffect;

	public float m_DelayBattleEffect;

	public bool needMoveUp;

	public SkillObjectInfo m_SkillObjInfo;

	public List<EffectInfo> m_LstImpactEffectInfo;

	public List<ShakeCameraInfo> m_LstShakeCamInfo;

	public List<DamageSplitInfo> m_lstDmgSplitInfo;

	public List<EffectInfo> m_LstCastEffectInfo;

	private void Start()
	{
	}

	public BaseSkill getSkill()
	{
		return null;
	}

	public void initData(BaseSkill mSkill)
	{
	}
}
