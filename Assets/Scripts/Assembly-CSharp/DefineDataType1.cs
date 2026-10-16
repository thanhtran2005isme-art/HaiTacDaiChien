using System.Collections.Generic;
using SimpleJSON;

public class DefineDataType1
{
	public string m_Image;

	public string m_Tiltle;

	public string m_Desc;

	public JSONArray m_ItemID;

	public string m_ImgType;

	public JSONArray cost;

	public int MaxItem;

	public string ExchangeUrl;

	public List<DefineDataType2> m_ListItem;

	public DefineDataType1()
	{
	}

	public DefineDataType1(string m_Image, string m_Tiltle, string m_Desc, JSONArray m_ItemID, string m_ImgType, JSONArray cost, int MaxItem, string ExchangeUrl, List<DefineDataType2> m_ListItem)
	{
	}
}
