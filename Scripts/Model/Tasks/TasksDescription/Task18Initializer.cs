using Main.Bubble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task18Initializer
    {
        static ServeredTimer servered_timer = new ServeredTimer();

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 30 * 60;
            int cur_task_index = 17;

            object[] time_msg_parametr_values = new object[2];
            time_msg_parametr_values[0] = TimerController.GetController().task18_timer;
            var time_msg_param = new Yaga.CommonMessageParametr(time_msg_parametr_values);
            Message timer_msg = new Message();
            timer_msg.Type = MainScene.MainMenuMessageType.TASK_TIME_UPDATE;
            timer_msg.parametrs = time_msg_param;

            bool done_dialog = false;

            Task task = new Task(cur_task_index, 1, time_wait, 3000, TextManager.getTaskName(18), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_016";

            if (data.storable_data[cur_task_index].started)
            {
                servered_timer.GetTime("Task18",
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

            task.CutSceneCondition = () => { return data.storable_data[14].done == true; };

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
                MainLocationOjects.instance.music.SetActiveTrueWithAnimation();

                List<Vector3> points_main3 = new List<Vector3>();
                Transform point = GameObject.Find("CameraTasksTargets")
                    .transform.Find("Music");
                points_main3.Add(point.position);
                CameraMoveController.GetController().SetDestinations(points_main3);

                MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                new BubbleCreateParametr(
                    CatsMoveController.GetController().main_cat, new List<string>()
                        {TextManager.getText("bubble_books") }, 5)));

                //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                TimerController.GetController().task18_timer.SetActive(false);
                MainLocationOjects.instance.music_farm.SetActive(false);

            };

            task.DoneInitAction = () =>
            {
                MainLocationOjects.instance.music.SetActive(true);
                MainLocationOjects.instance.music_farm.SetActive(false);
                //new cats
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


                List<Vector3> points_main3 = new List<Vector3>();
                Transform point = GameObject.Find("CameraTasksTargets")
                    .transform.Find("Music");
                points_main3.Add(point.position);
                CameraMoveController.GetController().SetDestinations(points_main3);

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(18, 1), DialogType.Black, DialogType.Main, DialogEntity.get_id(18, 1)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(18, 2), DialogType.Main, DialogType.Black, DialogEntity.get_id(18, 2)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();
                    task.in_action = false;

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);

                    servered_timer.SetTime("Task18", task.time_wait);
                    MainLocationOjects.instance.music_farm.SetActive(true);

                    time_msg_parametr_values[1] = task.time_wait;
                    MessageBus.Instance.SendMessage(timer_msg);

                    done_dialog = true;

                });
                dialog.ShowDialog();

                task.in_action = true;
            };
            tasc_action_1.condition_action = () => { return true; };


            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);

            task.Idle = () =>
            {
                if (task.data.started && !task.data.done && !MainLocationOjects.instance.music_farm.activeSelf &&
                        task.data.current_action_index > 0)
                {
                    MainLocationOjects.instance.music_farm.SetActive(true);
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
