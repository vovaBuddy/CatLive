using Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yaga;
using Yaga.MessageBus;

namespace CatShow
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class CatController : ExtendedBehaviour
    {
        public DresserInitializer di;

        Vector3 init_center;
        Vector3 init_size;

        bool sw_up;
        bool sw_down;
        bool sw_left;
        bool sw_right;

        Transform shadow;

        // Use this for initialization
        public override void ExtendedStart()
        {
            init_center = gameObject.GetComponent<BoxCollider>().center;
            init_size = gameObject.GetComponent<BoxCollider>().size;
            need_init = false;

            sw_up = false;
            sw_down = false;
            sw_left = false;
            sw_right = false;

            gameObject.transform.GetChild(0).GetComponent<Animator>().SetInteger("state", 1);

            di.Init(gameObject);

            shadow = gameObject.transform.Find("cat_001_rig01_clothes_").Find("New Sprite");
        }

        void SetInit()
        {
            gameObject.GetComponent<BoxCollider>().center = init_center;
            gameObject.GetComponent<BoxCollider>().size = init_size;

            gameObject.transform.GetChild(0).GetComponent<Animator>().SetInteger("state", 1);
        }

        void Down()
        {
            gameObject.GetComponent<BoxCollider>().center =
                new Vector3(init_center.x, 0.1f, init_center.y);

            gameObject.GetComponent<BoxCollider>().size =
                new Vector3(init_size.x, 0.2f, init_size.y);

            gameObject.transform.GetChild(0).GetComponent<Animator>().SetInteger("state", 2);
            gameObject.transform.GetChild(0).GetComponent<Animator>().Play("New State 1");
        }

        void Move(bool rigth)
        {
            if(rigth)
            {
                if(transform.position.x < 1.0f)
                {
                    transform.position = new Vector3
                        (transform.position.x + 1.0f,
                         transform.position.y,
                          transform.position.z);
                }
            }
            else
            {
                if (transform.position.x > -1.0f)
                {
                    transform.position = new Vector3
                        (transform.position.x - 1.0f,
                         transform.position.y,
                          transform.position.z);
                }
            }
        }

        [Subscribe(MainScene.MainMenuMessageType.SWIPE_LEFT)]
        public void swl(Message msg)
        {
            sw_left = true;
        }

        [Subscribe(MainScene.MainMenuMessageType.SWIPE_RIGHT)]
        public void swr(Message msg)
        {
            sw_right = true;
        }

        [Subscribe(MainScene.MainMenuMessageType.SWIPE_UP)]
        public void swu(Message msg)
        {
            sw_up = true;
        }

        [Subscribe(MainScene.MainMenuMessageType.SWIPE_DOWN)]
        public void swd(Message msg)
        {
            sw_down = true;
        }


        void Jump()
        {
            Vector3 jump = new Vector3(0, 450, 0);
            gameObject.GetComponent<Rigidbody>().AddForce(jump, ForceMode.Impulse);
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            //Debug.Log("Jump");
        }

        // Update is called once per frame
        float no_action_timer = 0.0f;
        float NO_ACTION_TIME = 0.76f;

        bool need_init;
        public override void ExtendedUpdate()
        {
            no_action_timer -= Time.deltaTime;
            if (no_action_timer <= 0.0f)
            {
                //Debug.Log(transform.forward);
                if (need_init)
                {
                    SetInit();
                    need_init = false;
                }

                if (Input.GetKeyUp(KeyCode.A) || sw_left)
                {
                    Move(false);
                    sw_left = false;
                }

                else if (Input.GetKeyUp(KeyCode.D) || sw_right)
                {
                    Move(true);
                    sw_right = false;
                }

                else if (Input.GetKeyUp(KeyCode.W) || sw_up)
                {
                    Jump();

                    no_action_timer = NO_ACTION_TIME;
                    sw_up = false;
                }

                else if (Input.GetKeyUp(KeyCode.S) || sw_down)
                {
                    Down();
                    need_init = true;
                    no_action_timer = NO_ACTION_TIME / 1.2f;
                    sw_down = false;
                }

            }

            shadow.position = new Vector3(shadow.position.x, 0, shadow.position.z);
        }
    }
}