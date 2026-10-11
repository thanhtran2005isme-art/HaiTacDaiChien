using UnityEngine;
using UnityEngine.EventSystems;

public class ParallaxLayerMng : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
	public GameObject m_GoPointHandSummonHouse;

	public GameObject m_GoPointHandCampaignHouse;

	public GameObject m_GoPointHandAltarHouse;

	public GameObject m_GoPointHandTower;

	public GameObject m_GoPointHandArena;

	public GameObject m_GoPointHandTarven;

	public GameObject m_GoPointHandDungeon;

	public GameObject m_GoPointHandIsland;

	public GameObject m_GoPointHandBrave;

	private Vector2 mSizeCanvas;

	private float m_LengthContent;

	private Vector2 mSizeScroll;

	public static ParallaxLayerMng Instance;

	[SerializeField]
	private RectTransform m_TransformLayerMain;

	private MovementHandler m_MovementHandler;

	private Vector2 mPointBeginDrag;

	private Vector2 mPosContentBeginDrag;

	private float mTimeStartDrag;

	private bool m_EnableScroll;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void InitData()
	{
	}

	public void ScrollToSummonHouse()
	{
	}

	public void ScrollToCampaignHouse()
	{
	}

	public void ScrollToAltarHouse()
	{
	}

	public void ScrollToTower()
	{
	}

	public void ScrollToArena()
	{
	}

	public void ScrollToDungeon()
	{
	}

	public void ScrollToIsland()
	{
	}

	public void ScrollToTarven()
	{
	}

	public void ScrollToBraveTrial()
	{
	}

	public void ClearHandOnArena()
	{
	}

	public void ClearHandOnAspendDungeon()
	{
	}

	public void ClearHandOnBraveTrial()
	{
	}

	private Vector2 ConvertMousePointerToCanvasPosition(Vector2 mousePointer)
	{
		return default;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnBeginDrag(PointerEventData pointerData)
	{
	}

	public void SetScrollEnable(bool enableScroll)
	{
	}

	public void OnDrag(PointerEventData pointerData)
	{
	}

	public void OnEndDrag(PointerEventData pointerData)
	{
	}

	public Vector2 GetLayerMainPosition()
	{
		return default;
	}

	public void SetLayerMainPosition(Vector2 pos)
	{
	}

	public void SetInitPosition()
	{
	}

	private void Update()
	{
	}
}
