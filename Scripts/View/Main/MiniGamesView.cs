using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class MiniGamesView : ExtendedBehaviour {

        public GameObject mini_games;
        public Text header_text;
        public Text coins_game_text;
        public Text stars_game_text;
        public Text fail_rule_text;

        [Subscribe(MainMenuMessageType.OPEN_MINI_GAMES)]
        public void Open(Message msg)
        {
            if (DataController.instance.world_state_data.need_first_booster_tutor == true)
            {
                DataController.instance.world_state_data.need_first_booster_tutor = false;
                MessageBus.Instance.SendMessage("CHAPTER_END_SHOW_BUSTER_TUTOR");
            }
            else
            {
                mini_games.GetComponent<Animator>().SetBool("close", false);
                //if (!Task.TaskController.GetController().check_any_task_in_action())
                mini_games.SetActive(true);
            }
        }

        [Subscribe(MainMenuMessageType.CLOSE_MINI_GAMES)]
        public void Close(Message msg)
        {
            mini_games.GetComponent<Animator>().SetBool("close", true);
        }


        // Use this for initialization
        override public void ExtendedStart() {

        header_text.text = TextManager.getText("mm_minigames_header_text");
        coins_game_text.text = TextManager.getText("mm_minigames_coins_game_text");
        stars_game_text.text = TextManager.getText("mm_minigames_stars_game_text");
        //fail_rule_text.text = TextManager.getText("mm_minigames_fail_rule_text");

        mini_games.SetActive(false);
        }

        // Update is called once per frame
        override public void ExtendedUpdate() {

        }
    }
}