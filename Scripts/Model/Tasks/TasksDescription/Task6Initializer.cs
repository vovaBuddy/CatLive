using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task6Initializer
    {
        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            Task task = new Task(5, 0, 0, 0, "3", false, true);
            task.data = data.storable_data[task.index];

            task.BeforeCutScene = () =>
            {
                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(6, 1), DialogType.Black, DialogType.Main, DialogEntity.get_id(6, 1)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(6, 2), DialogType.Black, DialogType.Main, DialogEntity.get_id(6, 2)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                    MainScene.ArrowController.Instance.arrow_kitchen_set.SetActive(true);
                    MainScene.ArrowController.Instance.tutor_customize.SetActive(true);
                });
                dialog.ShowDialog();
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_TASK_LIST);
            };

            task.CutSceneCondition = () => {
                return false;
            };

            task.Init = () => 
            {
                if (!DataController.instance.tasks_storage.content.ContainsKey("custom_location_tutor"))
                {
                    DataController.instance.tasks_storage.content["custom_location_tutor"] = false;
                    DataController.instance.tasks_storage.Store();
                }
                else if ((bool)DataController.instance.tasks_storage.content.ContainsKey("custom_location_tutor") == true)
                {
                    if (!data.storable_data[task.index].done)
                    {
                        GameStatistics.instance.SendStat("start_story_mission", task.index);
                        data.storable_data[task.index].started = true;
                    }
                }
            };

            task.BeforeAction = () => {

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU_GARNITUR);

                if (!DataController.instance.tasks_storage.content.ContainsKey("game_tutor_done"))
                {
                    DataController.instance.tasks_storage.content["game_tutor_done"] = false;
                    DataController.instance.tasks_storage.Store();
                }
            };


            task.DoneAction = () =>
            {
                task.in_action = true;

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(6, 3), DialogType.Main, DialogType.Black, DialogEntity.get_id(6, 3)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);

                    task.in_action = false;
                });
                dialog.ShowDialog();
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_CUSTOMIZER);
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
                MainScene.ArrowController.Instance.arrow_kitchen_set.SetActive(false);
                MainScene.ArrowController.Instance.tutor_customize.SetActive(false);
            };

            task.DoneInitAction = () =>
            {
            };


            task.TaskActions = new List<TaskAction>();

            task.Idle = () =>
            {
            };


            task.DoneCondition = () =>
            {
                return (bool)DataController.instance.tasks_storage.content["custom_location_tutor"];
            };

            return task;
        }
    }
}
