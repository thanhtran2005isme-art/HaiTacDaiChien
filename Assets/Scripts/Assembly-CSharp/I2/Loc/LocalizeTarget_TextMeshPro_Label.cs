using TMPro;
using UnityEngine;

namespace I2.Loc
{
	public class LocalizeTarget_TextMeshPro_Label : LocalizeTarget<TextMeshPro>
	{
		private TextAlignmentOptions mAlignment_RTL;

		private TextAlignmentOptions mAlignment_LTR;

		private bool mAlignmentWasRTL;

		private bool mInitializeAlignment;

		static LocalizeTarget_TextMeshPro_Label()
		{
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
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

		internal static TMP_FontAsset GetTMPFontFromMaterial(Localize cmp, string matName)
		{
			return null;
		}

		internal static void InitAlignment_TMPro(bool isRTL, TextAlignmentOptions alignment, out TextAlignmentOptions alignLTR, out TextAlignmentOptions alignRTL)
		{
			alignLTR = default;
			alignRTL = default;
		}

		internal static void SetFont(TMP_Text label, TMP_FontAsset newFont)
		{
		}

		internal static void SetMaterial(TMP_Text label, Material newMat)
		{
		}
	}
}
