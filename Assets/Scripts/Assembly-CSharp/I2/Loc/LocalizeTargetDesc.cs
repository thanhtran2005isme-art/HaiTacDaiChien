using System;

namespace I2.Loc
{
	public abstract class LocalizeTargetDesc<T> : ILocalizeTargetDescriptor where T : ILocalizeTarget
	{
		public override ILocalizeTarget CreateTarget(Localize cmp)
		{
			return null;
		}

		public override Type GetTargetType()
		{
			return null;
		}
	}
}
