using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CatShow
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class ResultView : ExtendedBehaviour
    {
        public GameObject result_panel;

        public Text points;
        public Text coins;
        public Text btn;
        public Text header_text;

        [Subscribe(CatShowMessageType.OPEN_RESULT)]
        public void UpdEnergy(Message msg)
        {
            var param = CastHelper.Cast<CommonMessageParametr>(msg.parametrs);
            result_panel.SetActive(true);

            points.text = TextManager.getText("catshow_result_yes_text") + " +" + ((object[])param.obj)[0];
            coins.text = ((object[])param.obj)[1].ToString();
            if (!DataController.instance.tasks_storage.content.ContainsKey("first_show"))
            {
                btn.text = TextManager.getText("catshow_result_btn_cont_text");
            }
        }

        // Use this for initialization
        override public void ExtendedStart()
        {
            btn.text = TextManager.getText("catshow_result_btn_text");
            header_text.text = TextManager.getText("catshow_result_header_text");
            result_panel.SetActive(false);
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {

        }
    }
}