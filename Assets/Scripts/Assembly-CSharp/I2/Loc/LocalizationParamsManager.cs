using System;
using System.Collections.Generic;
using UnityEngine;

namespace I2.Loc
{
	public class LocalizationParamsManager : MonoBehaviour, ILocalizationParamsManager
	{
		[Serializable]
		public struct ParamValue
		{
			public string Name;

			public string Value;
		}

		[SerializeField]
		public List<ParamValue> _Params;

		public bool _IsGlobalManager;

		public string GetParameterValue(string ParamName)
		{
			return null;
		}

		public void SetParameterValue(string ParamName, string ParamValue, bool localize = true)
		{
		}

		public void OnLocalize()
		{
		}

		public virtual void OnEnable()
		{
		}

		public void DoAutoRegister()
		{
		}

		public void OnDisable()
		{
		}
	}
}
