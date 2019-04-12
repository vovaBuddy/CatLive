using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;
using Yaga;
using TimeManager.Tutor;

namespace TimeManager.Level
{
    [System.Serializable]
    public class ProductCustomerConfig
    {
        public Product.ProductType product;
        public Customer.CustomerConfig config;
    }

    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class LevelsController : ExtendedBehaviour
    {
        public List<LevelConfig> level_confs;
        Level cur_level;
        LevelData data;
        public LevelConfig cur_level_conf;

        int continue_price = 100;

        public List<ProductCustomerConfig> productCustomerConfigs;
        public Customer.CustomerConfig FindCustomerConfig(Product.ProductType type)
        {
            foreach(var c in productCustomerConfigs)
            {
                if (c.product == type)
                    return c.config;
            }

            return null;
        }

        public int level_id;

        // Use this for initialization
        public override void ExtendedStart()
        {
            level_id = StarTasksController.instance.get_cur_index();
            //level_confs = Yaga.Helpers.ResourceHelper.FindAssetsByType<LevelConfig>();
            cur_level_conf = find_cur_level_conf();
            //StartCoroutine(init());
        }

        LevelConfig find_cur_level_conf()
        {
            foreach(var conf in level_confs)
            {
                if (conf.id == level_id)
                    return conf;
            }

            return null;
        }

        public IEnumerator Tick()
        {
            while (data.time > 0)
            {
                yield return new WaitForSeconds(1.0f);

                data.time -= 1;

                MessageBus.Instance.SendMessage(new Message(LevelAPI.Messages.TICK,
                    new LevelAPI.TickParametrs(data.time)));

                //Debug.Log("tick");
            }
        }

        [Subscribe(LevelAPI.Messages.ADD_TIME)]
        public void AddTime(Message msg)
        {
            data.time += 30;
            StartCoroutine(CheckWin());
            StartCoroutine(Tick());

            List<Customer.CustomerConfig> additional_customers = new List<Customer.CustomerConfig>();
            data.remainig_customers_count += 3;

            if (cur_level_conf.non_product_goals.Count > 0)
            {
                for (int i = 0; i < 3; ++i)
                    additional_customers.Add(cur_level_conf.customers[Random.Range(0, cur_level_conf.customers.Count)]);
            }

            MessageBus.Instance.SendMessage(new Message(CustomerAPI.Messages.INIT_CUSTOMERS,
                new CustomerAPI.InitCustParams(additional_customers, cur_level_conf.simultaneously_customers_amount,
                cur_level_conf.wait_time_until_next_min, cur_level_conf.wait_time_until_next_max, cur_level_conf.randomize, level_id == 0)));
        }

        [Subscribe(LevelAPI.Messages.ADD_CUSTOMERS)]
        public void AddCustomers(Message msg)
        {
            List<Customer.CustomerConfig> additional_customers = new List<Customer.CustomerConfig>();
            data.remainig_customers_count = 3;

            if (cur_level_conf.non_product_goals.Count > 0)
            {
                for(int i = 0; i < 3; ++i)
                    additional_customers.Add(cur_level_conf.customers[Random.Range(0, cur_level_conf.customers.Count)]);
            }
            else if(cur_level_conf.quantitative_goals.Count > 0)
            {
                foreach (var goal in cur_level_conf.quantitative_goals)
                {
                    if(data.products_amount.ContainsKey(goal.product))
                    {
                        for (int i = 0; i < Mathf.Max(0, goal.amount - data.products_amount[goal.product]); ++i)
                        {
                            if(additional_customers.Count < 3)
                            {
                                additional_customers.Add(FindCustomerConfig(goal.product));
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                if(additional_customers.Count < 3)
                {
                    int cnt = 3 - additional_customers.Count;
                    for (int i = 0; i < cnt; ++i)
                        additional_customers.Add(cur_level_conf.customers[Random.Range(0, cur_level_conf.customers.Count)]);
                }
            }

            MessageBus.Instance.SendMessage(new Message(CustomerAPI.Messages.INIT_CUSTOMERS,
                new CustomerAPI.InitCustParams(additional_customers, cur_level_conf.simultaneously_customers_amount,
                cur_level_conf.wait_time_until_next_min, cur_level_conf.wait_time_until_next_max, cur_level_conf.randomize, level_id == 0)));

            StartCoroutine(CheckWin());
        }

        public bool check_time()
        {
            return data.time > 0;
        }

        public IEnumerator init()
        {
            yield return new WaitForSeconds(0.05f);

            GameStatistics.instance.SendStat("start_stargame_tm",
                    StarTasksController.instance.get_cur_index());

            cur_level = new Level(level_id);
            data = new LevelData(cur_level_conf.level_time_seconds, cur_level_conf.customers.Count);

            MessageBus.Instance.SendMessage(new Message(ProductProviderAPI.Messages.INIT_PRODUCT_PROVIDERS,
                new ProductProviderAPI.InitPPParams(cur_level_conf.product_providers)));

            MessageBus.Instance.SendMessage(new Message(ProductionFieldAPI.Messages.INIT_PRODUCTION_FIELDS,
                new ProductionFieldAPI.InitPFParams(cur_level_conf.production_fields)));

            MessageBus.Instance.SendMessage(new Message(ProductionUnitAPI.Messages.INIT_PRODUCTION_UNITS,
                new ProductionUnitAPI.InitPUParams(cur_level_conf.production_units)));

            MessageBus.Instance.SendMessage(new Message(CustomerAPI.Messages.INIT_CUSTOMERS,
                new CustomerAPI.InitCustParams(cur_level_conf.customers, cur_level_conf.simultaneously_customers_amount,
                cur_level_conf.wait_time_until_next_min, cur_level_conf.wait_time_until_next_max, cur_level_conf.randomize, level_id == 0)));

            foreach (var goal in cur_level_conf.non_product_goals)
            {
                switch (goal.goal)
                {
                    case NonProductsGoal.MONEY:
                        cur_level.win_conditions.Add(() =>
                        {
                            return data.coins >= goal.value;
                        });
                        break;

                    case NonProductsGoal.SUCCESS_CUSTOMERS:
                        cur_level.win_conditions.Add(() =>
                        {
                            return data.success_customers >= goal.value;
                        });
                        break;

                    case NonProductsGoal.AVOID_BAD_CUSTOMERS:
                        cur_level.win_conditions.Add(() =>
                        {
                            return data.bad_customers == 0 && data.remainig_customers_count == 0;
                        });

                        cur_level.lose_conditions.Add(() =>
                        {
                            return data.bad_customers > 0;
                        });
                        break;
                }

                var go = Instantiate(ResourcesController.get_instance().prefabs_resources.GoalItemView,
                    ResourcesController.get_instance().GoalItemViewsConteiner.transform);
                var view = go.GetComponent<GoalItemView>();
                view.Init(GoalType.NON_PRODUCTS, goal.value, Product.ProductType.NONE, goal.goal);
            }
            

            foreach(var goal in cur_level_conf.quantitative_goals)
            {
                cur_level.win_conditions.Add(() =>
                {
                    return data.products_amount.ContainsKey(goal.product) && data.products_amount[goal.product] >= goal.amount;
                });

                var go = Instantiate(ResourcesController.get_instance().prefabs_resources.GoalItemView,
                    ResourcesController.get_instance().GoalItemViewsConteiner.transform);
                GoalItemView view = go.GetComponent<GoalItemView>();
                view.Init(GoalType.PRODUCTS, goal.amount, goal.product, NonProductsGoal.NON);
            }

            MessageBus.Instance.SendMessage(new Message(ContinuePanel.ContinuePanelController.Messages.INIT,
                new ContinuePanel.ContinuePanelController.InitParam(cur_level_conf.restriction, continue_price)));

            switch (cur_level_conf.restriction)
            {
                case Restriction.CUSTOMERS:
                    var go = Instantiate(ResourcesController.get_instance().prefabs_resources.CustomersRestrictionView,
                        ResourcesController.get_instance().RestrictionViewConteiner.transform);
                    CustomersRestrictionView view = go.GetComponent<CustomersRestrictionView>();
                    view.Init(cur_level_conf.customers.Count);

                    cur_level.win_conditions.Add(() =>
                    {
                        return data.remainig_customers_count == 0;
                    });

                    cur_level.lose_conditions.Add(() =>
                    {
                        return data.remainig_customers_count == 0;
                    });

                    break;
                case Restriction.TIME:
                    Instantiate(ResourcesController.get_instance().prefabs_resources.TimerRestrictionView,
                        ResourcesController.get_instance().RestrictionViewConteiner.transform);

                    cur_level.win_conditions.Add(() =>
                    {
                        return data.time <= 0;
                    });
                    cur_level.lose_conditions.Add(() =>
                    {
                        return data.time <= 0;
                    });

                    break;
            }

            Instantiate(ResourcesController.get_instance().prefabs_resources.CoinsView,
                ResourcesController.get_instance().RestrictionViewConteiner.transform);

            if (cur_level_conf.tutors.Count > 0)
            {
                List<TutorItem> tutors = new List<TutorItem>();

                for(int i = 0; i < cur_level_conf.tutors.Count; ++i)
                {
                    tutors.Add(new TutorItem(cur_level_conf.tutors[i].type,
                        cur_level_conf.tutors[i].aim_ready,
                        cur_level_conf.tutors[i].aim,
                        new List<Product.ProductType>(cur_level_conf.tutors[i].product),
                        cur_level_conf.tutors[i].position_index,
                        cur_level_conf.tutors[i].text_id,
                        cur_level_conf.tutors[i].clicks,
                        cur_level_conf.tutors[i].name));
                }

                MessageBus.Instance.SendMessage(new Message(TutorAPI.Messages.INIT,
                    new TutorAPI.InitParams(new List<TutorItem>(tutors))));
            }


            StartCoroutine(Tick());
            StartCoroutine(CheckWin());
        }

        [Subscribe(CustomerAPI.Messages.SUCCESS_CUSTOMER, CustomerAPI.Messages.BAD_CUSTOMER)]
        public void uncount(Message msg)
        {
            data.remainig_customers_count--;
        }

        [Subscribe(CustomerAPI.Messages.BAD_CUSTOMER)]
        public void bad(Message msg)
        {
            data.bad_customers++;
        }

        [Subscribe(CustomerAPI.Messages.ADD_MONEY)]
        public void add_money(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<CustomerAPI.AddMoneyParametrs>(msg.parametrs);

            data.coins += param.money;

            MessageBus.Instance.SendMessage(new Message(LevelAPI.Messages.UPDATE_GOAL_VALUE,
                new LevelAPI.GoalParams(GoalType.NON_PRODUCTS, false, Product.ProductType.NONE, NonProductsGoal.MONEY, (int)data.coins)));
        }

        [Subscribe(CustomerAPI.Messages.SUCCESS_CUSTOMER)]
        public void SuccessCust(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<CustomerAPI.NeedProductParametrs>(msg.parametrs);

            data.success_customers++;

            data.coins += param.money;

            MessageBus.Instance.SendMessage(new Message(LevelAPI.Messages.UPDATE_GOAL_VALUE,
                new LevelAPI.GoalParams(GoalType.NON_PRODUCTS, false, Product.ProductType.NONE, NonProductsGoal.MONEY, (int)data.coins)));

            MessageBus.Instance.SendMessage(new Message(LevelAPI.Messages.UPDATE_GOAL_VALUE,
                new LevelAPI.GoalParams(GoalType.NON_PRODUCTS, false, Product.ProductType.NONE, 
                NonProductsGoal.SUCCESS_CUSTOMERS, data.success_customers)));

            if (!data.products_amount.ContainsKey(param.type))
            {
                data.products_amount[param.type] = 1;
            }
            else 
                data.products_amount[param.type] += 1;

            MessageBus.Instance.SendMessage(new Message(LevelAPI.Messages.UPDATE_GOAL_VALUE,
                new LevelAPI.GoalParams(GoalType.PRODUCTS, false, param.type,
                NonProductsGoal.NON, data.products_amount[param.type])));
        }

        [Subscribe(LevelAPI.Messages.LOSE)]
        public void Lose(Message msg)
        {
            GameStatistics.instance.SendStat("lose_stargame_tm",
                StarTasksController.instance.get_cur_index());

            if (!DataController.instance.catsPurse.InfinityHearts)
            {
                DataController.instance.catsPurse.Hearts -= 1;
            }
            DataController.instance.catsPurse.Coins += (int)data.coins;

            MessageBus.Instance.SendMessage(LosePanelAPI.Messages.SHOW);
        }

        Message empty_msg = new Message();
        public IEnumerator CheckWin()
        {
            
            while(true)
            {
                yield return new WaitForSeconds(1.0f);
                if (cur_level.check_win())
                {
                    DataController.instance.catsPurse.Stars += 1;
                    DataController.instance.catsPurse.Coins += (int)data.coins;
                    DataController.instance.world_state_data.last_game_event = GAME_EVENT.WON_MINIGAME;

                    GameStatistics.instance.SendStat("finish_stargame_tm",
                        StarTasksController.instance.get_cur_index());

                    StarTasksController.instance.DoneCurTask();
                    MessageBus.Instance.SendMessage(WinPanelAPI.Messages.SHOW);
                    break;
                }
                else if(cur_level.check_lose())
                {
                    if ((cur_level_conf.non_product_goals.Count > 0 &&
                        cur_level_conf.non_product_goals[0].goal == NonProductsGoal.AVOID_BAD_CUSTOMERS) ||

                        (DataController.instance.catsPurse.Coins < continue_price))
                    {
                        Lose(empty_msg);
                        break;
                    }
                    else
                    {
                        MessageBus.Instance.SendMessage(ContinuePanel.ContinuePanelController.Messages.OPEN);
                        break;
                    }
                }
            }
        }
    }
}