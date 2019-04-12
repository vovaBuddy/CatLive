using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaga;
using Yaga.MessageBus;
using UnityEngine;
using UnityEngine.UI;
using TimeManager.Product;

namespace TimeManager.Level
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    class GoalItemView : ExtendedBehaviour
    {
        public Image goal_icon;
        public Text value;
        public GameObject success_icon;
        public GameObject lose_icon;
        public GameObject text_area;

        GoalType goal_type;
        ProductType type;
        NonProductsGoal non_product;
        int start_value;

        public void Init(GoalType goal_t, int v, ProductType t, NonProductsGoal g)
        {
            type = t;
            goal_type = goal_t;
            start_value = v;
            non_product = g;
            value.text = v.ToString();

            if(goal_t == GoalType.NON_PRODUCTS)
                goal_icon.sprite = ResourcesController.get_instance().product_resources.get_by_non_product_type(g);
            else 
                goal_icon.sprite = ResourcesController.get_instance().product_resources.get_big_by_type(t);
        }

        [Subscribe(LevelAPI.Messages.UPDATE_GOAL_VALUE)]
        public void Upd(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<LevelAPI.GoalParams>(msg.parametrs);

            if(param.goal_type == GoalType.NON_PRODUCTS && goal_type == GoalType.NON_PRODUCTS)
            {
                if (param.goal == NonProductsGoal.MONEY && non_product == NonProductsGoal.MONEY)
                {
                    value.text = (start_value - param.value > 0 ? start_value - param.value : 0).ToString();
                }
                if (param.goal == NonProductsGoal.AVOID_BAD_CUSTOMERS && non_product == NonProductsGoal.AVOID_BAD_CUSTOMERS)
                {
                    value.text = "0";
                }
                else if(param.goal == NonProductsGoal.SUCCESS_CUSTOMERS && non_product == NonProductsGoal.SUCCESS_CUSTOMERS)
                {
                    value.text = (start_value - param.value > 0 ? start_value - param.value : 0).ToString();
                }
            }
            else if((type == param.product && param.product != ProductType.NONE) || 
                (non_product == param.goal && param.goal != NonProductsGoal.NON))
            {
                value.text = (start_value - param.value > 0 ? start_value - param.value : 0).ToString();
            }

            if(type == param.product && param.goal == non_product && goal_type == param.goal_type && start_value - param.value <= 0)
            {
                param.success = true;
                Done(param);
            }
        }

        public void Done(LevelAPI.GoalParams param)
        {
            if (param.goal_type == GoalType.NON_PRODUCTS && (param.goal == NonProductsGoal.MONEY ||
                param.goal == NonProductsGoal.SUCCESS_CUSTOMERS))
            {
                text_area.SetActive(false);
                lose_icon.SetActive(!param.success);
                success_icon.SetActive(param.success);
            }
            else if(param.goal_type == GoalType.PRODUCTS)
            {
                text_area.SetActive(false);
                lose_icon.SetActive(!param.success);
                success_icon.SetActive(param.success);
            }
        }
    }
}
