using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;
using TimeManager.Level;

namespace TimeManager.ContinuePanel
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    class ContinuePanelController : ExtendedBehaviour
    {
        public class Messages
        {
            public const string INIT = "TimeManager.ContinuePanel.INIT";
            public const string OPEN = "TimeManager.ContinuePanel.OPEN";
        }

        public class InitParam : MessageParametrs
        {
            public Restriction restriction;
            public int cost;

            public InitParam(Restriction res, int coins)
            {
                restriction = res;
                cost = coins;
            }
        }

        public Text header;
        public Text btn_text;
        public GameObject customers_block;
        public GameObject time_block;
        public Text customers_text;
        public Text time_text;
        public GameObject panel;
        int price;

        [Subscribe(Messages.OPEN)]
        public void Open(Message msg)
        {
            panel.SetActive(true);
        }

        [Subscribe(Messages.INIT)]
        public void Init(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<InitParam>(msg.parametrs);

            customers_block.SetActive(param.restriction == Restriction.CUSTOMERS);
            time_block.SetActive(!customers_block.activeSelf);

            btn_text.text = param.cost.ToString();

            if(param.restriction == Restriction.TIME)
            {
                buy_action = () => { MessageBus.Instance.SendMessage(LevelAPI.Messages.ADD_TIME); };
            }
            else
            {
                buy_action = () => { MessageBus.Instance.SendMessage(LevelAPI.Messages.ADD_CUSTOMERS); };
            }

            price = param.cost;
        }

        public void Close()
        {
            panel.SetActive(false);
            MessageBus.Instance.SendMessage(LevelAPI.Messages.LOSE);
        }

        public Action buy_action;
        public void Buy()
        {
            if (DataController.instance.catsPurse.Coins >= price)
            {
                DataController.instance.catsPurse.Coins -= price;
                buy_action();
                panel.SetActive(false);
            }
        }
    }
}
