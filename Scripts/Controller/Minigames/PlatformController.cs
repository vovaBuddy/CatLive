using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using MainScene;
using Yaga.MessageBus;


namespace Minigames
{
    [Extension(Extensions.PAUSE, Extensions.SUBSCRIBE_MESSAGE)]
    public class PlatformController : ExtendedBehaviour
    {
        PlatformCreater platform_creater;

        // Use this for initialization
        override public void ExtendedStart()
        {
            switch ((GameName)System.Enum.Parse(typeof(GameName),
                DataController.instance.other_storage["game_name"].ToString()))
            {
                case GameName.taptap:
                    platform_creater = new TapTap.PlatformCreater();
                    break;

                case GameName.zigzag:
                    platform_creater = new ZigZag.PlatformCreater();
                    break;
            }

            platform_creater.LoadStartPlatform();
        }

        [Subscribe(Minigames.MiniGameMessageType.MINIGAME_REBORN)]
        public void Reborn(Message msg)
        {
            if (platform_creater is IRebornBusterable)
            {
                (platform_creater as IRebornBusterable).Reborn();
            }
        }

        [Subscribe("START_FLY_PLATFORM")]
        public void StartBusterFly(Message msg)
        {
            if (platform_creater is IFlyBusterable)
            {
                (platform_creater as IFlyBusterable).StartFly();
            }
        }

        [Subscribe(Minigames.MiniGameMessageType.MINIGAME_UP_DIFFICULT)]
        public void UpDifficult(Message msg)
        {
            if(platform_creater is ZigZag.PlatformCreater)
            {
                (platform_creater as ZigZag.PlatformCreater).UpDifficult();
            }
        }


        [Subscribe("END_FLY")]
        public void EndBusterFly(Message msg)
        {
            if (platform_creater is IFlyBusterable)
            {
                (platform_creater as IFlyBusterable).EndFly();
            }
        }

        [Subscribe(MiniGameMessageType.REVERSE_RUNE_CATCHED)]
        public void Reverse(Message msg)
        {
            (platform_creater as TapTap.PlatformCreater).ReflexAngle();
        }

        [Subscribe(Minigames.MiniGameMessageType.BTN_PRESSED)]
        public void BtnPressed(Message msg)
        {
            platform_creater.onBtnPressed(msg);
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {
            platform_creater.MovePlatform();
            platform_creater.UpdatePlatform();
        }
    }
}
