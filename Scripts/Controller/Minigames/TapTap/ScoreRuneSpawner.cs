using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minigames;
using System;
using Yaga.MessageBus;
using Yaga.Helpers;

namespace TapTap
{
    public enum RuneType
    {
        Reverse = 0,
        Points = 1,
    }

    public class RuneSpawner : MonoBehaviour, IRuneSpawner
    {
        Transform InitPos;
        Transform Platform;

        GameObject PatternRunes;
        GameObject PatternRunesPoint;

        private float speed = 0.95f;

        private float SPAWN_CD_INIT = 2.1f;
        private float spawn_cool_down_rune;

        private int last_index;

        private float DESTROY_Z_COORD = -7.0f;

        int index = 0;


        public void Init()
        {
        }

        public void InitPlatform(Message m)
        {
            var param = CastHelper.Cast<InitUpdate>(m.parametrs);

            Platform = param.platform_tr;
            PatternRunes = param.rune1_templ.gameObject;
            PatternRunesPoint = param.rune2_templ.gameObject;
        }

        public void UpdateInitPos(Message m)
        {

        }

        public void Update()
        {
            spawn_cool_down_rune -= Time.deltaTime;
            if (spawn_cool_down_rune < 0.0f)
            {
                spawn_cool_down_rune = SPAWN_CD_INIT / GameSpeed.speed_factor;

                int pat = UnityEngine.Random.Range(0, 4);

                Transform patObj;

                float r = UnityEngine.Random.Range(0, 100);

                RuneType type = r > 10 ? RuneType.Points : RuneType.Reverse;
                string msg_type;

                if (type == RuneType.Reverse)
                {
                    patObj = PatternRunes.transform.GetChild(pat);
                    msg_type = MiniGameMessageType.REVERSE_RUNE_CATCHED;
                }
                else
                {
                    patObj = PatternRunesPoint.transform.GetChild(pat);
                    msg_type = MiniGameMessageType.SCORE_RUNE_CATCHED;
                }

                GameObject box = Instantiate(patObj).gameObject;
                box.name = "rune" + index.ToString();
                index++;

                //if (String.Equals(patObj.name, "score_3") )
                //{
                //    box.transform.localEulerAngles = new Vector3(patObj.localEulerAngles.x, patObj.localEulerAngles.y, 90);
                //}

                //else if (String.Equals(patObj.name, "score_1"))
                //{
                //    box.transform.localEulerAngles = new Vector3(patObj.localEulerAngles.x, patObj.localEulerAngles.y, -90);
                //}

                //else if (String.Equals(patObj.name, "score_2"))
                //{
                //    box.transform.localEulerAngles = new Vector3(patObj.localEulerAngles.x, patObj.localEulerAngles.y, -180);
                //}

                box.GetComponent<Rune>().msg_type = msg_type;
                box.transform.parent = Platform;
                box.transform.position = patObj.position;
                box.transform.localEulerAngles = patObj.localEulerAngles;
                
            }
        }
    }
}
