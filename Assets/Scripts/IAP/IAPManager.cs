using System;
using UnityEngine;
using UnityEngine.Purchasing;
using BayatGames.SaveGameFree;
public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    //Step 1 create your products
    private string Coins500 = "coins_500";
    private string Coins1k = "coins_1000";
    private string Coins2k = "coins_2000";


    //************************************** Adjust these methods **************************************
    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Step 2 choose if your product is a consumable or non consumable
        builder.AddProduct(Coins500, ProductType.Consumable);
        builder.AddProduct(Coins1k, ProductType.Consumable);
        builder.AddProduct(Coins2k, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    //Step 3 Create methods
    public void Buy500Coins()
    {
        //BuyProductID(Coins500);
    }
    public void Buy1000Coins()
    {
        //BuyProductID(Coins1k);
    }
    public void Buy2000Coins()
    {
        //BuyProductID(Coins2k);
    }



    //Step 4 modify purchasing
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, Coins500, StringComparison.Ordinal))
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT
            //SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount", 0) + 500);
            Debug.Log("ADD 500 COINS");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Coins1k, StringComparison.Ordinal))
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT
            //SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount", 0) + 1000);
            Debug.Log("ADD 1000 COINS");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Coins2k, StringComparison.Ordinal))
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT
            //SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount", 0) + 2000);
            Debug.Log("ADD 2000 COINS");
        }
        else
        {
            SoundManager.PlaySFX("NotEnoughtCoins", false, 0, .3f); // SOUND NOTENOUGHTCOINS
            Debug.Log("Purchase Failed");
        }
        return PurchaseProcessingResult.Complete;
    }

    //**************************** Dont worry about these methods ***********************************
    private void Awake()
    {
        TestSingleton();
    }

    void Start()
    {
        if (m_StoreController == null) { InitializePurchasing(); }
    }

    private void TestSingleton()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}