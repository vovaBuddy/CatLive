using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;

namespace TimeManager.ProductProviderAPI
{
    using TimeManager.Product;

    public static class Messages
    {
        public const string SEND_PRODUCT_TO_PRODUCTION = "TimeManager.ProductProviderAPI.SEND_PRODUCT_TO_PRODUCTION";
        public const string INIT_PRODUCT_PROVIDERS =     "TimeManager.ProductProviderAPI.INIT_PRODUCT_PROVIDERS";
    }

    public class InitPPParams : MessageParametrs
    {
        public List<ProductProvider.ProductProviderConfig> configs;

        public InitPPParams(List<ProductProvider.ProductProviderConfig> c)
        {
            configs = c;
        }
    }


    public class SendToProductionParams : MessageParametrs
    {
        public ProductType type;
        public GameObject provider;
        public float time;

        public SendToProductionParams(ProductType t, GameObject go, float tm)
        {
            type = t;
            provider = go;
            time = tm;
        }
    }    
}