using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task12Initializer
    {
        static ServeredTimer servered_timer = new ServeredTimer();

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 10 * 60;
            int cur_task_index = 11;

            object[] time_msg_parametr_values = new object[2];
            time_msg_parametr_values[0] = TimerController.GetController().task12_timer;
            var time_msg_param = new Yaga.CommonMessageParametr(time_msg_parametr_values);
            Message timer_msg = new Message();
            timer_msg.Type = MainScene.MainMenuMessageType.TASK_TIME_UPDATE;
            timer_msg.parametrs = time_msg_param;

            Task task = new Task(cur_task_index, 1, time_wait, 1000, TextManager.getTaskName(12), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_010";

            if (data.storable_data[cur_task_index].started)
            {
                servered_timer.GetTime("Task12",
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

            task.CutSceneCondition = () => { return data.storable_data[24].done == true; };

            task.Init = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.ADD_NEW_TASK_ITEM;
                msg.parametrs = new Yaga.CommonMessageParametr(task);
                MessageBus.Instance.SendMessage(msg);
            };

            task.BeforeAction = () => {

            };


            bool task_done = false;
            task.DoneAction = () =>
            {
                MainLocationOjects.instance.obstruction_toilet.SetActive(false);

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(12, 1), DialogType.Main, DialogType.Black, DialogEntity.get_id(12, 1)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(12, 2), DialogType.Black, DialogType.Main, DialogEntity.get_id(12, 2)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(12, 3), DialogType.Main, DialogType.Black, DialogEntity.get_id(12, 3)));
                dialog.SetDialogs(deList);
                dialog.SetMissionIcon("task_icon_012");
                dialog.SetBtnAction(() =>
                {
                    task.in_action = false;

                    dialog.CloseDialog();
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);
                });
                dialog.ShowDialog();
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);

                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 31");
                CatsMoveController.GetController().SetDestination(Cats.Black, "Point 58");

                TimerController.GetController().task12_timer.SetActive(false);
                MainLocationOjects.instance.obstruction_toilet_farm.SetActive(false);

                task.in_action = true;
            };

            task.DoneInitAction = () =>
            {
                MainLocationOjects.instance.obstruction_toilet.SetActive(false);
                MainLocationOjects.instance.obstruction_toilet_farm.SetActive(false);
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

                servered_timer.SetTime("Task12", task.time_wait);

                List<Vector3> points = new List<Vector3>();
                Transform point = GameObject.Find("CameraTasksTargets").transform
                .Find("TolietObstructions");
                points.Add(point.position);
                CameraMoveController.GetController().SetDestinations(points);

                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 23");
                CatsMoveController.GetController().SetDestination(Cats.Black, "Point 59");

                MainLocationOjects.instance.obstruction_toilet_farm.SetActive(true);
                MainLocationOjects.instance.obstruction_toilet.SetActive(false);

                task.in_action = true;
            };
            tasc_action_1.condition_action = () => { return true; };


            TaskAction task_action_2 = new TaskAction();
            task_action_2.condition_action = () => { return task.time_wait <= 0; };
            task_action_2.action = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);
            };

            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);
            task.TaskActions.Add(task_action_2);

            task.Idle = () =>
            {
                if (task.data.started && !task.data.done && !MainLocationOjects.instance.obstruction_toilet_farm.activeSelf &&
                task.data.current_action_index > 0)
                {
                    MainLocationOjects.instance.obstruction_toilet_farm.SetActive(true);
                    MainLocationOjects.instance.obstruction_toilet.SetActive(false);
                }
            };

            task.CheckActionConditions = () =>
            {
                if (task.data.current_action_index == 0 && CameraMoveController.GetController().DoesReachDestination())
                {
                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);
                    task.in_action = false;
                }
            };

            task.DoneCondition = () =>
            {
                return task.data.current_action_index >= task.TaskActions.Count;
            };

            return task;
        }
    }
}
