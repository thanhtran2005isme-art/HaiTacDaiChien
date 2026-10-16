using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LoginScene : BaseScene
{
	[CompilerGenerated]
	private sealed class _003CLoadBackgroundLoginFromAssetBundle_003Ed__24 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public LoginScene _003C_003E4__this;

		private GameObject _003CprefabBgr_003E5__2;

		private WWW _003Cwww_003E5__3;

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
		public _003CLoadBackgroundLoginFromAssetBundle_003Ed__24(int _003C_003E1__state)
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

	public const int LOGIN_TYPE_ACCOUNT = 1;

	public const int LOGIN_TYPE_QUICK = 2;

	public const int LOGIN_TYPE_FB = 3;

	public const int LOGIN_TYPE_UNKNOWN = 100;

	public const int UPDATE_NONE = 0;

	public const int UPDATE_RECOMMEND = 1;

	public const int UPDATE_FORCE = 2;

	public GameObject m_GoPanelBlock;

	public GameObject m_BoxServers;

	public GameObject m_BoxLogin;

	public GameObject m_BoxRegister;

	public PanelInitData m_PanelInitData;

	public PanelLoadingAsset m_PanelLoadingAsset;

	public static LoginScene Instance;

	public Text m_TextVersion;

	public Text m_TextBuild;

	public Image m_ImageLogo18Plus;

	public GameObject m_GoButtonClearAsset;

	public Transform m_TfThemeBgrContainer;

	private List<DataNotifyNew> lstDataNotifyNew;

	private AssetBundle _assetBgrLogin;

	public const string ASSET_TEST_DATA = "{\"urlPrefix\":\"http://320.gsscdn.com/test/android/\",\"infoFile\":\"data.txt\",\"assets\":[{\"id\":18,\"name\":\"theme_log\",\"file\":\"bgr_login\",\"v\":2,\"s\":null}]}";

	public const bool TEST_ASSET_ON = false;

	private const int STATUS_LOGIN_SUCCESS = 0;

	private const int STATUS_LOGIN_MAINTENANCE = 5;

	protected override void Awake()
	{
	}

	private void LoadClientLanguage()
	{
	}

	public void Test()
	{
	}

	[IteratorStateMachine(typeof(_003CLoadBackgroundLoginFromAssetBundle_003Ed__24))]
	private IEnumerator LoadBackgroundLoginFromAssetBundle()
	{
		return null;
	}

	protected override void Start()
	{
	}

	public override void OnDestroy()
	{
	}

	public void ButtonClearAssetClick()
	{
	}

	public void ButtonInfoClick()
	{
	}

	public void ShowBoxRegister()
	{
	}

	public void ShowBoxLogin()
	{
	}

	private void OnGosuLoginCallback(bool isSuccess, Dictionary<string, string> data)
	{
	}

	public void ShowBoxServers()
	{
	}

	private void HideAllBox()
	{
	}

	private void Update()
	{
	}

	public void UnloadAssetBackgroundTheme()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void LoadTestBundle()
	{
	}

	private void LoadHeroesBundleForEditor()
	{
	}

	private void ProcessLoginAccount(int eventId, object data)
	{
	}
}
