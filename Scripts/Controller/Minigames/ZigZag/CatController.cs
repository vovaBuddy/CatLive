using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

namespace ZigZag
{
    public class CatController : MonoBehaviour, Minigames.CatControllerInterface, IFlyBusterable, IRebornBusterable
    {
        GameObject cat;

        float CAT_SPEED = 1.5f;
        float sign = 1.0f;
        private float align_timer;
        private bool need_align;
        float init_y;
        Quaternion initRotation;
        Vector3 initPosition;
        private float init_speed;

        string cat_prefab_path = "Cat";

        bool gameover;

        bool reborn = false;

        public void InitCat()
        {
            cat = (GameObject)Instantiate(Resources.Load(cat_prefab_path));

            cat.transform.GetChild(0).GetComponent<Animator>().SetInteger("state", 1);

            init_y = cat.transform.position.y;
            need_align = false;
            gameover = false;
        }

        public GameObject getCat()
        {
            return cat;
        }

        public void Move()
        {
            Update();

            if (!gameover)
            {
                float delta = Time.deltaTime;

                if (need_align)
                {
                    align_timer -= delta;
                    if (System.Math.Abs(cat.transform.position.y - init_y) > 0.5)
                    {
                        need_align = false;
                        align_timer = 0.0f;
                    }
                }

                if (need_align &&
                    align_timer < 0)
                {
                    cat.transform.localRotation = initRotation;

                    need_align = false;
                }

                cat.transform.position = new Vector3(
                cat.transform.position.x + sign * CAT_SPEED * delta,
                cat.transform.position.y,
                cat.transform.position.z + sign * CAT_SPEED * delta);

                if (cat.transform.position.y < -10)
                {
                    if(!reborn)
                        MessageBus.Instance.SendMessage(Minigames.MiniGameMessageType.GAME_OVER);
                    else
                    {
                        MessageBus.Instance.SendMessage(Minigames.MiniGameMessageType.MINIGAME_REBORN);
                        Reborn();
                    }
                }
            }
        }

        public void OnTapAction()
        {
            if (DataController.instance.gamesRecords.zigZagTutorDone == false &&
                DataController.instance.gamesRecords.tutor_tap == false)
            {
                return;
            }
            DataController.instance.gamesRecords.tutor_tap = false;

            if (!need_align)
                align_timer = 1.5f;
            need_align = true;


            sign *= -1.0f;
            cat.transform.rotation =
                sign == -1 ?
                Quaternion.Euler(0.0f, -90.0f, 0.0f) :
                Quaternion.Euler(0.0f, 0.0f, 0.0f);

            initRotation = cat.transform.localRotation;
            initPosition = cat.transform.position;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame

        float reborn_endfly_timer = float.MaxValue;
        void Update()
        {
            reborn_endfly_timer -= Time.deltaTime;

            if(reborn_endfly_timer <= 0.0f)
            {
                EndFly();
                reborn_endfly_timer = float.MaxValue;
            }
        }

        public void StartFly()
        {
            CAT_SPEED = 0;

            cat.transform.position = new Vector3(0,
                1, 0);

            cat.transform.rotation = Quaternion.Euler(0.0f, -45.0f, 0.0f);

            cat.GetComponent<Rigidbody>().useGravity = false;
            cat.GetComponent<Rigidbody>().mass += 100000;
            cat.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        public void EndFly()
        {
            CAT_SPEED = 1.5f;

            cat.transform.rotation =
                sign == -1 ?
                Quaternion.Euler(0.0f, -90.0f, 0.0f) :
                Quaternion.Euler(0.0f, 0.0f, 0.0f);

            cat.GetComponent<Rigidbody>().useGravity = true;
            cat.GetComponent<Rigidbody>().mass -= 100000;
            cat.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

        public void StartReborn()
        {
            reborn = true;
        }

        public void EndReborn()
        {
            reborn = false;
        }


        public void Reborn()
        {
            cat.transform.position = new Vector3(0,
                2, 0);
            cat.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        }
    }
}