using UnityEngine;
using UnityEngine.UI;

namespace Fake.Tap3
{
	public class TapBoostCountdown3 : MonoBehaviour
	{
		public GameObject m_Contents;

		public static TapBoostCountdown3 Instance;

		[HideInInspector]
		public bool m_IsBoostOn;

		public Image m_ImageFill;

		private TapHero3 _tapHeroBoost;

		private float _duration;

		private float _timeCount;

		private void Start()
		{
		}

		public void TurnBoostOn(TapHero3 heroBoost, float duration)
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
