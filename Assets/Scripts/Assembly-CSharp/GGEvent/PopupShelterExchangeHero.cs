using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class PopupShelterExchangeHero : BasePopup
	{
		public RectTransform m_TfRewards;

		public RectTransform m_TfRequires;

		public Text m_TitleReward;

		public Text m_TitleReq;

		public Text m_ActionLabel;

		public Action<List<int>> m_SubmitClickCb;

		private List<HeroMaterialInput> _listHeroMaterialInput;

		private bool _hasEnoughItemMaterial;

		protected override void Start()
		{
		}

		public void ButtonCloseClick()
		{
		}

		public void SetData(GGHeroExchangeElement exchangeData)
		{
		}

		public void ButtonSubmitClick()
		{
		}

		public static PopupShelterExchangeHero ShowUp(GGHeroExchangeElement exchangeData)
		{
			return null;
		}

		private void Update()
		{
		}
	}
}
