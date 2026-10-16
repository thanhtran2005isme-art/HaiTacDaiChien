using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace I2.Loc
{
	[AddComponentMenu("I2/Localization/I2 Localize")]
	public class Localize : MonoBehaviour
	{
		public enum TermModification
		{
			DontModify = 0,
			ToUpper = 1,
			ToLower = 2,
			ToUpperFirst = 3,
			ToTitle = 4
		}

		public string mTerm;

		public string mTermSecondary;

		[NonSerialized]
		public string FinalTerm;

		[NonSerialized]
		public string FinalSecondaryTerm;

		public TermModification PrimaryTermModifier;

		public TermModification SecondaryTermModifier;

		public string TermPrefix;

		public string TermSuffix;

		public bool LocalizeOnAwake;

		private string LastLocalizedLanguage;

		public bool IgnoreRTL;

		public int MaxCharactersInRTL;

		public bool IgnoreNumbersInRTL;

		public bool CorrectAlignmentForRTL;

		public bool AddSpacesToJoinedLanguages;

		public bool AllowLocalizedParameters;

		public List<UnityEngine.Object> TranslatedObjects;

		[NonSerialized]
		public Dictionary<string, UnityEngine.Object> mAssetDictionary;

		public UnityEvent LocalizeEvent;

		public static string MainTranslation;

		public static string SecondaryTranslation;

		public static string CallBackTerm;

		public static string CallBackSecondaryTerm;

		public static Localize CurrentLocalizeComponent;

		public bool AlwaysForceLocalize;

		[SerializeField]
		public EventCallback LocalizeCallBack;

		public bool mGUI_ShowReferences;

		public bool mGUI_ShowTems;

		public bool mGUI_ShowCallback;

		public ILocalizeTarget mLocalizeTarget;

		public string mLocalizeTargetName;

		public string Term
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string SecondaryTerm
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		public bool HasCallback()
		{
			return false;
		}

		public void OnLocalize(bool Force = false)
		{
		}

		public bool FindTarget()
		{
			return false;
		}

		public void GetFinalTerms(out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = null;
			secondaryTerm = null;
		}

		public string GetMainTargetsText()
		{
			return null;
		}

		public void SetFinalTerms(string Main, string Secondary, out string primaryTerm, out string secondaryTerm, bool RemoveNonASCII)
		{
			primaryTerm = null;
			secondaryTerm = null;
		}

		public void SetTerm(string primary)
		{
		}

		public void SetTerm(string primary, string secondary)
		{
		}

		internal T GetSecondaryTranslatedObj<T>(ref string mainTranslation, ref string secondaryTranslation) where T : UnityEngine.Object
		{
			return null;
		}

		public void UpdateAssetDictionary()
		{
		}

		internal T GetObject<T>(string Translation) where T : UnityEngine.Object
		{
			return null;
		}

		private T GetTranslatedObject<T>(string Translation) where T : UnityEngine.Object
		{
			return null;
		}

		private void DeserializeTranslation(string translation, out string value, out string secondary)
		{
			value = null;
			secondary = null;
		}

		public T FindTranslatedObject<T>(string value) where T : UnityEngine.Object
		{
			return null;
		}

		public bool HasTranslatedObject(UnityEngine.Object Obj)
		{
			return false;
		}

		public void AddTranslatedObject(UnityEngine.Object Obj)
		{
		}

		public void SetGlobalLanguage(string Language)
		{
		}
	}
}
