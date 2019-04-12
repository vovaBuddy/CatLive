using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using UnityEngine.SceneManagement;

namespace Minigames
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class TaskResultView : ExtendedBehaviour
    {
        public GameObject panel_fail;
        public GameObject panel_lose;
        public GameObject panel_win;

        public GameObject panel_win_cont_btn;
        public Text panel_win_cont_btn_text;

        public Text win_coins;
        public Text fail_header_text;
        public Text fail_body_text;
        public Text fail_footer_text;
        public Text brokenheard_header_text;
        public Text brokenheard_body_text;
        public Text brokenheard_btn_text;
        public Text winstar_header_text;
        public Text winstar_body_text;

        public GameObject pick_up_btn;

        [Subscribe(MiniGameMessageType.LOADING_SCREEN_OPENED)]
        public void ShowBtn(Message msg)
        {
            pick_up_btn.SetActive(true);
        }

        [Subscribe(MiniGameMessageType.OPEN_FAIL_PANEL)]
        public void OpenFail(Message msg)
        {
            panel_fail.SetActive(true);
        }

        [Subscribe(MiniGameMessageType.OPEN_LOSE_PANEL)]
        public void OpenLose(Message msg)
        {
            panel_lose.SetActive(true);
        }

        [Subscribe(MiniGameMessageType.OPEN_WIN_PANEL)]
        public void OpenWin(Message msg)
        {
            var param = CastHelper.Cast<CommonMessageParametr>(msg.parametrs);
            var dic = param.obj as Dictionary<string, int>;

            win_coins.text = dic["catched_coins"].ToString();
            panel_win.SetActive(true);

            //if(dic["in_tutor"] == 1)
            //{
            //    panel_win_cont_btn.GetComponent<Button>().onClick.RemoveAllListeners();
            //    panel_win_cont_btn.GetComponent<Button>().onClick.AddListener(() =>
            //    {
            //        MessageBus.Restore();
            //        SceneManager.LoadScene("main");
            //    });

            //    panel_win_cont_btn_text.text = TextManager.getText("minigame_winstar_cont_btn_text"); ;
            //}
        }

        public void PickUp()
        {
            panel_win.GetComponent<Animator>().SetBool("pickup", true);
        }

        // Use this for initialization
        override public void ExtendedStart()
        {
            panel_fail.SetActive(false);
            panel_lose.SetActive(false);
            panel_win.SetActive(false);

            fail_header_text.text = TextManager.getText("minigame_fail_header_text");
            fail_body_text.text = TextManager.getText("minigame_fail_body_text");
            fail_footer_text.text = TextManager.getText("minigame_fail_footer_text");

            brokenheard_header_text.text = TextManager.getText("minigame_brokenheard_header_text");
            brokenheard_body_text.text = TextManager.getText("minigame_brokenheard_body_text");
            brokenheard_btn_text.text = TextManager.getText("minigame_brokenheard_btn_text");

            winstar_header_text.text = TextManager.getText("minigame_winstar_header_text");
            winstar_body_text.text = TextManager.getText("minigame_winstar_body_text");
            panel_win_cont_btn_text.text = TextManager.getText("minigame_winstar_btn_text");
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {

        }
    }
}