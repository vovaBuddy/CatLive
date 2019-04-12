using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Yaga.MessageBus;

namespace Task
{
    public static class Chapter1Finish
    {
        public static Task Init(DataEntity data)
        {
            DialogController dialog = DialogController.GetController();

            int cur_task_index = 24;

            Task task = new Task(cur_task_index, 0, 0, 0, TextManager.getTaskName(25), true, false);
            task.data = data.storable_data[task.index];

            task.icon_name = "task_icons_chapter";

            task.BeforeCutScene = () =>
            {
                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(30, 1), DialogType.Black, DialogType.Main, DialogEntity.get_id(30, 1)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(30, 2), DialogType.Main, DialogType.Black, DialogEntity.get_id(30, 2)));
                dialog.SetDialogs(deList);
                dialog.SetMissionIcon("task_icons_chapter");
                dialog.SetBtnAction(() =>
                {
                    dialog.CloseDialog();

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                    //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                    CatsMoveController.GetController().ActiveCat(Cats.Gamer1);
                    CatsMoveController.GetController().ActiveCat(Cats.Gamer2);
                    CatsMoveController.GetController().ActiveCat(Cats.Kitchen1);

                    CatsMoveController.GetController().SetDestination(Cats.Gamer1, "Point 19");
                    CatsMoveController.GetController().SetDestination(Cats.Gamer2, "Point 20");
                    CatsMoveController.GetController().SetDestination(Cats.Kitchen1, "Point 12");

                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
                    msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(msg);
                });
                dialog.ShowDialog();
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
            };

            task.CutSceneCondition = () => { return data.storable_data[7].done == true && data.storable_data[8].done == true; };

            task.Init = () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.ADD_NEW_TASK_ITEM;
                msg.parametrs = new Yaga.CommonMessageParametr(task);
                MessageBus.Instance.SendMessage(msg);

            };

            var subs = new MessageSubscriber();
            subs.MessageTypes = new string[1] { "CHAPTER_PRIZE_CLOSE_END" };
            subs.action = (m) =>
            {
                    task.in_action = false;

                    Message new_msg = new Message();
                    new_msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    new_msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(new_msg);
            };
            MessageBus.Instance.AddSubscriber(subs);

            //var subs_mask = new MessageSubscriber();
            //subs_mask.MessageTypes = new string[1] { "CHAPTER_END_SHOW_BUSTER_TUTOR" };
            //subs_mask.action = (m) =>
            //{
            //    List<DialogEntity> deList = new List<DialogEntity>();
            //    deList.Add(new DialogEntity(
            //        TextManager.getDialogsText(30, 6), DialogType.Black, DialogType.Main, DialogEntity.get_id(30, 6)));
            //    dialog.SetDialogs(deList);

            //    dialog.SetBtnAction(() =>
            //    {
            //        //                   task.in_action = false;

            //        dialog.CloseDialog();

            //        Message new_msg = new Message();
            //        new_msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
            //        new_msg.parametrs = new UpdateInt(task.index);
            //        MessageBus.Instance.SendMessage(new_msg);

            //        if (DataController.instance.tasks_storage.content.ContainsKey("minigame_magnet_tutor") &&
            //           (bool)DataController.instance.tasks_storage.content["minigame_magnet_tutor"] == false)
            //            return;

            //        MessageBus.Instance.SendMessage(new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
            //            new TutorMaskController.TutorMaskParametr(
            //                MainLocationOjects.instance.minigames_btn_footer.transform.position, false, true,
            //                "minigames_btn_footer")));

            //        MessageBus.Instance.SendMessage(new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
            //            new TutorMaskController.TutorMaskParametr(
            //                MainLocationOjects.instance.minigames_play_star_btn.GetComponent<RectTransform>().anchoredPosition, true, false,
            //                "minigames_play_star_btn")));

            //        DataController.instance.tasks_storage.content["minigame_magnet_tutor"] = true;
            //        DataController.instance.tasks_storage.Store();

            //        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.TOGGLE_MAIN_MENU_REVIEW_BTN);
            //    });

            //    dialog.ShowDialog();
            //    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
            //};
            //MessageBus.Instance.AddSubscriber(subs_mask);

            task.BeforeAction = () => {



            };


            task.DoneAction = () =>
            {

            };

            task.DoneInitAction = () =>
            {

            };

            task.TickAction = () =>
            {
            };

            TaskAction tasc_action_1 = new TaskAction();
            tasc_action_1.action = () =>
            {
                MessageBus.Instance.SendMessage(Main.Chapter.API.Messages.OPEN_PRIZE_SCREEN);

                task.in_action = true;
            };
            tasc_action_1.condition_action = () => { return true; };
            
            TaskAction tasc_action_2 = new TaskAction();
            tasc_action_2.action = () =>
            {
                task.in_action = true;

                List<DialogEntity> deList = new List<DialogEntity>();
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(30, 3), DialogType.Main, DialogType.Black, DialogEntity.get_id(30, 3)));
                deList.Add(new DialogEntity(
                    TextManager.getDialogsText(30, 4), DialogType.Black, DialogType.Main, DialogEntity.get_id(30, 4)));
                //deList.Add(new DialogEntity(
                //    TextManager.getDialogsText(30, 5), DialogType.Main, DialogType.Black));
                //deList.Add(new DialogEntity(
                //    TextManager.getDialogsText(30, 6), DialogType.Black, DialogType.Main));
                dialog.SetDialogs(deList);
                dialog.SetMissionIcon("task_icon_009");
                dialog.SetMissionIcon("task_icon_010");
                dialog.SetBtnAction(() =>
                {
                    task.in_action = false;

                    dialog.CloseDialog();

                    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
                    //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);

                    Message new_msg = new Message();
                    new_msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                    new_msg.parametrs = new UpdateInt(task.index);
                    MessageBus.Instance.SendMessage(new_msg);

                    DataController.instance.world_state_data.need_first_booster_tutor = true;
                });
                dialog.ShowDialog();
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
            };
            tasc_action_2.condition_action = () => { return true; };

            TaskAction tasc_action_3 = new TaskAction();
            tasc_action_3.action = () =>
            {
                task.in_action = true;

               
            };
            tasc_action_3.condition_action = () => { return true; };

            task.TaskActions = new List<TaskAction>();
            task.TaskActions.Add(tasc_action_1);
            task.TaskActions.Add(tasc_action_2);
            //task.TaskActions.Add(tasc_action_3);

            task.CheckActionConditions = () =>
            {
                //if(task.data.current_action_index == 2 && DataController.instance.tasks_storage.content.ContainsKey("minigame_magnet_tutor") &&
                //   (bool)DataController.instance.tasks_storage.content["minigame_magnet_tutor"] == false)
                //{
                //    task.in_action = false;

                //    Message new_msg = new Message();
                //    new_msg.Type = MainScene.MainMenuMessageType.SOME_ACTION_DONE;
                //    new_msg.parametrs = new UpdateInt(task.index);
                //    MessageBus.Instance.SendMessage(new_msg);
                //}
            };

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
