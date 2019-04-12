using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;

namespace TimeManager.LosePanel
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class LosePanelController : ExtendedBehaviour
    {
        public LosePanelBinder binder;

        [Subscribe(LosePanelAPI.Messages.SHOW)]
        public void show(Message msg)
        {
            binder.LosePanel.SetActive(true);
        }

        public void ReStart()
        {
            if(DataController.instance.catsPurse.Hearts > 0)
            {
                Time.timeScale = 1.0f;
                GameStatistics.instance.SendStat("restart_stargame_tm",
                    StarTasksController.instance.get_cur_index());

                MessageBus.Instance.SendMessage(new Message(
                    Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
                    new Common.LoadingScreen.API.SceneNameParametr(
                        Common.LoadingScreen.API.SceneNames.MINIGAMES, false)));
            }
            else
            {
                Time.timeScale = 1.0f;
                binder.LosePanel.SetActive(false);
                binder.NoHertsPanel.SetActive(true);
            }
        }

        public void BuyChance()
        {
            if(DataController.instance.catsPurse.Coins >= 900)
            {
                DataController.instance.catsPurse.Coins -= 900;

                MessageBus.Instance.SendMessage(new Message(
                    Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
                    new Common.LoadingScreen.API.SceneNameParametr(
                        Common.LoadingScreen.API.SceneNames.MINIGAMES, false)));
            }
        }

        public void Revard()
        {
            AppodealController.instance.InitRewardActions(() => {
                MessageBus.Instance.SendMessage(new Message(
                    Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
                    new Common.LoadingScreen.API.SceneNameParametr(
                        Common.LoadingScreen.API.SceneNames.MINIGAMES, false)));
            }, () => {
                MessageBus.Instance.SendMessage(new Message(
                    Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
                    new Common.LoadingScreen.API.SceneNameParametr(
                        Common.LoadingScreen.API.SceneNames.MAIN, false)));
            });

            AppodealController.instance.showRewarded();
        }
    }
}