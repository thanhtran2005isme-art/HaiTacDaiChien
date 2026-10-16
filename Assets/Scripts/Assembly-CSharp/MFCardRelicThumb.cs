using UnityEngine;
using UnityEngine.UI;

public class MFCardRelicThumb : MonoBehaviour
{
	[HideInInspector]
	public int m_CardId;

	public Image m_ImageQuality;

	public Text m_TextName;

	public Text m_TextDesc;

	public Image m_ImageRelic;

	public GameObject m_GroupContents;

	public GameObject m_GroupAffect;

	public GameObject m_GoAffectHeroHead;

	public GameObject m_GoAffectText;

	public GameObject m_GoAffectFaction;

	public GameObject m_GoAffectClass;

	public Text m_TextAffect;

	public Image m_ImageHeroFaction;

	public Image m_ImageHeroClass;

	public Image m_ImageHeroHead;

	private Color COLOR_RARE;

	private Color COLOR_ELITE;

	private Color COLOR_LEGENDARY;

	private MFRelicInfo _relicInfo;

	private void Start()
	{
	}

	public int GetRelicId()
	{
		return 0;
	}

	public void SetRelicInfo(MFRelicInfo relicInfo)
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
