using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using UnityEngine.UI;

namespace PhotoScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class ResultScreenView : ExtendedBehaviour
    {

        public GameObject result_screen;
        public GameObject star_giver;
        public GameObject scan_info;
        public GameObject pickup_panel;

        

        public GameObject share_btn;
        public GameObject home_btn;
        public GameObject restart_btn;
        public GameObject pickup_btn;
        public GameObject pick_up_btn_anim_obj;
        public GameObject header_star_anim;

        string pets_folder = "Cats";
        string empty_sprite_name = "empty_cat";
        public GameObject cat_img;

        public GameObject pb_give_star;
        int max_pb_with = 940;

        public Text now_you_are_text;
        public Text star_rule_text;
        public Text give_star_title;
        public Text cat_status;
        public Text pickup_btn_text;
        public Text pickup_panel_btn_text;

        public List<GameObject> recent_opened;

        public GameObject star_start;
        public GameObject star_dest;
        GameObject move_star;
        public GameObject star_header;
        public Text star_count;


        private float update_animation_timeout = 0.034f;
        private float update_animation_timer = 0.0f;
        private Queue<Message> animation_messages;

        // Use this for initialization
        override public void ExtendedStart()
        {
            now_you_are_text.text = TextManager.getText("now_you_are_text");
            star_rule_text.text = TextManager.getText("star_rule_text");
            pickup_btn_text.text = TextManager.getText("pickup_btn_text");
            pickup_panel_btn_text.text = TextManager.getText("pickup_panel_btn_text");
            result_screen.SetActive(false);
            star_header.SetActive(false);
            star_count.text = DataController.instance.catsPurse.Stars.ToString();

            update_animation_timer = update_animation_timeout;
            animation_messages = new Queue<Message>();
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {
            if (animation_messages.Count != 0)
            {
                update_animation_timer -= Time.deltaTime;
                if (update_animation_timer < 0)
                {
                    update_animation_timer = update_animation_timeout;

                    MessageBus.Instance.SendMessage(animation_messages.Dequeue());
                }
            }
        }

        [Subscribe(UIMessageType.SHOW_RESULT_SELFIE_SCREEN)]
        public void ShowResiltSelfie(Message msg)
        {
            result_screen.SetActive(true);
            star_giver.SetActive(false);
            scan_info.SetActive(false);
        }

        [Subscribe(UIMessageType.INCREMENT_STARS_VALUE)]
        public void IncrStarsValue(Message msg)
        {
            var param = CastHelper.Cast<UpdateValueParametr>(msg.parametrs);
            star_count.text = param.value.ToString();
        }

        [Subscribe(UIMessageType.PROCESS_PROGRESS_BAR)]
        public void ProcPB(Message msg)
        {
            var param = CastHelper.Cast<UpdateValueParametr>(msg.parametrs);
            float tmp = pb_give_star.GetComponent<RectTransform>().sizeDelta.y;
            pb_give_star.GetComponent<RectTransform>().sizeDelta =
                new Vector2(param.value, tmp);

            pb_give_star.transform.GetChild(0).gameObject.SetActive(true);

            pb_give_star.transform.GetChild(0).transform.localPosition =
                new Vector3(param.value, pb_give_star.transform.GetChild(0).transform.localPosition.y, pb_give_star.transform.GetChild(0).transform.localPosition.z);
        }

        [Subscribe(UIMessageType.PROCESS_STAR_MOVE)]
        public void ProcStarMove(Message msg)
        {
            var param = CastHelper.Cast<UpdateVector2Parametr>(msg.parametrs);
            move_star.transform.position = param.value;
        }

        [Subscribe(UIMessageType.SHOW_RESULT_TUTOR)]
        public void ShowTutorResult(Message msg)
        {
            share_btn.SetActive(false);
            home_btn.SetActive(false);
            restart_btn.SetActive(false);
            pickup_btn.SetActive(true);
            star_header.SetActive(true);

            //
            var param = CastHelper.Cast<ResultScreenParametr>(msg.parametrs);
            Sprite sprite = ResourceHelper.LoadSprite(pets_folder, param.name);
            cat_img.GetComponent<Image>().sprite = sprite;
            cat_status.text = TextManager.getCatsText(param.name) + " " + TextManager.getText("cate");

            //pb processing
            float pb_width_start = 0;
            float pb_width_end = max_pb_with;
            float delta = pb_width_end - pb_width_start;
            Message m = new Message();
            m.Type = UIMessageType.PROCESS_PROGRESS_BAR;

            while (pb_width_start < pb_width_end)
            {

                m.parametrs = new UpdateValueParametr(pb_width_start);
                animation_messages.Enqueue(m);

                pb_width_start += delta / 20.0f;
            }

            m.parametrs = new UpdateValueParametr(pb_width_end);
            animation_messages.Enqueue(m);

            m.Type = UIMessageType.CLOSE_PB_PS;
            animation_messages.Enqueue(m);

            //
            DataController.instance.catsPurse.Stars += 1;
            give_star_title.text = TextManager.getText("get_star_text");

            //
            for (int i = 1; i < param.need_open; ++i)
            {
                recent_opened[i].SetActive(false);
            }

            recent_opened[0].GetComponent<Image>().sprite =
                               ResourceHelper.LoadSprite(pets_folder, param.names[0]);
        }

        [Subscribe(UIMessageType.SHOW_RESULT)]
        public void Show(Message msg)
        {
            if (DataController.instance.tasks_storage.content.ContainsKey("opened_pets") &&
                (int)DataController.instance.tasks_storage.content["opened_pets"] > 5)
            {
                share_btn.SetActive(true);
                home_btn.SetActive(true);
            }
            else if (DataController.instance.tasks_storage.content.ContainsKey("opened_pets") &&
                (int)DataController.instance.tasks_storage.content["opened_pets"] == 5)
            {
                share_btn.SetActive(false);
                restart_btn.SetActive(false);
                home_btn.SetActive(true);
            }
            else
            {
                share_btn.SetActive(false);
                home_btn.SetActive(false);
            }
            var param = CastHelper.Cast<ResultScreenParametr>(msg.parametrs);
            Sprite sprite = ResourceHelper.LoadSprite(pets_folder, param.name);
            cat_img.GetComponent<Image>().sprite = sprite;

            give_star_title.text = TextManager.getText("next_star_1") + (param.need_open - param.opened).ToString() + TextManager.getText("next_star_2");
            cat_status.text = TextManager.getCatsText(param.name) + " " + TextManager.getText("cate");


            //init messqge queue with pb increase values
            if (param.new_pet)
            {
                float pb_width_start = (param.opened - 1) / ((float)param.need_open) * max_pb_with;
                float pb_width_end = (param.opened) / ((float)param.need_open) * max_pb_with;
                float delta = pb_width_end - pb_width_start;
                Message m = new Message();
                m.Type = UIMessageType.PROCESS_PROGRESS_BAR;

                while (pb_width_start < pb_width_end)
                {

                    m.parametrs = new UpdateValueParametr(pb_width_start);
                    animation_messages.Enqueue(m);

                    pb_width_start += delta / 10.0f;
                }
                
                m.parametrs = new UpdateValueParametr(pb_width_end);
                animation_messages.Enqueue(m);

                m.Type = UIMessageType.CLOSE_PB_PS;
                animation_messages.Enqueue(m);

                if (param.opened == param.need_open)
                {
                    //tests
                    //DataController.instance.catsPurse.Stars += 50;

                    restart_btn.SetActive(false);
                    pickup_btn.SetActive(true);

                    DataController.instance.catsPurse.Stars += 1;
                    star_header.SetActive(true);
                    //move_star = Instantiate(star_start);
                    //move_star.transform.parent = star_start.transform.parent;
                    //move_star.transform.localScale = star_start.transform.localScale;

                    //m.Type = UIMessageType.PROCESS_STAR_MOVE;

                    //for (float y = 0.0f; y < 1.0f; y += 0.06f)
                    //{
                    //    m.parametrs = new UpdateVector2Parametr(
                    //        Vector3.Lerp(move_star.transform.position, star_dest.transform.position, y));

                    //    animation_messages.Enqueue(m);
                    //}

                    //m.parametrs = new UpdateVector2Parametr(star_dest.transform.position);
                    //animation_messages.Enqueue(m);

                    give_star_title.text = TextManager.getText("get_star_text");
                }
            }
            else
            {
                pb_give_star.transform.GetChild(0).gameObject.SetActive(false);
                float pb_width_end = (param.opened) / ((float)param.need_open) * max_pb_with;
                Message m = new Message();
                m.Type = UIMessageType.PROCESS_PROGRESS_BAR;
                m.parametrs = new UpdateValueParametr(pb_width_end);
                ProcPB(m);

                pb_give_star.transform.GetChild(0).gameObject.SetActive(false);
            }


            for (int i = 0; i < param.opened; ++i)
            {
                recent_opened[i].GetComponent<Image>().sprite = 
                    ResourceHelper.LoadSprite(pets_folder, param.names[i]);
            }

            for(int i = param.opened; i < param.need_open; ++i)
            {
                recent_opened[i].GetComponent<Image>().sprite =
                    ResourceHelper.LoadSprite(pets_folder, empty_sprite_name);
            }
        }

        private void close_pbps()
        {
            pb_give_star.transform.GetChild(0).gameObject.SetActive(false);
        }
        [Subscribe(UIMessageType.CLOSE_PB_PS)]
        public void ClosePBPS(Message msg)
        {
            Invoke("close_pbps", 0.5f);           
        }

        [Subscribe(UIMessageType.OPEN_PICKUP_SCREEN)]
        public void PickUp(Message msg)
        {
            pickup_panel.SetActive(true);
            pb_give_star.transform.GetChild(0).gameObject.SetActive(false);
            pick_up_btn_anim_obj.SetActive(false);
            header_star_anim.SetActive(true);

            var m = new Message();
            m.Type = UIMessageType.INCREMENT_STARS_VALUE;
            m.parametrs = new UpdateValueParametr(
                DataController.instance.catsPurse.Stars);
            MessageBus.Instance.SendMessage(m);
        }

        [Subscribe(UIMessageType.OPEN_RESULT_SCREEN)]
        public void Open(Message msg)
        {
            result_screen.SetActive(true);
        }

        [Subscribe(UIMessageType.CLOSE_RESULT_SCREEN)]
        public void Close(Message msg)
        {
            result_screen.SetActive(false);
        }
    }
}