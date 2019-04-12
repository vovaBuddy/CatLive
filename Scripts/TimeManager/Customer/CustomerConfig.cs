using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TimeManager.Product;

namespace TimeManager.Customer
{
    [CreateAssetMenu(fileName = "NewCustomerConfig", menuName = "Customer")]
    public class CustomerConfig : ScriptableObject
    {
        public ProductType need_product;
        public float wait_time;
        public float cash;
    }
}
