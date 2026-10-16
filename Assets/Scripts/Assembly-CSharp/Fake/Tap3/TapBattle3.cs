using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fake.Tap3
{
	public class TapBattle3 : MonoBehaviour
	{
		public GameObject m_PointEnemy;

		public GameObject m_PointPlayer;

		public GameObject m_PointSpell;

		public TapPanelPause3 m_PanelPause;

		public TapPanelCurtain3 m_PanelCurtain;

		public TapButtonSpell3 m_ButtonSpellFreeze;

		public TapButtonSpell3 m_ButtonSpellBurn;

		public TapPanelLoot3 m_PanelLoot;

		public List<GameObject> m_ListPointSidekick;

		public TapSkillExecutorMng m_TapSkillExecutorMng;

		public GameObject m_GoHandTut;

		public GameObject m_GoPanelTut;

		public Transform m_TfGroupHeroes;

		public Image m_ImageBgr;

		public Text m_TextStage;

		private const int STATE_PAUSED = 1;

		private const int STATE_ON_BATTLE = 2;

		private const int STATE_LOAD_NEXT_STAGE = 3;

		private int _battleState;

		private bool _onTutorial;

		public static TapBattle3 Instance;

		private const bool CHEAT_DMG = false;

		private TapHero3 _tapHeroPlayer;

		private TapHero3 _tapHeroEnemy;

		private List<TapHero3> _lstTapHeroSidekick;

		private void Start()
		{
		}

		public void InitBattle()
		{
		}

		public string BoostPlayer()
		{
			return null;
		}

		public void LoadRighteousHeroes()
		{
		}

		private TapHero3 LoadSideKick(int skId, float cooldown, TapHeroInfo3 skInfo)
		{
			return null;
		}

		public void CalculateHeroPower(bool firstInit)
		{
		}

		public void OnHeroDeath(TapHero3 tapHero)
		{
		}

		public void StartBattle()
		{
		}

		private void OnTutFinised()
		{
		}

		private void OnTutChanged(int tutId)
		{
		}

		private void ShowHandTutorial(int tutId)
		{
		}

		private void PauseAllAI()
		{
		}

		private void ResumeAllAI()
		{
		}

		public void PauseBattle()
		{
		}

		public void ResumeBattle()
		{
		}

		private void OnHeroPlayerClick()
		{
		}

		private void OnHeroEnemyClick()
		{
		}

		public void CastSpellOnEnemy(TapSpellData spellData)
		{
		}

		public bool IsOnBattleState()
		{
			return false;
		}

		private void ReturnToFirstStageOfGroup()
		{
		}

		private void LoadNextStage()
		{
		}

		public void LoadStage(int stageId)
		{
		}

		private void CheckToChangeTheme(int stageId)
		{
		}

		private void Update()
		{
		}
	}
}
