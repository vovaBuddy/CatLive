using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yaga.MessageBus;
using Yaga.Helpers;
using Minigames;

namespace MainScene
{
    public enum GameType
    {
        COINS = 0, 
        STARS = 1,
    }

    public enum GameName
    {
        zigzag, 
        taptap,
    }


    public class GameItem : MonoBehaviour
    {
        public GameType game_type;
        public GameName game_name;

        public Image icon;
        public Text title;
        public Text score;

        public GameObject btn;

        public void Init()
        {
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (DataController.instance.catsPurse.Hearts > 0 || game_type == GameType.COINS)
                {
                    //MessageBus.Restore();
                    DataController.instance.other_storage["game_name"] =
                        game_name.ToString();

                    DataController.instance.other_storage["game_type"] =
                        game_type.ToString();

                    MessageBus.Instance.SendMessage(new Message(
                        Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
                        new Common.LoadingScreen.API.SceneNameParametr(
                            Common.LoadingScreen.API.SceneNames.MINIGAMES, false)));
                }

                else
                {
                    MessageBus.Instance.SendMessage(MainMenuMessageType.NOTE_ENOUGH_HEARTS);
                }
            });

            icon.sprite = ResourceHelper.LoadSprite(game_name.ToString());

            title.text = game_name.ToString().ToUpper() + " CAT";

            //if (game_type == GameType.COINS)
            //{
            //    GameRecords rec =
            //        DataController.instance.gamesRecords.Record(game_name.ToString());
            //    score.text = "Best: " + rec.best_value + "\n" +
            //        "Last: " + rec.last_value;
            //}
            //else if(game_type == GameType.STARS)
            //{
            //    var task = StarTasksController.instance.get_cur_task(game_name.ToString());
            //    string text = TextManager.getText("mm_minigames_aims_text");

            //    foreach (TaskInfo t in task.task_info)
            //    {
            //        text += TextManager.getText("mm_minigames_" + t.type.ToString() + "_text")
            //            + " " + t.value.ToString() + "\n";
            //    }

            //    score.text = text;
            //}

            btn.transform.GetChild(0).gameObject.GetComponent<Text>().text = TextManager.getText("mm_minigames_play_btn_text");
        }
    }
}
