using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task11Initializer
    {
        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 0;
            int cur_task_index = 10;

            Task task = new Task(cur_task_index, 2, time_wait, 0, TextManager.getTaskName(11), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_009";

            task.BeforeCutScene = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);
            };

            task.CutSceneCondition = () => { return data.storable_data[24].done == true; };

            task.Init = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.ADD_NEW_TASK_ITEM;
                msg.parametrs = new Yaga.CommonMessageParametr(task);
                MessageBus.Instance.SendMessage(msg);
            };

            task.BeforeAction = () => {
            };


            task.DoneAction = () =>
            {
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.TOGGLE_MAIN_MENU_SOCIAL_BTN);
            };

            task.DoneInitAction = () =>
            {
            };

            TaskAction task_action_1 = new TaskAction();
            task_action_1.condition_action = () => { return true; };
            task_action_1.action = () =>
            {
                List<Vector3> points = new List<Vector3>();
                Transform point = GameObject.Find("CameraTasksTargets").transform
                .Find("Toliet");

                points.Add(point.position);

                CameraMoveController.GetController().SetDestinations(points);

                task.in_action = true;
            };

            TaskAction task_action_2 = new TaskAction();
            task_action_2.condition_action = () => { return true; };
            task_action_2.action = () =>
            {
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.DRESS_DEFAULT_HOME_WALL);
                task.in_action = true;
            };

            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(task_action_1);
            task.TaskActions.Add(task_action_2);

            task.Idle = () =>
            {
            };

            float timer = -1.0f;
            task.CheckActionConditions = () =>
            {
                if (task.data.current_action_index == 0 && CameraMoveController.GetController().DoesReachDestination())
                {
                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);
                    task.in_action = false;
                }
                if (task.data.current_action_index == 1)
                {
                    if (timer == -1.0f)
                    {
                        timer = 1.5f;
                    }

                    timer -= Time.deltaTime;

                    if (timer <= 0.0f)
                    {
                        task.in_action = false;

                        Message msg = new Message();
                        msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                        msg.parametrs = new UpdateInt(task.index);
                        MessageBus.Instance.SendMessage(msg);
                    }
                }
            };


            task.DoneCondition = () =>
            {
                return task.data.current_action_index >= task.TaskActions.Count;
            };

            return task;
        }
    }
}
