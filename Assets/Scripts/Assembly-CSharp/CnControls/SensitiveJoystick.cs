using UnityEngine;
using UnityEngine.EventSystems;

namespace CnControls
{
	public class SensitiveJoystick : SimpleJoystick
	{
		public AnimationCurve SensitivityCurve;

		public override void OnDrag(PointerEventData eventData)
		{
		}
	}
}
