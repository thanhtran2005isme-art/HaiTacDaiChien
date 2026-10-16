using System.Collections;
using UnityEngine;

namespace I2.Loc
{
	public class CoroutineManager : MonoBehaviour
	{
		private static CoroutineManager mInstance;

		private static CoroutineManager pInstance => null;

		private void Awake()
		{
		}

		public static Coroutine Start(IEnumerator coroutine)
		{
			return null;
		}
	}
}
