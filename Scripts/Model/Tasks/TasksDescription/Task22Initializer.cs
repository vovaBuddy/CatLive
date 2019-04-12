using Main.Bubble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task22Initializer
    {
        static ServeredTimer servered_timer = new ServeredTimer();

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 10 * 60;
            int cur_task_index = 21;

            object[] time_msg_parametr_values = new object[2];
            time_msg_parametr_values[0] = TimerController.GetController().task22_timer;
            var time_msg_param = new Yaga.CommonMessageParametr(time_msg_parametr_values);
            Message timer_msg = new Message();
            timer_msg.Type = MainScene.MainMenuMessageType.TASK_TIME_UPDATE;
            timer_msg.parametrs = time_msg_param;

            Task task = new Task(cur_task_index, 2, time_wait, 1000, TextManager.getTaskName(22), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_020";

            if (data.storable_data[cur_task_index].started)
            {
                servered_timer.GetTime("Task22",
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

                MainLocationOjects.instance.garden2_staf.SetActiveTrueWithAnimation();

                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 40");
                CatsMoveController.GetController().SetDestination(Cats.Black, "Point 45");
                CameraMoveController.GetController().SetDestinations("CameraTasksTargets", "Saray");

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(22, 1), DialogType.Main, DialogType.One, DialogEntity.get_id(22, 1)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                    //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                    MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_game") }, 5)));

                    dialog.CloseDialog();
                });

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
                dialog.ShowDialog();

                TimerController.GetController().task22_timer.SetActive(false);
                MainLocationOjects.instance.garden2_staf_farm.SetActive(false);

            };

            task.DoneInitAction = () =>
            {
                MainLocationOjects.instance.garden2_staf.SetActive(true);
                MainLocationOjects.instance.garden2_staf_farm.SetActive(false);
            };

            task.TickAction = () =>
            {
                time_msg_parametr_values[1] = task.time_wait;
                MessageBus.Instance.SendMessage(timer_msg);
            };

            TaskAction tasc_action_1 = new TaskAction();
            tasc_action_1.action = () =>
            {
                servered_timer.SetTime("Task22", task.time_wait);

                time_msg_parametr_values[1] = task.time_wait;
                MessageBus.Instance.SendMessage(timer_msg);

                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);

                //MainLocationOjects.instance.Children_zone.SetActive(true);
                //new cats
            };
            tasc_action_1.condition_action = () => { return true; };

            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);

            task.Idle = () =>
            {
                if (task.data.started && !task.data.done && !MainLocationOjects.instance.garden2_staf_farm.activeSelf &&
                    task.data.current_action_index > 0)
                {
                    MainLocationOjects.instance.garden2_staf_farm.SetActive(true);
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
