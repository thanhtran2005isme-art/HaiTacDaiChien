using UnityEngine;

public class MonoEventHandler : MonoBehaviour, INetEventHandler
{
	protected DataSender m_DataSender;

	private bool m_HasAddToListEventListener;

	protected virtual void Awake()
	{
	}

	protected virtual void Start()
	{
	}

	public virtual void OnDestroy()
	{
	}

	public virtual void ProcessKEvent(int eventId, object data)
	{
	}
}
