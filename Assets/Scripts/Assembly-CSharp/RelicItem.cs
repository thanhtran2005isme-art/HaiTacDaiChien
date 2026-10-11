using System;
using UnityEngine;
using UnityEngine.UI;

public class RelicItem : MonoBehaviour
{
	public Image m_ImageRelic;

	public Image m_ImageQuality;

	public Text m_TextName;

	public Text m_TextDesc;

	public GameObject m_GroupAffect;

	public GameObject m_GoAffectHeroHead;

	public GameObject m_GoAffectText;

	public GameObject m_GoAffectFaction;

	public GameObject m_GoAffectClass;

	public Text m_TextAffect;

	public Image m_ImageHeroFaction;

	public Image m_ImageHeroClass;

	public Image m_ImageHeroHead;

	public InputField mInput;

	private Color COLOR_RARE;

	private Color COLOR_ELITE;

	private Color COLOR_LEGENDARY;

	public Action<int, int> m_DataChange;

	private MFRelicInfo m_RelicInfo;

	private void Start()
	{
	}

	public void NumberChange()
	{
	}

	public void SetRelicInfo(MFRelicInfo relicInfo, int number)
	{
	}

	private void Update()
	{
	}
}
