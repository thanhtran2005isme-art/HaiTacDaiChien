using UnityEngine;

namespace Fake.Tap3
{
	public class TapBottomMenu3 : MonoBehaviour
	{
		public TapBattle3 m_TabBattle;

		public TabManager m_TabMng;

		public const int TAB_BTN_HERO_UPGRADE = 1;

		public const int TAB_BTN_SIDE_KICK = 2;

		public const int TAB_BTN_BOOST = 3;

		public const int TAB_BTN_LUCKY_SPIN = 4;

		public const int TAB_BTN_SHOP = 5;

		public GameObject m_GoBgrBlock;

		public GameObject m_GoBoxUpgradeHero;

		public GameObject m_GoBoxUpgradeSidekick;

		public GameObject m_GoBoxBoostPower;

		public GameObject m_GoBoxLuckySpin;

		public GameObject m_GoBoxShop;

		public static TapBottomMenu3 Instance;

		private GameObject _goBoxActive;

		private void Start()
		{
		}

		public void CloseAllBox()
		{
		}

		private void HideAllBoxContent()
		{
		}

		public void ShowBoxUpgradeHero()
		{
		}

		private void OnButtonTabClick(int btnId)
		{
		}

		public void OnBgrContentClick()
		{
		}

		private void Update()
		{
		}
	}
}
