using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Yaga.MessageBus;

namespace TimeManager.Tutor
{
    public class SpriteMaskController : MonoBehaviour
    {
        private string _tutor_event_name = string.Empty;
        public string tutor_event_name
        {
            get { return _tutor_event_name; }
            set { _tutor_event_name = value; }
        }

        public int count;
        public TutorAim tutor_aim;

        public void Close()
        {
            transform.parent.gameObject.SetActive(false);
            //transform.parent.GetComponent<Animator>().SetBool("close", true);
            Time.timeScale = 1.0f;
            //GameStatistics.instance.SendStat("tutor_pressed_" + mask.GetComponent<UIMaskController>().tutor_event_name, 0);
        }

        public void OnMouseDown()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);

            foreach(var hit in hits)
            {
                switch(tutor_aim)
                {
                    case TutorAim.CUSTOMER:
                       
                        var go = hit.collider.gameObject.GetComponent<Customer.Customer>();
                        if (go != null)
                        {
                            go.OnMouseDown();
                            count--;
                        }
                        break;

                    case TutorAim.PRODUCTION_FIELD:

                        var pf = hit.collider.gameObject.GetComponent<ProductionField.ProductionFieldUnit>();
                        if (pf != null)
                        {
                            pf.OnMouseDown();
                            count--;
                        }
                        break;

                    case TutorAim.PRODUCTION_UNIT:

                        var pu = hit.collider.gameObject.GetComponent<ProductionUnit.ProductionUnit>();
                        if (pu != null)
                        {
                            pu.OnMouseDown();
                            count--;
                        }
                        break;

                    case TutorAim.PRODUCT_PROVIDER:

                        var pp = hit.collider.gameObject.GetComponent<ProductProvider.ProductProvider>();
                        if (pp != null)
                        {
                            pp.OnMouseDown();
                            count--;
                        }
                        break;
                }
            }

            if(count <= 0)
            {
                Close();
                MessageBus.Instance.SendMessage(TutorAPI.Messages.CLOSE_TUTOR_MASK);
            }         
        }
    }
}