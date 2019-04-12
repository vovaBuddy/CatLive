using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.Storage;
using System;


public enum GAME_EVENT
{
    NON,
    SCANNED,
    NEW_RANK_REACHED,
    WON_MINIGAME,
    LOSE_MINIGAME,
    LOCATION_CUSTOMIZED,

}

[Serializable]
class WorldStateData
{
    public GAME_EVENT last_game_event;
    public DateTime last_customize_date;
    public bool need_first_booster_tutor;

    public WorldStateData()
    {
        last_game_event = GAME_EVENT.NON;
        last_customize_date = DateTime.Now;
        need_first_booster_tutor = false;
    }
}

public class WorldStateDataStorage
{
    StorableData<WorldStateData> data;

    public WorldStateDataStorage()
    {
        data = new StorableData<WorldStateData>("WorldStateData");
    }

    public bool need_first_booster_tutor
    {
        get { return data.content.need_first_booster_tutor; }
        set { data.content.need_first_booster_tutor = value; data.Store(); }
    }


    public void CustomizeToday()
    {
        data.content.last_customize_date = DateTime.Now;
    }
    public int DaysWithOutCusomize
    {
        private set { }
        get
        {
            return (int)(DateTime.Now - data.content.last_customize_date).TotalDays;
        }
    }

    public GAME_EVENT last_game_event
    {
        get
        {
            return data.content.last_game_event;
        }
        set
        {
            data.content.last_game_event = value;
            data.Store();
        }
    }
}

