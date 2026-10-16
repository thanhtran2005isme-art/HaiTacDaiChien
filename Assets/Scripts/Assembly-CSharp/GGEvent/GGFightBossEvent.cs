using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGFightBossEvent : GGGameEventBehavior
	{
		private class GGBossPossibleReward
		{
			public BonusModel m_BonusModel;

			public string m_StrPercent;
		}

		private static readonly int SV_STARTER;

		private static readonly int SV_LOAD_EVENT_DETAIL;

		private static readonly int SV_HIT_BOSS;

		private static readonly int SV_COLLECT_REWARDS;

		public GameObject m_GoContent;

		public Image m_ImageBgr;

		public Image m_ImageFeatHit;

		public Text m_TextFeatValue;

		public Text m_TextTimeDesc;

		public Text m_TextPercentHp;

		public Slider m_SliderHp;

		public TextCountdown m_CountdownTime;

		public GameObject m_GoButtonHit;

		public GameObject m_GoButtonGetReward;

		public SpineObject m_SpineEffHit;

		private const int BOSS_DEATH = 1;

		private const int BOSS_ALIVE = 2;

		private int m_BossState;

		public GGBossEvent m_BossEvent;

		private string _urlHitBoss;

		private string _urlGetReward;

		private int _currentFeatValue;

		private int _costPerHit;

		private string _strHelp;

		private BonusModel _bonusFixedReward;

		private List<GGBossPossibleReward> _listPossibleReward;

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

		private void DelayReloadEvent(float delay)
		{
		}

		public void ButtonInfoClick()
		{
		}

		public void ButtonHelpClick()
		{
		}

		public void ButtonHitClick()
		{
		}

		public void ButtonCollectRewardClick()
		{
		}

		private void ParseEventDetail(string strJson)
		{
		}

		public override void ProcessKEvent(int serviceId, object data)
		{
		}
	}
}
