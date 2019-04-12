using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task5Initializer
    {
        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            Task task = new Task(4, 1, 0, 0, TextManager.getTaskName(5), true, false);
            task.data = data.storable_data[4];

            task.icon_name = "task_icon_004";

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
                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(4, 1), DialogType.Main, DialogType.Black, DialogEntity.get_id(4, 1)));
                dialog.SetDialogs(deList);
                dialog.SetMissionIcon(task.icon_name);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                    //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                });
                dialog.ShowDialog();
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_TASK_LIST);
            };

            task.CutSceneCondition = () => { return data.storable_data[3].done == true; };

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
            };

            task.DoneInitAction = () =>
            {
                // MainLocationOjects.instance.kitchen.SetActive(true);
            };

            TaskAction task_action_1 = new TaskAction();
            task_action_1.condition_action = () => { return true; };
            task_action_1.action = () =>
            {
                List<Vector3> points = new List<Vector3>();
                Transform point = GameObject.Find("CameraTasksTargets").transform
                .Find("Kitchen");

                points.Add(point.position);

                CameraMoveController.GetController().SetDestinations(points);

                task.in_action = true;

                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.OPEN_CUSTOMIZER;
                msg.parametrs = new Yaga.CommonMessageParametr("Wall_Kitchen");
                MessageBus.Instance.SendMessage(msg);

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_FOOTER);
            };

            TaskAction task_action_2 = new TaskAction();
            task_action_2.condition_action = () => { return true; };
            task_action_2.action = () =>
            {

                task.in_action = true;

                var cust_subs = new MessageSubscriber();
                cust_subs.MessageTypes = new string[1] { MainScene.MainMenuMessageType.OPEN_CUSTOMIZER_WITH_CLOSE };
                cust_subs.action = (m) =>
                {
                    if (task.in_action)
                    {
                        GameStatistics.instance.SendStat("tutor_pressed_double_tap_customize", 0);

                        MainLocationOjects.instance.arrow_kitchen.SetActive(false);
                        MainLocationOjects.instance.customize_info_tutor.SetActive(false);                        
                    }
                };
                MessageBus.Instance.AddSubscriber(cust_subs);

                var cust_subs2 = new MessageSubscriber();
                cust_subs2.MessageTypes = new string[1] { MainScene.MainMenuMessageType.CLOSE_CUSTOMIZER_MISSIONS };
                cust_subs2.action = (m) =>
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
                MessageBus.Instance.AddSubscriber(cust_subs2);

                List<Vector3> points = new List<Vector3>();
                Transform point = GameObject.Find("CameraTasksTargets").transform
                .Find("Kitchen");

                points.Add(point.position);

                CameraMoveController.GetController().SetDestinations(points);

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(4, 2), DialogType.Black, DialogType.Main, DialogEntity.get_id(4, 2)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);

                    MainLocationOjects.instance.arrow_kitchen.SetActive(true);
                    MainLocationOjects.instance.customize_info_tutor.SetActive(true);
                });
                dialog.ShowDialog();
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
            };

            TaskAction task_action_3 = new TaskAction();
            task_action_3.condition_action = () => { return true; };
            task_action_3.action = () =>
            {
                task.in_action = true;

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(4, 3), DialogType.Main, DialogType.One, DialogEntity.get_id(4, 3)));
                dialog.SetMissionIcon("task_icon_005");
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();

                    task.in_action = false;

                    Message new_msg = new Message();
                    new_msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    new_msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(new_msg);

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                });
                dialog.ShowDialog();
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
            };

            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(task_action_1);
            task.TaskActions.Add(task_action_2);
            task.TaskActions.Add(task_action_3);

            task.Idle = () =>
            {
            };

            task.CheckActionConditions = () =>
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
