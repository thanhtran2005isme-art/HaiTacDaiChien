using UnityEngine;
using UnityEngine.UI;

public class PopupHeroAwakenResult : BasePopup
{
	public Text m_TextLevelFrom;

	public Text m_TextLevelTo;

	public Text m_TextAtkFrom;

	public Text m_TextAtkTo;

	public Text m_TextHpFrom;

	public Text m_TextHpTo;

	public Text m_TextArmorFrom;

	public Text m_TextArmorTo;

	public HeroHeadUI m_HeroHeadFrom;

	public HeroHeadUI m_HeroHeadTo;

	public ButtonSkillInfo m_ButtonSkillFrom;

	public ButtonSkillInfo m_ButtonSkillTo;

	public GameObject m_GoBoxSkillFrom;

	public GameObject m_GoBoxSkillTo;

	protected override void Start()
	{
	}

	public void ShowAwakenInfo(HeroModel hmFrom, HeroModel hmTo)
	{
	}

	public static PopupHeroAwakenResult ShowUp(HeroModel hmFrom, HeroModel hmTo)
	{
		return null;
	}

	private void Update()
	{
	}
}
