using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task14Initializer
    {

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 0;
            int cur_task_index = 13;

            Task task = new Task(cur_task_index, 2, time_wait, 0, TextManager.getTaskName(14), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_012";

            task.BeforeCutScene = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);
            };

            task.CutSceneCondition = () => { return data.storable_data[11].done == true; };

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
                MainLocationOjects.instance.door_garden.SetActive(false);
                MainLocationOjects.instance.door_wc.SetActive(false);
            };

            task.CheckActionConditions = () =>
            {
                if((task.data.current_action_index == 1 || task.data.current_action_index == 2) && 
                    CatsMoveController.GetController().DoesCatReachDestination(Cats.Jakky))
                {
                    MainLocationOjects.instance.door_wc.SetActive(false);

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);

                    task.in_action = false;
                }

                else if ((task.data.current_action_index == 3) &&
                    CatsMoveController.GetController().DoesCatReachDestination(Cats.Main))
                {
                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);

                    task.in_action = false;
                }
            };

            TaskAction tasc_action_1 = new TaskAction();
            tasc_action_1.action = () =>
            {

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(14, 1), DialogType.Djeki, DialogType.Main, DialogEntity.get_id(14, 1)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(14, 2), DialogType.Main, DialogType.Djeki, DialogEntity.get_id(14, 2)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(14, 3), DialogType.Djeki, DialogType.Main, DialogEntity.get_id(14, 3)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);

                    task.in_action = false;
                });
                dialog.ShowDialog();

                CatsMoveController.GetController().ActiveCat(Cats.Jakky);
                CatsMoveController.GetController().SetDestination(Cats.Jakky, "Point 55");
                task.in_action = true;


                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
            };
            tasc_action_1.condition_action = () => { return true; };


            TaskAction tasc_action_2 = new TaskAction();
            tasc_action_2.action = () =>
            {
                CatsMoveController.GetController().SetDestination(Cats.Jakky, "Point 57");
                task.in_action = true;
            };
            tasc_action_2.condition_action = () => { return true; };

            TaskAction tasc_action_3 = new TaskAction();
            tasc_action_3.action = () =>
            {
                CatsMoveController.GetController().SetDestination(Cats.Jakky, "Point 35");
                task.in_action = true;
            };
            tasc_action_3.condition_action = () => { return true; };

            TaskAction tasc_action_4 = new TaskAction();
            tasc_action_4.action = () =>
            {
                MainLocationOjects.instance.door_garden.SetActive(false);

                CatsMoveController.GetController().SetDestination(Cats.Black, "Point 61");
                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 45");

                task.in_action = true;
            };
            tasc_action_4.condition_action = () => { return true; };

            TaskAction tasc_action_5 = new TaskAction();
            tasc_action_5.action = () =>
            {
                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(14, 4), DialogType.Black, DialogType.One, DialogEntity.get_id(14, 4)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);
                });
                dialog.ShowDialog();

                CameraMoveController.GetController().SetPosition(
                    CatsMoveController.GetController().GetCatLocation(Cats.Black));

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
            };
            tasc_action_5.condition_action = () => { return true; };

            TaskAction tasc_action_6 = new TaskAction();
            tasc_action_6.action = () =>
            {
                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(14, 5), DialogType.Main, DialogType.One, DialogEntity.get_id(14, 5)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);
                });
                dialog.ShowDialog();

                CameraMoveController.GetController().SetPosition(
                    CatsMoveController.GetController().GetCatLocation(Cats.Main));

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
            };
            tasc_action_6.condition_action = () => { return true; };

            TaskAction tasc_action_7 = new TaskAction();
            tasc_action_7.action = () =>
            {
                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(14, 6), DialogType.Black, DialogType.Main, DialogEntity.get_id(14, 6)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);
                });
                dialog.ShowDialog();

                List<Vector3> points_main2 = new List<Vector3>();
                Transform point = GameObject.Find("CameraTasksTargets").transform
                .Find("Task14").transform.Find("Destinations");
                for (int i = 0; i < point.childCount; ++i)
                {
                    points_main2.Add(point.GetChild(i).position);
                }
                CameraMoveController.GetController().SetDestinations(points_main2);

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
            };
            tasc_action_7.condition_action = () => { return true; };

            TaskAction tasc_action_8 = new TaskAction();
            tasc_action_8.action = () =>
            {
                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(14, 7), DialogType.Main, DialogType.Black, DialogEntity.get_id(14, 7)));
                dialog.SetDialogs(deList);
                dialog.SetMissionIcon("task_icon_013");
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                    task.in_action = false;
                });
                dialog.ShowDialog();

                task.in_action = true;

                //камера на детской

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
            };
            tasc_action_8.condition_action = () => { return true; };

            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);
            task.TaskActions.Add(tasc_action_2);
            task.TaskActions.Add(tasc_action_3);
            task.TaskActions.Add(tasc_action_4);
            task.TaskActions.Add(tasc_action_5);
            task.TaskActions.Add(tasc_action_6);
            task.TaskActions.Add(tasc_action_7);
            task.TaskActions.Add(tasc_action_8);

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
