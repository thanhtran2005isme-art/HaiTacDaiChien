using System;

namespace XLua
{
	public class GCOptimizeAttribute : Attribute
	{
		private OptimizeFlag flag;

		public OptimizeFlag Flag => OptimizeFlag.Default;

		public GCOptimizeAttribute(OptimizeFlag flag = OptimizeFlag.Default)
		{
		}
	}
}
