using System.Collections.Generic;

namespace Fake.Tap2
{
	public class TapDbSpell
	{
		public const int SPELL_FREEZE = 1;

		public const int SPELL_BOOST = 2;

		public const int SPELL_BURN = 3;

		private static TapDbSpell _instance;

		private List<TapSpellData> _listSpellData;

		public static TapDbSpell Instance => null;

		private TapDbSpell()
		{
		}

		public TapSpellData GetSpell(int spellId)
		{
			return null;
		}
	}
}
