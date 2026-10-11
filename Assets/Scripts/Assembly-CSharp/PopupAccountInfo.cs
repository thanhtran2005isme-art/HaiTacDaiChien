using UnityEngine;
using UnityEngine.UI;

public class PopupAccountInfo : MonoEventHandler
{
	public Text m_TextPlayername;

	public Text m_TextGuildName;

	public Text m_TextId;

	public Text m_TextExp;

	public Slider m_SliderExp;

	public PlayerAvatar m_PlayerAvatar;

	public Text m_TextUsername;

	public ToggleScript m_ToggleSound;

	public ToggleScript m_ToggleMusic;

	public Text m_TextServer;

	public static bool NeedProcessChangeServer;

	public GameObject m_GoButtonLanguage;

	protected override void Start()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonEditNameClick()
	{
	}

	public void ButtonSettingClick()
	{
	}

	public void ButtonLogOutClick()
	{
	}

	public void ButtonMusicClick()
	{
	}

	public void ButtonSoundClick()
	{
	}

	public void ButtonLanguagesClick()
	{
	}

	public static PopupAccountInfo ShowUp()
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void ButtonChangeServerClick()
	{
	}

	private void Update()
	{
	}
}
