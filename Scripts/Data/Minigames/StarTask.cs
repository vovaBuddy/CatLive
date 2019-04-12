using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minigames
{
    public enum TaskType
    {
        COINS, 
        POINTS,
        TIME, 
        TIME_OUT
    }

    [Serializable]
    public class TaskInfo
    {
        public TaskType type;
        public int value;

        public TaskInfo(TaskType t, int v)
        {
            type = t;
            value = v;
        }
    }

    [Serializable]
    public class StarTask
    {
        public List<TaskInfo> task_info;
        public bool done;

        public StarTask()
        {
            done = false;
            task_info = new List<TaskInfo>();
        }
    }
}
