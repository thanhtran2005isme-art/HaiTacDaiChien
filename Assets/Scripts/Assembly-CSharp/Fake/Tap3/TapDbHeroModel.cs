using System.Collections.Generic;

namespace Fake.Tap3
{
	public class TapDbHeroModel
	{
		private static TapDbHeroModel _instance;

		private List<TapHeroModel> m_ListHeroModel;

		private const string PATH_PACK_HEROES = "_Pack/";

		private const string PATH_TAP3_HEROES = "GameFkTap3/Spines/";

		public static TapDbHeroModel Instance => null;

		private TapDbHeroModel()
		{
		}

		private void InitDb()
		{
		}

		public TapHeroModel GetHeroModel(int heroKey)
		{
			return null;
		}

		private void GenHeroPlayer()
		{
		}

		private void GenHeroSideKick()
		{
		}

		private void GenHeroEnemyMinion()
		{
		}

		private void GenHeroEnemyBoss()
		{
		}
	}
}
