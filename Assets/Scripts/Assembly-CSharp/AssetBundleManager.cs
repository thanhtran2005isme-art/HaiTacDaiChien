using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AssetBundleManager
{
	[CompilerGenerated]
	private sealed class _003CDownloadAllAssetBundle_003Ed__71 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AssetBundleManager _003C_003E4__this;

		private bool _003ChasError_003E5__2;

		private List<AssetInfo> _003ClstAssetInfoNeedUpdate_003E5__3;

		private List<AssetInfo>.Enumerator _003C_003E7__wrap3;

		private AssetInfo _003CassetInfo_003E5__5;

		private WWW _003Cwww_003E5__6;

		private int _003CnumBundle_003E5__7;

		private int _003Ci_003E5__8;

		private string _003CassetDisplayName_003E5__9;

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
		public _003CDownloadAllAssetBundle_003Ed__71(int _003C_003E1__state)
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

		private void _003C_003Em__Finally1()
		{
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	private static AssetBundleManager m_Instance;

	public const int BUNDLE_DB = 1;

	public const int BUNDLE_HERO_HEAD = 2;

	public const int BUNDLE_HERO_SPINE_UPDATE = 3;

	public const int BUNDLE_HERO_SPINE_1 = 31;

	public const int BUNDLE_HERO_SPINE_2 = 32;

	public const int BUNDLE_HERO_SPINE_3 = 33;

	public const int BUNDLE_HERO_SPINE_4 = 34;

	public const int BUNDLE_HERO_SPINE_5 = 35;

	public const int BUNDLE_HERO_SKIN = 40;

	public const int BUNDLE_HERO_SKILL = 4;

	public const int BUNDLE_PET_SPINE = 5;

	public const int BUNDLE_PET_HEAD = 6;

	public const int BUNDLE_ITEM_ARTIFACT = 7;

	public const int BUNDLE_ITEM_CONSUMABLE = 8;

	public const int BUNDLE_ITEM_EQUIPMENT = 9;

	public const int BUNDLE_ITEM_SHARD = 10;

	public const int BUNDLE_SKILL_ICON = 11;

	public const int BUNDLE_EVENT = 12;

	public const int BUNDLE_AVATAR = 13;

	public const int BUNDLE_HERO_BODY_EFFECT = 14;

	public const int BUNDLE_HERO_BUFF_EFFECT = 15;

	public const int BUNDLE_HERO_ART = 16;

	public const int BUNDLE_HERO_THUMB = 17;

	public const int BUNDLE_BGR_LOGIN = 18;

	public const int BUNDLE_DB_EX = 101;

	public const int BUNDLE_HERO_HEAD_EX = 102;

	public const int BUNDLE_HERO_SKILL_EX = 104;

	public const int BUNDLE_PET_SPINE_EX = 105;

	public const int BUNDLE_PET_HEAD_EX = 106;

	public const int BUNDLE_ITEM_ARTIFACT_EX = 107;

	public const int BUNDLE_ITEM_CONSUMABLE_EX = 108;

	public const int BUNDLE_ITEM_EQUIPMENT_EX = 109;

	public const int BUNDLE_ITEM_SHARD_EX = 110;

	public const int BUNDLE_SKILL_ICON_EX = 111;

	public const int BUNDLE_EVENT_EX = 112;

	public const int BUNDLE_AVATAR_EX = 113;

	public const int BUNDLE_HERO_BODY_EFFECT_EX = 114;

	public const int BUNDLE_HERO_ART_EX = 116;

	public const int BUNDLE_HERO_THUMB_EX = 117;

	public const int BUNDLE_LUA = 115;

	public const int BUNDLE_DB_EN = 120;

	public const int BUNDLE_DB_EN_EX = 121;

	public const int BUNDLE_DB_TH = 124;

	public const int BUNDLE_DB_TH_EX = 125;

	public const int BUNDLE_DB_ZH = 126;

	public const int BUNDLE_DB_ZH_EX = 127;

	public const int BUNDLE_EVENT_GLOBAL = 122;

	public const int BUNDLE_EVENT_GLOBAL_EX = 123;

	public const int BUNDLE_LUA_COMMON = 500;

	public const int BUNDLE_LUA_DICE = 501;

	public const int BUNDLE_LUA_SHIP_DECOR = 502;

	public const int BUNDLE_LUA_SHIP_FURNITURE = 503;

	public const int BUNDLE_LUA_FISH_MAN = 504;

	public const int BUNDLE_LUA_POT = 508;

	private List<AssetInfo> m_LstAssetInfo;

	private Dictionary<int, AssetBundle> m_DictAssetBundle;

	public Action<string, float, string> m_DownloadAssetProgressCallback;

	public Action<bool> m_DownloadAssetDoneCallback;

	private List<int> _listAssetBundleIgnoreLoading;

	public static AssetBundleManager GetInstance()
	{
		return null;
	}

	private AssetBundleManager()
	{
	}

	public void GenAssetDataFile()
	{
	}

	public void SetListAssetBundleInfo(List<AssetInfo> lstAssetInfo)
	{
	}

	public AssetBundle GetAssetBundle(int bundleId)
	{
		return null;
	}

	public void SetAssetBundle(int bundleId, string pathAssetBundle)
	{
	}

	public Sprite LoadSprite(int bundleId, string imageName)
	{
		return null;
	}

	public GameObject LoadLuaPrefab(int bundleId, string prefabName)
	{
		return null;
	}

	public Sprite LoadLuaSprite(int bundleId, string imageName)
	{
		return null;
	}

	public string LoadText(int bundleId, string fileName)
	{
		return null;
	}

	public UnityEngine.Object LoadUnityObject(int bundleId, string assetName)
	{
		return null;
	}

	[Obsolete]
	[IteratorStateMachine(typeof(_003CDownloadAllAssetBundle_003Ed__71))]
	public IEnumerator DownloadAllAssetBundle()
	{
		return null;
	}

	public void UnloadAllAssetBundle()
	{
	}
}
