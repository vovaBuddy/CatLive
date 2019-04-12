using Main.Bubble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class CustomizerView : ExtendedBehaviour
    {
        public GameObject customizer;
        public GameObject close_btn;
        public GameObject content;
        public Text header_text;

        private Message cur_prew_message;
        private Message cur_pre_dress_message;

        bool mission_customizer = false;
        bool predressed = false;

        public override void ExtendedStart()
        {
            header_text.text = TextManager.getText("mm_customizer_header_text");
        }
        
        [Subscribe(MainMenuMessageType.OPEN_CUSTOMIZER,
           MainMenuMessageType.OPEN_CUSTOMIZER_WITH_CLOSE)]
        public void Open(Message msg)
        {
            customizer.SetActive(true);

            if (msg.Type == MainMenuMessageType.OPEN_CUSTOMIZER_WITH_CLOSE)
            {
                close_btn.SetActive(true);
                mission_customizer = false;

                GameStatistics.instance.SendStat("open_customizer_by_user", 0);
            }
            else
            {
                close_btn.SetActive(false);
                mission_customizer = true;
                GameStatistics.instance.SendStat("open_customizer_by_mission", 0);
            }

            for (int i = 0; i < content.transform.childCount; ++i)
            {
                content.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        [Subscribe(MainMenuMessageType.OPEN_CUSTOMIZER,
           MainMenuMessageType.OPEN_CUSTOMIZER_WITH_CLOSE)]
        public void KitchenSet(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<CommonMessageParametr>(msg.parametrs);

            GameStatistics.instance.SendStat("open_customizer_" + (string)param.obj, 0);

            for (int i = 0; i < content.transform.childCount; ++i)
            {
                content.transform.GetChild(i).gameObject.SetActive(false);
            }

            var go = content.transform.Find((string)param.obj).gameObject;
            go.SetActive(true);

            if (msg.Type == MainMenuMessageType.OPEN_CUSTOMIZER)
            {
                Message m = new Message();
                m.Type = MainMenuMessageType.CHECK_BOUGHT;
                var p = new BuyItemParametr();
                p.type = go.transform.GetChild(0).GetComponent<ShopItem>().type;
                m.parametrs = p;
                MessageBus.Instance.SendMessage(m);
            }


            var width_item = content.transform.Find((string)param.obj).
                gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;

            var w = content.transform.Find((string)param.obj).
                gameObject.transform.childCount * (width_item + 20);

            content.GetComponent<RectTransform>().sizeDelta = new Vector2(w, 100);
        }

        [Subscribe(MainMenuMessageType.PREVIEW_ITEM)]
        public void PrewItem(Message msg)
        {
            cur_prew_message = msg;
        }

        [Subscribe(MainMenuMessageType.PRE_DRESS_ITEM)]
        public void PreDressItem(Message msg)
        {
            cur_pre_dress_message = msg;
            predressed = true;
        }
        [Subscribe(MainMenuMessageType.DRESS_ITEM, MainMenuMessageType.BOUGHT_ITEM)]
        public void NoPreDressItem(Message msg)
        {
            predressed = false;
        }

        [Subscribe(MainMenuMessageType.CLOSE_CUSTOMIZER, MainMenuMessageType.OPEN_MINI_GAMES)]
        public void Close(Message msg)
        {
            customizer.SetActive(false);

            if (msg.Type != MainMenuMessageType.OPEN_MINI_GAMES)
            {
                cur_prew_message.Type = MainMenuMessageType.TAKE_OFF_PREVIEW_ITEM;
                MessageBus.Instance.SendMessage(cur_prew_message);
            }
        }

        public void Apply()
        {
            GameStatistics.instance.SendStat("customizer_apply_pressed", 0);

            if (predressed)
            {
                cur_pre_dress_message.Type = MainMenuMessageType.DRESS_ITEM;
                MessageBus.Instance.SendMessage(cur_pre_dress_message);
                predressed = false;
            }

            MessageBus.Instance.SendMessage(MainMenuMessageType.CLOSE_CUSTOMIZER);
            MessageBus.Instance.SendMessage(MainMenuMessageType.SHOW_MAIN_MENU);
            MessageBus.Instance.SendMessage(MainMenuMessageType.CLOSE_CUSTOMIZER_MISSIONS);

            if (!mission_customizer)
            {
                MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                new BubbleCreateParametr(
                    CatsMoveController.GetController().main_cat, new List<string>()
                        {TextManager.getText("bubble_customize_" +
                                            UnityEngine.Random.Range(0,10).ToString()) }, 5)));
            }

            mission_customizer = false;
        }

        public void Close()
        {
            GameStatistics.instance.SendStat("customizer_close_pressed", 0);

            MessageBus.Instance.SendMessage(MainMenuMessageType.CLOSE_CUSTOMIZER);
            MessageBus.Instance.SendMessage(MainMenuMessageType.SHOW_MAIN_MENU);
            MessageBus.Instance.SendMessage(MainMenuMessageType.CLOSE_CUSTOMIZER_MISSIONS);

            cur_prew_message.Type = MainMenuMessageType.TAKE_OFF_PREVIEW_ITEM;
            MessageBus.Instance.SendMessage(cur_prew_message);

            cur_pre_dress_message.Type = MainMenuMessageType.TAKE_OFF_PREVIEW_ITEM;
            MessageBus.Instance.SendMessage(cur_pre_dress_message);
        }
    }
}
