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
public class LocationCustomizationController : ExtendedBehaviour
{

    [Serializable]
    enum ResourceType
    {
        TEXTURE,
        SPRITE,
    }

    [Serializable]
    class LocationItem
    {
        public MainScene.ShopItemType type;
        public string res_name;
        public ResourceType res_type;

        public LocationItem()
        {

        }

        public LocationItem(MainScene.ShopItemType t, object res, ResourceType rt)
        {
            type = t;
            res_type = rt;
            switch (rt)
            {
                case ResourceType.SPRITE:
                    res_name = ((Sprite)res).name;
                    break;
                case ResourceType.TEXTURE:
                    res_name = ((Texture)res).name;
                    break;
            }
        }
    }

    [Serializable]
    class LocationEntity
    {
        public List<LocationItem> location_items;
        public List<string> bought_resources;

        public LocationEntity()
        {
            location_items = new List<LocationItem>();
            bought_resources = new List<string>();
        }
    }

    StorableData<LocationEntity> wear_entity;

    private Dictionary<MainScene.ShopItemType, string> default_sprites;

    [Subscribe(MainScene.MainMenuMessageType.TAKEOFF_ITEM)]
    public void TakeOff(Message msg)
    {
        var param = CastHelper.Cast<MainScene.BuyItemParametr>(msg.parametrs);

        if (param.type < MainScene.ShopItemType.KITCHEN_SET)
            return;

        for (int i = 0; i < wear_entity.content.location_items.Count; ++i)
        {
            if (wear_entity.content.location_items[i].type == param.type)
            {
                wear_entity.content.location_items.RemoveAt(i);
                break;
            }
        }

        wear_entity.Store();
    }

    [Subscribe(MainScene.MainMenuMessageType.DRESS_ITEM)]
    public void Dress(Message msg)
    {
        var param = CastHelper.Cast<MainScene.BuyItemParametr>(msg.parametrs);

        if (param.type < MainScene.ShopItemType.KITCHEN_SET)
            return;

        bool finded = false;

        for (int i = 0; i < wear_entity.content.location_items.Count; ++i)
        {
            if (wear_entity.content.location_items[i].type == param.type)
            {
                finded = true;
                wear_entity.content.location_items[i].res_name =
                    param.item_texture == null ? param.item_sprite.name : param.item_texture.name;
                break;
            }
        }

        if (!finded)
        {
            wear_entity.content.location_items.Add(
                new LocationItem(param.type, param.item_texture == null ? (object)param.item_sprite : (object)param.item_texture, param.item_texture == null ? ResourceType.SPRITE : ResourceType.TEXTURE));
        }

        wear_entity.Store();
    }

    [Subscribe(MainScene.MainMenuMessageType.TAKE_OFF_PREVIEW_ITEM)]
    public void TakeOffPreview(Message msg)
    {
        if (msg.parametrs == null)
            return;

        var param = CastHelper.Cast<MainScene.BuyItemParametr>(msg.parametrs);

        if (param.type < MainScene.ShopItemType.KITCHEN_SET)
            return;

        for (int i = 0; i < wear_entity.content.location_items.Count; ++i)
        {
            if (wear_entity.content.location_items[i].type == param.type)
            {
                msg.Type = MainScene.MainMenuMessageType.DRESS_ITEM;
                param.item_sprite = ResourceHelper.LoadSprite(wear_entity.content.location_items[i].res_name);
                msg.parametrs = param;
                MessageBus.Instance.SendMessage(msg);

                return;
            }
        }

        msg.Type = MainScene.MainMenuMessageType.DRESS_ITEM;
        param.item_sprite = ResourceHelper.LoadSprite(default_sprites[param.type]);
        msg.parametrs = param;
        MessageBus.Instance.SendMessage(msg);
    }

    [Subscribe(MainScene.MainMenuMessageType.CHECK_BOUGHT)]
    public void check_bought(Message msg)
    {
        var param = CastHelper.Cast<MainScene.BuyItemParametr>(msg.parametrs);

        if (param.type < MainScene.ShopItemType.KITCHEN_SET)
            return;

        if (!wear_entity.content.bought_resources.Contains(default_sprites[param.type]))
        {
            msg.Type = MainScene.MainMenuMessageType.DRESS_ITEM;
            param.item_sprite = ResourceHelper.LoadSprite(default_sprites[param.type]);
            msg.parametrs = param;
            MessageBus.Instance.SendMessage(msg);
        }

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

        if (param.type < MainScene.ShopItemType.KITCHEN_SET)
            return;

        msg.Type = MainScene.MainMenuMessageType.PRE_DRESS_ITEM;
        MessageBus.Instance.SendMessage(msg);

        if (!wear_entity.content.bought_resources.Contains(
            param.item_texture == null ? param.item_sprite.name : param.item_texture.name))
            wear_entity.content.bought_resources.Add(
                param.item_texture == null ? param.item_sprite.name : param.item_texture.name);

        wear_entity.Store();

        //todo
        if(param.type == MainScene.ShopItemType.KITCHEN_SET)
        {
            DataController.instance.tasks_storage.content["custom_location_tutor"] = true;
            DataController.instance.tasks_storage.Store();
        }
        else if(param.type == MainScene.ShopItemType.HOME_FLOOR)
        {
            DataController.instance.tasks_storage.content["floor_customized"] = true;
            DataController.instance.tasks_storage.Store();
        }
    }

    void Dress()
    {
        Message msg = new Message();
        msg.Type = MainScene.MainMenuMessageType.DRESS_ITEM;
        var param = new MainScene.BuyItemParametr();

        foreach (var item in wear_entity.content.location_items)
        {
            if (item.res_type == ResourceType.TEXTURE)
            {
                param.item_texture = ResourceHelper.LoadTexture(item.res_name);
            }
            else
            {
                param.item_sprite = ResourceHelper.LoadSprite(item.res_name);
            }
            param.type = item.type;
            param.beauty_value = 0;
            msg.parametrs = param;

            MessageBus.Instance.SendMessage(msg);
        }
    }

    // Use this for initialization
    override public void ExtendedStart()
    {
        wear_entity = new StorableData<LocationEntity>("location_entity");

        default_sprites = new Dictionary<MainScene.ShopItemType, string>();
        default_sprites.Add(MainScene.ShopItemType.HOME_WALL, "wall_hs_tile_001");
        default_sprites.Add(MainScene.ShopItemType.HOME_FLOOR, "floor_tile_022");
        default_sprites.Add(MainScene.ShopItemType.KITCHEN_FLOOR, "floor_ktch_tile_001");
        default_sprites.Add(MainScene.ShopItemType.KITCHEN_WALL, "wall_ktch_tile_001");
        default_sprites.Add(MainScene.ShopItemType.KITCHEN_SET, "kitchen_001");
        default_sprites.Add(MainScene.ShopItemType.HOME_SOFA, "sofa_001");
        default_sprites.Add(MainScene.ShopItemType.HOME_BED, "bad_001");
        default_sprites.Add(MainScene.ShopItemType.GARDEN1_BENCHES, "bench0_b");
        default_sprites.Add(MainScene.ShopItemType.GARDEN1_BOOSHES, "garden_bush_003_0");

        //для того чтобы проинициализировать нужны видимые объекты
        //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_CUSTOMIZER);
    }

    bool need_update = true;
    override public void ExtendedUpdate()
    {
        if (need_update)
        {
            Dress();

            Message msg = new Message();
            msg.Type = MainScene.MainMenuMessageType.INIT_BOUGHT_ITEM;
            var param = new MainScene.BuyItemParametr();
            foreach (string name in wear_entity.content.bought_resources)
            {
                param.item_texture = ResourceHelper.LoadTexture(name);
                param.item_sprite = ResourceHelper.LoadSprite(name);
                //param.item_texture = ResourceHelper.LoadTexture(name);
                msg.parametrs = param;
                MessageBus.Instance.SendMessage(msg);
            }

            need_update = false;

            MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_CUSTOMIZER);
        }
    }
}
