using UnityEngine;
using UnityEngine.UI;

public class PopupMakeBuilding : MonoEventHandler
{
	public const int MINE_GOLD = 0;

	public const int MINE_GEM = 1;

	public const int MINE_MAGIC_DUST = 2;

	public Text m_ReqGold;

	public Text m_ReqStone;

	public Transform m_LayoutMine;

	public GameObject m_ButtonGray;

	public GameObject m_GroupMine;

	public GameObject m_PanelAGI;

	public GameObject m_PanelMili;

	public GameObject m_PanelDaffodil;

	private MineModel mineModel;

	private BuildingModel buildingModel;

	private int typeMine;

	private int currTab;

	private bool isOkToBuild;

	public void InitPopup(MineModel model)
	{
	}

	public void InitPopup(BuildingModel model)
	{
	}

	private void MineSelectClick(int tabId)
	{
	}

	public void BuildClick()
	{
	}

	public void CloseClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private new void Start()
	{
	}

	private void Update()
	{
	}

	public static void ShowUp(MineModel model)
	{
	}

	public static void ShowUp(BuildingModel model)
	{
	}
}
