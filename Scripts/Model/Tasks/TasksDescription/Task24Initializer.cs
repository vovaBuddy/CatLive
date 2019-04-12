using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task24Initializer
    {
        static ServeredTimer servered_timer = new ServeredTimer();



        public static Task Init(DataEntity data)
        {

            DialogController dialog = DialogController.GetController();

            int time_wait = 30 * 60;
            int cur_task_index = 23;

            bool done_dialog = false;

            object[] time_msg_parametr_values = new object[2];
            time_msg_parametr_values[0] = TimerController.GetController().task24_timer;
            var time_msg_param = new Yaga.CommonMessageParametr(time_msg_parametr_values);
            Message timer_msg = new Message();
            timer_msg.Type = MainScene.MainMenuMessageType.TASK_TIME_UPDATE;
            timer_msg.parametrs = time_msg_param;

            Task task = new Task(cur_task_index, 1, time_wait, 3000, TextManager.getTaskName(24), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_022";

            if (data.storable_data[cur_task_index].started)
            {
                servered_timer.GetTime("Task24",
                    (answ) =>
                    {
                        if (!task.data.done)
                        {
                            task.time_wait = answ.data.time;

                            time_msg_parametr_values[1] = task.time_wait;
                            MessageBus.Instance.SendMessage(timer_msg, true);

                            done_dialog = true;
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

                MainLocationOjects.instance.garden1_pond.SetActive(true);
                MainLocationOjects.instance.garden1_pond_bad.SetActive(false);

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(24, 1), DialogType.Main, DialogType.One, DialogEntity.get_id(24, 1)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);                    
                });
                dialog.ShowDialog();

                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);

                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 78");
                CatsMoveController.GetController().SetDestination(Cats.Black, "Point 79");
                CameraMoveController.GetController().SetDestinations("CameraTasksTargets", "Garden_1");

                TimerController.GetController().task24_timer.SetActive(false);
                MainLocationOjects.instance.garden1_pond_farm.SetActive(false);
            };

            task.DoneInitAction = () =>
            {
                MainLocationOjects.instance.garden1_pond.SetActive(true);
                MainLocationOjects.instance.garden1_pond_bad.SetActive(false);
                MainLocationOjects.instance.garden1_pond_farm.SetActive(false);
            };

            task.TickAction = () =>
            {
                if (done_dialog)
                {
                    time_msg_parametr_values[1] = task.time_wait;
                    MessageBus.Instance.SendMessage(timer_msg);
                }
            };

            TaskAction tasc_action_1 = new TaskAction();
            tasc_action_1.action = () =>
            {


                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(24, 2), DialogType.Main, DialogType.Black, DialogEntity.get_id(24, 2)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(24, 3), DialogType.Black, DialogType.Main, DialogEntity.get_id(24, 3)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                    task.in_action = false;

                    servered_timer.SetTime("Task24", task.time_wait);

                    time_msg_parametr_values[1] = task.time_wait;
                    MessageBus.Instance.SendMessage(timer_msg);

                    done_dialog = true;

                    dialog.CloseDialog();
                });

                dialog.ShowDialog();
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
                CameraMoveController.GetController().SetDestinations("CameraTasksTargets", "Garden_1");
                task.in_action = true;
            };
            tasc_action_1.condition_action = () => { return true; };

            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);

            task.Idle = () =>
            {
                if (task.data.started && !task.data.done && !MainLocationOjects.instance.garden1_pond_farm.activeSelf &&
                    task.data.current_action_index > 0)
                {
                    MainLocationOjects.instance.garden1_pond_farm.SetActive(true);
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
