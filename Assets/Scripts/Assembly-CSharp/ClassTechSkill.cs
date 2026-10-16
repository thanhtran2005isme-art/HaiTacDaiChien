using System.Collections.Generic;

internal class ClassTechSkill
{
	public int m_ClassIndex;

	public List<TechSkillInfo> m_LstTechInfo;

	public ClassTechSkill(int classIdx, List<TechSkillInfo> lstTechInfo)
	{
	}

	public bool HasUpgrade()
	{
		return false;
	}
}
