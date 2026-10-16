public class MailContent
{
	public int m_MailId;

	public string m_Title;

	public string m_Content;

	public bool m_DoneProcessed;

	public string m_TimeSent;

	public BonusModel m_BonusModel;

	public long m_SenderId;

	public bool m_Active;

	public MailContent(int mailId, string title, string content, string timeSent, bool doneProcess = false, BonusModel bonus = null)
	{
	}

	public bool HasBonusAttachment()
	{
		return false;
	}

	public bool HasUncollectedBonus()
	{
		return false;
	}
}
