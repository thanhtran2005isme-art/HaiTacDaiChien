public class PanelDiscountItemEvent : MonoEventHandler
{
	public BoxGameEventInfo m_BoxEventInfo;

	public VerticalPoolGroup m_PoolGroup;

	private bool m_InitPool;

	private GameDiscountItemEvent m_GameEvent;

	public void LoadEventData(GameDiscountItemEvent gameEvent, bool isFresh = false)
	{
	}

	public void CellActionClick(object data)
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
