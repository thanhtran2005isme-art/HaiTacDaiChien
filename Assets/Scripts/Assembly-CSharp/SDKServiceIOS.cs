using System;
using System.Collections.Generic;

public class SDKServiceIOS : ISDKService
{
	private string clientID;

	private string secrectKey;

	private static bool _initSDKSuccess;

	private Action<bool, Dictionary<string, string>> _paymentIAPResultCb;

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
}
