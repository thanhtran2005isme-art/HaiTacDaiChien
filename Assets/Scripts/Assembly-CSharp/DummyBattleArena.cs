using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyBattleArena : MonoBehaviour
{
	[SerializeField]
	private Text m_TextCoinCollected;

	[SerializeField]
	private DummyHeroBattle m_HeroPlayer;

	public JoystickController m_JoystickController;

	public DummyPlayerHeroControl m_PlayerHeroControl;

	public GameObject m_GoPointEdgeLeft;

	public GameObject m_GoPointEdgeRight;

	public GameObject m_GoPointEdgeBottom;

	public GameObject m_GoPointEdgeTop;

	public CameraFollower m_CameraFollower;

	public DummyBotFactory m_BotFactory;

	[SerializeField]
	private Transform m_TfBotContainer;

	public Transform m_TfHealthBarContainer;

	public GGPool m_GGPool;

	public GGPool m_GGPoolCanvas;

	private int m_CoinCollected;

	[SerializeField]
	private List<GameObject> m_ListGoSpawningSpot;

	public static DummyBattleArena Instance;

	private List<DummyBattleObject> m_ListBattleObject;

	private List<MonoRendererSortable> m_ListSortingRenderer;

	private Vector2 _posH1;

	private Vector2 _posH2;

	private bool _showPopupEnd;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void AddCoinCollected(int add)
	{
	}

	public Vector2 GetRandomSpawningSpot()
	{
		return default;
	}

	public void RegisterCoinForSorting(DummySpinCoin coin)
	{
	}

	public void RemoveCoinFromSorting(DummySpinCoin coin)
	{
	}

	public void RegisterHeroBotForSorting(DummyHeroBattle heroBot)
	{
	}

	public void RemoveHeroBotFromSortring(DummyHeroBattle heroBot)
	{
	}

	public List<DummyHeroBattle> GetListHeroBattle()
	{
		return null;
	}

	private void SortHeroLayerOrder()
	{
	}

	private void CheckEndBattle()
	{
	}

	public void AddBattleObject(DummyBattleObject battleObj)
	{
	}

	private void Update()
	{
	}
}
