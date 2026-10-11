using System.Collections.Generic;

namespace I2.Loc
{
	public class SpecializationManager : BaseSpecializationManager
	{
		public static SpecializationManager Singleton;

		private SpecializationManager()
		{
		}

		public static string GetSpecializedText(string text, string specialization = null)
		{
			return null;
		}

		public static string SetSpecializedText(string text, string newText, string specialization)
		{
			return null;
		}

		public static string SetSpecializedText(Dictionary<string, string> specializations)
		{
			return null;
		}

		public static Dictionary<string, string> GetSpecializations(string text, Dictionary<string, string> buffer = null)
		{
			return null;
		}

		public static void AppendSpecializations(string text, List<string> list = null)
		{
		}
	}
}
