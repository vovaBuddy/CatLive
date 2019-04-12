using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using TimeManager.Product;

namespace TimeManager.FastSaleUnitAPI
{
    public static class Messages
    {
        public const string FAST_SALE_PRODUCT = "TimeManager.FastSaleUnitAPI.FAST_SALE_PRODUCT";
    }

    public class ProductParams : MessageParametrs
    {
        public ProductType product;
        public GameObject pr_unit;

        public ProductParams(ProductType pr, GameObject go)
        {
            product = pr;
            pr_unit = go;
        }
    }
}