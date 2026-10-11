using System;

namespace XLua
{
	public class DelegateBridge : DelegateBridgeBase
	{
		internal static DelegateBridge[] DelegateBridgeList;

		public static bool Gen_Flag;

		public void __Gen_Delegate_Imp0()
		{
		}

		public double __Gen_Delegate_Imp1(double p0, double p1)
		{
			return 0.0;
		}

		public void __Gen_Delegate_Imp2(string p0)
		{
		}

		public void __Gen_Delegate_Imp3(double p0)
		{
		}

		static DelegateBridge()
		{
		}

		public override Delegate GetDelegateByType(Type type)
		{
			return null;
		}

		public DelegateBridge(int reference, LuaEnv luaenv)
			: base(0, null)
		{
		}

		public void PCall(IntPtr L, int nArgs, int nResults, int errFunc)
		{
		}

		public void Action()
		{
		}

		public void Action<T1>(T1 p1)
		{
		}

		public void Action<T1, T2>(T1 p1, T2 p2)
		{
		}

		public void Action<T1, T2, T3>(T1 p1, T2 p2, T3 p3)
		{
		}

		public void Action<T1, T2, T3, T4>(T1 p1, T2 p2, T3 p3, T4 p4)
		{
		}

		public TResult Func<TResult>()
		{
			return default;
		}

		public TResult Func<T1, TResult>(T1 p1)
		{
			return default;
		}

		public TResult Func<T1, T2, TResult>(T1 p1, T2 p2)
		{
			return default;
		}

		public TResult Func<T1, T2, T3, TResult>(T1 p1, T2 p2, T3 p3)
		{
			return default;
		}

		public TResult Func<T1, T2, T3, T4, TResult>(T1 p1, T2 p2, T3 p3, T4 p4)
		{
			return default;
		}
	}
}
