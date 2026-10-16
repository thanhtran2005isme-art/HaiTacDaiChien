using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class PopupPurchaseIAP : BasePopup
{
	public GameObject m_GoGroupLoading;

	private IStoreController m_StoreController;

	private IExtensionProvider m_StoreExtensionProvider;

	public Action<PurchaseEventArgs> m_PurchaseSuccessCb;

	public Action<string> m_PurchaseFailCb;

	private string _productIdPurchase;

	protected override void Start()
	{
	}

	public void InitializePurchasing()
	{
	}

	private bool IsInitialized()
	{
		return false;
	}

	private void SetData(string productId)
	{
	}

	private void MakePurchase()
	{
	}

	private void ClosePopupWithFailResult(string message = "")
	{
	}

	private void ClosePopupWithSuccessResult(PurchaseEventArgs purchaseArgs)
	{
	}

	public static PopupPurchaseIAP ShowUp(string productId)
	{
		return null;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
	{
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
	}
}
