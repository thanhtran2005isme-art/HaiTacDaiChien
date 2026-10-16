using UnityEngine;
using UnityEngine.UI;

public class PanelMonthCardEvent : MonoBehaviour
{
	public Text m_TextLabel;

	public Text m_Price;

	public Text m_TextDesc;

	public Image m_Background;

	public Button m_Action;

	private string productId;

	public void LoadEventData(GameMonthCardEvent gameEvent, bool isPopup = false)
	{
	}

	public void ActionClick()
	{
	}

	private void Start()
	{
	}
}
