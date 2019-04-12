using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Yaga.Storage;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using UnityEngine.Analytics;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class WardrobeController : ExtendedBehaviour {

    [Serializable]
    class WearItem
    {
        public MainScene.ShopItemType type;
        public string texture_name;
        public int beauty_value;

        public WearItem()
        {

        }

        public WearItem(MainScene.ShopItemType t, Texture txtr, int v)
        {
            type = t;
            texture_name = txtr.name;
            beauty_value = v;
        }
    }

    [Serializable]
    class WearEntity
    {
        public List<WearItem> wear_items;
        public List<string> bought_textures;

        public WearEntity()
        {
            wear_items = new List<WearItem>();
            bought_textures = new List<string>();
        }
    }

    StorableData<WearEntity> wear_entity;

    [Subscribe(MainScene.MainMenuMessageType.TAKEOFF_ITEM)]
    public void TakeOff(Message msg)
    {
        var param = CastHelper.Cast<MainScene.BuyItemParametr>(msg.parametrs);

        if (param.type >= MainScene.ShopItemType.KITCHEN_SET)
            return;

        //if (param.type == MainScene.ShopItemType.SKIN || param.type == MainScene.ShopItemType.EYE)
        //    return;

        for (int i = 0; i < wear_entity.content.wear_items.Count; ++i)
        {
            if (wear_entity.content.wear_items[i].type == param.type)
            {
                wear_entity.content.wear_items.RemoveAt(i);
                break;
            }
        }

        wear_entity.Store();
    }

    [Subscribe(MainScene.MainMenuMessageType.TAKE_OFF_PREVIEW_ITEM)]
    public void TakeOffPreview(Message msg)
    {
        var param = CastHelper.Cast<MainScene.BuyItemParametr>(msg.parametrs);

        if (param == null)
            return;

        if (param.type >= MainScene.ShopItemType.KITCHEN_SET)
            return;

        for (int i = 0; i < wear_entity.content.wear_items.Count; ++i)
        {
            if (wear_entity.content.wear_items[i].type == param.type)
            {
                msg.Type = MainScene.MainMenuMessageType.DRESS_ITEM;
                param.item_texture = ResourceHelper.LoadTexture(wear_entity.content.wear_items[i].texture_name);
                msg.parametrs = param;
                MessageBus.Instance.SendMessage(msg);

                return;
            }
        }

        msg.Type = MainScene.MainMenuMessageType.TAKEOFF_ITEM;
        MessageBus.Instance.SendMessage(msg);
    }

    [Subscribe(MainScene.MainMenuMessageType.DRESS_ITEM)]
    public void Dress(Message msg)
    {
        var param = CastHelper.Cast<MainScene.BuyItemParametr>(msg.parametrs);
        bool finded = false;

        if (param.type >= MainScene.ShopItemType.KITCHEN_SET)
            return;

        for (int i = 0; i < wear_entity.content.wear_items.Count; ++i)
        {
            if (wear_entity.content.wear_items[i].type == param.type)
            {
                finded = true;
                wear_entity.content.wear_items[i].beauty_value = param.beauty_value;
                wear_entity.content.wear_items[i].texture_name = param.item_texture.name;
                break;
            }
        }

        if (!finded)
        {
            wear_entity.content.wear_items.Add(
                new WearItem(param.type, param.item_texture, param.beauty_value));
        }

        wear_entity.Store();
    }

    [Subscribe(MainScene.MainMenuMessageType.BOUGHT_ITEM)]
    public void Wear(Message msg)
    {
        var param = CastHelper.Cast<MainScene.BuyItemParametr>(msg.parametrs);

        Analytics.CustomEvent("BOUGHT_ITEM", new Dictionary<string, object>
        {
            { "type", param.type },
            { "name", param.item_sprite != null ? param.item_sprite.name.ToString() : param.item_texture.name.ToString() }
        });

        if (param.type >= MainScene.ShopItemType.KITCHEN_SET)
            return;

        //msg.Type = MainScene.MainMenuMessageType.DRESS_ITEM;
        //MessageBus.Instance.SendMessage(msg);

        if(!wear_entity.content.bought_textures.Contains(param.item_texture.name))
            wear_entity.content.bought_textures.Add(param.item_texture.name);

        wear_entity.Store();

        DataController.instance.tasks_storage.content["first_shopped"] = true;
        DataController.instance.tasks_storage.Store();
    }

    void Dress()
    {
        Message msg = new Message();
        msg.Type = MainScene.MainMenuMessageType.DRESS_ITEM;
        var param = new MainScene.BuyItemParametr();


        foreach (var item in wear_entity.content.wear_items)
        {
            param.item_texture = ResourceHelper.LoadTexture(item.texture_name);
            param.type = item.type;
            param.beauty_value = item.beauty_value;
            msg.parametrs = param;

            MessageBus.Instance.SendMessage(msg);
        }
    }

	// Use this for initialization
	override public void ExtendedStart () {
        wear_entity = new StorableData<WearEntity>("wear_entity");

        //для того чтобы проинициализировать нужны видимые объекты
        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_CAT_SHOW);
        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_SHOP);
    }

    public GameObject skins_conteiner;

    bool need_update = true;
    override public void ExtendedUpdate() {
		if(need_update)
        {
            Dress();

            Message msg = new Message();
            msg.Type = MainScene.MainMenuMessageType.INIT_BOUGHT_ITEM;
            var param = new MainScene.BuyItemParametr();
            foreach (string name in wear_entity.content.bought_textures)
            {
                param.item_texture = ResourceHelper.LoadTexture(name);
                msg.parametrs = param;
                MessageBus.Instance.SendMessage(msg);
            }

            need_update = false;

            MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_SHOP);
            MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_CAT_SHOW);
        }
	}
}
