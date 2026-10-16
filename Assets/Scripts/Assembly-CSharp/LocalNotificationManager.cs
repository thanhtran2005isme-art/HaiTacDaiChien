using System.Collections.Generic;

public class LocalNotificationManager : MonoEventHandler
{
	private static LocalNotificationManager m_Instance;

	public List<DbSendNoti> lstDbSendNoti;

	public static LocalNotificationManager GetInstance()
	{
		return null;
	}

	protected override void Start()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void ProcessNotification(DbSendNoti data)
	{
	}

	private int CreateLocalNotification(string channel_id, string title, string desc, float time)
	{
		return 0;
	}

	private void OnApplicationPause(bool pauseStatus)
	{
	}
}
