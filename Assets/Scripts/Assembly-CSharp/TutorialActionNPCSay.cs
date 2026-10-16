using System.Collections.Generic;

public class TutorialActionNPCSay : TutorialAction
{
	public const int LUFFY = 1;

	public const int NAMI = 3;

	public const int USOP = 2;

	public List<string> m_LstText;

	public int m_HelperId;

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

	public TutorialActionNPCSay(int mHelperId, string[] arrText)
		: base(0)
	{
	}
}
