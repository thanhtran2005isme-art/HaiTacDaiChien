using UnityEngine;

namespace I2.Loc
{
	public class LocalizeTarget_UnityStandard_Prefab : LocalizeTarget<GameObject>
	{
		static LocalizeTarget_UnityStandard_Prefab()
		{
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
		}

		public override bool IsValid(Localize cmp)
		{
			return false;
		}

		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		public override bool CanUseSecondaryTerm()
		{
			return false;
		}

		public override bool AllowMainTermToBeRTL()
		{
			return false;
		}

		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = null;
			secondaryTerm = null;
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
		}

		private Transform InstantiateNewPrefab(Localize cmp, string mainTranslation)
		{
			return null;
		}
	}
}
