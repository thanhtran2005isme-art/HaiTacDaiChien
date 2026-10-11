using System;
using System.Collections.Generic;
using UnityEngine;

public class SdkBrigdeSerivce : MonoBehaviour
{
	public const int AF_START_TRIAL = 0;

	public const int AF_TUTORIAL_COMPLETION = 1;

	public const int AF_LEVEL_ACHIEVED = 2;

	public const int AF_ACHIEVEMENT_UNLOCKED = 3;

	public const int AF_SPENT_CREDIT = 4;

	private static SdkBrigdeSerivce _api;

	public const string IdBundle = "com.gosu.sdk.unity.SdkBridge";

	public const string GoName = "SdkBrigde";

	public const string FunctionCallback = "HandleNativeMessage";

	public static string clientId;

	private Action<bool, Dictionary<string, string>> _sdkLoginCallback;

	private Action<bool, Dictionary<string, string>> _sdkPaymentCallback;

	private Action<bool, Dictionary<string, string>> _sdkPaymentIAPCallback;

	private Action<bool, Dictionary<string, string>> _sdkLogoutCallback;

	private Action<bool, Dictionary<string, string>> _sdkSharePhoToCallback;

	public static SdkBrigdeSerivce Api => null;

	public void HandleNativeMessage(string content)
	{
	}

	public void Init()
	{
	}

	public void DoLogin(Action<bool, Dictionary<string, string>> onLoginCallBack = null)
	{
	}

	public void DoLogout(Action<bool, Dictionary<string, string>> onLogoutCallBack = null)
	{
	}

	public void DoPaymentIAP(string userName, string product_id, string server, string orderId, string orderInfo, string amount, string character, string extraInfo, Action<bool, Dictionary<string, string>> onPaymentIAPCallback = null)
	{
	}

	public void TrackingAppOpen(string serverId, string roleId, string roleName)
	{
	}

	public void TrackingEvent(int type, string level)
	{
	}

	public void TrackingEventCustom(string eventName, string jsonContent)
	{
	}

	public void FisebaseTrackingEvent(string eventName, string jsonContent)
	{
	}

	public void TrackingScreenOnFisebase(string screenName, string classOveride)
	{
	}

	public void UserPropertiesOnFirebase(string name, string value)
	{
	}

	public void DoShowDialog(string content)
	{
	}

	public void ShareLinkToFacebook(Uri link)
	{
	}

	public void ShareVideoToFacebook(Uri videoPath)
	{
	}

	private void CallSdkBridge(string method, params object[] args)
	{
	}
}
