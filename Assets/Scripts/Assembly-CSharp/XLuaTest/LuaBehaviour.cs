using System;
using UnityEngine;
using XLua;

namespace XLuaTest
{
	[LuaCallCSharp(GenFlag.No)]
	public class LuaBehaviour : MonoBehaviour
	{
		public TextAsset luaScript;

		public Injection[] injections;

		internal static LuaEnv luaEnv;

		internal static float lastGCTime;

		internal const float GCInterval = 1f;

		private Action luaStart;

		private Action luaUpdate;

		private Action luaOnDestroy;

		protected LuaTable scriptEnv;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void OnDestroy()
		{
		}
	}
}
