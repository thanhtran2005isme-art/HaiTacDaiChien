using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupHeroESkill : BasePopup
{
	public Action<bool> m_CloseClickCb;

	private bool m_HasSwitchSkill;

	private HeroModel m_HeroModel;

	public GameObject m_PrefabCellGroupSkills;

	public Transform m_TfScrollContent;

	public GameObject m_GoPanelSkillDetail;

	private List<CellGroupESkills> m_ListCellSkillGroup;

	public static PopupHeroESkill Instance;

	private int m_SwitchSkillGroupIndex;

	private int m_SwitchSkillIndex;

	public GameObject m_GoGroupChiPoint;

	public List<GameObject> m_ListGoChiPoint;

	public SpineObject m_SpineStarFlash;

	public GameObject m_GoPrefabChiStar;

	public Transform m_TfGroupStar;

	public PanelHeroESkill m_PanelHeroESkill;

	private List<GameObject> m_ListGoChiStar;

	protected override void Start()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ShowSkillDetail(ESkillInfo eSkillInfo)
	{
	}

	public void SetSkillActive(int groupSkillIdx, int skillIdx)
	{
	}

	public void SetChiPointActive(int groupSkillIdx, int skillIdx, bool playAnimation = false)
	{
	}

	public void LoadSkillData(HeroModel hm)
	{
	}

	public List<ESkillModel> GetHeroESkillModel(int heroStarOfSkill)
	{
		return null;
	}

	public static PopupHeroESkill ShowUp(HeroModel hm)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
