using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;
using Yaga;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

class PrizeController : MonoBehaviour
{
    public ServeredData.PrizeAnswer answer;
    public bool set_value = false;

    public GameObject CanvasPrizes;

    public GameObject week_prize;
    public Text wp_beauty;
    public Text wp_place;
    public Text wp_place_in;

    public GameObject prize_all;
    public Text wp_pa_time_prize;
    public Text wp_pa_coins;

    public GameObject prize_coins;
    public Text wp_pc_coins;

    public Text points_header_text;
    public Text points_points_text;
    public Text points_prize_text;
    public Text points_btn_text;
    public Text week_header_text;
    public Text week_points_text;
    public Text week_place_text;
    public Text week_percent_place_text;
    public Text week_prize_text;
    public Text week_btn_text;

    ///

    public GameObject PointsPrize;
    public Text pp_beauty;
    public Text pp_energy;
    public Text pp_coins;

    void ShowPointsPrize(int beauty, int energy, int coins)
    {
        DataController.instance.catsPurse.Coins += coins;
        DataController.instance.catsPurse.Energy += energy;

        PointsPrize.SetActive(true);
        pp_beauty.text = beauty.ToString();
        pp_energy.text = energy.ToString();
        pp_coins.text = coins.ToString();
    }

    void ShowWeekPrize(int beauty, int place, string place_in)
    {        
        //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);

        CanvasPrizes.SetActive(true);
        week_prize.SetActive(true);

        wp_beauty.text = beauty.ToString();
        wp_place.text = "#" + place.ToString();
        wp_place_in.text = place_in;
    }

    void ShowPrizePanelAll(string time, int coins)
    {
        prize_all.SetActive(true);
        wp_pa_time_prize.text = time;
        wp_pa_coins.text = coins.ToString();
    }

    void ShowPrizePanelCoins(int coins)
    {
        prize_coins.SetActive(true);
        wp_pc_coins.text = coins.ToString();
    }

    void checkBeautyPrizes()
    {
        var b_value = DataController.instance.catsPurse.Beauty;

        if(b_value >= 50 && !DataController.instance.catsPurse.HasPrize(0))
        {
            DataController.instance.catsPurse.SetPrize(0);
            ShowPointsPrize(50, 1, 100);
        }
       else if (b_value >= 150 && !DataController.instance.catsPurse.HasPrize(1))
        {
            DataController.instance.catsPurse.SetPrize(1);
            ShowPointsPrize(150, 1, 150);
        }
        else if (b_value >= 300 && !DataController.instance.catsPurse.HasPrize(2))
        {
            DataController.instance.catsPurse.SetPrize(2);
            ShowPointsPrize(300, 1, 200);
        }
        else if (b_value >= 600 && !DataController.instance.catsPurse.HasPrize(3))
        {
            DataController.instance.catsPurse.SetPrize(3);
            ShowPointsPrize(600, 1, 300);
        }
        else if(b_value >= 900 && !DataController.instance.catsPurse.HasPrize(4))
        {
            DataController.instance.catsPurse.SetPrize(4);
            ShowPointsPrize(900, 1, 400);
        }
        else if(b_value >= 1200 && !DataController.instance.catsPurse.HasPrize(5))
        {
            DataController.instance.catsPurse.SetPrize(5);
            ShowPointsPrize(1200, 1, 500);
        }
        else if(b_value >= 1600 && !DataController.instance.catsPurse.HasPrize(6))
        {
            ShowPointsPrize(1600, 1, 600);
            DataController.instance.catsPurse.SetPrize(6);
        }
        else if(b_value >= 2000 && !DataController.instance.catsPurse.HasPrize(7))
        {
            ShowPointsPrize(2000, 1, 800);
            DataController.instance.catsPurse.SetPrize(7);
        }
        else if(b_value >= 2400 && !DataController.instance.catsPurse.HasPrize(8))
        {
            ShowPointsPrize(2400, 1, 1000);
            DataController.instance.catsPurse.SetPrize(8);
        }
        else if(b_value >= 3200 && !DataController.instance.catsPurse.HasPrize(9))
        {
            ShowPointsPrize(3200, 1, 1400);
            DataController.instance.catsPurse.SetPrize(9);
        }
        else if(b_value >= 4000 && !DataController.instance.catsPurse.HasPrize(10))
        {
            ShowPointsPrize(4000, 1, 1900);
            DataController.instance.catsPurse.SetPrize(10);
        }
        else if(b_value >= 4800 && !DataController.instance.catsPurse.HasPrize(11))
        {
            ShowPointsPrize(4800, 1, 2000);
            DataController.instance.catsPurse.SetPrize(11);
        }
        else if(b_value >= 6400 && !DataController.instance.catsPurse.HasPrize(12))
        {
            ShowPointsPrize(6400, 1, 2500);
            DataController.instance.catsPurse.SetPrize(12);
        }
        else if(b_value >= 8000 && !DataController.instance.catsPurse.HasPrize(13))
        {
            ShowPointsPrize(8000, 1, 3000);
            DataController.instance.catsPurse.SetPrize(13);
        }
        else if(b_value >= 9600 && !DataController.instance.catsPurse.HasPrize(14))
        {
            ShowPointsPrize(9600, 1, 3700);
            DataController.instance.catsPurse.SetPrize(14);
        }
        else if(b_value >= 12800 && !DataController.instance.catsPurse.HasPrize(15))
        {
            ShowPointsPrize(12800, 1, 4400);
            DataController.instance.catsPurse.SetPrize(15);
        }
        else if(b_value >= 16000 && !DataController.instance.catsPurse.HasPrize(16))
        {
            ShowPointsPrize(16000, 1, 5100);
            DataController.instance.catsPurse.SetPrize(16);
        }
        else if(b_value >= 19200 && !DataController.instance.catsPurse.HasPrize(17))
        {
            ShowPointsPrize(19200, 1, 6000);
            DataController.instance.catsPurse.SetPrize(17);
        }
    }

    public void Start()
    {
        points_header_text.text = TextManager.getText("prize_points_header_text");
        points_points_text.text = TextManager.getText("prize_points_points_text");
        points_prize_text.text = TextManager.getText("prize_points_prize_text");
        points_btn_text.text = TextManager.getText("prize_points_btn_text");
        week_header_text.text = TextManager.getText("prize_week_header_text");
        week_points_text.text = TextManager.getText("prize_week_points_text");
        week_place_text.text = TextManager.getText("prize_week_place_text");
        week_percent_place_text.text = TextManager.getText("prize_week_percent_place_text");
        week_prize_text.text = TextManager.getText("prize_week_prize_text");
        week_btn_text.text = TextManager.getText("prize_week_btn_text");

        ServeredData data = new ServeredData();
        data.GetPrize((ServeredData.PrizeAnswer answ) =>

        {
            answer = answ;
            set_value = true;
        },

        () => { });

        checkBeautyPrizes();
    }

    public void Show(ServeredData.PrizeAnswer answ)
    {
        int value = answ.data.value;
        int cnt = answ.data.cnt;
        int place = answ.data.place;

        DataController.instance.catsPurse.RefreshPrizes();

        float place_percent = (place / (float)cnt) * 100;

        if (place_percent <= 3)
        {
            DataController.instance.catsPurse.Coins += 30000;
            DataController.instance.SetInfinitHearts(1 * 60 * 60);

            ShowPrizePanelAll("1ч", 30000);

            ShowWeekPrize(value, place, "1-3%");


        }
        else if (place_percent > 3 && place_percent <= 10)
        {
            DataController.instance.catsPurse.Coins += 15000;
            DataController.instance.SetInfinitHearts(30 * 60);

            ShowPrizePanelAll("30м", 15000);

            ShowWeekPrize(value, place, "3-10%");

        }
        else if (place_percent > 10 && place_percent <= 25)
        {
            DataController.instance.SetInfinitHearts(1 * 10 * 60);
            DataController.instance.catsPurse.Coins += 5000;

            ShowPrizePanelAll("10м", 5000);

            ShowWeekPrize(value, place, "10-25%");

        }
        else if (place_percent > 25 && place_percent <= 50)
        {
            DataController.instance.SetInfinitHearts(1 * 5 * 60);
            DataController.instance.catsPurse.Coins += 1500;

            ShowPrizePanelAll("5м", 1500);
            ShowWeekPrize(value, place, "25-50%");

        }
        else if (place_percent > 50 && place_percent <= 100)
        {
            DataController.instance.catsPurse.Coins += 500;

            ShowPrizePanelCoins(500);
            ShowWeekPrize(value, place, "50-100%");

        }
    }

    public void Share()
    {
        GameObject.Find("Share").GetComponent<NativeShare>().ShareScreenshotWithText("Узнай какой ты кот прямо сейчас! goo.gl/9ED1nR");
    }

    bool check_bprizes = false;
    public void PickUp()
    {
        if(week_prize.activeSelf)
        {
            MessageBus.Instance.SendMessage(new Message(
                Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
                new Common.LoadingScreen.API.SceneNameParametr(
                    Common.LoadingScreen.API.SceneNames.MAIN, false)));
        }
        CanvasPrizes.SetActive(false);
        PointsPrize.SetActive(false);
        check_bprizes = true;
    }

    public void Update()
    {
        if(set_value)
        {
            set_value = false;

            Show(answer);
        }

        if(check_bprizes)
        {
            check_bprizes = false;

            checkBeautyPrizes();
        }
    }

    public static PrizeController GetController()
    {
        return GameObject.Find("Controllers").transform.Find("PrizeController")
            .GetComponent<PrizeController>();
    }

}
