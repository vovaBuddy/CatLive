using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaga;
using TimeManager.Product;
using TimeManager.Level;


namespace TimeManager.LevelAPI
{
    public static class Messages
    {
        public const string UPDATE_GOAL_VALUE = "TimeManager.LevelAPI.UPDATE_GOAL_VALUE";
        public const string DONE_GOAL = "TimeManager.LevelAPI.DONE_GOAL";
        public const string TICK = "TimeManager.LevelAPI.TICK";
        public const string CUSTOMER_GONE = "TimeManager.LevelAPI.CUSTOMER_GONE";
        public const string ADD_TIME = "TimeManager.LevelAPI.ADD_TIME";
        public const string ADD_CUSTOMERS = "TimeManager.LevelAPI.ADD_CUSTOMERS";
        public const string LOSE = "TimeManager.LevelAPI.LOSE";
    }

    public class GoalParams : MessageParametrs
    {
        public NonProductsGoal goal;
        public ProductType product;
        public GoalType goal_type;
        public bool success;
        public int value;

        public GoalParams(GoalType type, bool suc, ProductType p, NonProductsGoal g, int val)
        {
            product = p;
            goal = g;
            goal_type = type;
            success = suc;
            value = val;
        }
    }

    public class CustomerGoneParametrs : MessageParametrs
    {
        public int value;

        public CustomerGoneParametrs(int v)
        {
            value = v;
        }
    }

    public class TickParametrs : MessageParametrs
    {
        public float value;
        public TickParametrs(float t)
        {
            value = t;
        }
    }


}
