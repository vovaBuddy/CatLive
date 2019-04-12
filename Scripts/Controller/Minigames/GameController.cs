//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using Yaga;
//using Yaga.MessageBus;
//using MainScene;
//using UnityEngine.Analytics;

//namespace Minigames
//{
//    [Extension(Extensions.SUBSCRIBE_MESSAGE, Extensions.PAUSE)]
//    public class GameController : ExtendedBehaviour
//    {

//        public int points;
//        public int catched_coins;
//        public int coins;
//        public int time;

//        float TimeTimer = 0.0f;

//        float PoinsAddTimeOut = 0.5f;
//        float PoinsAddTimer = 0.0f;
//        int PointsAddValue = 1;

//        bool advert_continue = true;
//        private TaskType measure_task;
//        private int task_aim;
//        private bool time_down_mode = false;
//        private GameType game_type;
//        string cur_game_name;

//        // Use this for initialization
//        override public void ExtendedStart()
//        {
//            //AppodealController.instance.ShowBanner();

//            DataController.instance.advEntity.sequential_games++;

//            if (DataController.instance.other_storage.ContainsKey("Continue") &&
//                (bool)DataController.instance.other_storage["Continue"] == true)
//            {
//                GameRecords tmp = DataController.instance.gamesRecords.Record(
//                    DataController.instance.other_storage["game_name"].ToString());
//                points = tmp.last_value;
//                catched_coins = tmp.last_coins;
//                DataController.instance.catsPurse.Coins -= tmp.last_coins;
//            }
//            else
//            {
//                catched_coins = 0;
//                points = 0;
//            }

//            coins = DataController.instance.catsPurse.Coins;

//            UpdateCoinsValue(0);
//            UpdatePointsValue(0);

//            measure_task = StarTasksController.instance.get_cur_task(
//                DataController.instance.other_storage["game_name"].ToString())
//                .task_info[0].type;

//            task_aim = StarTasksController.instance.get_cur_task(
//                DataController.instance.other_storage["game_name"].ToString())
//                .task_info[0].value;

//            if (StarTasksController.instance.get_cur_task(
//                DataController.instance.other_storage["game_name"].ToString())
//                .task_info.Count > 1)
//            {
//                time_down_mode = StarTasksController.instance.get_cur_task(
//                    DataController.instance.other_storage["game_name"].ToString())
//                    .task_info[1].type == TaskType.TIME_OUT;

//                time = StarTasksController.instance.get_cur_task(
//                    DataController.instance.other_storage["game_name"].ToString())
//                    .task_info[1].value;
//            }

//            game_type = (GameType)System.Enum.Parse(typeof(GameType),
//                DataController.instance.other_storage["game_type"].ToString());

//            cur_game_name = DataController.instance.other_storage["game_name"].ToString();

//            if (game_type == GameType.COINS)
//            {
//                GameStatistics.instance.SendStat("start_minigame_" + cur_game_name, 0);


//                Analytics.CustomEvent("start_minigame_" + cur_game_name);
//            }
//            else
//            {
//                Analytics.CustomEvent("start_stargame_" + cur_game_name, new Dictionary<string, object>
//                    {
//                        { "task_id", StarTasksController.instance.get_cur_index(cur_game_name) }
//                    });

//                GameStatistics.instance.SendStat("start_stargame_" + cur_game_name,
//                    StarTasksController.instance.get_cur_index(cur_game_name));
//            }
//        }

//        void UpdatePointsValue(int value)
//        {
//            points += value;
//            Message msg = new Message();
//            msg.Type = Minigames.MiniGameMessageType.SCORE_UPDATE;
//            msg.parametrs = new CommonMessageParametr(points);
//            MessageBus.Instance.SendMessage(msg);

//            //if (points % 50 == 0)
//            //{
//            //    MessageBus.Instance.SendMessage(MiniGameMessageType.MINIGAME_UP_DIFFICULT);
//            //}

//            UpdatePB();
//        }

//        void WinTask()
//        {
//            Analytics.CustomEvent("winMiniGame", new Dictionary<string, object>
//                    {
//                        { "task_id", StarTasksController.instance.get_cur_index(cur_game_name) }
//                    });

//            DataController.instance.world_state_data.last_game_event = GAME_EVENT.WON_MINIGAME;

//            GameStatistics.instance.SendStat("finish_stargame_" + cur_game_name,
//                StarTasksController.instance.get_cur_index(cur_game_name));

//            //AppodealController.instance.CloseBanner();
//            bool in_tutor = false;

//            if (!(bool)DataController.instance.tasks_storage.content["game_tutor_done"])
//            {
//                in_tutor = !(bool)DataController.instance.tasks_storage.content["game_tutor_done"];
//                DataController.instance.tasks_storage.content["game_tutor_done"] = true;
//                DataController.instance.tasks_storage.Store();
//            }

//            StarTasksController.instance.DoneCurTask(
//                DataController.instance.other_storage["game_name"].ToString());

//            //for tests
//            DataController.instance.catsPurse.Stars += 1;
//            //DataController.instance.catsPurse.Coins += 100000;

//            DataController.instance.catsPurse.Coins = coins + catched_coins;

//            MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_START);

//            Message m = new Message();
//            m.Type = MiniGameMessageType.OPEN_WIN_PANEL;
//            Dictionary<string, int> dic = new Dictionary<string, int>();
//            dic.Add("catched_coins", catched_coins);
//            dic.Add("in_tutor", in_tutor ? 1 : 0);
//            CommonMessageParametr param = new CommonMessageParametr(dic);
//            m.parametrs = param;

//            MessageBus.Instance.SendMessage(m);
//            MessageBus.Instance.SendMessage(MiniGameMessageType.CLOSE_SCORE_HEADER);

//            m.parametrs = new UpdatePurseParametrs(coins,
//                DataController.instance.catsPurse.Hearts,
//                DataController.instance.catsPurse.Stars - 1);
//            m.Type = MiniGameMessageType.OPEN_GLOBAL_HEADER;
//            MessageBus.Instance.SendMessage(m);
//        }

//        bool checkTaskItem(TaskType t, int v)
//        {
//            switch (t)
//            {
//                case TaskType.POINTS:
//                    if (points >= v)
//                    {
//                        return true;
//                    }
//                    break;

//                case TaskType.TIME:
//                    if (time >= v)
//                    {
//                        return true;
//                    }
//                    break;

//                case TaskType.COINS:
//                    if (catched_coins >= v)
//                    {
//                        return true;
//                    }
//                    break;

//                case TaskType.TIME_OUT:
//                    if (time == 0)
//                    {
//                        MessageBus.Instance.SendMessage(Minigames.MiniGameMessageType.GAME_OVER);
//                        return false;
//                    }
//                    else
//                    {
//                        return true;
//                    }
//                    break;
//            }

//            return false;
//        }
//        public void CheckTask()
//        {
//            int win_cnt = 0;

//            var task = StarTasksController.instance.get_cur_task(
//                DataController.instance.other_storage["game_name"].ToString());

//            foreach (var t in task.task_info)
//            {
//                win_cnt += checkTaskItem(t.type, t.value) ? 1 : 0;
//            }

//            if (win_cnt == task.task_info.Count)
//            {
//                WinTask();
//            }
//        }

//        void UpdatePB()
//        {
//            var param = new UpdatePBParametrs();

//            param.aim = task_aim;

//            switch (measure_task)
//            {
//                case TaskType.POINTS:
//                    param.title_text = points.ToString() + " / " + task_aim.ToString();
//                    param.pb_value = points;
//                    break;

//                case TaskType.TIME:
//                    param.pb_value = time;
//                    param.title_text = string.Format("{0:0}:{1:0}",
//                        (time / 60) % 60,
//                        time % 60) + " / " +

//                        string.Format("{0:0}:{1:0}",
//                        (task_aim / 60) % 60,
//                        task_aim % 60);
//                    break;

//                case TaskType.COINS:
//                    param.pb_value = catched_coins;
//                    param.title_text = catched_coins + " / " + task_aim.ToString();
//                    break;
//            }

//            Message m = new Message();
//            m.parametrs = param;
//            m.Type = MiniGameMessageType.PB_UPDATE;
//            MessageBus.Instance.SendMessage(m);
//        }

//        void UpdateTime(int value, bool time_down = false)
//        {
//            time += time_down ? -value : value;
//            Message msg = new Message();
//            msg.Type = Minigames.MiniGameMessageType.TIME_UPDATE;
//            msg.parametrs = new CommonMessageParametr(time);
//            MessageBus.Instance.SendMessage(msg);

//            UpdatePB();
//        }

//        void UpdateCoinsValue(int value)
//        {
//            catched_coins += value;
//            Message msg = new Message();
//            msg.Type = Minigames.MiniGameMessageType.COINS_UPDATE;
//            msg.parametrs = new CommonMessageParametr(catched_coins);
//            MessageBus.Instance.SendMessage(msg);

//            UpdatePB();
//        }

//        [Subscribe(Minigames.MiniGameMessageType.SCORE_RUNE_CATCHED)]
//        public void ScoreRune(Message msg)
//        {
//            UpdatePointsValue(10);
//            UpdateCoinsValue(1);
//        }

//        public void AdvContinue()
//        {
//        }

//        [Subscribe(Minigames.MiniGameMessageType.GAME_OVER)]
//        public void GameOver(Message msg)
//        {
//            //AppodealController.instance.CloseBanner();
//            MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_START);
//            DataController.instance.world_state_data.last_game_event = GAME_EVENT.LOSE_MINIGAME;

//            if (game_type == GameType.COINS)
//            {
//                GameStatistics.instance.SendStat("finish_minigame_" + cur_game_name, catched_coins);

//                Minigames.GameRecords gr =
//                    DataController.instance.gamesRecords.Record(
//                        DataController.instance.other_storage["game_name"].ToString());

//                if (gr.best_value < points)
//                {
//                    gr.best_value = points;
//                }

//                gr.last_value = points;
//                gr.last_coins = catched_coins;
//                DataController.instance.gamesRecords.setRecords(
//                    DataController.instance.other_storage["game_name"].ToString(), gr);

//                DataController.instance.catsPurse.Coins = coins + catched_coins;

//                DataController.instance.other_storage["ShowContinue"] = true;

//                if (DataController.instance.other_storage.ContainsKey("Continue") &&
//                (bool)DataController.instance.other_storage["Continue"])
//                {
//                    MessageBus.Instance.SendMessage(MiniGameMessageType.SHOW_ADV_MENU_RESULT);
//                    DataController.instance.other_storage["Continue"] = false;
//                    DataController.instance.other_storage["ShowContinue"] = false;
//                }
//                else
//                {

//                    Message m = new Message();
//                    m.Type = MiniGameMessageType.OPEN_RESULT;
//                    Dictionary<string, int> dic = new Dictionary<string, int>();
//                    dic.Add("catched_coins", catched_coins);
//                    dic.Add("points", points);
//                    CommonMessageParametr param = new CommonMessageParametr(dic);
//                    m.parametrs = param;

//                    MessageBus.Instance.SendMessage(m);
//                    MessageBus.Instance.SendMessage(MiniGameMessageType.CLOSE_SCORE_HEADER);

//                    m.parametrs = new UpdatePurseParametrs(coins,
//                        DataController.instance.catsPurse.Hearts,
//                        DataController.instance.catsPurse.Stars);
//                    m.Type = MiniGameMessageType.OPEN_GLOBAL_HEADER;
//                    MessageBus.Instance.SendMessage(m);
//                }
//            }

//            else
//            {
//                GameStatistics.instance.SendStat("lose_stargame_" + cur_game_name,
//                    StarTasksController.instance.get_cur_index(cur_game_name));

//                Analytics.CustomEvent("lose_stargame_" + cur_game_name, new Dictionary<string, object>
//                    {
//                        { "task_id", StarTasksController.instance.get_cur_index(cur_game_name) }
//                    });

//                if (!DataController.instance.catsPurse.InfinityHearts)
//                {
//                    DataController.instance.catsPurse.Hearts -= 1;
//                }
//                DataController.instance.catsPurse.Coins = coins + catched_coins;

//                Message m = new Message();
//                m.parametrs = new UpdatePurseParametrs(DataController.instance.catsPurse.Coins,
//                    DataController.instance.catsPurse.Hearts,
//                    DataController.instance.catsPurse.Stars);
//                m.Type = MiniGameMessageType.OPEN_GLOBAL_HEADER;
//                MessageBus.Instance.SendMessage(m);

//                MessageBus.Instance.SendMessage(MiniGameMessageType.CLOSE_SCORE_HEADER);
//                MessageBus.Instance.SendMessage(MiniGameMessageType.OPEN_LOSE_PANEL);
//            }
//        }

//        [Subscribe("START_FLY")]
//        public void StartFly(Message msg)
//        {
//            var p = Yaga.Helpers.CastHelper.Cast<UpdateInt>(msg.parametrs);
//            {
//                PointsAddValue = p.value;
//            }
//        }

//        [Subscribe("END_FLY")]
//        public void EndFly(Message msg)
//        {
//            PointsAddValue = 1;
//        }

//        bool inited = false;
//        float up_difficulty_timer = 10;
//        // Update is called once per frame
//        override public void ExtendedUpdate()
//        {
//            if (!inited)
//            {
//                inited = true;

//                if (StarTasksController.instance.get_cur_index(GameName.zigzag.ToString()) == 0)
//                {
//                    MessageBus.Instance.SendMessage(
//                        new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
//                            new TutorMaskController.TutorMaskParametr(
//                                MinigameObjects.instance.start_star_game_btn.GetComponent<RectTransform>().anchoredPosition, true, true,
//                                "start_star_minigame_btn")));
//                }

//                else if (DataController.instance.tasks_storage.content.ContainsKey("minigame_magnet_tutor") &&
//                   (bool)DataController.instance.tasks_storage.content["minigame_magnet_tutor"] == true)
//                {
//                    DataController.instance.tasks_storage.content["minigame_magnet_tutor"] = false;
//                    DataController.instance.tasks_storage.Store();

//                    MessageBus.Instance.SendMessage(
//                        new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
//                            new TutorMaskController.TutorMaskParametr(
//                                MinigameObjects.instance.start_star_magnet_btn.GetComponent<RectTransform>().anchoredPosition, true, true,
//                                "start_star_minigame_magnet_btn")));

//                    MessageBus.Instance.SendMessage(
//                        new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
//                            new TutorMaskController.TutorMaskParametr(
//                                MinigameObjects.instance.start_star_game_btn.GetComponent<RectTransform>().anchoredPosition, true, true,
//                                "start_star_minigame_btn")));
//                }
//                else if (DataController.instance.tasks_storage.content.ContainsKey("minigame_fly_tutor") &&
//                   (bool)DataController.instance.tasks_storage.content["minigame_fly_tutor"] == true)
//                {
//                    DataController.instance.tasks_storage.content["minigame_fly_tutor"] = false;
//                    DataController.instance.tasks_storage.Store();

//                    MessageBus.Instance.SendMessage(
//                        new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
//                            new TutorMaskController.TutorMaskParametr(
//                                MinigameObjects.instance.start_star_fly_btn.GetComponent<RectTransform>().anchoredPosition, true, true,
//                                "start_star_minigame_fly_btn")));

//                    MessageBus.Instance.SendMessage(
//                        new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
//                            new TutorMaskController.TutorMaskParametr(
//                                MinigameObjects.instance.start_star_game_btn.GetComponent<RectTransform>().anchoredPosition, true, true,
//                                "start_star_minigame_btn")));
//                }

//                else if (DataController.instance.tasks_storage.content.ContainsKey("minigame_reborn_tutor") &&
//                   (bool)DataController.instance.tasks_storage.content["minigame_reborn_tutor"] == true)
//                {
//                    DataController.instance.tasks_storage.content["minigame_reborn_tutor"] = false;
//                    DataController.instance.tasks_storage.Store();

//                    MessageBus.Instance.SendMessage(
//                        new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
//                            new TutorMaskController.TutorMaskParametr(
//                                MinigameObjects.instance.start_star_reborn_btn.GetComponent<RectTransform>().anchoredPosition, true, true,
//                                "start_star_minigame_reborn_btn")));

//                    MessageBus.Instance.SendMessage(
//                        new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
//                            new TutorMaskController.TutorMaskParametr(
//                                MinigameObjects.instance.start_star_game_btn.GetComponent<RectTransform>().anchoredPosition, true, true,
//                                "start_star_minigame_btn")));
//                }
//            }

//            TimeTimer -= Time.deltaTime;

//            if (TimeTimer <= 0.0f)
//            {
//                TimeTimer = 1.0f;

//                UpdateTime(1, time_down_mode);
//            }

//            PoinsAddTimer -= Time.deltaTime;

//            if (PoinsAddTimer <= 0)
//            {
//                UpdatePointsValue(PointsAddValue);

//                PoinsAddTimer = PoinsAddTimeOut;
//            }

//            up_difficulty_timer -= Time.deltaTime;
//            if (up_difficulty_timer <= 0)
//            {
//                up_difficulty_timer = 15.0f;

//                MessageBus.Instance.SendMessage(Minigames.MiniGameMessageType.MINIGAME_UP_DIFFICULT);
//            }

//            if (game_type == GameType.STARS)
//                CheckTask();
//        }
//    }
//}