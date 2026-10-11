using System;

public class DataCache
{
	public const int EXPIRE_1D = 86400;

	public const int EXPIRE_WEEK = 604800;

	public const int EXPIRE_3D = 259200;

	public const int EXPIRE_1H = 3600;

	public const int EXPIRE_2H = 7200;

	public const int EXPIRE_12H = 43200;

	public const int EXPIRE_30M = 1800;

	public const int EXPIRE_15M = 900;

	public const int EXPIRE_10M = 600;

	public const int EXPIRE_5M = 300;

	public const int EXPIRE_1M = 60;

	public DateTime lastTime;

	public int expTime;

	public byte[] data;

	public virtual bool expiredTimeCache()
	{
		return false;
	}
}
