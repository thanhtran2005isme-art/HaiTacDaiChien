using System;
using System.Collections.Generic;
using UnityEngine;

public class SDKBridgeService : MonoBehaviour
{
	public const string SDKGameObject = "SDKService";

	public static string CallbackFunctionName;

	private static SDKBridgeService _instance;

	public ISDKService _skdService;

	public Action<string, string, string> LoginSuccessCb;

	public Action<string> LoginFailCb;

	public Action LogoutSuccessCb;

	public Action LogoutFailCb;

	public Action<bool> InitSdkCallback;

	public static SDKBridgeService Instance => null;

	private void InitBridge()
	{
	}

	public void InitSDK()
	{
	}

	public void HandleNativeServiceData(string data)
	{
	}

	public void ShowLogin()
	{
	}

	public void DoLogOut()
	{
	}

	public void DoPaymentIAP2(string productId, string productName, string currencyUnit, string amount, string serverID, string characterID, string extraInfo, Action<bool, Dictionary<string, string>> onPaymentIAPCallback = null)
	{
	}

	public void DoPaymentIAP(string usernameGosu, string productId, string server, string orderId, string orderInfo, string amount, string character, string extraInfo, Action<bool, Dictionary<string, string>> onPaymentIAPCallback)
	{
	}

	public void TrackingEvent(int type, string level)
	{
	}

	public void TrackingEventCustom(string eventName, Dictionary<string, string> data)
	{
	}
}
