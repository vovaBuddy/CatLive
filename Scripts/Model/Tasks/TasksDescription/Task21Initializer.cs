using Main.Bubble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task21Initializer
    {

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 0;
            int cur_task_index = 20;

            Task task = new Task(cur_task_index, 1, time_wait, 0, TextManager.getTaskName(21), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_019";

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
            };

            task.DoneInitAction = () =>
            {
            };

            task.CheckActionConditions = () =>
            {
                if(task.data.current_action_index == 0 && CameraMoveController.GetController().DoesReachDestination())
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
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
                CameraMoveController.GetController().SetDestinations("CameraTasksTargets", "Garden_1");
                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 70");
                CatsMoveController.GetController().SetDestination(Cats.Black, "Point 77");
                task.in_action = true;
            };
            tasc_action_1.condition_action = () => { return true; };

            TaskAction tasc_action_2 = new TaskAction();
            tasc_action_2.action = () =>
            {
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.DRESS_NORMAL_GARDEN_FLOOR);

                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_soft_floor") }, 5)));
            };
            tasc_action_2.condition_action = () => { return true; };
            

            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);
            task.TaskActions.Add(tasc_action_2);

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
