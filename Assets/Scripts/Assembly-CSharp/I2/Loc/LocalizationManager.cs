using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

namespace I2.Loc
{
	public static class LocalizationManager
	{
		public delegate object _GetParam(string param);

		public delegate void OnLocalizeCallback();

		[CompilerGenerated]
		private sealed class _003CCoroutine_LocalizeAll_003Ed__86 : IEnumerator<object>, IEnumerator, IDisposable
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
			public _003CCoroutine_LocalizeAll_003Ed__86(int _003C_003E1__state)
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

		[CompilerGenerated]
		private sealed class _003CDelayed_Import_Google_003Ed__59 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public LanguageSourceData source;

			public bool justCheck;

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
			public _003CDelayed_Import_Google_003Ed__59(int _003C_003E1__state)
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

		private static string mCurrentLanguage;

		private static string mLanguageCode;

		private static CultureInfo mCurrentCulture;

		private static bool mChangeCultureInfo;

		public static bool IsRight2Left;

		public static bool HasJoinedWords;

		public static List<ILocalizationParamsManager> ParamManagers;

		private static string[] LanguagesRTL;

		public static List<LanguageSourceData> Sources;

		public static string[] GlobalSources;

		private static string mCurrentDeviceLanguage;

		public static List<ILocalizeTargetDescriptor> mLocalizeTargets;

		[CompilerGenerated]
		private static OnLocalizeCallback m_OnLocalizeEvent;

		private static bool mLocalizeIsScheduled;

		private static bool mLocalizeIsScheduledWithForcedValue;

		public static bool HighlightLocalizedTargets;

		public static string CurrentLanguage
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public static string CurrentLanguageCode
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public static string CurrentRegion
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public static string CurrentRegionCode
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public static CultureInfo CurrentCulture => null;

		public static event OnLocalizeCallback OnLocalizeEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static void InitializeIfNeeded()
		{
		}

		public static string GetVersion()
		{
			return null;
		}

		public static int GetRequiredWebServiceVersion()
		{
			return 0;
		}

		public static string GetWebServiceURL(LanguageSourceData source = null)
		{
			return null;
		}

		public static void SetLanguageAndCode(string LanguageName, string LanguageCode, bool RememberLanguage = true, bool Force = false)
		{
		}

		private static CultureInfo CreateCultureForCode(string code)
		{
			return null;
		}

		public static void EnableChangingCultureInfo(bool bEnable)
		{
		}

		private static void SetCurrentCultureInfo()
		{
		}

		private static void SelectStartupLanguage()
		{
		}

		public static bool HasLanguage(string Language, bool AllowDiscartingRegion = true, bool Initialize = true, bool SkipDisabled = true)
		{
			return false;
		}

		public static string GetSupportedLanguage(string Language, bool ignoreDisabled = false)
		{
			return null;
		}

		public static string GetLanguageCode(string Language)
		{
			return null;
		}

		public static string GetLanguageFromCode(string Code, bool exactMatch = true)
		{
			return null;
		}

		public static List<string> GetAllLanguages(bool SkipDisabled = true)
		{
			return null;
		}

		public static List<string> GetAllLanguagesCode(bool allowRegions = true, bool SkipDisabled = true)
		{
			return null;
		}

		public static bool IsLanguageEnabled(string Language)
		{
			return false;
		}

		private static void LoadCurrentLanguage()
		{
		}

		public static void PreviewLanguage(string NewLanguage)
		{
		}

		public static void AutoLoadGlobalParamManagers()
		{
		}

		public static void ApplyLocalizationParams(ref string translation, bool allowLocalizedParameters = true)
		{
		}

		public static void ApplyLocalizationParams(ref string translation, GameObject root, bool allowLocalizedParameters = true)
		{
		}

		public static void ApplyLocalizationParams(ref string translation, Dictionary<string, object> parameters, bool allowLocalizedParameters = true)
		{
		}

		public static void ApplyLocalizationParams(ref string translation, _GetParam getParam, bool allowLocalizedParameters = true)
		{
		}

		internal static string GetLocalizationParam(string ParamName, GameObject root)
		{
			return null;
		}

		private static string GetPluralType(MatchCollection matches, string langCode, _GetParam getParam)
		{
			return null;
		}

		public static string ApplyRTLfix(string line)
		{
			return null;
		}

		public static string ApplyRTLfix(string line, int maxCharacters, bool ignoreNumbers)
		{
			return null;
		}

		public static string FixRTL_IfNeeded(string text, int maxCharacters = 0, bool ignoreNumber = false)
		{
			return null;
		}

		public static bool IsRTL(string Code)
		{
			return false;
		}

		public static bool UpdateSources()
		{
			return false;
		}

		private static void UnregisterDeletededSources()
		{
		}

		private static void RegisterSceneSources()
		{
		}

		private static void RegisterSourceInResources()
		{
		}

		internal static void AddSource(LanguageSourceData Source)
		{
		}

		[IteratorStateMachine(typeof(_003CDelayed_Import_Google_003Ed__59))]
		private static IEnumerator Delayed_Import_Google(LanguageSourceData source, float delay, bool justCheck)
		{
			return null;
		}

		internal static void RemoveSource(LanguageSourceData Source)
		{
		}

		public static bool IsGlobalSource(string SourceName)
		{
			return false;
		}

		public static LanguageSourceData GetSourceContaining(string term, bool fallbackToFirst = true)
		{
			return null;
		}

		public static UnityEngine.Object FindAsset(string value)
		{
			return null;
		}

		public static void ApplyDownloadedDataFromGoogle()
		{
		}

		public static string GetCurrentDeviceLanguage(bool force = false)
		{
			return null;
		}

		private static void DetectDeviceLanguage()
		{
		}

		public static void RegisterTarget(ILocalizeTargetDescriptor desc)
		{
		}

		public static string GetTranslationGiang(string Term)
		{
			return null;
		}

		public static bool TryGetTranslationGiang(string Term, out string Translation)
		{
			Translation = null;
			return false;
		}

		public static string GetTranslation(string Term, bool FixForRTL = true, int maxLineLengthForRTL = 0, bool ignoreRTLnumbers = true, bool applyParameters = false, GameObject localParametersRoot = null, string overrideLanguage = null)
		{
			return null;
		}

		public static string GetTermTranslation(string Term, bool FixForRTL = true, int maxLineLengthForRTL = 0, bool ignoreRTLnumbers = true, bool applyParameters = false, GameObject localParametersRoot = null, string overrideLanguage = null)
		{
			return null;
		}

		public static bool TryGetTranslation(string Term, out string Translation, bool FixForRTL = true, int maxLineLengthForRTL = 0, bool ignoreRTLnumbers = true, bool applyParameters = false, GameObject localParametersRoot = null, string overrideLanguage = null)
		{
			Translation = null;
			return false;
		}

		public static T GetTranslatedObject<T>(string AssetName, Localize optionalLocComp = null) where T : UnityEngine.Object
		{
			return null;
		}

		public static T GetTranslatedObjectByTermName<T>(string Term, Localize optionalLocComp = null) where T : UnityEngine.Object
		{
			return null;
		}

		public static string GetAppName(string languageCode)
		{
			return null;
		}

		public static void LocalizeAll(bool Force = false)
		{
		}

		[IteratorStateMachine(typeof(_003CCoroutine_LocalizeAll_003Ed__86))]
		private static IEnumerator Coroutine_LocalizeAll()
		{
			return null;
		}

		private static void DoLocalizeAll(bool Force = false)
		{
		}

		public static List<string> GetCategories()
		{
			return null;
		}

		public static List<string> GetTermsList(string Category = null)
		{
			return null;
		}

		public static TermData GetTermData(string term)
		{
			return null;
		}

		public static TermData GetTermData(string term, out LanguageSourceData source)
		{
			source = null;
			return null;
		}
	}
}
