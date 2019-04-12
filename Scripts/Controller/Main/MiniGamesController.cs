using Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yaga.MessageBus;
using Yaga;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class MiniGamesController : ExtendedBehaviour
    {
        //tmp
        public List<TimeManager.Level.LevelConfig> level_confs;

        public GameObject coins_game_items_content;
        public GameObject stars_game_items_content;

        public GameObject btn_stars;
        public Image star_image;
        public Text star_header;
        public Text star_body;

        public GameObject btn_coins;
        public Image coins_image;
        public Text coins_header;
        public Text coins_body;

        public GameObject coins_games_panel;

        [Subscribe(MainMenuMessageType.SHOW_COINS_GAMES)]
        public void ShowCoinsGames(Message msg)
        {
            coins_games_panel.SetActive(true);
            DataController.instance.minigames_screen_data.ShowCoinsGame = true;
        }

        bool inited = false;
        public override void ExtendedStart()
        {
            //Init();
            coins_games_panel.SetActive(DataController.instance.minigames_screen_data.ShowCoinsGame);

            btn_stars.transform.GetChild(0).gameObject.GetComponent<Text>().text = TextManager.getText("mm_minigames_play_btn_text");
            btn_coins.transform.GetChild(0).gameObject.GetComponent<Text>().text = TextManager.getText("mm_minigames_play_btn_text");
            

            //var task = StarTasksController.instance.get_cur_task();
            string text = TextManager.getText("mm_minigames_aims_text");

            //!!!
            //ToDo add aims
            //!!!

            //text += TextManager.getText("mm_minigames_" + task.task_info[0].type.ToString() + "_text")
            //    + " " + task.task_info[0].value.ToString();

            //if(task.task_info.Count == 2)
            //{
            //    if(task.task_info[1].type == TaskType.TIME_OUT)
            //    {
            //        text += " " + TextManager.getText("mm_minigames_in_seconds_text").Replace("%N%", task.task_info[1].value.ToString());
            //    }
            //}

            star_body.text = text;
            star_header.text = TextManager.getText("mm_minigames_level_text") + " " +
                (StarTasksController.instance.get_cur_index() + 1);
            coins_header.text = TextManager.getText("mm_minigames_best_text");

            GameRecords rec = DataController.instance.gamesRecords.Record(GameName.zigzag.ToString());
            coins_body.text = "Best: " + rec.best_value + "\n" +
                "Last: " + rec.last_value; ;
        }

        public void Init()
        {
            //if (inited)
            //    return;

            //inited = true;

            //GameObject gi = (GameObject)Instantiate(Resources.Load("game_item"));

            //if (DataController.instance.tasks_storage.content.ContainsKey("game_tutor_done") &&
            //    (bool)DataController.instance.tasks_storage.content["game_tutor_done"])
            //{
            //    if (StarTasksController.instance.star_tasks.tasks_entity.content.cur_task_indexes[GameName.taptap] >= 5)
            //    {
            //        gi.transform.parent = coins_game_items_content.transform;
            //        gi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //        gi.GetComponent<GameItem>().game_name = GameName.zigzag;
            //        gi.GetComponent<GameItem>().game_type = GameType.COINS;
            //        gi.GetComponent<GameItem>().Init();
            //    }

            //    gi = (GameObject)Instantiate(Resources.Load("game_item"));
            //    gi.transform.parent = coins_game_items_content.transform;
            //    gi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //    gi.GetComponent<GameItem>().game_name = GameName.taptap;
            //    gi.GetComponent<GameItem>().game_type = GameType.COINS;
            //    gi.GetComponent<GameItem>().Init();

            //    if (StarTasksController.instance.star_tasks.tasks_entity.content.cur_task_indexes[GameName.taptap] >= 5)
            //    {
            //        gi = (GameObject)Instantiate(Resources.Load("game_item"));
            //        gi.transform.parent = stars_game_items_content.transform;
            //        gi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //        gi.GetComponent<GameItem>().game_name = GameName.zigzag;
            //        gi.GetComponent<GameItem>().game_type = GameType.STARS;
            //        gi.GetComponent<GameItem>().Init();
            //    }

            //    gi = (GameObject)Instantiate(Resources.Load("game_item"));
            //    gi.transform.parent = stars_game_items_content.transform;
            //    gi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //    gi.GetComponent<GameItem>().game_name = GameName.taptap;
            //    gi.GetComponent<GameItem>().game_type = GameType.STARS;
            //    gi.GetComponent<GameItem>().Init();
            //}
            //else
            //{
            //    gi = (GameObject)Instantiate(Resources.Load("game_item"));
            //    gi.transform.parent = stars_game_items_content.transform;
            //    gi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //    gi.GetComponent<GameItem>().game_name = GameName.taptap;
            //    gi.GetComponent<GameItem>().game_type = GameType.STARS;
            //    gi.GetComponent<GameItem>().Init();
            //}
        }

        public void Open()
        {
            MessageBus.Instance.SendMessage(MainMenuMessageType.OPEN_MINI_GAMES);

            //GameObject gi = (GameObject)Instantiate(Resources.Load("game_item"));

            //Init();
        }

        public void Close()
        {
            MessageBus.Instance.SendMessage(MainMenuMessageType.CLOSE_MINI_GAMES);
            DataController.instance.gamesRecords.StarMinigameNeed = false;
        }

        [Subscribe(MainMenuMessageType.START_MINI_GAME_ZIGZAG)]
        public void OPenZigZagGame(Message msg)
        {
            OpenZigZagStars();
        }

        public void OpenZigZagStars()
        {
            if (DataController.instance.catsPurse.Hearts > 0)
            {
                //MessageBus.Restore();
                DataController.instance.other_storage["game_name"] =
                    GameName.zigzag.ToString();

                DataController.instance.other_storage["game_type"] =
                    GameType.STARS.ToString();

                MessageBus.Instance.SendMessage(new Message(
                    Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
                    new Common.LoadingScreen.API.SceneNameParametr(
                        Common.LoadingScreen.API.SceneNames.MINIGAMES, false)));
            }
            else
            {
                MessageBus.Instance.SendMessage(MainMenuMessageType.NOTE_ENOUGH_HEARTS);
            }
        }

        public void OpenZigZagCoins()
        {
            if (DataController.instance.catsPurse.Hearts > 0)
            {
                //MessageBus.Restore();
                DataController.instance.other_storage["game_name"] =
                    GameName.zigzag.ToString();

                DataController.instance.other_storage["game_type"] =
                    GameType.COINS.ToString();

                MessageBus.Instance.SendMessage(new Message(
                    Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
                    new Common.LoadingScreen.API.SceneNameParametr(
                        Common.LoadingScreen.API.SceneNames.MINIGAMES, false)));
            }
            else
            {
                MessageBus.Instance.SendMessage(MainMenuMessageType.NOTE_ENOUGH_HEARTS);
            }
        }
    }
}