using System;
using System.Collections.Generic;
using XLua;

public static class ExampleGenConfig
{
	[LuaCallCSharp(GenFlag.No)]
	public static List<Type> LuaCallCSharp;

	[CSharpCallLua]
	public static List<Type> CSharpCallLua;

	[BlackList]
	public static List<List<string>> BlackList;
}
