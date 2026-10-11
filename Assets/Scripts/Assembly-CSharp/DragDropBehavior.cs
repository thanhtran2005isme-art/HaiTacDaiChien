using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropBehavior : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Vector2 mSizeCanvas;

	private RectTransform mRectTransform;

	[HideInInspector]
	public bool mEnableDragDrop;

	private Vector2 mPointDown;

	private Vector2 mPointBeginDrag;

	private Vector2 mPosContentBeginDrag;

	private int m_IndexCurrentMap;

	public virtual void Start()
	{
	}

	public void MoveToPosition(Vector2 pos)
	{
	}

	public void SetPosition(Vector2 pos)
	{
	}

	public Vector2 GetPosition()
	{
		return default;
	}

	public void OnPointerDown(PointerEventData pointerData)
	{
	}

	public void OnPointerUp(PointerEventData pointerData)
	{
	}

	private Vector2 ConvertMousePointerToCanvasPosition(Vector2 mousePointer)
	{
		return default;
	}

	public void OnBeginDrag(PointerEventData pointerData)
	{
	}

	public virtual void OnDrag(PointerEventData pointerData)
	{
	}

	public void OnEndDrag(PointerEventData pointerData)
	{
	}

	public virtual void OnItemClick()
	{
	}

	public virtual void OnItemDrop(Vector2 dropPosition)
	{
	}
}
