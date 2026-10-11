using System;
using UnityEngine;
using UnityEngine.UI;

public class CellTabNotifyNew : MonoBehaviour
{
	public Text _TitleTab;

	[HideInInspector]
	public int indexTab;

	private Action<DataNotifyNew> cbTabClick;

	private DataNotifyNew dataMessage;

	public void SetData(DataNotifyNew data)
	{
	}

	public void SetTabClickCb(Action<DataNotifyNew> cb)
	{
	}

	public void SetImageTabOn(bool checkOn)
	{
	}

	public void OnTabClick()
	{
	}
}
