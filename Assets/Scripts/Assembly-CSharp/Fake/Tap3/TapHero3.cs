using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fake.Tap3
{
	public class TapHero3 : MonoBehaviour
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

		public Action<TapHero3> m_HeroDeathCb;

		private List<TapSpellController3> _listSpellController;

		private TapHero3 m_TapHeroTarget;

		private bool m_OnBoost;

		private TapHeroInfo3 _heroInfo;

		private TapHeroModel _heroModel;

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

		public void SetHeroTarget(TapHero3 tapHeroTarget)
		{
		}

		public bool IsReadyToAttack()
		{
			return false;
		}

		public void SetPosition(Vector2 pos)
		{
		}

		public Vector2 GetPosition()
		{
			return default;
		}

		public void LoadHero2(int heroKey, GameDirection facing = GameDirection.RIGHT)
		{
		}

		public GameDirection GetFacingDirection()
		{
			return (GameDirection)0;
		}

		public void SetClickCallback(Action cb)
		{
		}

		public void ResetHeroInfo()
		{
		}

		public TapHeroInfo3 GetHeroInfo()
		{
			return null;
		}

		public TapHeroModel GetHeroModel()
		{
			return null;
		}

		public void SetHeroInfo(TapHeroInfo3 heroInfo)
		{
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

		public static TapHero3 InstanceOne(Transform tfParent)
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
