using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using UnityEngine.UI;

namespace TimeManager.Tutor
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class TutorController : ExtendedBehaviour
    {
        public List<TutorItem> tutor_items;
        public GameObject mask_prefub;
        public GameObject parent;

        public GameObject info_panel;
        public Text description;

        public GameObject info_2_panel;
        public Text description2;

        bool aim_ready = false;
        int req_products = -1;
        bool busy = false;

        [Subscribe(TutorAPI.Messages.INIT)]
        public void Init(Message msg)
        {
            var p = Yaga.Helpers.CastHelper.Cast<TutorAPI.InitParams>(msg.parametrs);

            tutor_items = p.items;

            aim_ready = tutor_items[0].aim_ready;
            req_products = tutor_items[0].product.Count;

            check();
        }

        [Subscribe(TutorAPI.Messages.CLOSE_TUTOR_MASK)]
        public void Close(Message msg)
        {
            busy = false;
            info_panel.SetActive(false);
            check();
        }

        void check()
        {
            if (busy)
                return;
            
            if (tutor_items.Count > 0)
            {
                if (tutor_items[0].type == TutorType.INFO)
                {
                    tutor_items.RemoveAt(0);
                }
                else
                {
                    aim_ready = tutor_items[0].aim_ready;
                    req_products = tutor_items[0].product.Count;
                }
            }

            if (aim_ready && req_products == 0)
            {
                ShowTutor();
                aim_ready = false;
                req_products = -1;
            }
        }

        [Subscribe(CustomerAPI.Messages.CUSTOMER_START_WAITING)]
        public void Customer(Message msg)
        {
            var p = Yaga.Helpers.CastHelper.Cast<CustomerAPI.WaitnigParams>(msg.parametrs);

            if(tutor_items[0].aim == TutorAim.CUSTOMER)
            {
                tutor_items[0].position_index = p.index;
                tutor_items[0].aim_ready = true;
                check();
            }
        }

        [Subscribe(ProductionFieldAPI.Messages.READY)]
        public void PFReady(Message msg)
        {
            var p = Yaga.Helpers.CastHelper.Cast<ProductionFieldAPI.ReadyParams>(msg.parametrs);

            foreach(var conf in tutor_items)
            {
                int pr_index = 0;
                foreach(var pr in conf.product)
                {
                    if(pr == p.type)
                    {
                        conf.product.RemoveAt(pr_index);
                        check();
                        return;
                    }
                }
                pr_index++;
            }
        }

        [Subscribe(GameController.Messages.START_GAME)]
        public void close_panel_2(Message msg)
        {
            busy = false;
            info_2_panel.SetActive(false);
        }

        public void ShowTutor2(TutorItem conf)
        {
            busy = true;

            info_2_panel.SetActive(true);
            description2.text = TextManager.getText(conf.text_id);
        }

        void ShowTutor()
        {
            busy = true;

            Time.timeScale = 0.2f;

            TutorItem conf = tutor_items[0];

            var mask = Instantiate(mask_prefub, parent.transform, false);
            mask.name = "mask_" + UnityEngine.Random.Range(1000, 9999).ToString();
            mask.transform.localScale = new Vector3(1, 1, 1);
            mask.SetActive(true);
            var mask_comp = mask.GetComponentInChildren<SpriteMaskController>();
            mask_comp.tutor_event_name = conf.name;
            mask_comp.count = conf.clicks;
            mask_comp.tutor_aim = conf.aim;

            mask.transform.position = ResourcesController.get_instance().GetPositionByIndexAndType(conf.aim, conf.position_index);

            //(3.6 - 0.75, 5.5)
            if(conf.aim == TutorAim.CUSTOMER)
                mask.transform.position = new Vector3(mask.transform.position.x + 2.85f, mask.transform.position.y + 5.5f, -1);
            else
                mask.transform.position = new Vector3(mask.transform.position.x, mask.transform.position.y, -1);

            if (tutor_items[0].text_id != string.Empty)
            {
                description.text = TextManager.getText(tutor_items[0].text_id);
                info_panel.SetActive(true);
            }

            tutor_items.RemoveAt(0);
        }
    }
}