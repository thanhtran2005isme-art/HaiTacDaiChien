using UnityEngine;

namespace I2.Loc
{
	[CreateAssetMenu(fileName = "I2Languages", menuName = "I2 Localization/LanguageSource", order = 1)]
	public class LanguageSourceAsset : ScriptableObject, ILanguageSource
	{
		public LanguageSourceData mSource;

		public LanguageSourceData SourceData
		{
			get
			{
				return null;
			}
			set
			{
			}
		}
	}
}
