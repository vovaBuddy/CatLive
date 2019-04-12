using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task17Initializer
    {
        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 0;
            int cur_task_index = 16;

            Task task = new Task(cur_task_index, 1, time_wait, 0, TextManager.getTaskName(17), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_015";

            task.BeforeCutScene = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);
            };

            task.CutSceneCondition = () => { return data.storable_data[14].done == true; };

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

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

            };

            task.DoneInitAction = () =>
            {

            };

            task.CheckActionConditions = () =>
            {
                if(task.data.current_action_index == 0 && CatsMoveController.GetController().DoesCatReachDestination(Cats.Jakky))
                {
                    task.in_action = false;

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);
                }

                else if (task.data.current_action_index == 2 && CatsMoveController.GetController().DoesCatReachDestination(Cats.Main))
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
                CatsMoveController.GetController().SetDestination(Cats.Jakky, "Point 48");
                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 47");
                CatsMoveController.GetController().SetDestination(Cats.Black, "Point 39");
                CameraMoveController.GetController().SetDestinations("CameraTasksTargets", "Saray");
                task.in_action = true;
            };
            tasc_action_1.condition_action = () => { return true; };

           TaskAction tasc_action_2 = new TaskAction();
            tasc_action_2.action = () =>
            {
                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(17, 1), DialogType.Djeki, DialogType.Main, DialogEntity.get_id(17, 1)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(17, 2), DialogType.Main, DialogType.Djeki, DialogEntity.get_id(17, 2)));
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
                task.in_action = true;
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
            };
            tasc_action_2.condition_action = () => { return true; };

            TaskAction tasc_action_3 = new TaskAction();
            tasc_action_3.action = () =>
            {
                CatsMoveController.GetController().SetDestination(Cats.Jakky, "Point 50");
                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 54");
                CameraMoveController.GetController().SetDestinations("CameraTasksTargets", "Three");

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(17, 3), DialogType.Black, DialogType.Main, DialogEntity.get_id(17, 3)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();
                });
                dialog.ShowDialog();
                task.in_action = true;
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);

            };
            tasc_action_3.condition_action = () => { return true; };

            TaskAction tasc_action_4 = new TaskAction();
            tasc_action_4.action = () =>
            {
                task.in_action = true;
                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(17, 4), DialogType.Main, DialogType.Black, DialogEntity.get_id(17, 4)));
                dialog.SetDialogs(deList);
                dialog.SetMissionIcon("task_icon_017");
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);

                    task.in_action = false;
                });
                dialog.ShowDialog();
            };
            tasc_action_4.condition_action = () => { return true; };


            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);
            task.TaskActions.Add(tasc_action_2);
            task.TaskActions.Add(tasc_action_3);
            task.TaskActions.Add(tasc_action_4);

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
