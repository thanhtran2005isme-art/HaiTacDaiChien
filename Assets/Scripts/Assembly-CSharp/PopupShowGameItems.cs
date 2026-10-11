using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupShowGameItems : BasePopup
{
	public Text m_TextTitle;

	public Transform m_TransformContainer;

	public GameObject m_GoScrollView;

	protected override void Start()
	{
	}

	public void LoadItem(string title, List<object> lstObjectItemModel, bool forceContentCenter)
	{
	}

	public static PopupShowGameItems ShowUp(string title, List<object> lstObjectItemModel, bool forceContentCenter = false)
	{
		return null;
	}

	private void Update()
	{
	}
}
