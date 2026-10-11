public class MyActionDelegate
{
	public delegate void MyAction();

	public delegate void MyAction<T>(T arg);

	public delegate void MyAction<T1, T2>(T1 arg1, T2 arg2);

	public delegate void MyAction<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);
}
