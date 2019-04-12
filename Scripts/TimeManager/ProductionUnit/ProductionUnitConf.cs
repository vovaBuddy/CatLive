using System.Collections;
using System.Collections.Generic;
using TimeManager.Product;
using UnityEngine;

namespace TimeManager.ProductionUnit
{
    [CreateAssetMenu(fileName = "NewProductionUnitConf", menuName = "Production Unit")]
    public class ProductionUnitConf : ScriptableObject
    {
        public List<ProductType> recipe;
        public ProductType result_product;
        public float process_time;
    }
}