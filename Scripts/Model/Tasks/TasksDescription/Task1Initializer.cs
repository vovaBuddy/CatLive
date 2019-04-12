using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Yaga.MessageBus;

namespace Task
{
    public static class Task1Initializer
    {
        public class Messages
        {
            public const string REMOVE_TRASH = "Task2Initializer.REMOVE_TRASH";
        }

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            Task task = new Task(0, 1, 0, 0, TextManager.getTaskName(1), true, false);
            task.data = data.storable_data[0];

            task.icon_name = "task_icon_001";

            //Initiate remove trash action, and subscribe
            var subs = new MessageSubscriber();
            subs.MessageTypes = new string[1] { Messages.REMOVE_TRASH };
            subs.action = (msg) => {
                MainLocationOjects.instance.trash.GetComponent<Animator>().SetBool("end", true);
                MainLocationOjects.instance.trash.transform.Find("trash_001").GetComponent<SpriteRenderer>().enabled = false;
                //MainLocationOjects.instance.trash_scanner.SetActive(true);
                MainLocationOjects.instance.trash_pack.SetActive(true);
            };
            MessageBus.Instance.AddSubscriber(subs);

            task.BeforeCutScene = () =>
            {
                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(TextManager.getDialogsText(1, 1), DialogType.Main, DialogType.One, DialogEntity.get_id(1, 1)));
                deList.Add(new DialogEntity(TextManager.getDialogsText(1, 2), DialogType.Main, DialogType.One, DialogEntity.get_id(1, 2)));
                deList.Add(new DialogEntity(TextManager.getDialogsText(1, 3), DialogType.Main, DialogType.One, DialogEntity.get_id(1, 3)));
                dialog.SetDialogs(deList);

                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);

                    var m = new Message();
                    m.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                    m.parametrs = new UpdateInt(0);
                    MessageBus.Instance.SendMessage(m);
                });
                dialog.ShowDialog();
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);

                CameraMoveController.GetController().GoCatOnce();
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.ZOOM_CAMERA);                
            };

            task.CutSceneCondition = () => { return true; };

            task.BeforeAction = () => {
                //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                if (DataController.instance.catsPurse.Stars == 0)
                {

                    MessageBus.Instance.SendMessage(Main.StarsScreen.API.Messages.HIDE_CLOSE_BTN);

                    MessageBus.Instance.SendMessage(
                        new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
                            new TutorMaskController.TutorMaskParametr(
                                MainLocationOjects.instance.tasks_btn_footer.transform.position, false, true,
                                "tasks_btn_footer")));

                    MessageBus.Instance.SendMessage(
                        new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
                            new TutorMaskController.TutorMaskParametr(
                                MainLocationOjects.instance.taks_1_btn.GetComponent<RectTransform>().anchoredPosition, true, true, 
                                "task_list_task1_btn")));

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU_GAME_BTN);

                    //MessageBus.Instance.SendMessageAfterEvent(MainScene.MainMenuMessageType.OPEN_MINI_GAMES,
                    //    new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
                    //        new TutorMaskController.TutorMaskParametr(
                    //            MainLocationOjects.instance.minigames_play_star_btn.GetComponent<RectTransform>().anchoredPosition, true, false,
                    //            "minigames_play_star_btn")));
                }
                else
                {
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                    MessageBus.Instance.SendMessage(
                        new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
                            new TutorMaskController.TutorMaskParametr(
                                MainLocationOjects.instance.taks_1_btn.GetComponent<RectTransform>().anchoredPosition, true, true,
                                "task_list_task1_btn")));
                }
            };

            task.Idle = () =>
            {
            };

            task.DoneCondition = () =>
            {
                return data.storable_data[task.index].current_action_index >= task.TaskActions.Count;
            };


            task.CheckActionConditions = () =>
            {
            };

            task.Init = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.ADD_NEW_TASK_ITEM;
                msg.parametrs = new Yaga.CommonMessageParametr(task);
                MessageBus.Instance.SendMessage(msg);

                DataController.instance.tasks_storage.content["game_tutor_done"] = false;
                if(!DataController.instance.tasks_storage.content.ContainsKey("opened_pets"))
                    DataController.instance.tasks_storage.content["opened_pets"] = 0;
            };


            task.TaskActions = new List<TaskAction>();
            TaskAction action1 = new TaskAction();
            action1.condition_action = () => { return true; };
            action1.action = () =>
            {
                MainLocationOjects.instance.trash.GetComponent<Animator>().SetBool("start", true);

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(2, 1), DialogType.Black, DialogType.Main, DialogEntity.get_id(2, 1)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(2, 2), DialogType.Main, DialogType.Black, DialogEntity.get_id(2, 2)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(2, 3), DialogType.Main, DialogType.Black, DialogEntity.get_id(2, 3)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(2, 4), DialogType.Black, DialogType.Main, DialogEntity.get_id(2, 4)));
                dialog.SetDialogs(deList);
                dialog.SetMessagesByIndex(2, new List<Message> { new Message(Messages.REMOVE_TRASH, null) });
                dialog.SetBtnAction(() =>
                {
                    task.in_action = false;

                    dialog.CloseDialog();
                    CameraMoveController.GetController().ClearDestination();

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);

                    //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.NEW_TASK_AVALIBLE);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                });
                dialog.ShowDialog();

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
                //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_TASK_LIST);

                CatsMoveController.GetController().ActiveCat(Cats.Black);
                CatsMoveController.GetController().
                    SetDestination(Cats.Black, "Point 4");
                CatsMoveController.GetController().
                    SetDestination(Cats.Main, "Point 2");
                CameraMoveController.GetController().GoCat();

                task.in_action = true;

            };
            task.TaskActions.Add(action1);

            task.CheckActionConditions = () =>
            {
                if (task.data.current_action_index == 1)
                {
                    if (DataController.instance.cat_rang.curRang > 0)
                    {
                        Message new_msg = new Message();
                        new_msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                        new_msg.parametrs = new UpdateInt(task.index);
                        MessageBus.Instance.SendMessage(new_msg);

                        task.in_action = false;
                    }
                }
            };


            //TaskAction action2 = new TaskAction();
            //action2.condition_action = () => { return true; };
            //action2.action = () =>
            //{
            //    task.in_action = true;

            //    if (DataController.instance.cat_rang.curRang == 0)
            //    {
            //        MainLocationOjects.instance.trash.SetActive(false);
            //        MainLocationOjects.instance.trash_scanner.SetActive(true);
            //        MainLocationOjects.instance.trash_pack.SetActive(true);

            //        MessageBus.Instance.SendMessage(
            //            new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
            //                new TutorMaskController.TutorMaskParametr(
            //                    MainLocationOjects.instance.trash_scanner.transform.position, false, true,
            //                    "task1_trash_scanner")));

            //        MessageBus.Instance.SendMessage(
            //            new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
            //                new TutorMaskController.TutorMaskParametr(
            //                    MainLocationOjects.instance.scanning_btn.transform.position, false, true,
            //                    "scanning_btn")));
            //    }
            //};
            //task.TaskActions.Add(action2);

            task.DoneAction = () =>
            {
                MainLocationOjects.instance.trash.SetActive(false);
                MainLocationOjects.instance.trash_pack.SetActive(true);
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU_SCAN_BTN);
            };

            task.DoneInitAction = () =>
            {
                MainLocationOjects.instance.trash.SetActive(false);
                MainLocationOjects.instance.trash_pack.SetActive(true);
            };

            return task;
        }
    }
}
