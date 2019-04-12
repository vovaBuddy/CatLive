using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga.MessageBus;
using Yaga;

public class BuyBusterParametr : MessageParametrs
{
    public BusterType type;
    public int price;
}


[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class BusterMenuController : ExtendedBehaviour {

    public Text title;

    public Image rebotn_img;
    public GameObject reborn_active_bg;
    public GameObject reborn_asq;
    public GameObject reborn_buy;
    public GameObject reborn_count;
    public GameObject reborn_btn;
    public GameObject reborn_big_btn;

    public Image magnit_img;
    public GameObject magnit_active_bg;
    public GameObject magnit_asq;
    public GameObject magnit_buy;
    public GameObject magnit_count;
    public GameObject magnit_btn;
    public GameObject magnit_big_btn;

    public Image fly_img;
    public GameObject fly_active_bg;
    public GameObject fly_asq;
    public GameObject fly_buy;
    public GameObject fly_count;
    public GameObject fly_btn;
    public GameObject fly_big_btn;

    public GameObject quest_panel;
    public Text quest_header;
    public Text quest_body;

    ///
    public GameObject buy_panel;
    public Text buy_panel_header;
    public Image item_img;
    public Text booster_name;
    public Text booster_description;
    public Button buy_btn;
    public Text price_text;

    public void OpenQuestPanel()
    {
        quest_panel.SetActive(true);
    }

    public void CloseQuestPanel()
    {
        quest_panel.SetActive(false);
    }

    [Subscribe(MainScene.MainMenuMessageType.UPDATE_BUSTER_VALUES)]
    public void BoughtBuster(Message msg)
    {
        Init();
    }

    public void OpenShop(string img_name, string buster_name, string buster_desc, int price, Action btn_buy_action)
    {
        buy_panel.SetActive(true);
        item_img.sprite = Resources.Load<Sprite>(img_name);
        booster_name.text = buster_name;
        booster_description.text = buster_desc;
        price_text.text = price.ToString();
        buy_btn.onClick.RemoveAllListeners();
        buy_btn.onClick.AddListener(() => { btn_buy_action(); });
    }

    public void CloseShop()
    {
        buy_panel.SetActive(false);
    }


    public void UseBuster(int type)
    {
        switch ((BusterType)type)
        {
            case BusterType.REBORN:
                if(!reborn_active_bg.activeSelf)
                    use_reborn();
                reborn_active_bg.SetActive(true);                
                break;

            case BusterType.MAGNIT:
                if (!magnit_active_bg.activeSelf)
                    use_magnit();
                magnit_active_bg.SetActive(true);
                break;

            case BusterType.FLY:
                if (!fly_active_bg.activeSelf)
                    use_fly();
                fly_active_bg.SetActive(true);
                break;
        }

    }

    void use_reborn()
    {
        DataController.instance.buster_entity.active_busters.Add(
            new RebornBuster(DataController.instance.buster_entity.getLevel(BusterType.REBORN)));
    }

    void use_magnit()
    {
        DataController.instance.buster_entity.active_busters.Add(
            new MagnitBuster(DataController.instance.buster_entity.getLevel(BusterType.MAGNIT)));
    }

    void use_fly()
    {
        DataController.instance.buster_entity.active_busters.Add(
            new FlyBuster(DataController.instance.buster_entity.getLevel(BusterType.FLY)));
    }

    void init_reborn(int cnt)
    {
        rebotn_img.sprite = Resources.Load<Sprite>(rebotn_img.sprite.name.Replace("02", "01"));

        reborn_asq.SetActive(false);
        reborn_buy.SetActive(false);
        reborn_count.SetActive(false);

        if (cnt==0)
        {
            reborn_big_btn.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            reborn_big_btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                OpenShop(rebotn_img.sprite.name.Replace("02", "01"), 
                    TextManager.getText("booster_reborn_name"), TextManager.getText("booster_reborn_description"), 100,
                    () => {
                        Message msg = new Message();
                        msg.Type = MainScene.MainMenuMessageType.BUY_BUSTER;
                        var p = new BuyBusterParametr();
                        p.price = 100;
                        p.type = BusterType.REBORN;
                        msg.parametrs = p;

                        MessageBus.Instance.SendMessage(msg);

                        CloseShop();
                    });
            });

            reborn_buy.SetActive(true);
        }
        else
        {
            reborn_count.SetActive(true);
            reborn_count.GetComponent<Text>().text = cnt.ToString();

            reborn_big_btn.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            reborn_big_btn.GetComponent<Button>().onClick.RemoveAllListeners();
            reborn_big_btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                UseBuster((int)BusterType.REBORN);
            });
        }        
    }

    void init_fly(int cnt)
    {
        fly_img.sprite = Resources.Load<Sprite>(fly_img.sprite.name.Replace("02", "01"));

        fly_asq.SetActive(false);
        fly_buy.SetActive(false);
        fly_count.SetActive(false);

        if (cnt == 0)
        {
            fly_big_btn.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            fly_big_btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                OpenShop(fly_img.sprite.name.Replace("02", "01"),
                    TextManager.getText("booster_supercat_name"), 
                    TextManager.getText("booster_supercat_description").
                        Replace("%N%", FlyBuster.set_time_by_lvl(
                            DataController.instance.buster_entity.getLevel(BusterType.FLY) * 50).ToString()), 
                    100,
                    () => {
                        Message msg = new Message();
                        msg.Type = MainScene.MainMenuMessageType.BUY_BUSTER;
                        var p = new BuyBusterParametr();
                        p.price = 100;
                        p.type = BusterType.FLY;
                        msg.parametrs = p;

                        MessageBus.Instance.SendMessage(msg);

                        CloseShop();
                    });
            });

            fly_buy.SetActive(true);
        }
        else
        {
            fly_count.SetActive(true);
            fly_count.GetComponent<Text>().text = cnt.ToString();

            fly_big_btn.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            fly_big_btn.GetComponent<Button>().onClick.RemoveAllListeners();
            fly_big_btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                UseBuster((int)BusterType.FLY);
            });
        }
    }

    void init_magnit(int cnt)
    {
        magnit_img.sprite = Resources.Load<Sprite>(magnit_img.sprite.name.Replace("02", "01"));

        magnit_asq.SetActive(false);
        magnit_buy.SetActive(false);
        magnit_count.SetActive(false);

        if (cnt == 0)
        {
            magnit_buy.SetActive(true);

            magnit_big_btn.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            magnit_big_btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                OpenShop(magnit_img.sprite.name.Replace("02", "01"), 
                    TextManager.getText("booster_magnet_name"), TextManager.getText("booster_magnet_description"), 100,
                    () => {
                        Message msg = new Message();
                        msg.Type = MainScene.MainMenuMessageType.BUY_BUSTER;
                        var p = new BuyBusterParametr();
                        p.price = 100;
                        p.type = BusterType.MAGNIT;
                        msg.parametrs = p;

                        MessageBus.Instance.SendMessage(msg);

                        CloseShop();
                    });
            });
        }
        else
        {
            magnit_count.SetActive(true);
            magnit_count.GetComponent<Text>().text = cnt.ToString();

            magnit_big_btn.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            magnit_big_btn.GetComponent<Button>().onClick.RemoveAllListeners();
            magnit_big_btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                UseBuster((int)BusterType.MAGNIT);
            });
        }
    }

    public void Init()
    {
        foreach (var pair in DataController.instance.buster_entity.buster_storage.content.busters)
        {
            switch (pair.Key)
            {
                case BusterType.REBORN:
                    init_reborn(pair.Value.count);
                    break;
                case BusterType.FLY:
                    init_fly(pair.Value.count);
                    break;
                case BusterType.MAGNIT:
                    init_magnit(pair.Value.count);
                    break;
            }

        }
    }
    // Use this for initialization
    override public void ExtendedStart () {

        quest_header.text = TextManager.getText("minigames_quest_booster_header_text");
        quest_body.text = TextManager.getText("minigames_quest_booster_body_text");
        buy_panel_header.text = TextManager.getText("minigames_buy_booster_panel_header_text");
        title.text = TextManager.getText("minigames_boosters_title");


        DataController.instance.buster_entity.active_busters.Clear();

        Init();
    }
}
