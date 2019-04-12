using System;
using Yaga;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Yaga.MessageBus;
using Yaga.Storage;
using UnityEngine.Analytics;
using MainScene;
using Main.Chapter;
using Main.MinigamesScreen;

class DataController : DontDestroy<DataController>, Initable
{
    public CatsPurse catsPurse;
    public GamesRecords gamesRecords;
    public Dictionary<string, object> other_storage;
    public StorableData<Dictionary<string, object>> tasks_storage;
    public ChapterData chapter_data;
    public AdvEntity advEntity;
    public BusterEntity buster_entity;
    public PushEntity push_entity;
    public CatRang cat_rang;
    public MinigamesScreenData minigames_screen_data;
    public WorldStateDataStorage world_state_data;

    public const int HeartCost = 900;

    public void SetInfinitHearts(int time)
    {
        catsPurse.inf_h_timer.SetTime("infinity_heart", time);
        catsPurse.InfinityHearts = true;
    }
   
    public bool BuyHeartsForCoins()
    {
        if (catsPurse.Coins >= HeartCost)
        {
            catsPurse.Coins -= HeartCost;
            catsPurse.Hearts += 1;

            return true;
        }

        return false;
    }

    public void Init()
    {
        advEntity = new AdvEntity();
        catsPurse = new CatsPurse();
        gamesRecords = new GamesRecords();
        buster_entity = new BusterEntity();
        push_entity = new PushEntity();
        cat_rang = new CatRang();
        minigames_screen_data = new MinigamesScreenData();
        world_state_data = new WorldStateDataStorage();

        other_storage = new Dictionary<string, object>();
        tasks_storage = new StorableData<Dictionary<string, object>>("tasks_storage_help");
        chapter_data = new ChapterData();

        if (!tasks_storage.content.ContainsKey("all_done"))
        {
            tasks_storage.content["all_done"] = false;
            tasks_storage.Store();
        }

        Analytics.CustomEvent("catsPurse", new Dictionary<string, object>
            {
                { "coins", catsPurse.Coins },
            });

        Analytics.CustomEvent("gamesRecords", new Dictionary<string, object>
            {
                { "taptap_best_value", gamesRecords.Record(GameName.taptap.ToString()).best_value },
                { "zigzag_best_value", gamesRecords.Record(GameName.zigzag.ToString()).best_value },
            });
    }
}

