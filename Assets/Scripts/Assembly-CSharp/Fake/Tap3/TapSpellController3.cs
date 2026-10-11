using System.Collections.Generic;
using UnityEngine;

namespace Fake.Tap3
{
	public class TapSpellController3
	{
		private float _countTimeFinish;

		private TapSpellData _spellData;

		private bool _finished;

		private TapHero3 _tapHero;

		private List<TapSpellEffect3> _listSpellEffect;

		private float _countTimeEffect;

		public TapSpellController3(TapHero3 tapHero, TapSpellData spellData)
		{
		}

		public int GetSpellId()
		{
			return 0;
		}

		public void ApplySpell(Transform tfEffectBelow, Transform tfEffectAbove)
		{
		}

		public void FirstActiveSpell()
		{
		}

		public void UpdateSpell(float deltaTime)
		{
		}

		public void FinishEffect()
		{
		}

		public bool IsFinished()
		{
			return false;
		}

		public void Update(float deltaTime)
		{
		}
	}
}
