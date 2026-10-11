using System.Collections.Generic;

public class FAQModel
{
	public class QuestModel
	{
		public string question;

		public string anwser;
	}

	public string header;

	public List<QuestModel> listQuest;
}
