using System;

namespace XLua
{
	public enum GenFlag
	{
		No = 0,
		[Obsolete("use GCOptimizeAttribute instead")]
		GCOptimize = 1
	}
}
