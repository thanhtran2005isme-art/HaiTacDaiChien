using System;

public class UIHelperEnum
{
	[Flags]
	public enum HorizontalAlignment
	{
		UpperLeft = 0,
		MiddleLeft = 1,
		LowerLeft = 2
	}

	[Flags]
	public enum VerticalAlignment
	{
		UpperLeft = 0,
		UpperCenter = 1,
		UpperRight = 2
	}

	[Flags]
	public enum StartAxis
	{
		Horizontal = 0,
		Vertical = 1
	}

	[Flags]
	public enum GridAlignment
	{
		UpperLeft = 0,
		UpperCenter = 1,
		UpperRight = 2,
		MiddleLeft = UpperCenter | UpperRight,
		LowerLeft = 4
	}
}
