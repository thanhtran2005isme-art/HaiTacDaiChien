using UnityEngine;

namespace CnControls
{
	public class DpadAxis : MonoBehaviour
	{
		public string AxisName;

		public float AxisMultiplier;

		private VirtualAxis _virtualAxis;

		public RectTransform RectTransform { get; private set; }

		public int LastFingerId { get; set; }

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public void Press(Vector2 screenPoint, Camera eventCamera, int pointerId)
		{
		}

		public void TryRelease(int pointerId)
		{
		}
	}
}
