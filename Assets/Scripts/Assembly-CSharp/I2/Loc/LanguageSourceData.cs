using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace I2.Loc
{
	[Serializable]
	[ExecuteInEditMode]
	public class LanguageSourceData
	{
		public enum MissingTranslationAction
		{
			Empty = 0,
			Fallback = 1,
			ShowWarning = 2,
			ShowTerm = 3
		}

		public enum eAllowUnloadLanguages
		{
			Never = 0,
			OnlyInDevice = 1,
			EditorAndDevice = 2
		}

		public enum eGoogleUpdateFrequency
		{
			Always = 0,
			Never = 1,
			Daily = 2,
			Weekly = 3,
			Monthly = 4,
			OnlyOnce = 5,
			EveryOtherDay = 6
		}

		public enum eGoogleUpdateSynchronization
		{
			Manual = 0,
			OnSceneLoaded = 1,
			AsSoonAsDownloaded = 2
		}

		[CompilerGenerated]
		private sealed class _003CImport_Google_Coroutine_003Ed__65 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public LanguageSourceData _003C_003E4__this;

			public bool JustCheck;

			private UnityWebRequest _003Cwww_003E5__2;

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
			public _003CImport_Google_Coroutine_003Ed__65(int _003C_003E1__state)
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

		[NonSerialized]
		public ILanguageSource owner;

		public bool UserAgreesToHaveItOnTheScene;

		public bool UserAgreesToHaveItInsideThePluginsFolder;

		public bool GoogleLiveSyncIsUptoDate;

		[NonSerialized]
		public bool mIsGlobalSource;

		public List<TermData> mTerms;

		public bool CaseInsensitiveTerms;

		[NonSerialized]
		public Dictionary<string, TermData> mDictionary;

		public MissingTranslationAction OnMissingTranslation;

		public string mTerm_AppName;

		public List<LanguageData> mLanguages;

		public bool IgnoreDeviceLanguage;

		public eAllowUnloadLanguages _AllowUnloadingLanguages;

		public string Google_WebServiceURL;

		public string Google_SpreadsheetKey;

		public string Google_SpreadsheetName;

		public string Google_LastUpdatedVersion;

		public eGoogleUpdateFrequency GoogleUpdateFrequency;

		public eGoogleUpdateFrequency GoogleInEditorCheckFrequency;

		public eGoogleUpdateSynchronization GoogleUpdateSynchronization;

		public float GoogleUpdateDelay;

		[CompilerGenerated]
		private LanguageSource.fnOnSourceUpdated m_Event_OnSourceUpdateFromGoogle;

		public List<UnityEngine.Object> Assets;

		[NonSerialized]
		public Dictionary<string, UnityEngine.Object> mAssetDictionary;

		private string mDelayedGoogleData;

		public static string EmptyCategory;

		public static char[] CategorySeparators;

		public UnityEngine.Object ownerObject => null;

		public event LanguageSource.fnOnSourceUpdated Event_OnSourceUpdateFromGoogle
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

		public void Awake()
		{
		}

		public void OnDestroy()
		{
		}

		public bool IsEqualTo(LanguageSourceData Source)
		{
			return false;
		}

		internal bool ManagerHasASimilarSource()
		{
			return false;
		}

		public void ClearAllData()
		{
		}

		public bool IsGlobalSource()
		{
			return false;
		}

		public void Editor_SetDirty()
		{
		}

		public void UpdateAssetDictionary()
		{
		}

		public UnityEngine.Object FindAsset(string Name)
		{
			return null;
		}

		public bool HasAsset(UnityEngine.Object Obj)
		{
			return false;
		}

		public void AddAsset(UnityEngine.Object Obj)
		{
		}

		public string Export_I2CSV(string Category, char Separator = ',', bool specializationsAsRows = true)
		{
			return null;
		}

		private static void AppendI2Term(StringBuilder Builder, int nLanguages, string Term, TermData termData, char Separator, string forceSpecialization)
		{
		}

		private static void AppendI2Text(StringBuilder Builder, string text)
		{
		}

		private string Export_Language_to_Cache(int langIndex, bool fillTermWithFallback)
		{
			return null;
		}

		public string Export_CSV(string Category, char Separator = ',', bool specializationsAsRows = true)
		{
			return null;
		}

		private static void AppendTerm(StringBuilder Builder, int nLanguages, string Term, TermData termData, string specialization, char Separator)
		{
		}

		private static void AppendString(StringBuilder Builder, string Text, char Separator)
		{
		}

		private static void AppendTranslation(StringBuilder Builder, string Text, char Separator, string tags)
		{
		}

		public UnityWebRequest Export_Google_CreateWWWcall(eSpreadsheetUpdateMode UpdateMode = eSpreadsheetUpdateMode.Replace)
		{
			return null;
		}

		private string Export_Google_CreateData()
		{
			return null;
		}

		public string Import_CSV(string Category, string CSVstring, eSpreadsheetUpdateMode UpdateMode = eSpreadsheetUpdateMode.Replace, char Separator = ',')
		{
			return null;
		}

		public string Import_I2CSV(string Category, string I2CSVstring, eSpreadsheetUpdateMode UpdateMode = eSpreadsheetUpdateMode.Replace)
		{
			return null;
		}

		public string Import_CSV(string Category, List<string[]> CSV, eSpreadsheetUpdateMode UpdateMode = eSpreadsheetUpdateMode.Replace)
		{
			return null;
		}

		private bool ArrayContains(string MainText, params string[] texts)
		{
			return false;
		}

		public static eTermType GetTermType(string type)
		{
			return eTermType.Text;
		}

		private void Import_Language_from_Cache(int langIndex, string langData, bool useFallback, bool onlyCurrentSpecialization)
		{
		}

		public static void FreeUnusedLanguages()
		{
		}

		public void Import_Google_FromCache()
		{
		}

		private bool IsNewerVersion(string currentVersion, string newVersion)
		{
			return false;
		}

		public void Import_Google(bool ForceUpdate, bool justCheck)
		{
		}

		private string GetSourcePlayerPrefName()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CImport_Google_Coroutine_003Ed__65))]
		private IEnumerator Import_Google_Coroutine(bool JustCheck)
		{
			return null;
		}

		private void ApplyDownloadedDataOnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
		}

		public void ApplyDownloadedDataFromGoogle()
		{
		}

		public UnityWebRequest Import_Google_CreateWWWcall(bool ForceUpdate, bool justCheck)
		{
			return null;
		}

		public bool HasGoogleSpreadsheet()
		{
			return false;
		}

		public string Import_Google_Result(string JsonString, eSpreadsheetUpdateMode UpdateMode, bool saveInPlayerPrefs = false)
		{
			return null;
		}

		public int GetLanguageIndex(string language, bool AllowDiscartingRegion = true, bool SkipDisabled = true)
		{
			return 0;
		}

		public LanguageData GetLanguageData(string language, bool AllowDiscartingRegion = true)
		{
			return null;
		}

		public bool IsCurrentLanguage(int languageIndex)
		{
			return false;
		}

		public int GetLanguageIndexFromCode(string Code, bool exactMatch = true, bool ignoreDisabled = false)
		{
			return 0;
		}

		public static int GetCommonWordInLanguageNames(string Language1, string Language2)
		{
			return 0;
		}

		public static bool AreTheSameLanguage(string Language1, string Language2)
		{
			return false;
		}

		public static string GetLanguageWithoutRegion(string Language)
		{
			return null;
		}

		public void AddLanguage(string LanguageName)
		{
		}

		public void AddLanguage(string LanguageName, string LanguageCode)
		{
		}

		public void RemoveLanguage(string LanguageName)
		{
		}

		public List<string> GetLanguages(bool skipDisabled = true)
		{
			return null;
		}

		public List<string> GetLanguagesCode(bool allowRegions = true, bool skipDisabled = true)
		{
			return null;
		}

		public bool IsLanguageEnabled(string Language)
		{
			return false;
		}

		public void EnableLanguage(string Language, bool bEnabled)
		{
		}

		public bool AllowUnloadingLanguages()
		{
			return false;
		}

		private string GetSavedLanguageFileName(int languageIndex)
		{
			return null;
		}

		public void LoadLanguage(int languageIndex, bool UnloadOtherLanguages, bool useFallback, bool onlyCurrentSpecialization, bool forceLoad)
		{
		}

		public void LoadAllLanguages(bool forceLoad = false)
		{
		}

		public void UnloadLanguage(int languageIndex)
		{
		}

		public void SaveLanguages(bool unloadAll, PersistentStorage.eFileType fileLocation = PersistentStorage.eFileType.Temporal)
		{
		}

		public bool HasUnloadedLanguages()
		{
			return false;
		}

		public List<string> GetCategories(bool OnlyMainCategory = false, List<string> Categories = null)
		{
			return null;
		}

		public static string GetKeyFromFullTerm(string FullTerm, bool OnlyMainCategory = false)
		{
			return null;
		}

		public static string GetCategoryFromFullTerm(string FullTerm, bool OnlyMainCategory = false)
		{
			return null;
		}

		public static void DeserializeFullTerm(string FullTerm, out string Key, out string Category, bool OnlyMainCategory = false)
		{
			Key = null;
			Category = null;
		}

		public void UpdateDictionary(bool force = false)
		{
		}

		public string GetTranslation(string term, string overrideLanguage = null, string overrideSpecialization = null, bool skipDisabled = false, bool allowCategoryMistmatch = false)
		{
			return null;
		}

		public bool TryGetTranslation(string term, out string Translation, string overrideLanguage = null, string overrideSpecialization = null, bool skipDisabled = false, bool allowCategoryMistmatch = false)
		{
			Translation = null;
			return false;
		}

		private bool TryGetFallbackTranslation(TermData termData, out string Translation, int langIndex, string overrideSpecialization = null, bool skipDisabled = false)
		{
			Translation = null;
			return false;
		}

		public TermData AddTerm(string term)
		{
			return null;
		}

		public TermData GetTermData(string term, bool allowCategoryMistmatch = false)
		{
			return null;
		}

		public bool ContainsTerm(string term)
		{
			return false;
		}

		public List<string> GetTermsList(string Category = null)
		{
			return null;
		}

		public TermData AddTerm(string NewTerm, eTermType termType, bool SaveSource = true)
		{
			return null;
		}

		public void RemoveTerm(string term)
		{
		}

		public static void ValidateFullTerm(ref string Term)
		{
		}
	}
}
