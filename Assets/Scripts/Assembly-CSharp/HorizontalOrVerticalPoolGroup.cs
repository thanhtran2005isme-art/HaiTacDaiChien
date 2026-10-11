using System.Collections.Generic;
using UnityEngine;

public abstract class HorizontalOrVerticalPoolGroup : BasePoolGroup
{
	public void howToUseCellSize(CellSizeDelegate func)
	{
	}

	public virtual void scrollTo(int index, float duration = 0f, bool cancelIfFitInMask = false)
	{
	}

	protected Vector2 getElementSize(int index)
	{
		return default;
	}

	protected void checkToAddAtleast3Cells()
	{
	}

	public override void setAdapter(List<object> adapter, bool toFirst = true)
	{
	}

	public override void scrollToLast(float duration = 0f)
	{
	}
}
