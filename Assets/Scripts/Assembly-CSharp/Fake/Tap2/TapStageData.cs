using UnityEngine;

namespace Fake.Tap2
{
	public class TapStageData : MonoBehaviour
	{
		private static TapStageData _instance;

		public static TapStageData Instance => null;

		private TapStageData()
		{
		}

		private void InitData()
		{
		}

		public TapStageInfo GetStageInfo(int stageId)
		{
			return null;
		}
	}
}
