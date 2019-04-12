using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using TimeManager.Product;

namespace TimeManager.CustomerAPI
{
    public static class Messages
    {
        public const string NEED_PRODUCT = "TimeManager.CustomerAPI.NEED_PRODUCT";
        public const string INIT_CUSTOMERS = "TimeManager.CustomerAPI.INIT_CUSTOMERS";
        public const string SUCCESS_CUSTOMER = "TimeManager.CustomerAPI.SUCCESS_CUSTOMER";
        public const string BAD_CUSTOMER = "TimeManager.CustomerAPI.BAD_CUSTOMER";
        public const string ADD_MONEY = "TimeManager.CustomerAPI.ADD_MONEY";
        public const string CUSTOMER_START_WAITING = "CUSTOMER_START_WAITING";
    }

    public class WaitnigParams : MessageParametrs
    {
        public int index;
        public ProductType need_product;

        public WaitnigParams(int i, ProductType type)
        {
            index = i;
            need_product = type;
        }
    }


    public class InitCustParams : MessageParametrs
    {
        public List<Customer.CustomerConfig> configs;
        public int sim_customers_amount;
        public float min_time_wait;
        public float max_time_wait;
        public bool randomize;
        public bool successable;

        public InitCustParams(List<Customer.CustomerConfig> c, int count, float min, float max, bool rand, bool s)
        {
            configs = c;
            sim_customers_amount = count;
            min_time_wait = min;
            max_time_wait = max;
            randomize = rand;
            successable = s;
        }
    }

    public class AddMoneyParametrs : MessageParametrs
    {
        public float money;

        public AddMoneyParametrs(float m = 0)
        {
            money = m;
        }
    }

    public class NeedProductParametrs : MessageParametrs
    {
        public ProductType type;
        public GameObject customer;
        public float money;

        public NeedProductParametrs(ProductType t, GameObject go, float m = 0)
        {
            type = t;
            customer = go;
            money = m;
        }
    }
}