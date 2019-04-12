using System;
using Yaga;
using System.Collections.Generic;
using Minigames;
using MainScene;

class StarTasksController : DontDestroy<StarTasksController>, Initable
{
    public StarTasks star_tasks;

    //public StarTask get_cur_task(string name)
    //{
    //    int index = star_tasks.tasks_entity.content.cur_task_indexes[(GameName)Enum.Parse(typeof(GameName), name)];
    //    return star_tasks.tasks_entity.content.tasks[(GameName)Enum.Parse(typeof(GameName), name)][index];
    //}

    public int get_cur_index()
    {
        return star_tasks.tasks_entity.content.cur_task_index;
    }

    public void DoneCurTask()
    {
        //int index = star_tasks.tasks_entity.content.cur_task_indexes[(GameName)Enum.Parse(typeof(GameName), name)];
        //star_tasks.tasks_entity.content.tasks[(GameName)Enum.Parse(typeof(GameName), name)][index].done = true;

        //star_tasks.tasks_entity.content.cur_task_indexes[(GameName)Enum.Parse(typeof(GameName), name)] += 1;

        star_tasks.tasks_entity.content.cur_task_index++;

        star_tasks.tasks_entity.Store();
    }

    public void Init()
    {
        star_tasks = new StarTasks();
    }
}

