using UnityEngine;
using UnityEngine.UI;

public class BoxGuildOrder : MonoEventHandler
{
	public GameObject m_GoButtonGetOrder;

	public GameObject m_GoRefresh;

	public TextCountdown m_TextCountdown;

	public GameObject m_CellOrderPrefab;

	public GameObject m_GoScrollOrder;

	public VerticalPoolGroup m_PoolOrder;

	public Text m_TextNote;

	private bool m_IsFirstLoadOrder;

	protected override void Start()
	{
	}

	public void LoadTestOrderMill()
	{
	}

	public void ButtonGetOrderClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
