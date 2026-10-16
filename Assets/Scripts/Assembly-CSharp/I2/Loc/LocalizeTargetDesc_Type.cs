using UnityEngine;

namespace I2.Loc
{
	public class LocalizeTargetDesc_Type<T, G> : LocalizeTargetDesc<G> where T : Object where G : LocalizeTarget<T>
	{
		public override bool CanLocalize(Localize cmp)
		{
			return false;
		}

		public override ILocalizeTarget CreateTarget(Localize cmp)
		{
			return null;
		}
	}
}
