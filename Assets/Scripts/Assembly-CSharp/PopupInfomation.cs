using System;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class PopupInfomation : BasePopup
{
	public Text title;

	public Text desc1;

	public Text desc2;

	public Image bgr;

	public SkeletonGraphic skeleton;

	public Action cbClick;

	public static PopupInfomation ShowUp(InformationBraveTrail data, Transform parent)
	{
		return null;
	}

	public void SetData(InformationBraveTrail data)
	{
	}

	public override void ClosePopup()
	{
	}
}
