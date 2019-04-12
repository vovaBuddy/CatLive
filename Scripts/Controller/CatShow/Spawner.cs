using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;

namespace CatShow
{

    [Extension(Extensions.SUBSCRIBE_MESSAGE, Extensions.PAUSE)]
    public class Spawner : ExtendedBehaviour
    {

        public GameObject obstacle1_1;
        public GameObject obstacle2_1;
        public GameObject obstacle2_2;
        public GameObject bty;
        public GameObject cn;
        public GameObject finish;
        bool finished;
        bool start;

        float z_offset = 1.5f;

        public Transform parent_platform;
        float[] x_coordinates = { -1.5f, -0.5f, 0.5f, 1.5f };

        // Use this for initialization
        public override void ExtendedStart()
        {
            finished = false;
            start = false;
        }

        [Subscribe(CatShowMessageType.START_GAME)]
        public void StartGame(Message msg)
        {
            start = true;
        }

        void Spown()
        {
            float gold_value = Random.Range(0.0f, 1.0f);
            if (gold_value > 0.2f)
            {
                GameObject gold = (GameObject)Instantiate
                    (Resources.Load("CatShow/coin"));
                gold.transform.parent = parent_platform;
                gold.transform.position = new Vector3(
                                x_coordinates[Random.Range(0, 4)],
                                cn.transform.position.y,
                                cn.transform.position.z);
                //gold.GetComponent<Movable>().check_away = true;
            }

                GameObject beauty = (GameObject)Instantiate
                (Resources.Load("CatShow/beauty"));
            beauty.transform.parent = parent_platform;
            beauty.transform.position = new Vector3(
                            x_coordinates[Random.Range(0, 4)],
                            bty.transform.position.y,
                            bty.transform.position.z);
            //beauty.GetComponent<Movable>().check_away = true;

            float type1_value = Random.Range(0.0f, 1.0f);

            //if(true)
            if (type1_value > 0.7f)
            {
                GameObject go = (GameObject)Instantiate
                    (Resources.Load("CatShow/Obstacle1_1"));
                go.transform.parent = parent_platform;
                go.transform.position = obstacle1_1.transform.position;
                go.GetComponent<Movable>().check_away = true;
            }
            else
            {
                bool select_checked = false;
                int obstacle2_avalible = 3;
                for (int i = 0; i < 4; ++i)
                {
                    type1_value = Random.Range(0.0f, 1.0f);

                    if (type1_value < 0.33f)
                    {
                        GameObject go = (GameObject)Instantiate
                            (Resources.Load("CatShow/Obstacle2_1"));
                        go.transform.parent = parent_platform;
                        go.transform.position = new Vector3(
                            x_coordinates[i],
                            obstacle2_1.transform.position.y,
                            obstacle2_1.transform.position.z);

                        if(!select_checked)
                        {
                            select_checked = true;
                            go.GetComponent<Movable>().check_away = true;
                        }


                    }
                    else if (type1_value > 0.40f)
                    {
                        GameObject go;
                        if (obstacle2_avalible > 0)
                        {
                            go = (GameObject)Instantiate
                                (Resources.Load("CatShow/Obstacle2_2"));
                            go.transform.position = obstacle2_2.transform.position;
                        }
                        else
                        {
                            go = (GameObject)Instantiate
                                (Resources.Load("CatShow/Obstacle2_1"));
                            go.transform.position = obstacle2_1.transform.position;
                        }
                        go.transform.parent = parent_platform;
                        obstacle2_avalible--;

                        go.transform.position = new Vector3(
                            x_coordinates[i],
                            go.transform.position.y,
                            go.transform.position.z);

                        if (!select_checked)
                        {
                            select_checked = true;
                            go.GetComponent<Movable>().check_away = true;
                        }
                    }
                }
            }
        }

        float SPOWN_TIME = 1.5f;
        //float SPOWN_TIME = 3.15f;
        float spown_timer = 0.0f;

        [Subscribe(CatShowMessageType.LAST_OBSTACLE)]
        public void last(Yaga.MessageBus.Message msg)
        {
            finished = true;
        }

        public override void ExtendedUpdate()
        {
            //if (!start)
            //    return;

            spown_timer -= Time.deltaTime;

            if (spown_timer <= 0)
            {
                spown_timer = SPOWN_TIME;

                if (!finished)
                {
                    Spown();
                }
                else
                {
                    finish.SetActive(true);

                    spown_timer = 1000000;
                }

                Yaga.MessageBus.MessageBus.Instance.SendMessage(CatShow.CatShowMessageType.SPAWNED);
            }

        }
    }
}