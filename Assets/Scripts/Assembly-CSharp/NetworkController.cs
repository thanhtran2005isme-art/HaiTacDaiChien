using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using pbdson;

public class NetworkController : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CWaitForHttpRequest_003Ed__25 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string urlRequest;

		public NetworkController _003C_003E4__this;

		public int serviceId;

		private UnityWebRequest _003CwebRequest_003E5__2;

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
		public _003CWaitForHttpRequest_003Ed__25(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CWaitForRequest_003Ed__24 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NetworkController _003C_003E4__this;

		public RequestData requestData;

		public string url;

		private string _003CstrLstSentService_003E5__2;

		private UnityWebRequest _003CwebRequest_003E5__3;

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
		public _003CWaitForRequest_003Ed__24(int _003C_003E1__state)
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

	private static NetworkController m_Instance;

	private List<INetEventHandler> m_LstEventHandler;

	private List<HttpRequestData> m_LstQueuedHttpRequest;

	private List<PbAction> m_LstQueuedSendAction;

	private List<PbAction> m_LstQueuedReceiveAction;

	private readonly object syncLockSend;

	private readonly object syncLockReceive;

	[HideInInspector]
	public string m_UrlGameServer;

	public string MSG_ERRROR_SERVICE;

	private List<INetEventHandler> m_LstTempEventHandler;

	public static NetworkController GetInstance()
	{
		return null;
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void AddNetworkEventHandler(INetEventHandler hdl)
	{
	}

	public void RemoveNetworkEventHandler(INetEventHandler hdl)
	{
	}

	public void SendHttpRequest(int serviceId, string urlServer, List<HttpParam> lstParams = null)
	{
	}

	public void SendProtoPackage(int serviceId, object protoObject = null)
	{
	}

	private PbAction GenAction(int serviceId, object data = null)
	{
		return null;
	}

	private byte[] Selialize(object data)
	{
		return null;
	}

	public static T ParsePb<T>(byte[] data)
	{
		return default;
	}

	public static object ParsePb(string typeName, byte[] data)
	{
		return null;
	}

	private void UpdateSendListPackage()
	{
	}

	private void SendPackage(RequestData request)
	{
	}

	private void UpdateSendListHttpRequest()
	{
	}

	private void SendHttpRequest(HttpRequestData request)
	{
	}

	[IteratorStateMachine(typeof(_003CWaitForRequest_003Ed__24))]
	private IEnumerator WaitForRequest(string url, RequestData requestData)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CWaitForHttpRequest_003Ed__25))]
	private IEnumerator WaitForHttpRequest(int serviceId, string urlRequest)
	{
		return null;
	}

	private void UpdateProcessReceivedPackage()
	{
	}

	private void PreProcessKEvent(int eventId, byte[] data)
	{
	}

	public void ProcessPackageEvent(int action, byte[] byte_data)
	{
	}

	private void Update()
	{
	}
}
