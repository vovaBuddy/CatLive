using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Minigames;
using MainScene;
using Yaga.Storage;
using UnityEngine.Analytics;

public class StarTasks
{

    [Serializable]
    public class StarTasksEntity
    {
        public int cur_task_index;

        public StarTasksEntity()
        {
            cur_task_index = 0;
        }
    }

    public StarTasks()
    {
        tasks_entity = new StorableData<StarTasksEntity>("tm_star_tasks");

        Analytics.CustomEvent("minigame_indexes", new Dictionary<string, object>
            {
                { "cur_task_index", tasks_entity.content.cur_task_index}
            });
    }

    public StorableData<StarTasksEntity> tasks_entity;
}
