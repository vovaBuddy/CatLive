using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task23Initializer
    {

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 0;
            int cur_task_index = 22;

            Task task = new Task(cur_task_index, 1, time_wait, 0, TextManager.getTaskName(23), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_021";

            task.BeforeCutScene = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);
            };

            task.CutSceneCondition = () => { return data.storable_data[18].done == true; };

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
                task.in_action = true;

                MainLocationOjects.instance.garden1_benches.SetActiveTrueWithAnimation();
                //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_FOOTER);
                //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                Message m;
                m.Type = MainScene.MainMenuMessageType.OPEN_CUSTOMIZER;
                m.parametrs = new Yaga.CommonMessageParametr("bench");
                MessageBus.Instance.SendMessage(m);


            };

            task.DoneInitAction = () =>
            {
                MainLocationOjects.instance.garden1_benches.SetActive(true);
            };


            task.CheckActionConditions = () =>
            {
                if (task.data.current_action_index == 0 && CameraMoveController.GetController().DoesReachDestination())
                {
                    task.in_action = false;

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);
                }
            };

            TaskAction tasc_action_1 = new TaskAction();
            tasc_action_1.action = () =>
            {
                CameraMoveController.GetController().SetDestinations("CameraTasksTargets", "Garden_1");
                task.in_action = true;
            };
            tasc_action_1.condition_action = () => { return true; };

            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);

            task.Idle = () =>
            {

            };

            task.DoneCondition = () =>
            {
                return task.data.current_action_index >= task.TaskActions.Count;
            };

            return task;
        }
    }
}
