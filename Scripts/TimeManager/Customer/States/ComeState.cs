using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TimeManager.Customer
{
    public class ComeState : CustomerState
    {
        Customer customer;

        public ComeState(Customer cust)
        {
            customer = cust;
        }

        public void EndState()
        {
        }

        public CustomerStates GetCurStateName()
        {
            return CustomerStates.COME;
        }

        public void OnClick()
        {
        }

        public void StartState()
        {
            customer.cur_place_index = ResourcesController.get_instance().get_customer_position_index();

            if(customer.cur_place_index == 1)
            {
                customer.Down.SetActive(false);
                customer.entity = customer.Up.GetComponent<CustomerEntity>();
            }
            else
            {
                customer.Up.SetActive(false);
                customer.entity = customer.Down.GetComponent<CustomerEntity>();
            }

            customer.entity.customer_sprite.sprite =
                ResourcesController.get_instance().product_resources.get_customer_sprite(customer.type, 0);

            customer.ShowNeedArea(false);

            customer.transform.DOMoveX(ResourcesController.get_instance().customer_slots[customer.cur_place_index].position.x
                , 2.0f).onComplete = () => { customer.StartWait(); };

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