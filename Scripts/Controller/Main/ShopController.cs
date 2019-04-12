using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using System;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class ShopController : ExtendedBehaviour
    {
        Message cur_prew_message;

        public void SelectShopItems(int shop)
        {
            Message msg = new Message();
            msg.Type = MainMenuMessageType.SELECT_SHOP_ITEM;
            msg.parametrs = new ShopTypeParametr((ShopItemType)shop);
            MessageBus.Instance.SendMessage(msg);

            cur_prew_message.Type = MainMenuMessageType.TAKE_OFF_PREVIEW_ITEM;
            MessageBus.Instance.SendMessage(cur_prew_message);
        }

        [Subscribe(MainMenuMessageType.PREVIEW_ITEM)]
        public void PrewItem(Message msg)
        {
            cur_prew_message = msg;
        }

        public void OpenShop()
        {
            MessageBus.Instance.SendMessage(MainMenuMessageType.OPEN_SHOP);
            MainScene.ArrowController.Instance.arrow_shop.SetActive(false);
        }

        public void CloseShop()
        {
            MessageBus.Instance.SendMessage(MainMenuMessageType.CLOSE_SHOP);

            cur_prew_message.Type = MainMenuMessageType.TAKE_OFF_PREVIEW_ITEM;
            MessageBus.Instance.SendMessage(cur_prew_message);
        }

        public GameObject shop_item_prefub;
        public GameObject skin_container;
        public List<StoreItemData> skin_items;

        public GameObject glasses_container;
        public List<StoreItemData> glasses_items;

        public GameObject head_container;
        public List<StoreItemData> head_items;

        public GameObject eye_container;
        public List<StoreItemData> eye_items;

        public GameObject collar_container;
        public List<StoreItemData> collar_items;

        void InstantinateContent()
        {
            foreach(var item in skin_items)
            {
                ShopItem.InstantiateItem(shop_item_prefub, skin_container.transform, item);
            }

            foreach (var item in glasses_items)
            {
                ShopItem.InstantiateItem(shop_item_prefub, glasses_container.transform, item);
            }

            foreach (var item in head_items)
            {
                ShopItem.InstantiateItem(shop_item_prefub, head_container.transform, item);
            }

            foreach (var item in collar_items)
            {
                ShopItem.InstantiateItem(shop_item_prefub, collar_container.transform, item);
            }

            foreach (var item in eye_items)
            {
                ShopItem.InstantiateItem(shop_item_prefub, eye_container.transform, item);
            }
        }

        // Use this for initialization
        public override void ExtendedStart()
        {
            InstantinateContent();
        }
    }
}
