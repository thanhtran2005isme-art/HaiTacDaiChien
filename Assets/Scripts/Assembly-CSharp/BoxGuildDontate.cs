using UnityEngine;
using UnityEngine.UI;

public class BoxGuildDontate : MonoEventHandler
{
	public Text m_TextLevelMax;

	public Text m_TextMillDescMax;

	public Image m_ImageMillMax;

	public Text m_TextLevelFrom;

	public Text m_TextLevelTo;

	public Text m_TextGoldDonating;

	public Text m_TextDonate1;

	public Text m_TextDonate10;

	public Text m_TextMillDesc1;

	public Text m_TextMillDesc2;

	public Slider m_SliderDonateProgress;

	public Image m_ImageMill1;

	public Image m_ImageMill2;

	public GameObject m_GoContent;

	private int m_GoldDonate1;

	private const int MAX_GUILD_MILL_LEVEL = 10;

	public GameObject m_GoGroupDonate;

	public GameObject m_GoGroupDonateMax;

	public GameObject m_GoBoxMillStatus;

	public GameObject m_GoBoxMillStatusMax;

	protected override void Start()
	{
	}

	public void ButtonDonate1Click()
	{
	}

	public void ButtonDonate10Click()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
