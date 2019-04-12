using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;

namespace TimeManager.Customer
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class CustomersController : ExtendedBehaviour
    {
        public List<Customer> customers;
        public List<Customer> cur_customers;

        bool randomize;
        int sim_customers_amount;
        float min_time_wait;
        float max_time_wait;

        public override void ExtendedStart()
        {
            customers = new List<Customer>();
        }

        [Subscribe(CustomerAPI.Messages.INIT_CUSTOMERS)]
        public void init(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<CustomerAPI.InitCustParams>(msg.parametrs);

            bool start_coroutine = customers.Count == 0;

            int i = 0;
            foreach (var conf in param.configs)
            {
                var go = Instantiate(ResourcesController.get_instance().prefabs_resources.Customer,
                    ResourcesController.get_instance().CustomersConteiner.transform);

                var customer = go.GetComponent<Customer>();

                customer.Init(conf.need_product, conf.wait_time, conf.cash, param.successable);

                customers.Add(go.GetComponent<Customer>());
                ++i;
            }

            sim_customers_amount = param.sim_customers_amount;
            min_time_wait = param.min_time_wait;
            max_time_wait = param.max_time_wait;
            randomize = param.randomize;

            if(start_coroutine)
                StartCoroutine(shadule_customers());
        }

        IEnumerator shadule_customers()
        {
            while (customers.Count > 0)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(min_time_wait, max_time_wait));

                for (int i = cur_customers.Count - 1; i >= 0; --i)
                {
                    if (cur_customers[i].isLeave())
                    {
                        cur_customers.RemoveAt(i);
                    }
                }

                if (cur_customers.Count < sim_customers_amount && customers.Count > 0)
                {
                    int index = randomize ? UnityEngine.Random.Range(0, customers.Count) : 0;
                    var new_customer = customers[index];
                    customers.RemoveAt(index);
                    cur_customers.Add(new_customer);
                    new_customer.Go();
                }
            }
        }
    }
}