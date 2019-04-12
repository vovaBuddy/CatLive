using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using UnityEngine.UI;

namespace CatShow
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class CatShowPanelView : ExtendedBehaviour
    {
        public GameObject cat_show;

        public Text header_text;
        public Text game_info_text;
        public Text game_btn_text;
        public Text rating_btn_text;
        public Text rating_prize_btn_text;
        public Text ponts_prize_btn_text;
        public Text pnts_points_text;
        public Text pnts_prize_text;
        public Text rt_place_text;
        public Text rt_name_text;
        public Text rt_points_text;
        public Text rtp_place_text;
        public Text rtp_prize_text;

        [Subscribe(CatShowMessageType.OPEN_CAT_SHOW)]
        public void Open(Message msg)
        {
            cat_show.SetActive(true);
        }

        [Subscribe(CatShowMessageType.CLOSE_CAT_SHOW)]
        public void Close(Message msg)
        {
            cat_show.SetActive(false);
        }


        // Use this for initialization
        override public void ExtendedStart()
        {
            header_text.text = TextManager.getText("cs_catshow_header_text");
            game_info_text.text = TextManager.getText("cs_catshow_game_info_text");
            game_btn_text.text = TextManager.getText("cs_catshow_game_btn_text");
            rating_btn_text.text = TextManager.getText("cs_catshow_rating_btn_text");
            rating_prize_btn_text.text = TextManager.getText("cs_catshow_rating_prize_btn_text");
            ponts_prize_btn_text.text = TextManager.getText("cs_catshow_ponts_prize_btn_text");
            pnts_points_text.text = TextManager.getText("cs_catshow_pnts_points_text");
            pnts_prize_text.text = TextManager.getText("cs_catshow_pnts_prize_text");
            rt_place_text.text = TextManager.getText("cs_catshow_rt_place_text");
            rt_name_text.text = TextManager.getText("cs_catshow_rt_name_text");
            rt_points_text.text = TextManager.getText("cs_catshow_rt_points_text");
            rtp_place_text.text = TextManager.getText("cs_catshow_rtp_place_text");
            rtp_prize_text.text = TextManager.getText("cs_catshow_rtp_prize_text");

            cat_show.SetActive(false);
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {

        }
    }
}