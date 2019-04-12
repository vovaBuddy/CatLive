using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using UnityEngine.UI;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class MainMenuView : ExtendedBehaviour
    {
        public MainMenuController mainMenu;

        public Image logo;

        public GameObject main_menu;
        public GameObject coins_obj;
        public GameObject hearts_obj;
        public GameObject stars_obj;
        public GameObject game_btn;
        public GameObject scan_btn;
        public GameObject tasks_btn;
        public GameObject social_btn;
        public GameObject rate_btn;
        public GameObject prize_btn;
        public GameObject catshow_btn;

        public GameObject panel_coins;
        public GameObject panel_hearts;

        public GameObject panel_reward_hearts;
        public GameObject panel_reward_energy;
        public GameObject panel_reward_coins;

        public GameObject Middle;

        public Text coins;
        public Text hearts;
        public Text stars;
        public Text energy;
        public Text beauty;

        public Text hearts_header_text;
        public Text hearts_body_text;
        public Text hearts_add_btn_text;
        public Text coins_header_text;
        public Text coins_body_text;
        public Text coins_play_btn_text;
        public Text coins_bank_btn_text;
        public Text coins_add_btn_text;
        public Text energy_header_text;
        public Text energy_body_text;
        public Text stars_header_text;
        public Text stars_body_text;
        public Text stars_play_btn_text;
        public Text regard_coins_header_text;
        public Text regard_coins_btn_text;
        public Text regard_hearts_header_text;
        public Text regard_hearts_btn_text;

        public Text customize_tutor_text;

        public GameObject shop_indicator;

        [Subscribe(MainMenuMessageType.CLOSE_REWARD)]
        public void CloseRew(Message msg)
        {
            panel_reward_hearts.SetActive(false);
            panel_reward_energy.SetActive(false);
            panel_reward_coins.SetActive(false);
        }

        [Subscribe(MainMenuMessageType.REWARD_HEART)]
        public void rewHearts(Message msg)
        {
            panel_reward_hearts.SetActive(true);
        }

        [Subscribe(MainMenuMessageType.REWARD_ENERGY)]
        public void rewEnergy(Message msg)
        {
            panel_reward_energy.SetActive(true);
        }

        [Subscribe(MainMenuMessageType.REWARD_COINS)]
        public void rewCoins(Message msg)
        {
            panel_reward_coins.SetActive(true);
        }

        [Subscribe(MainMenuMessageType.INDICATOR_START)]
        public void ShowIndicator(Message msg)
        {
            var p = Yaga.Helpers.CastHelper.Cast<CommonMessageParametr>(msg.parametrs);
            shop_indicator.SetActive(true);
            shop_indicator.GetComponent<Animator>().SetBool("end", false);

            shop_indicator.transform.position = new Vector3(((Vector2)p.obj).x, ((Vector2)p.obj).y + 200, 0);

            shop_indicator.GetComponent<Animator>().Play("Shopping_open");
        }

        [Subscribe(MainMenuMessageType.INDICATOR_CLOSE)]
        public void CloseIndicator(Message msg)
        {
            //shop_indicator.SetActive(false);
            shop_indicator.GetComponent<Animator>().SetBool("end", true);
        }

        [Subscribe(MainMenuMessageType.NOTE_ENOUGH_MONEY)]
        public void NotEnoughMoney(Message msg)
        {
            GameStatistics.instance.SendStat("note_enough_money", 0);
            panel_coins.SetActive(true);
        }

        [Subscribe(MainMenuMessageType.NOTE_ENOUGH_STARS)]
        public void NotEnoughStars(Message msg)
        {            
            DataController.instance.gamesRecords.StarMinigameNeed = true;
        }

        [Subscribe(MainMenuMessageType.NOTE_ENOUGH_HEARTS)]
        public void NotEnoughHearts(Message msg)
        {
            panel_hearts.SetActive(true);
        }

        [Subscribe(MainMenuMessageType.CLOSE_POPUP, 
                MainMenuMessageType.OPEN_MINI_GAMES, 
                MainMenuMessageType.BUY_ENERGY,
                MainMenuMessageType.BUY_HEART)]
        public void ClosePopUp(Message msg)
        {
            panel_coins.SetActive(false);
            panel_hearts.SetActive(false);
        }

        //need add in close star panel
        public void NoNeedMinigameStar()
        {
            DataController.instance.gamesRecords.StarMinigameNeed = false;
        }


        [Subscribe(CatShow.CatShowMessageType.UPDATE_ENERGY)]
        public void UpdateEnergy(Message msg)
        {
            var param = CastHelper.Cast<UpdateInt>(msg.parametrs);

            energy.text = param.value.ToString();
        }

        [Subscribe(CatShow.CatShowMessageType.UPDATE_BEAUTY)]
        public void UpdateBeauty(Message msg)
        {
            var param = CastHelper.Cast<UpdateInt>(msg.parametrs);

            beauty.text = param.value.ToString();
        }

        [Subscribe(MainMenuMessageType.UPDATE_STARS)]
        public void UpdateStars(Message msg)
        {
            var param = CastHelper.Cast<UpdateInt>(msg.parametrs);

            stars.text = param.value.ToString();
        }

        [Subscribe(MainMenuMessageType.SHOW_MAIN_MENU)]
        public void ShowMain(Message msg)
        {
            //main_menu.SetActive(true);

            game_btn.SetActive(mainMenu.showed_items.content.show_game_btn);
            scan_btn.SetActive(mainMenu.showed_items.content.show_scan_btn);
            tasks_btn.SetActive(mainMenu.showed_items.content.show_task_btn);

            Middle.SetActive(true);

            social_btn.SetActive(mainMenu.showed_items.content.show_socail);
            rate_btn.SetActive(mainMenu.showed_items.content.show_review);
            //prize_btn.SetActive(mainMenu.showed_items.content.show_prize);
            catshow_btn.SetActive(mainMenu.showed_items.content.show_catshow_btn);

            coins_obj.SetActive(mainMenu.showed_items.content.show_money);
            hearts_obj.SetActive(mainMenu.showed_items.content.show_hearts);
            stars_obj.SetActive(mainMenu.showed_items.content.show_stars);
        }

        [Subscribe(MainMenuMessageType.CLOSE_MAIN_FOOTER)]
        public void CloseMainFooter(Message msg)
        {
            game_btn.SetActive(false);
            scan_btn.SetActive(false);
            tasks_btn.SetActive(false);
        }

        [Subscribe(MainMenuMessageType.CLOSE_MAIN_MENU)]
        public void CloseMain(Message msg)
        {
            game_btn.SetActive(false);
            scan_btn.SetActive(false);
            tasks_btn.SetActive(false);

            Middle.SetActive(false);

            coins_obj.SetActive(false);
            hearts_obj.SetActive(false);
            stars_obj.SetActive(false);
        }
        // Use this for initialization
        override public void ExtendedStart()
        {
            coins.text = DataController.instance.catsPurse.Coins.ToString();
            hearts.text = DataController.instance.catsPurse.Hearts.ToString();
            stars.text = DataController.instance.catsPurse.Stars.ToString();

            hearts_header_text.text = TextManager.getText("mm_hearts_header_text");
            hearts_body_text.text = TextManager.getText("mm_hearts_body_text");
            hearts_add_btn_text.text = TextManager.getText("mm_hearts_add_btn_text");
            coins_header_text.text = TextManager.getText("mm_coins_header_text");
            coins_body_text.text = TextManager.getText("mm_coins_body_text");
            coins_play_btn_text.text = TextManager.getText("mm_coins_play_btn_text");
            coins_bank_btn_text.text = TextManager.getText("mm_coins_bank_btn_text");
            coins_add_btn_text.text = TextManager.getText("mm_coins_add_btn_text");
            energy_header_text.text = TextManager.getText("mm_energy_header_text");
            energy_body_text.text = TextManager.getText("mm_energy_body_text");
            stars_header_text.text = TextManager.getText("mm_stars_header_text");
            stars_body_text.text = TextManager.getText("mm_stars_body_text");
            stars_play_btn_text.text = TextManager.getText("mm_stars_play_btn_text");
            regard_coins_header_text.text = TextManager.getText("mm_regard_coins_header_text");
            regard_coins_btn_text.text = TextManager.getText("mm_regard_coins_btn_text");
            regard_hearts_header_text.text = TextManager.getText("mm_regard_hearts_header_text");
            regard_hearts_btn_text.text = TextManager.getText("mm_regard_hearts_btn_text");
            customize_tutor_text.text = TextManager.getText("mm_customize_tutor_text");

            //if (Application.systemLanguage == SystemLanguage.Russian)
            //{
            //    logo.sprite = Resources.Load<Sprite>("logo/Russian");
            //}
            //else
            //{
            //    logo.sprite = Resources.Load<Sprite>("logo/English");
            //}

            //main_menu.SetActive(false);
            ShowMain(new Message());
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {
            coins.text = DataController.instance.catsPurse.Coins.ToString();
            if (DataController.instance.catsPurse.InfinityHearts)
            {
                hearts.text = "8";
                hearts.gameObject.transform.localEulerAngles = new Vector3(0, 0, 90);
            }
            else
            {
                hearts.text = DataController.instance.catsPurse.Hearts.ToString();
                hearts.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            stars.text = DataController.instance.catsPurse.Stars.ToString();
        }
    }
}
