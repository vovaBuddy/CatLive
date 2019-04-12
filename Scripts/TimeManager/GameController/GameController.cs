using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using TimeManager.Level;

namespace TimeManager
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class GameController : ExtendedBehaviour
    {
        public GameObject Loading;

        public static class Messages
        {
            public const string START_GAME = "TimeManager.START_GAME";
        }
        
        public GameObject StartGameControllers;
        public LevelsController levelsController;

        [Subscribe(Messages.START_GAME)]
        public void StartGame(Message msg)
        {
            StartGameControllers.SetActive(true);
            StartCoroutine(levelsController.init());
        }

        public void HomeBtn()
        {
            Time.timeScale = 1.0f;
            MessageBus.Instance.SendMessage(new Message(
                Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
                new Common.LoadingScreen.API.SceneNameParametr(
                    Common.LoadingScreen.API.SceneNames.MAIN, false)));
        }

        [Subscribe(Minigames.MiniGameMessageType.PICK_UP_RESULT)]
        public void HomeWithStar(Message msg)
        {
            //Loading.SetActive(true);

            MessageBus.Instance.SendMessage(new Message(
                Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
                new Common.LoadingScreen.API.SceneNameParametr(
                    Common.LoadingScreen.API.SceneNames.MAIN, false)));
        }
    }
}