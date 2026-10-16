using System.Collections.Generic;

namespace Fake.Tap3
{
	public class TapTutorialMng3
	{
		public const int TUT_ATTACK_ENEMY = 1;

		public const int TUT_HEAL_PLAYER = 2;

		public const int TUT_SPELL = 3;

		private static TapTutorialMng3 _instance;

		private List<TapTutorial3> _listTut;

		public static TapTutorialMng3 Instance => null;

		private TapTutorialMng3()
		{
		}

		private void Init()
		{
		}

		public TapTutorial3 GetTutorial(int idx)
		{
			return null;
		}
	}
}
