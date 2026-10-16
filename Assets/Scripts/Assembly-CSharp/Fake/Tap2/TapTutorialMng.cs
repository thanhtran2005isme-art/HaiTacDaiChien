using System.Collections.Generic;

namespace Fake.Tap2
{
	public class TapTutorialMng
	{
		public const int TUT_ATTACK_ENEMY = 1;

		public const int TUT_HEAL_PLAYER = 2;

		public const int TUT_SPELL = 3;

		private static TapTutorialMng _instance;

		private List<TapTutorial> _listTut;

		public static TapTutorialMng Instance => null;

		private TapTutorialMng()
		{
		}

		private void Init()
		{
		}

		public TapTutorial GetTutorial(int idx)
		{
			return null;
		}
	}
}
