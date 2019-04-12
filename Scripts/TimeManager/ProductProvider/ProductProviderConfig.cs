using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager.Product;

namespace TimeManager.ProductProvider
{
    [CreateAssetMenu(fileName = "NewProductProvider", menuName = "Product Provider")]
    public class ProductProviderConfig : ScriptableObject
    {
        public ProductType product;
        public float production_time;
    }
}