using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupEquipActionReupOrUpgrade : BasePopup
{
	public Image icon;

	public CurvedItem star;

	public Text content;

	public Action cbUpgrade;

	public Action cbReup;

	public void OnButtonUpgradeClick()
	{
	}

	public void OnButtonReupClick()
	{
	}

	private void SetData(string content, Action cbUpgrade, Action cbReup, int star, Sprite sprite)
	{
	}

	public static GameObject ShowUp(string content, Action cbUpgrade, Action cbReup, int star, Sprite sprite)
	{
		return null;
	}
}
