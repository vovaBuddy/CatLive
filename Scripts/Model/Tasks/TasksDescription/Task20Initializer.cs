using Main.Bubble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task20Initializer
    {

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 0;
            int cur_task_index = 19;

            Task task = new Task(cur_task_index, 2, time_wait, 0, TextManager.getTaskName(20), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_018";

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

                MainLocationOjects.instance.garden1_bushes.SetActiveTrueWithAnimation();

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(20, 1), DialogType.Black, DialogType.Main, DialogEntity.get_id(20, 1)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(20, 2), DialogType.Main, DialogType.Black, DialogEntity.get_id(20, 2)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_FOOTER);
                    //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                    Message m;
                    m.Type = MainScene.MainMenuMessageType.OPEN_CUSTOMIZER;
                    m.parametrs = new Yaga.CommonMessageParametr("garden");
                    MessageBus.Instance.SendMessage(m);

                    MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_joy") }, 5)));

                    dialog.CloseDialog();
                });

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
                dialog.ShowDialog();

            };

            task.DoneInitAction = () =>
            {
                MainLocationOjects.instance.garden1_bushes.SetActive(true);
            };

            TaskAction tasc_action_1 = new TaskAction();

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

            tasc_action_1.action = () =>
            {
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 70");
                CatsMoveController.GetController().SetDestination(Cats.Black, "Point 77");
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
