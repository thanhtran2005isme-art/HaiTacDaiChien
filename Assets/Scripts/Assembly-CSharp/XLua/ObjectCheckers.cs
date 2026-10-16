using System;
using System.Collections.Generic;

namespace XLua
{
	public class ObjectCheckers
	{
		private Dictionary<Type, ObjectCheck> checkersMap;

		private ObjectTranslator translator;

		public ObjectCheckers(ObjectTranslator translator)
		{
		}

		private static bool objectCheck(IntPtr L, int idx)
		{
			return false;
		}

		private bool luaTableCheck(IntPtr L, int idx)
		{
			return false;
		}

		private bool numberCheck(IntPtr L, int idx)
		{
			return false;
		}

		private bool decimalCheck(IntPtr L, int idx)
		{
			return false;
		}

		private bool strCheck(IntPtr L, int idx)
		{
			return false;
		}

		private bool bytesCheck(IntPtr L, int idx)
		{
			return false;
		}

		private bool boolCheck(IntPtr L, int idx)
		{
			return false;
		}

		private bool int64Check(IntPtr L, int idx)
		{
			return false;
		}

		private bool uint64Check(IntPtr L, int idx)
		{
			return false;
		}

		private bool luaFunctionCheck(IntPtr L, int idx)
		{
			return false;
		}

		private bool intptrCheck(IntPtr L, int idx)
		{
			return false;
		}

		private ObjectCheck genChecker(Type type)
		{
			return null;
		}

		public ObjectCheck genNullableChecker(ObjectCheck oc)
		{
			return null;
		}

		public ObjectCheck GetChecker(Type type)
		{
			return null;
		}
	}
}
