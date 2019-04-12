using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class CatShowView : ExtendedBehaviour
    {

        public GameObject cat_show;

        public Text coins;
        public Text energy;
        public Text beauty;
        public Text energy_timer;

        public Text coins_header_text;
        public Text coins_body_text;
        public Text coins_add_btn_text;
        public Text energy_header_text;
        public Text energy_body_text;
        public Text energy_add_btn_text;
        public Text energy_regard_header_text;
        public Text energy_regard_btn_text;

        public GameObject energy_obj;
        public GameObject coins_obj;

        public GameObject energy_panel;
        public GameObject coins_panel;

        public Button addCoins;
        public Button addEnergy;

        [Subscribe(CatShow.CatShowMessageType.NOTE_ENOUGH_ENERGY)]
        public void NotEnoughEnergy(Message msg)
        {
            energy_panel.SetActive(true);
        }

        [Subscribe(MainMenuMessageType.NOTE_ENOUGH_MONEY)]
        public void NotEnoughMoney(Message msg)
        {
            coins_panel.SetActive(true);
        }

        [Subscribe(MainMenuMessageType.CLOSE_POPUP, MainMenuMessageType.OPEN_MINI_GAMES)]
        public void ClosePopUp(Message msg)
        {
            energy_panel.SetActive(false);
            coins_panel.SetActive(false);
        }

        [Subscribe(MainMenuMessageType.OPEN_CAT_SHOW)]
        public void OpenShow(Message msg)
        {
            cat_show.SetActive(true);
            MainLocationOjects.instance.cat_show.SetActive(true);
        }

        [Subscribe(MainMenuMessageType.CLOSE_CAT_SHOW)]
        public void CloseShow(Message msg)
        {
            cat_show.SetActive(false);
            MainLocationOjects.instance.cat_show.SetActive(false);
        }
        
        [Subscribe(MainMenuMessageType.UPDATE_CATSHOW_COINS)]
        public void UpdateCoins(Message msg)
        {
            var param = CastHelper.Cast<UpdateInt>(msg.parametrs);

            coins.text = param.value.ToString();
        }

        [Subscribe(MainMenuMessageType.UPDATE_ENERGY)]
        public void UpdateEnergy(Message msg)
        {
            var param = CastHelper.Cast<UpdateInt>(msg.parametrs);

            energy.text = param.value.ToString();
        }

        [Subscribe(MainMenuMessageType.UPDATE_ENERGY_TIMER)]
        public void UpdateEnergyTimer(Message msg)
        {
            var param = CastHelper.Cast<UpdateInt>(msg.parametrs);

            energy_timer.text = param.value.ToString();
        }

        [Subscribe(MainMenuMessageType.UPDATE_BEAUTY)]
        public void UpdateBeauty(Message msg)
        {
            var param = CastHelper.Cast<UpdateInt>(msg.parametrs);

            beauty.text = param.value.ToString();
        }

        // Use this for initialization
        override public void ExtendedStart()
        {
            coins_header_text.text = TextManager.getText("cs_coins_header_text");
            coins_body_text.text = TextManager.getText("cs_coins_body_text");
            coins_add_btn_text.text = TextManager.getText("cs_coins_add_btn_text");
            energy_header_text.text = TextManager.getText("cs_energy_header_text");
            energy_body_text.text = TextManager.getText("cs_energy_body_text");
            energy_add_btn_text.text = TextManager.getText("cs_energy_add_btn_text");
            energy_regard_header_text.text = TextManager.getText("cs_energy_regard_header_text");
            energy_regard_btn_text.text = TextManager.getText("cs_energy_regard_btn_text");

            cat_show.SetActive(false);
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {
            coins.text = DataController.instance.catsPurse.Coins.ToString();
            energy.text = DataController.instance.catsPurse.Energy.ToString();
        }
    }
}
