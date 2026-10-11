using System;

namespace XLua
{
	public class LuaCallCSharpAttribute : Attribute
	{
		private GenFlag flag;

		public GenFlag Flag => GenFlag.No;

		public LuaCallCSharpAttribute(GenFlag flag = GenFlag.No)
		{
		}
	}
}
