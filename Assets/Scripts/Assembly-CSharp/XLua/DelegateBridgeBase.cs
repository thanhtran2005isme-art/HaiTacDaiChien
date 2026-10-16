using System;
using System.Collections.Generic;

namespace XLua
{
	public abstract class DelegateBridgeBase : LuaBase
	{
		private Type firstKey;

		private Delegate firstValue;

		private Dictionary<Type, Delegate> bindTo;

		protected int errorFuncRef;

		public DelegateBridgeBase(int reference, LuaEnv luaenv)
			: base(0, null)
		{
		}

		public bool TryGetDelegate(Type key, out Delegate value)
		{
			value = null;
			return false;
		}

		public void AddDelegate(Type key, Delegate value)
		{
		}

		public virtual Delegate GetDelegateByType(Type type)
		{
			return null;
		}
	}
}
