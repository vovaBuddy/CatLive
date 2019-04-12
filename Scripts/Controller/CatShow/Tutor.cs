using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;
using Yaga;

namespace CatShow
{
    public class Tutor : MonoBehaviour
    {
        Vector3 cat_position;
        bool check_away = true;

        // Use this for initialization
        public void Start()
        {
            check_away = true;
        }


        // Update is called once per frame
        public void Update()
        {
            if (check_away)
            {
                if (transform.position.z  < cat_position.z - 0.75f)
                {
                    MessageBus.Instance.SendMessage(CatShow.CatShowMessageType.TUTOR_ITEM);
                    MessageBus.Instance.SendMessage(Yaga.YagaMessageType.PAUSE_START);
                    check_away = false;
                }
                
            }
        }
    }
}