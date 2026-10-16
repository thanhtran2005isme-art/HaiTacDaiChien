using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace I2.Loc
{
	public static class I2Utils
	{
		public const string ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";

		public const string NumberChars = "0123456789";

		public const string ValidNameSymbols = ".-_$#@*()[]{}+:?!&',^=<>~`";

		public static string ReverseText(string source)
		{
			return null;
		}

		public static string RemoveNonASCII(string text, bool allowCategory = false)
		{
			return null;
		}

		public static string GetValidTermName(string text, bool allowCategory = false)
		{
			return null;
		}

		public static string SplitLine(string line, int maxCharacters)
		{
			return null;
		}

		public static bool FindNextTag(string line, int iStart, out int tagStart, out int tagEnd)
		{
			tagStart = default;
			tagEnd = default;
			return false;
		}

		public static string RemoveTags(string text)
		{
			return null;
		}

		public static bool RemoveResourcesPath(ref string sPath)
		{
			return false;
		}

		public static bool IsPlaying()
		{
			return false;
		}

		public static string GetPath(this Transform tr)
		{
			return null;
		}

		public static Transform FindObject(string objectPath)
		{
			return null;
		}

		public static Transform FindObject(Scene scene, string objectPath)
		{
			return null;
		}

		public static Transform FindObject(Transform root, string objectPath)
		{
			return null;
		}

		public static H FindInParents<H>(Transform tr) where H : Component
		{
			return null;
		}

		public static string GetCaptureMatch(Match match)
		{
			return null;
		}

		public static void SendWebRequest(UnityWebRequest www)
		{
		}
	}
}
