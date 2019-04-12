using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaga.MessageBus;
using Yaga;
using UnityEngine.UI;

namespace TimeManager.Level
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    class CoinsView : ExtendedBehaviour
    {
        public Text coins;
        int coins_value = 0;

        [Subscribe(CustomerAPI.Messages.ADD_MONEY)]
        public void add_money(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<CustomerAPI.AddMoneyParametrs>(msg.parametrs);

            coins_value += (int)param.money;

            coins.text = coins_value.ToString();
        }

        [Subscribe(CustomerAPI.Messages.SUCCESS_CUSTOMER)]
        public void SuccessCust(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<CustomerAPI.NeedProductParametrs>(msg.parametrs);

            coins_value += (int)param.money;

            coins.text = coins_value.ToString();
        }
    }
}
