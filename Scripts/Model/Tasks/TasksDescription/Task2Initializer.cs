using Main.Bubble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task2Initializer
    {
        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            Task task = new Task(1, 1, 0, 0, TextManager.getTaskName(2), true, false);
            task.data = data.storable_data[1];

            task.icon_name = "task_icon_002";

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
                CatsMoveController.GetController().
                    SetDestination(Cats.Black, "Point 11");

                CatsMoveController.GetController().
                    SetDestination(Cats.Main, "Point 10");
                CameraMoveController.GetController().GoCat();

                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);
                
                
                //List<DialogEntity> deList = new List<DialogEntity>();
                ////deList.Add(new DialogEntity(
                ////    TextManager.getDialogsText(2, 5), DialogType.Black, DialogType.Main, DialogEntity.get_id(2, 5)));
                ////deList.Add(new DialogEntity(
                ////    TextManager.getDialogsText(2, 6), DialogType.Main, DialogType.Black, DialogEntity.get_id(2, 6)));
                ////dialog.SetDialogs(deList);
                //dialog.SetMissionIcon(task.icon_name);
                //dialog.SetBtnAction(() =>
                //{
                //    dialog.CloseDialog();
                //    CameraMoveController.GetController().ClearDestination();



                //    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                //});
                //dialog.ShowDialog();
            };

            task.CutSceneCondition = () => { return data.storable_data[0].done; };

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
                MainLocationOjects.instance.kitchen.SetActiveTrueWithAnimation();

                MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                new BubbleCreateParametr(
                    CatsMoveController.GetController().main_cat, new List<string>()
                        {TextManager.getText("bubble_joy") }, 5)));
            };

            task.DoneInitAction = () =>
            {
                MainLocationOjects.instance.kitchen.SetActiveTrueWithAnimation();
            };

            TaskAction action_1 = new TaskAction();
            action_1.condition_action = () => { return true; };
            action_1.action = () =>
            {
                task.in_action = true;

                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.OPEN_CUSTOMIZER;
                msg.parametrs = new Yaga.CommonMessageParametr("Kitchen_set");
                MessageBus.Instance.SendMessage(msg);

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_FOOTER);
            };


            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(action_1);

            task.DoneCondition = () =>
            {
                return task.data.current_action_index >= task.TaskActions.Count;
            };

            task.Idle = () => { };

            task.CheckActionConditions = () =>
            {
                if(task.data.current_action_index == 0)
                {
                    //if(CatsMoveController.GetController().DoesCatReachDestination(Cats.Black) &&
                    //CatsMoveController.GetController().DoesCatReachDestination(Cats.Main))
                    //{
                    //    task.in_action = false;

                    //    Message new_msg = new Message();
                    //    new_msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    //    new_msg.parametrs = new UpdateInt(task.index);
                    //    MessageBus.Instance.SendMessage(new_msg);
                    //}
                }
            };

            return task;
        }
    }
}
