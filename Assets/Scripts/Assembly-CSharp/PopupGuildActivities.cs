using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupGuildActivities : BasePopup
{
	public GameObject m_GoScrollView;

	public Text m_TextTitle;

	public GameObject m_GoCellActivity;

	public RectTransform m_TransformContent;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private void TestLog()
	{
	}

	public void LoadActivities(List<GuildActivity> lstGuildAct)
	{
	}

	public static PopupGuildActivities ShowUp(List<GuildActivity> lstGuildAct)
	{
		return null;
	}

	private void Update()
	{
	}
}
