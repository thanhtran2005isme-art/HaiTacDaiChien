using System.Collections;

namespace XLua.CSObjectWrap
{
	public class SystemCollectionsIEnumeratorBridge : LuaBase, IEnumerator
	{
		object IEnumerator.Current => null;

		public static LuaBase __Create(int reference, LuaEnv luaenv)
		{
			return null;
		}

		public SystemCollectionsIEnumeratorBridge(int reference, LuaEnv luaenv)
			: base(0, null)
		{
		}

		bool IEnumerator.MoveNext()
		{
			return false;
		}

		void IEnumerator.Reset()
		{
		}
	}
}
