using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Main.Bank
{
    public class BuyItemsInfo
    {
        public static readonly float[] prices_rur = new float[6] { 75, 375, 750, 1500, 3000, 5500 };
        public static readonly float[] prices_usd = new float[6] { 0.99f, 1.99f, 4.99f, 10.99f, 30.99f, 50.00f };

        public static readonly int[] values = new int[6] { 10000, 5500, 12000, 25000, 53000, 150000 };

        public static readonly string[] ids = new string[1]
            {"gold_1000"};
        public static readonly string[] google_play_ids = new string[1]
            {"gold_001"};

        public static string GetPriceString(int id)
        {
            if(Application.systemLanguage == SystemLanguage.Russian)
            {
                return prices_rur[id].ToString() + " Р.";
            }
            else
            {
                return prices_usd[id].ToString() + "$";
            }
        }

        public static int GetIndexOfId(string id)
        {
            for(int i = 0; i < ids.Length; ++i)
            {
                if (String.Equals(id, ids[i]))
                    return i;
            }

            return -1;
        }
    }
    
    public class BankController : MonoBehaviour, IStoreListener
    {
        public BankBinder binder;

        private static IStoreController m_StoreController;          // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

        // Product identifiers for all products capable of being purchased: 
        // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
        // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
        // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

        // General product identifiers for the consumable, non-consumable, and subscription products.
        // Use these handles in the code to reference which product to purchase. Also use these values 
        // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
        // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
        // specific mapping to Unity Purchasing's AddProduct, below.


        // Apple App Store-specific product identifier for the subscription product.
        private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

        // Google Play Store-specific product identifier subscription product.
        private static string kProductNameGooglePlaySubscription = "com.scannerapps.whatCat";

        void Start()
        {
            binder = gameObject.transform.parent.Find("BankBinder").gameObject.GetComponent<BankBinder>();

            // If we haven't set up the Unity Purchasing reference
            if (m_StoreController == null)
            {
                // Begin to configure our connection to Purchasing
                InitializePurchasing();
            }
            else
            {
                InitBuyItems();
            }

            binder.log_text.gameObject.SetActive(false);
        }

        public void InitializePurchasing()
        {
            // If we have already connected to Purchasing ...
            if (IsInitialized())
            {
                // ... we are done here.
                return;
            }

            // Create a builder, first passing in a suite of Unity provided stores.
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            // Add a product to sell / restore by way of its identifier, associating the general identifier
            // with its store-specific identifiers.
            builder.AddProduct(BuyItemsInfo.ids[0], ProductType.Consumable, new IDs() { { BuyItemsInfo.google_play_ids[0], GooglePlay.Name} });
            // Continue adding the non-consumable product.
            // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
            // if the Product ID was configured differently between Apple and Google stores. Also note that
            // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
            // must only be referenced here. 
            //builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
            //    { kProductNameAppleSubscription, AppleAppStore.Name },
            //    { kProductNameGooglePlaySubscription, GooglePlay.Name },
            //});

            // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
            // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
            UnityPurchasing.Initialize(this, builder);
        }


        private bool IsInitialized()
        {
            // Only say we are initialized if both the Purchasing references are set.
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }


        void BuyProductID(string productId)
        {
            GameStatistics.instance.SendStat("btn_buy_bank_item_pressed", 0);
            // If Purchasing has been initialized ...
            if (IsInitialized())
            {
                // ... look up the Product reference with the general product identifier and the Purchasing 
                // system's products collection.
                Product product = m_StoreController.products.WithID(productId);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                    // asynchronously.
                    m_StoreController.InitiatePurchase(product);
                }
                // Otherwise ...
                else
                {
                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            // Otherwise ...
            else
            {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
                // retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }


        // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
        // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
        public void RestorePurchases()
        {
            // If Purchasing has not yet been set up ...
            if (!IsInitialized())
            {
                // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                return;
            }

            // If we are running on an Apple device ... 
            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                // ... begin restoring purchases
                Debug.Log("RestorePurchases started ...");

                // Fetch the Apple store-specific subsystem.
                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
                // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
                apple.RestoreTransactions((result) => {
                    // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                    // no purchases are available to be restored.
                    Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
                });
            }
            // Otherwise ...
            else
            {
                // We are not running on an Apple device. No work is necessary to restore purchases.
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
        }


        //  
        // --- IStoreListener
        //

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            // Purchasing has succeeded initializing. Collect our Purchasing references.
            Debug.Log("OnInitialized: PASS");
           // binder.log_text.text = "OnInitialized";
            // Overall Purchasing system, configured with products for this application.
            m_StoreController = controller;
            // Store specific subsystem, for accessing device-specific store features.
            m_StoreExtensionProvider = extensions;

            for(int i = 0; i < BuyItemsInfo.ids.Length; ++i)
            {
                var product = m_StoreController.products.WithID(BuyItemsInfo.ids[i]);
                var go = Instantiate(binder.item_prefab, binder.items_conteiner.transform, false);
                go.GetComponent<BankBuyItem>().Init(i,
                    product.metadata.localizedPriceString,
                    () => { BuyProductID(BuyItemsInfo.ids[0]); });
            }
        }

        void InitBuyItems()
        {
            for (int i = 0; i < BuyItemsInfo.ids.Length; ++i)
            {
                var product = m_StoreController.products.WithID(BuyItemsInfo.ids[i]);
                var go = Instantiate(binder.item_prefab, binder.items_conteiner.transform, false);
                go.GetComponent<BankBuyItem>().Init(i,
                    product.metadata.localizedPriceString,
                    () => { BuyProductID(BuyItemsInfo.ids[0]); });
            }
        }


        public void OnInitializeFailed(InitializationFailureReason error)
        {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
           // binder.log_text.text = "OnInitializeFailed InitializationFailureReason:" + error;
        }


        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            // A consumable product has been purchased by this user.
            if (String.Equals(args.purchasedProduct.definition.id, BuyItemsInfo.ids[0], StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

                DataController.instance.catsPurse.Coins += BuyItemsInfo.values[BuyItemsInfo.GetIndexOfId(args.purchasedProduct.definition.id)];

                GameStatistics.instance.SendStat("IAP_SUCCESS", 0);

                Yaga.MessageBus.MessageBus.Instance.SendMessage(API.BankAPI.CLOSE);
            }
            // Or ... a non-consumable product has been purchased by this user.
            //else if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
            //{
            //    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            //    // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
            //}
            //// Or ... a subscription product has been purchased by this user.
            //else if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
            //{
            //    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            //    // TODO: The subscription item has been successfully purchased, grant this to the player.
            //}
            // Or ... an unknown product has been purchased by this user. Fill in additional products here....
            else
            {
                Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            }

            // Return a flag indicating whether this product has completely been received, or if the application needs 
            // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
            // saving purchased products to the cloud, and when that save is delayed. 
            return PurchaseProcessingResult.Complete;
        }


        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
            // this reason with the user to guide their troubleshooting actions.
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }
    }
}
