using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task7Initializer
    {
        static ServeredTimer servered_timer = new ServeredTimer();

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            bool show_money_tutor = false;

            int time_wait = 5 * 60;
            int cur_task_index = 6;
            object[] time_msg_parametr_values = new object[2];
            time_msg_parametr_values[0] = TimerController.GetController().task7_timer;
            var time_msg_param = new Yaga.CommonMessageParametr(time_msg_parametr_values);
            Message timer_msg = new Message();
            timer_msg.Type = MainScene.MainMenuMessageType.TASK_TIME_UPDATE;
            timer_msg.parametrs = time_msg_param;

            bool dialog_end = false;

            Task task = new Task(cur_task_index, 1, time_wait, 0, TextManager.getTaskName(7), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_005";

            if (data.storable_data[cur_task_index].started)
            {
                servered_timer.GetTime("Task7",
                    (answ) =>
                    {
                        if (!task.data.done)
                        {
                            task.time_wait = answ.data.time;

                            time_msg_parametr_values[1] = task.time_wait;
                            MessageBus.Instance.SendMessage(timer_msg, true);

                            dialog_end = true;
                        }
                    },
                    (answ) =>
                    {
                        //task.time_wait = time_wait;

                        //time_msg_parametr_values[1] = task.time_wait;
                        //MessageBus.Instance.SendMessage(timer_msg);
                    });
            }



            task.BeforeCutScene = () =>
            {
                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);
            };

            task.CutSceneCondition = () => { return data.storable_data[4].done == true; };

            task.Init = () =>
            {
                if (!DataController.instance.tasks_storage.content.ContainsKey("show_coins_tutor"))
                {
                    DataController.instance.tasks_storage.content["show_coins_tutor"] = false;
                    DataController.instance.tasks_storage.Store();
                }

                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.ADD_NEW_TASK_ITEM;
                msg.parametrs = new Yaga.CommonMessageParametr(task);
                MessageBus.Instance.SendMessage(msg);
            };

            task.BeforeAction = () => {

                show_money_tutor = true;

                //var subs = new MessageSubscriber();
                //subs.MessageTypes = new string[3] { MainScene.MainMenuMessageType.CLOSE_TASK_LIST, "TASK_ITEM_OPENED",
                //    MainScene.MainMenuMessageType.OPEN_MINI_GAMES };
                //subs.action = (m) =>
                //{
                //    if (m.Type == MainScene.MainMenuMessageType.OPEN_MINI_GAMES)
                //    {
                //        show_money_tutor = false;
                //        return;
                //    }

                //    if (m.Type == MainScene.MainMenuMessageType.CLOSE_TASK_LIST &&
                //    task.data.started)
                //        return;

                //    if (m.Type == "TASK_ITEM_OPENED" &&
                //    !task.data.started)
                //        return;

                //    if(!show_money_tutor && m.Type == MainScene.MainMenuMessageType.CLOSE_TASK_LIST)
                //        return;

                //    if (DataController.instance.gamesRecords.StarMinigameNeed)
                //        return;

                //    if (!(bool)DataController.instance.tasks_storage.content["show_coins_tutor"])
                //    {
                //        DataController.instance.tasks_storage.content["show_coins_tutor"] = true;
                //        DataController.instance.tasks_storage.Store();

                //        List<DialogEntity> deList = new List<DialogEntity>();
                //        deList.Add(new DialogEntity(
                //            TextManager.getDialogsText(7, 3), DialogType.Black, DialogType.Main, DialogEntity.get_id(7, 3)));
                //        dialog.SetDialogs(deList);
                //        dialog.SetBtnAction(() =>
                //        {
                //            dialog.CloseDialog();

                //            MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                //            MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_MINI_GAMES);
                //            MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_COINS_GAMES);

                //            MessageBus.Instance.SendMessage(new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
                //                    new TutorMaskController.TutorMaskParametr(
                //                        MainLocationOjects.instance.minigames_play_coins_btn.GetComponent<RectTransform>().anchoredPosition, true, true,
                //                        "minigames_play_coins_btn")));
                //        });
                //        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_TASK_LIST);
                //        dialog.ShowDialog();
                //        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
                //    }
                //};
                //MessageBus.Instance.AddSubscriber(subs);
            };


            task.DoneAction = () =>
            {
                task.in_action = true;

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);
            };

            task.DoneInitAction = () =>
            {
                MainLocationOjects.instance.trash_pack.SetActive(false);
            };

            
            task.TickAction = () =>
            {
                if (dialog_end)
                {
                    time_msg_parametr_values[1] = task.time_wait;
                    MessageBus.Instance.SendMessage(timer_msg);
                }
            };


            TaskAction tasc_action_1 = new TaskAction();
            tasc_action_1.action = () =>
            {
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(7, 1), DialogType.Main, DialogType.One, DialogEntity.get_id(7, 1)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();
                    //task.in_action = false;

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);
                    //timer

                    task.speed_up_price = (int)(DataController.instance.catsPurse.Coins * 0.9f);

                    MessageBus.Instance.SendMessage(
                        new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
                            new TutorMaskController.TutorMaskParametr(
                                MainLocationOjects.instance.taks_1_btn.GetComponent<RectTransform>().anchoredPosition, true, true,
                                "task_list_task1_btn")));

                    time_msg_parametr_values[1] = task.time_wait;
                    MessageBus.Instance.SendMessage(timer_msg);

                    servered_timer.SetTime("Task7", task.time_wait);

                    dialog_end = true;
                });
                dialog.ShowDialog();
                task.in_action = true;

                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 25");
                CatsMoveController.GetController().SetDestination(Cats.Black, "Point 7");

                List<Vector3> points = new List<Vector3>();
                Transform point = GameObject.Find("CameraTasksTargets").transform
                .Find("Trash_packeg");
                points.Add(point.position);   
                CameraMoveController.GetController().SetDestinations(points);
            };
            tasc_action_1.condition_action = () => { return true; };

            task.CheckActionConditions = () =>
            {
                if(task.data.current_action_index == 0)
                {
                    if(CameraMoveController.GetController().DoesReachDestination())
                    {
                        task.in_action = false;

                        Message msg = new Message();
                        msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                        msg.parametrs = new UpdateInt(task.index);
                        MessageBus.Instance.SendMessage(msg);
                    }
                }

                else if(task.data.current_action_index == 1 && task.time_wait < 0.0f)
                {
                    task.in_action = false;

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);
                }

                else if(task.data.current_action_index == 2 && GarbageTruckController.GetController().end_garbage_timer <= 0.0f)
                {
                    task.in_action = false;

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                }
            };


            TaskAction tasc_action_2 = new TaskAction();
            tasc_action_2.condition_action = () => { return task.time_wait < 0.0f; };
            tasc_action_2.action = () =>
            {
                List<Vector3> points = new List<Vector3>();
                Transform point = GameObject.Find("CameraTasksTargets").transform
                .Find("Trash_packeg");
                points.Add(point.position);
                CameraMoveController.GetController().SetDestinations(points);
                task.in_action = true;
            };

            TaskAction tasc_action_3 = new TaskAction();
            tasc_action_3.condition_action = () => { return true; };
            tasc_action_3.action = () =>
            {
                MainLocationOjects.instance.GarbageTruckController.SetActive(true);
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_FOOTER);

                task.in_action = true;
            };

            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);
            task.TaskActions.Add(tasc_action_2);
            task.TaskActions.Add(tasc_action_3);

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
