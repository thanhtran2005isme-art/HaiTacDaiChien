using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollCampaignMap : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
	private class MapPartInfo
	{
		public GameObject m_GoMapPart;

		public Vector2 m_Position;

		public int m_PartIndex;

		public bool m_HasMapObject;

		public MapPartInfo(int partIndex, Vector2 position)
		{
		}
	}

	private Vector2 mSizeCanvas;

	private Vector2 mSizeScroll;

	private MovementHandler m_MovementHandler;

	[SerializeField]
	private RectTransform m_TransformContent;

	private float m_LengthContent;

	private const float LENGTH_MAP_PART = 3280f;

	private const int NUM_GATE_PER_PART = 5;

	private List<MapPartInfo> m_LstMapPartInfo;

	private List<GameObject> m_LstPoolMapPart;

	private List<CampaignGateInfo> m_LstGateMap;

	private Vector2 mPointBeginDrag;

	private Vector2 mPosContentBeginDrag;

	private float mTimeStartDrag;

	private void Start()
	{
	}

	private void InitData()
	{
	}

	private void CheckMapScroll()
	{
	}

	private GameObject GetPoolMapPart()
	{
		return null;
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

	private void Update()
	{
	}
}
