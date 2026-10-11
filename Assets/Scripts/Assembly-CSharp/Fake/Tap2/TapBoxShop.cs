using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Fake.Tap2
{
	public class TapBoxShop : MonoBehaviour
	{
		public TapPanelLoading m_PanelLoading;

		private IStoreController m_StoreController;

		private IExtensionProvider m_StoreExtensionProvider;

		public GameObject m_PrefabShopItem;

		public RectTransform m_TfContainer;

		private const float TIME_WAIT_IAP = 10f;

		private static List<TapShopItemInfo> _listItemInfo;

		private List<TapShopItem> _listShopItem;

		static TapBoxShop()
		{
		}

		private void Start()
		{
		}

		private void LoadShopItem()
		{
		}

		public void InitializePurchasing()
		{
		}

		private bool IsInitialized()
		{
			return false;
		}

		private void BuyProductID(string productId)
		{
		}

		public void RestorePurchases()
		{
		}

		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
		}

		public void OnInitializeFailed(InitializationFailureReason error)
		{
		}

		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
		{
			return PurchaseProcessingResult.Complete;
		}

		public TapShopItemInfo GetIapItemInfo(string productId)
		{
			return null;
		}

		public void CloseShop()
		{
		}

		public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
		{
		}

		private void Update()
		{
		}
	}
}
