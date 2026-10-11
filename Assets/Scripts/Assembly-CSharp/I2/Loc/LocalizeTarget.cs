using UnityEngine;

namespace I2.Loc
{
	public abstract class LocalizeTarget<T> : ILocalizeTarget where T : Object
	{
		public T mTarget;

		public override bool IsValid(Localize cmp)
		{
			return false;
		}
	}
}
