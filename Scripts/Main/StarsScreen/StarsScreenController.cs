using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;

namespace Main.StarsScreen
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class StarsScreenController : ExtendedBehaviour
    {
        StarsScreenBinder binder;
        bool hide_btn;

        // Use this for initialization
        public override void ExtendedStart()
        {
            binder = gameObject.transform.parent.Find("StarsScreenBinder").GetComponent<StarsScreenBinder>();
            Yaga.Localizer.Localize(binder);
        }

        [Subscribe(API.Messages.OPEN,
            MainScene.MainMenuMessageType.NOTE_ENOUGH_STARS)]
        public void Open(Message msg)
        {
            binder.panel.SetActive(true);
            binder.close_btn.SetActive(!hide_btn);
        }

        [Subscribe(API.Messages.CLOSE)]
        public void Close(Message msg)
        {
            binder.panel.GetComponent<Animator>().SetBool("close", true);
        }

        [Subscribe(API.Messages.HIDE_CLOSE_BTN)]
        public void HideCloseBtn(Message msg)
        {
            hide_btn = true;
        }

        [Subscribe(API.Messages.START_SCHEME_ANIMATION)]
        public void SchemeAnimStart(Message msg)
        {
            binder.scheme_animator.SetBool("start", true);
        }

        public void PlayBtnClicked()
        {
            GameStatistics.instance.SendStat("not_stars_panel_play_btn_pressed", 0);

            if (DataController.instance.world_state_data.need_first_booster_tutor == true)
            {
                DataController.instance.world_state_data.need_first_booster_tutor = false;
                MessageBus.Instance.SendMessage("CHAPTER_END_SHOW_BUSTER_TUTOR");
            }
            else
            {
                MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.START_MINI_GAME_ZIGZAG);
            }           
        }
    }
}