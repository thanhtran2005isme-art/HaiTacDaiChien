using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fake.Tap2
{
	public class TapHero : MonoBehaviour
	{
		public CanvasHero m_CanvasHero;

		public Transform m_TfEffectBelow;

		public Transform m_TfEffectContainer;

		public Transform m_TfHpContainer;

		public GameObject m_PrefabHealingEff;

		public GameObject m_PrefabAppearEff;

		public GameObject m_PrefabHitEff;

		[HideInInspector]
		public bool m_AutoAttack;

		[HideInInspector]
		public bool m_DebugOn;

		private bool _readyToAttack;

		private float _countDownToReady;

		private float _attackCooldownTime;

		public const int TYPE_ENEMY = 1;

		public const int TYPE_HERO = 2;

		public const int TYPE_SIDEKICK = 3;

		[HideInInspector]
		public int m_TypeHero;

		[HideInInspector]
		public int m_SidekickId;

		private bool _onPaused;

		private bool _onControlEffect;

		private bool _onTutorial;

		public Action<TapHero> m_HeroDeathCb;

		private List<TapSpellController> _listSpellController;

		private TapHero m_TapHeroTarget;

		private bool m_OnBoost;

		private TapHeroInfo _heroInfo;

		private void Start()
		{
		}

		public void SetTimeCoolDownAttack(float timeCooldown)
		{
		}

		public bool IsOnBoost()
		{
			return false;
		}

		public void BoostPower()
		{
		}

		public void CancelBoost()
		{
		}

		public void Pause()
		{
		}

		public void Resume()
		{
		}

		public bool IsDeath()
		{
			return false;
		}

		public void SetHeroTarget(TapHero tapHeroTarget)
		{
		}

		public bool IsReadyToAttack()
		{
			return false;
		}

		public void LoadBoss(int bossId, GameDirection facing = GameDirection.RIGHT)
		{
		}

		public void LoadHero(int heroKey, GameDirection facing = GameDirection.RIGHT)
		{
		}

		public void SetClickCallback(Action cb)
		{
		}

		public void ResetHeroInfo()
		{
		}

		public void SetHeroInfo(TapHeroInfo heroInfo)
		{
		}

		public TapHeroInfo GetHeroInfo()
		{
			return null;
		}

		public void PlayAutoAttack()
		{
		}

		public void PlayAttack()
		{
		}

		public void PlayAniPause()
		{
		}

		public void PlayDie()
		{
		}

		public void ShowAppearEffect()
		{
		}

		public void HitByHpPercent(float percent)
		{
		}

		public void HealHero(float hpAmount)
		{
		}

		public void HitHero(float damagePoint)
		{
		}

		public void DisableHeroAction()
		{
		}

		public void OnCastSpell(TapSpellData spellData)
		{
		}

		public void CheckToRemoveControlEffect()
		{
		}

		public static TapHero InstanceOne(Transform tfParent)
		{
			return null;
		}

		private void Update()
		{
		}

		private void DebugInfo(string msg)
		{
		}
	}
}
