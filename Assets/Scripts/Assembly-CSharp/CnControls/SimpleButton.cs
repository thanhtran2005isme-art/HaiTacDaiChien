using UnityEngine;
using UnityEngine.EventSystems;

namespace CnControls
{
	public class SimpleButton : MonoBehaviour, IPointerUpHandler, IEventSystemHandler, IPointerDownHandler
	{
		public string ButtonName;

		private VirtualButton _virtualButton;

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public void OnPointerUp(PointerEventData eventData)
		{
		}

		public void OnPointerDown(PointerEventData eventData)
		{
		}
	}
}
