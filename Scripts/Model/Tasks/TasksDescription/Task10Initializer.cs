using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task10Initializer
    {

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int cur_task_index = 9;

            Task task = new Task(cur_task_index, 2, 0, 0, TextManager.getTaskName(10), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_008";

            task.BeforeCutScene = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);
            };

            //task.CutSceneCondition = () => { return data.storable_data[8].done == true; };
            task.CutSceneCondition = () => { return false; };

            task.Init = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.ADD_NEW_TASK_ITEM;
                msg.parametrs = new Yaga.CommonMessageParametr(task);
                MessageBus.Instance.SendMessage(msg);

                if (!DataController.instance.tasks_storage.content.ContainsKey("catshow_scene"))
                {
                    DataController.instance.tasks_storage.content["catshow_scene"] = false;
                    DataController.instance.tasks_storage.Store();
                }
                
                if (!DataController.instance.tasks_storage.content.ContainsKey("catshow_first_played"))
                {
                    DataController.instance.tasks_storage.content["catshow_first_played"] = false;
                    DataController.instance.tasks_storage.Store();
                }

                if (!DataController.instance.tasks_storage.content.ContainsKey("mainhome_scene"))
                {
                    DataController.instance.tasks_storage.content["mainhome_scene"] = false;
                    DataController.instance.tasks_storage.Store();
                }

                if (!DataController.instance.tasks_storage.content.ContainsKey("first_shopped"))
                {
                    DataController.instance.tasks_storage.content["first_shopped"] = false;
                    DataController.instance.tasks_storage.Store();
                }
            };

            task.BeforeAction = () => {

            };


            task.DoneAction = () =>
            {
                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 1), DialogType.Black, DialogType.Main, DialogEntity.get_id(10, 1)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 2), DialogType.Main, DialogType.Black, DialogEntity.get_id(10, 2)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 3), DialogType.Main, DialogType.Black, DialogEntity.get_id(10, 3)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 4), DialogType.Main, DialogType.Black, DialogEntity.get_id(10, 4)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 5), DialogType.Black, DialogType.Main, DialogEntity.get_id(10, 5)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 6), DialogType.Main, DialogType.Black, DialogEntity.get_id(10, 6)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);
                });
                dialog.ShowDialog();
                task.in_action = true;
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
            };

            task.DoneInitAction = () =>
            {
            };

            TaskAction tasc_action_1 = new TaskAction();
            tasc_action_1.action = () =>
            {
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 7), DialogType.Call_Worker, DialogType.Main, DialogEntity.get_id(10, 7)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 8), DialogType.Call_Worker, DialogType.Main, DialogEntity.get_id(10, 8)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 9), DialogType.Call_Worker, DialogType.Main, DialogEntity.get_id(10, 9)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();
                    task.in_action = false;

                    Message new_msg = new Message();
                    new_msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    new_msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(new_msg);

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU_CATSHOW);
                    MainScene.ArrowController.Instance.arrow_cat_show.SetActive(true);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                });
                dialog.ShowDialog();
                task.in_action = true;
            };
            tasc_action_1.condition_action = () => { return true; };

            TaskAction tasc_action_2 = new TaskAction();
            tasc_action_2.action = () =>
            {
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 10), DialogType.Main, DialogType.One, DialogEntity.get_id(10, 10)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 11), DialogType.Main, DialogType.One, DialogEntity.get_id(10, 11)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 12), DialogType.Main, DialogType.One, DialogEntity.get_id(10, 12)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 13), DialogType.Main, DialogType.One, DialogEntity.get_id(10, 13)));

                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();
                    task.in_action = false;

                    Message new_msg = new Message();
                    new_msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    new_msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(new_msg);

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_CAT_SHOW);
                    MainScene.ArrowController.Instance.arrow_play_show.SetActive(true);
                });
                dialog.ShowDialog();
                task.in_action = true;
            };
            tasc_action_2.condition_action = () => { return (bool)DataController.instance.tasks_storage.content["catshow_scene"]; };

            TaskAction tasc_action_3 = new TaskAction();
            tasc_action_3.action = () =>
            {
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 14), DialogType.Main, DialogType.One, DialogEntity.get_id(10, 14)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();
                    task.in_action = false;

                    Message new_msg = new Message();
                    new_msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    new_msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(new_msg);

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_CAT_SHOW);

                    MainScene.ArrowController.Instance.arrow_shop.SetActive(true);
                    MessageBus.Instance.SendMessage(CatShow.CatShowMessageType.CAT_SHOW_SHOP_BTN);
                });
                dialog.ShowDialog();
                task.in_action = true;
            };
            tasc_action_3.condition_action = () => { return (bool)DataController.instance.tasks_storage.content["catshow_first_played"]; };

            TaskAction tasc_action_4 = new TaskAction();
            tasc_action_4.action = () =>
            {
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 15), DialogType.Main, DialogType.One, DialogEntity.get_id(10, 15)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 16), DialogType.Main, DialogType.One, DialogEntity.get_id(10, 16)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(10, 17), DialogType.Main, DialogType.One, DialogEntity.get_id(10, 17)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();
                    task.in_action = false;

                    Message new_msg = new Message();
                    new_msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    new_msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(new_msg);

                    MessageBus.Instance.SendMessage(CatShow.CatShowMessageType.CAT_SHOW_MAIN_BTN);
                    //MessageBus.Instance.SendMessage(CatShow.CatShowMessageType.CAT_SHOW_SELFIE_BTN);
                });
                dialog.ShowDialog();
                task.in_action = true;

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_SHOP);
            };
            tasc_action_4.condition_action = () => { return (bool)DataController.instance.tasks_storage.content["first_shopped"]; };

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
                return (bool)DataController.instance.tasks_storage.content["mainhome_scene"]; ;
            };

            return task;
        }
    }
}
