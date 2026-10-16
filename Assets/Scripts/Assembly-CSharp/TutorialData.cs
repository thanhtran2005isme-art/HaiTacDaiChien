using System.Collections.Generic;

public class TutorialData
{
	public List<string> m_LstText;

	public string m_SceneName;

	public int m_TutorialId;

	public TutorialData(int tutId, string sceneName)
	{
	}

	public bool IsEmptyText()
	{
		return false;
	}

	public void SetListText(List<string> lstText)
	{
	}

	public void SetListText(string[] arrText)
	{
	}
}
