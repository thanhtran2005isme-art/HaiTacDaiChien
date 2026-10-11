using UnityEngine;

public static class Debug
{
	public static bool isDebugBuild;

	public static bool isDebugG;

	public static void Log(object message)
	{
	}

	public static void Log(object message, Object context)
	{
	}

	public static void DrawLine(Vector3 from, Vector3 to, Color color)
	{
	}

	public static void LogError(object message)
	{
	}

	public static void LogError(object message, Object context)
	{
	}

	public static void LogWarning(object message)
	{
	}

	public static void LogWarning(object message, Object context)
	{
	}

	public static void LogFormat(string message, params object[] arg)
	{
	}

	public static void LogWarningFormat(string message, params object[] arg)
	{
	}

	public static void LogErrorFormat(string message, params object[] arg)
	{
	}

	public static void WriteLine(object message)
	{
	}

	public static void Assert(bool b, object message)
	{
	}

	public static void LogG(string msg)
	{
	}

	internal static void Assert(bool v)
	{
	}
}
