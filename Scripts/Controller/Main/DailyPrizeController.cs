using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.Storage;
using Yaga.MessageBus;
using UnityEngine.UI;

public class DailyPrizesAPI
{
    public class Messages
    {
        public const string CLOSE = "DailyPrizesAPI.CLOSE";
    }
}


public class DailyPrizeController : MonoBehaviour {

    public Text header;
    public GameObject daily_prize_panel;
    public GameObject prizes_icon_conteiner;
    public GameObject prizes_conteiner;

    public GameObject pb;
    int pb_width = 856;

    public GameObject daily_prize_prefub;
    public GameObject daily_prize_icon_prefub;

    public GameObject button_prize;
    public Text button_open_text;
    public Text button_pick_up_text;
    public Text next_text;

    Button action_btn;

    int cur_day;

    IEnumerator MovePB(int day)
    {
        day %= 5;
        int day_width = pb_width / 4; 
        int from = day == 1 ? 0 : day_width * (day - 2);
        int to = day_width * (day - 1);

        var y = pb.GetComponent<RectTransform>().sizeDelta.y;

        pb.GetComponent<RectTransform>().sizeDelta = new Vector2(from, y);

        yield return new WaitForSeconds(0.20f);

        while (pb.GetComponent<RectTransform>().sizeDelta.x < to)
        {
            pb.GetComponent<RectTransform>().sizeDelta = new Vector2(
                pb.GetComponent<RectTransform>().sizeDelta.x + Time.deltaTime * 100,
                y);

            yield return new WaitForSeconds(0.01f);
        }
    }

    [Serializable]
    public class DatetimeData
    {
        public string last_date_time_value;
        public int retention_record;
        public int day_after_install;

        public DatetimeData()
        {
            retention_record = 0;
            day_after_install = 0;
            last_date_time_value = null;
        }
    }

    public StorableData<DatetimeData> datetimeData;
    private bool need_update = false;
    private string datetime = null;


    class PrizeItem
    {
        PrizeType type;
        object info;

        public PrizeItem(PrizeType t, object i)
        {
            type = t;
            info = i;
        }
    }

    List<DailyPrize>[] daily_prizes;

    enum PrizeType
    {
        COINS, // Add coins
        INF_HEARTS, // call fn
        BOOST_SUBERCAT,// bought_boost
        BOOST_MAGNIT, // bought_boost
        BOOST_REBURN, // bought_boost
        DRESS, // icon
        FURNITURE // icon
    }


    // Use this for initialization
    void Start () {

        daily_prizes = new List<DailyPrize>[35];
        daily_prizes[0] = new List<DailyPrize>();
        daily_prizes[0].Add(new CoinsPrize(500));
        daily_prizes[0].Add(new InfHeartsPrize(3));

        daily_prizes[1] = new List<DailyPrize>();
        daily_prizes[1].Add(new CoinsPrize(750));
        daily_prizes[1].Add(new InfHeartsPrize(5));

        daily_prizes[2] = new List<DailyPrize>();
        daily_prizes[2].Add(new CoinsPrize(1000));
        daily_prizes[2].Add(new InfHeartsPrize(8));
        daily_prizes[2].Add(new TextirePrize("kitchen_003"));

        daily_prizes[3] = new List<DailyPrize>();
        daily_prizes[3].Add(new CoinsPrize(1500));
        daily_prizes[3].Add(new InfHeartsPrize(10));

        daily_prizes[4] = new List<DailyPrize>();
        daily_prizes[4].Add(new CoinsPrize(2000));
        daily_prizes[4].Add(new InfHeartsPrize(15));
        daily_prizes[4].Add(new TextirePrize("clothes_icons_but_001"));


        daily_prizes[5] = new List<DailyPrize>();
        daily_prizes[5].Add(new CoinsPrize(500));
        daily_prizes[5].Add(new InfHeartsPrize(3));

        daily_prizes[6] = new List<DailyPrize>();
        daily_prizes[6].Add(new CoinsPrize(750));
        daily_prizes[6].Add(new InfHeartsPrize(5));

        daily_prizes[7] = new List<DailyPrize>();
        daily_prizes[7].Add(new CoinsPrize(1000));
        daily_prizes[7].Add(new InfHeartsPrize(8));
        daily_prizes[7].Add(new TextirePrize("bad_002_02"));

        daily_prizes[8] = new List<DailyPrize>();
        daily_prizes[8].Add(new CoinsPrize(1500));
        daily_prizes[8].Add(new InfHeartsPrize(10));

        daily_prizes[9] = new List<DailyPrize>();
        daily_prizes[9].Add(new CoinsPrize(2000));
        daily_prizes[9].Add(new InfHeartsPrize(15));
        daily_prizes[9].Add(new TextirePrize("clothes_icons_collar_002"));

        //
        daily_prizes[10] = new List<DailyPrize>();
        daily_prizes[10].Add(new CoinsPrize(500));
        daily_prizes[10].Add(new InfHeartsPrize(3));

        daily_prizes[11] = new List<DailyPrize>();
        daily_prizes[11].Add(new CoinsPrize(750));
        daily_prizes[11].Add(new InfHeartsPrize(5));

        daily_prizes[12] = new List<DailyPrize>();
        daily_prizes[12].Add(new CoinsPrize(1000));
        daily_prizes[12].Add(new InfHeartsPrize(8));
        daily_prizes[12].Add(new TextirePrize("chair_002_04"));

        daily_prizes[13] = new List<DailyPrize>();
        daily_prizes[13].Add(new CoinsPrize(1500));
        daily_prizes[13].Add(new InfHeartsPrize(10));

        daily_prizes[14] = new List<DailyPrize>();
        daily_prizes[14].Add(new CoinsPrize(2000));
        daily_prizes[14].Add(new InfHeartsPrize(15));
        daily_prizes[14].Add(new TextirePrize("clothes_icons_glasses_002"));

        //
        daily_prizes[15] = new List<DailyPrize>();
        daily_prizes[15].Add(new CoinsPrize(500));
        daily_prizes[15].Add(new InfHeartsPrize(3));

        daily_prizes[16] = new List<DailyPrize>();
        daily_prizes[16].Add(new CoinsPrize(750));
        daily_prizes[16].Add(new InfHeartsPrize(5));

        daily_prizes[17] = new List<DailyPrize>();
        daily_prizes[17].Add(new CoinsPrize(1000));
        daily_prizes[17].Add(new InfHeartsPrize(8));
        daily_prizes[17].Add(new TextirePrize("floor_hs_tile_004"));

        daily_prizes[18] = new List<DailyPrize>();
        daily_prizes[18].Add(new CoinsPrize(1500));
        daily_prizes[18].Add(new InfHeartsPrize(10));

        daily_prizes[19] = new List<DailyPrize>();
        daily_prizes[19].Add(new CoinsPrize(2000));
        daily_prizes[19].Add(new InfHeartsPrize(15));
        daily_prizes[19].Add(new TextirePrize("clothes_icons_skin_002"));

        //
        daily_prizes[20] = new List<DailyPrize>();
        daily_prizes[20].Add(new CoinsPrize(500));
        daily_prizes[20].Add(new InfHeartsPrize(3));

        daily_prizes[21] = new List<DailyPrize>();
        daily_prizes[21].Add(new CoinsPrize(750));
        daily_prizes[21].Add(new InfHeartsPrize(5));

        daily_prizes[22] = new List<DailyPrize>();
        daily_prizes[22].Add(new CoinsPrize(1000));
        daily_prizes[22].Add(new InfHeartsPrize(8));
        daily_prizes[22].Add(new TextirePrize("floor_ktch_tile_004"));

        daily_prizes[23] = new List<DailyPrize>();
        daily_prizes[23].Add(new CoinsPrize(1500));
        daily_prizes[23].Add(new InfHeartsPrize(10));

        daily_prizes[24] = new List<DailyPrize>();
        daily_prizes[24].Add(new CoinsPrize(2000));
        daily_prizes[24].Add(new InfHeartsPrize(15));

        //
        daily_prizes[25] = new List<DailyPrize>();
        daily_prizes[25].Add(new CoinsPrize(500));
        daily_prizes[25].Add(new InfHeartsPrize(3));

        daily_prizes[26] = new List<DailyPrize>();
        daily_prizes[26].Add(new CoinsPrize(750));
        daily_prizes[26].Add(new InfHeartsPrize(5));

        daily_prizes[27] = new List<DailyPrize>();
        daily_prizes[27].Add(new CoinsPrize(1000));
        daily_prizes[27].Add(new InfHeartsPrize(8));
        daily_prizes[27].Add(new TextirePrize("wall_hs_tile_005"));

        daily_prizes[28] = new List<DailyPrize>();
        daily_prizes[28].Add(new CoinsPrize(1500));
        daily_prizes[28].Add(new InfHeartsPrize(10));

        daily_prizes[29] = new List<DailyPrize>();
        daily_prizes[29].Add(new CoinsPrize(2000));
        daily_prizes[29].Add(new InfHeartsPrize(15));

        //
        daily_prizes[30] = new List<DailyPrize>();
        daily_prizes[30].Add(new CoinsPrize(500));
        daily_prizes[30].Add(new InfHeartsPrize(3));

        daily_prizes[31] = new List<DailyPrize>();
        daily_prizes[31].Add(new CoinsPrize(750));
        daily_prizes[31].Add(new InfHeartsPrize(5));

        daily_prizes[32] = new List<DailyPrize>();
        daily_prizes[32].Add(new CoinsPrize(1000));
        daily_prizes[32].Add(new InfHeartsPrize(8));
        daily_prizes[32].Add(new TextirePrize("wall_ktch_tile_004"));

        daily_prizes[33] = new List<DailyPrize>();
        daily_prizes[33].Add(new CoinsPrize(1500));
        daily_prizes[33].Add(new InfHeartsPrize(10));

        daily_prizes[34] = new List<DailyPrize>();
        daily_prizes[34].Add(new CoinsPrize(2000));
        daily_prizes[34].Add(new InfHeartsPrize(15));

        header.text = TextManager.getText("mm_daily_prize_header");
        button_open_text.text = TextManager.getText("mm_daily_prize_button_open_text");
        button_pick_up_text.text = TextManager.getText("mm_daily_prize_button_pick_up_text");

        datetimeData = new StorableData<DatetimeData>("DatetimeData");

        ServeredData data = new ServeredData();
        data.GetCurDateTime((ServeredData.TimeAnswer answ) =>

        {
            need_update = true;
            datetime = answ.data.time;
        },

        () => { });
    }


    void PickUpsPrizes(int day)
    {
        for (int i = 0; i < daily_prizes[day].Count; i++)
        {
            daily_prizes[day][i].ActiveAction();
        }

        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CHECK_DAILY_CONDITION);
    }

    IEnumerator OpenPrizesPanel(int day)
    {
        for (int i = 0; i < daily_prizes[day].Count; i++)
        {
            var go = Instantiate(daily_prize_prefub, prizes_conteiner.transform);
            daily_prizes[day][i].CreatePrefub(go);

            yield return new WaitForSeconds(0.15f);
        }
    }

    void OpenDailyPanel(int day)
    {         
        StartCoroutine(MovePB(day));

        daily_prize_panel.SetActive(true);

        int day_5 = day % 5;

        for(int i = 1; i < 6; ++i)
        {
            var go = Instantiate(daily_prize_icon_prefub, prizes_icon_conteiner.transform);

            if (i < day_5)
                go.GetComponent<DailyPrizeIconUI>().Init(DailyPrizeIconUI.DPIState.DONE, i);
            else if (i == day_5)
            {
                go.GetComponent<DailyPrizeIconUI>().Init(DailyPrizeIconUI.DPIState.TODAY, i);
                go.GetComponent<DailyPrizeIconUI>().btn.onClick.AddListener(() =>
                {
                    OpenBtn();
                    go.GetComponent<DailyPrizeIconUI>().btn.onClick.RemoveAllListeners();
                });

                action_btn = go.GetComponent<DailyPrizeIconUI>().btn;
            }
            else
                go.GetComponent<DailyPrizeIconUI>().Init(DailyPrizeIconUI.DPIState.FUTURE, i);
        }
    }

    public void close()
    {
        daily_prize_panel.SetActive(false);
        MessageBus.Instance.SendMessage(DailyPrizesAPI.Messages.CLOSE);
    }

    public void OpenBtn()
    {
        StartCoroutine(OpenPrizesPanel(cur_day));

        button_prize.GetComponent<Button>().onClick.RemoveAllListeners();
        button_prize.GetComponent<Button>().onClick.AddListener(() =>
        {
            close();
            PickUpsPrizes(cur_day);
        });
        button_prize.GetComponent<Animator>().SetBool("pressed", true);
    }

    public void ShadulePush(int day)
    {
        if(!DataController.instance.push_entity.notification_scheduled)
        {
            if (!DataController.instance.push_entity.data.content.set_natification)
            {
                int d = datetimeData.content.day_after_install == 7 || datetimeData.content.day_after_install == 30 ?
                    (datetimeData.content.day_after_install == 7 ? 4 : 16) : day;

                if (d > 16)
                    d = 15;

                OneSignalController.someMethod(d);

                DataController.instance.push_entity.notification_scheduled = true;
                DataController.instance.push_entity.data.content.set_natification = true;
                DataController.instance.push_entity.data.Store();
            }
            else
            {
                DataController.instance.push_entity.data.content.set_natification = false;
                DataController.instance.push_entity.data.Store();

                //Test!!!!
                if(System.DateTime.Now.Hour < 17)
                //if (System.DateTime.Now.Hour < 8)
                {
                    int d = datetimeData.content.day_after_install == 7 || datetimeData.content.day_after_install == 30 ?
                        (datetimeData.content.day_after_install == 7 ? 4 : 16) : day;

                    if (d > 16)
                        d = 15;

                    OneSignalController.someMethod(d);
                    DataController.instance.push_entity.data.content.set_natification = true;
                    DataController.instance.push_entity.data.Store();
                }
            }
        }
    }

    void CheckDailyPrize(string datetime)
    {
        button_pick_up_text.gameObject.SetActive(false);

        var splitted = datetime.Split('-');
        var part = splitted[2];
        var subs = part.Substring(3, 2);
        var current_time = Int32.Parse(subs);
        next_text.text = TextManager.getText("mm_daily_prize_next_text").Replace("%N%", (24 - current_time).ToString());

        button_prize.GetComponent<Button>().onClick.AddListener(() =>
        {
            action_btn.onClick.Invoke();
        });

        if (datetimeData.content.last_date_time_value == null)
        {
            datetimeData.content.last_date_time_value = datetime;
            datetimeData.content.retention_record = 1;
            datetimeData.Store();

            ShadulePush(datetimeData.content.retention_record);

            MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CHECK_DAILY_CONDITION);
        }
        else
        {
            int stored_month = Int32.Parse(datetimeData.content.last_date_time_value.Split('-')[1]);
            int stored_year = Int32.Parse(datetimeData.content.last_date_time_value.Split('-')[0]);
            int current_month = Int32.Parse(datetime.Split('-')[1]);

            int stored_data = Int32.Parse(datetimeData.content.last_date_time_value.Split('-')[2].Substring(0,2));
            int current_data = Int32.Parse(datetime.Split('-')[2].Substring(0, 2));

            if(current_data - 1 >= stored_data || current_month > stored_month)
            {
                datetimeData.content.last_date_time_value = datetime;
                datetimeData.content.retention_record += 1;
                datetimeData.Store();

                ShadulePush(datetimeData.content.retention_record);

                cur_day = datetimeData.content.retention_record - 1;

                OpenDailyPanel(datetimeData.content.retention_record - 1);
            }

            if(stored_month == current_month)
            {
                datetimeData.content.day_after_install += current_data - stored_data;
                datetimeData.Store();
            }
            else
            {
                datetimeData.content.day_after_install +=
                    Helper.TextHelper.getNumberOfDays(stored_month, stored_year) - stored_data + current_data;
                datetimeData.Store();
            }
        }
    }

    // Update is called once per frame

    bool one_frame_pause = true;
    void Update () {

        if(one_frame_pause)
        {
            one_frame_pause = false;
            return;
        }

        if (need_update && !Task.TaskController.GetController().check_any_task_in_action()
            && !DialogController.GetController().DialogWindow.activeSelf)
        {
            CheckDailyPrize(datetime);
            need_update = false;
        }
    }
}
