using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UIHelper/Grid Pool Group", 3)]
public class GridPoolGroup : BasePoolGroup
{
	[SerializeField]
	private UIHelperEnum.GridAlignment m_ChildAlignment;

	[SerializeField]
	private UIHelperEnum.StartAxis m_StartAxis;

	[SerializeField]
	private int m_ConstraintCount;

	public override void setAdapter(List<object> adapter, bool toFirst = true)
	{
	}

	protected override void calcSizeDelta()
	{
	}

	protected override void updateData()
	{
	}

	public void scrollTo(int index, float duration = 0f, bool cancelIfFitInMaskWidth = false, bool cancelIfFitInMaskHeight = false)
	{
	}

	public override void scrollToLast(float duration = 0f)
	{
	}

	protected override void checkCancelDragIfFits()
	{
	}
}
