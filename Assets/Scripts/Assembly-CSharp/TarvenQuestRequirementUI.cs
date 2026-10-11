using UnityEngine;
using UnityEngine.UI;

public class TarvenQuestRequirementUI : MonoBehaviour
{
	public Image m_ImageItem;

	public Text m_TextItem;

	public GameObject m_GoCheck;

	private QuestRequirement m_QuestRequirement;

	private void Start()
	{
	}

	public void SetRequirement(QuestRequirement requirement)
	{
	}

	public void SetRequirementSatisfied(bool satisfied)
	{
	}

	public bool IsSatisfied()
	{
		return false;
	}

	public bool IsHeroModelSatisfied(HeroModel hm)
	{
		return false;
	}

	private void Update()
	{
	}
}
