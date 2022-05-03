using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseButton : MonoBehaviour
{
    public enum PurchaseType { removeAds, coin1000, support_dev };
    public PurchaseType purchaseType;

    public Text priceText;
    private string defaultText;

    private void Start()
    {
        defaultText = priceText.text;
        StartCoroutine(LoadPriceRoutine());
    }

    public void ClickPurchaseButton()
    {
        switch (purchaseType)
        {
            case PurchaseType.removeAds:
                IAPManager.Instance.BuyNO_ADS();
                break;
            case PurchaseType.coin1000:
                IAPManager.Instance.BuyCoin1000();
                break;
            case PurchaseType.support_dev:
                IAPManager.Instance.BuySup_Dev();
                break;
        }
    }

    private IEnumerator LoadPriceRoutine()
    {
        while (!IAPManager.Instance.IsInitialized())
            yield return null;

        string loadedPrice = "";

        switch (purchaseType)
        {
            case PurchaseType.removeAds:
                loadedPrice = IAPManager.Instance.GetProductPriceFromStore(IAPManager.Instance.NO_ADS);
                break;
            case PurchaseType.coin1000:
                loadedPrice = IAPManager.Instance.GetProductPriceFromStore(IAPManager.Instance.coin1000);
                break;
            case PurchaseType.support_dev:
                loadedPrice = IAPManager.Instance.GetProductPriceFromStore(IAPManager.Instance.Sup_Dev);
                break;
        }
        priceText.text = defaultText + " " + loadedPrice;
    }
}
