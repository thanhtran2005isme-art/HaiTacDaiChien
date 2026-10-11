using System;
using System.Collections.Generic;

namespace XLua
{
	public class ObjectPool
	{
		private struct Slot
		{
			public int next;

			public object obj;

			public Slot(int next, object obj)
			{
				this.next = 0;
				this.obj = null;
			}
		}

		private const int LIST_END = -1;

		private const int ALLOCED = -2;

		private Slot[] list;

		private int freelist;

		private int count;

		public object this[int i] => null;

		public void Clear()
		{
		}

		private void extend_capacity()
		{
		}

		public int Add(object obj)
		{
			return 0;
		}

		public bool TryGetValue(int index, out object obj)
		{
			obj = null;
			return false;
		}

		public object Get(int index)
		{
			return null;
		}

		public object Remove(int index)
		{
			return null;
		}

		public object Replace(int index, object o)
		{
			return null;
		}

		public int Check(int check_pos, int max_check, Func<object, bool> checker, Dictionary<object, int> reverse_map)
		{
			return 0;
		}
	}
}
