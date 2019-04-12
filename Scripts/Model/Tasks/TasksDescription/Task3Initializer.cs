using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task3Initializer
    {
        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            Task task = new Task(2, 1, 0, 0, "3", false, true);
            task.data = data.storable_data[2];

            task.BeforeCutScene = () =>
            {
                
            };

            task.CutSceneCondition = () => { return false; };

            task.Init = () =>
            {
                //if (!DataController.instance.tasks_storage.content.ContainsKey("game_tutor_done"))
                //{
                //    DataController.instance.tasks_storage.content["game_tutor_done"] = false;
                //    DataController.instance.tasks_storage.Store();
                //}
                //else if((bool)DataController.instance.tasks_storage.content.ContainsKey("game_tutor_done") == true)
                //{
                //    if (!data.storable_data[task.index].done)
                //    {
                //        GameStatistics.instance.SendStat("start_story_mission", task.index);
                //        data.storable_data[task.index].started = true;
                //    }
                //}
            };

            task.BeforeAction = () => {
                if (!DataController.instance.tasks_storage.content.ContainsKey("game_tutor_done"))
                {
                    DataController.instance.tasks_storage.content["game_tutor_done"] = false;
                    DataController.instance.tasks_storage.Store();
                }
            };


            task.DoneAction = () =>
            {

            };

            task.DoneInitAction = () =>
            {
            };

            TaskAction tasc_action_1 = new TaskAction();

            tasc_action_1.action = () =>
            {
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU_GAMES);

                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);
            };
            tasc_action_1.condition_action = () => { return true; };


            TaskAction tasc_action_2 = new TaskAction();

            tasc_action_2.action = () =>
            {
                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(3, 4), DialogType.Main, DialogType.Black, DialogEntity.get_id(3, 4)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(3, 5), DialogType.Black, DialogType.Main, DialogEntity.get_id(3, 5)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(3, 6), DialogType.Black, DialogType.Main, DialogEntity.get_id(3, 6)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(3, 7), DialogType.Black, DialogType.Main, DialogEntity.get_id(3, 7)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();
                    task.in_action = false;

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);

                    DataController.instance.tasks_storage.content["game_tutor_done"] = true;
                    DataController.instance.tasks_storage.Store();

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                    //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                });
                dialog.ShowDialog();
                task.in_action = true;
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
            };
            tasc_action_2.condition_action = () => { return (bool)DataController.instance.tasks_storage.content["game_tutor_done"]; };

            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);
            task.TaskActions.Add(tasc_action_2);

            task.Idle = () =>
            {
            };

            task.CheckActionConditions = () => { };

            task.DoneCondition = () =>
            {
                return data.storable_data[task.index].current_action_index >= task.TaskActions.Count;
            };

            return task;
        }
    }
}
