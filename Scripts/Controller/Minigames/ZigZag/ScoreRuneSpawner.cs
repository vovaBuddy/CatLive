using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minigames;
using System;
using Yaga.MessageBus;
using Yaga.Helpers;

namespace ZigZag
{
    public class RandomizeStrategy
    {
        public int min;
        public int max;
        public float cool_down;

        public RandomizeStrategy(int mi, int ma, float t)
        {
            min = mi;
            max = ma;
            cool_down = t;
        }
    }

    public class ScoreRuneSpawner : IRuneSpawner
    {
        public static RandomizeStrategy rand1x1 = new RandomizeStrategy(0, 0, 1.0f);
        public static RandomizeStrategy rand2x2 = new RandomizeStrategy(0, 1, 1.1f);
        public static RandomizeStrategy rand3x3 = new RandomizeStrategy(-1, 1, 1.5f);
        RandomizeStrategy cur_strategy;
        Action UpdateAction;

        Transform InitPos;
        Transform Platform;

        int[][] offsets = new int[9][];

        float timer;
        float COOL_DOWM_bunch = 0.35f;
        int resp_count;
        int respowned = 0;

        public GameObject LevelObjects;

        public void Init()
        {
            offsets[0] = new int[2] { -1, -1 };
            offsets[1] = new int[2] { -1, 0 };
            offsets[2] = new int[2] { -1, 1 };
            offsets[3] = new int[2] { 1, -1 };
            offsets[4] = new int[2] { 1, 0 };
            offsets[5] = new int[2] { 1, 1 };
            offsets[6] = new int[2] { 0, -1 };
            offsets[7] = new int[2] { 0, 0 };
            offsets[8] = new int[2] { 0, 1 };

            cur_strategy = rand3x3;
            UpdateAction = Update3x3;
            timer = cur_strategy.cool_down;            
            resp_count = 1;
            respowned = 0;
        }

        public void UpDifficult()
        {
            if (cur_strategy == rand3x3)
            {
                cur_strategy = rand2x2;
                UpdateAction = Update2x2;
            }
            else if (cur_strategy == rand2x2)
            {
                cur_strategy = rand1x1;
                UpdateAction = Update1x1;
            }
        }

        public void InitPlatform(Message m)
        {
            var param = CastHelper.Cast<UpdateInitPos>(m.parametrs);

            Platform = param.transform;
        }

        Vector3 RandomizePosition(Vector3 pos, int value = 0)
        {
            if (value == 0)
            {
                pos.x += (int)UnityEngine.Random.Range(cur_strategy.min, cur_strategy.max);
                pos.z += (int)UnityEngine.Random.Range(cur_strategy.min, cur_strategy.max);
            }

            return pos;
        }

        public void UpdateInitPos(Message m)
        {
            var param = CastHelper.Cast<UpdateInitPos>(m.parametrs);

            InitPos = param.transform;
        }

        public void Update1x1()
        {
            if (respowned + 1 >= resp_count)
            {
                resp_count = UnityEngine.Random.Range(2, 7);
                respowned = 0;
                timer = UnityEngine.Random.Range(cur_strategy.cool_down - 0.7f, cur_strategy.cool_down + 1.0f);

                if (resp_count == 0)
                    return;
            }
            else
            {
                timer = COOL_DOWM_bunch;
            }

            GameObject go = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("CatShow/coin"));
            go.GetComponent<CatShow.Movable>().enabled = false;
            go.GetComponent<Rune>().enabled = true;
            go.GetComponent<Rune>().msg_type = MiniGameMessageType.SCORE_RUNE_CATCHED;
            Vector3 tmp = RandomizePosition(InitPos.position);
            go.transform.position = new Vector3(tmp.x, 0.1f, tmp.z);
            go.transform.parent = Platform;
            respowned += 1;
        }

        public void Update2x2()
        {
            timer = UnityEngine.Random.Range(cur_strategy.cool_down - 0.7f, cur_strategy.cool_down + 1.0f);

            List<int> indexes = new List<int>() {1, 2, 7, 8 };


            int value = UnityEngine.Random.Range(3, 5);
            for (int i = 0; i < value; ++i)
            {
                int index = UnityEngine.Random.Range(0, indexes.Count);
                int o_index = indexes[index];
                GameObject go = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("CatShow/coin"));
                go.GetComponent<CatShow.Movable>().enabled = false;
                go.GetComponent<Rune>().enabled = true;
                go.GetComponent<Rune>().msg_type = MiniGameMessageType.SCORE_RUNE_CATCHED;
                Vector3 tmp = new Vector3(
                    InitPos.position.x + offsets[o_index][0], InitPos.position.y, InitPos.position.z + offsets[o_index][1]);
                indexes.RemoveAt(index);
                go.transform.position = new Vector3(tmp.x, 0.1f, tmp.z);
                go.transform.parent = Platform;
            }
        }


        public void Update3x3()
        {
            timer = UnityEngine.Random.Range(cur_strategy.cool_down - 0.7f, cur_strategy.cool_down + 1.0f);

            List<int> indexes = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            
            int value = UnityEngine.Random.Range(4, 10);
            for (int i = 0; i < value; ++i)
            {
                int index = UnityEngine.Random.Range(0, indexes.Count);
                int o_index = indexes[index];
                GameObject go = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("CatShow/coin"));
                go.GetComponent<CatShow.Movable>().enabled = false;
                go.GetComponent<Rune>().enabled = true;
                go.GetComponent<Rune>().msg_type = MiniGameMessageType.SCORE_RUNE_CATCHED;
                Vector3 tmp = new Vector3(
                    InitPos.position.x + offsets[o_index][0], InitPos.position.y, InitPos.position.z + offsets[o_index][1]);
                indexes.RemoveAt(index);
                go.transform.position = new Vector3(tmp.x, 0.1f, tmp.z);
                go.transform.parent = Platform;
            }
        }

        public void Update()
        {
            timer -= Time.deltaTime;

            if (timer < 0.0f)
            {
                UpdateAction();
            }
        }
    }
}
