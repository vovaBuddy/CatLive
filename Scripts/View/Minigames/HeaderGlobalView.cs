using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using System;

namespace Minigames
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class HeaderGlobalView : ExtendedBehaviour
    {
        public GameObject header;
        public Text coins;
        public Text hearts;
        public Text hearts_timer;
        public Text stars;

        int time;

        [Subscribe(MiniGameMessageType.OPEN_GLOBAL_HEADER)]
        public void OpenGlobal(Message msg)
        {
            var param = CastHelper.Cast<UpdatePurseParametrs>(msg.parametrs);

            header.SetActive(true);

            coins.text = param.coins.ToString();
            if(DataController.instance.catsPurse.InfinityHearts)
            {
                hearts.text = "8";
                hearts.gameObject.transform.localEulerAngles = new Vector3(0, 0, 90);

                DataController.instance.catsPurse.inf_h_timer.GetTime("infinity_heart", (answ) =>
                    {
                        time = answ.data.time;
                    }, (answ) => {});
            }
            else
                hearts.text = param.hearts.ToString();
            stars.text = param.stars.ToString();

        }

        // Use this for initialization
        override public void ExtendedStart()
        {
            header.SetActive(false);
            coins.text = DataController.instance.catsPurse.Coins.ToString();
            stars.text = DataController.instance.catsPurse.Stars.ToString();
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {
            hearts_timer.text = Helper.TextHelper.TimeFormatMinutes(time);
        }

        [Subscribe(MiniGameMessageType.INCREASE_STARS_VIEW)]
        public void IncreaseStars(Message msg)
        {
            stars.text = (1 + Int32.Parse(stars.text)).ToString();
        }
    }
}