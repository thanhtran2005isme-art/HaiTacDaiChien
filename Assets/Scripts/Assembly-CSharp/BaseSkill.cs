using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SimpleJSON;
using UnityEngine;

public abstract class BaseSkill
{
	[CompilerGenerated]
	private sealed class _003CDelayGenBattleEffect_003Ed__38 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delayTime;

		public BaseSkill _003C_003E4__this;

		public BattleChainListEffect chainListEffect;

		public List<BattleHero> lstAllHero;

		public float delayPerChain;

		private List<string> _003ClstHeroKeyHasSplitDmg_003E5__2;

		private bool _003CneedSplitMainAttack_003E5__3;

		private List<List<HeroBattleEffect>>.Enumerator _003C_003E7__wrap3;

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
		public _003CDelayGenBattleEffect_003Ed__38(int _003C_003E1__state)
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
	private sealed class _003CDelayGenSkillEffect_003Ed__37 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public EffectInfo effectInfo;

		public BaseSkill _003C_003E4__this;

		public int layerOrderEffect;

		public GameDirection directionSkill;

		public float aniTimeScale;

		public Vector2 posOriginEffect;

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
		public _003CDelayGenSkillEffect_003Ed__37(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CDelayShakeCamera_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public float duration;

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
		public _003CDelayShakeCamera_003Ed__31(int _003C_003E1__state)
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

	public const int SKILL_MELEE = 1;

	public const int SKILL_RANGE = 2;

	public const int SKILL_AOE = 3;

	public const int SKILL_LASER = 4;

	public string m_SkillBattleDebugInfo;

	public int m_SkillType;

	public string m_AnimationName;

	public ImpactPower m_ImpactPower;

	public float m_DelayCastEffect;

	public float m_DelayBattleEffect;

	public float m_DurationSkill;

	public bool needMoveUp;

	public List<EffectInfo> m_LstCastEffectInfo;

	public List<EffectInfo> m_LstImpactEffectInfo;

	public List<ShakeCameraInfo> m_LstShakeCamInfo;

	public List<DamageSplitInfo> m_lstDmgSplitInfo;

	public GGPool m_PoolBattleObject;

	public const float ANI_TIME_SCALE_EFFECT_IMPACT = 0.6f;

	public const string EFFECT_SORTING_LAYER_DEFAULT = "Character";

	public BaseSkill(string animationName, float delayCastEffect)
	{
	}

	public virtual JSONClass ToJSONObject()
	{
		return null;
	}

	public virtual void ImportFromJSONObject(JSONClass jsonObject)
	{
	}

	public virtual void AdjustSkillPath(bool debug = false)
	{
	}

	public static int FindHeroFolderIdBaseOnPathResource(string path)
	{
		return 0;
	}

	public static void AdjustEffectInfoByPackId(EffectInfo effInfo)
	{
	}

	public static void AdjustSkillObjectInfoByPackId(SkillObjectInfo skillObjInfo)
	{
	}

	public virtual List<EffectInfo> GetListSpineEffectInfo()
	{
		return null;
	}

	public virtual List<SkillObjectInfo> GetListSpineSkillObjectInfo()
	{
		return null;
	}

	public void AddCastEffect(EffectInfo effect)
	{
	}

	public void AddImpactEffect(EffectInfo effect)
	{
	}

	public void PerformCameraShaking()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShakeCamera_003Ed__31))]
	private IEnumerator DelayShakeCamera(float delay, float duration)
	{
		return null;
	}

	public void PerformCastEffect(BattleHero heroAction)
	{
	}

	public void PerformPetCastEffect(BattlePet petAction)
	{
	}

	public virtual void PerformPetSkill(BattlePet petAction, List<BattleHero> listHeroBeImpacted, List<BattleHero> listAllHeroes, BattleChainListEffect chainListEffect, float delayBattleEffect)
	{
	}

	public abstract void PerformSkill(BattleHero heroAction, List<BattleHero> listHeroBeImpacted, List<BattleHero> listAllHeroes, BattleChainListEffect chainListEffect, float delayBattleEffect);

	public abstract void PerformSkillWithOutBattleEffect(BattleHero heroAction, List<BattleHero> listHeroBeImpacted);

	[IteratorStateMachine(typeof(_003CDelayGenSkillEffect_003Ed__37))]
	public IEnumerator DelayGenSkillEffect(float delay, GameDirection directionSkill, int layerOrderEffect, Vector2 posOriginEffect, EffectInfo effectInfo, float aniTimeScale = 1f)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayGenBattleEffect_003Ed__38))]
	public IEnumerator DelayGenBattleEffect(float delayTime, float delayPerChain, List<BattleHero> lstAllHero, BattleChainListEffect chainListEffect)
	{
		return null;
	}

	private void ProcessInstanceEffectOnPet(BattlePet petGotEff, BattleEffectData effData)
	{
	}

	private void ProcessInstantEffectLostHp(BattleHero heroGotEff, BattleEffectData effData, List<DamageSplitInfo> lstDmgSplitInfo)
	{
	}

	public void ProcessInstantEffectOnHero(BattleHero heroGotEff, BattleEffectData effData)
	{
	}

	private void ProcessBodyEffectOnHero(BattleHero heroGotEff, BattleEffectData effData)
	{
	}

	private BattleHero FindHeroInList(List<BattleHero> lstHeroes, int teamId, int slotIdx)
	{
		return null;
	}

	protected int GetOrderLayerByListHero(List<BattleHero> lstHeroScript, float yCheck)
	{
		return 0;
	}
}
