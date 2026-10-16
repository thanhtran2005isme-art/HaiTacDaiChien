using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

namespace I2.Loc
{
	public static class GoogleTranslation
	{
		public delegate void fnOnTranslated(string Translation, string Error);

		public delegate void fnOnTranslationReady(Dictionary<string, TranslationQuery> dict, string error);

		[CompilerGenerated]
		private sealed class _003CWaitForTranslations_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CWaitForTranslations_003Ed__11(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		private static List<UnityWebRequest> mCurrentTranslations;

		private static List<TranslationJob> mTranslationJobs;

		public static bool CanTranslate()
		{
			return false;
		}

		public static void Translate(string text, string LanguageCodeFrom, string LanguageCodeTo, fnOnTranslated OnTranslationReady)
		{
		}

		public static string ForceTranslate(string text, string LanguageCodeFrom, string LanguageCodeTo)
		{
			return null;
		}

		public static void Translate(Dictionary<string, TranslationQuery> requests, fnOnTranslationReady OnTranslationReady, bool usePOST = true)
		{
		}

		public static bool ForceTranslate(Dictionary<string, TranslationQuery> requests, bool usePOST = true)
		{
			return false;
		}

		public static List<string> ConvertTranslationRequest(Dictionary<string, TranslationQuery> requests, bool encodeGET)
		{
			return null;
		}

		private static void AddTranslationJob(TranslationJob job)
		{
		}

		[IteratorStateMachine(typeof(_003CWaitForTranslations_003Ed__11))]
		private static IEnumerator WaitForTranslations()
		{
			return null;
		}

		public static string ParseTranslationResult(string html, Dictionary<string, TranslationQuery> requests)
		{
			return null;
		}

		public static bool IsTranslating()
		{
			return false;
		}

		public static void CancelCurrentGoogleTranslations()
		{
		}

		public static void CreateQueries(string text, string LanguageCodeFrom, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
		}

		private static void CreateQueries_Plurals(string text, string LanguageCodeFrom, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
		}

		public static void AddQuery(string text, string LanguageCodeFrom, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
		}

		private static string GetTranslation(string text, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			return null;
		}

		private static TranslationQuery FindQueryFromOrigText(string origText, Dictionary<string, TranslationQuery> dict)
		{
			return default;
		}

		public static bool HasParameters(string text)
		{
			return false;
		}

		public static string GetPluralParameter(string text, bool forceTag)
		{
			return null;
		}

		public static string GetPluralText(string text, string pluralType)
		{
			return null;
		}

		private static int FindClosingTag(string tag, MatchCollection matches, int startIndex)
		{
			return 0;
		}

		private static string GetGoogleNoTranslateTag(int tagNumber)
		{
			return null;
		}

		private static void ParseNonTranslatableElements(ref TranslationQuery query)
		{
		}

		public static string GetQueryResult(string text, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			return null;
		}

		public static string RebuildTranslation(string text, Dictionary<string, TranslationQuery> dict, string LanguageCodeTo)
		{
			return null;
		}

		private static string RebuildTranslation_Plural(string text, Dictionary<string, TranslationQuery> dict, string LanguageCodeTo)
		{
			return null;
		}

		public static string UppercaseFirst(string s)
		{
			return null;
		}

		public static string TitleCase(string s)
		{
			return null;
		}
	}
}
