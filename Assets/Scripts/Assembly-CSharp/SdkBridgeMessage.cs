using System.Collections.Generic;

internal class SdkBridgeMessage
{
	private const int STATUS_SUSCCESS = 1;

	public string ActionName { get; private set; }

	public int ActionState { get; private set; }

	public bool IsSuccess => false;

	public bool IsCompleted => false;

	public bool IsClosed => false;

	public bool IsFailed => false;

	public Dictionary<string, string> Data { get; private set; }

	public SdkBridgeMessage(string nativeMessage)
	{
	}

	private void Parse(string nativeMessage)
	{
	}

	private void ParseIOS(string nativeMessage)
	{
	}
}
