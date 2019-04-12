//using Helper;
//using MainScene;
//using Minigames;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using Yaga;
//using Yaga.Helpers;
//using Yaga.MessageBus;

//[Extension(Extensions.SUBSCRIBE_MESSAGE)]
//public class GoalsStartController : ExtendedBehaviour {

//    public GameObject goal_panel;
//    public Text header;
//    public Image goal_img;
//    public Text goal_text;
//    public Text goal_value_text;
//    public Text btn_text;

//    public GameObject buster_controller;

//    // Use this for initialization
//    bool start = false;
//	override public void ExtendedStart () {

//        header.text = TextManager.getText("minigame_goals_stars_header_text");
//        goal_text.text = TextManager.getText("minigame_goals_stars_goal_text");
//        btn_text.text = TextManager.getText("minigame_goals_stars_btn_text");

//        if (string.Equals(DataController.instance.other_storage["game_type"],
//                GameType.STARS.ToString()))
//        {
//            goal_panel.SetActive(true);
//            buster_controller.SetActive(true);

//            StarTask task = StarTasksController.instance.get_cur_task(
//                DataController.instance.other_storage["game_name"].ToString());


//            goal_img.sprite = ResourceHelper.LoadSprite(
//                ResourcesNameHelper.UI_ICON_NAME_BY_TASK(
//                    task.task_info[0].type));

//            goal_value_text.text = task.task_info[0].value.ToString();

//            if (task.task_info[0].type == TaskType.TIME)
//                goal_value_text.text = goal_value_text.text + " " + TextManager.getText("minigame_goals_seconds_text");

//            if (task.task_info.Count == 2)
//            {
//                if (task.task_info[1].type == TaskType.TIME_OUT)
//                {
//                    goal_value_text.text += " " + TextManager.getText("mm_minigames_in_seconds_text").Replace("%N%", task.task_info[1].value.ToString());
//                }
//            }

//            start = true;

//            header.text = TextManager.getText("minigame_winstar_header_text") + " " + 
//                (StarTasksController.instance.get_cur_index( 
//                    DataController.instance.other_storage["game_name"].ToString()) + 1); ;
//        }
//    }

//    public void StartGame()
//    {
//        MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_END);
//        MessageBus.Instance.SendMessage(MiniGameMessageType.CLOSE_GOAL_SCREEN);
//    }

//    [Subscribe(MiniGameMessageType.CLOSE_GOAL_SCREEN)]
//    public void ClosePanel(Message msg)
//    {
//        goal_panel.SetActive(false);
//    }

//    public void Close()
//    {
//        MessageBus.Instance.SendMessage(new Message(
//            Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
//            new Common.LoadingScreen.API.SceneNameParametr(
//                Common.LoadingScreen.API.SceneNames.MAIN, false)));
//    }

//    public override void ExtendedUpdate()
//    {
//        if(start)
//        {
//            MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_START);
//            start = false;
//        }
//    }
//}
