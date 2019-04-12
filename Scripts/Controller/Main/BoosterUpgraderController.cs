using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga.MessageBus;
using Yaga;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class BoosterUpgraderController : ExtendedBehaviour {

    public Text header;

    public Image reborn_img;
    public Text reborn_lvl_text;
    public Text reborn_btn_text;
    public GameObject reborn_btn;

    public Image magnit_img;
    public Text magnit_lvl_text;
    public Text magnit_btn_text;
    public GameObject magnit_btn;

    public Image fly_img;
    public Text fly_lvl_text;
    public Text fly_btn_text;
    public GameObject fly_btn;

    public GameObject question_panel;
    public Text question_panel_header;
    public Text question_panel_body;

    public GameObject buy_panel;
    public Text buy_panel_header;
    public Text booster_name;
    public Text upgrate_lvl;
    public Text upgrate_description;
    public Text upgrate_price;
    public Text befor_text;
    public Text after_text;
    public Image booster_img;
    public Button upgrate_btn;

    public void OpenUpgradePanel(string img_name, int new_level, string name, string descr, string before, string after, int price, Action btn_action)
    {
        buy_panel.SetActive(true);
        booster_img.sprite = Resources.Load<Sprite>(img_name);
        booster_name.text = name;
        upgrate_lvl.text = new_level.ToString();
        upgrate_description.text = descr;
        upgrate_price.text = price.ToString();
        befor_text.text = before;
        after_text.text = after;
        upgrate_btn.onClick.RemoveAllListeners();
        upgrate_btn.onClick.AddListener(() => { btn_action(); });
    }

    public void CloseUpgratePanel()
    {
        buy_panel.SetActive(false);
    }

    public void CloseQuestionPanel()
    {
        question_panel.SetActive(false);
    }

    public void OpenQuestionPanel()
    {
        question_panel.SetActive(true);
    }

    [Subscribe(MainScene.MainMenuMessageType.UPDATE_BUSTER_VALUES)]
    public void UpdateValues(Message msg)
    {
        Init();
    }

    void init_reborn(int l)
    {
        var price = DataController.instance.buster_entity.GetPrice(BusterType.REBORN);
        reborn_img.sprite = Resources.Load<Sprite>(reborn_img.sprite.name.Replace("02", "01"));
        reborn_lvl_text.text = l.ToString();
        reborn_btn.GetComponent<Image>().color = new Color(0, 255, 0);
        reborn_btn.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
        reborn_btn.GetComponent<Button>().onClick.AddListener(() =>
        {
            OpenUpgradePanel(reborn_img.sprite.name, l + 1,
                TextManager.getText("booster_reborn_name"),
                TextManager.getText("booster_reborn_description"),
                TextManager.getText("booster_reborn_upgrate_description").Replace("%N%", 
                    RebornBuster.set_cnt_by_lvl(l).ToString()),
                TextManager.getText("booster_reborn_upgrate_description").Replace("%N%",
                    RebornBuster.set_cnt_by_lvl(l + 1).ToString()),
                price,
                () =>
                {
                    Message msg = new Message();
                    msg.Type = MainScene.MainMenuMessageType.BUY_BUSTER_UPGRATE;
                    var p = new BuyBusterParametr();
                    p.price = price;
                    p.type = BusterType.REBORN;
                    msg.parametrs = p;

                    MessageBus.Instance.SendMessage(msg);

                    CloseUpgratePanel();
                });
        });
    }

    void init_fly(int l)
    {
        var price = DataController.instance.buster_entity.GetPrice(BusterType.FLY);
        fly_img.sprite = Resources.Load<Sprite>(fly_img.sprite.name.Replace("02", "01"));
        fly_lvl_text.text = l.ToString();
        fly_btn.GetComponent<Image>().color = new Color(0, 255, 0);
        fly_btn.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
        fly_btn.GetComponent<Button>().onClick.AddListener(() =>
        {
            OpenUpgradePanel(fly_img.sprite.name, l + 1,
                TextManager.getText("booster_supercat_name"),
                TextManager.getText("booster_supercat_description"),
                TextManager.getText("booster_supercat_upgrate_description").Replace("%N%",
                    (FlyBuster.set_time_by_lvl(l) * 50).ToString()),
                TextManager.getText("booster_supercat_upgrate_description").Replace("%N%",
                    (FlyBuster.set_time_by_lvl(l + 1) * 50).ToString()),
                price,
            () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.BUY_BUSTER_UPGRATE;
                var p = new BuyBusterParametr();
                p.price = price;
                p.type = BusterType.FLY;
                msg.parametrs = p;

                MessageBus.Instance.SendMessage(msg);

                CloseUpgratePanel();
            });
        });
    }

    void init_magnit(int l)
    {
        var price = DataController.instance.buster_entity.GetPrice(BusterType.MAGNIT);
        magnit_img.sprite = Resources.Load<Sprite>(magnit_img.sprite.name.Replace("02", "01"));
        magnit_lvl_text.text = l.ToString();
        magnit_btn.GetComponent<Image>().color = new Color(0, 255, 0);
        magnit_btn.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
        magnit_btn.GetComponent<Button>().onClick.AddListener(() =>
        {
            OpenUpgradePanel(magnit_img.sprite.name, l + 1,
                TextManager.getText("booster_magnet_name"),
                TextManager.getText("booster_magnet_description"),
                TextManager.getText("booster_magnet_upgrate_description").Replace("%N%",
                    MagnitBuster.set_time_by_lvl (l).ToString()),
                TextManager.getText("booster_magnet_upgrate_description").Replace("%N%",
                    MagnitBuster.set_time_by_lvl(l + 1).ToString()),
                price,
            () =>
            {
                Message msg = new Message();
                msg.Type = MainScene.MainMenuMessageType.BUY_BUSTER_UPGRATE;
                var p = new BuyBusterParametr();
                p.price = price;
                p.type = BusterType.MAGNIT;
                msg.parametrs = p;

                MessageBus.Instance.SendMessage(msg);

                CloseUpgratePanel();
            });
        });
    }

    public void Init()
    {
        foreach (var pair in DataController.instance.buster_entity.buster_storage.content.busters)
        {
            switch (pair.Key)
            {
                case BusterType.REBORN:
                    init_reborn(pair.Value.level);
                    break;
                case BusterType.FLY:
                    init_fly(pair.Value.level);
                    break;
                case BusterType.MAGNIT:
                    init_magnit(pair.Value.level);
                    break;
            }
        }
    }

    // Use this for initialization
    override public void ExtendedStart() {

        header.text = TextManager.getText("mm_boosters_upgr_header_text");
        fly_btn_text.text = TextManager.getText("mm_boosters_upgr_btn_text");
        reborn_btn_text.text = TextManager.getText("mm_boosters_upgr_btn_text");
        magnit_btn_text.text = TextManager.getText("mm_boosters_upgr_btn_text");
        buy_panel_header.text = TextManager.getText("mm_boosters_upgr_buy_panel_header_text");
        question_panel_header.text = TextManager.getText("minigames_quest_booster_header_text");
        question_panel_body.text = TextManager.getText("minigames_quest_booster_body_text");

        Init();
    }

}
