using System;
using System.Collections.Generic;
using XLua.LuaDLL;

namespace XLua
{
	public class LuaEnv : IDisposable
	{
		internal struct GCAction
		{
			public int Reference;

			public bool IsDelegate;
		}

		public delegate byte[] CustomLoader(ref string filepath);

		public const string CSHARP_NAMESPACE = "xlua_csharp_namespace";

		public const string MAIN_SHREAD = "xlua_main_thread";

		internal IntPtr rawL;

		private LuaTable _G;

		internal ObjectTranslator translator;

		internal int errorFuncRef;

		private const int LIB_VERSION_EXPECT = 105;

		private static List<Action<LuaEnv, ObjectTranslator>> initers;

		private int last_check_point;

		private int max_check_per_tick;

		private Func<object, bool> object_valid_checker;

		private bool disposed;

		private Queue<GCAction> refQueue;

		private string init_xlua;

		internal List<CustomLoader> customLoaders;

		internal Dictionary<string, lua_CSFunction> buildin_initer;

		internal IntPtr L => (IntPtr)0;

		public LuaTable Global => null;

		public int GcPause
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int GcStepmul
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int Memroy => 0;

		public static void AddIniter(Action<LuaEnv, ObjectTranslator> initer)
		{
		}

		public T LoadString<T>(byte[] chunk, string chunkName = "chunk", LuaTable env = null)
		{
			return default;
		}

		public T LoadString<T>(string chunk, string chunkName = "chunk", LuaTable env = null)
		{
			return default;
		}

		public LuaFunction LoadString(string chunk, string chunkName = "chunk", LuaTable env = null)
		{
			return null;
		}

		public object[] DoString(byte[] chunk, string chunkName = "chunk", LuaTable env = null)
		{
			return null;
		}

		public object[] DoString(string chunk, string chunkName = "chunk", LuaTable env = null)
		{
			return null;
		}

		private void AddSearcher(lua_CSFunction searcher, int index)
		{
		}

		public void Alias(Type type, string alias)
		{
		}

		private static bool ObjectValidCheck(object obj)
		{
			return false;
		}

		public void Tick()
		{
		}

		public void GC()
		{
		}

		public LuaTable NewTable()
		{
			return null;
		}

		public void Dispose()
		{
		}

		public virtual void Dispose(bool dispose)
		{
		}

		public void ThrowExceptionFromError(int oldTop)
		{
		}

		internal void equeueGCAction(GCAction action)
		{
		}

		public void AddLoader(CustomLoader loader)
		{
		}

		public void AddBuildin(string name, lua_CSFunction initer)
		{
		}

		public void FullGc()
		{
		}

		public void StopGc()
		{
		}

		public void RestartGc()
		{
		}

		public bool GcStep(int data)
		{
			return false;
		}
	}
}
