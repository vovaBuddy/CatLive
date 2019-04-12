using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TimeManager.Product;

namespace TimeManager.FastSale
{
    [System.Serializable]
    public class ProductPrice
    {
        public ProductType type;
        public int cost;
    }


    [CreateAssetMenu(fileName = "NewFastSaleConfig", menuName = "FastSaleConfig")]
    public class FastSaleUnitConfig : ScriptableObject
    {
        public List<ProductPrice> prices;

        public int get_price(ProductType type)
        {
            foreach(var pr in prices)
            {
                if(pr.type == type)
                {
                    return pr.cost;
                }
            }

            return 0;
        }
    }
}
