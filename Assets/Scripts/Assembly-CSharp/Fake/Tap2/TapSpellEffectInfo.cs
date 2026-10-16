using UnityEngine;

namespace Fake.Tap2
{
	public class TapSpellEffectInfo
	{
		public const int LAYER_BELOW = 1;

		public const int LAYER_ABOVE = 2;

		public bool m_IsLoop;

		public string m_PathSpine;

		public string m_AniName;

		public int m_Layer;

		public float m_Delay;

		public Vector2 m_Offset;

		public float m_Scale;

		public TapSpellEffectInfo(string pathSpine, string aniName, bool isLoop)
		{
		}
	}
}
