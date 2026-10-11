using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollWarMap : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
	public static float LastYScroll;

	public static bool LastNavigateWardSideAlly;

	public RectTransform m_TransformContent;

	public MovementHandler m_MovementHandler;

	public Transform m_TransformGroupMap;

	public Transform m_TransformGroupBase;

	private Vector2 mSizeCanvas;

	private Vector2 mSizeScroll;

	private GuildWarData m_GuildWarData;

	private const int WIDTH_MAP_PART = 2272;

	private const int HEIGHT_MAP_PART = 650;

	private const int NUM_BASE_PER_PART = 4;

	private List<WarMapPartInfo> m_LstMapPartInfo;

	private List<GameObject> m_LstPoolWarBase;

	private List<GameObject> m_LstPoolMapPart;

	private ActionRepeatTimer m_TimerCheckScrollPos;

	public static ScrollWarMap Instance;

	private Vector2 mPointBeginDrag;

	private Vector2 mPosContentBeginDrag;

	private float mTimeStartDrag;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void RefreshWarData(GuildWarData warData)
	{
	}

	private void RefreshBase(WarBase warBase, List<GuildWarPlayer> lstAllPlayer)
	{
	}

	public void SetWarData(GuildWarData warData)
	{
	}

	public void ScrollMapToPosition(bool onAllySide, float yScroll)
	{
	}

	public void MoveToAllyMap()
	{
	}

	public void MoveToEnemyMap()
	{
	}

	private void CheckMapScroll()
	{
	}

	public GameObject GetPoolWarBase(WarBaseType baseType)
	{
		return null;
	}

	private GameObject GetPoolMapPart(int partType)
	{
		return null;
	}

	private void Update()
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

	public void OnDrag(PointerEventData pointerData)
	{
	}

	public void OnEndDrag(PointerEventData pointerData)
	{
	}
}
