using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

namespace TimeManager.Customer
{
    public class LeaveBadState : CustomerState
    {
        Customer customer;

        public LeaveBadState(Customer cust)
        {
            customer = cust;
        }

        public void EndState()
        {
        }

        public CustomerStates GetCurStateName()
        {
            return CustomerStates.LEAVE_BAD;
        }

        public void OnClick()
        {
        }

        public void StartState()
        {
            MessageBus.Instance.SendMessage(new Message(CustomerAPI.Messages.BAD_CUSTOMER,
                    new CustomerAPI.NeedProductParametrs(customer.need_product, customer.gameObject)));

            ResourcesController.get_instance().AddIndex(customer.cur_place_index);

            customer.ShowNeedArea(false);
            customer.transform.DOMoveX(customer.transform.position.x - 20.0f, 2.0f); //.onComplete = () => { customer.StartWait(); };
        }

        public bool GiveProduct()
        {
            return false;
        }

        public void Update()
        {
        }
    }
}
