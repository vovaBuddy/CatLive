using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

namespace TimeManager.Customer
{
    public class WaitState : CustomerState
    {
        Customer customer;

        float timer;

        Utilits.CustomerTimer timer_view;

        public WaitState(Customer cust, float t)
        {
            customer = cust;
            timer = t;
            timer_view = customer.entity.timer.GetComponent<Utilits.CustomerTimer>();
            timer_view.Init();
        }

        public void EndState()
        {
        }

        public CustomerStates GetCurStateName()
        {
            return CustomerStates.WAIT;
        }

        public void OnClick()
        {
            MessageBus.Instance.SendMessage(new Message(CustomerAPI.Messages.NEED_PRODUCT,
                new CustomerAPI.NeedProductParametrs(customer.need_product, customer.gameObject)));
        }

        public void StartState()
        {
            customer.ShowNeedArea(true);
            customer.entity.timer.SetActive(true);
            customer.entity.cost.text = "$" + (int)customer.cash;
            customer.entity.product_small_icon.GetComponent<SpriteRenderer>().sprite =
                ResourcesController.get_instance().product_resources.get_small_by_type(customer.need_product);

            MessageBus.Instance.SendMessage(new Message(CustomerAPI.Messages.CUSTOMER_START_WAITING,
                new CustomerAPI.WaitnigParams(customer.cur_place_index, customer.need_product)));

            customer.entity.customer_sprite.sprite =
                ResourcesController.get_instance().product_resources.get_customer_sprite(customer.type, 0);
        }

        public bool GiveProduct()
        {
            customer.LeaveSuccess();
            return true;
        }

        public void Update()
        {
            timer -= Time.deltaTime;

            timer_view.TickDown(customer.wait_time, Time.deltaTime);

            if (timer < 1 / 3.0f * customer.def_wait_time && customer.cat_success_state == 2)
            {
                customer.cat_success_state = 3;

                customer.entity.customer_sprite.sprite =
                    ResourcesController.get_instance().product_resources.get_customer_sprite(customer.type, customer.cat_success_state - 1);
            }
            else if (timer < 2 / 3.0f * customer.def_wait_time && customer.cat_success_state == 1)
            {
                customer.cat_success_state = 2;

                customer.entity.customer_sprite.sprite =
                    ResourcesController.get_instance().product_resources.get_customer_sprite(customer.type, customer.cat_success_state - 1);
            }

            if(timer <= 0 && !customer.successable)
            {
                customer.LeaveBad();
            }
        }
    }
}