using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PopupShopFakeEn : MonoBehaviour
{
	public List<GameObject> m_LstGoIapItemGem;

	public List<GameObject> m_LstGoIapItemGold;

	private IStoreController m_StoreController;

	private IExtensionProvider m_StoreExtensionProvider;

	private string m_PackageCoin0;

	private string m_PackageCoin1;

	private string m_PackageCoin2;

	private string m_PackageCoin3;

	private string m_PackageCoin4;

	private string m_PackageCoin5;

	private string m_PackageCoin6;

	private string m_PackageCoin7;

	private string m_PackageCoin8;

	private string m_PackageCoin9;

	private string m_PackageGold1;

	private string m_PackageGold2;

	public const int m_Coin0 = 100;

	public const int m_Coin1 = 300;

	public const int m_Coin2 = 900;

	public const int m_Coin3 = 1800;

	public const int m_Coin4 = 3600;

	public const int m_Coin5 = 5400;

	public const int m_Coin6 = 9000;

	public const int m_Coin7 = 12600;

	public const int m_Coin8 = 18000;

	public const int m_Coin9 = 22000;

	public const int m_Gold1 = 600;

	public const int m_Gold2 = 1600;

	private List<IapInfo> m_LstIapInfoGem;

	private List<IapInfo> m_LstIapInfoGold;

	[HideInInspector]
	public PanelLoadingInstant m_PanelLoading;

	private const float TIME_WAIT_IAP = 30f;

	public bool m_PopupActive;

	private void Start()
	{
	}

	private void OnDestroy()
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

	public IapInfo GetIapItemInfo(string productId)
	{
		return null;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
	}

	public static PopupShopFakeEn ShowUp()
	{
		return null;
	}

	private void LoadIAP()
	{
	}

	public void CloseShop()
	{
	}

	private void Update()
	{
	}
}
