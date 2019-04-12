using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task19Initializer
    {

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 0;
            int cur_task_index = 18;

            Task task = new Task(cur_task_index, 2, time_wait, 0, TextManager.getTaskName(19), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_017";

            task.BeforeCutScene = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);
            };

            task.CutSceneCondition = () => { return data.storable_data[16].done == true; };

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
                MainLocationOjects.instance.garden2_bran_closed.SetActive(false);
                MainLocationOjects.instance.garden2_bran.SetActive(true);
            };

            task.CheckActionConditions = () =>
            {
                if(task.data.current_action_index == 0 && CatsMoveController.GetController().DoesCatReachDestination(Cats.Main))
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
                CatsMoveController.GetController().SetDestination(Cats.Jakky, "Point 46");
                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 48");
                CameraMoveController.GetController().SetDestinations("CameraTasksTargets", "Saray");
                task.in_action = true;
            };
            tasc_action_1.condition_action = () => { return true; };

            TaskAction tasc_action_2 = new TaskAction();
            tasc_action_2.action = () =>
            {
                task.in_action = true;
                MainLocationOjects.instance.garden2_bran_closed.SetActive(false);
                MainLocationOjects.instance.garden2_bran.SetActiveTrueWithAnimation();

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(19, 1), DialogType.Main, DialogType.One, DialogEntity.get_id(19, 1)));
                dialog.SetDialogs(deList);
                dialog.SetMissionIcon("task_icon_018");
                dialog.SetMissionIcon("task_icon_019");
                dialog.SetMissionIcon("task_icon_021");
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
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
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
