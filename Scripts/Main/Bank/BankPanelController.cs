using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Yaga;
using Yaga.MessageBus;

namespace Main.Bank
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class BankPanelController : ExtendedBehaviour
    {
        BankBinder binder;

        public override void ExtendedStart()
        {
            binder = gameObject.transform.parent.Find("BankBinder").GetComponent<BankBinder>();
            Localizer.Localize(binder);
        }

        [Subscribe(API.BankAPI.OPEN)]
        public void Open(Message msg)
        {
            GameStatistics.instance.SendStat("btn_bank_clicked", 0);
            Analytics.CustomEvent("btn_bank_clicked");
            binder.bank_panel.SetActive(true);
        }

        [Subscribe(API.BankAPI.CLOSE)]
        public void Close(Message msg)
        {
            binder.bank_panel.SetActive(false);
        }
    }
}