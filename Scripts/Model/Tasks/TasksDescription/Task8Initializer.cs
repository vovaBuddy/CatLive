using Main.Bubble;
using MainScene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task8Initializer
    {
        static ServeredTimer servered_timer = new ServeredTimer();

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 0;
            int cur_task_index = 7;

            Task task = new Task(cur_task_index, 1, time_wait, 500, TextManager.getTaskName(8), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_006";

            var subs = new MessageSubscriber();
            subs.MessageTypes = new string[1] { MainScene.MainMenuMessageType.CLOSE_CUSTOMIZER_MISSIONS };
            subs.action = (m) =>
            {
                if (task.in_action)
                {
                    task.in_action = false;

                    Message new_msg = new Message();
                    new_msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    new_msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(new_msg);
                }
            };
            MessageBus.Instance.AddSubscriber(subs);

            task.BeforeCutScene = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);
            };

            task.CutSceneCondition = () => { return data.storable_data[6].done == true; };

            task.Init = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.ADD_NEW_TASK_ITEM;
                msg.parametrs = new Yaga.CommonMessageParametr(task);
                MessageBus.Instance.SendMessage(msg);
            };

            task.BeforeAction = () => {

            };

            task.CheckActionConditions = () => { };


            task.DoneAction = () =>
            {
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                if (data.storable_data[8].done == false)
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);
                else
                    MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                        new BubbleCreateParametr(
                            CatsMoveController.GetController().main_cat, new List<string>()
                                {TextManager.getText("bubble_soft_floor") }, 5)));
            };

            task.DoneInitAction = () =>
            {
            };

            TaskAction tasc_action_1 = new TaskAction();
            tasc_action_1.action = () =>
            {
                task.in_action = true;

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_FOOTER);
                Message msg_cuts = new Message();
                msg_cuts.parametrs = new Yaga.CommonMessageParametr("Floor_Home");
                msg_cuts.Type = MainMenuMessageType.OPEN_CUSTOMIZER;
                MessageBus.Instance.SendMessage(msg_cuts);

                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 23");
                CatsMoveController.GetController().SetDestination(Cats.Black, "Point 7");
                CameraMoveController.GetController().SetDestinations("CameraTasksTargets", "Home");
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
