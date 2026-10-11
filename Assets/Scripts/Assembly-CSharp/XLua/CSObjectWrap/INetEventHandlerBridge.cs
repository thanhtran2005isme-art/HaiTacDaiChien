namespace XLua.CSObjectWrap
{
	public class INetEventHandlerBridge : LuaBase, INetEventHandler
	{
		public static LuaBase __Create(int reference, LuaEnv luaenv)
		{
			return null;
		}

		public INetEventHandlerBridge(int reference, LuaEnv luaenv)
			: base(0, null)
		{
		}

		void INetEventHandler.ProcessKEvent(int eventId, object data)
		{
		}
	}
}
