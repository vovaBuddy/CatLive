using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

namespace TimeManager.Customer
{
    public class LeaveSuccesState : CustomerState
    {
        Customer customer;

        public LeaveSuccesState(Customer cust)
        {
            customer = cust;
        }

        public void EndState()
        {
        }

        public CustomerStates GetCurStateName()
        {
            return CustomerStates.LEAVE_SUCCESS;
        }

        public void OnClick()
        {
        }


        public void StartState()
        {
            ResourcesController.get_instance().AddIndex(customer.cur_place_index);
            wait_anim = 0.4f;
        }

        public bool GiveProduct()
        {
            return false;
        }

        //wait while anim product
        float wait_anim;
        bool done = false;
        public void Update()
        {
            wait_anim -= Time.deltaTime;
            if (wait_anim <= 0 && !done)
            {
                customer.entity.customer_sprite.sprite =
                    ResourcesController.get_instance().product_resources.get_customer_sprite(customer.type, 0);

                MessageBus.Instance.SendMessage(new Message(CustomerAPI.Messages.SUCCESS_CUSTOMER,
                    new CustomerAPI.NeedProductParametrs(customer.need_product, customer.gameObject, customer.cash)));

                customer.ShowNeedArea(false);
                customer.transform.DOMoveX(customer.transform.position.x - 20.0f, 2.0f);//.onComplete = () => { customer.StartWait(); };

                done = true;
            }
        }
    }
}
