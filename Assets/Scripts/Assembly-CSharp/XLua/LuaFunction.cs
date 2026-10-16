using System;

namespace XLua
{
	public class LuaFunction : LuaBase
	{
		public LuaFunction(int reference, LuaEnv luaenv)
			: base(0, null)
		{
		}

		public void Action<T>(T a)
		{
		}

		public TResult Func<T, TResult>(T a)
		{
			return default;
		}

		public void Action<T1, T2>(T1 a1, T2 a2)
		{
		}

		public TResult Func<T1, T2, TResult>(T1 a1, T2 a2)
		{
			return default;
		}

		public object[] Call(object[] args, Type[] returnTypes)
		{
			return null;
		}

		public object[] Call(params object[] args)
		{
			return null;
		}

		public T Cast<T>()
		{
			return default;
		}

		public void SetEnv(LuaTable env)
		{
		}

		internal override void push(IntPtr L)
		{
		}

		public override string ToString()
		{
			return null;
		}
	}
}
