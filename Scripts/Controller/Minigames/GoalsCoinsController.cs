using MainScene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
namespace Minigames
{
    public class GoalsCoinsController : MonoBehaviour
    {

        // Use this for initialization
        public void Start()
        {
            if (string.Equals(DataController.instance.other_storage["game_type"],
                GameType.COINS.ToString()))
            {
                if (DataController.instance.other_storage.ContainsKey("Continue") &&
                    (bool)DataController.instance.other_storage["Continue"] == true)
                {
                    MessageBus.Instance.SendMessage(MiniGameMessageType.CLOSE_GOAL_SCREEN);
                }
                else
                {
                    MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_START);
                }
            }
        }

        public void StartGame()
        {
            MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_END);
            MessageBus.Instance.SendMessage(MiniGameMessageType.CLOSE_GOAL_SCREEN);
        }

        // Update is called once per frame
        public void Update()
        {

        }
    }
}
