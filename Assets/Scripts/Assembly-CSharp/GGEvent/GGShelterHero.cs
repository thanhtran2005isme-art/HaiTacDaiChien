using System;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGShelterHero : MonoBehaviour
	{
		public SkeletonGraphic m_Skeleton;

		public Action<GGHeroExchangeElement> m_ActionClickCb;

		public GameObject m_GoButtonAction;

		public GameObject m_GoButtonActionIa;

		public Text m_TextAction;

		public Text m_TextActionIa;

		private GGHeroExchangeElement _heroExchangeElm;

		private void Start()
		{
		}

		public void SetShelterHero(GGHeroExchangeElement heroExchange)
		{
		}

		public void ButtonActionClick()
		{
		}

		private void Update()
		{
		}
	}
}
