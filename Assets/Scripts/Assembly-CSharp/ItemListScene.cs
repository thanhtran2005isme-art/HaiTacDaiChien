using UnityEngine;
using UnityEngine.UI;

public class ItemListScene : BaseScene, ITabListener
{
	public TabManager m_TabItems;

	public GameObject m_GroupEquipment;

	public GameObject m_GroupConsumable;

	public GameObject m_GroupShard;

	public GameObject m_GroupArtifact;

	public GridPoolGroup m_PoolEquipment;

	public GridPoolGroup m_PoolConsumable;

	public GridPoolGroup m_PoolShard;

	public GridPoolGroup m_PoolArtifact;

	public GameObject m_GoButtonBack;

	public GameObject m_GoButtonHeroHead;

	public Transform m_TransformContentShard;

	public AudioSource m_AudioShowHero;

	private bool FILTER_ZERO_ITEM;

	private const int TAB_ITEM = 1;

	private int currentCountItemConsumable;

	private const int TAB_BUTTON_EQUIPMENT = 1;

	private const int TAB_BUTTON_CONSUMABLE = 2;

	private const int TAB_BUTTON_SHARD = 3;

	private const int TAB_BUTTON_ARTIFACT = 4;

	private bool m_OnTutorial;

	public GameObject m_GoPointHandBack;

	public GameObject m_GoPointHandSelectShard;

	private GameObject m_GoHandSelectShard;

	private ActionRepeatTimer m_TimerCheckShard;

	public GameObject m_GoNotifyShard;

	public GameObject m_GoButtonDictArtifact;

	public Button m_BtnTabShard;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	protected override void ReciveTutorial(TutorialAction tutAct)
	{
	}

	private void LoadData()
	{
	}

	private void LoadCurrentTutorial11()
	{
	}

	private void TurnOffAllUI()
	{
	}

	private void SetUpUIForTutorial(int tutId)
	{
	}

	private void DisableAllShardItem()
	{
	}

	private int DisableAllShardItemExceptFirstHeroShard()
	{
		return 0;
	}

	public void ButtonHeroesClick()
	{
	}

	public void ButtonBackClick()
	{
	}

	private void OnItemEquipmentClick(ItemEquipment ie)
	{
	}

	private void OnItemConsumableClick(ItemConsumable ic)
	{
	}

	private void OnItemShardClick(ItemShard itmShard)
	{
	}

	private void OnItemArtifactClick(ItemArtifact itemArt)
	{
	}

	private void ReloadScrollShard()
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public void ButtonDictArtifactClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
