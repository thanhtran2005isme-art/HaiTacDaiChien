using UnityEngine;
using UnityEngine.EventSystems;

namespace CnControls
{
	public class Touchpad : MonoBehaviour, IDragHandler, IEventSystemHandler, IPointerUpHandler, IPointerDownHandler
	{
		public string HorizontalAxisName;

		public string VerticalAxisName;

		public bool PreserveInertia;

		public float Friction;

		private VirtualAxis _horizintalAxis;

		private VirtualAxis _verticalAxis;

		private int _lastDragFrameNumber;

		private bool _isCurrentlyTweaking;

		[Tooltip("Constraints on the joystick movement axis")]
		public ControlMovementDirection ControlMoveAxis;

		public Camera CurrentEventCamera { get; set; }

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public virtual void OnDrag(PointerEventData eventData)
		{
		}

		public void OnPointerUp(PointerEventData eventData)
		{
		}

		public void OnPointerDown(PointerEventData eventData)
		{
		}

		private void Update()
		{
		}
	}
}
