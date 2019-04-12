using System;
using System.Collections.Generic;
using Yaga.Storage;
using Yaga.MessageBus;

public class CatsPurse
{
    StorableData<CatsPurseEntity> entity;
    StorableData<NameEntity> name_entity;
    ServeredData serv_beauty;
    public ServeredTimer inf_h_timer;
    bool infinity_hearts;

    [Serializable]
    class NameEntity
    {
        public string name;

        public NameEntity()
        {
            name = Helper.DeviceNameHelper.GetDeviceName();
        }
    }


    [Serializable]
    class CatsPurseEntity
    {
        public int coins;
        public int stars;
        public int hearts;
        public int energy;
        public int beauty;
        public int wear_beauty;
        public bool[] beauty_prizes;

        public CatsPurseEntity()
        {
            coins = 1000;
            stars = 0;
            hearts = 5;
            energy = 1;
            beauty = 0;
            wear_beauty = 0;

            beauty_prizes = new bool[17];
        }
    }

    public bool InfinityHearts
    {
        get
        {
            return infinity_hearts;
        }
        set
        {
            infinity_hearts = value;
        }
    }

    public void RefreshPrizes()
    {
        for(int i = 0; i < entity.content.beauty_prizes.Length; ++i)
        {
            entity.content.beauty_prizes[i] = false;
        }
        entity.Store();
    }

    public bool HasPrize(int i)
    {
        return entity.content.beauty_prizes[i];
    }

    public void SetPrize(int i)
    {
        entity.content.beauty_prizes[i] = true;
        entity.Store();
    }

    public CatsPurse()
    {
        entity = new StorableData<CatsPurseEntity>("CatsPurseEntity");
        name_entity = new StorableData<NameEntity>("name_entity");
        serv_beauty = new ServeredData();
        inf_h_timer = new ServeredTimer();
        infinity_hearts = false;
    }

    public string Name
    {
        get { return name_entity.content.name; }
        set { name_entity.content.name = value; name_entity.Store(); }
    }

    public int Coins
    {
        get { return entity.content.coins; }
        set {
            entity.content.coins = value;
            entity.Store();

            Message msg = new Message();
            msg.parametrs = new UpdateInt(entity.content.coins);
            msg.Type = MainScene.MainMenuMessageType.UPDATE_CATSHOW_COINS;
            MessageBus.Instance.SendMessage(msg, true);
        }
    }

    public int Stars
    {
        get { return entity.content.stars; }
        set {
            entity.content.stars = value;
            entity.Store();

            Message msg = new Message();
            msg.parametrs = new UpdateInt(entity.content.stars);
            msg.Type = MainScene.MainMenuMessageType.UPDATE_STARS;
            MessageBus.Instance.SendMessage(msg);
        }
    }

    public int Hearts
    {
        get { return entity.content.hearts; }
        set {
            entity.content.hearts = value;

            if (entity.content.hearts > 5)
                entity.content.hearts = 5;

            entity.Store();

            Message msg = new Message();
            msg.parametrs = new UpdateInt(entity.content.hearts);
            msg.Type = MainScene.MainMenuMessageType.UPDATE_HEARTS;
            MessageBus.Instance.SendMessage(msg);
        }
    }

    public int Energy
    {
        get { return entity.content.energy; }
        set {
            entity.content.energy = value;

            if (entity.content.energy > 5)
                entity.content.energy = 5;

            entity.Store();

            Message msg = new Message();
            msg.parametrs = new UpdateInt(entity.content.energy);
            msg.Type = MainScene.MainMenuMessageType.UPDATE_ENERGY;
            MessageBus.Instance.SendMessage(msg);
        }
    }

    public int Beauty
    {
        get { return entity.content.beauty; }
        set
        {
            serv_beauty.SetValue(value);

            entity.content.beauty = value;
            entity.Store();
        }
    }


    public int head_beauty = 0;
    public int skin_beauty = 1;
    public int eye_beauty = 1;
    public int glasses_beauty = 0;
    public int collar_beauty = 0;

    public int WearBeauty
    {
        get { return head_beauty + skin_beauty + eye_beauty + glasses_beauty + collar_beauty; }
    }
}