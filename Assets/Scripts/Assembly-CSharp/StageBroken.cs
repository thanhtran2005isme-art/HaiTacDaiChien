using System;
using UnityEngine;
using UnityEngine.UI;

public class StageBroken : MonoBehaviour
{
	public Image imgIcon;

	public Image imgPath;

	public GameObject spine;

	public Action<StageBrokenSpace> clickCb;

	private StageBrokenSpace m_Data;

	public void InitData(StageBrokenSpace data)
	{
	}

	public void AtkClick()
	{
	}
}
