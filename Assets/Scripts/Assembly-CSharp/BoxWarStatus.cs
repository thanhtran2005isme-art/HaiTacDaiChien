using UnityEngine;
using UnityEngine.UI;

public class BoxWarStatus : MonoBehaviour
{
	public Text m_TextClan1;

	public Text m_TextClan2;

	public Image m_FlagClan1;

	public Image m_FlagClan2;

	public Text m_TextServer1;

	public Text m_TextServer2;

	public Text m_TextNumAtk1;

	public Text m_TextNumAtk2;

	public TextCountdown m_TextCountdown;

	public Text m_TextProgressStar1;

	public Text m_TextProgressStar2;

	public Slider m_SliderStar1;

	public Slider m_SliderStar2;

	public GameObject m_GoStar1;

	public GameObject m_GoStar2;

	public GameObject m_GoAttack1;

	public GameObject m_GoAttack2;

	private void Start()
	{
	}

	public void SetStatus(GuildWarData warData, int warStatus)
	{
	}

	private void Update()
	{
	}
}
