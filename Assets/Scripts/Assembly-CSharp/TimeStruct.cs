public struct TimeStruct
{
	private long time;

	private long timeNow;

	public long Time => 0L;

	public long TimeNow => 0L;

	public static implicit operator TimeStruct(long value)
	{
		return default;
	}

	public static TimeStruct operator -(TimeStruct source, long timePass)
	{
		return default;
	}

	public static TimeStruct operator +(TimeStruct source, long timePass)
	{
		return default;
	}

	public static bool operator >(TimeStruct source, long value)
	{
		return false;
	}

	public static bool operator <(TimeStruct source, long value)
	{
		return false;
	}

	public static bool operator ==(TimeStruct source, long value)
	{
		return false;
	}

	public static bool operator !=(TimeStruct source, long value)
	{
		return false;
	}

	public static bool operator >=(TimeStruct source, long value)
	{
		return false;
	}

	public static bool operator <=(TimeStruct source, long value)
	{
		return false;
	}
}
