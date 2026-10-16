using UnityEngine;
using UnityEngine.UI;

namespace Fake.Tap2
{
	public class TapBoostCountdown : MonoBehaviour
	{
		public GameObject m_Contents;

		public static TapBoostCountdown Instance;

		[HideInInspector]
		public bool m_IsBoostOn;

		public Image m_ImageFill;

		private TapHero _tapHeroBoost;

		private float _duration;

		private float _timeCount;

		private void Start()
		{
		}

		public void TurnBoostOn(TapHero heroBoost, float duration)
		{
		}

		public void TurnBoostOff()
		{
		}

		private void Update()
		{
		}
	}
}
