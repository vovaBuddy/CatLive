using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager.ProductionUnit;
using TimeManager.ProductProvider;
using TimeManager.ProductionField;
using TimeManager.Tutor;
using TimeManager.Customer;

namespace TimeManager.Level
{
    public enum Restriction
    {
        TIME,
        CUSTOMERS
    }

    public enum GoalType
    {
        PRODUCTS,
        NON_PRODUCTS
    }

    public enum NonProductsGoal
    {
        MONEY,
        SUCCESS_CUSTOMERS,
        AVOID_BAD_CUSTOMERS,
        NON
    }




    [CreateAssetMenu(fileName = "NewLevel", menuName = "Level")]
    public class LevelConfig : ScriptableObject
    {
        [Header("Description")]
        public int id;
        public string description;
        public int level_time_seconds;

        [Space(5)]
        [Header("Tutors")]
        public List<TutorItem> tutors;

        [Space(5)]
        [Header("Production")]
        public List<ProductProviderConfig> product_providers;
        public List<ProductionUnitConf> production_units;
        public List<ProductionFieldConfig> production_fields;

        [Space(5)]
        [Header("Consumers")]
        public bool randomize;
        public List<CustomerConfig> customers;
        public int simultaneously_customers_amount;
        public float wait_time_until_next_min;
        public float wait_time_until_next_max;

        [Space(5)]
        [Header("Goals")]
        public Restriction restriction;

        [Space(5)]
        public List<NonProductGoal> non_product_goals;

        [Space(5)]
        public List<QuantitativeGoal> quantitative_goals;
    }

    [System.Serializable]
    public class NonProductGoal
    {
        public NonProductsGoal goal;
        public int value;
    }


    [System.Serializable]
    public class QuantitativeGoal
    {
        public Product.ProductType product;
        public int amount;
    }

}