using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public abstract class GGCellAccumulatedBase : MonoBehaviour
	{
		public Text m_TextTitle;

		public RectTransform m_TfRewards;

		public ScrollRect m_ScrollRectReward;

		protected GGAccumulatedReward _accumulatedReward;

		private void Start()
		{
		}

		public virtual void SetCellData(GGAccumulatedReward accReward)
		{
		}

		protected void LoadReward(BonusModel bm, bool showCheckOnReward = false)
		{
		}

		private void Update()
		{
		}
	}
}
