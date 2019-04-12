using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaga;
using TimeManager.Product;
using UnityEngine;

namespace TimeManager.ProductionUnitAPI
{
    public class Messages
    {
        public const string NEED_INGREDIENT = "TimeManager.ProductionUnitAPI.NEED_INGREDIENT";
        public const string INIT_PRODUCTION_UNITS = "TimeManager.ProductionUnitAPI.INIT_PRODUCTION_UNITS";
    }

    public class InitPUParams : MessageParametrs
    {
        public List<ProductionUnit.ProductionUnitConf> configs;

        public InitPUParams(List<ProductionUnit.ProductionUnitConf> c)
        {
            configs = c;
        }
    }

    public class NeedIngredientParams : MessageParametrs
    {
        public ProductType ingredient;
        public GameObject prod_unit;

        public NeedIngredientParams(ProductType ingr, GameObject go)
        {
            ingredient = ingr;
            prod_unit = go;
        }
    }

}
