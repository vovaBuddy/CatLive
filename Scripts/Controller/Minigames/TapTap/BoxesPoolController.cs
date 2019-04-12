using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Yaga;
using Yaga.MessageBus;


namespace TapTap
{
    [Extension(Extensions.PAUSE, Extensions.SUBSCRIBE_MESSAGE)]
    public class BoxesPoolController : ExtendedBehaviour
    {

        public GameObject PatternBoxes;


        private List<GameObject> boxes;
        private float speed = 0.95f;

        private float SPAWN_CD_INIT = 2.1f;
        private float spawn_cool_down;
        private float spawn_cool_down_rune;

        static int box_index = 0;

        private int last_index;

        private float DESTROY_Z_COORD = -7.0f;



        // Use this for initialization
        override public void ExtendedStart()
        {
            Debug.Log("start");
            boxes = new List<GameObject>();
            spawn_cool_down = SPAWN_CD_INIT;
            spawn_cool_down_rune = SPAWN_CD_INIT;

            int children = transform.childCount;
            for (int i = 0; i < children; ++i)
            {
                boxes.Add(transform.GetChild(i).gameObject);
            }

            last_index = children;


        }

        private void update_boxes()
        {
            int children = transform.childCount;

            if (children > last_index)
            {
                for (int i = last_index; i < children; ++i)
                {
                    boxes.Add(transform.GetChild(i).gameObject);
                }

                last_index = children;
            }
        }

        public void clear_pool()
        {
            foreach (GameObject box in boxes.ToList())
            {
                boxes.Remove(box);
                Destroy(box);
            }

            last_index = 0;
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {
            update_boxes();


            foreach (GameObject box in boxes.ToList())
            {
                box.transform.position =
                    new Vector3(box.transform.position.x, box.transform.position.y,
                    box.transform.position.z - Time.deltaTime * speed * GameSpeed.speed_factor);

                if (box.transform.localPosition.z < DESTROY_Z_COORD)
                {
                    boxes.Remove(box);
                    Destroy(box);
                    last_index -= 1;

                    MessageBus.Instance.SendMessage(Minigames.MiniGameMessageType.BOX_CRASH);


                    //GameObject.Find("Sounds").GetComponent<SoundsController>().play_box_destroy();
                }
            }

            

            spawn_cool_down -= Time.deltaTime;
            if (spawn_cool_down < 0.0f && 
                (bool)DataController.instance.tasks_storage.content["game_tutor_done"])
            {
                spawn_cool_down = SPAWN_CD_INIT / GameSpeed.speed_factor;

                //float r1 = Random.Range(0.00f, 10.0f);

                //if(Random.Range(0,100) < 35)
                //{
                //    Debug.Log("rune resp");
                //    int pat = Random.Range(0, 4);

                //    Transform patObj;

                //    RuneType type = (RuneType)Random.Range(0, 2);
                //    if (type == RuneType.Reverse)
                //    {
                //        patObj = PatternRunes.transform.GetChild(pat);
                //    }
                //    else
                //    {
                //        patObj = PatternRunesPoint.transform.GetChild(pat);
                //    }

                //    GameObject box = Instantiate(patObj).gameObject;
                //    box.name = "box" + box_index.ToString();
                //    box_index++;

                //    box.transform.position = patObj.position;
                //    box.transform.rotation = patObj.rotation;
                //    box.transform.parent = transform;
                //}

                {
                    int times = Random.Range(1, 3);

                    int old_patt = 10;

                    for (int i = 0; i < times; ++i)
                    {
                        int pat = Random.Range(0, 4);
                        while (pat == old_patt) { pat = Random.Range(0, 4); }
                        old_patt = pat;

                        Transform patObj = PatternBoxes.transform.GetChild(pat);

                        GameObject box = Instantiate(patObj).gameObject;
                        box.name = "box" + box_index.ToString();
                        box_index++;

                        int rot_pat = Random.Range(0, 4);
                        patObj = PatternBoxes.transform.GetChild(rot_pat);

                        box.transform.position = patObj.position;
                        box.transform.rotation = patObj.rotation;
                        box.transform.parent = transform;

                    }

                }

            }

        }
    }
}