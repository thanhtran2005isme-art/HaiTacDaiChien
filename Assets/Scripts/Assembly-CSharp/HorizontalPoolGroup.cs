using UnityEngine;

[AddComponentMenu("UIHelper/Horizontal Pool Group", 1)]
public class HorizontalPoolGroup : HorizontalOrVerticalPoolGroup
{
	[SerializeField]
	private UIHelperEnum.HorizontalAlignment m_ChildAlignment;

	[SerializeField]
	private bool contentCenter;

	protected override void calcSizeDelta()
	{
	}

	protected override void updateData()
	{
	}

	public override void scrollTo(int index, float duration = 0f, bool cancelIfFitInMaskWidth = false)
	{
	}

	protected override void checkCancelDragIfFits()
	{
	}
}
