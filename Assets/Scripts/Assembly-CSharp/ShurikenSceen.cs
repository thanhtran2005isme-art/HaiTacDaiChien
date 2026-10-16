using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShurikenSceen : MonoBehaviour
{
	[HideInInspector]
	private Zone mZone;

	[HideInInspector]
	private GameObject mSkill;

	[HideInInspector]
	private MapController mMap;

	[HideInInspector]
	public NPC currentNPC;

	[HideInInspector]
	public const int MAX_LEVEL_HERO = 15;

	public int levelCamp;

	public int playerId;

	public PlayerController mPlayer;

	public float timeGenEnemy;

	public int numEnemy;

	public int skill_Index;

	public static ShurikenSceen instanse;

	public GameObject focus;

	public bool lockMove;

	public bool lockAttack;

	public bool pauseGame;

	public GameObject joyStick;

	public int mGold;

	public Text txtGold;

	public Text txtLevel;

	public Image m_BarExp;

	public Image ico_weapon;

	public Image ico_itemUse1;

	public Image ico_itemUse2;

	public Text txtItemUse1;

	public Text txtItemUse2;

	public GameObject btnAction;

	public PopupMarket popupMarket;

	public Text txtTimer;

	private int levelHero;

	private int currentExp;

	private int nextExpRequire;

	public GameObject help;

	public CameraFollower m_CameraFollower;

	public GameObject m_GoPointEdgeLeft;

	public GameObject m_GoPointEdgeRight;

	public GameObject m_GoPointEdgeBottom;

	public GameObject m_GoPointEdgeTop;

	private float tmpRegenMana;

	private bool endGame;

	public Zone MZone
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public GameObject MSkill
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public MapController MMap
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private void Start()
	{
	}

	public void PoolMap()
	{
	}

	public void AddExpHero(int expAdd)
	{
	}

	public void ShowAction(NPC npc)
	{
	}

	public void HideAction()
	{
	}

	public void ActionClick()
	{
	}

	public void ChangeWeapon()
	{
	}

	public void addGold(int gold)
	{
	}

	public void finalLevel()
	{
	}

	public void initLevel()
	{
	}

	public void UseItem(int index)
	{
	}

	public void ProcessEffect(PlayerController mPlayer, EffectFK effect)
	{
	}

	public void ProcessEffect(PlayerController mPlayer, List<EffectFK> lstEffect)
	{
	}

	public void UpdateUI()
	{
	}

	public void BackScene()
	{
	}

	public void CloseHelp()
	{
	}

	public void showControl(bool isActive)
	{
	}

	public bool isPauseGame()
	{
		return false;
	}

	public void useSkill(int index)
	{
	}

	public void endSkill()
	{
	}

	public List<PlayerController> GetListEnemyInRadius(PlayerController other, float radius)
	{
		return null;
	}

	public PlayerController GetNearPlayer(PlayerController other)
	{
		return null;
	}

	public void initTarget()
	{
	}

	public Vector3 ClampMap(Vector3 pos)
	{
		return default;
	}

	public float getAngle(Vector3 pointFrom, Vector3 pointTo)
	{
		return 0f;
	}

	public void processMove(int state)
	{
	}

	private void Update()
	{
	}
}
