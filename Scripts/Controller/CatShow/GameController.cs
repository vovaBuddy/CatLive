using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using UnityEngine.SceneManagement;

namespace CatShow
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class GameController : ExtendedBehaviour
    {
        int game_points;
        int coins;

        int obstacles = 23;
        int MAX_OBSTACLES = 23;
        int SUCCESS_POINT = 10;

        float game_factor;
        float factor_step = 0.07f;

        bool collided = false;



        [Subscribe(CatShowMessageType.MOVE_AWAY)]
        public void OnSuccessAvoidance(Message msg)
        {
            if (!collided)
            {
                Debug.Log("MOVE_AWAY");
                game_points += (int)(SUCCESS_POINT);
                //game_points += (int)(SUCCESS_POINT * game_factor);
                // if(game_factor < 2.0f) game_factor += factor_step;

                points_update_msg.parametrs = new UpdateInt(game_points);
                MessageBus.Instance.SendMessage(points_update_msg);

                //factor_update_msg.parametrs = new UpdateFloat(game_factor);
                //MessageBus.Instance.SendMessage(factor_update_msg);
            }

            collided = false;
        }

        [Subscribe(CatShowMessageType.COIN_CATCHED)]
        public void CatchedCoin(Message msg)
        {
            coins += 1;
            coins_update_msg.parametrs = new UpdateInt(coins);
            MessageBus.Instance.SendMessage(coins_update_msg);
        }

        [Subscribe(CatShowMessageType.SPAWNED)]
        public void Spawned(Message msg)
        {
            obstacles -= 1;

            obstacles_update_msg.parametrs = new UpdateInt(MAX_OBSTACLES - obstacles);
            MessageBus.Instance.SendMessage(obstacles_update_msg);
        }

        [Subscribe(CatShowMessageType.COLLISION)]
        public void onColl(Message msg)
        {
            Debug.Log("Collision");
            collided = true;

            game_factor = 1.0f;
            game_points -= SUCCESS_POINT;

            if (game_points < 0) game_points = 0;

            factor_update_msg.parametrs = new UpdateFloat(game_factor);
            MessageBus.Instance.SendMessage(factor_update_msg);

            points_update_msg.parametrs = new UpdateInt(game_points);
            MessageBus.Instance.SendMessage(points_update_msg);
        }

        Message factor_update_msg = new Message();
        Message points_update_msg = new Message();
        Message coins_update_msg = new Message();
        Message obstacles_update_msg = new Message();
        override public void ExtendedStart()
        {
            game_factor = 1.0f;
            coins = 0;

            DataController.instance.advEntity.sequential_games++;

            factor_update_msg.parametrs = new UpdateFloat(game_factor);
            factor_update_msg.Type = CatShowMessageType.UPDATE_GAME_FACTOR;
            MessageBus.Instance.SendMessage(factor_update_msg);

            obstacles_update_msg.parametrs = new UpdateInt(0);
            obstacles_update_msg.Type = CatShowMessageType.OBSTACLE_UPDATE;
            MessageBus.Instance.SendMessage(obstacles_update_msg);

            //game_points = 20 * DataController.instance.catsPurse.WearBeauty;
            SUCCESS_POINT = DataController.instance.catsPurse.WearBeauty;
            game_points = 0;

            points_update_msg.parametrs = new UpdateInt(game_points);
            points_update_msg.Type = CatShowMessageType.UPDATE_GAME_POINTS;
            MessageBus.Instance.SendMessage(points_update_msg);

            coins_update_msg.parametrs = new UpdateInt(coins);
            coins_update_msg.Type = CatShowMessageType.UPDATE_COINS;
            MessageBus.Instance.SendMessage(coins_update_msg);

            GameStatistics.instance.SendStat("start_catshow", 0);
        }

        [Subscribe(CatShowMessageType.END_GAME)]
        public void EndGame(Message msg)
        {
            DataController.instance.catsPurse.Beauty += game_points;
            DataController.instance.catsPurse.Coins += coins;

            DataController.instance.tasks_storage.content["catshow_first_played"] = true;
            DataController.instance.tasks_storage.Store();

            Message m = new Message();
            object[] objs = new object[2];

            objs[0] = game_points;
            objs[1] = coins;

            m.Type = CatShowMessageType.OPEN_RESULT;
            m.parametrs = new CommonMessageParametr(objs);
            MessageBus.Instance.SendMessage(m);
            //SceneManager.LoadScene("main");

            GameStatistics.instance.SendStat("finish_catshow", game_points);
        }

        public void Close()
        {
            //AppodealController.instance.ShowInterstitial();
            //MessageBus.Restore();
            MessageBus.Instance.SendMessage(new Message(
                Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
                new Common.LoadingScreen.API.SceneNameParametr(
                    Common.LoadingScreen.API.SceneNames.MAIN, false)));
        }

        public void Next()
        {
            if (!DataController.instance.tasks_storage.content.ContainsKey("first_show"))
            {
                DataController.instance.tasks_storage.content["first_show"] = true;
                DataController.instance.tasks_storage.Store();
                MessageBus.Instance.SendMessage(new Message(
                    Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
                    new Common.LoadingScreen.API.SceneNameParametr(
                        Common.LoadingScreen.API.SceneNames.MAIN, false)));
                return;
            }

            GameStatistics.instance.SendStat("next_catshow_btn", 0);

            if (DataController.instance.catsPurse.Energy > 0)
            {
                if (DataController.instance.advEntity.sequential_games >= 3)
                {
                    //AppodealController.instance.ShowInterstitial();
                    DataController.instance.advEntity.sequential_games = 0;
                }

                DataController.instance.catsPurse.Energy -= 1;
                MessageBus.Restore();
                SceneManager.LoadScene("cat_show_game");
            }
            else
            {
                MessageBus.Instance.SendMessage(CatShowMessageType.NOTE_ENOUGH_ENERGY);
            }
        }

        override public void ExtendedUpdate()
        {

            //time_update_msg.parametrs = new UpdateInt(time);
            //MessageBus.Instance.SendMessage(time_update_msg);

            if (obstacles < 0)
            {
                MessageBus.Instance.SendMessage(CatShowMessageType.LAST_OBSTACLE);
            }
                //    DataController.instance.catsPurse.Beauty += game_points;
                //    MessageBus.Restore();

                //    DataController.instance.tasks_storage.content["catshow_first_played"] = true;
                //    DataController.instance.tasks_storage.Store();

                //    SceneManager.LoadScene("main");
                //}

            }
    }
}