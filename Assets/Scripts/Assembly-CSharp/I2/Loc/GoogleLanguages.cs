using System.Collections.Generic;

namespace I2.Loc
{
	public static class GoogleLanguages
	{
		public struct LanguageCodeDef
		{
			public string Code;

			public string GoogleCode;

			public bool HasJoinedWords;

			public int PluralRule;
		}

		public static Dictionary<string, LanguageCodeDef> mLanguageDef;

		public static string GetLanguageCode(string Filter, bool ShowWarnings = false)
		{
			return null;
		}

		public static List<string> GetLanguagesForDropdown(string Filter, string CodesToExclude)
		{
			return null;
		}

		private static bool LanguageMatchesFilter(string Language, string[] Filters)
		{
			return false;
		}

		public static string GetFormatedLanguageName(string Language)
		{
			return null;
		}

		public static string GetCodedLanguage(string Language, string code)
		{
			return null;
		}

		public static void UnPackCodeFromLanguageName(string CodedLanguage, out string Language, out string code)
		{
			Language = null;
			code = null;
		}

		public static string GetGoogleLanguageCode(string InternationalCode)
		{
			return null;
		}

		public static string GetLanguageName(string code, bool useParenthesesForRegion = false, bool allowDiscardRegion = true)
		{
			return null;
		}

		public static List<string> GetAllInternationalCodes()
		{
			return null;
		}

		public static bool LanguageCode_HasJoinedWord(string languageCode)
		{
			return false;
		}

		private static int GetPluralRule(string langCode)
		{
			return 0;
		}

		public static bool LanguageHasPluralType(string langCode, string pluralType)
		{
			return false;
		}

		public static ePluralType GetPluralType(string langCode, int n)
		{
			return ePluralType.Zero;
		}

		public static int GetPluralTestNumber(string langCode, ePluralType pluralType)
		{
			return 0;
		}

		private static bool inRange(int amount, int min, int max)
		{
			return false;
		}
	}
}
