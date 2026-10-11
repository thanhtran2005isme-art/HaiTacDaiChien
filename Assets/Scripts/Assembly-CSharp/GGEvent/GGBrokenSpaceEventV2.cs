using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGBrokenSpaceEventV2 : GGGameEventBehavior
	{
		private class GGBrokenSpaceEventData
		{
			public ItemConsumable m_ItemConsumeFeat;

			public ItemConsumable m_ItemConsumeCost;

			public int m_NumberFeatPurchased;

			public int m_NumberFeatMaxToPurchased;

			public string m_StrDescFeatPurchaseStt;

			public string m_StrHelp;

			public int m_RequireLevel;

			public string m_UrlPurchaseFeat;

			public string m_PathBgr;

			public int m_CurrentStageLevel;

			public List<GGBrokenSpaceStageData> m_ListStageData;
		}

		public class GGBrokenSpaceStageData
		{
			public const int STATE_LOCKED = 1;

			public const int STATE_PASSED = 2;

			public const int STATE_ON_ATTACK = 3;

			public string m_StageName;

			public int m_StageId;

			public List<long> m_ListDataReward;

			public int m_PercentHpLeft;

			public List<long> m_ListDataMonster;

			public int m_State;
		}

		private static readonly int SV_STARTER;

		private static readonly int SV_LOAD_EVENT_DETAIL;

		private static readonly int SV_PURCHASE_STAMINA;

		public GameObject m_GoContent;

		public Image m_ImageBgr;

		public Image m_ImageFeat;

		public Text m_TextFeatValue;

		public Text m_TextTimeDesc;

		public Text m_TextFeatPurchaseDesc;

		public TextCountdown m_CountdownTime;

		public List<Image> m_ListImageLine;

		public List<BrokenSpaceStage> m_ListStage;

		public GameObject m_GoButtonAction;

		private GGBrokenSpaceEventData _eventData;

		private bool _isLastAttackSmash;

		protected override void Start()
		{
		}

		public override void SetEventInfo(GGEventHeadlineInfo eventHeadlineInfo)
		{
		}

		private void InitContent()
		{
		}

		private void ReloadEvent()
		{
		}

		public void ButtonAddFeatClick()
		{
		}

		private void RequestBuyAttackStamina(int number)
		{
		}

		public void ButtonHelpClick()
		{
		}

		public void ButtonActionClick()
		{
		}

		private void ShowEvent(GGBrokenSpaceEventData eventData)
		{
		}

		private void OnStageClick(GGBrokenSpaceStageData stageData)
		{
		}

		private GGBrokenSpaceEventData ParseEventDetail(string strJson)
		{
			return null;
		}

		public override void ProcessKEvent(int serviceId, object data)
		{
		}
	}
}
