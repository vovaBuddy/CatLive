using Main.Bubble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Task13Initializer
    {
        static ServeredTimer servered_timer = new ServeredTimer();

        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int time_wait = 30 * 60;
            int cur_task_index = 12;

            object[] time_msg_parametr_values = new object[2];
            time_msg_parametr_values[0] = TimerController.GetController().task13_timer;
            var time_msg_param = new Yaga.CommonMessageParametr(time_msg_parametr_values);
            Message timer_msg = new Message();
            timer_msg.Type = MainScene.MainMenuMessageType.TASK_TIME_UPDATE;
            timer_msg.parametrs = time_msg_param;

            Task task = new Task(cur_task_index, 1, time_wait, 3000, TextManager.getTaskName(13), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icon_011";

            if (data.storable_data[cur_task_index].started)
            {
                servered_timer.GetTime("Task13",
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

                //    DataController.instance.tasks_storage.content["minigame_fly_tutor"] = true;
                //    DataController.instance.tasks_storage.Store();

                //    Message msg = new Message();
                //    msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                //    msg.parametrs = new UpdateInt(task.index);
                //    MessageBus.Instance.SendMessage(msg);

                //    BoosterPrize prize = new BoosterPrize(BusterType.FLY, 3);
                //    prize.ActiveAction();

                //    task.in_action = false;
                //});
                //dialog.ShowDialog();

                //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_TASK_LIST);
                //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
                //task.in_action = true;
            };

            task.CutSceneCondition = () =>
            {
                return data.storable_data[11].done == true;
            };

            task.Init = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.ADD_NEW_TASK_ITEM;
                msg.parametrs = new Yaga.CommonMessageParametr(task);
                MessageBus.Instance.SendMessage(msg);
            };

            task.BeforeAction = () =>
            {
            };


            task.DoneAction = () =>
            {
                List<Vector3> points_main2 = new List<Vector3>();
                Transform point = GameObject.Find("CameraTasksTargets").transform
                .Find("Sleeping_room");
                points_main2.Add(point.position);
                CameraMoveController.GetController().SetDestinations(points_main2);

                MainLocationOjects.instance.sleep_room.SetActiveTrueWithAnimation();

                MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                new BubbleCreateParametr(
                    CatsMoveController.GetController().main_cat, new List<string>()
                        {TextManager.getText("bubble_relax") }, 5)));

                //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                TimerController.GetController().task13_timer.SetActive(false);
                MainLocationOjects.instance.sleep_room_farm.SetActive(false);
            };

            task.DoneInitAction = () =>
            {
                MainLocationOjects.instance.sleep_room.SetActive(true);
                MainLocationOjects.instance.sleep_room_farm.SetActive(false);
            };

            task.CheckActionConditions = () => { };

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

                servered_timer.SetTime("Task13", task.time_wait);
                MainLocationOjects.instance.sleep_room_farm.SetActive(true);

                List<Vector3> points_main2 = new List<Vector3>();
                Transform point = GameObject.Find("CameraTasksTargets").transform
                .Find("Sleeping_room");
                points_main2.Add(point.position);
                CameraMoveController.GetController().SetDestinations(points_main2);

                CatsMoveController.GetController().SetDestination(Cats.Main, "Point 26");

                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                msg.parametrs = new UpdateInt(task.index);
                MessageBus.Instance.SendMessage(msg);
            };
            tasc_action_1.condition_action = () => { return true; };


            TaskAction tasc_action_2 = new TaskAction();

            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);

            task.Idle = () =>
            {
                if (task.data.started && !task.data.done && !MainLocationOjects.instance.sleep_room_farm.activeSelf &&
                task.data.current_action_index > 0)
                {
                    MainLocationOjects.instance.sleep_room_farm.SetActive(true);
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
