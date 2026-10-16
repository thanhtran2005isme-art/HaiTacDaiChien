using System;

namespace I2.Loc
{
	[Serializable]
	public struct LocalizedString
	{
		public string mTerm;

		public bool mRTL_IgnoreArabicFix;

		public int mRTL_MaxLineLength;

		public bool mRTL_ConvertNumbers;

		public bool m_DontLocalizeParameters;

		public static implicit operator string(LocalizedString s)
		{
			return null;
		}

		public static implicit operator LocalizedString(string term)
		{
			return default;
		}

		public LocalizedString(LocalizedString str)
		{
			mTerm = null;
			mRTL_IgnoreArabicFix = false;
			mRTL_MaxLineLength = 0;
			mRTL_ConvertNumbers = false;
			m_DontLocalizeParameters = false;
		}

		public override string ToString()
		{
			return null;
		}
	}
}
