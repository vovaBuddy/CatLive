using System.Collections;
using System.Collections.Generic;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using UnityEngine.UI;
using UnityEngine;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class CatPurseController : ExtendedBehaviour
    {

        public void AddRewardHeart()
        {
            DataController.instance.catsPurse.Hearts += 1;
            MessageBus.Instance.SendMessage(MainMenuMessageType.CLOSE_REWARD);
        }

        public void AddRewardEnergy()
        {
            DataController.instance.catsPurse.Energy += 1;
            MessageBus.Instance.SendMessage(MainMenuMessageType.CLOSE_REWARD);
        }

        public void AddRewardCoins()
        {
            DataController.instance.catsPurse.Coins += 100;
            MessageBus.Instance.SendMessage(MainMenuMessageType.CLOSE_REWARD);
        }

        [Subscribe(CatShow.CatShowMessageType.START_CAT_SHOW_GAME)]
        public void StarShow(Message msg)
        {
            if (DataController.instance.catsPurse.Energy >= 1)
            {
                DataController.instance.catsPurse.Energy -= 1;
                msg.Type = CatShow.CatShowMessageType.STARTED_CAT_SHOW_GAME;
                MessageBus.Instance.SendMessage(msg);
            }
            else
            {
                MessageBus.Instance.SendMessage(CatShow.CatShowMessageType.NOTE_ENOUGH_ENERGY);
            }
        }

        [Subscribe(MainMenuMessageType.SPEED_UP_TASK)]
        public void SpeedUpTask(Message msg)
        {
            var param = CastHelper.Cast<StartTaskParametrs>(msg.parametrs);

            if (DataController.instance.catsPurse.Coins >= param.price)
            {
                DataController.instance.catsPurse.Coins -= param.price;
                msg.Type = MainMenuMessageType.SPEED_UPED_TASK;
                MessageBus.Instance.SendMessage(msg);
            }
            else
            {
                MessageBus.Instance.SendMessage(MainMenuMessageType.NOTE_ENOUGH_MONEY);
            }
        }

        [Subscribe(MainMenuMessageType.START_TASK)]
        public void StarTask(Message msg)
        {
            var param = CastHelper.Cast<StartTaskParametrs>(msg.parametrs);

            //if(true)
            if (DataController.instance.catsPurse.Stars >= param.price)
            {
                DataController.instance.catsPurse.Stars -= param.price;
                msg.Type = MainMenuMessageType.START_STAR_ANIMATION;
                MessageBus.Instance.SendMessage(msg);
            }
            else
            {
                MessageBus.Instance.SendMessage(MainMenuMessageType.NOTE_ENOUGH_STARS);
            }
        }

        public void AdvHearts()
        {
            if(Time.realtimeSinceStartup - DataController.instance.advEntity.last_rew < 30)
            {
                return;
            }

            AppodealController.instance.InitRewardActions(() =>
            {
                DataController.instance.advEntity.last_rew = Time.realtimeSinceStartup;
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.REWARD_HEART);
            }, () => { });

            GameStatistics.instance.SendStat("require_rewarded_hearts", 0);
            AppodealController.instance.showRewarded();
        }

        public void AdvEnergy()
        {
            if (Time.realtimeSinceStartup - DataController.instance.advEntity.last_rew < 30)
            {
                return;
            }

            AppodealController.instance.InitRewardActions(() =>
            {
                DataController.instance.advEntity.last_rew = Time.realtimeSinceStartup;
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.REWARD_ENERGY);
                //DataController.instance.catsPurse.Energy += 1;
            }, () => { });

            GameStatistics.instance.SendStat("require_rewarded_energy", 0);
            AppodealController.instance.showRewarded();
        }

        public void AdvCoins()
        {
            if (Time.realtimeSinceStartup - DataController.instance.advEntity.last_rew < 30)
            {
                return;
            }

            AppodealController.instance.InitRewardActions(() =>
            {
                DataController.instance.advEntity.last_rew = Time.realtimeSinceStartup;
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.REWARD_COINS);
                //DataController.instance.catsPurse.Coins += 500;
            }, () => { });

            GameStatistics.instance.SendStat("require_rewarded_coins", 0);
            AppodealController.instance.showRewarded();
        }

        [Subscribe(MainMenuMessageType.BUY_HEART)]
        public void BuyHeart(Message msg)
        {
            if (DataController.instance.catsPurse.Coins >= 900)
            {
                DataController.instance.catsPurse.Coins -= 900;
                DataController.instance.catsPurse.Hearts += 1;

                GameStatistics.instance.SendStat("buy_heart", 0);
            }
            else
            {
                MessageBus.Instance.SendMessage(MainMenuMessageType.NOTE_ENOUGH_MONEY);
            }            
        }

        [Subscribe(MainMenuMessageType.BUY_ENERGY)]
        public void BuyEnergy(Message msg)
        {
            if (DataController.instance.catsPurse.Coins >= 900)
            {
                DataController.instance.catsPurse.Coins -= 900;
                DataController.instance.catsPurse.Energy += 1;

                GameStatistics.instance.SendStat("buy_energy", 0);
            }
            else
            {
                MessageBus.Instance.SendMessage(MainMenuMessageType.NOTE_ENOUGH_MONEY);
            }
        }

        [Subscribe(MainMenuMessageType.BUY_BUSTER)]
        public void BuyBuster(Message msg)
        {
            var param = CastHelper.Cast<BuyBusterParametr>(msg.parametrs);

            if (DataController.instance.catsPurse.Coins >= param.price)
            {
                DataController.instance.catsPurse.Coins -= param.price;
                msg.Type = MainMenuMessageType.BOUGHT_BUSTER;
                MessageBus.Instance.SendMessage(msg);
            }
            else
            {
                MessageBus.Instance.SendMessage(MainMenuMessageType.NOTE_ENOUGH_MONEY);
            }
        }

        [Subscribe(MainMenuMessageType.BUY_BUSTER_UPGRATE)]
        public void BuyBusterUpgrade(Message msg)
        {
            var param = CastHelper.Cast<BuyBusterParametr>(msg.parametrs);

            if (DataController.instance.catsPurse.Coins >= param.price)
            {
                DataController.instance.catsPurse.Coins -= param.price;
                msg.Type = MainMenuMessageType.BOUGHT_BUSTER_UPGRATE;
                MessageBus.Instance.SendMessage(msg);
            }
            else
            {
                MessageBus.Instance.SendMessage(MainMenuMessageType.NOTE_ENOUGH_MONEY);
            }
        }

        [Subscribe(MainMenuMessageType.BUY_ITEM)]
        public void BuyItem(Message msg)
        {
            var param = CastHelper.Cast<BuyItemParametr>(msg.parametrs);

            if(DataController.instance.catsPurse.Coins >= param.price)
            {
                DataController.instance.catsPurse.Coins -= param.price;
                msg.Type = MainMenuMessageType.BOUGHT_ITEM;
                MessageBus.Instance.SendMessage(msg);

                GameStatistics.instance.SendStat("buy_item_" + MainMenuMessageType.BOUGHT_ITEM.ToString(), param.price);
            }
            else
            {
                MessageBus.Instance.SendMessage(MainMenuMessageType.NOTE_ENOUGH_MONEY);
            }
        }
    }
}
