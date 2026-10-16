using System.Collections.Generic;

public class ServerLocalization
{
	private static ServerLocalization _instance;

	public Dictionary<string, string> m_DictTerm;

	private int _recursiveDepth;

	public static ServerLocalization Instance => null;

	private string GetTermValue(string key)
	{
		return null;
	}

	public void ClearAll()
	{
	}

	public void SetLanguage(Language lang)
	{
	}

	private void ImportLanguageData(string strData)
	{
	}

	private string PurgeLanguageKey(string strKey)
	{
		return null;
	}

	public string BuildMessage(string strInput)
	{
		return null;
	}

	private string GetTabIndent()
	{
		return null;
	}

	private string RecursiveBuildMessage(string strInput)
	{
		return null;
	}
}
