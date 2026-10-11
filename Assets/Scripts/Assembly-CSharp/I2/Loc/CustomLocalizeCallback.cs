using UnityEngine;
using UnityEngine.Events;

namespace I2.Loc
{
	[AddComponentMenu("I2/Localization/I2 Localize Callback")]
	public class CustomLocalizeCallback : MonoBehaviour
	{
		public UnityEvent _OnLocalize;

		public void OnEnable()
		{
		}

		public void OnDisable()
		{
		}

		public void OnLocalize()
		{
		}
	}
}
