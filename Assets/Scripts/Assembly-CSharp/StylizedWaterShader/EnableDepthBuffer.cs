using UnityEngine;

namespace StylizedWaterShader
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class EnableDepthBuffer : MonoBehaviour
	{
		private Camera cam;

		private void OnValidate()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDestroy()
		{
		}
	}
}
