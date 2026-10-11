using UnityEngine;

[AddComponentMenu("UIHelper/Vertical Pool Group", 2)]
public class VerticalPoolGroup : HorizontalOrVerticalPoolGroup
{
	[SerializeField]
	private UIHelperEnum.VerticalAlignment m_ChildAlignment;

	protected override void calcSizeDelta()
	{
	}

	protected override void updateData()
	{
	}

	public override void scrollTo(int index, float duration = 0f, bool cancelIfFitInMaskHeight = false)
	{
	}

	protected override void checkCancelDragIfFits()
	{
	}
}
