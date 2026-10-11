using UnityEngine;

namespace StylizedWaterShader
{
	public class OrbitCamera : MonoBehaviour
	{
		[Header("Stylized Water Demo")]
		[Space]
		public Transform pivot;

		[Space]
		public bool enableMouse;

		public float idleRotationSpeed;

		private Transform cam;

		private float cameraRotSide;

		private float cameraRotUp;

		private float cameraRotSideCur;

		private float cameraRotUpCur;

		private float distance;

		private void Start()
		{
		}

		private void Update()
		{
		}
	}
}
