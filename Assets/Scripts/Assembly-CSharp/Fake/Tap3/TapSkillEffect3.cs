using UnityEngine;

namespace Fake.Tap3
{
	public class TapSkillEffect3 : GoPoolingBehavior
	{
		public SpineObject m_SpineObject;

		[HideInInspector]
		public GoPooler m_Pooler;

		private TapEffectInfo m_EffectInfo;

		private bool _loadSpine;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		public static GameObject InstanceOne(Transform tfParent, GoPooler pooler)
		{
			return null;
		}

		public bool SetEffectInfo(TapEffectInfo effInfo)
		{
			return false;
		}

		public void SetPosition(Vector2 pos)
		{
		}

		public string GetKeyPool()
		{
			return null;
		}

		public void PlayEffectOnce()
		{
		}

		public void PlayEffectLoop()
		{
		}

		public void DoFly(Vector2 from, Vector2 to, float duration)
		{
		}

		private void Update()
		{
		}
	}
}
