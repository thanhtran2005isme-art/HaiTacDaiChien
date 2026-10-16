using UnityEngine;
using UnityEngine.UI;

public class PanelFeedback : MonoEventHandler
{
	public InputField m_InputText;

	[HideInInspector]
	public int m_MinLength;

	public void ButtonActionClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
