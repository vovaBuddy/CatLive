using MainScene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;

namespace Minigames
{
    [Extension(Extensions.PAUSE, Extensions.SUBSCRIBE_MESSAGE)]
    public class CatController : ExtendedBehaviour
    {
        CatControllerInterface cat = null;
        public DresserInitializer dr_initializer;

        public GameObject cat_ref;

        //нужны чтобы кэт.мув вызывался после апдейта
        //иначе клик по целям воспринимается как клип игровой
        bool start_phase_1 = false;
        bool start_phase_2 = false;

        [Subscribe(MiniGameMessageType.CLOSE_GOAL_SCREEN)]
        public void Init(Message msg)
        {
            switch ((GameName)System.Enum.Parse(typeof(GameName),
                DataController.instance.other_storage["game_name"].ToString()))
            {
                case GameName.taptap:
                    cat = new TapTap.CatController();
                    break;

                case GameName.zigzag:
                    cat = new ZigZag.CatController();
                    break;
            }
                        
            cat.InitCat();
            start_phase_1 = true;

            cat_ref = cat.getCat();

            dr_initializer.Init(cat_ref);            
        }

        // Use this for initialization
        override public void ExtendedStart()
        {

        }

        //[Subscribe(Minigames.MiniGameMessageType.MINIGAME_REBORN)]
        //public void Reborn(Message msg)
        //{
        //    if (cat is IRebornBusterable)
        //    {
        //        (cat as IRebornBusterable).Reborn();
        //    }
        //}

        [Subscribe("START_REBORN")]
        public void StartBusterReborn(Message msg)
        {
            if (cat is IRebornBusterable)
            {
                (cat as IRebornBusterable).StartReborn();
            }
        }


        [Subscribe("END_REBORN")]
        public void EndBusterReborn(Message msg)
        {
            if (cat is IRebornBusterable)
            {
                (cat as IRebornBusterable).EndReborn();
            }
        }

        [Subscribe("START_FLY")]
        public void StartBusterFly(Message msg)
        {
            if(cat is IFlyBusterable)
            {
                (cat as IFlyBusterable).StartFly();
            }
        }


        [Subscribe("END_FLY")]
        public void EndBusterFly(Message msg)
        {
            if (cat is IFlyBusterable)
            {
                (cat as IFlyBusterable).EndFly();
            }
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {
            if (start_phase_2)
            {
                cat.Move();

                if (Input.GetMouseButtonUp(0))
                {
                    MessageBus.Instance.SendMessage(MiniGameMessageType.BTN_PRESSED);
                    cat.OnTapAction();
                }
            }

            start_phase_2 = start_phase_1;
        }
    }
}
