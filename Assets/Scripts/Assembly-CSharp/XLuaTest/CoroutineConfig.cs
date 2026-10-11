using System;
using System.Collections.Generic;
using XLua;

namespace XLuaTest
{
	public static class CoroutineConfig
	{
		[CSharpCallLua]
		public static List<Type> CSharpCallLua;

		[LuaCallCSharp(GenFlag.No)]
		public static List<Type> LuaCallCSharp => null;
	}
}
