using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Yaga.Storage;


public class CatRang
{
    [Serializable]
    public class CatRangData
    {
        public int cur_rang;
        public int scans_count;

        public CatRangData()
        {
            cur_rang = 0;
            scans_count = 1;
        }
    }

    static readonly int[] rangs_value = new int[6] { 1, 4, 5, 10, 15, 20 };

    StorableData<CatRangData> cat_rang_data;

    public int GetRangCount()
    {
        return CatRang.rangs_value.Length;
    }

    public void IncreeseRang()
    {
        cat_rang_data.content.cur_rang += 1;
        cat_rang_data.Store();
    }

    public void DecreeseCount()
    {
        cat_rang_data.content.scans_count -= 1;
        cat_rang_data.Store();
    }
    public void IncreeseCount()
    {
        cat_rang_data.content.scans_count += 1;
        cat_rang_data.Store();
    }

    public int curRang
    {
        get { return cat_rang_data.content.cur_rang; }
    }
    public int scansCount
    {
        get { return cat_rang_data.content.scans_count; }
        set { cat_rang_data.content.scans_count = value; cat_rang_data.Store(); }
    }

    public int getNeedOpenCat()
    {
        return CatRang.rangs_value[cat_rang_data.content.cur_rang];
    }
    public int getNeedOpenCatByRang(int value)
    {
        return CatRang.rangs_value[value];
    }

    public CatRang()
    {
        cat_rang_data = new StorableData<CatRangData>("cat_rang_data");
    }
}
