using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;
using Yaga;
using UnityEngine.UI;
using System;

namespace MainScene
{
    [Serializable]
    public class StoreItemData
    {
        public Sprite shop_sprite;
        public ShopItemType type;
        public Sprite item_sprite;
        public Texture item_texture;
        public int beauty_value;
        public int price;
    }

    public enum ShopItemType
    {
        HEADDRESS_CAP = 0,
        HEADDRESS_BOW = 1,
        COLLAR = 2,
        GLASSE = 3,
        EYE = 4,
        SKIN = 5,

        KITCHEN_SET = 6,
        KITCHEN_FLOOR = 7,
        KITCHEN_WALL = 8,

        HOME_FLOOR = 9,
        HOME_WALL = 10,

        GARDEN1_FLOOR = 11,
        GARDEN1_BOOSHES = 15,

        GARDEN1_BENCHES = 12,

        HOME_SOFA = 13,
        HOME_BED = 14,

    }

    
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class ShopItem : ExtendedBehaviour
    {
        public Sprite shop_sprite;
        public ShopItemType type;
        public Texture item_texture;
        public Sprite item_sprite;

        public int price;
        public int beauty_value;

        public Text beauty_value_text;
        public GameObject beauty;
        public Text btn_text;
        public Text prebuy_btn_text;
        public Button btn_buy;
        public Button btn_preview;
        public Button btn_takeoff;
        public Button btn_select;

        public GameObject calm_state;
        public GameObject prebuy_state;
        public GameObject buy_state;
        public GameObject active_state;

        public GameObject btn_icon;
        public GameObject sprite;
        public GameObject active_sprite;
        public GameObject calm_sprite;

        public GameObject cost_money;
        public GameObject cost_adv;
        public GameObject cost_retention;
        public Text cost_retention_text;

        public GameObject buy_money;
        public GameObject buy_adv;
        public GameObject buy_retention;
        public Text buy_retention_text;

        public static void InstantiateItem(GameObject prefab, Transform parent, StoreItemData item)
        {
            var new_item = Instantiate(prefab);
            var si = new_item.GetComponent<ShopItem>();
            si.shop_sprite = item.shop_sprite;
            si.type = item.type;
            si.item_texture = item.item_texture;
            si.beauty_value = item.beauty_value;
            si.price = item.price;

            new_item.transform.parent = parent;
            new_item.transform.localScale = new Vector3(1, 1, 1);
        }


        bool bought;

        [Subscribe(MainMenuMessageType.CHECK_DAILY_CONDITION)]
        public void CheckDailyCond(Message msg)
        {
            if (price > -2)
                return;

            price += 1;

            if(price == -1)
            {
                price = 1000;
                var param = new BuyItemParametr();
                param.price = price;
                param.beauty_value = beauty_value;
                param.type = type;
                param.item_texture = item_texture;
                param.item_sprite = item_sprite;
                Message ret_msg = new Message();
                ret_msg.parametrs = param;
                ret_msg.Type = MainMenuMessageType.BOUGHT_ITEM;
                MessageBus.Instance.SendMessage(ret_msg);
            }
        }

        [Subscribe(MainMenuMessageType.TAKE_OFF_PREVIEW_ITEM)]
        public void TakeOffSubs(Message msg)
        {
            if(buy_state != null && buy_state.activeSelf)
            {
                buy_state.SetActive(false);
                buy_state.GetComponent<Animator>().SetBool("buy", false);
                prebuy_state.SetActive(true);
            }
        }

        [Subscribe(MainMenuMessageType.PREVIEW_ITEM)]
        public void Preview(Message msg)
        {
            var p = Yaga.Helpers.CastHelper.Cast<BuyItemParametr>(msg.parametrs);

            if (string.Equals(p.item_texture == null ? p.item_sprite.name : p.item_texture.name,
                item_texture == null ? item_sprite.name : item_texture.name))
            {
                prebuy_state.SetActive(false);
                buy_state.SetActive(true);
                buy_state.GetComponent<Animator>().SetBool("buy", true);
            }
            else if(buy_state != null && buy_state.activeSelf)
            {
                buy_state.SetActive(false);
                active_state.SetActive(false);
                calm_state.SetActive(false);
                prebuy_state.SetActive(true);
            }
            else if(active_state != null && active_state.activeSelf && p.type == type)
            {
                active_state.SetActive(false);
                prebuy_state.SetActive(false);
                buy_state.SetActive(false);
                calm_state.SetActive(true);
            }
        }

        [Subscribe(MainMenuMessageType.BOUGHT_ITEM,
            MainMenuMessageType.DRESS_ITEM, 
            MainMenuMessageType.PRE_DRESS_ITEM)]
        public void BoughtItem(Message msg)
        {
            var p = Yaga.Helpers.CastHelper.Cast<BuyItemParametr>(msg.parametrs);

            if (string.Equals(p.item_texture == null ? p.item_sprite.name : p.item_texture.name,
                item_texture == null ? item_sprite.name : item_texture.name))
            {
                bought = true;

                buy_state.SetActive(false);
                prebuy_state.SetActive(false);
                active_state.SetActive(true);
                calm_state.SetActive(false);
            }

            else if ((bought || price == 0) &&
                type == p.type)
            {
                buy_state.SetActive(false);
                prebuy_state.SetActive(false);
                active_state.SetActive(false);
                calm_state.SetActive(true);
            }
            else if(type == p.type)
            {
                buy_state.SetActive(false);
                prebuy_state.SetActive(true);
                active_state.SetActive(false);
                calm_state.SetActive(false);
            }
        }

        [Subscribe(MainMenuMessageType.TAKEOFF_ITEM)]
        public void TakeOff(Message msg)
        {
            var p = Yaga.Helpers.CastHelper.Cast<BuyItemParametr>(msg.parametrs);

            if (string.Equals(p.item_texture == null ? p.item_sprite.name : p.item_texture.name,
                item_texture == null ? item_sprite.name : item_texture.name))
            {
                buy_state.SetActive(false);
                prebuy_state.SetActive(!bought);
                active_state.SetActive(false);
                calm_state.SetActive(bought);
            }
        }

        [Subscribe(MainMenuMessageType.INIT_BOUGHT_ITEM)]
        public void InitBought(Message msg)
        {
            var p = Yaga.Helpers.CastHelper.Cast<BuyItemParametr>(msg.parametrs);

            if (string.Equals(p.item_texture == null ? p.item_sprite.name : p.item_texture.name,
                item_texture == null ? item_sprite.name : item_texture.name))
            {
                bought = true;
                calm_state.SetActive(true);
                prebuy_state.SetActive(false);
             //   btn_select.SetActive(true);
            }
        }

        // Use this for initialization
        override public void ExtendedStart()
        {

            sprite.GetComponent<Image>().sprite = shop_sprite;
            btn_icon.GetComponent<Image>().sprite = shop_sprite;
            active_sprite.GetComponent<Image>().sprite = shop_sprite;
            calm_sprite.GetComponent<Image>().sprite = shop_sprite;

            btn_text.text = price.ToString();
            prebuy_btn_text.text = price.ToString();

            if(price < -1)
            {
                cost_retention.SetActive(true);
                buy_retention.SetActive(true);
                cost_money.SetActive(false);
                buy_money.SetActive(false);
                cost_adv.SetActive(false);
                buy_adv.SetActive(false);

                buy_retention_text.text = ((price + 1) * (-1)).ToString();
                cost_retention_text.text = ((price + 1) * (-1)).ToString();
            }

            else if(price == -1)
            {
                cost_retention.SetActive(false);
                buy_retention.SetActive(false);
                cost_money.SetActive(false);
                buy_money.SetActive(false);
                cost_adv.SetActive(true);
                buy_adv.SetActive(true);
            }

            else if(price == 0)
            {
                buy_state.SetActive(false);
                prebuy_state.SetActive(false);
                active_state.SetActive(false);
                calm_state.SetActive(true);
            }

            else
            {
                buy_state.SetActive(false);
                prebuy_state.SetActive(true);
                active_state.SetActive(false);
                calm_state.SetActive(false);
            }


            if (beauty_value == 0)
            {
                beauty.SetActive(false);
            }
            else
            {
                beauty_value_text.text = beauty_value.ToString();
            }

            btn_preview.GetComponent<Button>().onClick.AddListener(() =>
            {
                var param = new BuyItemParametr();
                param.price = price;
                param.beauty_value = beauty_value;
                param.type = type;
                param.item_texture = item_texture;
                param.item_sprite = item_sprite;
                Message msg = new Message();
                msg.parametrs = param;
                msg.Type = MainMenuMessageType.PREVIEW_ITEM;
                MessageBus.Instance.SendMessage(msg);
            });

            btn_buy.GetComponent<Button>().onClick.AddListener(() =>
            {
                var param = new BuyItemParametr();
                param.price = price;
                param.beauty_value = beauty_value;
                param.type = type;
                param.item_texture = item_texture;
                param.item_sprite = shop_sprite;
                Message msg = new Message();
                msg.parametrs = param;
                msg.Type = MainMenuMessageType.PREBUY_ITEM;
                MessageBus.Instance.SendMessage(msg);
            });

            btn_select.GetComponent<Button>().onClick.AddListener(() =>
            {
                var param = new BuyItemParametr();
                param.price = price;
                param.beauty_value = beauty_value;
                param.type = type;
                param.item_texture = item_texture;
                param.item_sprite = item_sprite;
                Message msg = new Message();
                msg.parametrs = param;
                msg.Type = MainMenuMessageType.PRE_DRESS_ITEM;
                MessageBus.Instance.SendMessage(msg);
            });

            btn_takeoff.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (type > ShopItemType.GLASSE)
                    return;
                var param = new BuyItemParametr();
                param.price = price;
                param.beauty_value = beauty_value;
                param.type = type;
                param.item_texture = item_texture;
                param.item_sprite = item_sprite;
                Message msg = new Message();
                msg.parametrs = param;
                msg.Type = MainMenuMessageType.TAKEOFF_ITEM;
                MessageBus.Instance.SendMessage(msg);
            });
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {

        }
    }
}