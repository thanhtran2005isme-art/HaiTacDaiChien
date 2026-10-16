using System;
using System.Collections.Generic;

public class SDKServiceAndroid : ISDKService
{
	public Action<bool, Dictionary<string, string>> m_OnPaymentIapCallback;

	public static string clientId;

	public static string secretKey;

	public static string SDKClass;

	private const string loginStr = "login";

	private const string initSdkCallbackStr = "initlistener";

	private const string logoutStr = "logout";

	private const string paymentIapStr = "paymentiap";

	private const string statusKey = "status";

	private const string messageKey = "message";

	public override void InitSDK()
	{
	}

	public override void Login()
	{
	}

	public override void Logout()
	{
	}

	public override void DoPaymentIAP(string usernameGosu, string productId, string server, string orderId, string orderInfo, string amount, string character, string extraInfo, Action<bool, Dictionary<string, string>> onPaymentIAPCallback = null)
	{
	}

	public override void DoPaymentIAP2(string productId, string productName, string currencyUnit, string amount, string serverID, string characterID, string extraInfo, Action<bool, Dictionary<string, string>> onPaymentIAPCallback = null)
	{
	}

	public override void ProcessNativeSDKData(string strData)
	{
	}

	private void InitSdkCallbackHandle(Dictionary<string, string> stringDic)
	{
	}

	private void LoginCallbackHandle(Dictionary<string, string> stringDic)
	{
	}

	private void LogoutCallbackHandle(Dictionary<string, string> stringDic)
	{
	}

	private void PaymentIAPCallbackHandle(Dictionary<string, string> stringDic)
	{
	}

	private void CallSdkBridge(string method, params object[] args)
	{
	}

	private Dictionary<string, string> JsonStringToDictionary(string jsonString)
	{
		return null;
	}
}
