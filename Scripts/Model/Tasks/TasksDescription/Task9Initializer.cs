using Main.Bubble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task9Initializer
    {
        static ServeredTimer servered_timer = new ServeredTimer();

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 0;
            int cur_task_index = 8;

            bool done_dialog = false;

            object[] time_msg_parametr_values = new object[2];
            time_msg_parametr_values[0] = TimerController.GetController().task9_timer;
            var time_msg_param = new Yaga.CommonMessageParametr(time_msg_parametr_values);
            Message timer_msg = new Message();
            timer_msg.Type = MainScene.MainMenuMessageType.TASK_TIME_UPDATE;
            timer_msg.parametrs = time_msg_param;

            Task task = new Task(cur_task_index, 1, time_wait, 500, TextManager.getTaskName(9), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_007";

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


            task.DoneAction = () =>
            {
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                //if(data.storable_data[7].done == false)
                //    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                new BubbleCreateParametr(
                    CatsMoveController.GetController().main_cat, new List<string>()
                        {TextManager.getText("bubble_relax") }, 5)));
            };

            task.DoneInitAction = () =>
            {
                MainLocationOjects.instance.tv_zone.SetActive(true);
            };

            task.TickAction = () =>
            {
                if (done_dialog)
                {
                    time_msg_parametr_values[1] = task.time_wait;
                    MessageBus.Instance.SendMessage(timer_msg);
                }
            };

            TaskAction tasc_action_1 = new TaskAction();
            tasc_action_1.action = () =>
            {
                MainLocationOjects.instance.tv_zone.SetActiveTrueWithAnimation();
                task.in_action = true;

                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 23");
                List<Vector3> points = new List<Vector3>();
                Transform point = GameObject.Find("CameraTasksTargets").transform
                .Find("Living_room");
                points.Add(point.position);
                CameraMoveController.GetController().SetDestinations(points);

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_FOOTER);

                Message msg_cuts = new Message();
                msg_cuts.parametrs = new Yaga.CommonMessageParametr("Sofa");
                msg_cuts.Type = MainScene.MainMenuMessageType.OPEN_CUSTOMIZER;
                MessageBus.Instance.SendMessage(msg_cuts);
            };
            tasc_action_1.condition_action = () => { return true; };
            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);

            task.CheckActionConditions = () =>
            {
            };

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
