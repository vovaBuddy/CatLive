using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using UnityEngine.UI;
using System.Linq;
using System;
using MainScene;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class Customizer : ExtendedBehaviour
{
    public MainScene.ShopItem default_kitchen_wall_shop_item;
    public MainScene.ShopItem default_kitchen_floor_shop_item;
    public MainScene.ShopItem default_home_wall_shop_item;
    public MainScene.ShopItem normal_garden_floor_shop_item;

    public GameObject kitchen_set;

    public GameObject buy_panel;
    public Image icon;
    public GameObject price_panel;
    public GameObject adv_panel;
    public GameObject retention_panel;
    public Text retention_panel_text;
    public Text price_value;
    public Button ok;
    public Button no;
    public Button single_ok;
    public Button close;
    public Button panel_ok;
    public Button panel_no;

    public CustomizerView cust_view;

    [Subscribe(MainScene.MainMenuMessageType.PREVIEW_ITEM)]
    public void PreviewItem(Message msg)
    {
        var param = CastHelper.Cast<MainScene.BuyItemParametr>(msg.parametrs);

        panel_ok.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
        panel_ok.onClick.RemoveAllListeners();
        panel_ok.onClick.AddListener(() =>
        {
            msg.Type = MainScene.MainMenuMessageType.PREBUY_ITEM;
            MessageBus.Instance.SendMessage(msg);
        });

        GameStatistics.instance.SendStat("customizer_preview_item_pressed_" + param.type, 0);
    }

    [Subscribe(MainScene.MainMenuMessageType.PREBUY_ITEM)]
    public void BuyItem(Message msg)
    {
        var param = CastHelper.Cast<MainScene.BuyItemParametr>(msg.parametrs);

        GameStatistics.instance.SendStat("customizer_buy_item_btn_pressed_" + param.type, param.price);

        buy_panel.SetActive(true);

        icon.sprite = param.item_sprite;

        if (param.price < -1)
        {
            adv_panel.SetActive(false);
            price_panel.SetActive(false);
            retention_panel.SetActive(true);

            ok.gameObject.SetActive(false);
            no.gameObject.SetActive(false);
            single_ok.gameObject.SetActive(true);
            single_ok.onClick.RemoveAllListeners();
            single_ok.onClick.AddListener(() =>
            {
                buy_panel.SetActive(false);

                panel_ok.onClick.RemoveAllListeners();
                panel_ok.onClick.AddListener(() =>
                {
                    cust_view.Apply();
                });

                panel_no.onClick.RemoveAllListeners();
                panel_no.onClick.AddListener(() =>
                {
                    cust_view.Close();
                });
            });
        }
        else if(param.price == -1)
        {
            adv_panel.SetActive(true);
            price_panel.SetActive(false);
            retention_panel.SetActive(false);
            ok.gameObject.SetActive(true);
            no.gameObject.SetActive(true);
            single_ok.gameObject.SetActive(false);

            ok.onClick.RemoveAllListeners();
            ok.onClick.AddListener(() =>
            {
                GameStatistics.instance.SendStat("customizer_buy_item_revorded_ok_pressed_" + param.type, 0);

                AppodealController.instance.InitRewardActions(() =>
                {
                    GameStatistics.instance.SendStat("customizer_buy_item_revorded_item_bought_" + param.type, 0);

                    param.price = 0;
                    msg.parametrs = param;
                    msg.Type = MainScene.MainMenuMessageType.BUY_ITEM;
                    MessageBus.Instance.SendMessage(msg);
                    buy_panel.SetActive(false);
                }, () => { });

                AppodealController.instance.showRewarded();

                panel_ok.onClick.RemoveAllListeners();
                panel_ok.onClick.AddListener(() =>
                {
                    cust_view.Apply();
                });

                panel_no.onClick.RemoveAllListeners();
                panel_no.onClick.AddListener(() =>
                {
                    cust_view.Close();
                });
            });
        }
        else
        {
            adv_panel.SetActive(false);
            price_panel.SetActive(true);
            retention_panel.SetActive(false);
            ok.gameObject.SetActive(true);
            no.gameObject.SetActive(true);
            single_ok.gameObject.SetActive(false);

            price_value.text = param.price.ToString();

            ok.onClick.RemoveAllListeners();
            ok.onClick.AddListener(() =>
            {
                GameStatistics.instance.SendStat("customizer_buy_item_coins_ok_pressed_" + param.type, 0);

                msg.Type = MainScene.MainMenuMessageType.BUY_ITEM;
                MessageBus.Instance.SendMessage(msg);
                buy_panel.SetActive(false);

                panel_ok.onClick.RemoveAllListeners();
                panel_ok.onClick.AddListener(() =>
                {
                    cust_view.Apply();
                });

                panel_no.onClick.RemoveAllListeners();
                panel_no.onClick.AddListener(() =>
                {
                    cust_view.Close();
                });
            });
        }

        no.onClick.RemoveAllListeners();
        no.onClick.AddListener(() =>
        {
            GameStatistics.instance.SendStat("customizer_buy_item_no_pressed_" + param.type, 0);

            buy_panel.SetActive(false);

            panel_ok.onClick.RemoveAllListeners();
            panel_ok.onClick.AddListener(() =>
            {
                cust_view.Apply();
            });

            panel_no.onClick.RemoveAllListeners();
            panel_no.onClick.AddListener(() =>
            {
                cust_view.Close();
            });
        });

        close.onClick.RemoveAllListeners();
        close.onClick.AddListener(() =>
        {
            buy_panel.SetActive(false);

            GameStatistics.instance.SendStat("customizer_buy_item_close_pressed_" + param.type, 0);

            panel_ok.onClick.RemoveAllListeners();
            panel_ok.onClick.AddListener(() =>
            {
                cust_view.Apply();
            });

            panel_no.onClick.RemoveAllListeners();
            panel_no.onClick.AddListener(() =>
            {
                cust_view.Close();
            });
        });
    }

    [Subscribe(MainScene.MainMenuMessageType.DRESS_DEFAULT_KITCHEN_WALL)]
    public void SetDefaultKitchenWall(Message msg)
    {
        var param = new MainScene.BuyItemParametr();
        param.price = 0;
        param.type = MainScene.ShopItemType.KITCHEN_WALL;
        param.item_texture = default_kitchen_wall_shop_item.item_texture;
        param.item_sprite = default_kitchen_wall_shop_item.item_sprite;
        Message m = new Message();
        m.parametrs = param;
        m.Type = MainScene.MainMenuMessageType.BUY_ITEM;
        MessageBus.Instance.SendMessage(m);
    }

    [Subscribe(MainScene.MainMenuMessageType.DRESS_DEFAULT_KITCHEN_FLOOR)]
    public void SetDefaultKitchenFloor(Message msg)
    {
        var param = new MainScene.BuyItemParametr();
        param.price = 0;
        param.type = MainScene.ShopItemType.KITCHEN_FLOOR;
        param.item_texture = default_kitchen_floor_shop_item.item_texture;
        param.item_sprite = default_kitchen_floor_shop_item.item_sprite;
        Message m = new Message();
        m.parametrs = param;
        m.Type = MainScene.MainMenuMessageType.BUY_ITEM;
        MessageBus.Instance.SendMessage(m);
    }

    [Subscribe(MainScene.MainMenuMessageType.DRESS_DEFAULT_HOME_WALL)]
    public void SetDefaultHomeWall(Message msg)
    {
        var param = new MainScene.BuyItemParametr();
        param.price = 0;
        param.type = MainScene.ShopItemType.HOME_WALL;
        param.item_texture = default_home_wall_shop_item.item_texture;
        param.item_sprite = default_home_wall_shop_item.item_sprite;
        Message m = new Message();
        m.parametrs = param;
        m.Type = MainScene.MainMenuMessageType.BUY_ITEM;
        MessageBus.Instance.SendMessage(m);

        m.Type = MainScene.MainMenuMessageType.DRESS_ITEM;
        MessageBus.Instance.SendMessage(m);
    }

    [Subscribe(MainScene.MainMenuMessageType.DRESS_NORMAL_GARDEN_FLOOR)]
    public void SetNormalGardenFloor(Message msg)
    {
        var param = new MainScene.BuyItemParametr();
        param.price = 0;
        param.type = MainScene.ShopItemType.GARDEN1_FLOOR;
        param.item_texture = normal_garden_floor_shop_item.item_texture;
        param.item_sprite = normal_garden_floor_shop_item.item_sprite;
        Message m = new Message();
        m.parametrs = param;
        m.Type = MainScene.MainMenuMessageType.BUY_ITEM;
        MessageBus.Instance.SendMessage(m);

        m.Type = MainScene.MainMenuMessageType.DRESS_ITEM;
        MessageBus.Instance.SendMessage(m);
    }

    [Subscribe(MainScene.MainMenuMessageType.TAKEOFF_ITEM)]
    public void TakeOff(Message msg)
    {
        var p = CastHelper.Cast<MainScene.BuyItemParametr>(msg.parametrs);

        switch (p.type)
        {
            case MainScene.ShopItemType.KITCHEN_SET:
                kitchen_set.SetActive(false);
                break;
        }

    }

    [Subscribe(MainScene.MainMenuMessageType.DRESS_ITEM,
        MainScene.MainMenuMessageType.PREVIEW_ITEM,
        MainScene.MainMenuMessageType.PRE_DRESS_ITEM)]
    public void ChangeWear(Message msg)
    {
        var p = CastHelper.Cast<MainScene.BuyItemParametr>(msg.parametrs);

        switch (p.type)
        {
            case MainScene.ShopItemType.KITCHEN_SET:
                kitchen_set.SetActive(true);
                kitchen_set.GetComponent<SpriteRenderer>().sprite =
                    p.item_sprite;
                break;

            case MainScene.ShopItemType.HOME_FLOOR:
                var floor_transform = MainLocationOjects.instance.home_floor.transform;
                for (int i = 0; i < MainLocationOjects.instance.home_floor.transform.childCount; ++i)
                {
                    floor_transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                        p.item_sprite;
                }
                break;
            case MainScene.ShopItemType.KITCHEN_FLOOR:
                var kitchen_floor_transform = MainLocationOjects.instance.kitchen_floor.transform;
                for (int i = 0; i < MainLocationOjects.instance.kitchen_floor.transform.childCount; ++i)
                {
                    kitchen_floor_transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                        p.item_sprite;
                }
                break;

            case MainScene.ShopItemType.KITCHEN_WALL:
                var kitchen_wall_transform = MainLocationOjects.instance.kitche_wall.transform;
                for (int i = 0; i < MainLocationOjects.instance.kitche_wall.transform.childCount; ++i)
                {
                    kitchen_wall_transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                        p.item_sprite;
                }
                break;
            case MainScene.ShopItemType.GARDEN1_FLOOR:
                var garden_floor_transform = MainLocationOjects.instance.garden1_floor.transform;
                for (int i = 0; i < MainLocationOjects.instance.garden1_floor.transform.childCount; ++i)
                {
                    garden_floor_transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                        p.item_sprite;
                }
                break;
            case MainScene.ShopItemType.HOME_WALL:
                var home_wall_transform = MainLocationOjects.instance.home_wall.transform;
                for (int i = 0; i < MainLocationOjects.instance.home_wall.transform.childCount; ++i)
                {
                    home_wall_transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                        p.item_sprite;
                }

                var home_wall_win_transform = MainLocationOjects.instance.home_wall_win.transform;
                for (int i = 0; i < MainLocationOjects.instance.home_wall_win.transform.childCount; ++i)
                {
                    home_wall_win_transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                        ResourceHelper.LoadSprite(p.item_sprite.name + "_win");
                }

                var home_wall_door_transform = MainLocationOjects.instance.home_wall_door.transform;
                for (int i = 0; i < MainLocationOjects.instance.home_wall_door.transform.childCount; ++i)
                {
                    home_wall_door_transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                        ResourceHelper.LoadSprite(p.item_sprite.name + "_door");
                }

                break;

            case MainScene.ShopItemType.HOME_SOFA:
                var home_sofa = MainLocationOjects.instance.tv_zone;

                var postfix = "";

                if(p.item_sprite.name.Count(f => f == '_') == 2)
                {
                    postfix = "_" + p.item_sprite.name.Split('_').Last<string>();
                }

                home_sofa.transform.Find("sofa_001").GetComponent<SpriteRenderer>().sprite =
                        ResourceHelper.LoadSprite(p.item_sprite.name);

                home_sofa.transform.Find("chair_001").GetComponent<SpriteRenderer>().sprite =
                        ResourceHelper.LoadSprite("chair_001" + postfix);

                home_sofa.transform.Find("chair_002").GetComponent<SpriteRenderer>().sprite =
                        ResourceHelper.LoadSprite("chair_002" + postfix);
                break;

            case MainScene.ShopItemType.HOME_BED:

                var home_bad = MainLocationOjects.instance.sleep_room;

                home_bad.transform.Find("bad_001").GetComponent<SpriteRenderer>().sprite =
                    ResourceHelper.LoadSprite(p.item_sprite.name);  

                break;

            case MainScene.ShopItemType.GARDEN1_BOOSHES:

                var garden1_bushes = MainLocationOjects.instance.garden1_bushes;
                var pstfix = "";

                if (p.item_sprite.name.Count(f => f == '_') == 3)
                {
                    pstfix = "_0";
                }

                var long_up = garden1_bushes.transform.Find("long_up");
                for (int i = 0; i < long_up.childCount; ++i)
                {
                    long_up.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                        ResourceHelper.LoadSprite("garden_bush_004" + pstfix);
                }

                var long_down = garden1_bushes.transform.Find("long_down");
                for (int i = 0; i < long_down.childCount; ++i)
                {
                    long_down.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                        ResourceHelper.LoadSprite("garden_bush_003" + pstfix);
                }

                var big = garden1_bushes.transform.Find("big");
                for (int i = 0; i < big.childCount; ++i)
                {
                    big.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                        ResourceHelper.LoadSprite("garden_bush_001" + pstfix);
                }

                var small = garden1_bushes.transform.Find("small");
                for (int i = 0; i < small.childCount; ++i)
                {
                    small.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                        ResourceHelper.LoadSprite("garden_bush_002" + pstfix);
                }

                break;

            case MainScene.ShopItemType.GARDEN1_BENCHES:

                var garden1_benches = MainLocationOjects.instance.garden1_benches;
                var pref = p.item_sprite.name.Split('_')[0];

                var up = garden1_benches.transform.Find("up");
                for (int i = 0; i < up.childCount; ++i)
                {
                    up.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                        ResourceHelper.LoadSprite(pref + "_u");
                }

                var down = garden1_benches.transform.Find("down");
                for (int i = 0; i < down.childCount; ++i)
                {
                    down.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                        ResourceHelper.LoadSprite(pref + "_d");
                }

                var back = garden1_benches.transform.Find("back");
                for (int i = 0; i < back.childCount; ++i)
                {
                    back.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                        ResourceHelper.LoadSprite(pref + "_b");
                }

                break;
        }

    }

    // Use this for initialization
    override public void ExtendedStart()
    {
    }

    // Update is called once per frame
    override public void ExtendedUpdate()
    {
    }
}
