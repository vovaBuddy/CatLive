using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.Storage;
using System;

public class BusterEntity
{
    public List<Buster> active_busters;
    public Dictionary<BusterType, int[]> upgrate_prices;


    [Serializable]
    public class BusterStoreInfo
    {
        public int level;
        public int count;

        public BusterStoreInfo(int lvl, int cnt)
        {
            level = lvl;
            count = cnt;
        }
    }

    [Serializable]
    public class BusterStorage
    {
        public Dictionary<BusterType, BusterStoreInfo> busters;

        public BusterStorage()
        {
            busters = new Dictionary<BusterType, BusterStoreInfo>();
        }
    }

    public StorableData<BusterStorage> buster_storage;

    public void UncountBuster(BusterType type)
    {
        buster_storage.content.busters[type].count -= 1;
        buster_storage.Store();
    }
    public int getLevel(BusterType type)
    {
        return buster_storage.content.busters[type].level;
    }

    public void UpgrateBuster(BusterType type)
    {
        buster_storage.content.busters[type].level += 1;
        buster_storage.Store();
    }

    public void BougthBuster(BusterType type, int cnt = 3)
    {
        if(!buster_storage.content.busters.ContainsKey(type))
        {
            buster_storage.content.busters.Add(type, new BusterStoreInfo(1, cnt));
        }
        else
        {
            buster_storage.content.busters[type].count += cnt;
        }

        buster_storage.Store();
    }

    public int GetPrice(BusterType t)
    {
        return upgrate_prices[t][getLevel(t)];
    }

    public BusterEntity()
    {
        upgrate_prices = new Dictionary<BusterType, int[]>();
        upgrate_prices.Add(BusterType.REBORN, new int[] { 10, 20, 30, 40, 50, 60, 1000000 });
        upgrate_prices.Add(BusterType.FLY, new int[] { 10, 20, 30, 40, 50, 60, 1000000 });
        upgrate_prices.Add(BusterType.MAGNIT, new int[] { 10, 20, 30, 40, 50, 60, 1000000 });
        buster_storage = new StorableData<BusterStorage>("buster_storage");
        //BougthBuster(BusterType.MAGNIT);
        //BougthBuster(BusterType.FLY);
        //BougthBuster(BusterType.REBORN);
        active_busters = new List<Buster>();

        //
        //active_busters.Add(new MagnitBuster(1));
        //active_busters.Add(new FlyBuster(1));
        //active_busters.Add(new RebornBuster(1));
    }

}
