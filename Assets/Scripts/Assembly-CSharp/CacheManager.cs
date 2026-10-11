using System;
using System.Collections.Generic;

public class CacheManager
{
	public class ItemCache
	{
		public int m_ItemKey;

		public object m_ItemData;

		public DateTime m_CacheMoment;

		public float m_DurationCacheInSec;
	}

	private static CacheManager _instance;

	public Dictionary<int, ItemCache> m_DictData;

	public static CacheManager Instance => null;

	private CacheManager()
	{
	}

	public void AddCacheItem(int key, object data, float cacheDuration)
	{
	}

	public object GetCacheData(int key)
	{
		return null;
	}

	public void ClearCache()
	{
	}
}
