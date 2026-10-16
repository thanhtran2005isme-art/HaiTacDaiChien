using UnityEngine;

namespace I2.Loc
{
	public class RegisterGlobalParameters : MonoBehaviour, ILocalizationParamsManager
	{
		public virtual void OnEnable()
		{
		}

		public virtual void OnDisable()
		{
		}

		public virtual string GetParameterValue(string ParamName)
		{
			return null;
		}
	}
}
