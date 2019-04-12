using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Yaga.MessageBus;

namespace TimeManager.Level
{
    public delegate bool ConditionAction();

    public class Level : MonoBehaviour
    {
        public List<ConditionAction> win_conditions;
        public List<ConditionAction> lose_conditions;
        public int id;

        public Level(int i)
        {
            id = i;
            lose_conditions = new List<ConditionAction>();
            win_conditions = new List<ConditionAction>();
        }

        public bool check_lose()
        {
            for (int i = lose_conditions.Count - 1; i >= 0; --i)
            {
                if (lose_conditions[i]())
                {
                    return true;
                }
            }

            return false;
        }

        public bool check_win()
        {
            Debug.Log("win_conditions.Count " + win_conditions.Count);

            for (int i = win_conditions.Count - 1; i >= 0; --i)
            {
                if(win_conditions[i]())
                {
                    win_conditions.RemoveAt(i);

                    Debug.Log("done condition");
                }
            }

            return win_conditions.Count == 0;
        }
    }
}
