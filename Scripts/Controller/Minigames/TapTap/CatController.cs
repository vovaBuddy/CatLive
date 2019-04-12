using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

namespace TapTap
{
    public class CatController : MonoBehaviour, Minigames.CatControllerInterface, IFlyBusterable, IRebornBusterable
    {
        GameObject cat;

        private bool need_align;
        private float time_before_align;

        private Vector3 def_pos;

        string cat_prefab_path = "Cat";

        bool gameover;

        bool reborn;

        public void InitCat()
        {
            cat = (GameObject)Instantiate(Resources.Load(cat_prefab_path));

            cat.transform.GetChild(0).GetComponent<Animator>().SetInteger("state", 1);

            gameover = false;

            GameSpeed.speed_factor = 2.0f;
        }

        public GameObject getCat()
        {
            return cat;
        }

        bool z_align = false;
        float time_from_last = 0.0f;
        public void needAlign()
        {
            need_align = true;
            time_before_align = 0.8f;

            if (time_from_last <= 0.0f)
            {
                time_from_last = time_before_align;

                cat.transform.position = new Vector3(
                 cat.transform.position.x, cat.transform.position.y + 0.6f,
                 z_align ? 0 : cat.transform.position.z);

                z_align = false;
            }
        }

        public void Respawn()
        {
            cat.transform.position = new Vector3(
               def_pos.x, def_pos.y, def_pos.z);
        }

        public void StartFly()
        {
            cat.transform.position = new Vector3(
                0, cat.transform.position.y + 3, cat.transform.position.z);
            cat.GetComponent<Rigidbody>().useGravity = false;
        }

        public void EndFly()
        {
            cat.GetComponent<Rigidbody>().useGravity = true;
            z_align = true;
            needAlign();
        }

        public void Move()
        {
            time_from_last -= Time.deltaTime;
            if (need_align)
            {
                time_before_align -= Time.deltaTime;

                if (time_before_align < 0.0f)
                {
                    cat.transform.localRotation = new Quaternion(0, 0, 0, 1);
                    cat.transform.position = new Vector3(
                        0, cat.transform.position.y, cat.transform.position.z);
                }
            }

            if (cat.transform.position.y < -10)
            {
                if (!reborn)
                {
                    MessageBus.Instance.SendMessage(Minigames.MiniGameMessageType.GAME_OVER);
                }
                else
                {
                    MessageBus.Instance.SendMessage(Minigames.MiniGameMessageType.MINIGAME_REBORN);
                    z_align = true;
                    Reborn();
                }
            }

            if (GameSpeed.speed_factor < 4.0f)
                GameSpeed.speed_factor += Time.deltaTime / 60.0f;

        }
        
        public void OnTapAction()
        {
            if (DataController.instance.gamesRecords.tapTapTutorDone == true)
                needAlign();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

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
            cat.transform.position = new Vector3( 0, 3.0f, 0);
        }
    }
}