using UnityEngine;
using UnityEngine.EventSystems;

public class ITouchProcessor : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
	public Camera m_Camera;

	private ITouchMode m_TouchMode;

	private Canvas m_Canvas;

	private bool m_InitProcessor;

	private Vector2 _sizeCanvas;

	private ITouchHandler m_TouchHandler;

	private float _timeStartDrag;

	private void Start()
	{
	}

	public void SetTouchInCanvasMode(Canvas canvas, ITouchHandler touchHandler)
	{
	}

	public void SetTouchInWorldMode(ITouchHandler touchHandler)
	{
	}

	private Vector2 ConvertMousePointerToWorldPosition(Vector2 mousePointer)
	{
		return default;
	}

	private Vector2 ConvertMousePointerToCanvasPosition(Vector2 mousePointer)
	{
		return default;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
	}

	public void OnDrag(PointerEventData eventData)
	{
	}

	public void OnEndDrag(PointerEventData eventData)
	{
	}

	private void Update()
	{
	}
}
