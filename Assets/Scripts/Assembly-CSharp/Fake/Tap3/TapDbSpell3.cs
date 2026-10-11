using System.Collections.Generic;

namespace Fake.Tap3
{
	public class TapDbSpell3
	{
		public const int SPELL_FREEZE = 1;

		public const int SPELL_BOOST = 2;

		public const int SPELL_BURN = 3;

		private static TapDbSpell3 _instance;

		private List<TapSpellData> _listSpellData;

		public static TapDbSpell3 Instance => null;

		private TapDbSpell3()
		{
		}

		public TapSpellData GetSpell(int spellId)
		{
			return null;
		}
	}
}
