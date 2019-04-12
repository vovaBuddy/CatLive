using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;
using Yaga;

namespace CatShow
{
    [Extension(Extensions.PAUSE)]
    public class Movable : ExtendedBehaviour
    {
        float speed = 6.0f;
        public bool check_away = false;
        Vector3 cat_position;

        // Use this for initialization
        public override void ExtendedStart()
        {

        }

        public void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "Player" && tag == "BeautyItem")
            {
                gameObject.GetComponent<Animator>().SetBool("catched", true);
                MessageBus.Instance.SendMessage(CatShowMessageType.MOVE_AWAY);                
            }

            else if (col.gameObject.tag == "Player" && tag == "GoldItem")
            {
                gameObject.GetComponent<Animator>().SetBool("catched", true);
                MessageBus.Instance.SendMessage(CatShowMessageType.COIN_CATCHED);
            }

        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
                MessageBus.Instance.SendMessage(CatShowMessageType.COLLISION);
        }

        // Update is called once per frame
        public override void ExtendedUpdate()
        {
            transform.position = new Vector3
                (transform.position.x,
                transform.position.y,
                transform.position.z - speed * Time.deltaTime);

            if(check_away)
            {
                if(transform.position.z < cat_position.z - 2.0f)
                {
                    Debug.Log("move_away");
                    check_away = false;
                    //MessageBus.Instance.SendMessage(CatShowMessageType.MOVE_AWAY);

                    if (string.Equals(gameObject.name, "finish"))
                    {
                        MessageBus.Instance.SendMessage(CatShowMessageType.END_GAME);
                    }
                }
            }
        }
    }
}