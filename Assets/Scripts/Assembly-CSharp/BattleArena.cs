using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BattleArena : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadBattle_003Ed__33 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BattleArena _003C_003E4__this;

		public BattleData battleData;

		private PanelPrepareScene _003CpanelPrepare_003E5__2;

		private List<HeroBattleInfo> _003ClstHeroField_T2_003E5__3;

		private GameObject _003CprefabHero_003E5__4;

		private GameObject _003CprefabPet_003E5__5;

		private int _003CbattleHeroId_003E5__6;

		private float _003CPROGRESS_VALUE_FOR_SKILL_003E5__7;

		private List<EffectInfo> _003ClstAllEffectInfo_003E5__8;

		private List<SkillObjectInfo> _003ClstAllSkillObjectInfo_003E5__9;

		private float _003CdelayBetweenLoad_003E5__10;

		private float _003CvalueLoadingEachHeroSpine_003E5__11;

		private float _003CprogressLoading_003E5__12;

		private float _003CvalueLoadingEachSkillSpine_003E5__13;

		private List<HeroBattleInfo>.Enumerator _003C_003E7__wrap13;

		private List<EffectInfo>.Enumerator _003C_003E7__wrap14;

		private List<SkillObjectInfo>.Enumerator _003C_003E7__wrap15;

		private string _003CplayerName1_003E5__17;

		private string _003CplayerName2_003E5__18;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CDelayLoadBattle_003Ed__33(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
		}

		private void _003C_003Em__Finally2()
		{
		}

		private void _003C_003Em__Finally3()
		{
		}

		private void _003C_003Em__Finally4()
		{
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CDoChainBatleAction_003Ed__65 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int skillIndex;

		public BattleArena _003C_003E4__this;

		public List<BattleAction> lstBa;

		private List<BattleAction>.Enumerator _003C_003E7__wrap1;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CDoChainBatleAction_003Ed__65(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CLoopToBattleAct_003Ed__59 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BattleArena _003C_003E4__this;

		private bool _003CbattleEnd_003E5__2;

		private int _003Ccount_003E5__3;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CLoopToBattleAct_003Ed__59(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	private List<GameObject> m_LstGoHero1;

	private List<GameObject> m_LstGoHero2;

	private GameObject m_GoPet1;

	private GameObject m_GoPet2;

	[SerializeField]
	private CameraController m_CameraController;

	public BattleSide m_BattleSideLeff;

	public BattleSide m_BattleSideRight;

	public PetRageBar m_PetRageBar1;

	public PetRageBar m_PetRageBar2;

	public BaGenerator m_BaGenerator;

	public PanelDebugPoint m_PanelDebugPoint;

	public GameObject m_GoHeroesContainer;

	[HideInInspector]
	public List<BattleHero> m_LstAllHeroScript;

	[SerializeField]
	private GGPool m_PoolBattleObject;

	public static BattleArena m_Instance;

	private ActionRepeatTimer m_ActionRepeatSortHero;

	private bool m_HasShowBoardVs;

	[SerializeField]
	private Text m_TextRound;

	public int m_SkillEffectTest;

	private List<BattleData> m_LstBattleData;

	private int m_CurrentBattleIndex;

	private BattleData m_CurrentBattleData;

	private BattleResultInfo m_BattleResultInfo;

	private bool m_SkipBattle;

	private bool m_HasShowPopupBattleEnd;

	private bool m_HasShowPopupNextBattle;

	[HideInInspector]
	public bool m_PauseBattle;

	public BoxAuraInBattle auraNew1;

	public BoxAuraInBattle auraNew2;

	private int m_BeIndex;

	[HideInInspector]
	public int m_BattleEventIdx;

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	private void LoadRandomBackground()
	{
	}

	public void LoadBattle()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadBattle_003Ed__33))]
	private IEnumerator DelayLoadBattle(BattleData battleData)
	{
		return null;
	}

	public void ResumeBattleAfterTutorial()
	{
	}

	public void InitBattleField()
	{
	}

	public Vector2 GetPointCastSkill(GameDirection side)
	{
		return default;
	}

	public Vector2 GetHeroFieldPosition(GameDirection side)
	{
		return default;
	}

	public BattlePet GetPetScript(int teamId)
	{
		return null;
	}

	public BattleHero GetHeroScript(HeroFieldInfo heroFieldInfo)
	{
		return null;
	}

	public void PrintHeroFieldInfo()
	{
	}

	public List<BattleHero> GetListHeroScript(List<HeroFieldInfo> lstHeroFieldInfo)
	{
		return null;
	}

	public void CheckHeroAnimationTimeScale()
	{
	}

	public void SortOrderAllHeroes()
	{
	}

	public void TestPet_1()
	{
	}

	public void TestPet_2()
	{
	}

	public void TestLeftSkill_1()
	{
	}

	public void TestLeftSkill_2()
	{
	}

	public void TestRightSkill_1()
	{
	}

	public void TestRightSkill_2()
	{
	}

	public List<HeroBattleEffect> GenListDebuffRageEff(List<HeroFieldInfo> lstHeroFieldInfo)
	{
		return null;
	}

	public List<HeroBattleEffect> GenListHpLostEff(BattleHero heroAction, List<HeroFieldInfo> lstHeroFieldInfo, bool enableEffect = true)
	{
		return null;
	}

	public List<HeroBattleEffect> GenListHpLostToDeathEff(BattleHero heroAction, List<HeroFieldInfo> lstHeroFieldInfo)
	{
		return null;
	}

	public List<HeroBattleEffect> GenListHpLostEffNoBattleEffect(BattleHero heroAction, List<HeroFieldInfo> lstHeroFieldInfo)
	{
		return null;
	}

	private bool IsSkillEffectTypeBuffDebuff(int effectId)
	{
		return false;
	}

	public List<HeroBattleEffect> GenListHpBuffEff(List<HeroFieldInfo> lstHeroFieldInfo)
	{
		return null;
	}

	public List<HeroBattleEffect> GenListRageBuffEff(List<HeroFieldInfo> lstHeroFieldInfo)
	{
		return null;
	}

	public void ShakeCamera(float duration)
	{
	}

	[IteratorStateMachine(typeof(_003CLoopToBattleAct_003Ed__59))]
	private IEnumerator LoopToBattleAct()
	{
		return null;
	}

	public void PlayNextBattleEvent()
	{
	}

	public void SkipThisBattle()
	{
	}

	public void CheckToShowBattleResult()
	{
	}

	private void ShowPopupBattleEnd(BattlePointDetail battlePointDetail)
	{
	}

	[IteratorStateMachine(typeof(_003CDoChainBatleAction_003Ed__65))]
	private IEnumerator DoChainBatleAction(int skillIndex, List<BattleAction> lstBa)
	{
		return null;
	}

	public void Update()
	{
	}
}
