using UnityEngine;

public class PopupMasteringHero : BasePopup
{
	private const int TAB_MASTERING = 1;

	private const int TAB_CHALLENGE = 2;

	public GameObject m_GoBoxMastering;

	public GameObject m_GoBoxChallenges;

	public TabManager m_TabMng;

	public static PopupMasteringHero Instance;

	public GameObject m_TabExpert;

	private int _currentTabOpen;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private void OnTabActive(int tabId)
	{
	}

	public static PopupMasteringHero ShowUp()
	{
		return null;
	}

	private void Update()
	{
	}
}
