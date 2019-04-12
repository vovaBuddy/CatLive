using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;

namespace TimeManager.ProductionFieldAPI
{
    public static class Messages
    {
        public const string INIT_PRODUCTION_FIELDS = "TimeManager.ProductionFieldAPI.INIT_PRODUCTION_FIELDS";
        public const string READY = "TimeManager.ProductionFieldAPI.READY";
    }

    public class ReadyParams : MessageParametrs
    {
        public Product.ProductType type;

        public ReadyParams(Product.ProductType t)
        {
            type = t;
        }
    }


    public class InitPFParams : MessageParametrs
    {
        public List<ProductionField.ProductionFieldConfig> configs;

        public InitPFParams(List<ProductionField.ProductionFieldConfig> c)
        {
            configs = c;
        }
    }
}