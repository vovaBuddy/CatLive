using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaga;
using Yaga.MessageBus;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class TutorController : ExtendedBehaviour
    {
        public GameObject tutor_window;
        public GameObject taptap_tutor_window;
        public GameObject zigzag_tutor_window;

        public override void ExtendedStart()
        {
            taptap_tutor_window.transform.Find("text_01").GetComponent<Text>().text =
                TextManager.getText("minigame_tutor_tap_text");
            taptap_tutor_window.transform.Find("text_02").GetComponent<Text>().text =
                TextManager.getText("minigame_tutor_action_tap_text");

            zigzag_tutor_window.transform.Find("text_01").GetComponent<Text>().text =
                TextManager.getText("minigame_tutor_tap_text");
            zigzag_tutor_window.transform.Find("text_02").GetComponent<Text>().text =
                TextManager.getText("minigame_tutor_action_zig_text");
        }

        //[Subscribe(MiniGameMessageType.TUTOR_TAP)]
        public void TutorTap()
        {
            DataController.instance.gamesRecords.tapTapTutorDone = true;
            MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_END);
            MessageBus.Instance.SendMessage(MiniGameMessageType.BTN_PRESSED);
            tutor_window.SetActive(false);
            taptap_tutor_window.SetActive(false);                  
        }

        //[Subscribe(MiniGameMessageType.TUTOR_TAP)]
        int tutor_count = 0;
        public void ZigZagTutorTap()
        {
            GameStatistics.instance.SendStat("ZigZagTutorTap", 0);

            tutor_count++;

            DataController.instance.gamesRecords.zigZagTutorDone = true;
            DataController.instance.gamesRecords.tutor_tap = true;

            MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_END);
            MessageBus.Instance.SendMessage(MiniGameMessageType.BTN_PRESSED);
            tutor_window.SetActive(false);
            zigzag_tutor_window.SetActive(false);

            if (tutor_count < 2)
                DataController.instance.gamesRecords.zigZagTutorDone = false;
        }

        [Subscribe(MiniGameMessageType.TUTOR_RUNE_CAHCED)]
        public void Tutor(Message msg)
        {
            if (DataController.instance.gamesRecords.tapTapTutorDone == false)
            {
                MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_START);
                tutor_window.SetActive(true);
                taptap_tutor_window.SetActive(true);
            }
        }

        [Subscribe(MiniGameMessageType.ZIGZAG_TUTOR_RUNE_CAHCED)]
        public void ZigZagTutor(Message msg)
        {
            if (DataController.instance.gamesRecords.zigZagTutorDone == false)
            {
                MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_START);
                tutor_window.SetActive(true);
                zigzag_tutor_window.SetActive(true);
            }
        }
    }
}
