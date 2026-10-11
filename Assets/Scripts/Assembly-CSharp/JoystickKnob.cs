using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickKnob : DragDropBehavior
{
	private const float RADIUS_JOYSTICK = 100f;

	private RectTransform m_RectTf;

	private JoystickState m_State;

	public JoystickController m_Controller;

	public override void Start()
	{
	}

	public override void OnDrag(PointerEventData pointerData)
	{
	}

	public override void OnItemDrop(Vector2 dropPosition)
	{
	}

	private void Update()
	{
	}
}
