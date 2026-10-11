using System;

public class Timer
{
	private static readonly DateTime Jan1st1970Utc;

	public static long CurrentTimeMillis => 0L;

	public static long CurrentTimeSeconds => 0L;

	public static long calculatingTimePass(long iDateTicks)
	{
		return 0L;
	}

	public static long calculatingTimePassTick(long iDateTicks)
	{
		return 0L;
	}

	public static string calculatingTimeDisplay(long remainSeconds, string format, string h = "", string m = "", string s = "")
	{
		return null;
	}

	public static string calculatingTimeMiliSecond(long remainMiliSeconds, string format, string h = "", string m = "", string s = "")
	{
		return null;
	}

	public static string getTimeDisplay(long remainSeconds)
	{
		return null;
	}

	public static string getTimeDisplay2(double timeCreatedInMiliSeconds, string format = "dd/MM/yyyy hh:mm tt")
	{
		return null;
	}

	public static string getTimeDisplay4(double timeCreatedInMiliSeconds, string format = "dd/MM/yyyy hh:mm tt")
	{
		return null;
	}

	public static string getTimeDisplay3(double timeCreatedInSeconds, string format = "dd/MM/yyyy hh:mm tt")
	{
		return null;
	}

	public static bool IsToday(long timeCreatedInSeconds)
	{
		return false;
	}
}
