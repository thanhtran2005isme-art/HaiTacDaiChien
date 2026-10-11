using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace I2.Loc
{
	public class LocalizationReader
	{
		public static Dictionary<string, string> ReadTextAsset(TextAsset asset)
		{
			return null;
		}

		public static bool TextAsset_ReadLine(string line, out string key, out string value, out string category, out string comment, out string termType)
		{
			key = null;
			value = null;
			category = null;
			comment = null;
			termType = null;
			return false;
		}

		public static string ReadCSVfile(string Path, Encoding encoding)
		{
			return null;
		}

		public static List<string[]> ReadCSV(string Text, char Separator = ',')
		{
			return null;
		}

		private static string[] ParseCSVline(string Line, ref int iStart, char Separator)
		{
			return null;
		}

		private static void AddCSVtoken(ref List<string> list, ref string Line, int iEnd, ref int iWordStart)
		{
		}

		public static List<string[]> ReadI2CSV(string Text)
		{
			return null;
		}

		public static void ValidateFullTerm(ref string Term)
		{
		}

		public static string EncodeString(string str)
		{
			return null;
		}

		public static string DecodeString(string str)
		{
			return null;
		}
	}
}
