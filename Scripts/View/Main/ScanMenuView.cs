using System.Collections;
using System.Collections.Generic;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using UnityEngine.UI;
using UnityEngine;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class ScanMenuView : ExtendedBehaviour
    {
        public GameObject scan_menu;
        string pets_folder = "Cats";

        public Text cats_opened;
        public Text header_text;
        public Text btn_text;
        public Text star_rule;

        public GameObject scaned_pets_cont;
        public GameObject star_cont;
        public GameObject pb_line;
        float pb_width = 950;

        [Subscribe(MainMenuMessageType.SHOW_SCAN_MENU)]
        public void ShowMain(Message msg)
        {
            //if (!Task.TaskController.GetController().check_any_task_in_action())
                scan_menu.SetActive(true);

            if (!DataController.instance.tasks_storage.content.ContainsKey("opened_pets") ||
                (int)DataController.instance.tasks_storage.content["opened_pets"] == 0)
            {
                ArrowController.Instance.scanning_btn.SetActive(true);
            }
        }

        [Subscribe(MainMenuMessageType.CLOSE_SCAN_MENU)]
        public void CloseMain(Message msg)
        {
            scan_menu.SetActive(false);
            ArrowController.Instance.scanning_btn.SetActive(false);
        }

        [Subscribe(MainMenuMessageType.SHOW_SCANNED_PETS)]
        public void ShowScanned(Message msg)
        {
            var param = CastHelper.Cast<ScanMenuMessageParametrs>(msg.parametrs);

            int i = 0;
            foreach(string name in param.names)
            {
                Sprite sprite = ResourceHelper.LoadSprite(pets_folder, name);
                scaned_pets_cont.transform.Find("ic (" + i.ToString() + ")").GetComponent<Image>().sprite = sprite;
                ++i;
            }

            for (int j = 0; j < param.max_star_cnt; j++ )
            {
                if (j >= param.star_cnt)
                {
                    Color clr = star_cont.transform.Find("Image (" + j.ToString() + ")").GetComponent<Image>().color;
                    clr.a = 0;
                    star_cont.transform.Find("Image (" + j.ToString() + ")").GetComponent<Image>().color = clr;
                }
            }

            float tmp = pb_line.GetComponent<RectTransform>().sizeDelta.y;
            pb_line.GetComponent<RectTransform>().sizeDelta =
                new Vector2((param.star_cnt / (float) param.max_star_cnt) * pb_width, tmp);

            cats_opened.text = TextManager.getText("open_cats") + param.names.Count + "/" + param.max_cats;
        }

        // Use this for initialization
        override public void ExtendedStart()
        {
            header_text.text = TextManager.getText("mm_scanning_header_text");
            btn_text.text = TextManager.getText("mm_scanning_btn_text");
            star_rule.text = TextManager.getText("star_rule_text");
            scan_menu.SetActive(false);
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {

        }
    }
}

