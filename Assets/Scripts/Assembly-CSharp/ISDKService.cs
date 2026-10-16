using System;
using System.Collections.Generic;

public abstract class ISDKService
{
	public Action<string> LoginFailCb;

	public Action<string, string, string> LoginSuccessCb;

	public Action<string> PaymentFailedCb;

	public Action<string> PaymentSuccessCb;

	public Action<string> LogoutSuccessCb;

	public Action<string> LogoutFailCb;

	public Action<bool> InitSdkCallback;

	public abstract void Login();

	public abstract void InitSDK();

	public abstract void Logout();

	public abstract void DoPaymentIAP(string usernameGosu, string productId, string server, string orderId, string orderInfo, string amount, string character, string extraInfo, Action<bool, Dictionary<string, string>> onPaymentIAPCallback);

	public abstract void DoPaymentIAP2(string productId, string productName, string currencyUnit, string amount, string serverID, string characterID, string extraInfo, Action<bool, Dictionary<string, string>> onPaymentIAPCallback = null);

	public abstract void ProcessNativeSDKData(string strData);
}
