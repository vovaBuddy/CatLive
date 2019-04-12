using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.Helpers;
using Yaga.MessageBus;
using UnityEngine.UI;
using MainScene;
using Minigames;
using Helper;
using System;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class CoinsGUIView : ExtendedBehaviour {

    public Text coins;
    public Text points;
    public GameObject header;


    public Image pb_image;
    public Text pb_text;
    public GameObject pb;
    int pb_max_width = 940;

    public Text coins_st;
    public Text time_st;
    public Text points_st;

    public GameObject header_stars;

    public Action pb_action;

    public TaskType measure_task { get; private set; }

    [Subscribe(Minigames.MiniGameMessageType.PB_UPDATE)]
    public void UpdatePB(Message msg)
    {
        var param = CastHelper.Cast<UpdatePBParametrs>(msg.parametrs);
        pb_text.text = param.title_text;

        pb.GetComponent<RectTransform>().sizeDelta =
            new Vector2((param.pb_value / (float)param.aim) * pb_max_width,
            pb.GetComponent<RectTransform>().sizeDelta.y);

    }

    [Subscribe(Minigames.MiniGameMessageType.CLOSE_SCORE_HEADER)]
    public void CloseHeader(Message msg)
    {
        header.SetActive(false);
        header_stars.SetActive(false);
    }

    [Subscribe(Minigames.MiniGameMessageType.TIME_UPDATE)]
    public void UpdateTime(Message m)
    {
        var value = Int32.Parse(CastHelper.Cast<CommonMessageParametr>(m.parametrs).obj.ToString());
        time_st.text = string.Format("{0:00}:{1:00}",
                        (value / 60) % 60,
                        value % 60);
    }

    [Subscribe(Minigames.MiniGameMessageType.COINS_UPDATE)]
    public void UpdateCoins(Message m)
    {
        var param = CastHelper.Cast<CommonMessageParametr>(m.parametrs);
        coins.text = param.obj.ToString();
        coins_st.text = param.obj.ToString();
    }

    [Subscribe(Minigames.MiniGameMessageType.SCORE_UPDATE)]
    public void UpdateScore(Message m)
    {
        var param = CastHelper.Cast<CommonMessageParametr>(m.parametrs);
        points.text = param.obj.ToString();
        points_st.text = param.obj.ToString();
    }

    // Use this for initialization
    override public void ExtendedStart () {
        //AppodealController.instance.ShowBanner();

        coins.text = DataController.instance.catsPurse.Coins.ToString();

        if (string.Equals(DataController.instance.other_storage["game_type"],
            GameType.STARS.ToString()))
        {
            //measure_task = StarTasksController.instance.get_cur_task(
            //    DataController.instance.other_storage["game_name"].ToString())
            //    .task_info[0].type;

            //header.SetActive(false);
            //header_stars.SetActive(true);

            //pb_image.sprite = ResourceHelper.LoadSprite(
            //    ResourcesNameHelper.UI_ICON_NAME_BY_TASK(measure_task));

            //time_st.text = "0";
        }
        else
        {
            header.SetActive(true);
            header_stars.SetActive(false);
        }
    }

    // Update is called once per frame
    override public void ExtendedUpdate() {

    }
}
