using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task15Initializer
    {
        static ServeredTimer servered_timer = new ServeredTimer();

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 15 * 60;
            int cur_task_index = 14;

            object[] time_msg_parametr_values = new object[2];
            time_msg_parametr_values[0] = TimerController.GetController().task15_timer;
            var time_msg_param = new Yaga.CommonMessageParametr(time_msg_parametr_values);
            Message timer_msg = new Message();
            timer_msg.Type = MainScene.MainMenuMessageType.TASK_TIME_UPDATE;
            timer_msg.parametrs = time_msg_param;

            Task task = new Task(cur_task_index, 1, time_wait, 500, TextManager.getTaskName(15), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_013";

            if (data.storable_data[cur_task_index].started)
            {
                servered_timer.GetTime("Task15",
                    (answ) =>
                    {
                        if (!task.data.done)
                        {
                            task.time_wait = answ.data.time;

                            time_msg_parametr_values[1] = task.time_wait;
                            MessageBus.Instance.SendMessage(timer_msg, true);
                        }
                    },
                    (answ) =>
                    {
                        //todo replay request
                    });
            }

            task.BeforeCutScene = () =>
            {
                //List<DialogEntity> deList = new List<DialogEntity>();
                //deList.Add(new DialogEntity(
                //    TextManager.getDialogsText(13, 1), DialogType.Black, DialogType.One, DialogEntity.get_id(13, 1)));
                //dialog.SetDialogs(deList);
                //dialog.SetBtnAction(() =>
                //{
                //    dialog.CloseDialog();

                //    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);

                //    MessageBus.Instance.SendMessage(new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
                //        new TutorMaskController.TutorMaskParametr(
                //            MainLocationOjects.instance.minigames_btn_footer.transform.position, false, true,
                //            "minigames_btn_footer")));

                //    MessageBus.Instance.SendMessage(new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
                //        new TutorMaskController.TutorMaskParametr(
                //            MainLocationOjects.instance.minigames_play_star_btn.GetComponent<RectTransform>().anchoredPosition, true, false,
                //            "minigames_play_star_btn")));

                //    DataController.instance.tasks_storage.content["minigame_reborn_tutor"] = true;
                //    DataController.instance.tasks_storage.Store();

                //    Message msg = new Message();
                //    msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                //    msg.parametrs = new UpdateInt(task.index);
                //    MessageBus.Instance.SendMessage(msg);

                //    BoosterPrize prize = new BoosterPrize(BusterType.REBORN, 3);
                //    prize.ActiveAction();

                //    task.in_action = false;
                //});
                //dialog.ShowDialog();

                //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_TASK_LIST);
                //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
                //task.in_action = true;
            };

            task.CutSceneCondition = () => { return data.storable_data[13].done == true; };

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

                MainLocationOjects.instance.Children_obstruction.SetActive(false);
                MainLocationOjects.instance.Children_boxes.SetActiveTrueWithAnimation();

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(15, 1), DialogType.Black, DialogType.One, DialogEntity.get_id(15, 1)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(15, 2), DialogType.Djeki, DialogType.Black, DialogEntity.get_id(15, 2)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    List<DialogEntity> deList2 = new List<DialogEntity>();
                    deList2.Add(new DialogEntity(
                        TextManager.getDialogsText(15, 3), DialogType.Main, DialogType.Black, DialogEntity.get_id(15, 3)));
                    deList2.Add(new DialogEntity(
                        TextManager.getDialogsText(15, 4), DialogType.Black, DialogType.Main, DialogEntity.get_id(15, 4)));
                    deList2.Add(new DialogEntity(
                        TextManager.getDialogsText(15, 5), DialogType.Main, DialogType.Black, DialogEntity.get_id(15, 5)));
                    dialog.SetDialogs(deList2);
                    dialog.SetMissionIcon("task_icon_014");
                    dialog.SetMissionIcon("task_icon_015");
                    dialog.SetMissionIcon("task_icon_016");

                    dialog.SetBtnAction(() =>
                    {
                        task.in_action = false;
                        dialog.CloseDialog();
                        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);
                    });

                    CatsMoveController.GetController().SetDestination(Cats.Jakky, "Point 64");
                    CatsMoveController.GetController().SetDestination(Cats.Main, "Point 76");
                    CatsMoveController.GetController().SetDestination(Cats.Black, "Point 75");

                    CameraMoveController.GetController().SetDestinations("CameraTasksTargets", "Garden_1");
                    //CatsMoveController.GetController().SetDestination(Cats.Black, "CatTasksTargets", "Task15", "BlackTargets2");

                });
                dialog.ShowDialog();

                //CatsMoveController.GetController().SetDestination(Cats.Black, "CatTasksTargets", "Task15", "BlackTargets");
                //CatsMoveController.GetController().SetDestination(Cats.Main, "CatTasksTargets", "Task15", "MainTargets");
                //CatsMoveController.GetController().SetDestination(Cats.Jakky, "CatTasksTargets", "Task15", "JakkyTargets");

                CameraMoveController.GetController().SetDestinations("CameraTasksTargets", "Child_room");

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);

                TimerController.GetController().task15_timer.SetActive(false);
                MainLocationOjects.instance.Children_obstruction_farm.SetActive(false);
            };

            task.DoneInitAction = () =>
            {
                MainLocationOjects.instance.Children_obstruction.SetActive(false);
                MainLocationOjects.instance.Children_obstruction_farm.SetActive(false);
                MainLocationOjects.instance.Children_boxes.SetActive(true);
            };


            task.TickAction = () =>
            {
                time_msg_parametr_values[1] = task.time_wait;
                MessageBus.Instance.SendMessage(timer_msg);
            };

            TaskAction tasc_action_1 = new TaskAction();
            tasc_action_1.action = () =>
            {
                time_msg_parametr_values[1] = task.time_wait;
                MessageBus.Instance.SendMessage(timer_msg);

                servered_timer.SetTime("Task15", task.time_wait);

                CameraMoveController.GetController().SetDestinations("CameraTasksTargets", "Child_room");

                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 59");
                CatsMoveController.GetController().SetDestination(Cats.Black, "Point 22");

                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);                
            };

            tasc_action_1.condition_action = () => { return true; };

            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);

            task.Idle = () =>
            {
                if (task.data.started && !task.data.done && !MainLocationOjects.instance.Children_obstruction_farm.activeSelf &&
                task.data.current_action_index > 0)
                {
                    MainLocationOjects.instance.Children_obstruction_farm.SetActive(true);
                    MainLocationOjects.instance.Children_obstruction.SetActive(false);
                }
            };

            task.DoneCondition = () =>
            {
                return task.time_wait < 0;
            };

            return task;
        }
    }
}
