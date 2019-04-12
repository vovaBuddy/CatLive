using System;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using Yaga.Storage;
using UnityEngine.Analytics;
using MainScene;

namespace Task
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class TaskController : ExtendedBehaviour
    {
        float now;
        float delta;
        public void OnApplicationPause(bool paused)
        {
            if (paused)
            {
                now = Time.realtimeSinceStartup;
            }
            else
            {
                delta = Time.realtimeSinceStartup - now;
            }
        }

        bool variable
        {
            get { return variable; }
        }

        List<Task> Tasks;
        StorableData<DataEntity> DataStorage;
        List<Task> ActiveTasks;
        int mission_cnt;

        [Subscribe(MainScene.MainMenuMessageType.CUT_SCENE_SHOWED)]
        public void CutSceneShowed(Message msg)
        {
            var param = CastHelper.Cast<UpdateInt>(msg.parametrs);
            DataStorage.content.storable_data[param.value].cut_scene_showed = true;
            DataStorage.Store();

            Tasks[param.value].BeforeAction();

            if (Tasks[param.value].not_for_pb)
            {
                if (!ActiveTasks.Contains(Tasks[param.value]))
                    ActiveTasks.Add(Tasks[param.value]);

                GameStatistics.instance.SendStat("start_story_mission", param.value);
                DataStorage.content.storable_data[param.value].started = true;
                DataStorage.Store();
            }
        }

        public void Initialization()
        {
            DataStorage = new StorableData<DataEntity>("DataStorage");

            Tasks = new List<Task>();
            Tasks.Add(Task1Initializer.Init(DataStorage.content));
            Tasks.Add(Task2Initializer.Init(DataStorage.content));
            Tasks.Add(Task3Initializer.Init(DataStorage.content));
            Tasks.Add(Task4Initializer.Init(DataStorage.content));
            Tasks.Add(Task5Initializer.Init(DataStorage.content));
            Tasks.Add(Task6Initializer.Init(DataStorage.content));
            Tasks.Add(Task7Initializer.Init(DataStorage.content));
            Tasks.Add(Task8Initializer.Init(DataStorage.content));
            Tasks.Add(Task9Initializer.Init(DataStorage.content));
            Tasks.Add(Task10Initializer.Init(DataStorage.content));
            Tasks.Add(Task11Initializer.Init(DataStorage.content));
            Tasks.Add(Task12Initializer.Init(DataStorage.content));
            Tasks.Add(Task13Initializer.Init(DataStorage.content));
            Tasks.Add(Task14Initializer.Init(DataStorage.content));
            Tasks.Add(Task15Initializer.Init(DataStorage.content));
            Tasks.Add(Task16Initializer.Init(DataStorage.content));
            Tasks.Add(Task17Initializer.Init(DataStorage.content));
            Tasks.Add(Task18Initializer.Init(DataStorage.content));
            Tasks.Add(Task19Initializer.Init(DataStorage.content));
            Tasks.Add(Task20Initializer.Init(DataStorage.content));
            Tasks.Add(Task21Initializer.Init(DataStorage.content));
            Tasks.Add(Task22Initializer.Init(DataStorage.content));
            Tasks.Add(Task23Initializer.Init(DataStorage.content));
            Tasks.Add(Task24Initializer.Init(DataStorage.content));
            Tasks.Add(Chapter1Finish.Init(DataStorage.content));
            ActiveTasks = new List<Task>();
        }

        public int GetTaskCount()
        {
            return mission_cnt;
        }

        public int GetDoneTaskCount()
        {
            return DataStorage.content.done_mission_cnt;
        }

        public int GetDoneTaskInChapterCount()
        {
            if(DataController.instance.chapter_data.GetCurChapter() == 1)
                return DataStorage.content.done_mission_cnt;
            else 
                return DataStorage.content.done_mission_cnt - Main.Chapter.ChapterInfo.missions_in_chapter[0] - 1;
        }

        public Task GetTaskInfo(int index)
        {
            return Tasks[index];
        }

        public static TaskController GetController()
        {
            return GameObject.Find("Controllers").transform.Find("TasksController")
                .GetComponent<TaskController>();
        }

        public bool check_any_task_in_action()
        {
            if (Tasks == null)
                return false;

            foreach(var t in Tasks)
            {
                if (t.in_action)
                    return true;
            }

            return false;
        }

        [Subscribe(MainMenuMessageType.TASK_OPEN_ANIM_SHOWED)]
        public void OpenAnimShowed(Message msg)
        {
            var p = CastHelper.Cast<UpdateInt>(msg.parametrs);
            DataStorage.content.storable_data[p.value].ready_show = false;
            DataStorage.content.storable_data[p.value].idle = true;
            DataStorage.Store();
        }

        void CheckTaskActivity(Task task, bool init)
        {
            if(init)
            {
                if(!task.not_for_pb)
                {
                    mission_cnt++;
                }
            }

            if (!task.data.done && !task.init_done && task.CutSceneCondition())
            {                
                DataStorage.content.storable_data[task.index].ready_show = true;
                DataStorage.Store();

                task.Init();

                task.init_done = true;
            }
            //Если миссия уже выполненна, нужно применить результаты миссии
            if (task.data.done && init)
            {
                task.DoneInitAction();
            }

            //если миссия начата, но не выполненна, нужно выполнить текущее действие
            //и добавить текущую миссию в список активных
            else if (task.data.started && !task.data.done)
            {
                task.Idle();

                
                if (task.in_action)
                {
                    task.CheckActionConditions();
                    return;
                }

                if (task.data.current_action_index < task.TaskActions.Count)
                {
                    //bool in_main_view = (DataController.instance.tasks_storage.content.ContainsKey("catshow_scene") ?
                    //  ((bool)DataController.instance.tasks_storage.content["catshow_scene"] == false &&
                    //  (bool)DataController.instance.tasks_storage.content["mainhome_scene"] == true) : true) || task.index == 9;

                    var ta = task.TaskActions[task.data.current_action_index];
                    if (ta.condition_action() && !check_any_task_in_action() /*&& in_main_view*/) 
                    {
                        //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_CAT_SHOW);
                        //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_CUSTOMIZER);
                        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_SCAN_MENU);
                        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_TASK_LIST_ONLY);
                        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_SHOP);
                        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MINI_GAMES);

                        ta.action();
                    }
                }                

                if(!ActiveTasks.Contains(task))
                    ActiveTasks.Add(task);

                //if (task.show_int_task_list)
                //{
                //    Message msg = new Message();
                //    msg.Type = MainScene.MainMenuMessageType.ADD_NEW_TASK_ITEM;
                //    msg.parametrs = new Yaga.CommonMessageParametr(task);
                //    MessageBus.Instance.SendMessage(msg);

                //    task.show_int_task_list = false;
                //}
            }

            else if (!task.data.done && task.data.cut_scene_showed)
            {
                task.BeforeAction();
            }

            else if (!task.data.cut_scene_showed && task.CutSceneCondition() && !task.data.done && !check_any_task_in_action())
            {
                task.BeforeCutScene();
            }
        }

        void StartTask(int index)
        {
            GameStatistics.instance.SendStat("start_story_mission", index);


            Analytics.CustomEvent("start_story_mission", new Dictionary<string, object>
                    {
                        { "task_id", index }
                    });

            DataStorage.content.storable_data[index].started = true;
            DataStorage.Store();

            ActiveTasks.Add(Tasks[index]);
        }

        [Subscribe(MainScene.MainMenuMessageType.SOME_ACTION_DONE)]
        public void ActionDone(Message msg)
        {
            var param = CastHelper.Cast<UpdateInt>
                (msg.parametrs);

            DataStorage.content.storable_data[param.value].current_action_index += 1;
            DataStorage.Store();
        }

        [Subscribe(MainScene.MainMenuMessageType.CLOSE_TASK_LIST)]
        public void AfterCloseTL(Message msg)
        {
            foreach (var t in ActiveTasks)
            {
                if(t.data.done && !t.data.show_finish_in_pb)
                {
                    DataStorage.content.storable_data[t.index].show_finish_in_pb = true;
                    DataStorage.Store();
                }
            }
        }

        [Subscribe(MainScene.MainMenuMessageType.DONE_TASK_IN_PB)]
        public void DoneTaskInPB(Message msg)
        {
            var param = CastHelper.Cast<UpdateInt>
                (msg.parametrs);

            DataStorage.content.storable_data[param.value].show_finish_in_pb = true;
            DataStorage.Store();

            foreach (Task t in Tasks)
            {
                CheckTaskActivity(t, false);
            }
        }

        [Subscribe(MainScene.MainMenuMessageType.SPEED_UPED_TASK)]
        public void SpeedUpedTask(Message msg)
        {
            var param = CastHelper.Cast<MainScene.StartTaskParametrs>
                (msg.parametrs);

            Tasks[param.index].time_wait = 0;
        }

        bool need_set_false_to_actions = false;
        [Subscribe(MainScene.MainMenuMessageType.CLOSE_TASK_LIST)]
        public void OpenTaskList(Message msg)
        {
            need_set_false_to_actions = true;
        }


        [Subscribe(MainScene.MainMenuMessageType.STARTED_TASK)]
        public void StartTask(Message msg)
        {
            var param = CastHelper.Cast<MainScene.StartTaskParametrs>
                (msg.parametrs);

            StartTask(param.index);
        }

        public override void ExtendedStart()
        {

        }

        bool AllDone()
        {
            foreach(Task t in Tasks)
            {
                if (!t.data.done)
                    return false;
            }

            return true;
        }

        //tmp
        float end_timer = 2.0f;
        bool doned = false;

        float second_timer = 1.0f;
        bool start = true;
        public override void ExtendedUpdate()
        {
            if(need_set_false_to_actions)
            {
                need_set_false_to_actions = false;

                foreach(var t in Tasks)
                {
                    t.in_action = false;
                }
            }

            if (start)
            {
                Initialization();

                foreach (Task task in Tasks)
                {
                    CheckTaskActivity(task, true);
                }

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.INIT_TASKS);

                start = false;
            }

            second_timer -= Time.deltaTime;

            if (second_timer <= 0.0f)
            {
                var seconds = (int)(second_timer * -1);

                second_timer = 1.0f;

                foreach (Task task in ActiveTasks)
                {
                    task.time_wait -= 1;
                    task.time_wait -= (int)delta;
                    if (task.TickAction != null)
                    {
                        task.TickAction();
                    }
                }

                delta = 0;
            }

            foreach (Task task in ActiveTasks)
            {
                CheckTaskActivity(task, false);

                //bool in_main_view = DataController.instance.tasks_storage.content.ContainsKey("catshow_scene") ?
                //    ((bool)DataController.instance.tasks_storage.content["catshow_scene"] == false &&
                //    (bool)DataController.instance.tasks_storage.content["mainhome_scene"] == true) : true;

                    if (task.DoneCondition() && !check_any_task_in_action() /*&& in_main_view*/)
                {

                    GameStatistics.instance.SendStat("finish_story_mission", task.index);

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_CAT_SHOW);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_CUSTOMIZER);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_SCAN_MENU);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_TASK_LIST_ONLY);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_SHOP);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MINI_GAMES);

                    Analytics.CustomEvent("taskDone", new Dictionary<string, object>
                    {
                        { "task_id", task.index }
                    });

                    task.DoneAction();
                    ActiveTasks.Remove(task);

                    DataStorage.content.storable_data[task.index].done = true;                    

                    if (!task.not_for_pb)
                    {
                        Message msg = new Message();
                        msg.Type = MainScene.MainMenuMessageType.UPDATE_TASKS_PB;
                        var p = new List<int>();
                        p.Add(task.index);
                        msg.parametrs = new CommonMessageParametr(p);
                        MessageBus.Instance.SendMessage(msg);
                        DataStorage.content.done_mission_cnt++;
                    }

                    DataStorage.Store();

                    foreach (Task t in Tasks)
                    {
                        CheckTaskActivity(t, false);
                    }
                }                
            }

            if (AllDone() && !(bool)DataController.instance.tasks_storage.content["all_done"])
            {
                end_timer -= Time.deltaTime;

                if (end_timer < 0)
                {
                    DoneAction.Action();

                    DataController.instance.tasks_storage.content["all_done"] = true;
                    DataController.instance.tasks_storage.Store();
                }
            }
        }
    }
}
