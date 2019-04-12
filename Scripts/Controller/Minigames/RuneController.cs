using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using MainScene;

namespace Minigames
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE, Extensions.PAUSE)]
    public class RuneController : ExtendedBehaviour
    {
        List<IRuneSpawner> spawners;

        // Use this for initialization
        override public void ExtendedStart()
        {
            spawners = new List<IRuneSpawner>();

            switch ((GameName)System.Enum.Parse(typeof(GameName),
                DataController.instance.other_storage["game_name"].ToString()))
            {
                case GameName.taptap:
                    spawners.Add(new TapTap.RuneSpawner());
                    break;

                case GameName.zigzag:
                    spawners.Add(new ZigZag.ScoreRuneSpawner());
                    break;
            }
            

            foreach (var spwn in spawners)
            {
                spwn.Init();
            }
        }

        [Subscribe(Minigames.MiniGameMessageType.MINIGAME_UP_DIFFICULT)]
        public void UpDifficult(Message msg)
        {
            foreach (var spwn in spawners)
            {
                if (spwn is ZigZag.ScoreRuneSpawner)
                {
                    (spwn as ZigZag.ScoreRuneSpawner).UpDifficult();
                }
            }

        }

        [Subscribe(MiniGameMessageType.UPDATE_INIT_POS)]
        public void UpdatePos(Message msg)
        {
            foreach (var spwn in spawners)
            {
                spwn.UpdateInitPos(msg);
            }
        }

        [Subscribe(MiniGameMessageType.INIT_PLATFORM)]
        public void InitPLatform(Message msg)
        {
            foreach (var spwn in spawners)
            {
                spwn.InitPlatform(msg);
            }
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {
            foreach (var spwn in spawners)
            {
                spwn.Update();
            }
        }

    }
}