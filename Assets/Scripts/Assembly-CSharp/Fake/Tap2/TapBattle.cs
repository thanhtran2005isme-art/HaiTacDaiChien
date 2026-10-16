using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fake.Tap2
{
	public class TapBattle : MonoBehaviour
	{
		public GameObject m_PointEnemy;

		public GameObject m_PointPlayer;

		public GameObject m_PointSpell;

		public TapPanelPause m_PanelPause;

		public TapPanelCurtain m_PanelCurtain;

		public TapButtonSpell m_ButtonSpellFreeze;

		public TapButtonSpell m_ButtonSpellBurn;

		public TapPanelLoot m_PanelLoot;

		public List<GameObject> m_ListPointSidekick;

		public GameObject m_GoHandTut;

		public Image m_ImageBgr;

		public Text m_TextStage;

		private const int STATE_PAUSED = 1;

		private const int STATE_ON_BATTLE = 2;

		private const int STATE_LOAD_NEXT_STAGE = 3;

		private int _battleState;

		private bool _onTutorial;

		public static TapBattle Instance;

		private const bool CHEAT_DMG = false;

		private TapHero _tapHeroPlayer;

		private TapHero _tapHeroEnemy;

		private List<TapHero> _lstTapHeroSidekick;

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

		private TapHero LoadSideKick(int skId, float cooldown, TapHeroInfo skInfo)
		{
			return null;
		}

		public void CalculateHeroPower(bool firstInit)
		{
		}

		public void OnHeroDeath(TapHero tapHero)
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
