using System;
using UnityEngine;

namespace I2.Loc
{
	[Serializable]
	public class EventCallback
	{
		public MonoBehaviour Target;

		public string MethodName;

		public void Execute(UnityEngine.Object Sender = null)
		{
		}

		public bool HasCallback()
		{
			return false;
		}
	}
}
