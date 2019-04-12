using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeManager.Product;

namespace TimeManager.Level
{
    class LevelData
    {
        public float coins;
        public int success_customers;
        public int bad_customers;
        public Dictionary<ProductType, int> products_amount;

        public float time;
        public int remainig_customers_count;

        public LevelData(float lvl_time, int remainig)
        {
            coins = 0;
            success_customers = 0;
            bad_customers = 0;
            products_amount = new Dictionary<ProductType, int>();

            time = lvl_time;
            remainig_customers_count = remainig;
        }
    }
}
