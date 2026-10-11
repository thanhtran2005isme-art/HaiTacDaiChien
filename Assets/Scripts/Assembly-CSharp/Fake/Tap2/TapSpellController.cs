using System.Collections.Generic;
using UnityEngine;

namespace Fake.Tap2
{
	public class TapSpellController
	{
		private float _countTimeFinish;

		private TapSpellData _spellData;

		private bool _finished;

		private TapHero _tapHero;

		private List<TapSpellEffect> _listSpellEffect;

		private float _countTimeEffect;

		public TapSpellController(TapHero tapHero, TapSpellData spellData)
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
