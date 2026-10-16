using UnityEngine;

namespace GGEvent
{
	public class PopupEasterPrayRewards : BasePopup
	{
		public TabManager m_TabManager;

		public GameObject m_GoPrefabTabButton;

		public RectTransform m_TfTabContainer;

		public GridPoolGroup m_PoolRewards;

		private GGEasterPrayEventData _eventData;

		protected override void Awake()
		{
		}

		protected override void Start()
		{
		}

		public void LoadEventData(GGEasterPrayEventData eventData, int indexActive)
		{
		}

		private void OnTabActive(int buttonTabId)
		{
		}

		public static PopupEasterPrayRewards ShowUp(GGEasterPrayEventData eventData, int statueIdActive)
		{
			return null;
		}

		private void Update()
		{
		}
	}
}
