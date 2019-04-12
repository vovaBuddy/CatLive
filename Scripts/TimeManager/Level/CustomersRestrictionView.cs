using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;

namespace TimeManager.Level
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    class CustomersRestrictionView : ExtendedBehaviour
    {
        public Text amount;
        int customers;

        public void Init(int custs)
        {
            customers = custs;

            amount.text = customers.ToString();
        }

        [Subscribe(CustomerAPI.Messages.BAD_CUSTOMER, CustomerAPI.Messages.SUCCESS_CUSTOMER)]
        public void Gone(Message msg)
        {
            if(customers > 0)
                customers--;

            amount.text = customers.ToString();
        }

        [Subscribe(LevelAPI.Messages.ADD_CUSTOMERS)]
        public void AddCustomers(Message msg)
        {
            customers = 3;
            amount.text = customers.ToString();
        }
    }
}
