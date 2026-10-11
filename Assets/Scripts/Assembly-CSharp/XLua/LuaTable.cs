using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace XLua
{
	public class LuaTable : LuaBase
	{
		[CompilerGenerated]
		private sealed class _003CGetKeys_003Ed__15 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			public LuaTable _003C_003E4__this;

			private IntPtr _003CL_003E5__2;

			private ObjectTranslator _003Ctranslator_003E5__3;

			private int _003ColdTop_003E5__4;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CGetKeys_003Ed__15(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<object> IEnumerable<object>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[CompilerGenerated]
		private sealed class _003CGetKeys_003Ed__16<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private T _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			public LuaTable _003C_003E4__this;

			private IntPtr _003CL_003E5__2;

			private ObjectTranslator _003Ctranslator_003E5__3;

			private int _003ColdTop_003E5__4;

			T IEnumerator<T>.Current
			{
				[DebuggerHidden]
				get
				{
					return default;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CGetKeys_003Ed__16(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[Obsolete("use no boxing version: GetInPath/SetInPath Get/Set instead!")]
		public object this[string field]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[Obsolete("use no boxing version: GetInPath/SetInPath Get/Set instead!")]
		public object this[object field]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public int Length => 0;

		public LuaTable(int reference, LuaEnv luaenv)
			: base(0, null)
		{
		}

		public void Get<TKey, TValue>(TKey key, out TValue value)
		{
			value = default;
		}

		public bool ContainsKey<TKey>(TKey key)
		{
			return false;
		}

		public void Set<TKey, TValue>(TKey key, TValue value)
		{
		}

		public T GetInPath<T>(string path)
		{
			return default;
		}

		public void SetInPath<T>(string path, T val)
		{
		}

		public void ForEach<TKey, TValue>(Action<TKey, TValue> action)
		{
		}

		[IteratorStateMachine(typeof(_003CGetKeys_003Ed__15))]
		public IEnumerable GetKeys()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CGetKeys_003Ed__16<>))]
		public IEnumerable<T> GetKeys<T>()
		{
			return null;
		}

		[Obsolete("use no boxing version: Get<TKey, TValue> !")]
		public T Get<T>(object key)
		{
			return default;
		}

		public TValue Get<TKey, TValue>(TKey key)
		{
			return default;
		}

		public TValue Get<TValue>(string key)
		{
			return default;
		}

		public void SetMetaTable(LuaTable metaTable)
		{
		}

		public T Cast<T>()
		{
			return default;
		}

		internal override void push(IntPtr L)
		{
		}

		public override string ToString()
		{
			return null;
		}
	}
}
