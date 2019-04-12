using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using PetsScannInfo;
using Yaga.Storage;
using UnityEngine.SceneManagement;
using System;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class MainMenuController : ExtendedBehaviour
    {
        public CatShowController cat_show_controller;

        [Serializable]
        public class ShowedItems
        {
            public bool show_scan_btn;
            public bool show_game_btn;
            public bool show_task_btn;
            public bool show_catshow_btn;
            public bool show_money;
            public bool show_hearts;
            public bool show_stars;

            public bool show_socail;
            public bool show_review;
            public bool show_prize;

            public ShowedItems()
            {
                show_scan_btn = false;
                show_game_btn = false;
                show_task_btn = true;
                show_catshow_btn = false;
                show_money = false;
                show_hearts = false;
                show_stars = true;
                show_socail = false;
                show_review = false;
                show_prize = false;
            }
        }

        public StorableData<ShowedItems> showed_items;

        [Subscribe(MainMenuMessageType.TOGGLE_MAIN_MENU_REVIEW_BTN)]
        public void toggleReviewBtn(Message msg)
        {
            showed_items.content.show_review = !showed_items.content.show_review;
            showed_items.Store();

            MessageBus.Instance.SendMessage(MainMenuMessageType.SHOW_MAIN_MENU);
        }

        [Subscribe(MainMenuMessageType.TOGGLE_MAIN_MENU_SOCIAL_BTN)]
        public void toggleSocialBtn(Message msg)
        {
            showed_items.content.show_socail = !showed_items.content.show_socail;
            showed_items.Store();

            MessageBus.Instance.SendMessage(MainMenuMessageType.SHOW_MAIN_MENU);
        }

        [Subscribe(MainMenuMessageType.TOGGLE_MAIN_MENU_PRIZE_BTN)]
        public void togglePrizeBtn(Message msg)
        {
            showed_items.content.show_prize = !showed_items.content.show_prize;
            showed_items.Store();

            MessageBus.Instance.SendMessage(MainMenuMessageType.SHOW_MAIN_MENU);
        }

        [Subscribe(MainMenuMessageType.SHOW_MAIN_MENU_SCAN_BTN)]
        public void ShowScanBtn(Message msg)
        {
            showed_items.content.show_scan_btn = true;
            showed_items.Store();

            MessageBus.Instance.SendMessage(MainMenuMessageType.SHOW_MAIN_MENU);
        }

        [Subscribe(MainMenuMessageType.SHOW_MAIN_MENU_GAME_BTN)]
        public void ShowGameBtn(Message msg)
        {
            showed_items.content.show_game_btn = true;
            showed_items.content.show_money = true;
            showed_items.content.show_hearts = true;
            showed_items.Store();

            MessageBus.Instance.SendMessage(MainMenuMessageType.SHOW_MAIN_MENU);
        }

        [Subscribe(MainMenuMessageType.SHOW_MAIN_MENU_CATSHOW)]
        public void ShowCatShowBtn(Message msg)
        {
            showed_items.content.show_catshow_btn = true;
            showed_items.Store();

            MessageBus.Instance.SendMessage(MainMenuMessageType.SHOW_MAIN_MENU);
        }

        public void OpenScan()
        {
            ArrowController.Instance.arrow_scan_btn.SetActive(false);
            MessageBus.Instance.SendMessage(MainMenuMessageType.SHOW_SCAN_MENU);
        }

        public void OpenScanning()
        {
            DataController.instance.other_storage["selfie"] = false;
            MessageBus.Instance.SendMessage(new Message(
                Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
                new Common.LoadingScreen.API.SceneNameParametr(
                    Common.LoadingScreen.API.SceneNames.SCANNING, false)));
        }

        public void OpenSelfie()
        {
            DataController.instance.other_storage["selfie"] = true;
            MessageBus.Instance.SendMessage(new Message(
                Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
                new Common.LoadingScreen.API.SceneNameParametr(
                    Common.LoadingScreen.API.SceneNames.SCANNING, false)));
        }

        public void sendMsg(string msg)
        {
            MessageBus.Instance.SendMessage(msg);
        }

        // Use this for initialization
        override public void ExtendedStart()
        {
            DataController.instance.advEntity.sequential_games = 0;
            showed_items = new StorableData<ShowedItems>("main_menu_showed_items");

            GameStatistics.instance.SendStat("main_scene_loaded", 0);
        }


        int init_after_updates = 2;
        public override void ExtendedUpdate()
        {
            if(init_after_updates >= 0)
                init_after_updates -= 1;
            if (init_after_updates < 0)
            {
                if (DataController.instance.tasks_storage.content.ContainsKey("catshow_scene") &&
                    (bool)DataController.instance.tasks_storage.content["catshow_scene"] == true)
                {
                    cat_show_controller.OpenCatShow();
                }
            }

        }
    }
}
