using System.Collections.Generic;

public class HomeQuest
{
	public int m_QuestId;

	public int m_TemplateId;

	public string m_Desc;

	public string m_Title;

	public int m_Progess;

	public const int STATUS_PROGESS = 1;

	public const int STATUS_COMPLETE = 2;

	public int m_Status;

	public List<long> m_BonusData;

	public List<long> m_ListData;
}
