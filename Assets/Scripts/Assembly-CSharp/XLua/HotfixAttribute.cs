using System;

namespace XLua
{
	public class HotfixAttribute : Attribute
	{
		private HotfixFlag flag;

		public HotfixFlag Flag => HotfixFlag.Stateless;

		public HotfixAttribute(HotfixFlag e = HotfixFlag.Stateless)
		{
		}
	}
}
