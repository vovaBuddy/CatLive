using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks
{
    public delegate bool CompleteCondition(TaskEntity e);

    [Serializable]
    public class TaskEntity
    {
        public int stars_price;
        public int time_wait;
        public int speed_up_price;
        public bool done;
        public bool finished;
        public bool started;
        public bool first_showed;
        public string name;

        public TaskEntity(int stars, int time, int speedup, string n, bool first)
        {
            stars_price = stars;
            time_wait = time;
            speed_up_price = speedup;
            done = false;
            started = false;
            name = n;
            first_showed = first;
            finished = false;
        }
    }
}