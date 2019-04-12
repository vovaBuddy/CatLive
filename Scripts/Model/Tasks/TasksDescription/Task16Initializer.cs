using Main.Bubble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task16Initializer
    {
        static ServeredTimer servered_timer = new ServeredTimer();

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 15 * 60;
            int cur_task_index = 15;

            object[] time_msg_parametr_values = new object[2];
            time_msg_parametr_values[0] = TimerController.GetController().task16_timer;
            var time_msg_param = new Yaga.CommonMessageParametr(time_msg_parametr_values);
            Message timer_msg = new Message();
            timer_msg.Type = MainScene.MainMenuMessageType.TASK_TIME_UPDATE;
            timer_msg.parametrs = time_msg_param;

            Task task = new Task(cur_task_index, 1, time_wait, 1500, TextManager.getTaskName(16), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_014";

            if (data.storable_data[cur_task_index].started)
            {
                servered_timer.GetTime("Task16",
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
                task.in_action = true;

                MainLocationOjects.instance.Children_zone.SetActiveTrueWithAnimation();
                MainLocationOjects.instance.Children_boxes.SetActive(false);
                //new cats

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(16, 1), DialogType.Main, DialogType.One, DialogEntity.get_id(16, 1)));
                dialog.SetDialogs(deList);
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);

                    MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_game") }, 5)));

                    //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);
                });
                dialog.ShowDialog();

                List<Vector3> points_main3 = new List<Vector3>();
                Transform point = GameObject.Find("CameraTasksTargets")
                    .transform.Find("Child_room");
                points_main3.Add(point.position);
                //CatsMoveController.GetController().SetDestination(Cats.Black, "Point 64");
                CameraMoveController.GetController().SetDestinations(points_main3);


                CatsMoveController.GetController().ActiveCat(Cats.Baby1);
                CatsMoveController.GetController().ActiveCat(Cats.Baby2);

                CatsMoveController.GetController().SetDestination(Cats.Baby1, "Point 65");
                CatsMoveController.GetController().SetDestination(Cats.Baby2, "Point 67");

                TimerController.GetController().task16_timer.SetActive(false);
                MainLocationOjects.instance.Children_stuff_farm.SetActive(false);

            };

            task.DoneInitAction = () =>
            {
                MainLocationOjects.instance.Children_zone.SetActive(true);
                MainLocationOjects.instance.Children_boxes.SetActive(false);
                MainLocationOjects.instance.Children_stuff_farm.SetActive(false);
                //new cats
            };

            task.TickAction = () =>
            {
                time_msg_parametr_values[1] = task.time_wait;
                MessageBus.Instance.SendMessage(timer_msg);
            };

            TaskAction tasc_action_1 = new TaskAction();
            tasc_action_1.action = () =>
            {
                servered_timer.SetTime("Task16", task.time_wait);

                time_msg_parametr_values[1] = task.time_wait;
                MessageBus.Instance.SendMessage(timer_msg);

                List<Vector3> points_main3 = new List<Vector3>();
                Transform point = GameObject.Find("CameraTasksTargets")
                    .transform.Find("Child_room");
                points_main3.Add(point.position);
                CameraMoveController.GetController().SetDestinations(points_main3);

                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);

                CatsMoveController.GetController().SetDestination(Cats.Black, "Point 55");
                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 58");
            };
            tasc_action_1.condition_action = () => { return true; };


            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);

            task.Idle = () =>
            {
                if (task.data.started && !task.data.done && !MainLocationOjects.instance.Children_stuff_farm.activeSelf &&
                task.data.current_action_index > 0)
                {
                    MainLocationOjects.instance.Children_stuff_farm.SetActive(true);
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
