using System.Collections.Generic;

namespace XLua
{
	internal class ReferenceEqualsComparer : IEqualityComparer<object>
	{
		public new bool Equals(object o1, object o2)
		{
			return false;
		}

		public int GetHashCode(object obj)
		{
			return 0;
		}
	}
}
