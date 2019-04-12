using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;
using Helper;
using Yaga.Helpers;
using System;
using MainScene;

namespace Minigames
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class GoalsCoinsView : ExtendedBehaviour
    {
        public GameObject goals;

        public Text up_text;
        public Text down_text;

        public Text header_text;
        public Text footer_text;
        public Text btn_text;

        public Image up_img;
        public Image down_img;

        // Use this for initialization
        override public void ExtendedStart()
        {
            up_text.text = TextManager.getText("minigame_goals_up_text");
            down_text.text = TextManager.getText("minigame_goals_down_text");
            header_text.text = TextManager.getText("minigame_goals_header_text");
            footer_text.text = TextManager.getText("minigame_goals_footer_text");
            btn_text.text = TextManager.getText("minigame_goals_btn_text");


            if (!(DataController.instance.other_storage.ContainsKey("Continue") &&
                (bool)DataController.instance.other_storage["Continue"] == true) && 
                string.Equals(DataController.instance.other_storage["game_type"],
                GameType.COINS.ToString()))
            {
                goals.SetActive(true);
            }            
        }

        [Subscribe(MiniGameMessageType.CLOSE_GOAL_SCREEN)]
        public void Close(Message msg)
        {
            goals.SetActive(false);
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {

        }
    }
}