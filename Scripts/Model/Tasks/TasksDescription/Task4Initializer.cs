using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task4Initializer
    {
        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            Task task = new Task(3, 1, 0, 0, TextManager.getTaskName(4), true, false);
            task.data = data.storable_data[3];

            task.icon_name = "task_icon_003";

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
                    TextManager.getDialogsText(3, 1), DialogType.Black, DialogType.Main, DialogEntity.get_id(3, 1)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(3, 2), DialogType.Main, DialogType.Black, DialogEntity.get_id(3, 2)));
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

            task.CutSceneCondition = () => { return data.storable_data[1].done == true; };

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
                MessageBus.Instance.SendMessage(new Message(Main.Bubble.BubbleAPI.PUSH_TO_QUEUE,
                    new Main.Bubble.BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            { TextManager.getText("bubble_task_3_1"),
                                TextManager.getText("bubble_thansk").Replace("%USERNAME%", DataController.instance.catsPurse.Name)}, 8)));
                //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);   
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
                msg.parametrs = new Yaga.CommonMessageParametr("Floor_Kitchen");
                MessageBus.Instance.SendMessage(msg);

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_FOOTER);
            };


            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(task_action_1);

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
