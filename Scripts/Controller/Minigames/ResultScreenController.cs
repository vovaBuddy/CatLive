//using MainScene;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using Yaga;
//using Yaga.MessageBus;

//namespace Minigames
//{
//    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
//    public class ResultScreenController : ExtendedBehaviour
//    {

//        public GameObject Loading;

//        public void ShowLoading()
//        {
//            if (DataController.instance.catsPurse.Hearts > 0)
//                Loading.SetActive(true);
//        }

//        [Subscribe(MiniGameMessageType.SHOW_RESULT)]
//        public void GameOver(Message msg)
//        {
//            MessageBus.Instance.SendMessage(MiniGameMessageType.OPEN_RESULT);
//        }

//        public void ShowAdvResult()
//        {
//            MessageBus.Instance.SendMessage(MiniGameMessageType.SHOW_ADV_MENU_RESULT);
//        }

//        public void BuyChance()
//        {
//            if (DataController.instance.BuyHeartsForCoins())
//            {
//                Restart();
//            }
//            else
//            {
//                //not enouf money
//            }
//        }


//        public void RetryStarGame()
//        {
//            var show_ads = false;
//            if (DataController.instance.advEntity.sequential_games >= 3)
//            {
//                show_ads = true;
//                DataController.instance.advEntity.sequential_games = 0;
//            }

//            if (DataController.instance.catsPurse.Hearts > 0)
//            {
//                MessageBus.Instance.SendMessage(new Message(
//                    Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
//                    new Common.LoadingScreen.API.SceneNameParametr(
//                        Common.LoadingScreen.API.SceneNames.MINIGAMES, show_ads)));
//            }

//            else
//            {
//                MessageBus.Instance.SendMessage(MiniGameMessageType.OPEN_FAIL_PANEL);
//                Loading.SetActive(false);
//            }
//        }

//        public void AdvResult()
//        {
//            if (Time.realtimeSinceStartup - DataController.instance.advEntity.last_rew < 30)
//            {
//                return;
//            }

//            GameStatistics.instance.SendStat("require_rewarded_add_coins_minigame", 0);

//            AppodealController.instance.InitRewardActions(() =>
//            {
//                DataController.instance.advEntity.last_rew = Time.realtimeSinceStartup;

//                Minigames.GameRecords gr =
//                    DataController.instance.gamesRecords.Record(
//                        DataController.instance.other_storage["game_name"].ToString());

//                //DataController.instance.catsPurse.Coins += gr.last_value;
//                DataController.instance.catsPurse.Coins += 100;
//                MessageBus.Instance.SendMessage(new Message(
//                    Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
//                    new Common.LoadingScreen.API.SceneNameParametr(
//                        Common.LoadingScreen.API.SceneNames.MAIN, false)));
//            }, () =>
//            {
//                MessageBus.Instance.SendMessage(new Message(
//                    Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
//                    new Common.LoadingScreen.API.SceneNameParametr(
//                     Common.LoadingScreen.API.SceneNames.MAIN, false)));
//            });

//            AppodealController.instance.showRewarded();

//            //DataController.instance.advEntity.last_rew = Time.realtimeSinceStartup;

//            //Minigames.GameRecords gr =
//            //    DataController.instance.gamesRecords.Record(
//            //        DataController.instance.other_storage["game_name"].ToString());

//            //DataController.instance.catsPurse.Coins += 200;

//            //MessageBus.Restore();
//            //SceneManager.LoadScene("main");
//        }

//        public void AdvRestart()
//        {
//            if (Time.realtimeSinceStartup - DataController.instance.advEntity.last_rew < 30)
//            {
//                return;
//            }

//            AppodealController.instance.InitRewardActions(() =>
//            {
//                DataController.instance.advEntity.last_rew = Time.realtimeSinceStartup;
//                DataController.instance.catsPurse.Hearts += 1;
//                Restart();
//            }, () => {
//                MessageBus.Instance.SendMessage(new Message(
//                    Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
//                    new Common.LoadingScreen.API.SceneNameParametr(
//                     Common.LoadingScreen.API.SceneNames.MINIGAMES, false)));
//            });

//            AppodealController.instance.showRewarded();

//            GameStatistics.instance.SendStat("require_rewarded_add_hearts_stargame", 0);

//            //DataController.instance.advEntity.last_rew = Time.realtimeSinceStartup;
//            //DataController.instance.catsPurse.Hearts += 1;
//            //Restart();
//        }

//        [Subscribe(Minigames.MiniGameMessageType.PICK_UP_RESULT)]
//        public void HomeWithStar(Message msg)
//        {
//            Loading.SetActive(true);

//            var game_type = (GameType)System.Enum.Parse(typeof(GameType),
//                DataController.instance.other_storage["game_type"].ToString());
//            var cur_game_name = DataController.instance.other_storage["game_name"].ToString();

//            if (game_type == GameType.STARS)
//            {
//                GameStatistics.instance.SendStat("next_mission_stargame_" + cur_game_name,
//                    StarTasksController.instance.get_cur_index(cur_game_name));
//            }

//            var show_ads = false;

//            if (DataController.instance.advEntity.sequential_games >= 3)
//            {
//                show_ads = true;
//                DataController.instance.advEntity.sequential_games = 0;
//            }
//            MessageBus.Instance.SendMessage(new Message(
//                Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
//                new Common.LoadingScreen.API.SceneNameParametr(
//                    Common.LoadingScreen.API.SceneNames.MAIN, show_ads)));
//        }

//        public void Restart()
//        {
//            var game_type = (GameType)System.Enum.Parse(typeof(GameType),
//                DataController.instance.other_storage["game_type"].ToString());
//            var cur_game_name = DataController.instance.other_storage["game_name"].ToString();

//            if (game_type == GameType.STARS)
//            {
//                GameStatistics.instance.SendStat("next_mission_stargame_" + cur_game_name,
//                    StarTasksController.instance.get_cur_index(cur_game_name));
//            }

//            var show_ads = false;

//            if (DataController.instance.advEntity.sequential_games >= 3)
//            {
//                show_ads = true;
//                DataController.instance.advEntity.sequential_games = 0;
//            }
//            MessageBus.Instance.SendMessage(new Message(
//                Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
//                new Common.LoadingScreen.API.SceneNameParametr(
//                    Common.LoadingScreen.API.SceneNames.MINIGAMES, show_ads)));
//        }

//        public void Home()
//        {
//            //AppodealController.instance.ShowInterstitial();
//            MessageBus.Instance.SendMessage(new Message(
//                Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
//                new Common.LoadingScreen.API.SceneNameParametr(
//                    Common.LoadingScreen.API.SceneNames.MAIN, true)));
//        }

//        public void Continue()
//        {
//            //init start value

//            GameStatistics.instance.SendStat("require_rewarded_continue_minigame", 0);

//            AppodealController.instance.InitRewardActions(() =>
//            {
//                DataController.instance.other_storage["Continue"] = true;

//                MessageBus.Instance.SendMessage(new Message(
//                    Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
//                    new Common.LoadingScreen.API.SceneNameParametr(
//                        Common.LoadingScreen.API.SceneNames.MINIGAMES, false)));
//            }, () =>
//            {
//                MessageBus.Instance.SendMessage(new Message(
//                    Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
//                    new Common.LoadingScreen.API.SceneNameParametr(
//                        Common.LoadingScreen.API.SceneNames.MAIN, false)));
//            });


//            AppodealController.instance.showRewarded();
//            //DataController.instance.other_storage["Continue"] = true;

//            //MessageBus.Restore();
//            //SceneManager.LoadScene("minigames");

//        }

//        // Use this for initialization
//        override public void ExtendedStart()
//        {

//        }

//        // Update is called once per frame
//        override public void ExtendedUpdate()
//        {

//        }
//    }
//}