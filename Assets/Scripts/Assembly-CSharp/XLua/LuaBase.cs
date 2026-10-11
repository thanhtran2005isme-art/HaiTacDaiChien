using System;

namespace XLua
{
	public abstract class LuaBase : IDisposable
	{
		protected bool disposed;

		protected readonly int luaReference;

		protected readonly LuaEnv luaEnv;

		public LuaBase(int reference, LuaEnv luaenv)
		{
		}

		~LuaBase()
		{
		}

		public void Dispose()
		{
		}

		public virtual void Dispose(bool disposeManagedResources)
		{
		}

		public override bool Equals(object o)
		{
			return false;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		internal virtual void push(IntPtr L)
		{
		}
	}
}
