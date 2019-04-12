using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using System;
using Yaga.Storage;
using UnityEngine.UI;
using UnityEngine.Analytics;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class CatShowController : ExtendedBehaviour
    {
        public GameObject main_btn;
        public GameObject selfie_btn;
        public GameObject shop_btn;

        public GameObject rating_panel;
        public GameObject raitprizes_panel;
        public GameObject scoreprizes_panel;

        TutorCatShow tutorCatShow;
        public Text cat_show_timer;

        public void OpenRating()
        {
            rating_panel.SetActive(true);
            raitprizes_panel.SetActive(false);
            scoreprizes_panel.SetActive(false);
        }

        public void OpenRatingPrizes()
        {
            rating_panel.SetActive(false);
            raitprizes_panel.SetActive(true);
            scoreprizes_panel.SetActive(false);
        }

        public void OpenScorePrizes()
        {
            rating_panel.SetActive(false);
            raitprizes_panel.SetActive(false);
            scoreprizes_panel.SetActive(true);
        }


        public GameObject rait_items_container;

        ServeredData scoreboard_data;

        [Serializable]
        public class ShowedItems
        {
            public bool show_selfie_btn;
            public bool show_game_btn;
            public bool show_shop_btn;
            public bool show_main_btn;

            public ShowedItems()
            {
                show_selfie_btn = false;
                show_shop_btn = false;
                show_game_btn = true;
                show_main_btn = false;
            }
        }

        public StorableData<ShowedItems> showed_items;


        [Subscribe(CatShow.CatShowMessageType.CAT_SHOW_MAIN_BTN)]
        public void ShowMainBtn(Message msg)
        {
            showed_items.content.show_main_btn = true;
            showed_items.Store();
        }

        [Subscribe(CatShow.CatShowMessageType.CAT_SHOW_SELFIE_BTN)]
        public void ShowSelfieBtn(Message msg)
        {
            showed_items.content.show_selfie_btn = true;
            showed_items.Store();
        }

        [Subscribe(CatShow.CatShowMessageType.CAT_SHOW_SHOP_BTN)]
        public void ShowShopBtn(Message msg)
        {
            showed_items.content.show_shop_btn = true;
            showed_items.Store();
        }

        public void CloseCatShow()
        {
            MessageBus.Instance.SendMessage(MainMenuMessageType.CLOSE_CAT_SHOW);
            MessageBus.Instance.SendMessage(MainMenuMessageType.SHOW_MAIN_MENU);

            DataController.instance.tasks_storage.content["catshow_scene"] = false;
            DataController.instance.tasks_storage.content["mainhome_scene"] = true;
            DataController.instance.tasks_storage.Store();
        }

        public void OpenCatShow()
        {
            selfie_btn.SetActive(showed_items.content.show_selfie_btn);
            main_btn.SetActive(showed_items.content.show_main_btn);
            shop_btn.SetActive(showed_items.content.show_shop_btn);
            MainScene.ArrowController.Instance.arrow_cat_show.SetActive(false);

            MessageBus.Instance.SendMessage(MainMenuMessageType.OPEN_CAT_SHOW);
            MessageBus.Instance.SendMessage(MainMenuMessageType.CLOSE_MAIN_MENU);

            Message msg = new Message();
            UpdateInt param = new UpdateInt(0);            

            param.value = DataController.instance.catsPurse.Coins;
            msg.Type = MainMenuMessageType.UPDATE_CATSHOW_COINS;
            msg.parametrs = param;
            MessageBus.Instance.SendMessage(msg);

            param.value = DataController.instance.catsPurse.Energy;
            msg.Type = MainMenuMessageType.UPDATE_ENERGY;
            msg.parametrs = param;
            MessageBus.Instance.SendMessage(msg);

            param.value = DataController.instance.catsPurse.Beauty;
            msg.Type = MainMenuMessageType.UPDATE_BEAUTY;
            msg.parametrs = param;
            MessageBus.Instance.SendMessage(msg);

            DataController.instance.tasks_storage.content["catshow_scene"] = true;
            DataController.instance.tasks_storage.content["mainhome_scene"] = false;
            DataController.instance.tasks_storage.Store();

            CatsMoveController.GetController().main_cat.GetComponent<RenderingOrder>().set_order_value(450);
        }

        [Subscribe(CatShow.CatShowMessageType.ADD_RAIT_ITEM)]
        public void AddRaitItem(Message msg)
        {
            object[] param = (object[])Yaga.Helpers.CastHelper.Cast<CommonMessageParametr>(msg.parametrs).obj;
            var go = (GameObject)Instantiate(Resources.Load("Main/RaitItem"));            
            var item = go.GetComponent<RaitItem>();
            item.scores.text = ((int)param[0]).ToString();
            item.id.text = (string)param[1];
            item.place.text = ((int)param[2]).ToString();
            go.transform.parent = rait_items_container.transform;
            go.transform.localScale = new Vector3(1, 1, 1);
        }

        [Subscribe(CatShow.CatShowMessageType.STARTED_CAT_SHOW_GAME)]
        public void IncrPlayCount(Message msg)
        {
            tutorCatShow.data.content.play_count++;
            tutorCatShow.data.Store();

            Analytics.CustomEvent("tutor_cat_show", new Dictionary<string, object>
            {
                { "tutor_cat_show_count",  tutorCatShow.data.content.play_count}
            });
        }

        // Use this for initialization
        public override void ExtendedStart()
        {
            tutorCatShow = new TutorCatShow();

            showed_items = new StorableData<ShowedItems>("CatShowShowedItems");

            if (tutorCatShow.data.content.done)
            {
                tutorCatShow.end_catshow_timer.GetTimeToEndShow(
                (data) => {
                    end_cat_show_timer = data.data.time;
                },
                (data) => {
                    end_cat_show_timer = 24 * 60 * 60;
                });

                scoreboard_data = new ServeredData();
                scoreboard_data.GetScoreBoard((data) =>
                {
                    int place = data.data.place > 5 ? data.data.place - 4 : 1;

                    for (int i = data.data.data.Count - 1; i >= 0; --i)
                    {
                        var d = data.data.data[i];
                        object[] objs = new object[3];
                        objs[0] = d.score;
                        objs[1] = d.id.Length >= 8 ? d.id.Substring(0, 7) : d.id;
                        objs[2] = place;
                        place++;

                        Message msg = new Message();
                        msg.parametrs = new CommonMessageParametr(objs);
                        msg.Type = CatShow.CatShowMessageType.ADD_RAIT_ITEM;
                        MessageBus.Instance.SendMessage(msg, true);
                    }
                },
                () =>
                {
                    Debug.Log("ERROR");
                });
            }
            else
            {
                tutorCatShow.UpdateValue(DataController.instance.catsPurse.Beauty);

                var time = 24 * 60 * 60;

                if (tutorCatShow.data.content.play_count == 0)
                {
                    end_cat_show_timer = time;
                    tutorCatShow.end_catshow_timer.SetTime("tutor_catshow", time);
                }
                else
                {
                    tutorCatShow.end_catshow_timer.GetTime("tutor_catshow",
                    (data) => {
                        end_cat_show_timer = data.data.time;
                    },
                    (data) => {
                        end_cat_show_timer = time;
                        tutorCatShow.end_catshow_timer.SetTime("tutor_catshow", time);
                    });
                }

                tutorCatShow.GetTutorSCoreBoard();
            }
        }

        // Update is called once per frame

        int end_cat_show_timer = 24 * 60 * 60;
        float tick = 1.0f;
        public override void ExtendedUpdate()
        {
            tick -= Time.deltaTime;
            if (tick < 0)
            {
                tick = 1.0f;

                end_cat_show_timer -= 1;
                if(end_cat_show_timer > 24 * 3600)
                {
                    cat_show_timer.text = TextManager.getText("cs_catshow_end_show_text")
                        + end_cat_show_timer / 86400 + TextManager.getText("cs_catshow_end_show_day_text")
                        + (end_cat_show_timer % 86400) / 3600 + TextManager.getText("cs_catshow_end_show_hours_text")
                        + ((end_cat_show_timer % 86400) % 3600) / 60 + TextManager.getText("cs_catshow_end_show_minutes_text")
                        + (((end_cat_show_timer % 86400) % 3600) % 60) + TextManager.getText("cs_catshow_end_show_seconds_text");
                }
                else if (end_cat_show_timer > 3600)
                {
                    cat_show_timer.text = TextManager.getText("cs_catshow_end_show_text")
                        + end_cat_show_timer / 3600 + TextManager.getText("cs_catshow_end_show_hours_text")
                        + (end_cat_show_timer % 3600) / 60 + TextManager.getText("cs_catshow_end_show_minutes_text")
                        + ((end_cat_show_timer % 3600) % 60) + TextManager.getText("cs_catshow_end_show_seconds_text");
                } 
                else if(end_cat_show_timer > 60)
                {
                    cat_show_timer.text = TextManager.getText("cs_catshow_end_show_text")
                        + end_cat_show_timer / 60 + TextManager.getText("cs_catshow_end_show_minutes_text")
                        + ((end_cat_show_timer % 3600) % 60) + TextManager.getText("cs_catshow_end_show_seconds_text");
                }
                else
                {
                    cat_show_timer.text = TextManager.getText("cs_catshow_end_show_text")
                        + end_cat_show_timer + TextManager.getText("cs_catshow_end_show_seconds_text");
                }
            }

            if(end_cat_show_timer < 0 &&
               !tutorCatShow.data.content.done && !DialogController.GetController().DialogWindow.activeSelf)
            {
                tutorCatShow.data.content.done = true;
                tutorCatShow.data.Store();

                DataController.instance.catsPurse.Beauty = 0;

                ServeredData.PrizeAnswer answ = new ServeredData.PrizeAnswer();
                answ.data = new ServeredData.PrizeAnswer.Data();
                answ.data.cnt = 100;
                answ.data.place = tutorCatShow.data.content.cur_place;
                answ.data.value = tutorCatShow.data.content.cur_value;

                PrizeController.GetController().set_value = true;
                PrizeController.GetController().answer = answ;
            }

        }
    }
}